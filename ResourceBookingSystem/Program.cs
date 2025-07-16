using Microsoft.EntityFrameworkCore;//use of EntityFramework

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. and tell the program to use SQLServer
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
    pattern: "{controller=Home}/{action=Index}/{id?}");
//Adds to the database opun creatation Seeding the data if the DB is empty
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!db.Resources.Any())
    {
        db.Resources.AddRange(
           new Resource
           {
               Name = "Meeting Room A",
               Capacity = 1,
               Location = "3rd Floor, West Wing",
               IsAvailable = true,
               Description = "Large room with projector and whiteboard"
           },
    new Resource
    {
        Name = "Company Car 1",
        Capacity = 4,
        Location = "Parking Bay 5",
        IsAvailable = true,
        Description = "Toyota Corolla"
    },

        new Resource
        {
            Name = "Company Car 2",
            Capacity = 4,
            Location = "Parking Bay 3",
            IsAvailable = true,
            Description = "Honda Civic"
        });
        
        db.SaveChanges();
    }
}

app.Run();
