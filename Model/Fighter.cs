using GladiatorsFight.Model.AbstractClasses;
using GladiatorsFight.Model.Interfaces;
using GladiatorsFight.Model.Stats;

namespace GladiatorsFight.Model
{
    public class Fighter : AbstractFighter
    {
        private RangedStat _health;
        private RangedStat _armor;

        public Fighter(RangedStat health, RangedStat armor, SpreadedStat damage)
        {
            _health = health;
            _armor = armor;
            Damage = damage;

            _health.ValueChanged += OnHealthChanged;
        }

        public override event Action<AbstractFighter>? FighterDied;
        public override event Action<int>? DamageTaken;
        public override event Action<int>? HealthRestored;
        public override event Action<IAttacker, IDamageable>? AttackPerformed;

        public override int Health => _health.Value;

        public override int Armor => _armor.Value;

        public override SpreadedStat Damage { get; }

        public override void Attack(IDamageable target)
        {
            AttackPerformed?.Invoke(this, target);

            target.TakeDamage(Damage.Value);
        }

        public override void TakeDamage(int damage)
        {
            damage = ReduceDamage(damage);

            _health.ReduceValue(damage);
        }

        public override void Heal(int healAmount)
        {
            _health.IncreaseValue(healAmount);
        }

        private int ReduceDamage(int damage) 
        {
            damage = damage * (1 - Armor / 100);

            if (damage <= 0) 
            {
                damage = 1;
            }
            
            return damage;
        }

        private void OnHealthChanged(int amount)
        {
            if (amount < 0)
            {
                DamageTaken?.Invoke(amount);
            }

            if (amount > 0) 
            { 
                HealthRestored?.Invoke(amount);
            }

            if (isDead(_health))
            {
                Die();
            }
        }

        private void Die()
        {
            _health.ValueChanged -= OnHealthChanged;

            FighterDied?.Invoke(this);
        }

        private bool isDead(RangedStat health)
        {
            return _health.Value == _health.MinValue;
        }
    }
}
