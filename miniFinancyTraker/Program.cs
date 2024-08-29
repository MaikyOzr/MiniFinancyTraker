using Microsoft.EntityFrameworkCore;
using miniFinancyTraker.Data;
using miniFinancyTraker.Repository;
using miniFinancyTraker.Service;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var services = builder.Services;
services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(
    config.GetConnectionString("DefaultConnection")
    ));
services.AddScoped<ITransactionRepository, TransactionRepository>();
services.AddScoped<ValidationService>();
services.AddMvc();
services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
