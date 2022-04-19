using MQTT2MVC4WebApp1.Internal;
using MQTT2MVC4WebApp1.Hubs;
using MQTT2MVC4WebApp1.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseUrls("http://*:5000");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<MessageDespatchService>()
                .AddHostedService<MQTTClientHostedServer>();

builder.Services.AddSignalR()
                .AddMessagePackProtocol();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!builder.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SightX}/{action=Index}/{id?}");

app.MapHub<MQTTServiceHub>("/mqttHub");

//app.UseWelcomePage();

app.Run();
