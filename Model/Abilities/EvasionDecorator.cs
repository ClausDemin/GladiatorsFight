using GladiatorsFight.Model.AbstractClasses;
using GladiatorsFight.Model.Infrastructure;

namespace GladiatorsFight.Model.Abilities
{
    public class EvasionDecorator : AbstractFighterDecorator
    {
        private float _evasionChance;

        public EvasionDecorator(AbstractFighter fighter, float evasionChance)
            : base(fighter, FighterType.Evasion)
        {
            _evasionChance = evasionChance;
        }

        public override event Action<FighterType, string>? AbilityUsed;

        public override string[] GetAbilityDescription()
        {
            List<string> description = new List<string>() 
            { 
                $"Боец может уклониться от атаки противника с шансом {_evasionChance}"
            };

            return description.ToArray();
        }

        public override void TakeDamage(int damage)
        {
            if (IsEvaded() == false)
            {
                base.TakeDamage(damage);
            }
            else 
            {
                AbilityUsed?.Invoke(_type, "Уклоняется от атаки");
            } 
        }

        private bool IsEvaded()
        {
            return _evasionChance >= UserUtils.GetRandomSingle();
        }
    }
}
