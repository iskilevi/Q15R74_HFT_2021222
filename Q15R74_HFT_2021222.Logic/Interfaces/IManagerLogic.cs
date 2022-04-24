using Q15R74_HFT_2021222.Models;
using System.Linq;

namespace Q15R74_HFT_2021222.Logic
{
    public interface IManagerLogic
    {
        void Create(Manager item);
        void Delete(int id);
        Manager Read(int id);
        IQueryable<Manager> ReadAll();
        void Update(Manager item);
        double? ManagerAvgSal();
    }
}