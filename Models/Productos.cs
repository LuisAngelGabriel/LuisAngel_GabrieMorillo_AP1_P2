using System.ComponentModel.DataAnnotations;

namespace LuisAngel_GabrieMorillo_AP1_P2.Models
{
    public class Productos
    {

        [Key]

        public int ProductoId { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public double Peso { get; set; }

        public double Existencia { get; set; }

        public bool EsCompuesto { get; set; } = false;

    }
}
