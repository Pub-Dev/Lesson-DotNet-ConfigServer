using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.Extensions.Configuration.Placeholder;
using Steeltoe.Management.Endpoint;
using Steeltoe.Management.Endpoint.Refresh;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost
    .AddConfigServer()
    .AddPlaceholderResolver()
    .AddRefreshActuator();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRefreshActuatorServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Map("/full", (IConfiguration configuration) =>
{
    return ((IConfigurationRoot)configuration).GetDebugView();
});

app.Map("/", (IConfiguration configuration) =>
{
    return new
    {
        Logging = new
        {
            LogLevel = new
            {
                Default = configuration["Logging:LogLevel:Default"]
            }
        },
        ConnectionString = new
        {
            SqlServer = configuration["ConnectionString:SqlServer"],
            Cassandra = configuration["ConnectionString:Cassandra"],
            Redis = configuration["ConnectionString:Redis"],
        },
        Urls = new
        {
            Product = configuration["Urls:Product"],
            User = configuration["Urls:User"],
        }
    };
});

app.Map<RefreshEndpoint>();

app.Run();
