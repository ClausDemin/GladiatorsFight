using GladiatorsFight.Model.AbstractClasses;
using GladiatorsFight.Model.Enums;
using GladiatorsFight.Model.Interfaces;
using GladiatorsFight.Model.Stats;

namespace GladiatorsFight.Model.Abilities
{
    public class FireBall : AbstractFighterDecorator
    {
        private RangedStat _mana;
        private int _manaCost;

        private SpreadedStat _damage;

        public FireBall(AbstractFighter fighter, RangedStat mana, int fireBallManaCost , SpreadedStat fireBallDamage)
            : base(fighter, FighterType.FireBall)
        {
            _mana = mana;
            _manaCost = fireBallManaCost;

            _damage = fireBallDamage;
        }

        public override event Action<FighterType, string>? AbilityUsed;
        public override event Action<IAttacker, IDamageable>? AttackPerformed;

        public override void Attack(IDamageable target)
        {
            if (_mana.Value >= _manaCost)
            {
                AttackPerformed?.Invoke(this, target);

                target.TakeDamage(_damage.Value);

                _mana.ReduceValue(_manaCost);
                
                AbilityUsed?.Invoke(_type, $"Использует заклинание огненный шар. У бойца осталось {_mana.Value} маны.");
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
                $"Запас магической энергии: {_mana.MaxValue}",
                $"Стоимость заклинания: {_manaCost}",
                $"Урон заклинания: {_damage}",
                "",
                $"Боец владеет искусством магии. Он будет атаковать противника заклинанием Огненный шар,",
                $"пока у него не закончится магическая энергия.",
            };

            return description.ToArray();
        }
    }
}
