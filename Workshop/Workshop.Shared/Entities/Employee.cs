using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Workshop.Shared.Entities;

public class Employee
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    [MaxLength(30, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Apellido")]
    [MaxLength(30, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Estado")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public bool IsActive { get; set; }

    [JsonIgnore]
    [Display(Name = "Fecha")]
    public DateTime HireDate { get; set; } = DateTime.UtcNow;

    public string HireDateForm => HireDate.ToString("dd/MM/yyyy HH:mm");

    [Display(Name = "Salario")]
    [Range(1000000, double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor o igual a {1}.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public decimal Salary { get; set; }
}