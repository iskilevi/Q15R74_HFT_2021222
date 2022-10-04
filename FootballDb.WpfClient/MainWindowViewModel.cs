using Q15R74_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballDb.WpfClient
{
    public class MainWindowViewModel
    {

        public RestCollection<Player> Players { get; set; }

        public MainWindowViewModel()
        {
            Players = new RestCollection<Player>("http://localhost:53910/", "player");
        }
    }
}
