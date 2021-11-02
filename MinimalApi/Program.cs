using MinimalApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

var app = builder.Build();

/* ========================================================================= */

app.MapGet("/", () => ApiService.TestConnection());
app.MapGet("/api/v1/items", () => ApiService.GetItems());
app.MapGet("/api/v1/items/{id}", (int id) => ApiService.GetItem(id));

app.MapPost("/api/v1/authenticate", (object user) => ApiService.Authenticate(user));

/* ========================================================================= */

app.Run();
