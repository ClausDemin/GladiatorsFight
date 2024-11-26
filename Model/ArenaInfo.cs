namespace GladiatorsFight.Model
{
    public class ArenaInfo
    {
        private FighterNumber _attacker;
        private FighterNumber _damageable;

        private Dictionary<FighterNumber, int> _currentHealthInfo = new Dictionary<FighterNumber, int>();

        private List<int> _damageTaken = new List<int>();
        private Dictionary<FighterNumber, List<string>> _abilityUsed = new Dictionary<FighterNumber, List<string>>();


        public ArenaInfo
        (
            FighterNumber attacker, 
            FighterNumber damageable,
            Dictionary<FighterNumber, int> currentHealthInfo,
            List<int> damageTaken,
            Dictionary<FighterNumber, List<string>> abilityUsedInfo
            ) 
        { 
            _attacker = attacker;
            _damageable = damageable;
            _currentHealthInfo = currentHealthInfo;
            _damageTaken = damageTaken;
            _abilityUsed = abilityUsedInfo;
        }

        public string GetInfo()
        {
            string result = string.Empty;

            result = ParseAttackInfo(result);

            result = ParseAbilityUsedInfo(result);

            result = ParseDamageInfo(result);

            return result;
        }

        private string ParseDamageInfo(string result)
        {
            if (_damageTaken != null && _damageTaken.Count > 0)
            {
                foreach (var value in _damageTaken)
                {
                    result += $"Боец {(int)_damageable} получает {value} урона. " +
                        $"У бойца {(int) _damageable} осталось {_currentHealthInfo[_damageable]} здоровья.";
                }
            }

            return result;
        }

        private string ParseAbilityUsedInfo(string result)
        {
            if (_abilityUsed != null && _abilityUsed.Count > 0)
            {
                foreach (var message in _abilityUsed.OrderBy(x => x.Key))
                {
                    foreach (var item in message.Value)
                    {
                        result += $"Боец {(int)message.Key} {item}.";
                    }
                }
            }

            return result;
        }

        private string ParseAttackInfo(string result)
        {
            if (_attacker != FighterNumber.Nobody && _damageable != FighterNumber.Nobody)
            {
                result += $"Боец {(int)_attacker} атакует бойца {(int)_damageable}.";
            }

            return result;
        }
    }
}
