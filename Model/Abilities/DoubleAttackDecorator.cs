using GladiatorsFight.Model.AbstractClasses;
using GladiatorsFight.Model.Infrastructure;
using GladiatorsFight.Model.Interfaces;

namespace GladiatorsFight.Model.Abilities
{
    public class DoubleAttackDecorator : AbstractFighterDecorator
    {
        private int _doubleAttackCooldown;
        private int _turnsPassed;

        public DoubleAttackDecorator(AbstractFighter fighter, int doubleAttackCooldown) 
            :base(fighter, FighterType.DoubleAttack)
        {
            _doubleAttackCooldown = doubleAttackCooldown;
            _turnsPassed = 0;
        }

        public override event Action<FighterType, string>? AbilityUsed;

        public override void Attack(IDamageable target)
        {
            if (IsDoubleAttackAvailable(_turnsPassed))
            {
                for (int i = 0; i < 2; i++)
                {
                    base.Attack(target);
                }

                _turnsPassed = 0;

                AbilityUsed?.Invoke(_type, "Атакует дважды");
            }
            else 
            {
                base.Attack(target);
                _turnsPassed++;
            }
        }

        public override string[] GetAbilityDescription()
        {
            List<string> description = new List<string>() 
            { 
                $"Каждая {_doubleAttackCooldown} атака этого бойца будет двойной"
            };

            return description.ToArray();
        }

        private bool IsDoubleAttackAvailable(int turnsPassed) 
        {
            return turnsPassed >= _doubleAttackCooldown;
        }
    }
}
