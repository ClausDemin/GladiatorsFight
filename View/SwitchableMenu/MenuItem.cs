namespace GladiatorsFight.View
{
    public class MenuItem
    {
        private string _text;

        public MenuItem(string text, int whiteSpacesCount = 2)
        {
            _text = AddWhiteSpaces(text, whiteSpacesCount);
        }

        public event Action? OnClick;

        public string Text => _text;
        public int Width => _text.Length;

        public void Click()
        {
            OnClick?.Invoke();
        }

        public override string ToString()
        {
            return _text;
        }

        private string AddWhiteSpaces(string text, int whiteSpacesCount)
        {
            string result = string.Empty;

            for (int i = 0; i < whiteSpacesCount; i++)
            {
                result += ' ';
            }

            result += text;

            return result;
        }
    }
}
