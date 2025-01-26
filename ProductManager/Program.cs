using Microsoft.EntityFrameworkCore;
using ProductManager.Data;
using ProductManager.Mapping;
using ProductManager.Repositories;
using ProductManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EshopContext") ?? throw new InvalidOperationException("Connection string 'EshopContext' not found.")));

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IApplicationDbContext, ApplicationDbContext>();

builder.Services.AddAutoMapper(typeof(ProductProfile));

builder.Services.AddControllers().AddXmlSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.MapControllers();
// app.UseHttpsRedirection();

app.Run();