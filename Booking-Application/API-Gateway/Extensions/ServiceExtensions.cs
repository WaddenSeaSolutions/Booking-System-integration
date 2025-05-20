using Microsoft.AspNetCore.Authorization; // Add this using directive
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API_Gateway.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApiGatewayServices(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationProviderKey = configuration["AuthenticationProviderKey"];

            if (authenticationProviderKey == null)
            {
                throw new ArgumentNullException($"AuthenticationProviderKey is required");
            }
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationProviderKey))
                };
            });

            // Define the "Anonymous" policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Anonymous", policy =>
                {
                    policy.RequireAssertion(context => true); // Always allows access
                });
            });

            services.AddOcelot(configuration);
            return services;
        }

        public static WebApplication UseApiGateway(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization(); // This order is important!
            app.UseOcelot().Wait();
            return app;
        }
    }
}