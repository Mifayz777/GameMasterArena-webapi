using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.DataAccess.Repositories;

public class BaseRepasitory
{
    protected readonly NpgsqlConnection _connection;

    public BaseRepasitory()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connection = new NpgsqlConnection("Host=localhost; Port=5432; Database=GameMasterArena; User Id=postgres; Password=Mirka_cr7;");
    }

}
