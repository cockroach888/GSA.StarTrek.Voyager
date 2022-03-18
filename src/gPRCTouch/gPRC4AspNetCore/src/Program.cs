using gPRC4AspNetCore.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
//else
//{
//    app.UseExceptionHandler("~/Error");
//}


app.MapGrpcService<UploaderService>();

app.MapGet("/", () => "Hello World!");

app.Run();
