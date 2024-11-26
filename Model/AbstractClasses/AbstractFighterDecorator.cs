using GladiatorsFight.Model.Infrastructure;
using GladiatorsFight.Model.Interfaces;
using GladiatorsFight.Model.Stats;

namespace GladiatorsFight.Model.AbstractClasses
{
    public abstract class AbstractFighterDecorator : AbstractFighter
    {
        protected AbstractFighter _fighter;
        protected FighterType _type;

        public AbstractFighterDecorator(AbstractFighter fighter, FighterType type)
        {
            _fighter = fighter;
            _type = type;

            _fighter.FighterDied += OnFighterDied;
            _fighter.DamageTaken += OnDamageTaken;
            _fighter.HealthRestored += OnHealthRestored;
            _fighter.AttackPerformed += (IAttacker attacker, IDamageable target) => OnAttackPerformed(target);

        }

        public override event Action<AbstractFighter>? FighterDied;
        public override event Action<int>? DamageTaken;
        public override event Action<int>? HealthRestored;
        public override event Action<IAttacker, IDamageable>? AttackPerformed;

        public abstract event Action<FighterType, string>? AbilityUsed;

        public FighterType Type => _type;

        public override int Health => _fighter.Health;

        public override SpreadedStat Damage => _fighter.Damage;

        public override int Armor => _fighter.Armor;

        public override void Attack(IDamageable target)
        {
            _fighter?.Attack(target);
        }

        public override void TakeDamage(int damage)
        {
            _fighter.TakeDamage(damage);
        }

        public override void Heal(int healAmount)
        {
            _fighter.Heal(healAmount);
        }

        public abstract string[] GetAbilityDescription();

        private void OnFighterDied(AbstractFighter fighter)
        {
            FighterDied?.Invoke(this);
        }

        private void OnDamageTaken(int amount)
        {
            DamageTaken?.Invoke(amount);
        }

        public void OnHealthRestored(int amount) 
        { 
            HealthRestored?.Invoke(amount);
        }

        private void OnAttackPerformed(IDamageable target)
        {
            AttackPerformed?.Invoke(this, target);
        }
    }
}
