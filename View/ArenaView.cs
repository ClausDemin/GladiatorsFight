using GladiatorsFight.Service;
using GladiatorsFight.View.Utils;
using System.Drawing;

namespace GladiatorsFight.View
{
    public class ArenaView
    {
        private BattleLogger _battleLogger;
        private ArenaService _arenaService;

        private FighterChooseMenu _fighterChooseMenu;
        private TextBox _textBox;

        public ArenaView() 
        {
            _battleLogger = new BattleLogger();

            var fighterService = new FighterService(_battleLogger);

            _fighterChooseMenu = new FighterChooseMenu(new Point(0, 0), fighterService);

            var textBoxPosition = UIUtils.GetPositionAfter(_fighterChooseMenu);

            _textBox = new TextBox(new Point(0, 0), Console.BufferWidth - 5);

            _arenaService = new ArenaService(_fighterChooseMenu.GetUserInput(), _fighterChooseMenu.GetUserInput(), fighterService, _battleLogger);

            _arenaService.GameOver += OnGameOver;

            _battleLogger.MessageReceived += (string message) => 
                PrintBattleMessage
                (
                    message.Split('.')
                    .Select(x => x.Trim())
                    .ToArray()
                );
        }

        public event Action? Closed;

        public void Show() 
        {
            Run();

            Close();
        }

        private void Run() 
        {
            _arenaService.StartFight();
        }

        public void Close() 
        { 
            Console.Clear();

            Closed?.Invoke();
        }

        private void PrintBattleMessage(string[] message) 
        {
            _textBox.UpdateText(message);
            
            Thread.Sleep(2000);
        }

        private void OnGameOver() 
        {
            _textBox.AppendText("Нажмите любую клавишу для возврата в главное меню", ConsoleColor.Yellow);
            Console.ReadKey(true);
        }
    }
}
