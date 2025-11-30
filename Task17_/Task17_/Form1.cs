namespace Task17_;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

public partial class Graphic : Form
{
    MyArrayList<int>[] array = new MyArrayList<int>[4];
    MyLinkedList<int>[] linked = new MyLinkedList<int>[4];
    
    public double[][][] MakeTests()
    {
        double[][][] result = new double[4][][];
        for (int i = 0; i < 4; ++i)
        {
            result[i] = new double[2][];
            for (int j = 0; j < 2; ++j)
                result[i][j] = new double[5];
        }

        Random rand = new Random();
        Stopwatch st;
        int el;
        
        Console.WriteLine("Начало тестирования...");

     
        for (int sizeIdx = 0; sizeIdx < 4; sizeIdx++)
        {
            long size = (long)Math.Pow(10, 3 + sizeIdx); 
            Console.WriteLine($"Тестирование Add для размера {size}");
            
            for (int k = 0; k < 5; ++k) 
            {
                array[sizeIdx] = new MyArrayList<int>();
                linked[sizeIdx] = new MyLinkedList<int>();
                
                for (int i = 0; i < size; ++i)
                {
                    el = rand.Next(1000);
                    
                    st = Stopwatch.StartNew();
                    array[sizeIdx].Add(el);
                    st.Stop();
                    result[sizeIdx][0][0] += st.ElapsedTicks;

                    st = Stopwatch.StartNew();
                    linked[sizeIdx].Add(el);
                    st.Stop();
                    result[sizeIdx][1][0] += st.ElapsedTicks;
                }
            }

            result[sizeIdx][0][0] /= (5 * size);
            result[sizeIdx][1][0] /= (5 * size);
        }
        
        for (int sizeIdx = 0; sizeIdx < 4; sizeIdx++)
        {
            long size = (long)Math.Pow(10, 3 + sizeIdx);
            Console.WriteLine($"Тестирование Get для размера {size}");
            
            
            array[sizeIdx] = new MyArrayList<int>();
            linked[sizeIdx] = new MyLinkedList<int>();
            for (int i = 0; i < size; ++i)
            {
                el = rand.Next(1000);
                array[sizeIdx].Add(el);
                linked[sizeIdx].Add(el);
            }

            for (int i = 0; i < Math.Min(size, 100); ++i) 
            {
                for (int k = 0; k < 5; ++k)
                {
                    el = rand.Next(array[sizeIdx].Size());
                    st = Stopwatch.StartNew();
                    int val = array[sizeIdx][el];
                    st.Stop();
                    result[sizeIdx][0][1] += st.ElapsedTicks;

                    el = rand.Next(linked[sizeIdx].Size());
                    st = Stopwatch.StartNew();
                    val = linked[sizeIdx][el];
                    st.Stop();
                    result[sizeIdx][1][1] += st.ElapsedTicks;
                }
            }

            result[sizeIdx][0][1] /= (5 * Math.Min(size, 100));
            result[sizeIdx][1][1] /= (5 * Math.Min(size, 100));
        }

     
        int setValue = 0;
        for (int sizeIdx = 0; sizeIdx < 4; sizeIdx++)
        {
            long size = (long)Math.Pow(10, 3 + sizeIdx);
            Console.WriteLine($"Тестирование Set для размера {size}");
            
            for (int i = 0; i < Math.Min(size, 100); ++i)
            {
                for (int k = 0; k < 5; ++k)
                {
                    el = rand.Next(array[sizeIdx].Size());
                    st = Stopwatch.StartNew();
                    array[sizeIdx][el] = setValue;
                    st.Stop();
                    result[sizeIdx][0][2] += st.ElapsedTicks;

                    el = rand.Next(linked[sizeIdx].Size());
                    st = Stopwatch.StartNew();
                    linked[sizeIdx][el] = setValue;
                    st.Stop();
                    result[sizeIdx][1][2] += st.ElapsedTicks;
                }
            }

            result[sizeIdx][0][2] /= (5 * Math.Min(size, 100));
            result[sizeIdx][1][2] /= (5 * Math.Min(size, 100));
        }

       
        for (int sizeIdx = 0; sizeIdx < 4; sizeIdx++)
        {
            long size = (long)Math.Pow(10, 3 + sizeIdx);
            Console.WriteLine($"Тестирование Insert для размера {size}");
            
            for (int i = 0; i < Math.Min(size, 50); ++i)
            {
                for (int k = 0; k < 5; ++k)
                {
                    int insertValue = rand.Next(1000);
                    el = rand.Next(array[sizeIdx].Size());
                    st = Stopwatch.StartNew();
                    array[sizeIdx].Add(el, insertValue);
                    st.Stop();
                    result[sizeIdx][0][3] += st.ElapsedTicks;

                    el = rand.Next(linked[sizeIdx].Size());
                    st = Stopwatch.StartNew();
                    linked[sizeIdx].Add(el, insertValue);
                    st.Stop();
                    result[sizeIdx][1][3] += st.ElapsedTicks;
                }
            }

            result[sizeIdx][0][3] /= (5 * Math.Min(size, 50));
            result[sizeIdx][1][3] /= (5 * Math.Min(size, 50));
        }

       
        for (int sizeIdx = 0; sizeIdx < 4; sizeIdx++)
        {
            long size = (long)Math.Pow(10, 3 + sizeIdx);
            Console.WriteLine($"Тестирование Remove для размера {size}");
            
            for (int i = 0; i < Math.Min(size, 50); ++i)
            {
                for (int k = 0; k < 5; ++k)
                {
                    
                    var tempArray = new MyArrayList<int>();
                    var tempLinked = new MyLinkedList<int>();
                    
                   
                    for (int j = 0; j < size; j++)
                    {
                        int value = rand.Next(1000);
                        tempArray.Add(value);
                        tempLinked.Add(value);
                    }
                    
                    
                    el = rand.Next(tempArray.Size() - 10) + 5; 
                    st = Stopwatch.StartNew();
                    tempArray.Remove(el); 
                    st.Stop();
                    result[sizeIdx][0][4] += st.ElapsedTicks;

                    el = rand.Next(tempLinked.Size() - 10) + 5;
                    st = Stopwatch.StartNew();
                    tempLinked.Remove(el);
                    st.Stop();
                    result[sizeIdx][1][4] += st.ElapsedTicks;
                }
            }

            result[sizeIdx][0][4] /= (5 * Math.Min(size, 50));
            result[sizeIdx][1][4] /= (5 * Math.Min(size, 50));
        }

        Console.WriteLine("Тестирование завершено");
        return result;
    }

