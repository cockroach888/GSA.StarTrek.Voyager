using MQTT2MVC4WebApp1.Internal;
using MQTT2MVC4WebApp1.Hubs;
using MQTT2MVC4WebApp1.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseUrls("http://*:5000");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<MessageDespatchService>()
                .AddHostedService<MQTTClientHostedServer>();

builder.Services.AddSignalR()
                .AddMessagePackProtocol();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MQTT2MVC4WebApp API",
        Description = "MQTT2MVC4WebApp ASP.NET Core Web API",
        TermsOfService = new Uri("https://github.com/cockroach888"),
        Contact = new OpenApiContact
        {
            Name = "ó¯òë¡¤»ê",
            Email = "cockroach888@outlook.com",
            Url = new Uri("https://cockroach888.github.io"),
        },
        License = new OpenApiLicense
        {
            Name = "MQTT2MVC4WebApp License",
            Url = new Uri("https://cockroach888.github.io/license"),
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "MQTT2MVC4WebApp API V1");
    });
    app.UseDeveloperExceptionPage();
    //app.UseWelcomePage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SightX}/{action=Index}/{id?}");

app.MapHub<MQTTServiceHub>("/mqttHub");

//app.MapGet("/", () => Results.LocalRedirect("/swagger"));

app.Run();
