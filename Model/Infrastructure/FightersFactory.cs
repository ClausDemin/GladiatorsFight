using GladiatorsFight.Model.AbstractClasses;
using GladiatorsFight.Model.Enums;
using GladiatorsFight.Service;

namespace GladiatorsFight.Model.Infrastructure
{
    public class FightersFactory
    {
        private BattleLogger _battleLogger;
        private FighterBuilder _builder;

        private static Dictionary<FighterType, int> _fightersHealth = new Dictionary<FighterType, int>()
        {
            {FighterType.CriticalStrike, 100},
            {FighterType.DoubleAttack, 100 },
            {FighterType.BattleRage, 100 },
            {FighterType.FireBall, 100 },
            {FighterType.Evasion, 100},
        };

        private static Dictionary<FighterType, int> _fightersArmor = new Dictionary<FighterType, int>()
        {
            {FighterType.CriticalStrike, 10},
            {FighterType.DoubleAttack, 7},
            {FighterType.BattleRage, 20},
            {FighterType.FireBall, 3},
            {FighterType.Evasion, 5},
        };

        private static Dictionary<FighterType, int[]> _fightersDamage =
            new Dictionary<FighterType, int[]>()
            {
                {FighterType.CriticalStrike, new int[] {15, 3 } },
                {FighterType.DoubleAttack,  new int[] {12, 2 } },
                {FighterType.BattleRage,  new int[] {18, 4 } },
                {FighterType.FireBall,  new int[] {10, 1 } },
                {FighterType.Evasion,  new int[] {18, 2 } },
            };

        public FightersFactory(BattleLogger logger)
        {
            _battleLogger = logger;
            _builder = new FighterBuilder();
        }

        public AbstractFighter CreateFighter(FighterType type)
        {
            var fighter = _builder
                .Reset()
                .SetHealth(_fightersHealth[type])
                .SetArmor(_fightersArmor[type])
                .SetDamage(_fightersDamage[type][0], _fightersDamage[type][1])
                .AddAbility(type)
                .Build();

            return fighter;
        }

        public string[] GetDescription(FighterType type)
        {
            var fighter = CreateFighter(type);

            List<string> description = new List<string>();
            description.AddRange(fighter.GetFighterDescription());
            description.Add(string.Empty);
            description.AddRange((fighter as AbstractFighterDecorator).GetAbilityDescription());

            return description.ToArray();
        }
    }
}
