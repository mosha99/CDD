using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace SimpleWebApi.Infrastructure.DomainInfra.EntityBase;

public abstract class Entity : IEntity
{
    public int Version { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public long? LastModifiedBy { get; set; }
    public long TenancyId { get; set; }
    public bool IsDeleted { get; set; }
}
