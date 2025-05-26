namespace Paddle_Court_Microservice.Infrastructure.Repositories
{
    public class DBUtils
    {
        public static readonly string ProperlyFormattedConnectionString;

        static DBUtils()
        {
            ProperlyFormattedConnectionString = "mongodb://myuser:mypassword@paddlecourtdb:27017/paddlecourtdb?authSource=admin";

        }
    }
}
