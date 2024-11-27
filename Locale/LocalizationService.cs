using GladiatorsFight.Model.Enums;

namespace GladiatorsFight.Locale
{
    public class LocalizationService
    {
        private Dictionary<FighterType, string> _fightersTypeLocale =
           new Dictionary<FighterType, string>()
           {
                {FighterType.CriticalStrike, "Критический удар" },
                {FighterType.DoubleAttack, "Двойная атака" },
                {FighterType.BattleRage, "Боевая ярость" },
                {FighterType.FireBall, "Огненный шар" },
                {FighterType.Evasion, "Уклонение" }
           };

        private Dictionary<string, FighterType> _localNameFighterTypeLink;

        public LocalizationService() 
        {
            LinkLocalNamesWithTypes();
        }

        public string GetLocalName(FighterType type)
        {
                return _fightersTypeLocale[type];
        }

        public FighterType GetTypeByName(string localeName) 
        {
            if (_localNameFighterTypeLink.TryGetValue(localeName.Trim(), out var result)) 
            { 
                return result;
            }

            throw new KeyNotFoundException(localeName);
        }

        private void LinkLocalNamesWithTypes() 
        {
            _localNameFighterTypeLink = new Dictionary<string, FighterType>();

            foreach (var typeNamePair in _fightersTypeLocale) 
            {
                _localNameFighterTypeLink.Add(typeNamePair.Value, typeNamePair.Key);
            }
        }
    }
}
