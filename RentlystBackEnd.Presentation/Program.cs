using RentlystBackEnd.Presentation;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    // builder.Configuration.UseKeyVault();
}
else
{
    builder.Configuration.AddJsonFile(
        "appsettings.Local.json",
        optional: true,
        reloadOnChange: false);
}

builder.Services.AddDb(builder.Configuration);
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddAuth();
// builder.Services.AddSingleton<ISessionContext, HttpSessionContext>();
builder.Services.AddGraph();
builder.Services.AddHealthChecks();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("https://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Include if cookies or credentials are needed
    });
});
builder.Services.ConfigureOptions(builder.Configuration);
// builder.Services.ConfigureOptions(builder.Configuration);
// builder.Services.AddSingleton<AppAvailableService>();
// builder.Services.AddSingleton<AppAvailableMiddleware>();
// builder.Services.AddSingleton<IAppAvailableService>(ctx => ctx.GetRequiredService<AppAvailableService>());
// builder.WebHost.AddSentry();

var app = builder.Build();

_ = app.Init();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseMiddleware<AppAvailableMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowLocalhost");
app.UseAuthentication();
app.UseAuthorization();

// app.MapGroup("/api/account")
//     .MapAccountEndpoints();

app.MapGraphQL();
app.MapHealthChecks("/healthz");
app.MapFallbackToFile("index.html");

app.Run();

public partial class Program {}