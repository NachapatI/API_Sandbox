var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    services.AddControllers();
    services.AddControllersWithViews();
}

var app = builder.Build();

if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

{
    // app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();

    app.MapControllerRoute(
        name: "defult",
        pattern:"{controller=Home}/{action=Index}/{id?}"
    );
}

app.Run();