using System.Drawing;
using GladiatorsFight.View.Intefaces;

namespace GladiatorsFight.View
{
    public class TextBox : IUIElement
    {
        private int _marginRight;
        private string[] _text;
        private Point _cursorPosition;
        public TextBox(Point position, int width, int marginRight = 5)
        {
            Position = position;
            _marginRight = marginRight;
            Width = width - marginRight;
            _cursorPosition = position;

            _text = [string.Empty];
        }

        public Point Position { get; set; }
        public int Height => _text.Length;
        public int Width { get; }

        public void UpdateText(string[] text, ConsoleColor textColor = ConsoleColor.Gray)
        {
            _text = text;

            ClearOutput();

            Console.SetCursorPosition(Position.X, Position.Y);

            for (int i = 0; i < Height; i++)
            {
                PrintLine(_text[i], textColor);
            }

        }

        public void AppendText(string text, ConsoleColor textColor = ConsoleColor.Gray)
        {
            if (text != string.Empty) 
            {
                var newText = _text.ToList();

                newText.Add(text);

                _text = newText.ToArray();

                PrintLine(text, textColor);
            }
        }

        private void PrintLine(string text, ConsoleColor textColor = ConsoleColor.Gray)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;

            Console.ForegroundColor = textColor;

            for (int i = 0; i < text.Length; i++)
            {
                Console.SetCursorPosition(_cursorPosition.X, _cursorPosition.Y);
                Console.Write(text[i]);

                if (_cursorPosition.X >= Width)
                {
                    _cursorPosition.Y++;
                    _cursorPosition.X = Position.X;
                }
                else 
                { 
                    _cursorPosition.X++;
                } 
            }

            _cursorPosition.Y++;
            _cursorPosition.X = Position.X;

            Console.ForegroundColor = defaultColor;
        }

        public string GetUserInput(string message)
        {
            Console.CursorVisible = true;

            UpdateText([message]);

            string output = Console.ReadLine();

            Console.CursorVisible = false;

            return output;
        }

        private void ClearOutput()
        {
            for (int i = _cursorPosition.Y; i >= Position.Y; i--)
            {
                for (int j = Console.BufferWidth - _marginRight; j >= Position.X; j--)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write(' ');
                }
            }

            _cursorPosition.X = Position.X;
            _cursorPosition.Y = Position.Y;
        }
    }
}
