namespace Task4
{
    partial class Graphic
    {
        private System.ComponentModel.IContainer components = null;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            zedGraphControl = new ZedGraph.ZedGraphControl();
            comboBoxOfTests = new ComboBox();
            comboBoxOfSorts = new ComboBox();
            generateButton = new Button();
            runButton = new Button();
            saveButton = new Button();
            SuspendLayout();
            
            // 
            // zedGraphControl
            // 
            zedGraphControl.Location = new Point(391, 35);
            zedGraphControl.Margin = new Padding(4, 5, 4, 5);
            zedGraphControl.Name = "zedGraphControl";
            zedGraphControl.ScrollGrace = 0D;
            zedGraphControl.ScrollMaxX = 0D;
            zedGraphControl.ScrollMaxY = 0D;
            zedGraphControl.ScrollMaxY2 = 0D;
            zedGraphControl.ScrollMinX = 0D;
            zedGraphControl.ScrollMinY = 0D;
            zedGraphControl.ScrollMinY2 = 0D;
            zedGraphControl.Size = new Size(820, 564);
            zedGraphControl.TabIndex = 0;
            zedGraphControl.UseExtendedPrintDialog = true;
            zedGraphControl.GraphPane.Title.Text = "Зависимость времени выполнения сортировок от размера массива";
            zedGraphControl.GraphPane.XAxis.Title.Text = "Размер массива";
            zedGraphControl.GraphPane.YAxis.Title.Text = "Время выполнения, мс";
            
            // 
            // comboBoxOfTests
            // 
            comboBoxOfTests.FormattingEnabled = true;
            comboBoxOfTests.Items.AddRange(new object[] { 
                "Случайные числа", 
                "Разбитые на подмассивы", 
                "Отсортированные массивы", 
                "Смешанные массивы" 
            });
            comboBoxOfTests.Location = new Point(40, 115);
            comboBoxOfTests.Name = "comboBoxOfTests";
            comboBoxOfTests.Size = new Size(300, 28);
            comboBoxOfTests.TabIndex = 1;
            comboBoxOfTests.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOfTests.SelectedIndexChanged += comboBoxOfTests_SelectedIndexChanged;
            
            // 
            // comboBoxOfSorts
            // 
            comboBoxOfSorts.FormattingEnabled = true;
            comboBoxOfSorts.Items.AddRange(new object[] { 
                "Первая группа", 
                "Вторая группа", 
                "Третья группа" 
            });
            comboBoxOfSorts.Location = new Point(40, 40);
            comboBoxOfSorts.Name = "comboBoxOfSorts";
            comboBoxOfSorts.Size = new Size(300, 28);
            comboBoxOfSorts.TabIndex = 2;
            comboBoxOfSorts.DropDownStyle = ComboBoxStyle.DropDownList;
            
            // 
            // generateButton
            // 
            generateButton.Location = new Point(40, 440);
            generateButton.Name = "generateButton";
            generateButton.Size = new Size(300, 29);
            generateButton.TabIndex = 3;
            generateButton.Text = "Сгенерировать массивы";
            generateButton.UseVisualStyleBackColor = true;
            generateButton.Click += generate_Click;
            
            // 
            // runButton
            // 
            runButton.Location = new Point(40, 506);
            runButton.Name = "runButton";
            runButton.Size = new Size(300, 29);
            runButton.TabIndex = 4;
            runButton.Text = "Запустить тесты";
            runButton.UseVisualStyleBackColor = true;
            runButton.Click += run_Click;
            
            // 
            // saveButton
            // 
            saveButton.Location = new Point(40, 570);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(300, 29);
            saveButton.TabIndex = 5;
            saveButton.Text = "Сохранить результаты сравнения";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += save_Click;
            
            // 
            // Graphic
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1350, 649);
            Controls.Add(saveButton);
            Controls.Add(runButton);
            Controls.Add(generateButton);
            Controls.Add(comboBoxOfSorts);
            Controls.Add(comboBoxOfTests);
            Controls.Add(zedGraphControl);
            Name = "Graphic";
            Text = "Сравнение производительности обобщенных алгоритмов сортировки";
            ResumeLayout(false);
        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl;
        private ComboBox comboBoxOfTests;
        private ComboBox comboBoxOfSorts;
        private Button generateButton;
        private Button runButton;
        private Button saveButton;
    }
}