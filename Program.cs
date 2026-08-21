using Delegame.Components;
using Delegame.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddSingleton<QuestionBank>();
builder.Services.AddSingleton<AvatarCatalog>();
builder.Services.AddSingleton<DelegameStore>();
builder.Services.AddSingleton<WorkshopService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<RoomService>();
builder.Services.AddScoped<ToastService>();
builder.Services.AddScoped<GameFlow>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
