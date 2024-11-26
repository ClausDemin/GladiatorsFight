using GladiatorsFight.Model;
using System.Text;

namespace GladiatorsFight.Service
{
    public class BattleLogger
    {
        private List<string> _loggedMessages = new List<string>();

        public event Action<string>? MessageReceived;
        
        public void SendMessage(string message) 
        { 
            _loggedMessages.Add(message);

            MessageReceived?.Invoke(message);
        }
    }
}
