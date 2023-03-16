using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

using var db = app.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
await db.Database.MigrateAsync();
if (!db.Lessons.Any())
{
    db.Lessons.Add(new Lesson { Name = "Lesson 1" });
    db.Lessons.Add(new Lesson { Name = "Lesson 2" });
    db.Lessons.Add(new Lesson { Name = "Lesson 3" });
    db.Lessons.Add(new Lesson { Name = "Lesson 4" });
    db.Lessons.Add(new Lesson { Name = "Lesson 5" });
    db.Lessons.Add(new Lesson { Name = "Lesson 6" });
    db.Lessons.Add(new Lesson { Name = "Lesson 7" });
    db.Lessons.Add(new Lesson { Name = "Lesson 8" });
    db.Lessons.Add(new Lesson { Name = "Lesson 9" });
    db.Lessons.Add(new Lesson { Name = "Lesson 10" });
    await db.SaveChangesAsync();
}

app.Run();
