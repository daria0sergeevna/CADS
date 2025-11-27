using System;
using System.Collections.Generic;
using System.Linq;

namespace HeapConsoleApp
{
    public class Heap<T> where T : IComparable<T>
    {
        private readonly List<T> _elements;
        private readonly bool _isMinHeap;

        public Heap(bool isMinHeap = true)
        {
            _elements = new List<T>();
            _isMinHeap = isMinHeap;
        }

        public Heap(T[] elements, bool isMinHeap = true)
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            _elements = new List<T>(elements);
            _isMinHeap = isMinHeap;
            BuildHeap();
        }

        private Heap(Heap<T> first, Heap<T> second)
        {
            if (first == null || second == null)
                throw new ArgumentNullException("Обе кучи должны быть не null");

            _isMinHeap = first._isMinHeap;
            
            if (_isMinHeap != second._isMinHeap)
                throw new InvalidOperationException("Нельзя объединять кучи разного типа");

            _elements = new List<T>(first._elements);
            _elements.AddRange(second._elements);
            BuildHeap();
        }

        public int Count => _elements.Count;
        public bool IsEmpty => _elements.Count == 0;

        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Куча пуста");
            return _elements[0];
        }

        public T ExtractRoot()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Куча пуста");

            T root = _elements[0];
            _elements[0] = _elements[^1];
            _elements.RemoveAt(_elements.Count - 1);
            
            if (!IsEmpty)
                HeapifyDown(0);

            return root;
        }

        public void Add(T element)
        {
            _elements.Add(element);
            HeapifyUp(_elements.Count - 1);
        }

        public void ChangeKey(int index, T newValue)
        {
            if (index < 0 || index >= _elements.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона");

            T oldValue = _elements[index];
            _elements[index] = newValue;

            int comparison = newValue.CompareTo(oldValue);
            
            if (_isMinHeap)
            {
                if (comparison < 0)
                    HeapifyUp(index);
                else if (comparison > 0)
                    HeapifyDown(index);
            }
            else
            {
                if (comparison > 0)
                    HeapifyUp(index);
                else if (comparison < 0)
                    HeapifyDown(index);
            }
        }

        public Heap<T> Merge(Heap<T> other)
        {
            return new Heap<T>(this, other);
        }

        public bool IsValid()
        {
            for (int i = 0; i < _elements.Count; i++)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;

                if (left < _elements.Count && !IsCorrectOrder(i, left))
                    return false;

                if (right < _elements.Count && !IsCorrectOrder(i, right))
                    return false;
            }
            return true;
        }

        public T[] ToArray()
        {
            return _elements.ToArray();
        }

        public void Print()
        {
            if (IsEmpty)
            {
                Console.WriteLine("Куча пуста");
                return;
            }

            Console.WriteLine("Элементы кучи:");
            for (int i = 0; i < _elements.Count; i++)
            {
                Console.WriteLine($"[{i}] = {_elements[i]}");
            }
        }

        private void BuildHeap()
        {
            for (int i = _elements.Count / 2 - 1; i >= 0; i--)
            {
                HeapifyDown(i);
            }
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                
                if (IsCorrectOrder(parent, index))
                    break;

                Swap(index, parent);
                index = parent;
            }
        }

        private void HeapifyDown(int index)
        {
            int size = _elements.Count;
            
            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int extreme = index;

                if (left < size && !IsCorrectOrder(extreme, left))
                    extreme = left;

                if (right < size && !IsCorrectOrder(extreme, right))
                    extreme = right;

                if (extreme == index)
                    break;

                Swap(index, extreme);
                index = extreme;
            }
        }

        private bool IsCorrectOrder(int parent, int child)
        {
            int comparison = _elements[parent].CompareTo(_elements[child]);
            return _isMinHeap ? comparison <= 0 : comparison >= 0;
        }

        private void Swap(int i, int j)
        {
            T temp = _elements[i];
            _elements[i] = _elements[j];
            _elements[j] = temp;
        }
    }

    public class HeapConsole
    {
        private Heap<int> _heap;
        private bool _isMinHeap = true;

        public void Run()
        {
            Console.WriteLine("=== Консольное приложение для работы с кучей ===");
            InitializeHeap();
            ShowMenu();
        }

        private void InitializeHeap()
        {
            Console.WriteLine("\nВыберите тип кучи:");
            Console.WriteLine("1 - Min-Куча (по умолчанию)");
            Console.WriteLine("2 - Max-Куча");
            Console.Write("Ваш выбор: ");
            
            var choice = Console.ReadLine();
            _isMinHeap = choice != "2";
            
            Console.WriteLine("\nИнициализация кучи:");
            Console.WriteLine("1 - Создать пустую кучу");
            Console.WriteLine("2 - Создать кучу из массива");
            Console.Write("Ваш выбор: ");
            
            var initChoice = Console.ReadLine();
            
            if (initChoice == "2")
            {
                CreateHeapFromArray();
            }
            else
            {
                _heap = new Heap<int>(_isMinHeap);
                Console.WriteLine("Пустая куча успешно создана!");
            }
        }

        private void CreateHeapFromArray()
        {
            try
            {
                Console.Write("Введите числа через пробел: ");
                var input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input))
                {
                    _heap = new Heap<int>(_isMinHeap);
                    Console.WriteLine("Пустой ввод. Создана пустая куча.");
                    return;
                }

                var numbers = input.Split(' ')
                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                 .Select(int.Parse)
                                 .ToArray();

                _heap = new Heap<int>(numbers, _isMinHeap);
                Console.WriteLine($"Куча успешно создана из {numbers.Length} элементов!");
                Console.WriteLine($"Куча корректна: {_heap.IsValid()}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Пожалуйста, введите целые числа!");
                CreateHeapFromArray();
            }
        }

        private void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== Операции с кучей ===");
                Console.WriteLine($"Текущий тип кучи: {(_isMinHeap ? "Min-Куча" : "Max-Куча")}");
                Console.WriteLine($"Количество элементов: {_heap.Count}");
                if (!_heap.IsEmpty)
                    Console.WriteLine($"Корневой элемент: {_heap.Peek()}");
                
                Console.WriteLine("\nВыберите операцию:");
                Console.WriteLine("1 - Просмотр корня (без удаления)");
                Console.WriteLine("2 - Извлечение корня");
                Console.WriteLine("3 - Добавление элемента");
                Console.WriteLine("4 - Изменение ключа");
                Console.WriteLine("5 - Вывод кучи");
                Console.WriteLine("6 - Проверка корректности кучи");
                Console.WriteLine("7 - Объединение с другой кучей");
                Console.WriteLine("8 - Создать новую кучу");
                Console.WriteLine("0 - Выход");
                Console.Write("Ваш выбор: ");

                var choice = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            PeekOperation();
                            break;
                        case "2":
                            ExtractRootOperation();
                            break;
                        case "3":
                            AddOperation();
                            break;
                        case "4":
                            ChangeKeyOperation();
                            break;
                        case "5":
                            PrintOperation();
                            break;
                        case "6":
                            CheckValidityOperation();
                            break;
                        case "7":
                            MergeOperation();
                            break;
                        case "8":
                            InitializeHeap();
                            break;
                        case "0":
                            Console.WriteLine("До свидания!");
                            return;
                        default:
                            Console.WriteLine("Неверный выбор! Пожалуйста, попробуйте снова.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        private void PeekOperation()
        {
            if (_heap.IsEmpty)
            {
                Console.WriteLine("Куча пуста!");
                return;
            }

            var root = _heap.Peek();
            Console.WriteLine($"Корневой элемент: {root}");
        }

        private void ExtractRootOperation()
        {
            if (_heap.IsEmpty)
            {
                Console.WriteLine("Куча пуста!");
                return;
            }

            var root = _heap.ExtractRoot();
            Console.WriteLine($"Извлечен корень: {root}");
            Console.WriteLine($"Новый размер кучи: {_heap.Count}");
            
            if (!_heap.IsEmpty)
                Console.WriteLine($"Новый корень: {_heap.Peek()}");
        }

        private void AddOperation()
        {
            Console.Write("Введите число для добавления: ");
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                _heap.Add(number);
                Console.WriteLine($"Число {number} успешно добавлено!");
                Console.WriteLine($"Новый размер кучи: {_heap.Count}");
            }
            else
            {
                Console.WriteLine("Неверное число!");
            }
        }

        private void ChangeKeyOperation()
        {
            if (_heap.IsEmpty)
            {
                Console.WriteLine("Куча пуста!");
                return;
            }

            _heap.Print();
            Console.Write("Введите индекс для изменения: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 0 || index >= _heap.Count)
            {
                Console.WriteLine("Неверный индекс!");
                return;
            }

            Console.Write("Введите новое значение: ");
            if (!int.TryParse(Console.ReadLine(), out int newValue))
            {
                Console.WriteLine("Неверное число!");
                return;
            }

            _heap.ChangeKey(index, newValue);
            Console.WriteLine($"Изменен элемент по индексу {index} на значение {newValue}");
            Console.WriteLine($"Куча корректна: {_heap.IsValid()}");
        }

        private void PrintOperation()
        {
            _heap.Print();
        }

        private void CheckValidityOperation()
        {
            Console.WriteLine($"Куча корректна: {_heap.IsValid()}");
        }

        private void MergeOperation()
        {
            Console.WriteLine("Создайте вторую кучу для объединения:");
            Console.Write("Введите числа через пробел: ");
            
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Пустой ввод. Объединение отменено.");
                return;
            }

            try
            {
                var numbers = input.Split(' ')
                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                 .Select(int.Parse)
                                 .ToArray();

                var secondHeap = new Heap<int>(numbers, _isMinHeap);
                var mergedHeap = _heap.Merge(secondHeap);
                
                Console.WriteLine($"Объединение успешно! Новый размер кучи: {mergedHeap.Count}");
                Console.WriteLine($"Объединенная куча корректна: {mergedHeap.IsValid()}");
                
                _heap = mergedHeap;
                Console.WriteLine("Текущая куча заменена на объединенную.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Пожалуйста, введите целые числа!");
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            var heapConsole = new HeapConsole();
            heapConsole.Run();
        }
    }
}