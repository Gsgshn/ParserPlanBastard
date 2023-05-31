using Microsoft.EntityFrameworkCore;
using ParserPlanBastard.Data;
using ParserPlanBastard.Models.Entities;
using ParserPlanBastard.Service;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IXmlRepository, XmlFileRepresentation>();
builder.Services.AddScoped<IFileService,FileService>();

var configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("BloggingDbContext")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "StatusAdmin",
        policyBuilder => policyBuilder
        .RequireClaim("AdminPassNumber","A1234"));
});

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

app.MapRazorPages();

app.Run();
