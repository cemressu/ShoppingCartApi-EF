using Microsoft.EntityFrameworkCore;
using PostrgreSqlApi.Model;
using ShoppingCartApi;
using ShoppingCartApi.Repositories.Abstract;
using ShoppingCartApi.Repositories.Concrete;
using ShoppingCartApi.Service.BasketService;
using ShoppingCartApi.Service.CustomerService;
using ShoppingCartApi.Service.OrderService;
using ShoppingCartApi.Service.ProductService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
//en son yazdığım servis en önemlisi dependency injectionumun calismasini engelleyen tamamen bu cumleymis 
//burada DbContext icine veritabanı baglantımı koyuyorum ve bunları servis haline getiriyorum
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBasketService,BasketService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //ortamimiz development ortamiysa yani kendi bilgisayarimda calistiriyorsam 
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

