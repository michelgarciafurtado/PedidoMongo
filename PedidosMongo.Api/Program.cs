using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using PedidosMongo.Application.Interfaces;
using PedidosMongo.Application.UseCase.Categorias;
using PedidosMongo.Application.UseCase.Pedidos;
using PedidosMongo.Application.UseCase.Produtos;
using PedidosMongo.Domain.Interfaces;
using PedidosMongo.Infra.Consultas;
using PedidosMongo.Infra.DbContext;
using PedidosMongo.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

var MongoSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();

builder.Services.AddSingleton<IMongoClient>(mc => new MongoClient(MongoSettings.ConnectionString));

builder.Services.AddScoped(mc =>
{
    var client = mc.GetRequiredService<IMongoClient>();
    return client.GetDatabase(MongoSettings.DatabaseName);
});

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaUseCase, CategoriaUsecase>();
builder.Services.AddScoped<IProdutosUseCase, ProdutoUseCase>();
builder.Services.AddScoped<IProdutosRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutosConsultas, ProdutosConsulta>();
builder.Services.AddScoped<ObterProdutosUseCase>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoUseCase, PedidosUseCase>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
