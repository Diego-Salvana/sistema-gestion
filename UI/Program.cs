using UI;
using UI.ClientServices;
using UI.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTransient<ProductosService>();
builder.Services.AddSingleton<UsuariosService>();
builder.Services.AddTransient<ProductosVendidosService>();
builder.Services.AddTransient<VentasService>();
builder.Services.AddTransient<NombreAPIService>();

builder.Services.AddSingleton<InicioRegistro>();

builder.Services.AddHttpClient<ProductosService>(
    client => client.BaseAddress = new Uri("http://localhost:5292/api/Productos/")
);
builder.Services.AddHttpClient<UsuariosService>(
    client => client.BaseAddress = new Uri("http://localhost:5292/api/Usuarios/")
);
builder.Services.AddHttpClient<ProductosVendidosService>(
    client => client.BaseAddress = new Uri("http://localhost:5292/api/ProductosVendidos/")
);
builder.Services.AddHttpClient<VentasService>(
    client => client.BaseAddress = new Uri("http://localhost:5292/api/Ventas/")
);
builder.Services.AddHttpClient<NombreAPIService>(
    client => client.BaseAddress = new Uri("http://localhost:5292/api/NombreAPI/")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
