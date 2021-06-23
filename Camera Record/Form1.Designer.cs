namespace Camera_Record
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.lineChart1 = new MindFusion.Charting.WinForms.LineChart();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(109, 164);
            this.button1.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(346, 77);
            this.button1.TabIndex = 1;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(2819, 975);
            this.button2.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(908, 153);
            this.button2.TabIndex = 3;
            this.button2.Text = "Start Recording";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(109, 90);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(346, 45);
            this.comboBox1.TabIndex = 4;
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
            this.videoSourcePlayer1.Location = new System.Drawing.Point(75, 1004);
            this.videoSourcePlayer1.Margin = new System.Windows.Forms.Padding(5);
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.Size = new System.Drawing.Size(2070, 1000);
            this.videoSourcePlayer1.TabIndex = 5;
            this.videoSourcePlayer1.Text = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
            this.videoSourcePlayer1.NewFrame += new AForge.Controls.VideoSourcePlayer.NewFrameHandler(this.videoSourcePlayer1_NewFrame_1);
            // 
            // lineChart1
            // 
            this.lineChart1.AllowMoveLegend = false;
            this.lineChart1.AllowPan = false;
            this.lineChart1.AllowZoom = false;
            this.lineChart1.BackColor = System.Drawing.SystemColors.GrayText;
            this.lineChart1.LegendHorizontalAlignment = MindFusion.Charting.Components.LayoutAlignment.Far;
            this.lineChart1.LegendTitle = "Legend";
            this.lineChart1.Location = new System.Drawing.Point(30, 45);
            this.lineChart1.Name = "lineChart1";
            this.lineChart1.Padding = new System.Windows.Forms.Padding(5);
            this.lineChart1.ShowDataLabels = MindFusion.Charting.LabelKinds.YAxisLabel;
            this.lineChart1.ShowLegend = true;
            this.lineChart1.ShowLegendTitle = false;
            this.lineChart1.ShowToolTips = false;
            this.lineChart1.ShowXCoordinates = false;
            this.lineChart1.ShowXTicks = false;
            this.lineChart1.Size = new System.Drawing.Size(2532, 935);
            this.lineChart1.SubtitleFontName = null;
            this.lineChart1.SubtitleFontSize = null;
            this.lineChart1.SubtitleFontStyle = null;
            this.lineChart1.TabIndex = 6;
            this.lineChart1.Text = "lineChart1";
            this.lineChart1.Theme.AxisLabelsBrush = new MindFusion.Drawing.SolidBrush("#FFFFFFFF");
            this.lineChart1.Theme.AxisStroke = new MindFusion.Drawing.SolidBrush("#FFFFFFFF");
            this.lineChart1.Theme.AxisTitleBrush = new MindFusion.Drawing.SolidBrush("#FFFFFFFF");
            this.lineChart1.Theme.DataLabelsBrush = new MindFusion.Drawing.SolidBrush("#FFFFFFFF");
            this.lineChart1.Theme.GaugeTickStroke = new MindFusion.Drawing.SolidBrush("#FFFFFFFF");
            this.lineChart1.Theme.SubtitleBrush = new MindFusion.Drawing.SolidBrush("#FFFFFFFF");
            this.lineChart1.Theme.UniformSeriesFill = new MindFusion.Drawing.SolidBrush("#FF90EE90");
            this.lineChart1.Theme.UniformSeriesStroke = new MindFusion.Drawing.SolidBrush("#FF000000");
            this.lineChart1.Theme.UniformSeriesStrokeThickness = 2D;
            this.lineChart1.TitleFontName = null;
            this.lineChart1.TitleFontSize = null;
            this.lineChart1.TitleFontStyle = null;
            this.lineChart1.XAxisTickLength = 0D;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Control;
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(2819, 1154);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(268, 329);
            this.button3.TabIndex = 7;
            this.button3.Text = "Start Exercise";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(167, 106);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(300, 44);
            this.textBox1.TabIndex = 8;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(544, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 37);
            this.label2.TabIndex = 11;
            this.label2.Text = "Gender";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.comboBox2.Location = new System.Drawing.Point(490, 105);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(171, 45);
            this.comboBox2.TabIndex = 12;
            // 
            // textBox2
            // 
            this.textBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textBox2.Location = new System.Drawing.Point(167, 213);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(232, 44);
            this.textBox2.TabIndex = 13;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(429, 213);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(232, 44);
            this.textBox3.TabIndex = 14;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(544, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 37);
            this.label4.TabIndex = 16;
            this.label4.Text = "Weight";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(2819, 529);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(908, 329);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "USER";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Control;
            this.button4.Location = new System.Drawing.Point(483, 90);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(318, 151);
            this.button4.TabIndex = 19;
            this.button4.Text = "Output Directory";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.Control;
            this.button6.Location = new System.Drawing.Point(3143, 1154);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(268, 329);
            this.button6.TabIndex = 21;
            this.button6.Text = "Walking";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.Control;
            this.button5.Location = new System.Drawing.Point(3459, 1154);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(268, 329);
            this.button5.TabIndex = 22;
            this.button5.Text = "Nothing";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Location = new System.Drawing.Point(2819, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(908, 311);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "INITIALIZE";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.Control;
            this.button7.Location = new System.Drawing.Point(2881, 1515);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(144, 136);
            this.button7.TabIndex = 24;
            this.button7.Text = "Bad Form";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(3844, 2070);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lineChart1);
            this.Controls.Add(this.videoSourcePlayer1);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.MaximumSize = new System.Drawing.Size(4096, 2304);
            this.MinimumSize = new System.Drawing.Size(1920, 1080);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DUMAGUS Data Recorder";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer1;
        private MindFusion.Charting.WinForms.LineChart lineChart1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button7;
    }
}

