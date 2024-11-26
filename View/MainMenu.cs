using System.Drawing;

namespace GladiatorsFight.View
{
    public class MainMenu
    {
        private SwitchableMenu _menu;

        bool _isExitRequest;
        public MainMenu() 
        {
            var menuItems = new List<MenuItem>();

            var startBattleMenuItem = new MenuItem("Начать бой");
            startBattleMenuItem.OnClick += StartBattle;

            var closeApplicationMenuItem = new MenuItem("Закрыть приложение");
            closeApplicationMenuItem.OnClick += CloseApplication;

            menuItems.Add(startBattleMenuItem);
            menuItems.Add(closeApplicationMenuItem);

            _menu = new SwitchableMenu(new Point(0, 0), menuItems);
        }

        public event Action? BattleStarted;

        public void Show() 
        {
            _isExitRequest = false;

            _menu.Show();
            
            Run();
        }

        private void StartBattle() 
        {
            Console.Clear();

            BattleStarted?.Invoke();
        }

        private void CloseApplication() 
        {
            Console.Clear();

            _isExitRequest = true;
        }

        private void Run() 
        {
            Console.CursorVisible = false;

            while (_isExitRequest == false)
            {
                var command = Console.ReadKey(true).Key;

                HandleUserInput(command);
            }
        }

        private void HandleUserInput(ConsoleKey command)
        {
            switch (command)
            {
                case ConsoleKey.Enter:
                    _menu.Click();
                    break;

                case ConsoleKey.UpArrow:
                    _menu.MoveCursor(CursorMovement.Up);
                    break;

                case ConsoleKey.DownArrow:
                    _menu.MoveCursor(CursorMovement.Down);
                    break;
            }
        }
    }
}
