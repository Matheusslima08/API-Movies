using FilmesApi.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FilmesApi.Profiles;


var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("FilmeConnection");

builder.Services.AddDbContext<FilmeContext>(opts =>
 opts.UseLazyLoadingProxies().UseMySQL(connectionString!));

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new CinemaProfile());
    cfg.AddProfile(new EnderecoProfile());
    cfg.AddProfile(new FilmeProfile());
    cfg.AddProfile(new SessaoProfile());

});
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
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
