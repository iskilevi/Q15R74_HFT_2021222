using Q15R74_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q15R74_HFT_2021222.Repository
{
    public class PlayerRepository : Repository<Player>, IRepository<Player>
    {
        public PlayerRepository(FootballDbContext ctx) : base(ctx)
        {
        }

        public override Player Read(int id)
        {
            return ctx.Players.FirstOrDefault(t => t.PlayerId == id);
        }

        public override void Update(Player item)
        {
            var old = Read(item.PlayerId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
