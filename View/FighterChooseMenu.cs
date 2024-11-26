using GladiatorsFight.Service;
using GladiatorsFight.View.Infrastructure;
using GladiatorsFight.View.Intefaces;
using GladiatorsFight.View.Utils;
using System.Drawing;

namespace GladiatorsFight.View
{
    public class FighterChooseMenu: IUIElement
    {
        private TextBox _text;
        private SwitchableMenu _menu;
        private FighterService _fighterService;

        private bool _isExitRequest;

        public FighterChooseMenu(Point position, FighterService fighterService) 
        {
            _fighterService = fighterService;

            var menuFactory = new FighterChooseMenuFactory(_fighterService, () => GetCurrentItem());
            
            _menu = menuFactory.CreateSwitchableMenu(position);

            var textPosition = UIUtils.GetPositionAfter(_menu);

            _text = new TextBox(textPosition, Console.BufferWidth - textPosition.X - 5);

            _menu.CursorMoved += UpdateFighterInfo;
        }

        public Point Position { get; set; }

        public int Width => GetWidth();

        public int Height => GetHeight();

        public string GetUserInput() 
        {
            Show();

            Run();

            return GetCurrentItem();
        }

        private void Show()
        {
            _isExitRequest = false;

            _menu.Show();

            Run();
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

        private void Close() 
        {
            _isExitRequest = true;

            Console.Clear();
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

                case ConsoleKey.Escape:
                    Close();
                    break;
            }
        }

        private string GetCurrentItem()
        {
            Close();

            return _menu.GetCurrentItem();
        }

        private void UpdateFighterInfo() 
        {
            var item = _menu.GetCurrentItem();

            _text.UpdateText(_fighterService.GetDescription(item));
        }

        private int GetWidth() 
        { 
            return _text.Width + _menu.Width;
        }

        private int GetHeight() 
        {
            return Math.Max(_text.Height, _menu.Height);
        }
    }
}
