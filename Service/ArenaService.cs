using GladiatorsFight.Model;

namespace GladiatorsFight.Service
{
    public class ArenaService
    {
        private Arena _arena;
        private FighterService _fighterService;
        private BattleLogger _battleLogger;

        private bool _isFightOver;

        public ArenaService(string leftFighter, string rightFighter, FighterService fighterService, BattleLogger logger) 
        {
            _fighterService = fighterService;
            _battleLogger = logger;

            _arena = new Arena(_fighterService.GetFighter(leftFighter), _fighterService.GetFighter(rightFighter));
            _arena.AttackPerformed += _battleLogger.SendMessage;

            _arena.FightOver += OnFightOver;
        }

        public event Action? GameOver;

        public void StartFight() 
        {
            _isFightOver = false;

            while (_isFightOver == false) 
            { 
                _arena.MakeTurn();
            }

            GameOver?.Invoke();
        }

        public void OnFightOver(FighterNumber fightResult) 
        {
            _battleLogger.SendMessage(SendGameOverMessage(fightResult));

            _isFightOver = true;
        }

        private string SendGameOverMessage(FighterNumber fightResult) 
        {
            string result = string.Empty;

            if (fightResult != FighterNumber.Nobody)
            {
                result = $"Боец {(int)fightResult} победил";
            }
            else
            {
                result = "Ничья";
            }

            return result;
        }
    }
}
