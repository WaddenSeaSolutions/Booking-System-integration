var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
