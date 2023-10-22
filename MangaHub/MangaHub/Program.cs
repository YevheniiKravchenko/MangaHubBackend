using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Common.Configs;
using Common.IoC;
using DAL.DbContexts;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddDbContext<DbContextBase>(options => options.UseNpgsql(connection));

#region Init Mapper Profiles

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddMaps(new[] {
        "DAL",
    });
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

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
