using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcWebIdentity.Context;
using MvcWebIdentity.Services;

var builder = WebApplication.CreateBuilder(args);

//*CONEXAO COM BANCO MYSQL
var connectionStringMySql = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(x => x.UseMySql(connectionStringMySql, ServerVersion.Parse("8.0.29")));

//*IDENTITY.
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

//*DEFINE AS CONFIGURACOES DA COMPLEXIDADE DA SENHA.
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 10;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireNonAlphanumeric = false;
});

//*DEFINE AS OPCOES DOS COOKIES - CONTROLA OS COOKIES - TEMPO ETC.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "AspNetCore.Cookies";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        options.SlidingExpiration = true;
    });


//*REGISTRO DO SERVICO DAS ROLES.
builder.Services.AddScoped<ICriaRegrasUsuario, CriaRegasUsuario>();


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

//*APOS O METODO UserRouting() VAMOS INVOCAR O METODO CriarPerfisUsuarioAsync() COM A INSTACIA DE app.
await CriarPerfisUsuarioAsync(app);


//*AUTENTICACAO P USO DO IDENTITY.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//*METODOS PARA CRIACAO DOS PERFIS E SUAS ROLES.
async Task CriarPerfisUsuarioAsync(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ICriaRegrasUsuario>();
        await service.CriaRegrasAsync();
        await service.CriaUsuarioComRegraAsync();
    }
}
