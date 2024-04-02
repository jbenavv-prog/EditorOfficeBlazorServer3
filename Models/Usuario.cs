using System.ComponentModel.DataAnnotations;

namespace EditorOfficeBlazorServer3.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Cedula { get; set; }

        public string Direccion { get; set; }
    }
}
