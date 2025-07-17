using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfCalculator
{
    public partial class MainWindow : Window
    {
        private double _currentNumber = 0;
        private double _firstOperand = 0;
        private string _operation = "";
        private bool _isNewNumber = true;
        private double _memoryValue = 0;

        public MainWindow()
        {
            InitializeComponent();
            ResultDisplay.Text = "0";
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string digit = button.Content?.ToString() ?? string.Empty;

                if (_isNewNumber)
                {
                    ResultDisplay.Text = digit;
                    _isNewNumber = false;
                }
                else
                {
                    if (ResultDisplay.Text == "0" && digit != ".")
                    {
                        ResultDisplay.Text = digit;
                    }
                    else
                    {
                        ResultDisplay.Text += digit;
                    }
                }
            }
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (_isNewNumber)
            {
                ResultDisplay.Text = "0.";
                _isNewNumber = false;
            }
            else if (!ResultDisplay.Text.Contains("."))
            {
                ResultDisplay.Text += ".";
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string newOperation = button.Content?.ToString() ?? string.Empty;

                if (!_isNewNumber && _operation != "")
                {
                    CalculateResult();
                }

                _firstOperand = double.Parse(ResultDisplay.Text);
                _operation = newOperation;
                _isNewNumber = true;
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (_operation != "")
            {
                CalculateResult();
                _operation = "";
                _isNewNumber = true;
            }
        }

        private void CalculateResult()
        {
            _currentNumber = double.Parse(ResultDisplay.Text);

            try
            {
                switch (_operation)
                {
                    case "+":
                        _firstOperand += _currentNumber;
                        break;
                    case "-":
                        _firstOperand -= _currentNumber;
                        break;
                    case "×":
                        _firstOperand *= _currentNumber;
                        break;
                    case "÷":
                        if (_currentNumber == 0)
                        {
                            ResultDisplay.Text = "Error: Div por 0";
                            return;
                        }
                        _firstOperand /= _currentNumber;
                        break;
                    case "^": // Potencia
                        _firstOperand = Math.Pow(_firstOperand, _currentNumber);
                        break;
                }
                ResultDisplay.Text = _firstOperand.ToString();
            }
            catch (Exception ex)
            {
                ResultDisplay.Text = "Error";
                Console.WriteLine($"Error de cálculo: {ex.Message}");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _currentNumber = 0;
            _firstOperand = 0;
            _operation = "";
            _isNewNumber = true;
            ResultDisplay.Text = "0";
        }

        private void ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            ResultDisplay.Text = "0";
            _isNewNumber = true;
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (ResultDisplay.Text.Length > 1)
            {
                ResultDisplay.Text = ResultDisplay.Text.Remove(ResultDisplay.Text.Length - 1);
            }
            else
            {
                ResultDisplay.Text = "0";
                _isNewNumber = true;
            }
        }

        private void Negate_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultDisplay.Text, out double value))
            {
                ResultDisplay.Text = (-value).ToString();
            }
        }

        // Scientific Functions
        private void Sin_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultDisplay.Text, out double value))
            {
                ResultDisplay.Text = Math.Sin(value * Math.PI / 180).ToString(); // Convert degrees to radians
                _isNewNumber = true;
            }
        }

        private void Cos_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultDisplay.Text, out double value))
            {
                ResultDisplay.Text = Math.Cos(value * Math.PI / 180).ToString(); // Convert degrees to radians
                _isNewNumber = true;
            }
        }

        private void Tan_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultDisplay.Text, out double value))
            {
                double angleInRadians = value * Math.PI / 180;
                // Check for angles where tan is undefined (e.g., 90, 270 degrees)
                if (Math.Abs(Math.Cos(angleInRadians)) < 1e-9) // Small epsilon to account for floating point inaccuracies
                {
                    ResultDisplay.Text = "Error";
                }
                else
                {
                    ResultDisplay.Text = Math.Tan(angleInRadians).ToString();
                }
                _isNewNumber = true;
            }
        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultDisplay.Text, out double value))
            {
                if (value < 0)
                {
                    ResultDisplay.Text = "Error";
                }
                else
                {
                    ResultDisplay.Text = Math.Sqrt(value).ToString();
                }
                _isNewNumber = true;
            }
        }

        private void Square_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultDisplay.Text, out double value))
            {
                ResultDisplay.Text = (value * value).ToString();
                _isNewNumber = true;
            }
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultDisplay.Text, out double value))
            {
                if (value <= 0)
                {
                    ResultDisplay.Text = "Error";
                }
                else
                {
                    ResultDisplay.Text = Math.Log10(value).ToString();
                }
                _isNewNumber = true;
            }
        }

        // Memory Functions
        private void MemoryClear_Click(object sender, RoutedEventArgs e)
        {
            _memoryValue = 0;
        }

        private void MemoryRecall_Click(object sender, RoutedEventArgs e)
        {
            ResultDisplay.Text = _memoryValue.ToString();
            _isNewNumber = true;
        }

        private void MemoryAdd_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultDisplay.Text, out double value))
            {
                _memoryValue += value;
                _isNewNumber = true;
            }
        }

        private void MemorySubtract_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultDisplay.Text, out double value))
            {
                _memoryValue -= value;
                _isNewNumber = true;
            }
        }
    }
}
