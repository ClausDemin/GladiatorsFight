using GladiatorsFight.Model;

namespace GladiatorsFight.Service
{
    public class ArenaInfoBuilder
    {
        private FighterNumber _attacker;
        private FighterNumber _damageable;

        private Dictionary<FighterNumber, int> _currentHealthInfo = new Dictionary<FighterNumber, int>();
        
        private List<int> _damageTaken = new List<int>();
        private Dictionary<FighterNumber, List<string>> _abilityUsed  = new Dictionary<FighterNumber, List<string>>();

        public ArenaInfoBuilder AddAttackInfo(FighterNumber attacker, FighterNumber damageable) 
        { 
            _attacker = attacker;
            _damageable = damageable;

            return this;
        }

        public ArenaInfoBuilder AddDamageInfo(int amount) 
        {
            if (amount < 0) 
            { 
                _damageTaken.Add(-amount);
            }

            return this;
        }

        public ArenaInfoBuilder AddAbilityUseInfo(FighterNumber fighter, string message) 
        {
            if (_abilityUsed.ContainsKey(fighter))
            {
                _abilityUsed[fighter].Add(message);
            }
            else 
            { 
                _abilityUsed[fighter] = new List<string>() {message};
            }

            return this;
        }

        public ArenaInfoBuilder AddCurrentHealthInfo(FighterNumber number, int health) 
        {
            _currentHealthInfo[number] = health;

            return this;
        }

        public ArenaInfo Build() 
        {
            var info = new ArenaInfo(_attacker, _damageable, _currentHealthInfo, _damageTaken, _abilityUsed);
            
            Reset();
            
            return info;
        }

        private void Reset() 
        { 
            _attacker = FighterNumber.Nobody;
            _damageable = FighterNumber.Nobody;
            _abilityUsed = new Dictionary<FighterNumber, List<string>>();
            _damageTaken = new List<int>();
            _currentHealthInfo = new Dictionary<FighterNumber, int>();
        }
    }
}
