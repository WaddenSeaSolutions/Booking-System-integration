namespace Padel_Court_Time_Slot_Microservice.Infrastructure.Repositories;

public class DBUtils
{
    public static readonly string ProperlyFormattedConnectionString;

    static DBUtils()
    {
        ProperlyFormattedConnectionString = "mongodb://paddleuser:password@paddlecourtdb:27017/paddlecourttimeslotdb";

    }
}