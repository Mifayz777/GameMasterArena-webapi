using GameMasterArena.Service.Dtos.Notifications;
namespace GameMasterArena.Service.Interfaces.Notifications;

public interface IMailSender
{
    public Task<bool> SendAsync(EmailMessage message);
}
