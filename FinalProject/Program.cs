
                                                                                                                                                        
using FinalProject;
var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.Register(config);



var app = builder.Build();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute("default",
    "{controller=home}/{action=index}/{id?}");


app.Run();