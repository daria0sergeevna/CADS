using System;
using System.Collections.Generic;
using System.Linq;

namespace Task4
{
    public static class GenericSortings<T>
    {
        // ==================== ПЕРВАЯ ГРУППА - ПРОСТЫЕ СОРТИРОВКИ O(n^2) ====================

        /// <summary>
        /// Сортировка пузырьком - O(n^2)
        /// </summary>
        public static void BubbleSort(T[] arr, IComparer<T> comparer)
        {
            T temp;
            for (int i = 0; i < arr.Length - 1; ++i)
                for (int j = 0; j < arr.Length - i - 1; ++j)
                    if (comparer.Compare(arr[j], arr[j + 1]) > 0)
                    {
                        temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
        }

        /// <summary>
        /// Шейкерная сортировка - O(n^2)
        /// </summary>
        public static void ShakerSort(T[] arr, IComparer<T> comparer)
        {
            T temp;
            int left = 0, right = arr.Length - 1;
            while (left < right)
            {
                for (int i = left; i < right; ++i)
                    if (comparer.Compare(arr[i], arr[i + 1]) > 0)
                    {
                        temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                    }
                --right;
                for (int i = right; i > left; --i)
                    if (comparer.Compare(arr[i], arr[i - 1]) < 0)
                    {
                        temp = arr[i];
                        arr[i] = arr[i - 1];
                        arr[i - 1] = temp;
                    }
                ++left;
            }
        }

        /// <summary>
        /// Сортировка расческой - O(n^2)
        /// </summary>
        public static void CombSort(T[] arr, IComparer<T> comparer)
        {
            T temp;
            int gap = arr.Length;
            bool swapped = true;
            while (gap > 1 || swapped)
            {
                gap = Math.Max(1, (int)(gap / 1.3));
                swapped = false;
                for (int i = 0; i + gap < arr.Length; ++i)
                    if (comparer.Compare(arr[i], arr[i + gap]) > 0)
                    {
                        temp = arr[i];
                        arr[i] = arr[i + gap];
                        arr[i + gap] = temp;
                        swapped = true;
                    }
            }
        }

        /// <summary>
        /// Сортировка вставками - O(n^2)
        /// </summary>
        public static void InsertionSort(T[] arr, IComparer<T> comparer)
        {
            int j;
            T key;
            for (int i = 1; i < arr.Length; ++i)
            {
                key = arr[i];
                j = i - 1;
                while (j >= 0 && comparer.Compare(arr[j], key) > 0)
                {
                    arr[j + 1] = arr[j];
                    --j;
                }
                arr[j + 1] = key;
            }
        }

        /// <summary>
        /// Сортировка выбором - O(n^2)
        /// </summary>
        public static void SelectionSort(T[] arr, IComparer<T> comparer)
        {
            T temp;
            int minIndex;
            for (int i = 0; i < arr.Length - 1; ++i)
            {
                minIndex = i;
                for (int j = i + 1; j < arr.Length; ++j)
                    if (comparer.Compare(arr[j], arr[minIndex]) < 0)
                        minIndex = j;
                temp = arr[i];
                arr[i] = arr[minIndex];
                arr[minIndex] = temp;
            }
        }

        /// <summary>
        /// Гномья сортировка - O(n^2)
        /// </summary>
        public static void GnomeSort(T[] arr, IComparer<T> comparer)
        {
            T temp;
            int index = 1;
            while (index < arr.Length)
            {
                if (index == 0 || comparer.Compare(arr[index - 1], arr[index]) <= 0)
                {
                    ++index;
                }
                else
                {
                    temp = arr[index];
                    arr[index] = arr[index - 1];
                    arr[index - 1] = temp;
                    --index;
                }
            }
        }

        // ==================== ВТОРАЯ ГРУППА - УЛУЧШЕННЫЕ СОРТИРОВКИ ====================

        /// <summary>
        /// Сортировка Шелла - O(n^2)
        /// </summary>
        public static void Shellsort(T[] arr, IComparer<T> comparer)
        {
            for (int gap = arr.Length / 2; gap > 0; gap /= 2)
                for (int i = gap; i < arr.Length; ++i)
                {
                    T temp = arr[i];
                    int j;
                    for (j = i; j >= gap && comparer.Compare(arr[j - gap], temp) > 0; j -= gap)
                        arr[j] = arr[j - gap];
                    arr[j] = temp;
                }
        }

        /// <summary>
        /// Вспомогательный класс для сортировки деревом
        /// </summary>
        private class TreeNode
        {
            public T Value { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
            private IComparer<T> comparer;

            public TreeNode(T value, IComparer<T> comp)
            {
                Value = value;
                Left = null;
                Right = null;
                comparer = comp;
            }

            public void Insert(T value)
            {
                if (comparer.Compare(value, Value) < 0)
                {
                    if (Left == null)
                        Left = new TreeNode(value, comparer);
                    else
                        Left.Insert(value);
                }
                else
                {
                    if (Right == null)
                        Right = new TreeNode(value, comparer);
                    else
                        Right.Insert(value);
                }
            }

            public void Traverse(T[] result, ref int index)
            {
                Left?.Traverse(result, ref index);
                result[index++] = Value;
                Right?.Traverse(result, ref index);
            }
        }

        /// <summary>
        /// Сортировка бинарным деревом - O(n log n) в среднем, O(n^2) в худшем
        /// </summary>
        public static void TreeSort(T[] arr, IComparer<T> comparer)
        {
            if (arr.Length == 0) return;

            TreeNode root = new TreeNode(arr[0], comparer);
            for (int i = 1; i < arr.Length; ++i)
                root.Insert(arr[i]);

            int index = 0;
            root.Traverse(arr, ref index);
        }

        /// <summary>
        /// Битонная сортировка - O(n log^2 n)
        /// </summary>
        public static void BitonicSort(T[] arr, IComparer<T> comparer)
        {
            BitonicSort(arr, 0, arr.Length, true, comparer);
        }

        private static void BitonicSort(T[] arr, int low, int count, bool ascending, IComparer<T> comparer)
        {
            if (count <= 1) return;

            int k = count / 2;
            BitonicSort(arr, low, k, true, comparer);
            BitonicSort(arr, low + k, k, false, comparer);
            BitonicMerge(arr, low, count, ascending, comparer);
        }

        private static void BitonicMerge(T[] arr, int low, int count, bool ascending, IComparer<T> comparer)
        {
            if (count <= 1) return;

            int k = count / 2;
            for (int i = low; i < low + k; ++i)
            {
                bool condition = comparer.Compare(arr[i], arr[i + k]) > 0;
                if (condition == ascending)
                {
                    T temp = arr[i];
                    arr[i] = arr[i + k];
                    arr[i + k] = temp;
                }
            }
            BitonicMerge(arr, low, k, ascending, comparer);
            BitonicMerge(arr, low + k, k, ascending, comparer);
        }

        // ==================== ТРЕТЬЯ ГРУППА - ЭФФЕКТИВНЫЕ СОРТИРОВКИ O(n log n) ====================

        /// <summary>
        /// Пирамидальная сортировка - O(n log n)
        /// </summary>
        public static void Heapsort(T[] arr, IComparer<T> comparer)
        {
            // Построение max-кучи
            for (int i = arr.Length / 2 - 1; i >= 0; --i)
                Heapify(arr, arr.Length, i, comparer);

            // Извлечение элементов из кучи
            for (int i = arr.Length - 1; i >= 0; --i)
            {
                T temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;

                Heapify(arr, i, 0, comparer);
            }
        }

        private static void Heapify(T[] arr, int heapSize, int rootIndex, IComparer<T> comparer)
        {
            int largest = rootIndex;
            int leftChild = 2 * rootIndex + 1;
            int rightChild = 2 * rootIndex + 2;

            if (leftChild < heapSize && comparer.Compare(arr[leftChild], arr[largest]) > 0)
                largest = leftChild;

            if (rightChild < heapSize && comparer.Compare(arr[rightChild], arr[largest]) > 0)
                largest = rightChild;

            if (largest != rootIndex)
            {
                T temp = arr[rootIndex];
                arr[rootIndex] = arr[largest];
                arr[largest] = temp;

                Heapify(arr, heapSize, largest, comparer);
            }
        }

        /// <summary>
        /// Быстрая сортировка - O(n log n) в среднем, O(n^2) в худшем
        /// </summary>
        public static void QuickSort(T[] arr, IComparer<T> comparer)
        {
            QuickSort(arr, 0, arr.Length - 1, comparer);
        }

        private static void QuickSort(T[] arr, int low, int high, IComparer<T> comparer)
        {
            if (low < high)
            {
                int pivotIndex = Partition(arr, low, high, comparer);
                QuickSort(arr, low, pivotIndex - 1, comparer);
                QuickSort(arr, pivotIndex + 1, high, comparer);
            }
        }

        private static int Partition(T[] arr, int low, int high, IComparer<T> comparer)
        {
            T pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; ++j)
            {
                if (comparer.Compare(arr[j], pivot) <= 0)
                {
                    ++i;
                    T temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            T temp2 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp2;

            return i + 1;
        }

        /// <summary>
        /// Сортировка слиянием - O(n log n)
        /// </summary>
        public static void MergeSort(T[] arr, IComparer<T> comparer)
        {
            if (arr.Length <= 1) return;

            int mid = arr.Length / 2;
            T[] left = new T[mid];
            T[] right = new T[arr.Length - mid];

            Array.Copy(arr, 0, left, 0, mid);
            Array.Copy(arr, mid, right, 0, arr.Length - mid);

            MergeSort(left, comparer);
            MergeSort(right, comparer);
            Merge(arr, left, right, comparer);
        }

        private static void Merge(T[] arr, T[] left, T[] right, IComparer<T> comparer)
        {
            int i = 0, j = 0, k = 0;

            while (i < left.Length && j < right.Length)
            {
                if (comparer.Compare(left[i], right[j]) <= 0)
                    arr[k++] = left[i++];
                else
                    arr[k++] = right[j++];
            }

            while (i < left.Length)
                arr[k++] = left[i++];

            while (j < right.Length)
                arr[k++] = right[j++];
        }

        /// <summary>
        /// Поразрядная сортировка - O(nk) - работает только для целых чисел
        /// </summary>
        public static void RadixSort(int[] arr, IComparer<int> comparer)
        {
            if (arr.Length == 0) return;

            int max = arr.Max();
            for (int exp = 1; max / exp > 0; exp *= 10)
                CountingSort(arr, exp);
        }

        private static void CountingSort(int[] arr, int exp)
        {
            int[] output = new int[arr.Length];
            int[] count = new int[10];

            for (int i = 0; i < arr.Length; ++i)
                count[(arr[i] / exp) % 10]++;

            for (int i = 1; i < 10; ++i)
                count[i] += count[i - 1];

            for (int i = arr.Length - 1; i >= 0; --i)
            {
                output[count[(arr[i] / exp) % 10] - 1] = arr[i];
                count[(arr[i] / exp) % 10]--;
            }

            Array.Copy(output, arr, arr.Length);
        }
    }
}