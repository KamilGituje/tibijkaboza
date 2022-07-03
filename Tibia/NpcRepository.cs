using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public class NpcRepository : ItemRepository
    {
        public NpcRepository()
        {
            NpcBase.Add(new Npc
            {
                Name = "Sam",
                ItemsSell =
                {
                    new NpcItemRepository("Katana").SetPrice(60),
                    new NpcItemRepository("Short sword").SetPrice(20),
                    new NpcItemRepository("Legion helmet").SetPrice(30),
                    new NpcItemRepository("Halberd").SetPrice(400)
                }
            });
            NpcBase.Add(new Npc
            {
                Name = "Frodo",
                ItemsSell =
                {
                    new NpcItemRepository("Cyclops toe").SetPrice(30)
                }
            });
        }
        private static List<Npc> NpcBase = new List<Npc>();
        public Npc GetNpc (string npcName)
        {
            var npc = new Npc();
            try
            {
                npc = NpcBase.First(npc => npc.Name == npcName);
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong name");
            }
            return npc;
        }
    }
}
