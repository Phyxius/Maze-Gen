namespace MazeGenSolve
{
    partial class SettingsDialog
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
            this.numericUpDownFPS = new System.Windows.Forms.NumericUpDown();
            this.trackBarFPS = new System.Windows.Forms.TrackBar();
            this.labelFPS = new System.Windows.Forms.Label();
            this.trackBarWaitTime = new System.Windows.Forms.TrackBar();
            this.numericUpDownWaitTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxShowGeneration = new System.Windows.Forms.CheckBox();
            this.checkBoxShowSolving = new System.Windows.Forms.CheckBox();
            this.checkBoxShowFPS = new System.Windows.Forms.CheckBox();
            this.checkBoxVSync = new System.Windows.Forms.CheckBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWaitTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaitTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownFPS
            // 
            this.numericUpDownFPS.Location = new System.Drawing.Point(300, 12);
            this.numericUpDownFPS.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownFPS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFPS.Name = "numericUpDownFPS";
            this.numericUpDownFPS.Size = new System.Drawing.Size(56, 20);
            this.numericUpDownFPS.TabIndex = 0;
            this.numericUpDownFPS.ThousandsSeparator = true;
            this.numericUpDownFPS.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownFPS.ValueChanged += new System.EventHandler(this.numericUpDownFPS_ValueChanged);
            // 
            // trackBarFPS
            // 
            this.trackBarFPS.LargeChange = 50;
            this.trackBarFPS.Location = new System.Drawing.Point(12, 12);
            this.trackBarFPS.Maximum = 1000;
            this.trackBarFPS.Minimum = 1;
            this.trackBarFPS.Name = "trackBarFPS";
            this.trackBarFPS.Size = new System.Drawing.Size(282, 45);
            this.trackBarFPS.SmallChange = 10;
            this.trackBarFPS.TabIndex = 1;
            this.trackBarFPS.TickFrequency = 50;
            this.trackBarFPS.Value = 1;
            this.trackBarFPS.Scroll += new System.EventHandler(this.trackBarFPS_Scroll);
            // 
            // labelFPS
            // 
            this.labelFPS.AutoSize = true;
            this.labelFPS.Location = new System.Drawing.Point(409, 14);
            this.labelFPS.Name = "labelFPS";
            this.labelFPS.Size = new System.Drawing.Size(109, 13);
            this.labelFPS.TabIndex = 2;
            this.labelFPS.Text = "Iterations Per Second";
            // 
            // trackBarWaitTime
            // 
            this.trackBarWaitTime.Location = new System.Drawing.Point(12, 50);
            this.trackBarWaitTime.Name = "trackBarWaitTime";
            this.trackBarWaitTime.Size = new System.Drawing.Size(282, 45);
            this.trackBarWaitTime.TabIndex = 3;
            this.trackBarWaitTime.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarWaitTime.Scroll += new System.EventHandler(this.trackBarWaitTime_Scroll);
            // 
            // numericUpDownWaitTime
            // 
            this.numericUpDownWaitTime.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownWaitTime.Location = new System.Drawing.Point(300, 50);
            this.numericUpDownWaitTime.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownWaitTime.Name = "numericUpDownWaitTime";
            this.numericUpDownWaitTime.Size = new System.Drawing.Size(56, 20);
            this.numericUpDownWaitTime.TabIndex = 4;
            this.numericUpDownWaitTime.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownWaitTime.ValueChanged += new System.EventHandler(this.numericUpDownWaitTime_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(380, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Wait Time Between Phases";
            // 
            // checkBoxShowGeneration
            // 
            this.checkBoxShowGeneration.AutoSize = true;
            this.checkBoxShowGeneration.Location = new System.Drawing.Point(22, 88);
            this.checkBoxShowGeneration.Name = "checkBoxShowGeneration";
            this.checkBoxShowGeneration.Size = new System.Drawing.Size(107, 17);
            this.checkBoxShowGeneration.TabIndex = 6;
            this.checkBoxShowGeneration.Text = "Show Generation";
            this.checkBoxShowGeneration.UseVisualStyleBackColor = true;
            this.checkBoxShowGeneration.CheckedChanged += new System.EventHandler(this.checkBoxShowGeneration_CheckedChanged);
            // 
            // checkBoxShowSolving
            // 
            this.checkBoxShowSolving.AutoSize = true;
            this.checkBoxShowSolving.Location = new System.Drawing.Point(137, 88);
            this.checkBoxShowSolving.Name = "checkBoxShowSolving";
            this.checkBoxShowSolving.Size = new System.Drawing.Size(90, 17);
            this.checkBoxShowSolving.TabIndex = 7;
            this.checkBoxShowSolving.Text = "Show Solving";
            this.checkBoxShowSolving.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowFPS
            // 
            this.checkBoxShowFPS.AutoSize = true;
            this.checkBoxShowFPS.Location = new System.Drawing.Point(235, 88);
            this.checkBoxShowFPS.Name = "checkBoxShowFPS";
            this.checkBoxShowFPS.Size = new System.Drawing.Size(75, 17);
            this.checkBoxShowFPS.TabIndex = 8;
            this.checkBoxShowFPS.Text = "Show FPS";
            this.checkBoxShowFPS.UseVisualStyleBackColor = true;
            // 
            // checkBoxVSync
            // 
            this.checkBoxVSync.AutoSize = true;
            this.checkBoxVSync.Location = new System.Drawing.Point(318, 88);
            this.checkBoxVSync.Name = "checkBoxVSync";
            this.checkBoxVSync.Size = new System.Drawing.Size(123, 17);
            this.checkBoxVSync.TabIndex = 9;
            this.checkBoxVSync.Text = "Enable Vertical Sync";
            this.checkBoxVSync.UseVisualStyleBackColor = true;
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Foreground",
            "Background",
            "Start",
            "End",
            "Checked",
            "Path",
            "Current Cell"});
            this.listBox1.Location = new System.Drawing.Point(135, 141);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 10;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(281, 141);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(93, 66);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(281, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Change Color";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(209, 278);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Run Screensaver";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 313);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.checkBoxVSync);
            this.Controls.Add(this.checkBoxShowFPS);
            this.Controls.Add(this.checkBoxShowSolving);
            this.Controls.Add(this.checkBoxShowGeneration);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownWaitTime);
            this.Controls.Add(this.trackBarWaitTime);
            this.Controls.Add(this.labelFPS);
            this.Controls.Add(this.trackBarFPS);
            this.Controls.Add(this.numericUpDownFPS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Maze Screensaver Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsDialog_FormClosed);
            this.Load += new System.EventHandler(this.SettingsDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWaitTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaitTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownFPS;
        private System.Windows.Forms.TrackBar trackBarFPS;
        private System.Windows.Forms.Label labelFPS;
        private System.Windows.Forms.TrackBar trackBarWaitTime;
        private System.Windows.Forms.NumericUpDown numericUpDownWaitTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxShowGeneration;
        private System.Windows.Forms.CheckBox checkBoxShowSolving;
        private System.Windows.Forms.CheckBox checkBoxShowFPS;
        private System.Windows.Forms.CheckBox checkBoxVSync;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}