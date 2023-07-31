using GameMasterArena.DataAccess.Interfaces.Persons;
using GameMasterArena.Domain.Entities.Persons;
using GameMasterArena.Domain.Exceptions.Auth;
using GameMasterArena.Domain.Exceptions.Users;
using GameMasterArena.Service.Common.Helpers;
using GameMasterArena.Service.Common.Security;
using GameMasterArena.Service.Dtos.Notifications;
using GameMasterArena.Service.Dtos.Security;
using GameMasterArena.Service.Dtos.Persons;
using GameMasterArena.Service.Dtos.PersonsAuth;
using GameMasterArena.Service.Interfaces.Notifications;
using GameMasterArena.Service.Interfaces.Persons;
using Microsoft.Extensions.Caching.Memory;
using GetTalim.Service.Interfaces.Persons;
using GameMasterArena.Service.Services.Common;
using GameMasterArena.Service.Interfaces.Common;

namespace GameMasterArena.Service.Services.Students;

public class AuthPersonService : IAuthPersonService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IPerson _personRepository;
    private readonly IMailSender _mailSender;
    private readonly ITokenPersonService _tokenPersonService;
    private readonly IFileService _fileService;
    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;
    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;

    public AuthPersonService(IMemoryCache memoryCache,
        IPerson personRepository,
        IMailSender mailSender,
        ITokenPersonService tokenPersonService,
        IFileService fileService)
    {
        this._memoryCache = memoryCache;
        this._personRepository = personRepository;
        this._mailSender = mailSender;
        this._tokenPersonService = tokenPersonService;
        this._fileService = fileService;
    }

 

#pragma warning disable
    public async Task<(bool Result, int CashedMinutes)> RegisterAsync(PersonRegisterDto registerDto)
    {
        var student = await _personRepository.GetByEmailAsync(registerDto.Email);
        if (student is not null) throw new UserAlreadyExistsException(registerDto.Email);

        // delete if exists user by this phone number
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + registerDto.Email, out PersonRegisterDto cachedRegisterDto))
        {
            cachedRegisterDto.FirstName = cachedRegisterDto.FirstName;
            _memoryCache.Remove(REGISTER_CACHE_KEY + registerDto.Email);
        }
        else _memoryCache.Set(REGISTER_CACHE_KEY + registerDto.Email, registerDto,
            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

        return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER);
    }

    public async Task<(bool Result, int CashedVerificationMinutes)> SendCodeForRegisterAsync(string mail)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + mail, out PersonRegisterDto registerDto))
        {
            PersonVerificationDto verificationDto = new PersonVerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();

            verificationDto.Code = CodeGenerator.GenerateRandomNumber();

            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + mail, out PersonVerificationDto oldDto))
            {
                _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + mail);
            }
            _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + mail, verificationDto,
                TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

            EmailMessage emailSms = new EmailMessage();
            emailSms.Title = "Game Master Arena";
            emailSms.Content = "Verification code : " + verificationDto.Code;
            emailSms.Recipent = mail;

            var mailResult = await _mailSender.SendAsync(emailSms);
            if (mailResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
            else return (Result: false, CachedVerificationMinutes: 0);
        }
        else throw new UserCacheDataExpiredException(); 
    }


    public async Task<(bool Result, string Token)> VerifyRegisterAsync(string mail, int code)
    {
        if(_memoryCache.TryGetValue(REGISTER_CACHE_KEY + mail, out PersonRegisterDto registroDto))
        {
            if(_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + mail, out PersonVerificationDto verificationDto))
            {
                if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    throw new VerificationTooManyRequestsException();
                else if (verificationDto.Code == code)
                {
                    var dbResult = await RegisterToDatabaseAsync(registroDto);
                    if(dbResult is true)
                    {
                        var student = await _personRepository.GetByEmailAsync(mail);
                        string token = _tokenPersonService.GenerateToken(student);
                        return (Result: true, Token: token);
                    }
                    else return (Result: false, Token: "");
                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + mail);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + mail, verificationDto,
                        TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));
                    return (Result: false, Token: "");
                }
            }
            else throw new VerificationCodeExpiredException();
        }
        else throw new UserCacheDataExpiredException();
    }

    private async Task<bool> RegisterToDatabaseAsync(PersonRegisterDto registroDto)
    {
        
        var person = new Person();
        person.FirstName = registroDto.FirstName;
        person.LastName = registroDto.LastName;
        person.Email = registroDto.Email;

        person.Image = "media\\images\\IMG_d51ee0a1-71ac-4a97-b98f-0880f3fed1e4.jpg";
        var hasherResult = PasswordHasher.Hash(registroDto.Password);
        person.Password = hasherResult.Hash;
        person.Salt = hasherResult.Salt;

        person.Create_at = person.Update_at = TimeHelper.GetDateTime();

        var dbResult = await _personRepository.CreateAsync(person);
        return dbResult > 0;
    }

    public async Task<(bool Result, string Token)> LoginAsync(PersonLoginDto loginDto)
    {
        var student = await _personRepository.GetByEmailAsync(loginDto.Email);
        if(student is null) throw new UserNotFoundException();

        var hasherResult = PasswordHasher.Verify(loginDto.Password, student.Password, student.Salt);
        if (hasherResult == false) throw new PasswordNotMatchException();

        string token = _tokenPersonService.GenerateToken(student);

        return (Result: true, Token: token);
    }

    

}
