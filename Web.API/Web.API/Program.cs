using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Utilisez votre propre chaîne de connexion SQLite ci-dessous
builder.Services.AddDbContext<DbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error"); // Gestion d'erreur globale, redirige vers une action "error" personnalisée.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Ajoutez cette classe pour gérer les erreurs de manière personnalisée (par exemple, dans un fichier ErrorController.cs)
app.Map("/error", errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500; // Ou un autre code d'erreur de votre choix
        context.Response.ContentType = "text/html";

        await context.Response.WriteAsync("Une erreur s'est produite. Veuillez réessayer plus tard ou contactez l'administrateur.");
    });
});