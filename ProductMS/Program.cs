using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using OrderMS.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// WARNING: Remember to set in .csproj file the next configuration to go
// smoothly with the next builder configuration:
// <InvariantGlobalization>false</InvariantGlobalization>
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = new [] { new CultureInfo("en-US") };
    options.SupportedUICultures = new [] { new CultureInfo("en-US") };
});
// WARNING: If you're going to use docker, this line is getting the
// DB configuration.
builder.Services.AddDbContext<ProductContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnectionProduct")
));
// WARNING: Because we use MVC in our web api project.
builder.Services.AddControllers();

var app = builder.Build();

// WARNING: In case to use a custom localization configuration (builder.Services.Configure<RequestLocalizationOptions>).
app.UseRequestLocalization();
// WARNING: To update automatically the Models' schema with Entity Framework.
// With this code we ensure a db exists before running the web api.
using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductContext>();
    // It's the same like running in the terminal "dotnet ef database update".
    dbContext.Database.Migrate();
} 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
// WARNING: Additional configuration callbacks for MVC.
/* if your api uses credentials to make requests */
// app.UseAutorization();
app.MapControllers();

app.Run();
