using System;

namespace Task3
{
    public class TestGroups
    {
        public class RandomNumbersTest
        {
            public int[] TestArray { get; private set; }
            const int VALUE_RANGE = 1000;

            public RandomNumbersTest(int size)
            {
                TestArray = new int[size];
                Random random = new Random();
                for (int i = 0; i < size; ++i)
                    TestArray[i] = random.Next(0, VALUE_RANGE);
            }
        }

        public class SubarraysTest
        {
            public int[][] TestArrays { get; private set; }

            public SubarraysTest(int arraySize, int arraysCount, int maxSubarraySize)
            {
                TestArrays = new int[arraysCount][];
                Random random = new Random();
                
                for (int arrayIndex = 0; arrayIndex < arraysCount; ++arrayIndex)
                {
                    TestArrays[arrayIndex] = new int[arraySize];
                    
                    for (int position = 0; position < arraySize;)
                    {
                        int currentSubarraySize = random.Next(1, Math.Min(maxSubarraySize, arraySize - position) + 1);
                        int[] sortedSubarray = new int[currentSubarraySize];
                        
                        for (int j = 0; j < currentSubarraySize; ++j)
                            sortedSubarray[j] = random.Next(0, 1000);
                        
                        Array.Sort(sortedSubarray);
                        
                        for (int j = 0; j < currentSubarraySize && position < arraySize; ++j, ++position)
                            TestArrays[arrayIndex][position] = sortedSubarray[j];
                    }
                }
            }
        }

        public class PartiallySortedTest
        {
            public int[][] TestArrays { get; private set; }
            const int VALUE_RANGE = 1000;

            public PartiallySortedTest(int arraySize, int arraysCount, int permutationsCount)
            {
                TestArrays = new int[arraysCount][];
                Random random = new Random();
                
                for (int arrayIndex = 0; arrayIndex < arraysCount; ++arrayIndex)
                {
                    TestArrays[arrayIndex] = new int[arraySize];
                    
                    for (int i = 0; i < arraySize; ++i)
                        TestArrays[arrayIndex][i] = random.Next(0, VALUE_RANGE);
                    
                    Array.Sort(TestArrays[arrayIndex]);
                    
                    for (int i = 0; i < permutationsCount; ++i)
                    {
                        int index1 = random.Next(0, arraySize);
                        int index2 = random.Next(0, arraySize);
                        int temp = TestArrays[arrayIndex][index1];
                        TestArrays[arrayIndex][index1] = TestArrays[arrayIndex][index2];
                        TestArrays[arrayIndex][index2] = temp;
                    }
                }
            }
        }

        public class MixedArraysTest
        {
            public int[][] TestArrays { get; private set; }
            const int VALUE_RANGE = 1000;

            public MixedArraysTest(int arraySize, int arraysCount, int[] commands)
            {
                TestArrays = new int[arraysCount][];
                Random random = new Random();
                
                for (int arrayIndex = 0; arrayIndex < arraysCount; ++arrayIndex)
                {
                    TestArrays[arrayIndex] = new int[arraySize];
                    
                    if (commands.Length == 3 && commands[2] > 0)
                    {
                        int repeatedValue = random.Next(0, VALUE_RANGE);
                        int repeatCount = arraySize * commands[2] / 100;
                        
                        for (int i = 0; i < repeatCount; ++i)
                            TestArrays[arrayIndex][i] = repeatedValue;
                            
                        for (int i = repeatCount; i < arraySize; ++i)
                            TestArrays[arrayIndex][i] = random.Next(0, VALUE_RANGE);
                    }
                    else
                    {
                        for (int i = 0; i < arraySize; ++i)
                            TestArrays[arrayIndex][i] = random.Next(0, VALUE_RANGE);
                    }
                    
                    if (commands[0] == 2)
                    {
                        Array.Sort(TestArrays[arrayIndex]);
                    }
                    else if (commands[0] == 1)
                    {
                        Array.Sort(TestArrays[arrayIndex]);
                        Array.Reverse(TestArrays[arrayIndex]);
                    }
                    
                    if (commands[1] == 1)
                    {
                        int permutations = Math.Min(arraySize / 10, 100);
                        for (int i = 0; i < permutations; ++i)
                        {
                            int index1 = random.Next(0, arraySize);
                            int index2 = random.Next(0, arraySize);
                            int temp = TestArrays[arrayIndex][index1];
                            TestArrays[arrayIndex][index1] = TestArrays[arrayIndex][index2];
                            TestArrays[arrayIndex][index2] = temp;
                        }
                    }
                }
            }
        }
    }
}