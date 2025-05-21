namespace Booking_Microservice.Infrastructure.Repositories
{
    public static class DBUtils
    {
        public static readonly string ProperlyFormattedConnectionString;

        static DBUtils()
        {
            ProperlyFormattedConnectionString = "Server=bookingdb;User=myuser;Password=mypassword;Port=3306;Database=bookingdb;Pooling=true;";

        }
    }
}
