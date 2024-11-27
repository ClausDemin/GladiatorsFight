namespace GladiatorsFight.Model.Stats
{
    public class RangedStat
    {
        private int _value;
        private int _minValue;
        private int _maxValue;

        public RangedStat(int minValue, int maxValue, int startValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _value = startValue;
        }

        public event Action<int>? ValueChanged;

        public int Value => _value;
        public int MinValue => _minValue;
        public int MaxValue => _maxValue;

        public void ReduceValue(int value) 
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);

            if (_value - value < _minValue)
            {
                value = _value;
            }

            _value -= value;

            ValueChanged?.Invoke(-value);
        }

        public void IncreaseValue(int value) 
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);

            if (_value + value > _maxValue)
            {
                value = _maxValue - _value;
            }

            _value += value;

            ValueChanged?.Invoke(value);
        }
    }
}
