using encuestas_backend;

var builder = WebApplication.CreateBuilder(args);
var starup = new StartUp(builder.Configuration);
starup.ConfigurationServices(builder.Services);
var app = builder.Build();
starup.Configure(app, app.Environment);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.Run();