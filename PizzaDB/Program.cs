using Microsoft.EntityFrameworkCore;
using PizzaDB;
using PizzaDB.utils;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")  // Aggiungi il dominio della tua app Angular
                  .AllowAnyMethod()   // Consente tutti i metodi (GET, POST, PUT, DELETE, ecc.)
                  .AllowAnyHeader()   // Consente qualsiasi header
                  .AllowCredentials(); // Consente credenziali (opzionale)
        });
});

try
{
    builder.Services.AddDbContext<OrdiniContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );



}
catch (Exception ex)
{
    Console.WriteLine("Errore nella connessione al database: " + ex.Message);
    throw;
}


// Aggiungi i servizi MVC (se non l'hai già fatto)
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();
