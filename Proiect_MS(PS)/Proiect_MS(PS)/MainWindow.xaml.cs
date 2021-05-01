﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Proiect_MS_PS_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Viewbox[] tabsVB;
        String pythonPath;
        String scriptPath;
        public MainWindow()
        {
            InitializeComponent();

            tabsVB = new Viewbox[] { bernVB, unifVB, geomVB, expVB, exitVB };
            pythonPath = getPythonPath();
            scriptPath = getScriptPath();
        }

        private String getPythonPath()
        {
            String[] Paths = Environment.GetEnvironmentVariable("Path").Split(';');
            foreach(String p in Paths)
            {
                if (p.Contains("Python") && !p.Contains("Scripts")) return (p + "python.exe");
            }

            return null;
        }
        private String getScriptPath()
        {
            String appFolderPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            String scriptsFolderPath = Path.Combine(Directory.GetParent(appFolderPath).Parent.FullName, "Scripts");
            return Path.Combine(scriptsFolderPath, @"main.py");
        }
        public String RunScript(String args)
        {
            Process process = new Process();            
            process.StartInfo = new ProcessStartInfo()
            {
                FileName = pythonPath,
                Arguments = scriptPath + " " + args,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            process.Start();

            StreamReader reader = process.StandardOutput;
            String result = reader.ReadToEnd();
            process.WaitForExit();
            return result;
        }

        private void setNewSize()
        {
            double newSize = (this.Width * .9) / 5 - 16;
            foreach (Viewbox v in tabsVB)
            {
                v.MaxWidth = newSize;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            setNewSize();
        }


        private void distrTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem selectedTab = (TabItem)((TabControl)sender).SelectedItem;


            if(selectedTab == bernTab)
            {
                setChart(bernChart);
                bernItTB.Text = "1";
                bernPbTB.Text = "0.5";
            }
            if(selectedTab == unifTab)
            {
                setChart(unifChart);
                unifItTB.Text = "1";
                bernPbTB.Text = "0.1";
            }
            if(selectedTab == geomTab)
            {
                setChart(geomChart);
                geomItTB.Text = "1";
                geomPbTB.Text = "0.1";
            }
            if(selectedTab == expTab)
            {
                setChart(expChart);
                expItTB.Text = "1";
                expPbTB.Text = "0.5";
            }
            if (selectedTab == exitTab)
            {
                this.Close();
            }
        }

        private void setChart(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            Dictionary<string, double> value;
            value = new Dictionary<string, double>();            
            chart.DataSource = value;
            chart.Series["series"].XValueMember = "Key";
            chart.Series["series"].YValueMembers = "Value";

            chart.ChartAreas.Clear();
            chart.ChartAreas.Add("MainChartArea");
        }

        private void addValue(System.Windows.Forms.DataVisualization.Charting.Chart chart, String[] values)
        {
            if(chart.DataSource==null)
            {
                MessageBox.Show("canci");
                return;
            }
            Dictionary<string, double> value = (Dictionary<string, double>)chart.DataSource;

            foreach(string s in values)
            {
                if (String.IsNullOrWhiteSpace(s)) break;
                if (value.ContainsKey(s)) value[s]++;
                else value.Add(s, 1);
            }

            chart.DataSource = value;
            chart.DataBind();
            chart.Update();
        }

        private void bernGenBT_Click(object sender, RoutedEventArgs e)
        {
            int iterations;
            double p;

            if(!int.TryParse(bernItTB.Text, out iterations) || iterations<1)
            {
                MessageBox.Show("Introduceți un număr valid de iterații!");
                bernItTB.Text = "1";
                return;
            }

            if(!double.TryParse(bernPbTB.Text, out p) || !(p>0 && p<1))
            {
                MessageBox.Show("Introduceți o probabilitate validă! (Număr  în intervalul (0, 1) )");
                bernPbTB.Text = "0.5";
                return;
            }

            String[] values = RunScript(iterations.ToString() + " 0 " + p.ToString()).Split('\n');
            addValue(bernChart, values);
        }
        private void bernResetBT_Click(object sender, RoutedEventArgs e)
        {
            setChart(bernChart);          
            
        }

        private void unifGetBT_Click(object sender, RoutedEventArgs e)
        {
            int iterations;
            int a, b;

            if (!int.TryParse(unifItTB.Text, out iterations) || iterations < 1)
            {
                MessageBox.Show("Introduceți un număr valid de iterații!");
                bernItTB.Text = "1";
                return;
            }

            if(!int.TryParse(unifLowerTB.Text, out a))
            {
                MessageBox.Show("Introduceți un număr valid pentru limita inferioară a intervalului!");
                unifLowerTB.Text = "1";
                unifLowerTB.Focus();
                return;
            }

            if (!int.TryParse(unifUpperTB.Text, out b))
            {
                MessageBox.Show("Introduceți un număr valid pentru limita superioară a intervalului!");
                unifUpperTB.Text = "10";
                unifUpperTB.Focus();
                return;
            }

            if(a>=b)
            {
                MessageBox.Show("Introduceți un interval valid (a<b) !");
                unifLowerTB.Text = "1";
                unifUpperTB.Text = "10";
                return;
            }

            String[] values = RunScript(iterations.ToString() + " 1 " + a.ToString() + " " + b.ToString()).Split('\n');
            addValue(unifChart, values);
        }

        private void uinifResetBT_Click(object sender, RoutedEventArgs e)
        {
            setChart(unifChart);
        }

        private void geomGetBT_Click(object sender, RoutedEventArgs e)
        {
            int iterations;
            double p;

            if (!int.TryParse(geomItTB.Text, out iterations) || iterations < 1)
            {
                MessageBox.Show("Introduceți un număr valid de iterații!");
                geomItTB.Text = "1";
                return;
            }

            if (!double.TryParse(geomPbTB.Text, out p) || !(p > 0 && p < 1))
            {
                MessageBox.Show("Introduceți o probabilitate validă! (Număr  în intervalul (0, 1) )");
                geomPbTB.Text = "0.5";
                return;
            }

            String[] values = RunScript(iterations.ToString() + " 2 " + p.ToString()).Split('\n');
            addValue(geomChart, values);
        }

        private void geomfResetBT_Click(object sender, RoutedEventArgs e)
        {
            setChart(geomChart);
        }

        private void expGetBT_Click(object sender, RoutedEventArgs e)
        {
            int iterations;
            double p;

            if (!int.TryParse(expItTB.Text, out iterations) || iterations < 1)
            {
                MessageBox.Show("Introduceți un număr valid de iterații!");
                expItTB.Text = "1";
                return;
            }

            if (!double.TryParse(expPbTB.Text, out p) || !(p > 0))
            {
                MessageBox.Show("Introduceți o probabilitate validă! (Număr pozitiv)");
                expPbTB.Text = "0.5";
                return;
            }

            String[] values = RunScript(iterations.ToString() + " 3 " + p.ToString()).Split('\n');
            addValue(expChart, values);
        }

        private void expfResetBT_Click(object sender, RoutedEventArgs e)
        {
            setChart(expChart);
        }
    }
}
