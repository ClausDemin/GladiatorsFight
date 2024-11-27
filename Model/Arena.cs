using GladiatorsFight.Model.AbstractClasses;
using GladiatorsFight.Model.Enums;
using GladiatorsFight.Model.Interfaces;
using GladiatorsFight.Service;

namespace GladiatorsFight.Model
{
    internal class Arena
    {
        private AbstractFighter _leftFighter;
        private AbstractFighter _rightFighter;

        private ArenaInfoBuilder _arenaInfoBuilder;
        private Queue<AbstractFighter> _deadFighters;

        public Arena(AbstractFighter leftFighter, AbstractFighter rightFighter)
        {
            _leftFighter = leftFighter;
            _rightFighter = rightFighter;

            _deadFighters = new Queue<AbstractFighter>();
            _arenaInfoBuilder = new ArenaInfoBuilder();

            _leftFighter.FighterDied += OnFighterDead;
            _leftFighter.AttackPerformed += OnAttack;
            _leftFighter.DamageTaken += OnDamageTaken;
            ((AbstractFighterDecorator)_leftFighter).AbilityUsed += 
                (FighterType type, string message) => _arenaInfoBuilder.AddAbilityUseInfo(GetFighterNumber(type), message);

            _rightFighter.FighterDied += OnFighterDead;
            _rightFighter.AttackPerformed += OnAttack;
            _rightFighter.DamageTaken += OnDamageTaken;
            ((AbstractFighterDecorator)_rightFighter).AbilityUsed += 
                (FighterType type, string message) => _arenaInfoBuilder.AddAbilityUseInfo(GetFighterNumber(type), message);
        }

        public event Action<FighterNumber>? FightOver;
        public event Action<string>? AttackPerformed;

        public void MakeTurn()
        {
            _leftFighter.Attack(_rightFighter);
            _arenaInfoBuilder.AddCurrentHealthInfo(FighterNumber.Second, _rightFighter.Health);
            AttackPerformed?.Invoke(_arenaInfoBuilder.Build().GetInfo());

            _rightFighter.Attack(_leftFighter);
            _arenaInfoBuilder.AddCurrentHealthInfo(FighterNumber.First, _leftFighter.Health);
            AttackPerformed?.Invoke(_arenaInfoBuilder.Build().GetInfo());

            if (_deadFighters.Count > 0) 
            {
                FightOver?.Invoke(GetFightResult());
            } 
        }

        private void OnFighterDead(AbstractFighter fighter)
        {
            _deadFighters.Enqueue(fighter);
        }

        private void OnAttack(IAttacker attacker, IDamageable damageable)
        {
            _arenaInfoBuilder.AddAttackInfo(GetFighterNumber(attacker), GetFighterNumber(damageable));
        }

        private void OnDamageTaken(int amount) 
        {
            _arenaInfoBuilder.AddDamageInfo(amount);
        }

        private FighterNumber GetFighterNumber(IAttacker attacker)
        {
            if ((attacker as AbstractFighter) == _leftFighter)
            {
                return FighterNumber.First;
            }

            else if ((attacker as AbstractFighter) == _rightFighter)
            {
                return FighterNumber.Second;
            }

            return FighterNumber.Nobody;
        }

        private FighterNumber GetFighterNumber(IDamageable damageable)
        {
            if ((damageable as AbstractFighter) == _leftFighter)
            {
                return FighterNumber.First;
            }

            else if ((damageable as AbstractFighter) == _rightFighter)
            {
                return FighterNumber.Second;
            }

            return FighterNumber.Nobody;
        }

        private FighterNumber GetFighterNumber(FighterType type) 
        {
            if ((_leftFighter as AbstractFighterDecorator)?.Type == type)
            {
                return FighterNumber.First;
            }
            else if ((_rightFighter as AbstractFighterDecorator)?.Type == type) 
            {
                return FighterNumber.Second;
            }

            return FighterNumber.Nobody;
        }

        private FighterNumber GetFightResult() 
        {
            if (_deadFighters.Count > 1)
            {
                return FighterNumber.Nobody;
            }
            else if (_deadFighters.Dequeue() == _leftFighter)
            {
                return FighterNumber.Second;
            }

            return FighterNumber.First;
        }
    }
}
