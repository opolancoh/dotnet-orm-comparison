using DotNetOrmComparison.Api.Extensions;
using DotNetOrmComparison.Data.EntityFramework;
using DotNetOrmComparison.Data.EntityFramework.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigurePersistence();
builder.Services.ConfigureControllers();

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

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed data
/* using (var scope = ((IApplicationBuilder)app).ApplicationServices.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DataSeeder.Seed(context);
} */

app.Run();