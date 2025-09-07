using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.General;

[Table("Roles")]
[PrimaryKey("RoleID")]
public class Role
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoleID { get; set; }
    [Column(TypeName = "varchar(20)")]
    public string RoleTitle { get; set; } = null!;
    public ICollection<Employee> Employees { get; }
}