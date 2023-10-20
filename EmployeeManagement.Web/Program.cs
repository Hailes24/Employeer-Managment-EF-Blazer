using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using AutoMapper;
using EmployeeManagement.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Web.Data;
using EmployeeManagement.Web;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EmployeeManagementWebContextConnection") ?? throw new InvalidOperationException("Connection string 'EmployeeManagementWebContextConnection' not found.");

builder.Services.AddDbContext<EmployeeManagementWebContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<EmployeeManagementWebContext>();

// Add services to the container.
builder.Services.AddAuthentication("Identity.Application").AddCookie();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDevExpressBlazor();
builder.Services.AddAutoMapper(typeof(EmployeeProfile));
builder.Services.AddHttpClient<IEmployeeService, EmployeeService>((cli) => cli.BaseAddress = new Uri("https://localhost:7299/"));
builder.Services.AddHttpClient<IDepartmentService, DepartmentService>((cli) => cli.BaseAddress = new Uri("https://localhost:7299/"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});

//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");
//app.UseAuthentication();;

app.Run();
