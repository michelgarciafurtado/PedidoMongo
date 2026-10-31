# 📦 PedidoMongo

Projeto de estudos focado na exploração e implementação de persistência de dados utilizando **.NET** e **MongoDB** (banco de dados NoSQL orientado a documentos).

---

## 🎯 Objetivos do Projeto
- Compreender os conceitos teóricos e práticos de bancos de dados orientados a documentos.
- Implementar operações CRUD em agregados e entidades do domínio.
- Praticar abordagens de modelagem NoSQL: transicionando entre **Referência (Lookup)** e **Embutimento (Embedding/Desnormalização)**.
- Identificar e analisar as principais diferenças de modelagem, integridade de dados e performance entre o modelo relacional (SQL) e o modelo orientado a documentos (NoSQL).

---

## 🧠 Aprendizados e Experiência com MongoDB

### 🚀 Principais Conhecimentos Adquiridos

- **Evolução da Modelagem de Dados:**
  - **Abordagem Inicial (Relacional/Lookup):** Inicialmente, a modelagem seguiu padrões de bancos de dados relacionais (uso extensivo de IDs e propriedades de navegação). Para resolver o acoplamento de consultas como `Produto -> Categoria`, utilizei a etapa `$lookup` do MongoDB pipelines via agregações (`ProdutoLookUp` e DTOs de consulta).
  - **Transição para a Abordagem NoSQL (Desnormalização):** Percebi que a melhor prática em bancos orientados a documentos costuma ser a **desnormalização (duplicação de dados)**. No modelo final de `Pedidos`, dados essenciais de `Cliente`, `Endereco` e `Item` foram incorporados/embutidos diretamente no documento do pedido, reduzindo a necessidade de *joins/lookups* em tempo de leitura.

- **Integridade de Dados e Camada de Aplicação:**
  - Diferente do ecossistema relacional, em que o banco garante a integridade referencial por meio de *Foreign Keys*, no MongoDB a garantia da consistência dos dados desnormalizados passa a ser **responsabilidade da camada de aplicação e do modelo de domínio**.


---

### 💭 Impressões e Desafios

- **Flexibilidade de Esquema (*Schema-less*):** A ausência de um *schema* rígido no banco de dados acelera a persistência e a evolução da aplicação. No entanto, exige validações robustas e mapeamento cuidadoso via C# Driver para evitar persistência de tipos incorretos ou documentos inconsistentes.
- **Mudança de Mentalidade (SQL vs. NoSQL):** A transição da lógica de tabelas/chaves estrangeiras para documentos JSON/BSON desnormalizados proporciona grandes ganhos de performance em leitura e simplifica o mapeamento objeto-documento no ecossistema .NET.

---

### Primeira abordagem, (relacionamento/LookUp)
```csharp

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
```

```csharp

public sealed record CriarProdutoDto
(string Descricao, string IdCategoria, decimal Valor);


public sealed class ProdutosConsulta : IProdutosConsultas
{
    private readonly IMongoCollection<Produto> _produtosCollection;
    private readonly IMongoCollection<Categoria> _categoriasCollection;

    public ProdutosConsulta(IMongoDatabase database)
    {
        _produtosCollection = database.GetCollection<Produto>("Produtos");
        _categoriasCollection = database.GetCollection<Categoria>("Categorias");
    }

    public Task<ObterProdutoDto?> ObterPorIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ObterProdutoDto>> ObterTodosAsync()
    {
        var resultado = await _produtosCollection
            .Aggregate()
            .Lookup<Produto, Categoria, ProdutoLookUp>(
                _categoriasCollection,
                produto => produto.CategoriaId,
                categoria => categoria.Id,
                resultado => resultado.Categorias
            ).ToListAsync();

        return resultado.Select(produto =>
                new ObterProdutoDto(
                        produto.Id.ToString(),
                        produto.Descricao,
                        new ObterCategoriaDto(
                            produto.Categorias.First().Id.ToString(),
                            produto.Categorias.First().Descricao
                         ),
                        produto.Valor
                        )
                ).ToList();
    }
}

```
### resultado da abordagem (como ficou no banco)

