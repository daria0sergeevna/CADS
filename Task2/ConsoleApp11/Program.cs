using System;

namespace ComplexNumbers
{
    public struct ComplexNumber : IEquatable<ComplexNumber>
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(
                a.Real * b.Real - a.Imaginary * b.Imaginary,
                a.Real * b.Imaginary + a.Imaginary * b.Real);
        }

        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
        {
            double denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;
            return new ComplexNumber(
                (a.Real * b.Real + a.Imaginary * b.Imaginary) / denominator,
                (a.Imaginary * b.Real - a.Real * b.Imaginary) / denominator);
        }

       
        public static bool operator ==(ComplexNumber a, ComplexNumber b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(ComplexNumber a, ComplexNumber b)
        {
            return !a.Equals(b);
        }

       
        public override bool Equals(object obj)
        {
            return obj is ComplexNumber other && Real == other.Real && Imaginary == other.Imaginary;;
        }
        

        
        public override int GetHashCode()
        {
            return HashCode.Combine(Real, Imaginary);
        }

        public double GetMagnitude()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public double GetArgument()
        {
            if (Real == 0 && Imaginary == 0)
                return 0;
            
            return Math.Atan2(Imaginary, Real);
        }

        public override string ToString()
        {
            if (Imaginary == 0)
                return $"{Real}";
            if (Real == 0)
                return $"{Imaginary}i";
            
            string sign = Imaginary > 0 ? "+" : "";
            return $"{Real}{sign}{Imaginary}i";
        }
    }

    public class ComplexCalculator
    {
        private ComplexNumber _currentNumber;

        public void Run()
        {
            Console.WriteLine("Калькулятор комплексных чисел");
            
            while (true)
            {
                DisplayMenu();
                var choice = Console.ReadKey().KeyChar;
                Console.Clear();
                
                if (!ProcessChoice(choice))
                    break;
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("\nТекущее число: " + _currentNumber);
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1 - Ввести новое комплексное число");
            Console.WriteLine("2 - Сложить с другим числом");
            Console.WriteLine("3 - Вычесть другое число");
            Console.WriteLine("4 - Вычесть из другого числа");
            Console.WriteLine("5 - Умножить на другое число");
            Console.WriteLine("6 - Поделить на другое число");
            Console.WriteLine("7 - Поделить другое число");
            Console.WriteLine("8 - Найти модуль и аргумент");
            Console.WriteLine("9 - Показать вещественную и мнимую части");
            Console.WriteLine("0 - Показать алгебраическую форму");
            Console.WriteLine("E - Проверить равенство с другим числом");
            Console.WriteLine("Q - Выйти из программы");
            Console.Write("\nВаш выбор: ");
        }

        private bool ProcessChoice(char choice)
        {
            switch (char.ToLower(choice))
            {
                case '1':
                    InputNewNumber();
                    break;
                case '2':
                    AddNumber();
                    break;
                case '3':
                    SubtractNumber();
                    break;
                case '4':
                    SubtractFromNumber();
                    break;
                case '5':
                    MultiplyByNumber();
                    break;
                case '6':
                    DivideByNumber();
                    break;
                case '7':
                    DivideNumber();
                    break;
                case '8':
                    DisplayMagnitudeAndArgument();
                    break;
                case '9':
                    DisplayRealAndImaginaryParts();
                    break;
                case '0':
                    DisplayAlgebraicForm();
                    break;
                case 'e':
                    CheckEquality();
                    break;
                case 'q':
                    Console.WriteLine("До свидания!");
                    return false;
                default:
                    Console.WriteLine("Неизвестная команда");
                    break;
            }
            
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
            return true;
        }

        private ComplexNumber InputComplexNumber()
        {
            Console.Write("Введите вещественную часть: ");
            double real = double.Parse(Console.ReadLine() ?? "0");
            
            Console.Write("Введите мнимую часть: ");
            double imaginary = double.Parse(Console.ReadLine() ?? "0");
            
            return new ComplexNumber(real, imaginary);
        }

        private void InputNewNumber()
        {
            _currentNumber = InputComplexNumber();
            Console.WriteLine($"Установлено новое число: {_currentNumber}");
        }

        private void AddNumber()
        {
            var other = InputComplexNumber();
            _currentNumber += other;
            Console.WriteLine($"Результат: {_currentNumber}");
        }

        private void SubtractNumber()
        {
            var other = InputComplexNumber();
            _currentNumber -= other;
            Console.WriteLine($"Результат: {_currentNumber}");
        }

        private void SubtractFromNumber()
        {
            var other = InputComplexNumber();
            _currentNumber = other - _currentNumber;
            Console.WriteLine($"Результат: {_currentNumber}");
        }

        private void MultiplyByNumber()
        {
            var other = InputComplexNumber();
            _currentNumber *= other;
            Console.WriteLine($"Результат: {_currentNumber}");
        }

        private void DivideByNumber()
        {
            var other = InputComplexNumber();
            
            if (other.Real == 0 && other.Imaginary == 0)
            {
                Console.WriteLine("Ошибка: деление на ноль!");
                return;
            }
            
            _currentNumber /= other;
            Console.WriteLine($"Результат: {_currentNumber}");
        }

        private void DivideNumber()
        {
            var other = InputComplexNumber();
            
            if (_currentNumber.Real == 0 && _currentNumber.Imaginary == 0)
            {
                Console.WriteLine("Ошибка: деление на ноль!");
                return;
            }
            
            _currentNumber = other / _currentNumber;
            Console.WriteLine($"Результат: {_currentNumber}");
        }

        private void DisplayMagnitudeAndArgument()
        {
            Console.WriteLine($"Модуль: {_currentNumber.GetMagnitude():F4}");
            Console.WriteLine($"Аргумент: {_currentNumber.GetArgument():F4} радиан");
        }

        private void DisplayRealAndImaginaryParts()
        {
            Console.WriteLine($"Вещественная часть: {_currentNumber.Real}");
            Console.WriteLine($"Мнимая часть: {_currentNumber.Imaginary}");
        }

        private void DisplayAlgebraicForm()
        {
            Console.WriteLine($"Алгебраическая форма: {_currentNumber}");
        }

        private void CheckEquality()
        {
            Console.WriteLine("Введите число для сравнения:");
            var other = InputComplexNumber();
            
            bool areEqual = _currentNumber == other;
            
            Console.WriteLine($"Текущее число: {_currentNumber}");
            Console.WriteLine($"Введенное число: {other}");
            Console.WriteLine($"Числа {(areEqual ? "равны" : "не равны")}");
            
        
            Console.WriteLine($"Хэш-код текущего числа: {_currentNumber.GetHashCode()}");
            Console.WriteLine($"Хэш-код введенного числа: {other.GetHashCode()}");
            Console.WriteLine($"Хэш-коды {(areEqual ? "совпадают" : "разные")}");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var calculator = new ComplexCalculator();
            calculator.Run();
        }
    }
}