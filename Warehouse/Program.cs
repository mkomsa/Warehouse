using Warehouse.Core;
using Warehouse.Infrastructure;
using Warehouse.Infrastructure.Seeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterCoreServices();
builder.Services.RegisterInfrastructureServices(builder.Configuration);

var app = builder.Build();

IServiceScope scope = app.Services.CreateScope();
AppDbContextSeeder seeder = scope.ServiceProvider.GetRequiredService<AppDbContextSeeder>();

seeder.SeedDatabase();

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
