using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = Environment.GetEnvironmentVariable("ClientId") ??
        "489423100830-l79kp1n9ndh5tl3fghrk9q20h87elrh3.apps.googleusercontent.com";
    googleOptions.ClientSecret = Environment.GetEnvironmentVariable("ClientSecret") ??
        "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";
});

// Add services to the container.
var connectionString = Environment.GetEnvironmentVariable("con") ??
    builder.Configuration.GetConnectionString("SomeeConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IUserRep, UserRep>();
builder.Services.AddScoped<ITaskItemRep, TaskRep>();
builder.Services.AddScoped<ISolutionRep, SolutionRep>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var mvcBuilder = builder.Services.AddControllersWithViews();

if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
