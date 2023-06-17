using Microsoft.EntityFrameworkCore;
using MoscowWeather.DataAccess.Context;


var builder = WebApplication.CreateBuilder(args);

// ���������� �������� � ���������.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IDatabaseContext, DatabaseContext>(options =>
{
    string? localConnection = builder.Configuration.GetConnectionString("LocalConnection");
    options.UseSqlServer(localConnection);
});

var app = builder.Build();


// ��������� ��������� HTTP-��������.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Weather}/{action=Index}");

app.Run();
