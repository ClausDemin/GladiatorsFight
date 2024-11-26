using GladiatorsFight.Model.AbstractClasses;
using GladiatorsFight.Model.Infrastructure;
using GladiatorsFight.Model.Stats;

namespace GladiatorsFight.Model.Abilities
{
    public class BattleRageDecorator: AbstractFighterDecorator
    {
        private RangedStat _rage;
        private SpreadedStat _ragePerDamage;
        private SpreadedStat _enrageHealAmount;

        public BattleRageDecorator(AbstractFighter fighter, RangedStat rage, SpreadedStat enrageHealAmount, SpreadedStat ragePerDamage) 
            : base(fighter, FighterType.BattleRage)
        {
            _rage = rage;
            _ragePerDamage = ragePerDamage;
            _enrageHealAmount = enrageHealAmount;

            _rage.ValueChanged += OnRageAccumulated;
            HealthRestored += OnHeal;
        }

        public override event Action<FighterType, string>? AbilityUsed;

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);

            AccumulateRage(damage);

            if (IsEnraged()) 
            {
                Enrage();
            }
        }

        private void Enrage() 
        {
            int healAmount = _enrageHealAmount.Value;

            base.Heal(healAmount);

            _rage.ReduceValue(_rage.MaxValue);
        }

        private void AccumulateRage(int damage) 
        {
            int rageAmount = damage * _ragePerDamage.Value;

            _rage.IncreaseValue(rageAmount);
        }

        private bool IsEnraged() 
        {
            return _rage.Value == _rage.MaxValue;
        }

        private void OnHeal(int amount) 
        {
            if (amount > 0) 
            {
                string enrageMessage = $"Впадает в ярость и восстанавливает {amount} здоровья";

                AbilityUsed?.Invoke(_type, enrageMessage);
            }
        }

        private void OnRageAccumulated(int amount) 
        {
            if (amount > 0) 
            { 
                string accumulateRageMessage = $"Накапливает {amount} ярости";
                AbilityUsed?.Invoke(_type, accumulateRageMessage);
            }

        }

        public override string[] GetAbilityDescription()
        {
            List<string> description = new List<string>()
            {
                $"Получая урон, боец накапливает ярость. Когда ярость достигает максимального значения,",
                $"чувствительность к боли у бойца снижается (боец восстанавливает здоровье).",
                $"Каждая единица полученного урона накапливает {_ragePerDamage} ярости.",
                $"Когда боец накопит {_rage.MaxValue} ярости, он восстановит {_enrageHealAmount} здоровья."
            };

            return description.ToArray();
        }
    }
}
