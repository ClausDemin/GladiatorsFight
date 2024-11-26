using System.Drawing;
using GladiatorsFight.View.Intefaces;

namespace GladiatorsFight.View
{
    public class SwitchableMenu: IUIElement
    {
        private List<MenuItem> _items;
        private MenuItem _item;
        private int _itemIndex;

        public SwitchableMenu(Point poisition, List<MenuItem> items, char cursorSymbdol = '>')
        {
            _items = items;
            _itemIndex = 0;
            _item = _items[_itemIndex];

            Position = poisition;
            CursorSymbol = cursorSymbdol;
        }

        public event Action CursorMoved;

        public Point Position { get; set; }
        public int Width => GetWidth();
        public int Height => _items.Count;
        public char CursorSymbol { get; }

        public void MoveCursor(CursorMovement direction)
        {
            if (IsCursorInBounds(direction))
            {
                ClearCursor();

                _itemIndex += (int)direction;
                _item = _items[_itemIndex];

                DrawCursor();

                CursorMoved?.Invoke();
            }
        }

        public void Show() 
        {
            for (int i = 0; i < Height; i++) 
            {
                Console.SetCursorPosition(Position.X, Position.Y + i);
                Console.Write(_items[i].Text);
            }

            DrawCursor();

            CursorMoved?.Invoke();   
        }

        public void Click() 
        { 
            _item.Click();
        }

        public string GetCurrentItem() 
        { 
            return _item.Text;
        }

        private void DrawCursor()
        {
            Console.SetCursorPosition(Position.X, Position.Y + _itemIndex);
            Console.Write(CursorSymbol);
        }

        private void ClearCursor()
        {
            Console.SetCursorPosition(Position.X, Position.Y + _itemIndex);
            Console.Write(" ");
        }

        private bool IsCursorInBounds(CursorMovement direction)
        {
            int wishedPosition = _itemIndex + (int)direction;

            return wishedPosition >= 0 && wishedPosition < _items.Count;
        }

        private int GetWidth()
        {
            int width = 0;

            foreach (var item in _items)
            {
                if (item.Width > width)
                {
                    width = item.Width;
                }
            }

            return width;
        }
    }
}
