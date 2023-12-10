using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Common.Configs;
using Common.IoC;
using DAL.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterBuildCallback(ctx => IoC.Container = ctx.Resolve<ILifetimeScope>());
    BLL.Startup.BootStrapper.Bootstrap(containerBuilder);
});

builder.Host.ConfigureAppConfiguration(config =>
{
    config.AddJsonFile(@$"{Path.GetFullPath(@"../")}/config.json");
});

var connectionModel = builder.Configuration.GetSection("Connection").Get<ConnectionModel>();

var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? connectionModel.Host;
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? connectionModel.Database;
var dbPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? connectionModel.Password;

var connection = string.Format(connectionModel.ConnectionString, dbHost, dbName, dbPassword);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<DbContextBase>(options => options.UseNpgsql(connection));

var authOptions = builder.Configuration.GetSection("Auth").Get<AuthOptions>();
builder.Services.AddSingleton(authOptions);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = authOptions.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = authOptions.SymmetricSecurityKey,
            ClockSkew = TimeSpan.FromMinutes(0)
        };
    });

var emailCreds = builder.Configuration.GetSection("EmailCreds").Get<EmailCreds>();
builder.Services.AddSingleton(emailCreds);

#region Init Mapper Profiles

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddMaps(new[] {
        "DAL",
        "BLL"
    });
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
{
    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
    In = ParameterLocation.Header,
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey
});

options.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddCors(option =>
{
    option.AddPolicy(name: MyAllowSpecificOrigins, builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
