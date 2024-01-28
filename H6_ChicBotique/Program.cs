using H6_ChicBotique.Database;
using H6_ChicBotique.Repositories;
using H6_ChicBotique.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();


builder.Services.AddTransient<IAccountInfoService, AccountInfoService>();
builder.Services.AddTransient<IAccountInfoRepository, AccountInfoRepository>();

builder.Services.AddTransient<IHomeAddressRepository, HomeAddressRepository>();
builder.Services.AddTransient<IHomeAddressService, HomeAddressService>();



builder.Services.AddTransient<IPasswordEntityRepository, PasswordEntityRepository>();

builder.Services.AddDbContext<ChicBotiqueDatabaseContext>(
                        o => o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));




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
app.UseCors(policy => policy
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