```json

{
  _id: '345abb15-cdbc-49f9-bf1f-2701c13be9d8',
  CreatedAt: ISODate('2026-10-29T23:46:35.208Z'),
  Descricao: 'X-Calabresa',
  CategoriaId: 'd8baf24e-ab14-44ed-ab39-2d01c7615e0e',
  Valor: NumberDecimal('25.5')
}
{
  _id: '96c9c341-711f-4e5f-99ef-b2079ecf4d14',
  CreatedAt: ISODate('2026-10-29T23:56:41.427Z'),
  Descricao: 'Cheese-Burguer',
  CategoriaId: 'd8baf24e-ab14-44ed-ab39-2d01c7615e0e',
  Valor: NumberDecimal('18.3')
}

```
### Agora usando a abordagem mais tradicional do mongo sem mapeamentos e com campos duplicados

```csharp

public sealed class CriarPedidoDto
{
    [Required(ErrorMessage ="A data do pedido deve ser informada")]
    public DateTime DataPedido { get; set; }
    [Required(ErrorMessage = "Id do cliente nao informado")]
    public Guid IdCliente { get; set; }
    public string NomeCliente { get; set; }
    public CriarEnderecoDto? EnderecoEntrega { get; set; }
    public int StatusEnum { get; set; }
    public List<CriarItemPedidoDto>? Itens { get; set; }
}

public record CriarItemPedidoDto(
 Guid ProdutoId,
 string NomeProduto,
 decimal Valor,
 int Qtd
);

public sealed class PedidosUseCase(IPedidoRepository pedidoRepository) : IPedidoUseCase
{
    private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
    public async Task CriarPedido(CriarPedidoDto dto)
    {
        var itensDoDominio = dto.Itens?
       .Select(itemDto => new Item(
           itemDto.ProdutoId,
           itemDto.NomeProduto,
           itemDto.Valor,
           itemDto.Qtd
       )).ToList() ?? new List<Item>();


        PedidosMongo.Domain.Agregados.Pedidos novoPedido = new
             PedidosMongo.Domain.Agregados.Pedidos(dto.DataPedido,
             dto.IdCliente,
             new Cliente(dto.IdCliente, dto.NomeCliente),
             new Endereco(dto.EnderecoEntrega.Rua,
                                dto.EnderecoEntrega.n,dto.EnderecoEntrega.Bairro,
                                dto.EnderecoEntrega.Cidade, dto.EnderecoEntrega.Cep,
                                dto.EnderecoEntrega.compl),
             (StatusPedidoEnum)dto.StatusEnum,
             itensDoDominio
             );

        await _pedidoRepository.CriarPedidoAsync(novoPedido);
    }
}
 
```

### Como ficou no banco

