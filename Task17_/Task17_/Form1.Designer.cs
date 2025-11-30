namespace Task17_

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
            SuspendLayout();
            // 
            // zedGraphControl
            // 
            zedGraphControl.Location = new Point(37, 14);
            zedGraphControl.Margin = new Padding(4, 5, 4, 5);
            zedGraphControl.Name = "zedGraphControl";
            zedGraphControl.ScrollGrace = 0D;
            zedGraphControl.ScrollMaxX = 0D;
            zedGraphControl.ScrollMaxY = 0D;
            zedGraphControl.ScrollMaxY2 = 0D;
            zedGraphControl.ScrollMinX = 0D;
            zedGraphControl.ScrollMinY = 0D;
            zedGraphControl.ScrollMinY2 = 0D;
            zedGraphControl.Size = new Size(1344, 659);
            zedGraphControl.TabIndex = 0;
            zedGraphControl.UseExtendedPrintDialog = true;
            // 
            // Graphic
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1394, 703);
            Controls.Add(zedGraphControl);
            Name = "Graphic";
            Text = "Динамический массив и двунаправленный список";
            ResumeLayout(false);
        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl;
    }
}