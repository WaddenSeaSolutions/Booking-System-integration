using API_Gateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add Ocelot configuration
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;
    config.SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
        .AddJsonFile("Configuration/ocelot.sample-microservice.json", true, true) // will match all 'ocelot.json' files and merge them into a single configuration
                                                                                  //.AddOcelot("Configuration",env as IWebHostEnvironment) // will match all '^ocelot\.(.*?)\.json$' files and merge them into a single configuration
        .AddEnvironmentVariables();
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiGatewayServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseEndpoints(e => e.MapControllers());

app.UseApiGateway();

app.Run();
