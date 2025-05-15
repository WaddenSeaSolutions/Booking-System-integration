namespace User_Microservice.Infrastructure.Repositories;

public static class DBUtils
{
    public static readonly string ProperlyFormattedConnectionString;

    static DBUtils()
    {
        ProperlyFormattedConnectionString = "Server=userdb;User=myuser;Password=mypassword;Port=3306;Database=userdb;Pooling=true;";

    }
}