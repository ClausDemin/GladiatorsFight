using GladiatorsFight.Model.Interfaces;
using GladiatorsFight.Model.Stats;

namespace GladiatorsFight.Model.AbstractClasses
{
    public abstract class AbstractFighter : IAttacker, IDamageable
    {
        public abstract event Action<AbstractFighter>? FighterDied;

        public abstract event Action<int>? DamageTaken;

        public abstract event Action<int>? HealthRestored;

        public abstract event Action<IAttacker, IDamageable>? AttackPerformed;

        public abstract int Health { get; }
        public abstract SpreadedStat Damage { get; }
        public abstract int Armor { get; }

        public abstract void Attack(IDamageable target);

        public abstract void TakeDamage(int damage);

        public abstract void Heal(int healAmount);

        public string[] GetFighterDescription() 
        { 
            List<string> description = new List<string>() 
            { 
                $"Здоровье: {Health}",
                $"Броня: {Armor}",
                $"Урон: {Damage}"
            };
            
            return description.ToArray();
        }
    }
}
