namespace LuisAngel_GabrieMorillo_AP1_P2.Models
{
    public class EntradasDetalle
    {

        public int Id { get; set; }
        public int EntradaId { get; set; }
        public Entradas? Entrada { get; set; }

        public int ProductoId { get; set; }
        public Productos? Producto { get; set; }

        public double Cantidad { get; set; }
    }
}