```json

{
  _id: '14f3a1eb-2066-4070-8561-d48cc6d34309',
  CreatedAt: ISODate('2026-10-30T22:41:02.404Z'),
  DataPedido: ISODate('2026-10-30T19:20:00.000Z'),
  IdCliente: 'a4b2c1d3-e4f5-6789-0123-456789abcdef',
  Cliente: {
    _id: 'a4b2c1d3-e4f5-6789-0123-456789abcdef',
    CreatedAt: ISODate('2026-10-30T22:41:02.403Z'),
    Nome: 'João Silva',
    Cpf: null,
    Tel: null,
    Endereco: null
  },
  EnderecoEntrega: {
    Rua: 'R das Acacias',
    n: NumberInt('123'),
    Bairro: 'Jd das Flores',
    Cidade: 'Sorocaba',
    Cep: '18075000',
    compl: 'casa'
  },
  Status: NumberInt('1'),
  Itens: [
    {
      _id: '1ed75e6c-e960-411b-b143-8f25de258d93',
      CreatedAt: ISODate('2026-10-30T22:41:02.403Z'),
      ProdutoId: '345abb15-cdbc-49f9-bf1f-2701c13be9d8',
      Descricao: 'X-Calabresa',
      ValorUn: NumberDecimal('25.5'),
      Qtd: NumberInt('1'),
      VlUn: NumberDecimal('25.5')
    },
    {
      _id: 'f5527e75-ae8e-4edd-8fa0-214d3eb89d21',
      CreatedAt: ISODate('2026-10-30T22:41:02.403Z'),
      ProdutoId: '96c9c341-711f-4e5f-99ef-b2079ecf4d14',
      Descricao: 'Cheese-Burguer',
      ValorUn: NumberDecimal('18.3'),
      Qtd: NumberInt('1'),
      VlUn: NumberDecimal('18.3')
    }
  ],
  VlTotal: NumberDecimal('43.8')
}

```
## 🛠️ Tecnologias e Ferramentas
- **Linguagem & Framework:** .NET (C#)
- **Banco de Dados:** MongoDB
- **Driver de Conexão:** MongoDB.Driver para C# / .NET
- **Ferramenta de Gestão do BD:** MongoDB Compass
- **IDE:** Visual Studio

---

## 🏗️ Modelo de Domínio

```csharp
public class EntidadeBase
{
    public EntidadeBase()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }
    public EntidadeBase(Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

}

public class Pedidos:EntidadeBase
{
    public Pedidos(DateTime dataPedido, Guid idCliente, Cliente? cliente, Endereco? enderecoEntrega, StatusPedidoEnum status, List<Item>? itens)
    {
        DataPedido = dataPedido;
        IdCliente = idCliente;
        Cliente = cliente;
        EnderecoEntrega = enderecoEntrega;
        Status = status;
        Itens = itens;
        VlTotal = CalculaValorTotal();
    }

    private decimal CalculaValorTotal()
    {
        if (Itens == null || !Itens.Any())
            throw new ArgumentException("Pedido sem itens");
        var resultado = Itens.Sum(item => item.ValorUn * item.Qtd);
        return resultado;
    }

    public DateTime DataPedido { get; set; }
    public Guid IdCliente { get; set; }
    public Cliente? Cliente { get; set; }
    public Endereco? EnderecoEntrega { get; set; }
    public StatusPedidoEnum Status { get; set; }
    public List<Item>? Itens { get; set; }
    public decimal VlTotal { get; set; }
}
public sealed class Categoria:EntidadeBase
{
    public string Descricao { get; set; }
    public Categoria(string descricao)
    {
        Descricao = descricao;
    }
}

public class Cliente : EntidadeBase
{
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Tel { get; set; }
    public Endereco Endereco { get; set; }

    public Cliente(string nome, string cpf, string tel, Endereco endereco)
    {
        Nome = nome;
        Cpf = cpf;
        Tel = tel;
        Endereco = endereco;
    }

    public Cliente(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }

}

public class Item:EntidadeBase
{
    public Guid ProdutoId { get; set; }
    public string Descricao { get; set; }
    public decimal ValorUn { get; set; }
    public int Qtd { get; set; }
    public decimal VlUn { get; set; }

    public Item(Guid produtoId, string descricao, decimal valorUn, int qtd)
    {
        ProdutoId = produtoId;
        Descricao = descricao;
        ValorUn = valorUn;
        Qtd = qtd;
        VlUn = CalculaTotalItem();
    }

    private decimal CalculaTotalItem()
    {
        if (Qtd <= 0)
            new ArgumentException("A quantidade não pode ser nula ou 0");
        if (ValorUn <= 0)
            new ArgumentException("O valor não pode ser nulo");
        return Qtd * ValorUn;
            
    }
}
public sealed class Produto:EntidadeBase
{
    public Produto(string descricao, Guid categoriaId, Categoria? categoria, decimal valor)
    {
        Descricao = descricao;
        CategoriaId = categoriaId;
        Categoria = categoria;
        Valor = valor;
    }

    public Produto(Guid id, string descricao, Categoria categoria, decimal valor)
    {
        Id = id;
        Descricao = descricao;
        CategoriaId = categoria.Id;
        Categoria = categoria;
        Valor = valor;
    }

    public string Descricao { get; set; }
    public Guid CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
    public decimal Valor { get; set; }
}


```
