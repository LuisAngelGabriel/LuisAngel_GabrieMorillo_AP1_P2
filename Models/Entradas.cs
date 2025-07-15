using System.ComponentModel.DataAnnotations;

namespace LuisAngel_GabrieMorillo_AP1_P2.Models
{
    public class Entradas
    {

        [Key]
        public int EntradaId { get; set; }


        [Required]
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        public string Concepto { get; set; } = string.Empty;    

        public double PesoTotal { get; set; }

        public int IdProducido { get; set; }

        public Productos? Producido { get; set; }
        public int CantidadProducida { get; set; }

        public List<EntradasDetalle> Detalles { get; set; } = new List<EntradasDetalle>();

    }
}
