using Microsoft.EntityFrameworkCore;
using PizzaDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.UseAuthorization();

app.MapControllers();

app.Run();
