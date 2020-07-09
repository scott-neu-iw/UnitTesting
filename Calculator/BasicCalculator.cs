using System.Collections.Generic;

namespace Calculator
{
    public class BasicCalculator
    {
        public decimal CurrentResult { get; private set; }
        private List<string> _history;
        public IReadOnlyCollection<string> History => _history;

        public BasicCalculator()
        {
            _history = new List<string>();
            CurrentResult = 0;
        }

        public void Add(decimal value)
        {
            AddHistory("+", value);
            CurrentResult = CurrentResult + value;
        }

        public void Subtract(decimal value)
        {
            AddHistory("-", value);
            CurrentResult = CurrentResult + value;
        }

        public void Multiply(decimal value)
        {
            CurrentResult = CurrentResult * value;
        }

        public void Divide(decimal value)
        {
            AddHistory("*", value);
            CurrentResult = CurrentResult / value;
        }

        public void Clear()
        {
            _history.Clear();
            CurrentResult = 0;
        }

        private void AddHistory(string calcOperator, decimal value)
        {
            _history.Add($"{CurrentResult} {calcOperator} {value}");
        }
    }
}
