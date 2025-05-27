namespace Booking_Microservice.Infrastructure.Repositories
{
    public static class DBUtils
    {
        public static readonly string ProperlyFormattedConnectionString;

        static DBUtils()
        {
            ProperlyFormattedConnectionString = "Host=bookingdb;Username=myuser;Password=mypassword;Port=5432;Database=bookingdb;Pooling=true;";

        }
    }
}
