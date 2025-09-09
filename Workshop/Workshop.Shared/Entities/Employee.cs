using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Shared.Entities;

public class Employee
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Apellido")]
    [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Estado")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public bool IsActive { get; set; }

    [Display(Name = "Fecha")]
    public DateTime HireDate { get; set; } = DateTime.UtcNow;

    [Display(Name = "Salario")]
    [Range(0, double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor o igual a {1}.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public decimal Salary { get; set; }
}