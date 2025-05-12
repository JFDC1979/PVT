using System.ComponentModel.DataAnnotations;

namespace PVTv1.Models
{
    public class GrupoViewModel
    {
        [Key] 
        [Display(Name = "ID Grupo")]
        public int IdGrupo { get; set; } 

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(100, ErrorMessage = "La descripción no puede exceder los 100 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = string.Empty; 
    }
}