    public void Draw()
    {
        try
        {
            GraphPane pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();
            pane.Title.Text = "Сравнение производительности структур данных";
            pane.XAxis.Title.Text = "Размер данных";
            pane.YAxis.Title.Text = "Время выполнения (тики)";

            PointPairList list;
            double[][][] tests = MakeTests();

            double[] sizes = { 1e3, 1e4, 1e5, 1e6}; 
            pane.XAxis.Type = AxisType.Log;
            pane.XAxis.Scale.Min = 1e3;
            pane.XAxis.Scale.Max = 1e6;

            Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Purple, Color.Orange };
            string[] operationNames = { "Add", "Get", "Set", "Insert", "Remove" };

            for (int op = 0; op < 5; op++)
            {
                list = new PointPairList();
                for (int sizeIdx = 0; sizeIdx < 4; sizeIdx++)
                {
                    if (tests[sizeIdx][0][op] > 0) 
                        list.Add(sizes[sizeIdx], tests[sizeIdx][0][op]);
                }
                if (list.Count > 0)
                    pane.AddCurve($"ArrayList {operationNames[op]}", list, colors[op], SymbolType.Circle);
            }

            for (int op = 0; op < 5; op++)
            {
                list = new PointPairList();
                for (int sizeIdx = 0; sizeIdx < 4; sizeIdx++)
                {
                    if (tests[sizeIdx][1][op] > 0) 
                        list.Add(sizes[sizeIdx], tests[sizeIdx][1][op]);
                }
                if (list.Count > 0)
                    pane.AddCurve($"LinkedList {operationNames[op]}", list, colors[op], SymbolType.Square);
            }

            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при построении графиков: {ex.Message}");
        }
    }

    public Graphic()
    {
        InitializeComponent();
        Draw();
    }
}