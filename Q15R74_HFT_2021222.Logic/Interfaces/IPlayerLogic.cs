using Q15R74_HFT_2021222.Models;
using System.Linq;

namespace Q15R74_HFT_2021222.Logic
{
    public interface IPlayerLogic
    {
        void Create(Player item);
        void Delete(int id);
        Player Read(int id);
        IQueryable<Player> ReadAll();
        void Update(Player item);
    }
}