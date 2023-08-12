using Sharee.Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sharee.Application.Data.Entities;

public record Unit : IBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Int32 Id { get; set; }
    
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Code is not empty")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = $"{nameof(Code)} cannot be shorter than 3 characters")]
    public String? Code { get; set; }
    
    [Required]
    public Guid Token { get; set; }
    
    [Display(Description = "Last upload data files date")]
    public DateTime? LastUpdateTime { get; set; }

    [Display(Description = "Last download data files date")]
    public DateTime? LastDownloadTime { get; set; }
}