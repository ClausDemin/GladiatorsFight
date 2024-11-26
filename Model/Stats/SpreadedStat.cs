using GladiatorsFight.Model.Infrastructure;

namespace GladiatorsFight.Model.Stats
{
    public class SpreadedStat
    {
        private int _minValue;
        private int _maxValue;

        public SpreadedStat(int value, int valueSpread)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(valueSpread, value);

            _minValue = value - valueSpread;
            _maxValue = value + valueSpread;
        }

        public int Value => GetValue();    
        public int MinValue => _minValue;
        public int MaxValue => _maxValue;

        private int GetValue() 
        {
            return UserUtils.GetRandomNumber(_minValue, _maxValue);
        }

        public override string ToString()
        {
            string output = $"{_minValue} - {_maxValue}";

            return output;
        }
    }
}
