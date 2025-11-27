using System;
using System.Linq;

namespace Task3
{
    public static class Sortings
    {
        // ==================== ПЕРВАЯ ГРУППА - ПРОСТЫЕ СОРТИРОВКИ O(n^2) ====================

        /// <summary>
        /// Сортировка пузырьком - O(n^2)
        /// </summary>
        public static void BubbleSort(int[] arr)
        {
            int temp;
            for (int i = 0; i < arr.Length - 1; ++i)
                for (int j = 0; j < arr.Length - i - 1; ++j)
                    if (arr[j] > arr[j + 1])
                    {
                        temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
        }

        /// <summary>
        /// Шейкерная сортировка - O(n^2)
        /// </summary>
        public static void ShakerSort(int[] arr)
        {
            int temp, left = 0, right = arr.Length - 1;
            while (left < right)
            {
                for (int i = left; i < right; ++i)
                    if (arr[i] > arr[i + 1])
                    {
                        temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                    }
                --right;
                for (int i = right; i > left; --i)
                    if (arr[i] < arr[i - 1])
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
        public static void CombSort(int[] arr)
        {
            int temp, gap = arr.Length;
            bool swapped = true;
            while (gap > 1 || swapped)
            {
                gap = Math.Max(1, (int)(gap / 1.3));
                swapped = false;
                for (int i = 0; i + gap < arr.Length; ++i)
                    if (arr[i] > arr[i + gap])
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
        public static void InsertionSort(int[] arr)
        {
            int j, key;
            for (int i = 1; i < arr.Length; ++i)
            {
                key = arr[i];
                j = i - 1;
                while (j >= 0 && arr[j] > key)
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
        public static void SelectionSort(int[] arr)
        {
            int temp, minIndex;
            for (int i = 0; i < arr.Length - 1; ++i)
            {
                minIndex = i;
                for (int j = i + 1; j < arr.Length; ++j)
                    if (arr[j] < arr[minIndex])
                        minIndex = j;
                temp = arr[i];
                arr[i] = arr[minIndex];
                arr[minIndex] = temp;
            }
        }

        /// <summary>
        /// Гномья сортировка - O(n^2)
        /// </summary>
        public static void GnomeSort(int[] arr)
        {
            int temp, index = 1;
            while (index < arr.Length)
            {
                if (index == 0 || arr[index - 1] <= arr[index])
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
        public static void Shellsort(int[] arr)
        {
            for (int gap = arr.Length / 2; gap > 0; gap /= 2)
                for (int i = gap; i < arr.Length; ++i)
                {
                    int temp = arr[i];
                    int j;
                    for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                        arr[j] = arr[j - gap];
                    arr[j] = temp;
                }
        }

        /// <summary>
        /// Вспомогательный класс для сортировки деревом
        /// </summary>
        private class TreeNode
        {
            public int Value { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public TreeNode(int value)
            {
                Value = value;
                Left = null;
                Right = null;
            }

            public void Insert(int value)
            {
                if (value < Value)
                {
                    if (Left == null)
                        Left = new TreeNode(value);
                    else
                        Left.Insert(value);
                }
                else
                {
                    if (Right == null)
                        Right = new TreeNode(value);
                    else
                        Right.Insert(value);
                }
            }

            public void Traverse(int[] result, ref int index)
            {
                Left?.Traverse(result, ref index);
                result[index++] = Value;
                Right?.Traverse(result, ref index);
            }
        }

        /// <summary>
        /// Сортировка бинарным деревом - O(n log n) в среднем, O(n^2) в худшем
        /// </summary>
        public static void TreeSort(int[] arr)
        {
            if (arr.Length == 0) return;

            TreeNode root = new TreeNode(arr[0]);
            for (int i = 1; i < arr.Length; ++i)
                root.Insert(arr[i]);

            int index = 0;
            root.Traverse(arr, ref index);
        }

        /// <summary>
        /// Битонная сортировка - O(n log^2 n)
        /// </summary>
        public static void BitonicSort(int[] arr)
        {
            BitonicSort(arr, 0, arr.Length, true);
        }

        private static void BitonicSort(int[] arr, int low, int count, bool ascending)
        {
            if (count <= 1) return;

            int k = count / 2;
            BitonicSort(arr, low, k, true);
            BitonicSort(arr, low + k, k, false);
            BitonicMerge(arr, low, count, ascending);
        }

        private static void BitonicMerge(int[] arr, int low, int count, bool ascending)
        {
            if (count <= 1) return;

            int k = count / 2;
            for (int i = low; i < low + k; ++i)
            {
                if ((arr[i] > arr[i + k]) == ascending)
                {
                    int temp = arr[i];
                    arr[i] = arr[i + k];
                    arr[i + k] = temp;
                }
            }
            BitonicMerge(arr, low, k, ascending);
            BitonicMerge(arr, low + k, k, ascending);
        }

        // ==================== ТРЕТЬЯ ГРУППА - ЭФФЕКТИВНЫЕ СОРТИРОВКИ O(n log n) ====================

        /// <summary>
        /// Пирамидальная сортировка - O(n log n)
        /// </summary>
        public static void Heapsort(int[] arr)
        {
            // Построение max-кучи
            for (int i = arr.Length / 2 - 1; i >= 0; --i)
                Heapify(arr, arr.Length, i);

            // Извлечение элементов из кучи
            for (int i = arr.Length - 1; i >= 0; --i)
            {
                int temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;

                Heapify(arr, i, 0);
            }
        }

        private static void Heapify(int[] arr, int heapSize, int rootIndex)
        {
            int largest = rootIndex;
            int leftChild = 2 * rootIndex + 1;
            int rightChild = 2 * rootIndex + 2;

            if (leftChild < heapSize && arr[leftChild] > arr[largest])
                largest = leftChild;

            if (rightChild < heapSize && arr[rightChild] > arr[largest])
                largest = rightChild;

            if (largest != rootIndex)
            {
                int temp = arr[rootIndex];
                arr[rootIndex] = arr[largest];
                arr[largest] = temp;

                Heapify(arr, heapSize, largest);
            }
        }

        /// <summary>
        /// Быстрая сортировка - O(n log n) в среднем, O(n^2) в худшем
        /// </summary>
        public static void QuickSort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
        }

        private static void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(arr, low, high);
                QuickSort(arr, low, pivotIndex - 1);
                QuickSort(arr, pivotIndex + 1, high);
            }
        }

        private static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; ++j)
            {
                if (arr[j] <= pivot)
                {
                    ++i;
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            int temp2 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp2;

            return i + 1;
        }

        /// <summary>
        /// Сортировка слиянием - O(n log n)
        /// </summary>
        public static void MergeSort(int[] arr)
        {
            if (arr.Length <= 1) return;

            int mid = arr.Length / 2;
            int[] left = new int[mid];
            int[] right = new int[arr.Length - mid];

            Array.Copy(arr, 0, left, 0, mid);
            Array.Copy(arr, mid, right, 0, arr.Length - mid);

            MergeSort(left);
            MergeSort(right);
            Merge(arr, left, right);
        }

        private static void Merge(int[] arr, int[] left, int[] right)
        {
            int i = 0, j = 0, k = 0;

            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
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
        /// Поразрядная сортировка - O(nk)
        /// </summary>
        public static void RadixSort(int[] arr)
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