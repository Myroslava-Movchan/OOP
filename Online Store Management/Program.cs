using Online_Store_Management.Infrastructure;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Services;
using Xkcd;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<IOrderInfo, OrderInfoService>();

builder.Services.AddHttpClient();

builder.Services.AddScoped<XkcdClient>();
builder.Services.AddScoped<Logger>();

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
