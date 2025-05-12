using System.ComponentModel.DataAnnotations;

namespace PVTv1.Models
{
    public class UsuarioViewModel
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre y apellido son obligatorios.")]
        [StringLength(150, ErrorMessage = "El nombre y apellido no pueden exceder los 150 caracteres.")]
        [Display(Name = "Nombre y Apellido")]
        public string NombreApellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La sigla es obligatoria.")]
        [StringLength(10, ErrorMessage = "La sigla no puede exceder los 10 caracteres.")]
        [Display(Name = "Sigla")]
        public string Sigla { get; set; } = string.Empty;
    }
}