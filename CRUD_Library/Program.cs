using CRUD_Library.Meddleware;
using CRUD_Library.Models;
using CRUD_Library.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserManager, UserManager>();
var app = builder.Build();

BookDbInitializer.seed(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<KeyMiddleware>();


app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "{area}/{controller=Home}/{action=Index}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{CategoryId?}/{ReaderId?}");

app.Run();
