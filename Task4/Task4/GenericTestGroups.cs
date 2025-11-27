using System;
using System.Collections.Generic;

namespace Task4
{
    public class GenericTestGroups<T>
    {
        public class RandomNumbersTest
        {
            public T[] TestArray { get; private set; }
            private Func<T> valueGenerator;

            public RandomNumbersTest(int size, Func<T> generator)
            {
                TestArray = new T[size];
                valueGenerator = generator;
                for (int i = 0; i < size; ++i)
                    TestArray[i] = generator();
            }
        }

        public class SubarraysTest
        {
            public T[][] TestArrays { get; private set; }

            public SubarraysTest(int arraySize, int arraysCount, int maxSubarraySize, 
                               Func<T> valueGenerator, IComparer<T> comparer)
            {
                TestArrays = new T[arraysCount][];
                Random random = new Random();
                
                for (int arrayIndex = 0; arrayIndex < arraysCount; ++arrayIndex)
                {
                    TestArrays[arrayIndex] = new T[arraySize];
                    
                    for (int position = 0; position < arraySize;)
                    {
                        int currentSubarraySize = random.Next(1, Math.Min(maxSubarraySize, arraySize - position) + 1);
                        T[] sortedSubarray = new T[currentSubarraySize];
                        
                        for (int j = 0; j < currentSubarraySize; ++j)
                            sortedSubarray[j] = valueGenerator();
                        
                        Array.Sort(sortedSubarray, comparer);
                        
                        for (int j = 0; j < currentSubarraySize && position < arraySize; ++j, ++position)
                            TestArrays[arrayIndex][position] = sortedSubarray[j];
                    }
                }
            }
        }

        public class PartiallySortedTest
        {
            public T[][] TestArrays { get; private set; }

            public PartiallySortedTest(int arraySize, int arraysCount, int permutationsCount, 
                                     Func<T> valueGenerator, IComparer<T> comparer)
            {
                TestArrays = new T[arraysCount][];
                Random random = new Random();
                
                for (int arrayIndex = 0; arrayIndex < arraysCount; ++arrayIndex)
                {
                    TestArrays[arrayIndex] = new T[arraySize];
                    
                    for (int i = 0; i < arraySize; ++i)
                        TestArrays[arrayIndex][i] = valueGenerator();
                    
                    Array.Sort(TestArrays[arrayIndex], comparer);
                    
                    for (int i = 0; i < permutationsCount; ++i)
                    {
                        int index1 = random.Next(0, arraySize);
                        int index2 = random.Next(0, arraySize);
                        T temp = TestArrays[arrayIndex][index1];
                        TestArrays[arrayIndex][index1] = TestArrays[arrayIndex][index2];
                        TestArrays[arrayIndex][index2] = temp;
                    }
                }
            }
        }

        public class MixedArraysTest
        {
            public T[][] TestArrays { get; private set; }

            public MixedArraysTest(int arraySize, int arraysCount, int[] commands, 
                                 Func<T> valueGenerator, IComparer<T> comparer)
            {
                TestArrays = new T[arraysCount][];
                Random random = new Random();
                
                for (int arrayIndex = 0; arrayIndex < arraysCount; ++arrayIndex)
                {
                    TestArrays[arrayIndex] = new T[arraySize];
                    
                    // Заполнение массива случайными значениями
                    for (int i = 0; i < arraySize; ++i)
                        TestArrays[arrayIndex][i] = valueGenerator();
                    
                    // Сортировка или обратный порядок
                    if (commands[0] == 2)
                    {
                        Array.Sort(TestArrays[arrayIndex], comparer);
                    }
                    else if (commands[0] == 1)
                    {
                        Array.Sort(TestArrays[arrayIndex], comparer);
                        Array.Reverse(TestArrays[arrayIndex]);
                    }
                    
                    // Перестановки
                    if (commands[1] == 1)
                    {
                        int permutations = Math.Min(arraySize / 10, 100);
                        for (int i = 0; i < permutations; ++i)
                        {
                            int index1 = random.Next(0, arraySize);
                            int index2 = random.Next(0, arraySize);
                            T temp = TestArrays[arrayIndex][index1];
                            TestArrays[arrayIndex][index1] = TestArrays[arrayIndex][index2];
                            TestArrays[arrayIndex][index2] = temp;
                        }
                    }
                }
            }
        }
    }

    // Компараторы для различных типов данных
    public class IntComparer : IComparer<int>
    {
        public int Compare(int x, int y) => x.CompareTo(y);
    }

    public class DoubleComparer : IComparer<double>
    {
        public int Compare(double x, double y) => x.CompareTo(y);
    }

    public class StringComparer : IComparer<string>
    {
        public int Compare(string x, string y) => string.Compare(x, y);
    }

    public class CustomClassComparer : IComparer<CustomClass>
    {
        public int Compare(CustomClass x, CustomClass y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Value.CompareTo(y.Value);
        }
    }

    // Пример пользовательского класса для тестирования
    public class CustomClass
    {
        public int Value { get; set; }
        public string Name { get; set; }
    
        public CustomClass(int value, string name)
        {
            Value = value;
            Name = name;
        }
    }
}