using System.Diagnostics;
using ZedGraph;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Task4
{
    public partial class Graphic : Form
    {
        // Основные переменные
        private int selectedTestGroup = 0;
        private int selectedSortGroup = 0;
        private int selectedDataType = 0;
        private object[][][] testData;
        private object[][][] originalTestData;

        // Элементы управления UI
        private System.Windows.Forms.Label countLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label parameterLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label dataTypeLabel = new System.Windows.Forms.Label();
        private TextBox countTextBox = new TextBox();
        private TextBox moduleTextBox = new TextBox();
        private TextBox permutationsTextBox = new TextBox();
        private CheckBox sortedCheckBox = new CheckBox();
        private CheckBox reversedCheckBox = new CheckBox();
        private CheckBox repeatCheckBox = new CheckBox();
        private ComboBox repeatsComboBox = new ComboBox();
        private ComboBox dataTypeComboBox = new ComboBox();
        private System.Windows.Forms.Label errorLabel = new System.Windows.Forms.Label();

        private Random random = new Random();
        
     
        private delegate void SortDelegate(object[] array);
        
        public Graphic()
        {
            InitializeComponent();
            InitializeAdditionalControls();
        }

        private void InitializeAdditionalControls()
        {
            errorLabel.ForeColor = Color.Red;
            errorLabel.Size = new Size(300, 50);
            errorLabel.Visible = false;
            
            dataTypeLabel.Text = "Тип данных";
            dataTypeLabel.Location = new Point(40, 80);
            dataTypeLabel.Size = new Size(150, 29);
            
            dataTypeComboBox.Location = new Point(200, 80);
            dataTypeComboBox.Size = new Size(140, 28);
            dataTypeComboBox.Items.AddRange(new string[] { "Целые числа", "Вещественные числа", "Строки" });
            dataTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            dataTypeComboBox.SelectedIndex = 0;
            dataTypeComboBox.SelectedIndexChanged += DataTypeComboBox_SelectedIndexChanged;
            
            this.Controls.Add(dataTypeLabel);
            this.Controls.Add(dataTypeComboBox);
            this.Controls.Add(errorLabel);
        }

        private void DataTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedDataType = dataTypeComboBox.SelectedIndex;
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
                this.Controls.Add(dataTypeLabel);
                this.Controls.Add(dataTypeComboBox);
            }

            string selectedTest = comboBoxOfTests.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedTest)) return;
            
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
            countTextBox.Text = "5";
            this.Controls.Add(countTextBox);
        }

        private void SetupModuleControls()
        {
            parameterLabel.Text = "Макс размер подмассива";
            parameterLabel.Location = new Point(40, 275);
            parameterLabel.Size = new Size(150, 29);
            this.Controls.Add(parameterLabel);
            
            moduleTextBox.Location = new Point(200, 275);
            moduleTextBox.Text = "100";
            this.Controls.Add(moduleTextBox);
        }

        private void SetupPermutationsControls()
        {
            parameterLabel.Text = "Число перестановок";
            parameterLabel.Location = new Point(40, 275);
            parameterLabel.Size = new Size(160, 29);
            this.Controls.Add(parameterLabel);
            
            permutationsTextBox.Location = new Point(200, 275);
            permutationsTextBox.Text = "10";
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
            repeatsComboBox.SelectedIndex = 0;
            this.Controls.Add(repeatsComboBox);
        }

        private void generate_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxOfSorts.SelectedItem == null)
                {
                    ShowErrorMessage("Ошибка: выберите сортировку!");
                    return;
                }

                string selectedSort = comboBoxOfSorts.SelectedItem.ToString();
                if (selectedSort == "Первая группа") selectedSortGroup = 1;
                else if (selectedSort == "Вторая группа") selectedSortGroup = 2;
                else if (selectedSort == "Третья группа") selectedSortGroup = 3;

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
                ShowErrorMessage("Массивы успешно сгенерированы!", false);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка генерации: {ex.Message}");
            }
        }

        private void GenerateRandomNumbersTest()
        {
            int testLevelsCount = 3 + selectedSortGroup;
            testData = new object[testLevelsCount][][];
            
            for (int level = 0; level < testLevelsCount; ++level)
            {
                int arraySize = (int)Math.Pow(10, 1 + level);
                testData[level] = new object[1][];
                
                switch (selectedDataType)
                {
                    case 0: // int
                        var intTest = new GenericTestGroups<int>.RandomNumbersTest(arraySize, () => random.Next(0, 1000));
                        testData[level][0] = intTest.TestArray.Cast<object>().ToArray();
                        break;
                    case 1: // double
                        var doubleTest = new GenericTestGroups<double>.RandomNumbersTest(arraySize, () => random.NextDouble() * 1000);
                        testData[level][0] = doubleTest.TestArray.Cast<object>().ToArray();
                        break;
                    case 2: // string
                        var stringTest = new GenericTestGroups<string>.RandomNumbersTest(arraySize, GenerateRandomString);
                        testData[level][0] = stringTest.TestArray.Cast<object>().ToArray();
                        break;
                }
            }
        }

        private void GenerateSubarraysTest()
        {
            int arraysCount = int.Parse(countTextBox.Text);
            int maxSubarraySize = int.Parse(moduleTextBox.Text);
            
            int testLevelsCount = 3 + selectedSortGroup;
            testData = new object[testLevelsCount][][];
            
            for (int level = 0; level < testLevelsCount; ++level)
            {
                int arraySize = (int)Math.Pow(10, 1 + level);
                testData[level] = new object[arraysCount][];
                
                switch (selectedDataType)
                {
                    case 0:
                        var intTest = new GenericTestGroups<int>.SubarraysTest(arraySize, arraysCount, maxSubarraySize, 
                            () => random.Next(0, 1000), Comparer<int>.Default);
                        testData[level] = ConvertToObjectArrays(intTest.TestArrays);
                        break;
                    case 1:
                        var doubleTest = new GenericTestGroups<double>.SubarraysTest(arraySize, arraysCount, maxSubarraySize,
                            () => random.NextDouble() * 1000, Comparer<double>.Default);
                        testData[level] = ConvertToObjectArrays(doubleTest.TestArrays);
                        break;
                    case 2:
                        var stringTest = new GenericTestGroups<string>.SubarraysTest(arraySize, arraysCount, maxSubarraySize,
                            GenerateRandomString, Comparer<string>.Default);
                        testData[level] = ConvertToObjectArrays(stringTest.TestArrays);
                        break;
                }
            }
        }

        private void GenerateSortedArraysTest()
        {
            int arraysCount = int.Parse(countTextBox.Text);
            int permutationsCount = int.Parse(permutationsTextBox.Text);
            
            int testLevelsCount = 3 + selectedSortGroup;
            testData = new object[testLevelsCount][][];
            
            for (int level = 0; level < testLevelsCount; ++level)
            {
                int arraySize = (int)Math.Pow(10, 1 + level);
                testData[level] = new object[arraysCount][];
                
                switch (selectedDataType)
                {
                    case 0:
                        var intTest = new GenericTestGroups<int>.PartiallySortedTest(arraySize, arraysCount, permutationsCount,
                            () => random.Next(0, 1000), Comparer<int>.Default);
                        testData[level] = ConvertToObjectArrays(intTest.TestArrays);
                        break;
                    case 1:
                        var doubleTest = new GenericTestGroups<double>.PartiallySortedTest(arraySize, arraysCount, permutationsCount,
                            () => random.NextDouble() * 1000, Comparer<double>.Default);
                        testData[level] = ConvertToObjectArrays(doubleTest.TestArrays);
                        break;
                    case 2:
                        var stringTest = new GenericTestGroups<string>.PartiallySortedTest(arraySize, arraysCount, permutationsCount,
                            GenerateRandomString, Comparer<string>.Default);
                        testData[level] = ConvertToObjectArrays(stringTest.TestArrays);
                        break;
                }
            }
        }

        private void GenerateMixedArraysTest()
        {
            int arraysCount = int.Parse(countTextBox.Text);
            int[] parameters = PrepareMixedArrayParameters();
            
            int testLevelsCount = 3 + selectedSortGroup;
            testData = new object[testLevelsCount][][];
            
            for (int level = 0; level < testLevelsCount; ++level)
            {
                int arraySize = (int)Math.Pow(10, 1 + level);
                testData[level] = new object[arraysCount][];
                
                switch (selectedDataType)
                {
                    case 0:
                        var intTest = new GenericTestGroups<int>.MixedArraysTest(arraySize, arraysCount, parameters,
                            () => random.Next(0, 1000), Comparer<int>.Default);
                        testData[level] = ConvertToObjectArrays(intTest.TestArrays);
                        break;
                    case 1:
                        var doubleTest = new GenericTestGroups<double>.MixedArraysTest(arraySize, arraysCount, parameters,
                            () => random.NextDouble() * 1000, Comparer<double>.Default);
                        testData[level] = ConvertToObjectArrays(doubleTest.TestArrays);
                        break;
                    case 2:
                        var stringTest = new GenericTestGroups<string>.MixedArraysTest(arraySize, arraysCount, parameters,
                            GenerateRandomString, Comparer<string>.Default);
                        testData[level] = ConvertToObjectArrays(stringTest.TestArrays);
                        break;
                }
            }
        }

        private string GenerateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            int length = random.Next(5, 15);
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private object[][] ConvertToObjectArrays<T>(T[][] arrays)
        {
            object[][] result = new object[arrays.Length][];
            for (int i = 0; i < arrays.Length; i++)
            {
                result[i] = arrays[i].Cast<object>().ToArray();
            }
            return result;
        }

        private int[] PrepareMixedArrayParameters()
        {
            int parametersCount = repeatCheckBox.Checked ? 3 : 2;
            int[] parameters = new int[parametersCount];
            
            parameters[0] = sortedCheckBox.Checked ? (reversedCheckBox.Checked ? 1 : 2) : 0;
            parameters[1] = 1;
            
            if (repeatCheckBox.Checked && repeatsComboBox.SelectedItem != null)
            {
                string percentStr = repeatsComboBox.SelectedItem.ToString().Replace("%", "");
                parameters[2] = int.Parse(percentStr);
            }
            else
            {
                parameters[2] = 0;
            }
            
            return parameters;
        }

        private void ShowErrorMessage(string message, bool isError = true)
        {
            errorLabel.ForeColor = isError ? Color.Red : Color.Green;
            errorLabel.Location = new Point(100, 470);
            errorLabel.Text = message;
            errorLabel.Visible = true;
            this.Controls.Add(errorLabel);
    
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 3000;
            timer.Tick += (s, e) => {
                errorLabel.Visible = false;
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
            const int testIterations = 3;
            
            for (int sizeLevel = 0; sizeLevel < testData.Length; ++sizeLevel)
            {
                for (int arrayIndex = 0; arrayIndex < testData[sizeLevel].Length; ++arrayIndex)
                {
                    for (int iteration = 0; iteration < testIterations; ++iteration)
                    {
                        // Используем ДЕЛЕГАТЫ для вызова сортировок
                        results[0][sizeLevel] += MeasureSort(testData[sizeLevel][arrayIndex], BubbleSortWrapper);
                        results[1][sizeLevel] += MeasureSort(testData[sizeLevel][arrayIndex], InsertionSortWrapper);
                        results[2][sizeLevel] += MeasureSort(testData[sizeLevel][arrayIndex], SelectionSortWrapper);
                        results[3][sizeLevel] += MeasureSort(testData[sizeLevel][arrayIndex], ShakerSortWrapper);
                        results[4][sizeLevel] += MeasureSort(testData[sizeLevel][arrayIndex], GnomeSortWrapper);
                    }
                }
            }
            
            CalculateAverageTimes(results, testIterations);
            return results;
        }

        private double[][] RunGroup2Tests()
        {
            double[][] results = InitializeResultsArray(3);
            const int testIterations = 3;
            
            for (int sizeLevel = 0; sizeLevel < testData.Length; ++sizeLevel)
            {
                for (int arrayIndex = 0; arrayIndex < testData[sizeLevel].Length; ++arrayIndex)
                {
                    for (int iteration = 0; iteration < testIterations; ++iteration)
                    {
                        results[0][sizeLevel] += MeasureSort(testData[sizeLevel][arrayIndex], ShellSortWrapper);
                        results[1][sizeLevel] += MeasureSort(testData[sizeLevel][arrayIndex], HeapSortWrapper);
                        results[2][sizeLevel] += MeasureSort(testData[sizeLevel][arrayIndex], QuickSortWrapper);
                    }
                }
            }
            
            CalculateAverageTimes(results, testIterations);
            return results;
        }

        private double[][] RunGroup3Tests()
        {
            double[][] results = InitializeResultsArray(4);
            const int testIterations = 3;
            
            for (int sizeLevel = 0; sizeLevel < testData.Length; ++sizeLevel)
            {
                for (int arrayIndex = 0; arrayIndex < testData[sizeLevel].Length; ++arrayIndex)
                {
                    for (int iteration = 0; iteration < testIterations; ++iteration)
                    {
                        results[0][sizeLevel] += MeasureSort(testData[sizeLevel][arrayIndex], CombSortWrapper);
                        results[1][sizeLevel] += MeasureSort(testData[sizeLevel][arrayIndex], MergeSortWrapper);
                        results[2][sizeLevel] += MeasureSort(testData[sizeLevel][arrayIndex], BuiltInSortWrapper);
                        results[3][sizeLevel] += MeasureSort(testData[sizeLevel][arrayIndex], TreeSortWrapper);
                    }
                }
            }
            
            CalculateAverageTimes(results, testIterations);
            return results;
        }

        
        private void BubbleSortWrapper(object[] arr)
        {
            switch (selectedDataType)
            {
                case 0:
                    int[] intArr = arr.Cast<int>().ToArray();
                    GenericSortings<int>.BubbleSort(intArr, Comparer<int>.Default);
                    Array.Copy(intArr, arr, arr.Length);
                    break;
                case 1:
                    double[] doubleArr = arr.Cast<double>().ToArray();
                    GenericSortings<double>.BubbleSort(doubleArr, Comparer<double>.Default);
                    Array.Copy(doubleArr, arr, arr.Length);
                    break;
                case 2:
                    string[] stringArr = arr.Cast<string>().ToArray();
                    GenericSortings<string>.BubbleSort(stringArr, Comparer<string>.Default);
                    Array.Copy(stringArr, arr, arr.Length);
                    break;
            }
        }

        private void InsertionSortWrapper(object[] arr)
        {
            switch (selectedDataType)
            {
                case 0:
                    int[] intArr = arr.Cast<int>().ToArray();
                    GenericSortings<int>.InsertionSort(intArr, Comparer<int>.Default);
                    Array.Copy(intArr, arr, arr.Length);
                    break;
                case 1:
                    double[] doubleArr = arr.Cast<double>().ToArray();
                    GenericSortings<double>.InsertionSort(doubleArr, Comparer<double>.Default);
                    Array.Copy(doubleArr, arr, arr.Length);
                    break;
                case 2:
                    string[] stringArr = arr.Cast<string>().ToArray();
                    GenericSortings<string>.InsertionSort(stringArr, Comparer<string>.Default);
                    Array.Copy(stringArr, arr, arr.Length);
                    break;
            }
        }

        private void SelectionSortWrapper(object[] arr)
        {
            switch (selectedDataType)
            {
                case 0:
                    int[] intArr = arr.Cast<int>().ToArray();
                    GenericSortings<int>.SelectionSort(intArr, Comparer<int>.Default);
                    Array.Copy(intArr, arr, arr.Length);
                    break;
                case 1:
                    double[] doubleArr = arr.Cast<double>().ToArray();
                    GenericSortings<double>.SelectionSort(doubleArr, Comparer<double>.Default);
                    Array.Copy(doubleArr, arr, arr.Length);
                    break;
                case 2:
                    string[] stringArr = arr.Cast<string>().ToArray();
                    GenericSortings<string>.SelectionSort(stringArr, Comparer<string>.Default);
                    Array.Copy(stringArr, arr, arr.Length);
                    break;
            }
        }

        private void ShakerSortWrapper(object[] arr)
        {
            switch (selectedDataType)
            {
                case 0:
                    int[] intArr = arr.Cast<int>().ToArray();
                    GenericSortings<int>.ShakerSort(intArr, Comparer<int>.Default);
                    Array.Copy(intArr, arr, arr.Length);
                    break;
                case 1:
                    double[] doubleArr = arr.Cast<double>().ToArray();
                    GenericSortings<double>.ShakerSort(doubleArr, Comparer<double>.Default);
                    Array.Copy(doubleArr, arr, arr.Length);
                    break;
                case 2:
                    string[] stringArr = arr.Cast<string>().ToArray();
                    GenericSortings<string>.ShakerSort(stringArr, Comparer<string>.Default);
                    Array.Copy(stringArr, arr, arr.Length);
                    break;
            }
        }

        private void GnomeSortWrapper(object[] arr)
        {
            switch (selectedDataType)
            {
                case 0:
                    int[] intArr = arr.Cast<int>().ToArray();
                    GenericSortings<int>.GnomeSort(intArr, Comparer<int>.Default);
                    Array.Copy(intArr, arr, arr.Length);
                    break;
                case 1:
                    double[] doubleArr = arr.Cast<double>().ToArray();
                    GenericSortings<double>.GnomeSort(doubleArr, Comparer<double>.Default);
                    Array.Copy(doubleArr, arr, arr.Length);
                    break;
                case 2:
                    string[] stringArr = arr.Cast<string>().ToArray();
                    GenericSortings<string>.GnomeSort(stringArr, Comparer<string>.Default);
                    Array.Copy(stringArr, arr, arr.Length);
                    break;
            }
        }

        private void ShellSortWrapper(object[] arr)
        {
            switch (selectedDataType)
            {
                case 0:
                    int[] intArr = arr.Cast<int>().ToArray();
                    GenericSortings<int>.Shellsort(intArr, Comparer<int>.Default);
                    Array.Copy(intArr, arr, arr.Length);
                    break;
                case 1:
                    double[] doubleArr = arr.Cast<double>().ToArray();
                    GenericSortings<double>.Shellsort(doubleArr, Comparer<double>.Default);
                    Array.Copy(doubleArr, arr, arr.Length);
                    break;
                case 2:
                    string[] stringArr = arr.Cast<string>().ToArray();
                    GenericSortings<string>.Shellsort(stringArr, Comparer<string>.Default);
                    Array.Copy(stringArr, arr, arr.Length);
                    break;
            }
        }

        private void HeapSortWrapper(object[] arr)
        {
            switch (selectedDataType)
            {
                case 0:
                    int[] intArr = arr.Cast<int>().ToArray();
                    GenericSortings<int>.Heapsort(intArr, Comparer<int>.Default);
                    Array.Copy(intArr, arr, arr.Length);
                    break;
                case 1:
                    double[] doubleArr = arr.Cast<double>().ToArray();
                    GenericSortings<double>.Heapsort(doubleArr, Comparer<double>.Default);
                    Array.Copy(doubleArr, arr, arr.Length);
                    break;
                case 2:
                    string[] stringArr = arr.Cast<string>().ToArray();
                    GenericSortings<string>.Heapsort(stringArr, Comparer<string>.Default);
                    Array.Copy(stringArr, arr, arr.Length);
                    break;
            }
        }

        private void QuickSortWrapper(object[] arr)
        {
            switch (selectedDataType)
            {
                case 0:
                    int[] intArr = arr.Cast<int>().ToArray();
                    GenericSortings<int>.QuickSort(intArr, Comparer<int>.Default);
                    Array.Copy(intArr, arr, arr.Length);
                    break;
                case 1:
                    double[] doubleArr = arr.Cast<double>().ToArray();
                    GenericSortings<double>.QuickSort(doubleArr, Comparer<double>.Default);
                    Array.Copy(doubleArr, arr, arr.Length);
                    break;
                case 2:
                    string[] stringArr = arr.Cast<string>().ToArray();
                    GenericSortings<string>.QuickSort(stringArr, Comparer<string>.Default);
                    Array.Copy(stringArr, arr, arr.Length);
                    break;
            }
        }

        private void CombSortWrapper(object[] arr)
        {
            switch (selectedDataType)
            {
                case 0:
                    int[] intArr = arr.Cast<int>().ToArray();
                    GenericSortings<int>.CombSort(intArr, Comparer<int>.Default);
                    Array.Copy(intArr, arr, arr.Length);
                    break;
                case 1:
                    double[] doubleArr = arr.Cast<double>().ToArray();
                    GenericSortings<double>.CombSort(doubleArr, Comparer<double>.Default);
                    Array.Copy(doubleArr, arr, arr.Length);
                    break;
                case 2:
                    string[] stringArr = arr.Cast<string>().ToArray();
                    GenericSortings<string>.CombSort(stringArr, Comparer<string>.Default);
                    Array.Copy(stringArr, arr, arr.Length);
                    break;
            }
        }

        private void MergeSortWrapper(object[] arr)
        {
            switch (selectedDataType)
            {
                case 0:
                    int[] intArr = arr.Cast<int>().ToArray();
                    GenericSortings<int>.MergeSort(intArr, Comparer<int>.Default);
                    Array.Copy(intArr, arr, arr.Length);
                    break;
                case 1:
                    double[] doubleArr = arr.Cast<double>().ToArray();
                    GenericSortings<double>.MergeSort(doubleArr, Comparer<double>.Default);
                    Array.Copy(doubleArr, arr, arr.Length);
                    break;
                case 2:
                    string[] stringArr = arr.Cast<string>().ToArray();
                    GenericSortings<string>.MergeSort(stringArr, Comparer<string>.Default);
                    Array.Copy(stringArr, arr, arr.Length);
                    break;
            }
        }

        private void TreeSortWrapper(object[] arr)
        {
            switch (selectedDataType)
            {
                case 0:
                    int[] intArr = arr.Cast<int>().ToArray();
                    GenericSortings<int>.TreeSort(intArr, Comparer<int>.Default);
                    Array.Copy(intArr, arr, arr.Length);
                    break;
                case 1:
                    double[] doubleArr = arr.Cast<double>().ToArray();
                    GenericSortings<double>.TreeSort(doubleArr, Comparer<double>.Default);
                    Array.Copy(doubleArr, arr, arr.Length);
                    break;
                case 2:
                    string[] stringArr = arr.Cast<string>().ToArray();
                    GenericSortings<string>.TreeSort(stringArr, Comparer<string>.Default);
                    Array.Copy(stringArr, arr, arr.Length);
                    break;
            }
        }

        private void BuiltInSortWrapper(object[] arr)
        {
            // Используем встроенную сортировку для сравнения
            Array.Sort(arr);
        }

     
        private long MeasureSort(object[] originalArray, SortDelegate sortAction)
        {
            object[] testArray = (object[])originalArray.Clone();
            
            Stopwatch stopwatch = Stopwatch.StartNew();
            sortAction(testArray); 
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private double[][] InitializeResultsArray(int algorithmsCount)
        {
            double[][] results = new double[algorithmsCount][];
            for (int i = 0; i < algorithmsCount; ++i)
            {
                results[i] = new double[testData.Length];
            }
            return results;
        }

        private void CalculateAverageTimes(double[][] results, int testIterations)
        {
            for (int algorithm = 0; algorithm < results.Length; ++algorithm)
            {
                for (int sizeLevel = 0; sizeLevel < results[algorithm].Length; ++sizeLevel)
                {
                    results[algorithm][sizeLevel] /= (testData[sizeLevel].Length * testIterations);
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

            try
            {
                double[][] results = RunPerformanceTests();
                DisplayResultsOnGraph(results);
                ShowErrorMessage("Тестирование завершено успешно!", false);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка тестирования: {ex.Message}");
            }
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
                "Сортировка Шелла", "Пирамидальная сортировка", "Быстрая сортировка"
            };
            
            string[] group3Names = new string[] {
                "Сортировка расчёской", "Сортировка слиянием", "Встроенная сортировка", "Сортировка деревом"
            };
            
            string dataTypeName = dataTypeComboBox.SelectedItem?.ToString() ?? "Неизвестный тип";
            pane.Title.Text = $"Производительность обобщенных сортировок ({dataTypeName})";
            pane.XAxis.Title.Text = "Размер массива";
            pane.YAxis.Title.Text = "Время выполнения, мс";
            
            Color[] colors = new Color[] { Color.Blue, Color.Red, Color.Green, Color.Purple, Color.Orange, Color.Brown, Color.Pink };
            
            switch (selectedSortGroup)
            {
                case 1:
                    for (int i = 0; i < Math.Min(5, results.Length); i++)
                        AddCurveToGraph(pane, results[i], group1Names[i], colors[i]);
                    break;
                case 2:
                    for (int i = 0; i < Math.Min(3, results.Length); i++)
                        AddCurveToGraph(pane, results[i], group2Names[i], colors[i]);
                    break;
                case 3:
                    for (int i = 0; i < Math.Min(4, results.Length); i++)
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

            try
            {
                using (StreamWriter writer = new StreamWriter("comparison_results.txt", false))
                {
                    SaveComparisonResults(writer);
                }
                
                ShowErrorMessage("Результаты сохранены в comparison_results.txt!", false);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка сохранения: {ex.Message}");
            }
        }

        private void SaveComparisonResults(StreamWriter writer)
        {
            writer.WriteLine("СРАВНЕНИЕ ПРОИЗВОДИТЕЛЬНОСТИ ОБОБЩЕННЫХ СОРТИРОВОК");
            writer.WriteLine("===================================================");
            writer.WriteLine($"Тип данных: {dataTypeComboBox.SelectedItem}");
            writer.WriteLine($"Тип теста: {comboBoxOfTests.SelectedItem}");
            writer.WriteLine($"Группа сортировок: {comboBoxOfSorts.SelectedItem}");
            writer.WriteLine($"Время тестирования: {DateTime.Now}");
            writer.WriteLine();
            writer.WriteLine("ОБОБЩЕННЫЕ СОРТИРОВКИ:");
            writer.WriteLine("- Работают с любыми типами данных через IComparer<T>");
            writer.WriteLine("- Используют делегаты для гибкого вызова алгоритмов");
            writer.WriteLine("- Показывают сравнимую производительность со специализированными версиями");
            writer.WriteLine();
            writer.WriteLine("ИСПОЛЬЗУЕМЫЕ ДЕЛЕГАТЫ:");
            writer.WriteLine("- SortDelegate для универсального вызова сортировок");
            writer.WriteLine("- Каждая сортировка вызывается через делегатную обертку");
            writer.WriteLine("- Делегаты обеспечивают единый интерфейс для всех алгоритмов");
        }
    }
}