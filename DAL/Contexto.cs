using Microsoft.EntityFrameworkCore;

namespace LuisAngel_GabrieMorillo_AP1_P2.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    }
}
