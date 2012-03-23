using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MazeGenSolve.Properties;

namespace MazeGenSolve
{
    public partial class SettingsDialog : Form
    {
        private Color c = Color.Black;
        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void trackBarFPS_Scroll(object sender, EventArgs e)
        {
            numericUpDownFPS.Value = ((TrackBar) sender).Value;
            Settings.Default.Framerate = (double)(1/numericUpDownFPS.Value);
        }

        private void numericUpDownFPS_ValueChanged(object sender, EventArgs e)
        {
            trackBarFPS.Value = (int)((NumericUpDown) sender).Value;
        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {
            numericUpDownFPS.Value = (decimal)(1/Settings.Default.Framerate);
            numericUpDownWaitTime.Value = (decimal) (Settings.Default.WaitTime);
            checkBoxShowFPS.Checked = Settings.Default.ShowFPS;
            checkBoxShowGeneration.Checked = Settings.Default.ShowGeneration;
            checkBoxShowSolving.Checked = Settings.Default.ShowSolving;
            checkBoxVSync.Checked = Settings.Default.VerticalSync;
            listBox1.SelectedIndex = 0;

        }

        private void SettingsDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void updateColorPreview()
        {
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    c = Settings.Default.ForegroundColor;
                    break;
                case 1:
                    c = Settings.Default.BackgroundColor;
                    break;
                case 2:
                    c = Settings.Default.BeginCellColor;
                    break;
                case 3:
                    c = Settings.Default.EndCellColor;
                    break;
                case 4:
                    c = Settings.Default.VisitedCellColor;
                    break;
                case 5:
                    c = Settings.Default.PathColor;
                    break;
                case 6:
                    c = Settings.Default.CurrentCellColor;
                    break;
                default:
                    throw new Exception("Error in color update");
            }
            var color = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for(int i = 0; i < color.Width; i++)
                for(int j = 0; j < color.Height; j++)
                    color.SetPixel(i, j, c);
            pictureBox1.Image = color;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateColorPreview();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = c;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                c = colorDialog1.Color;
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    Settings.Default.ForegroundColor = c;
                    break;
                case 1:
                    Settings.Default.BackgroundColor = c;
                    break;
                case 2:
                    Settings.Default.BeginCellColor = c;
                    break;
                case 3:
                    Settings.Default.EndCellColor = c;
                    break;
                case 4:
                    Settings.Default.VisitedCellColor = c;
                    break;
                case 5:
                    Settings.Default.PathColor = c;
                    break;
                case 6:
                    Settings.Default.CurrentCellColor = c;
                    break;
                default:
                    throw new Exception("Error in color update");
            }
            updateColorPreview();
        }

        private void trackBarWaitTime_Scroll(object sender, EventArgs e)
        {
            Settings.Default.WaitTime = trackBarWaitTime.Value;
            numericUpDownWaitTime.Value = trackBarWaitTime.Value;
        }

        private void numericUpDownWaitTime_ValueChanged(object sender, EventArgs e)
        {
            trackBarWaitTime.Value = (int)numericUpDownWaitTime.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
