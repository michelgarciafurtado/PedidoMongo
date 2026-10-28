using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosMongo.Domain.Agregados;

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
