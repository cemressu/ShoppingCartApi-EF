using ShoppingCartApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DbContext>(new DbContext("Server=localhost; Database=urun; Port=5432; User Id=postgres; Password=cemre1381;"));
//en son yazdığım servis en önemlisi dependency injectionumun calismasini engelleyen tamamen bu cumleymis 
//burada DbContext icine veritabanı baglantımı koyuyorum ve bunları servis haline getiriyorum

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

