using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using PedidosMongo.Domain.Entidades;

namespace PedidosMongo.Infra.Configurations;

public static class ProdutoConfiguration
{
    public static void Register()
    {
        BsonClassMap.RegisterClassMap<Produto>(
            map =>
            {
                map.AutoMap();

                map.MapIdProperty(p => p.Id)
                   .SetSerializer(new GuidSerializer(BsonType.String));
                map.MapMember(p => p.CreatedAt)
                   .SetSerializer(new DateTimeSerializer(DateTimeKind.Utc));
                map.MapMember(p => p.CategoriaId)
                   .SetElementName("categoriaId")
                   .SetSerializer(new GuidSerializer(BsonType.String));
                map.MapMember(p => p.Categoria)
                   .SetElementName("Categoria")
                   .SetIgnoreIfNull(true);
                map.MapMember(p => p.Valor)
                   .SetSerializer(new DecimalSerializer(BsonType.Decimal128));
            });

        BsonClassMap.RegisterClassMap<Categoria>(
            map =>
            {
                map.AutoMap();
                map.MapIdProperty(c => c.Id)
                    .SetSerializer(new GuidSerializer(BsonType.String));
                map.MapMember(p => p.CreatedAt)
                  .SetSerializer(new DateTimeSerializer(DateTimeKind.Utc));
                map.MapProperty(c => c.Descricao)
                    .SetSerializer(new StringSerializer());
            });


    }
}
