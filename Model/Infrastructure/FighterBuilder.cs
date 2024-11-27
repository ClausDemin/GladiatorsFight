using GladiatorsFight.Model.Abilities;
using GladiatorsFight.Model.AbstractClasses;
using GladiatorsFight.Model.Enums;
using GladiatorsFight.Model.Stats;

namespace GladiatorsFight.Model.Infrastructure
{
    public class FighterBuilder
    {
        private int _minHealth = 0;
        private int _defaultHealth = 100;

        private int _minArmor = -10;
        private int _maxArmor = 100;
        private int _defaultArmor = 3;

        private int _defaultDamage = 14;
        private int _defaultDamageSpread = 4;

        private RangedStat _health;
        private RangedStat _armor;
        private SpreadedStat _damage;
        private FighterType _ability;

        private Dictionary<FighterType, Func<AbstractFighter, AbstractFighter>> _fightersConstructors =
            new Dictionary<FighterType, Func<AbstractFighter, AbstractFighter>>
            {
                {FighterType.CriticalStrike, CreateCriticalStrikeFighter},
                {FighterType.DoubleAttack, CreateDoubleAttackFighter},
                {FighterType.BattleRage, CreateBattleRageFighter},
                {FighterType.FireBall, CreateFireBallFighter},
                {FighterType.Evasion, CreateEvasionFighter}
            };

        public FighterBuilder() 
        {
            _health = new RangedStat(_minHealth, _defaultHealth, _defaultHealth);
            _armor = new RangedStat(_minArmor, _maxArmor, _defaultArmor);
            _damage = new SpreadedStat(_defaultDamage, _defaultDamageSpread);
            _ability = FighterType.CriticalStrike;
        }

        public FighterBuilder Reset() 
        {
            return new FighterBuilder();
        }

        public FighterBuilder SetHealth(int value) 
        {
            _health = new RangedStat(_minHealth, value, value);

            return this;
        }

        public FighterBuilder SetArmor(int value) 
        {
            _armor = new RangedStat(_minArmor, _maxArmor, value);

            return this;
        }

        public FighterBuilder SetDamage(int value, int spread) 
        {
            _damage = new SpreadedStat(value, spread);

            return this;
        }

        public FighterBuilder AddAbility(FighterType type) 
        { 
            _ability = type;

            return this;
        }

        public AbstractFighter Build() 
        {
            var fighter = new Fighter(_health, _armor, _damage);

            if (_ability != FighterType.None)
            {
                return _fightersConstructors[_ability].Invoke(fighter);
            }

            else 
            { 
                return fighter;
            } 
        }

        private static AbstractFighter CreateCriticalStrikeFighter(AbstractFighter fighter)
        {
            float criticalStrikeChance = 0.25f;

            return new CriticalStrike(fighter, criticalStrikeChance);
        }

        private static AbstractFighter CreateDoubleAttackFighter(AbstractFighter fighter)
        {
            int doubleAttackCooldown = 3;

            return new DoubleAttack(fighter, doubleAttackCooldown);
        }

        private static AbstractFighter CreateBattleRageFighter(AbstractFighter fighter)
        {
            var rage = new RangedStat(0, 100, 0);
            var ragePerDamage = new SpreadedStat(2, 1);
            var enrageHealAmount = new SpreadedStat(15, 5);

            return new BattleRage(fighter, rage, enrageHealAmount, ragePerDamage);
        }

        private static AbstractFighter CreateFireBallFighter(AbstractFighter fighter)
        {
            var mana = new RangedStat(0, 20, 20);
            int fireBallManaCost = 5;
            var fireBallDamage = new SpreadedStat(25, 5);

            return new FireBall(fighter, mana, fireBallManaCost, fireBallDamage);
        }

        private static AbstractFighter CreateEvasionFighter(AbstractFighter fighter)
        {
            float evasionChance = 0.25f;

            return new Evasion(fighter, evasionChance);
        }
    }
}
