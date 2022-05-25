using EstimativaColheita.Persistence;
using EstimativaColheita.Repositories.Interfaces;
using EstimativaColheita.Repositories.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar os serviçoes dos repositoirios
builder.Services.AddTransient<IColheitaRealizada, ColheitaRealizadaService>();
builder.Services.AddTransient<IContrato, ContratoService>();
builder.Services.AddTransient<IEncarregado, EncarregadoService>();
builder.Services.AddTransient<IEstimativaColheita, EstimativaColheitaService>();
builder.Services.AddTransient<IEstimativaMotivo, EstimativaMotivoService>();
builder.Services.AddTransient<IFiscalCampo, FiscalCampoService>();
builder.Services.AddTransient<ITalhao, TalhaoService>();
builder.Services.AddTransient<ITipoLancamento, TipoLancamentoService>();
builder.Services.AddTransient<IVariedade, VariedadeService>();

// definindo o tempo de vida do HttpContext que será durante todo o tempo de vida da aplicação
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
builder.Services.AddRazorPages().AddSessionStateTempDataProvider();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString"));
});

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
app.UseCookiePolicy();
app.UseSession();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
