using Q15R74_HFT_2021222.Models;
using System.Linq;

namespace Q15R74_HFT_2021222.Logic
{
    public interface IClubLogic
    {
        void Create(Club item);
        void Delete(int id);
        Club Read(int id);
        IQueryable<Club> ReadAll();
        void Update(Club item);
    }
}