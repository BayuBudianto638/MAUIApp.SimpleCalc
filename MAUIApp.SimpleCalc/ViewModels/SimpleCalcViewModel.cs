using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using NCalc;

namespace MAUIApp.SimpleCalc.ViewModels
{
    [INotifyPropertyChanged]
    public partial class SimpleCalcViewModel 
    {
        [ObservableProperty]
        private string inputText = "";

        [ObservableProperty]
        private string calculatedResult = "0";

        private bool isSciOpWaiting = false;

        public SimpleCalcViewModel()
        {
        }

        [RelayCommand]
        private void Reset()
        {
            CalculatedResult = "0";
            InputText = "";
            isSciOpWaiting = false;
        }

        [RelayCommand]
        private void Calculate()
        {
            if (InputText.Length == 0)
                return;

            if (isSciOpWaiting)
            {
                InputText += ")";
                isSciOpWaiting = false;
            }

            try
            {
                var inputString = NormalizeInputString();
                var expression = new Expression(inputString);
                var result = expression.Evaluate();

                CalculatedResult = result.ToString();
            }
            catch (Exception ex)
            {
                CalculatedResult = "NaN";
            }
        }

        private string NormalizeInputString()
        {
            Dictionary<string, string> _operation = new()
            {
                {"×", "*"},
                {"÷", "/"}
            };

            var retString = InputText;

            foreach (var key in _operation.Keys)
            {
                retString = retString.Replace(key, _operation[key]);
            }

            return retString;
        }

        [RelayCommand]
        private void Backspace()
        {
            if (InputText.Length > 0)
                InputText = InputText.Substring(0, InputText.Length - 1);
        }

        [RelayCommand]
        private void NumberInput(string key)
        {
            InputText += key;
        }

        [RelayCommand]
        private void MathOperator(string op)
        {
            if (isSciOpWaiting)
            {
                InputText += ")";
                isSciOpWaiting = false;
            }

            InputText += $" {op} ";
        }

        [RelayCommand]
        private void RegionOperator(string op)
        {
            if (op == ")")
                isSciOpWaiting = false;

            InputText += op;
        }

        [RelayCommand]
        private void ScientificOperator(string op)
        {
            InputText += $"{op}(";
            isSciOpWaiting = true;
        }
    }
}
