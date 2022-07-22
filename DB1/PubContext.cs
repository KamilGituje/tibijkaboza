using Microsoft.EntityFrameworkCore;
using TibiaModels.BL;

namespace DB1
{
    public class PubContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemMonster> ItemMonster { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Npc> Npcs { get; set; }
        public DbSet<ItemNpc> ItemNpc { get; set; }
        public DbSet<ItemInstance> ItemInstances { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source = DESKTOP-HBGE4GF; Initial catalog = tibia; Integrated security = true");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ItemNpc>().HasKey(itn => new { itn.NpcId, itn.ItemId });
            builder.Entity<ItemMonster>().HasKey(mli => new { mli.MonsterId, mli.ItemId });
            builder.Entity<ItemInstance>().Property(ii => ii.ContainerId).IsRequired(false);
        }
    }
}