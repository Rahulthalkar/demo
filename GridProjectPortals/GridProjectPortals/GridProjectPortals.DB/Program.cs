using GridProjectPortals.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connString = builder.Configuration.GetConnectionString("GRIDDb");
builder.Services.AddDbContext<GridDbContext>(options =>
{
    options.UseSqlServer(connString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();
