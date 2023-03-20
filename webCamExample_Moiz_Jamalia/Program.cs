using Microsoft.EntityFrameworkCore;
using webCamExample_Moiz_Jamalia.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<AppDbContext>();

string _GetConnStringName = builder.Configuration.GetConnectionString("DefaultConnectionMySQL");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(_GetConnStringName));


// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
     pattern: "{controller=Camera}/{action=Capture}/{id?}");

app.Run();