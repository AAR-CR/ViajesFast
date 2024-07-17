using API_Vuelos.Data;
using API_Vuelos.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<VueloService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ReservaService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configurar Entity Framework
builder.Services.AddDbContext<ViajesFastDbConext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api_Vuelos v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
