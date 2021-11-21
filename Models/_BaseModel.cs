using System.Runtime.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using FOBOS_API.Data;

namespace FOBOS_API.Models
{
  [DataContract]
  public abstract class _BaseModel
  {
    [Key]
    [Column(Name = "SQ_CODIGO")]
    public int? id { get; set; }
    [Column(Name = "BL_ATIVO")]
    public bool ativo { get; set; }
    [Column(Name = "DT_CREATED_AT")]
    public DateTime? createdAt { get; set; } = DateTime.Now;
    [Column(Name = "DT_UPDATED_AT")]
    public DateTime? updatedAt { get; set; }
    public virtual DateTime now { get; set; } = DateTime.Now;
    }
}