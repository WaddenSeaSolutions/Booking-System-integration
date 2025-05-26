namespace Padel_Court_Time_Slot_Microservice.Infrastructure.Repositories;

public class DBUtils
{
    public static readonly string ProperlyFormattedConnectionString;

    static DBUtils()
    {
        ProperlyFormattedConnectionString = "mongodb://myuser:mypassword@paddletimedb:27017/paddletimedb?authSource=admin";

    }
}