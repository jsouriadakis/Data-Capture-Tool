namespace Camera_Record
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lineChart3 = new MindFusion.Charting.WinForms.LineChart();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.selectionRangeSlider2 = new Camera_Record.SelectionRangeSlider();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(12, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1920, 1080);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // lineChart3
            // 
            this.lineChart3.AllowMoveLegend = false;
            this.lineChart3.AllowPan = false;
            this.lineChart3.AllowZoom = false;
            this.lineChart3.BackColor = System.Drawing.SystemColors.GrayText;
            this.lineChart3.ForeColor = System.Drawing.Color.Transparent;
            this.lineChart3.LegendHorizontalAlignment = MindFusion.Charting.Components.LayoutAlignment.Far;
            this.lineChart3.LegendTitle = "Legend";
            this.lineChart3.Location = new System.Drawing.Point(1938, 12);
            this.lineChart3.Name = "lineChart3";
            this.lineChart3.Padding = new System.Windows.Forms.Padding(5);
            this.lineChart3.ShowDataLabels = MindFusion.Charting.LabelKinds.YAxisLabel;
            this.lineChart3.ShowLegend = true;
            this.lineChart3.ShowLegendTitle = false;
            this.lineChart3.ShowToolTips = false;
            this.lineChart3.ShowXCoordinates = false;
            this.lineChart3.ShowXTicks = false;
            this.lineChart3.Size = new System.Drawing.Size(1866, 1134);
            this.lineChart3.SubtitleFontName = null;
            this.lineChart3.SubtitleFontSize = null;
            this.lineChart3.SubtitleFontStyle = null;
            this.lineChart3.TabIndex = 6;
            this.lineChart3.Text = "lineChart3";
            this.lineChart3.Theme.AxisLabelsBrush = new MindFusion.Drawing.SolidBrush("#FFFFFFFF");
            this.lineChart3.Theme.AxisStroke = new MindFusion.Drawing.SolidBrush("#FFFFFFFF");
            this.lineChart3.Theme.AxisTitleBrush = new MindFusion.Drawing.SolidBrush("#FFFFFFFF");
            this.lineChart3.Theme.DataLabelsBrush = new MindFusion.Drawing.SolidBrush("#FFFFFFFF");
            this.lineChart3.Theme.GaugeTickStroke = new MindFusion.Drawing.SolidBrush("#FFFFFFFF");
            this.lineChart3.Theme.SubtitleBrush = new MindFusion.Drawing.SolidBrush("#FFFFFFFF");
            this.lineChart3.Theme.UniformSeriesFill = new MindFusion.Drawing.SolidBrush("#FF90EE90");
            this.lineChart3.Theme.UniformSeriesStroke = new MindFusion.Drawing.SolidBrush("#FF000000");
            this.lineChart3.Theme.UniformSeriesStrokeThickness = 2D;
            this.lineChart3.TitleFontName = null;
            this.lineChart3.TitleFontSize = null;
            this.lineChart3.TitleFontStyle = null;
            this.lineChart3.XAxisTickLength = 0D;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(67, 1140);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(3759, 136);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "*.bin";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1512, 1420);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(908, 207);
            this.button1.TabIndex = 5;
            this.button1.Text = "SelectFile";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Location = new System.Drawing.Point(1512, 1676);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(908, 329);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "USER";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(494, 106);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(167, 44);
            this.textBox5.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(544, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 37);
            this.label4.TabIndex = 16;
            this.label4.Text = "Weight";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 37);
            this.label3.TabIndex = 15;
            this.label3.Text = "Height";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(429, 213);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(232, 44);
            this.textBox3.TabIndex = 14;
            // 
            // textBox2
            // 
            this.textBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textBox2.Location = new System.Drawing.Point(167, 213);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(232, 44);
            this.textBox2.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(544, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 37);
            this.label2.TabIndex = 11;
            this.label2.Text = "Gender";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 37);
            this.label1.TabIndex = 10;
            this.label1.Text = "UserID";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(167, 106);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(300, 44);
            this.textBox4.TabIndex = 8;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(1512, 1335);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(908, 44);
            this.textBox6.TabIndex = 20;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(940, 1335);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(509, 292);
            this.textBox1.TabIndex = 22;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3600, 1335);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(189, 670);
            this.button2.TabIndex = 23;
            this.button2.Text = "Play";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(940, 1818);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(509, 80);
            this.button4.TabIndex = 25;
            this.button4.Text = "Export Selected To CSV";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 37;
            this.listBox1.Location = new System.Drawing.Point(51, 1335);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(829, 670);
            this.listBox1.TabIndex = 26;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(940, 1666);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(320, 111);
            this.button3.TabIndex = 27;
            this.button3.Text = "Modify Selected Action";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(305, 1285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(279, 37);
            this.label5.TabIndex = 28;
            this.label5.Text = "Selectable Actions";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1287, 1666);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(162, 111);
            this.button5.TabIndex = 29;
            this.button5.Text = "Save Modified";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(1218, 1927);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(231, 78);
            this.button6.TabIndex = 30;
            this.button6.Text = "Export Video";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Enabled = false;
            this.progressBar1.Location = new System.Drawing.Point(102, 1209);
            this.progressBar1.Maximum = 1000;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(3687, 67);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 32;
            this.progressBar1.Visible = false;
            // 
            // selectionRangeSlider2
            // 
            this.selectionRangeSlider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.selectionRangeSlider2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.selectionRangeSlider2.Location = new System.Drawing.Point(102, 1209);
            this.selectionRangeSlider2.Max = 100;
            this.selectionRangeSlider2.Min = 0;
            this.selectionRangeSlider2.Name = "selectionRangeSlider2";
            this.selectionRangeSlider2.SelectedMax = 100;
            this.selectionRangeSlider2.SelectedMin = 0;
            this.selectionRangeSlider2.Size = new System.Drawing.Size(3687, 67);
            this.selectionRangeSlider2.TabIndex = 31;
            this.selectionRangeSlider2.Value = 50;
            this.selectionRangeSlider2.SelectionChanged += new System.EventHandler(this.selectionRangeSlider2_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(3840, 2180);
            this.panel1.TabIndex = 33;
            this.panel1.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Enabled = false;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(103, 1134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(204, 73);
            this.label6.TabIndex = 34;
            this.label6.Text = "label6";
            this.label6.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(940, 1947);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(244, 41);
            this.checkBox1.TabIndex = 35;
            this.checkBox1.Text = "Compressed";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(3840, 2080);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.selectionRangeSlider2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.lineChart3);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.checkBox1);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.MaximumSize = new System.Drawing.Size(4096, 2304);
            this.MinimumSize = new System.Drawing.Size(1920, 1080);
            this.Name = "Form2";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DUMAGUS Data Player";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox3;
        private MindFusion.Charting.WinForms.LineChart lineChart3;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private SelectionRangeSlider selectionRangeSlider2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}