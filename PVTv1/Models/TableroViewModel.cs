
using System;
using System.ComponentModel.DataAnnotations;

namespace PVTv1.Models
{
    public class TableroViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del tablero es obligatorio.")]
        [Display(Name = "Nombre del Tablero")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string NombreTablero { get; set; } = string.Empty;

        [Display(Name = "Descripción")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La URL es obligatoria.")]
        [Url(ErrorMessage = "Por favor, ingrese una URL válida.")]
        [Display(Name = "URL")]
        public string URL { get; set; } = string.Empty;

        [Display(Name = "Grupo")]
        [StringLength(50, ErrorMessage = "El grupo no puede exceder los 50 caracteres.")]
        public string? Grupo { get; set; }

        [Required(ErrorMessage = "El orden es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El orden debe ser un número positivo.")]
        [Display(Name = "Orden")]
        public int Orden { get; set; }

        [Required(ErrorMessage = "La fecha de modificación es obligatoria.")]
        [Display(Name = "Fecha de Modificación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaModificacion { get; set; } = DateTime.Today;

    }
}