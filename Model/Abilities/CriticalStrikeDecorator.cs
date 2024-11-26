using GladiatorsFight.Model.AbstractClasses;
using GladiatorsFight.Model.Infrastructure;
using GladiatorsFight.Model.Interfaces;

namespace GladiatorsFight.Model.Abilities
{
    public class CriticalStrikeDecorator : AbstractFighterDecorator
    {
        private float _criticalStrikeChance;
        private int _criticalDamageMultiplyer;

        public CriticalStrikeDecorator(AbstractFighter fighter, float criticalStrikeChance, int criticalDamageMultiplyer = 2)
            :base(fighter, FighterType.CriticalStrike)
        {
            _criticalStrikeChance = criticalStrikeChance;
            _criticalDamageMultiplyer = criticalDamageMultiplyer;
        }

        public override event Action<FighterType, string>? AbilityUsed;
        public override event Action<IAttacker, IDamageable>? AttackPerformed;

        public override void Attack(IDamageable target)
        {
            if (isCriticalStrike())
            {
                AttackPerformed?.Invoke(this, target);
                
                target.TakeDamage(base.Damage.Value * _criticalDamageMultiplyer);
                
                AbilityUsed?.Invoke(_type, "Наносит критический удар");
            }
            else 
            {
                AttackPerformed?.Invoke(this, target);
                base.Attack(target);
            }
        }

        public override string[] GetAbilityDescription()
        {
            List<string> description = new List<string>()
            {
                $"Боец имеет шанс {_criticalStrikeChance} нанести врагу критический удар.",
                $"Критический удар кратно усиливает атаку бойца.",
                $"Множитель критического урона {_criticalDamageMultiplyer}"
            };

            return description.ToArray();
        }

        private bool isCriticalStrike() 
        { 
            return _criticalStrikeChance >= UserUtils.GetRandomSingle();
        }
    }
}
