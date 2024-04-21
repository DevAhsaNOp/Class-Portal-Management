using Domain.DbEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int? DeletedBy { get; set; }
    public int Status { get; set; }

    [ForeignKey("CreatedBy")]
    public tblAdmin CreatedByAdmin { get; set; }

    [ForeignKey("UpdatedBy")]
    public tblAdmin UpdatedByAdmin { get; set; }

    [ForeignKey("DeletedBy")]
    public tblAdmin DeletedByAdmin { get; set; }
}