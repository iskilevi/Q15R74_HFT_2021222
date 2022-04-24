using ConsoleTools;


namespace Q15R74_HFT_2021222.Client
{

    internal class Program
    {

        static void Main(string[] args)
        {
            RestService rest = new RestService("http://localhost:53910/", "player");
            CrudService crud = new CrudService(rest);
            NonCrudService nonCrud = new NonCrudService(rest);

            var clubSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => crud.List("Club"))
                .Add("Create", () => crud.Create("Club"))
                .Add("Delete", () => crud.Delete("Club"))
                .Add("Update", () => crud.Update("Club"))
                .Add("Club Average Age", () => nonCrud.ClubAvgAge())
                .Add("Club All Goals", () => nonCrud.ClubAllGoals())
                .Add("Highest Paid Club", () => nonCrud.HighestPaid())
                .Add("Club Players", () => nonCrud.ClubPlayers())
                .Add("Nations", () => nonCrud.NationList())
                .Add("Exit", ConsoleMenu.Close);

            var managerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => crud.List("Manager"))
                .Add("Create", () => crud.Create("Manager"))
                .Add("Delete", () => crud.Delete("Manager"))
                .Add("Update", () => crud.Update("Manager"))
                .Add("Managers Average Salary", () => nonCrud.ManagerAvgSal())
                .Add("Best Manager", () => nonCrud.BestManager())
                .Add("Exit", ConsoleMenu.Close);

            var playerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => crud.List("Player"))
                .Add("Create", () => crud.Create("Player"))
                .Add("Delete", () => crud.Delete("Player"))
                .Add("Update", () => crud.Update("Player"))
                .Add("Best Attacker", () => nonCrud.BestAttacker())
                .Add("Players Average Age", () => nonCrud.PlayersAvgAge())
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Players", () => playerSubMenu.Show())
                .Add("Managers", () => managerSubMenu.Show())
                .Add("Clubs", () => clubSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }
    }
}
