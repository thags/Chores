using Chores.Controllers;
using Chores.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var sqlController = new sqliteController();
sqlController.CreateTables();
//sqlController.AddChore(new Chores.Models.Chore
//{
//    Name = "test",
//    Note = "None",
//    CompletionDate = DateTime.Today,
//    NextDueDate = DateTime.Today.AddDays(7),
//    Recurrence = TimeSpan.FromDays(7)
//}); ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IDBInterface, sqliteController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();

