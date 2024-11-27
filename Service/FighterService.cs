using GladiatorsFight.Locale;
using GladiatorsFight.Model.AbstractClasses;
using GladiatorsFight.Model.Enums;
using GladiatorsFight.Model.Infrastructure;

namespace GladiatorsFight.Service
{
    public class FighterService
    {
        private FightersFactory _factory;
        private LocalizationService _localiztionService;

        public FighterService(BattleLogger logger) 
        { 
            _factory = new FightersFactory(logger);
            _localiztionService = new LocalizationService();
        }

        public string[] GetDescription(string fighterName) 
        {
            return _factory.GetDescription(GetFighterTypeByName(fighterName));
        }

        public AbstractFighter GetFighter(string fighterName)
        {
            return _factory.CreateFighter(GetFighterTypeByName(fighterName));
        }

        private FighterType GetFighterTypeByName(string fighterName)
        {
            return _localiztionService.GetTypeByName(fighterName);
        }

        public string GetFighterName(FighterType type) 
        {
            return _localiztionService.GetLocalName(type);
        }
    }
}
