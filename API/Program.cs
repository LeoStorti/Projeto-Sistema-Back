using API.Context;
using API.Models;
//using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do banco de dados
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<APIDbContext>(options =>
    options.UseSqlServer(connection));

// Configura��o dos servi�os MVC
builder.Services.AddControllers();

// Configura��o do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configura��o do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Middleware de desenvolvimento para Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.InjectStylesheet("/swagger-ui/custom.css");
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); // Verifique o caminho correto para seu Swagger JSON
    });
}

// Middleware para redirecionamento HTTPS
app.UseHttpsRedirection();

// Middleware para autoriza��o
app.UseAuthorization();

// Middleware para aplicar pol�tica CORS
app.UseCors("AllowFrontend");

// Middleware para mapear controllers
app.MapControllers();

app.Run();
