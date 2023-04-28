using GBM_Stocks.Filters;
using GBM_Stocks_Accounts_Core.Extensions;
using GBM_Stocks_Accounts_Infrastructure.Extensions;
using GBM_Stocks_Infrastructure.Extensions;
using GBM_Stocks_Orders_Core.Extensions;
using GBM_Stocks_Orders_Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add filters
builder.Services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));
//add GBM Services
builder.Services.AddGBMServices();
//adding account services
builder.Services.StocksAccountsRepositoryServiceCollection();
builder.Services.StocksAccountsCoreServiceCollection();
//adding Orders services
builder.Services.StocksOrdersRepositoryServiceCollection();
builder.Services.StocksOrdersCoreServiceCollection();

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
