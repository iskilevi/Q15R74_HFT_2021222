using Q15R74_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q15R74_HFT_2021222.Repository
{
    public class ClubRepository : Repository<Club>, IRepository<Club>
    {
        public ClubRepository(FootballDbContext ctx) : base(ctx)
        {
        }

        public override Club Read(int id)
        {
            return ctx.Clubs.FirstOrDefault(t => t.ClubId == id);
        }

        public override void Update(Club item)
        {
            var old = Read(item.ClubId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
