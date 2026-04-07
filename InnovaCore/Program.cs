using InnovaCore.Data.Context;
using InnovaCore.Services.Interfaces;
using InnovaCore.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args); builder.Services.AddDefaultIdentity<IdentityUser>(options => {

    options.Password.RequireDigit = false;

    options.Password.RequiredLength = 6;

    options.Password.RequireNonAlphanumeric = false;

    options.Password.RequireUppercase = false;

    options.Password.RequireLowercase = false;
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ " + // Note o espaço aqui
        "áéíóúàèìòùâêîôûãõçÁÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕÇ";

})

.AddRoles<IdentityRole>()

.AddEntityFrameworkStores<InnovationCoreDbContext>(); builder.Services.ConfigureApplicationCookie(options =>

{

    options.LoginPath = "/Account/Login";

    options.AccessDeniedPath = "/Account/AccessDenied";



}); builder.Services.AddControllersWithViews(); builder.Services.AddRazorPages(); builder.Services.AddControllersWithViews(); builder.Services.AddScoped<ISolicitacaoService, SolicitacaoService>(); builder.Services.AddScoped<ITarefaService, TarefaService>(); builder.Services.AddScoped<IDashboardService, DashboardService>(); builder.Services.AddTransient<IEmailServices, EmailServices>(); builder.Services.AddScoped<ITemaService, TemaService>(); builder.Services.AddScoped<ISetorService, SetorService>(); var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); builder.Services.AddDbContext<InnovationCoreDbContext>(options =>

options.UseSqlServer(connectionString)); var app = builder.Build();
using (var scope = app.Services.CreateScope())
{var services = scope.ServiceProvider;
// Note que usamos "await", então o método Main precisaria ser "async Task Main"
// ou usamos .GetAwaiter().GetResult() se for o estilo antigo do Program.cs


await DbInitializer.SeedRolesAndAdminAsync(services);
}
// Configure the HTTP request pipeline.if (!app.Environment.IsDevelopment())

{

    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

    app.UseHsts();

}
app.UseHttpsRedirection(); 
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(

name: "default",

pattern: "{controller=Home}/{action=Index}/{id?}"); 
app.MapRazorPages(); 
app.Run();