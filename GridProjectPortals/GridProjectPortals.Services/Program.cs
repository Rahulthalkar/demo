using GridProjectPortals.BLL;
using GridProjectPortals.DAL;
using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string corsDomains = "http://localhost:9121,http://localhost:4200";
string[] domains = corsDomains.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

builder.Services.AddCors(o => o.AddPolicy("AppCORSPolicy", builder =>
{
    builder
    .WithOrigins(domains)
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
}));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
// Add services to the container.

builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
builder.Services.AddScoped<IGridSearchRepository, GridSearchRepository>();
builder.Services.AddTransient<ISendIMailRepository,SendMailRepository>();
builder.Services.AddScoped<IMailServices, MailServices>();
builder.Services.AddScoped<IUserRespository,UserRepository>();
builder.Services.AddScoped<Cryptography>();
builder.Services.AddScoped<ITokenRespository, TokenRespository>();
builder.Services.AddScoped<tokenService>();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ASP.NET 7 Web API",
        Description = "Authentication and Authorization in ASP.NET 7 with JWT and Swagger"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = $"Enter ‘Bearer’ [space] and then your valid token in the text input below.{Environment.NewLine}Example: " +
                        $"{Environment.NewLine}Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9"
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AppCORSPolicy");
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
