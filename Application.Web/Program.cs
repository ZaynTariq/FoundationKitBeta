using FoundationKit.Authentication.Persistence.Extensions;
using FoundationKit.Web.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDBConext>(options =>
{
    options.UseSqlServer("Server=192.168.1.56;Database=AuthTestDB;User Id=sa;Password=123456;MultipleActiveResultSets=true;TrustServerCertificate=True; Max Pool Size=100");
});

builder.Services.AddFoundationKit<ApplicationDBConext>(options =>
{
    options.UseAsNotTracking = true;
});

//builder.Services.AddFoundationKit(sp =>
//{
//    var factory = sp.GetRequiredService<IDbContextFactory<ApplicationDBConext>>();
//    return factory.CreateDbContext();
//});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
