using MQTT2MVC4WebApp1.Internal;
using MQTT2MVC4WebApp1.Hubs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.WebHost.UseUrls("http://*:5000");
builder.Services.AddHostedService<MQTTClientHostedServer>();

builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
