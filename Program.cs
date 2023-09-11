using Microsoft.EntityFrameworkCore;  //#6 add in the framework
using Delicious.Models;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();  // #2 They fit nicely right after AddControllerWithViews() // Add these two lines before calling the builder.Build() method
builder.Services.AddSession();
builder.Services.AddDbContext<DishContext>(options =>  //#9 Accessing mycontext.cs file
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
