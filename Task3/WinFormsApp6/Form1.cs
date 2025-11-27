`using System.Diagnostics;
using ZedGraph;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System;

namespace Task3_
{
    public partial class Graphic : Form
    {
        // Основные переменные для управления тестами
        private int selectedTestGroup = 0;
        private int selectedSortGroup = 0;
        private int[][][] testData;
        private int[][][] originalTestData;

        // Элементы управления UI
        private System.Windows.Forms.Label countLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label parameterLabel = new System.Windows.Forms.Label();
        private TextBox countTextBox = new TextBox();
        private TextBox moduleTextBox = new TextBox();
        private TextBox permutationsTextBox = new TextBox();
        private CheckBox sortedCheckBox = new CheckBox();
        private CheckBox reversedCheckBox = new CheckBox();
        private CheckBox repeatCheckBox = new CheckBox();
        private ComboBox repeatsComboBox = new ComboBox();
        private System.Windows.Forms.Label errorLabel = new System.Windows.Forms.Label();

        public Graphic()
        {
            InitializeComponent();
            InitializeAdditionalControls();
        }

        /// <summary>
        /// Инициализация дополнительных элементов управления
        /// </summary>
        private void InitializeAdditionalControls()
        {
            errorLabel.ForeColor = Color.Red;
            errorLabel.Size = new Size(300, 50);
            errorLabel.Visible = false;
            this.Controls.Add(errorLabel);
        }

        private void comboBoxOfTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedTestGroup != 0)
            {
                this.Controls.Clear();
                this.Controls.Add(comboBoxOfTests);
                this.Controls.Add(comboBoxOfSorts);
                this.Controls.Add(zedGraphControl);
                this.Controls.Add(generateButton);
                this.Controls.Add(runButton);
                this.Controls.Add(saveButton);
            }

            string selectedTest = comboBoxOfTests.SelectedItem.ToString();
            
            if (selectedTest == "Случайные числа")
            {
                selectedTestGroup = 1;
            }
            else if (selectedTest == "Разбитые на подмассивы")
            {
                SetupArrayCountControls();
                SetupModuleControls();
                selectedTestGroup = 2;
            }
            else if (selectedTest == "Отсортированные массивы")
            {
                SetupArrayCountControls();
                SetupPermutationsControls();
                selectedTestGroup = 3;
            }
            else if (selectedTest == "Смешанные массивы")
            {
                SetupArrayCountControls();
                SetupMixedArrayControls();
                selectedTestGroup = 4;
            }
        }

        private void SetupArrayCountControls()
        {
            countLabel.Text = "Число массивов";
            countLabel.Location = new Point(40, 195);
            countLabel.Size = new Size(150, 29);
            this.Controls.Add(countLabel);
            
            countTextBox.Location = new Point(200, 195);
            this.Controls.Add(countTextBox);
        }

        private void SetupModuleControls()
        {
            parameterLabel.Text = "Макс размер подмассива";
            parameterLabel.Location = new Point(40, 275);
            parameterLabel.Size = new Size(150, 29);
            this.Controls.Add(parameterLabel);
            
            moduleTextBox.Location = new Point(200, 275);
            this.Controls.Add(moduleTextBox);
        }

        private void SetupPermutationsControls()
        {
            parameterLabel.Text = "Число перестановок";
            parameterLabel.Location = new Point(40, 275);
            parameterLabel.Size = new Size(160, 29);
            this.Controls.Add(parameterLabel);
            
            permutationsTextBox.Location = new Point(200, 275);
            this.Controls.Add(permutationsTextBox);
        }

        private void SetupMixedArrayControls()
        {
            sortedCheckBox.Location = new Point(40, 275);
            sortedCheckBox.Text = "Сортированный";
            this.Controls.Add(sortedCheckBox);
            
            reversedCheckBox.Location = new Point(160, 275);
            reversedCheckBox.Text = "Обратный порядок";
            this.Controls.Add(reversedCheckBox);
            
            repeatCheckBox.Location = new Point(280, 275);
            repeatCheckBox.Text = "Повторы";
            this.Controls.Add(repeatCheckBox);
            
            repeatsComboBox.Location = new Point(40, 355);
            repeatsComboBox.Size = new Size(300, 29);
            repeatsComboBox.Items.AddRange(new string[] { "10%", "25%", "50%", "75%", "90%" });
            this.Controls.Add(repeatsComboBox);
        }

        private void generate_Click(object sender, EventArgs e)
        {
            if (comboBoxOfSorts.SelectedItem == null)
            {
                ShowErrorMessage("Ошибка: выберите сортировку!");
                return;
            }

            string selectedSort = comboBoxOfSorts.SelectedItem.ToString();
            if (selectedSort == "Первая группа") selectedSortGroup = 1;
            if (selectedSort == "Вторая группа") selectedSortGroup = 2;
            if (selectedSort == "Третья группа") selectedSortGroup = 3;

            switch (selectedTestGroup)
            {
                case 0:
                    ShowErrorMessage("Ошибка: выберите тип массива!");
                    break;
                case 1:
                    GenerateRandomNumbersTest();
                    break;
                case 2:
                    GenerateSubarraysTest();
                    break;
                case 3:
                    GenerateSortedArraysTest();
                    break;
                case 4:
                    GenerateMixedArraysTest();
                    break;
            }
            
            originalTestData = testData;
        }

        private void GenerateRandomNumbersTest()
        {
            int testLevelsCount = 3 + selectedSortGroup;
            testData = new int[testLevelsCount][][];
            
            for (int level = 0; level < testLevelsCount; ++level)
            {
                int arraySize = (int)Math.Pow(10, 1 + level);
                testData[level] = new int[1][];
                TestGroups.RandomNumbersTest test = new TestGroups.RandomNumbersTest(arraySize);
                testData[level][0] = test.TestArray;
            }
        }

        private void GenerateSubarraysTest()
        {
            int arraysCount = int.Parse(countTextBox.Text);
            int maxSubarraySize = int.Parse(moduleTextBox.Text);
            
            int testLevelsCount = 3 + selectedSortGroup;
            testData = new int[testLevelsCount][][];
            
            for (int level = 0; level < testLevelsCount; ++level)
            {
                int arraySize = (int)Math.Pow(10, 1 + level);
                testData[level] = new int[arraysCount][];
                
                TestGroups.SubarraysTest test = new TestGroups.SubarraysTest(arraySize, arraysCount, maxSubarraySize);
                testData[level] = test.TestArrays;
            }
        }

        private void GenerateSortedArraysTest()
        {
            int arraysCount = int.Parse(countTextBox.Text);
            int permutationsCount = int.Parse(permutationsTextBox.Text);
            
            int testLevelsCount = 3 + selectedSortGroup;
            testData = new int[testLevelsCount][][];
            
            for (int level = 0; level < testLevelsCount; ++level)
            {
                int arraySize = (int)Math.Pow(10, 1 + level);
                testData[level] = new int[arraysCount][];
                
                TestGroups.PartiallySortedTest test = new TestGroups.PartiallySortedTest(arraySize, arraysCount, permutationsCount);
                testData[level] = test.TestArrays;
            }
        }

        private void GenerateMixedArraysTest()
        {
            int arraysCount = int.Parse(countTextBox.Text);
            int[] parameters = PrepareMixedArrayParameters();
            
            int testLevelsCount = 3 + selectedSortGroup;
            testData = new int[testLevelsCount][][];
            
            for (int level = 0; level < testLevelsCount; ++level)
            {
                int arraySize = (int)Math.Pow(10, 1 + level);
                testData[level] = new int[arraysCount][];
                
                TestGroups.MixedArraysTest test = new TestGroups.MixedArraysTest(arraySize, arraysCount, parameters);
                testData[level] = test.TestArrays;
            }
        }

        private int[] PrepareMixedArrayParameters()
        {
            int parametersCount = repeatCheckBox.Checked ? 3 : 2;
            int[] parameters = new int[parametersCount];
            
            if (sortedCheckBox.Checked)
                parameters[0] = reversedCheckBox.Checked ? 1 : 2;
            else
                parameters[0] = 0;
            
            parameters[1] = permutationsTextBox.Text != null ? 1 : 0;
            
            if (repeatCheckBox.Checked && repeatsComboBox.SelectedItem != null)
                parameters[2] = int.Parse(repeatsComboBox.SelectedItem.ToString().Replace("%", ""));
            
            return parameters;
        }

        private void ShowErrorMessage(string message)
        {
            errorLabel.Location = new Point(100, 270);
            errorLabel.Text = message;
            errorLabel.Visible = true;
            this.Controls.Add(errorLabel);
    
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer(); // Явное указание пространства имен
            timer.Interval = 3000;
            timer.Tick += (s, e) => {
                errorLabel.Visible = false;
                this.Controls.Remove(errorLabel);
                timer.Stop();
            };
            timer.Start();
        }

        private double[][] RunPerformanceTests()
        {
            switch (selectedSortGroup)
            {
                case 1:
                    return RunGroup1Tests();
                case 2:
                    return RunGroup2Tests();
                case 3:
                    return RunGroup3Tests();
                default:
                    return new double[0][];
            }
        }

        private double[][] RunGroup1Tests()
        {
            double[][] results = InitializeResultsArray(5);
            const int testIterations = 5;
            
            for (int sizeLevel = 0; sizeLevel < testData.Length; ++sizeLevel)
            {
                for (int arrayIndex = 0; arrayIndex < testData[sizeLevel].Length; ++arrayIndex)
                {
                    for (int iteration = 0; iteration < testIterations; ++iteration)
                    {
                        results[0][sizeLevel] += MeasureBubbleSort(testData[sizeLevel][arrayIndex]);
                        results[1][sizeLevel] += MeasureInsertionSort(testData[sizeLevel][arrayIndex]);
                        results[2][sizeLevel] += MeasureSelectionSort(testData[sizeLevel][arrayIndex]);
                        results[3][sizeLevel] += MeasureShakerSort(testData[sizeLevel][arrayIndex]);
                        results[4][sizeLevel] += MeasureGnomeSort(testData[sizeLevel][arrayIndex]);
                    }
                }
            }
            
            CalculateAverageTimes(results, testIterations);
            return results;
        }

        private double[][] RunGroup2Tests()
        {
            double[][] results = InitializeResultsArray(3);
            const int testIterations = 5;
            
            for (int sizeLevel = 0; sizeLevel < testData.Length; ++sizeLevel)
            {
                for (int arrayIndex = 0; arrayIndex < testData[sizeLevel].Length; ++arrayIndex)
                {
                    for (int iteration = 0; iteration < testIterations; ++iteration)
                    {
                        results[0][sizeLevel] += MeasureBitonicSort(testData[sizeLevel][arrayIndex]);
                        results[1][sizeLevel] += MeasureShellSort(testData[sizeLevel][arrayIndex]);
                        results[2][sizeLevel] += MeasureTreeSort(testData[sizeLevel][arrayIndex]);
                    }
                }
            }
            
            CalculateAverageTimes(results, testIterations);
            return results;
        }

        private double[][] RunGroup3Tests()
        {
            double[][] results = InitializeResultsArray(5);
            const int testIterations = 5;
            
            for (int sizeLevel = 0; sizeLevel < testData.Length; ++sizeLevel)
            {
                for (int arrayIndex = 0; arrayIndex < testData[sizeLevel].Length; ++arrayIndex)
                {
                    for (int iteration = 0; iteration < testIterations; ++iteration)
                    {
                        results[0][sizeLevel] += MeasureCombSort(testData[sizeLevel][arrayIndex]);
                        results[1][sizeLevel] += MeasureHeapSort(testData[sizeLevel][arrayIndex]);
                        results[2][sizeLevel] += MeasureQuickSort(testData[sizeLevel][arrayIndex]);
                        results[3][sizeLevel] += MeasureMergeSort(testData[sizeLevel][arrayIndex]);
                        results[4][sizeLevel] += MeasureRadixSort(testData[sizeLevel][arrayIndex]);
                    }
                }
            }
            
            CalculateAverageTimes(results, testIterations);
            return results;
        }

        // Методы измерения времени для каждой сортировки
        private long MeasureBubbleSort(int[] originalArray)
        {
            int[] testArray = (int[])originalArray.Clone();
            Stopwatch stopwatch = Stopwatch.StartNew();
            Sortings.BubbleSort(testArray);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long MeasureInsertionSort(int[] originalArray)
        {
            int[] testArray = (int[])originalArray.Clone();
            Stopwatch stopwatch = Stopwatch.StartNew();
            Sortings.InsertionSort(testArray);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long MeasureSelectionSort(int[] originalArray)
        {
            int[] testArray = (int[])originalArray.Clone();
            Stopwatch stopwatch = Stopwatch.StartNew();
            Sortings.SelectionSort(testArray);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long MeasureShakerSort(int[] originalArray)
        {
            int[] testArray = (int[])originalArray.Clone();
            Stopwatch stopwatch = Stopwatch.StartNew();
            Sortings.ShakerSort(testArray);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long MeasureGnomeSort(int[] originalArray)
        {
            int[] testArray = (int[])originalArray.Clone();
            Stopwatch stopwatch = Stopwatch.StartNew();
            Sortings.GnomeSort(testArray);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long MeasureBitonicSort(int[] originalArray)
        {
            int[] testArray = (int[])originalArray.Clone();
            Stopwatch stopwatch = Stopwatch.StartNew();
            Sortings.BitonicSort(testArray);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long MeasureShellSort(int[] originalArray)
        {
            int[] testArray = (int[])originalArray.Clone();
            Stopwatch stopwatch = Stopwatch.StartNew();
            Sortings.Shellsort(testArray);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long MeasureTreeSort(int[] originalArray)
        {
            int[] testArray = (int[])originalArray.Clone();
            Stopwatch stopwatch = Stopwatch.StartNew();
            Sortings.TreeSort(testArray);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long MeasureCombSort(int[] originalArray)
        {
            int[] testArray = (int[])originalArray.Clone();
            Stopwatch stopwatch = Stopwatch.StartNew();
            Sortings.CombSort(testArray);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long MeasureHeapSort(int[] originalArray)
        {
            int[] testArray = (int[])originalArray.Clone();
            Stopwatch stopwatch = Stopwatch.StartNew();
            Sortings.Heapsort(testArray);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long MeasureQuickSort(int[] originalArray)
        {
            int[] testArray = (int[])originalArray.Clone();
            Stopwatch stopwatch = Stopwatch.StartNew();
            Sortings.QuickSort(testArray);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long MeasureMergeSort(int[] originalArray)
        {
            int[] testArray = (int[])originalArray.Clone();
            Stopwatch stopwatch = Stopwatch.StartNew();
            Sortings.MergeSort(testArray);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private long MeasureRadixSort(int[] originalArray)
        {
            int[] testArray = (int[])originalArray.Clone();
            Stopwatch stopwatch = Stopwatch.StartNew();
            Sortings.RadixSort(testArray);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private double[][] InitializeResultsArray(int algorithmsCount)
        {
            double[][] results = new double[algorithmsCount][];
            for (int i = 0; i < algorithmsCount; ++i)
            {
                results[i] = new double[testData.Length];
                for (int j = 0; j < testData.Length; ++j)
                    results[i][j] = 0.0;
            }
            return results;
        }

        private void CalculateAverageTimes(double[][] results, int testIterations)
        {
            for (int algorithm = 0; algorithm < results.Length; ++algorithm)
            {
                for (int sizeLevel = 0; sizeLevel < results[algorithm].Length; ++sizeLevel)
                {
                    results[algorithm][sizeLevel] /= (testData[0].Length * testIterations);
                }
            }
        }

        private void run_Click(object sender, EventArgs e)
        {
            if (testData == null)
            {
                ShowErrorMessage("Сначала сгенерируйте массивы!");
                return;
            }

            double[][] results = RunPerformanceTests();
            DisplayResultsOnGraph(results);
        }

        private void DisplayResultsOnGraph(double[][] results)
        {
            GraphPane pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();
            
            string[] group1Names = new string[] {
                "Сортировка пузырьком", "Сортировка вставками", "Сортировка выбором",
                "Шейкерная сортировка", "Гномья сортировка"
            };
            
            string[] group2Names = new string[] {
                "Битонная сортировка", "Сортировка Шелла", "Сортировка деревом"
            };
            
            string[] group3Names = new string[] {
                "Сортировка расчёской", "Пирамидальная сортировка", "Быстрая сортировка",
                "Сортировка слиянием", "Порязрядная сортировка"
            };
            
            Color[] colors = new Color[] { Color.Blue, Color.Red, Color.Green, Color.Purple, Color.Orange };
            
            switch (selectedSortGroup)
            {
                case 1:
                    for (int i = 0; i < 5; i++)
                        AddCurveToGraph(pane, results[i], group1Names[i], colors[i]);
                    break;
                case 2:
                    for (int i = 0; i < 3; i++)
                        AddCurveToGraph(pane, results[i], group2Names[i], colors[i]);
                    break;
                case 3:
                    for (int i = 0; i < 5; i++)
                        AddCurveToGraph(pane, results[i], group3Names[i], colors[i]);
                    break;
            }
            
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        private void AddCurveToGraph(GraphPane pane, double[] data, string label, Color color)
        {
            PointPairList points = new PointPairList();
            for (int i = 0; i < data.Length; ++i)
            {
                points.Add(Math.Pow(10, i + 1), data[i]);
            }
            pane.AddCurve(label, points, color, SymbolType.Circle);
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (originalTestData == null)
            {
                ShowErrorMessage("Нет данных для сохранения!");
                return;
            }

            using (StreamWriter writer = new StreamWriter("results.txt", false))
            {
                SaveOriginalArrays(writer);
                SaveSortedArrays(writer);
            }
            
            MessageBox.Show("Результаты сохранены в файл results.txt", "Сохранение завершено", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveOriginalArrays(StreamWriter writer)
        {
            writer.WriteLine("Сгенерированные массивы:\n");
            for (int sizeLevel = 0; sizeLevel < originalTestData.Length; ++sizeLevel)
            {
                writer.WriteLine($"=== Размер массива: {(int)Math.Pow(10, sizeLevel + 1)} ===");
                for (int arrayIndex = 0; arrayIndex < originalTestData[sizeLevel].Length; ++arrayIndex)
                {
                    writer.WriteLine($"Массив #{arrayIndex + 1}:");
                    writer.WriteLine(string.Join(" ", originalTestData[sizeLevel][arrayIndex]));
                    writer.WriteLine();
                }
                writer.WriteLine();
            }
        }

        private void SaveSortedArrays(StreamWriter writer)
        {
            writer.WriteLine("\nОтсортированные версии:\n");
            
            switch (selectedSortGroup)
            {
                case 1:
                    SaveGroup1SortedArrays(writer);
                    break;
                case 2:
                    SaveGroup2SortedArrays(writer);
                    break;
                case 3:
                    SaveGroup3SortedArrays(writer);
                    break;
            }
        }

        private void SaveGroup1SortedArrays(StreamWriter writer)
        {
            string[] sortNames = new string[] {
                "Сортировка пузырьком", "Сортировка вставками", "Сортировка выбором",
                "Шейкерная сортировка", "Гномья сортировка"
            };

            for (int sortIndex = 0; sortIndex < 5; sortIndex++)
            {
                writer.WriteLine($"\n{sortNames[sortIndex]}:\n");
                for (int sizeLevel = 0; sizeLevel < originalTestData.Length; sizeLevel++)
                {
                    writer.WriteLine($"Размер: {(int)Math.Pow(10, sizeLevel + 1)}");
                    for (int arrayIndex = 0; arrayIndex < originalTestData[sizeLevel].Length; arrayIndex++)
                    {
                        int[] arrayToSort = (int[])originalTestData[sizeLevel][arrayIndex].Clone();
                        
                        switch (sortIndex)
                        {
                            case 0: Sortings.BubbleSort(arrayToSort); break;
                            case 1: Sortings.InsertionSort(arrayToSort); break;
                            case 2: Sortings.SelectionSort(arrayToSort); break;
                            case 3: Sortings.ShakerSort(arrayToSort); break;
                            case 4: Sortings.GnomeSort(arrayToSort); break;
                        }
                        
                        writer.WriteLine(string.Join(" ", arrayToSort));
                    }
                    writer.WriteLine();
                }
            }
        }

        private void SaveGroup2SortedArrays(StreamWriter writer)
        {
            string[] sortNames = new string[] {
                "Битонная сортировка", "Сортировка Шелла", "Сортировка деревом"
            };

            for (int sortIndex = 0; sortIndex < 3; sortIndex++)
            {
                writer.WriteLine($"\n{sortNames[sortIndex]}:\n");
                for (int sizeLevel = 0; sizeLevel < originalTestData.Length; sizeLevel++)
                {
                    writer.WriteLine($"Размер: {(int)Math.Pow(10, sizeLevel + 1)}");
                    for (int arrayIndex = 0; arrayIndex < originalTestData[sizeLevel].Length; arrayIndex++)
                    {
                        int[] arrayToSort = (int[])originalTestData[sizeLevel][arrayIndex].Clone();
                        
                        switch (sortIndex)
                        {
                            case 0: Sortings.BitonicSort(arrayToSort); break;
                            case 1: Sortings.Shellsort(arrayToSort); break;
                            case 2: Sortings.TreeSort(arrayToSort); break;
                        }
                        
                        writer.WriteLine(string.Join(" ", arrayToSort));
                    }
                    writer.WriteLine();
                }
            }
        }

        private void SaveGroup3SortedArrays(StreamWriter writer)
        {
            string[] sortNames = new string[] {
                "Сортировка расчёской", "Пирамидальная сортировка", "Быстрая сортировка",
                "Сортировка слиянием", "Порязрядная сортировка"
            };

            for (int sortIndex = 0; sortIndex < 5; sortIndex++)
            {
                writer.WriteLine($"\n{sortNames[sortIndex]}:\n");
                for (int sizeLevel = 0; sizeLevel < originalTestData.Length; sizeLevel++)
                {
                    writer.WriteLine($"Размер: {(int)Math.Pow(10, sizeLevel + 1)}");
                    for (int arrayIndex = 0; arrayIndex < originalTestData[sizeLevel].Length; arrayIndex++)
                    {
                        int[] arrayToSort = (int[])originalTestData[sizeLevel][arrayIndex].Clone();
                        
                        switch (sortIndex)
                        {
                            case 0: Sortings.CombSort(arrayToSort); break;
                            case 1: Sortings.Heapsort(arrayToSort); break;
                            case 2: Sortings.QuickSort(arrayToSort); break;
                            case 3: Sortings.MergeSort(arrayToSort); break;
                            case 4: Sortings.RadixSort(arrayToSort); break;
                        }
                        
                        writer.WriteLine(string.Join(" ", arrayToSort));
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}