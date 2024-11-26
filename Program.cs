using GladiatorsFight.Model.AbstractClasses;
using GladiatorsFight.Service;
using GladiatorsFight.View;

namespace GladiatorsFight
{
    public static class Program
    {
        public static bool IsGameOver = false;

        public static void Main(string[] args)
        {
            var mainMenu = new MainMenu();

            mainMenu.BattleStarted += () => ShowArenaView(mainMenu);

            mainMenu.Show();

        }

        public static void OnFighterDeath() 
        {
            IsGameOver = true;
        }

        public static void PrintFightersStats(AbstractFighter fighter) 
        {
            string output = $"Здоровье: {fighter.Health}\n";
            Console.WriteLine(output);
        }

        public static void ShowArenaView(MainMenu menu) 
        { 
            var view =  new ArenaView();

            view.Closed += menu.Show;

            view.Show();
        }
    }


}
