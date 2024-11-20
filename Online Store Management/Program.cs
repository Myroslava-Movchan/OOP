using Online_Store_Management.Infrastructure;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Services;
using Catalogue;
using Online_Store_Management.DataAccess; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<IOrderInfo, OrderInfoService>();
builder.Services.AddScoped<IRepository<CustomerDbModel>, CustomerRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INewCustomer, NewCustomerService>();
builder.Services.AddScoped<IRegularCustomer, RegularCustomerService>();

builder.Services.AddDbContext<CustomerDBContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient();

builder.Services.AddScoped<CatalogueClient>();
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
