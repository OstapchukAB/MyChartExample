using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        Chart MyChart;
        //List<A> LstA = new List<A>();
        SortedSet<A> LstA = new SortedSet<A>(new ACompare());
        string[] NameSeries = {"Line","Point" };
        struct A {
            public int id { get; set; }
            public string v { get; set; }

            public DateTime dtm { get; set; }

            public A(int id, string v, DateTime dtm)
            {
                this.id = id;
                this.v = v;
                this.dtm = dtm;
            }
        }
        //Реализация сортировки структуры A
        class ACompare : Comparer<A>
        {
            public override int Compare(A x, A y)
            {
                if (object.Equals(x, y)) return 0;
                return x.dtm.CompareTo(y.dtm); 
            }
        }

        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LstA.Clear();
            MyChart.Series.Clear();  
            DateTime dtm = DateTime.Now;
            var rnd = new Random();
            for (int i = 0; i < 1000; i++) {
                var v = i.ToString("D2");
               
                var a = new A( rnd.Next(1,100), v, dtm.AddDays(rnd.Next(0, 1000)));
                LstA.Add(a);

            }
            //LstA.Sort(new ACompare());   


                   

            Series s = new Series(NameSeries[0]);
            s.Legend = "myLegend";
            s.ChartType = SeriesChartType.Line;
            s.Color = Color.Blue;

            Series sPoint = new Series(NameSeries[1]);
            sPoint.Legend = "myLegend";
            sPoint.ChartType = SeriesChartType.Point;
            sPoint.Color = Color.DarkBlue;    
            foreach (A a in LstA) 
            {
                s.Points.AddXY(a.dtm, a.id);
                sPoint.Points.AddXY(a.dtm, a.id);
            }
            MyChart.Series.Add(s);
            MyChart.Series.Add(sPoint);
            checkBoxLine.Checked = true;
            checkBoxPoint.Checked = true;  

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MyChart = new Chart();
            MyChart.Parent = groupBox1;
            MyChart.Dock = DockStyle.Fill;
            MyChart.DataSource = LstA;
            MyChart.ChartAreas.Add(new ChartArea("myChartArea"));
            MyChart.Legends.Add(new Legend("myLegend"));
            MyChart.ChartAreas[0].AxisY.Title = "Коэффициент";
            MyChart.ChartAreas[0].AxisX.Title = "Дата";
            
           

            checkBoxLine.Text = NameSeries[0];
            checkBoxPoint.Text = NameSeries[1];
            
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ch = (CheckBox)sender;
            Series s = MySeriesFind(ch.Text);
            if (s !=null)
            if (ch.Checked)
                s.Enabled = true;
            else
                s.Enabled = false;
           
        }

        private Series MySeriesFind(string nameSeries) {
            return MyChart.Series.FindByName(nameSeries);
           
        }

        private void Zoom_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxZoom.Checked)
            {
                MyChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                MyChart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
                MyChart.MouseWheel += MyChart_MouseWheel;
            }
            else {
               
                var xAxis = MyChart.ChartAreas[0].AxisX;
                var yAxis = MyChart.ChartAreas[0].AxisY;
                xAxis.ScaleView.ZoomReset();
                yAxis.ScaleView.ZoomReset();
                MyChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
                MyChart.ChartAreas[0].AxisY.ScaleView.Zoomable = false;
                MyChart.MouseWheel -= MyChart_MouseWheel;
            }
        }

        private void MyChart_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart = (Chart)sender;
            var xAxis = chart.ChartAreas[0].AxisX;
            var yAxis = chart.ChartAreas[0].AxisY;

            try
            {
                if (e.Delta < 0) // Scrolled down.
                {
                    xAxis.ScaleView.ZoomReset();
                    yAxis.ScaleView.ZoomReset();
                }
                else if (e.Delta > 0) // Scrolled up.
                {
                    var xMin = xAxis.ScaleView.ViewMinimum;
                    var xMax = xAxis.ScaleView.ViewMaximum;
                    var yMin = yAxis.ScaleView.ViewMinimum;
                    var yMax = yAxis.ScaleView.ViewMaximum;
                    double k =2;

                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / k;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / k;
                    var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / k;
                    var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / k;

                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                    yAxis.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }



        private void checkBoxMeasurementMode_CheckedChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("checkBoxMeasurementMode_CheckedChanged");
           

            if (checkBoxMeasurementMode.Checked)
            {
                MyChart.Cursor = Cursors.Cross;
                //labelPosCursor.BackColor = Color.White;
                MyChart.MouseMove += new System.Windows.Forms.MouseEventHandler(myChart_MouseMove);
            }
            else
            {
                MyChart.Cursor = Cursors.Default;
               // labelPosCursor.BackColor = default(Color);
                //labelPosCursor.Text = "";
                tooltip.RemoveAll();
                MyChart.MouseMove -= new System.Windows.Forms.MouseEventHandler(myChart_MouseMove);
            }
        }


        async private void myChart_MouseMove(object sender, MouseEventArgs e)
        {

            //labelPosCursor.BackColor = Color.White;
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var pX = pos.X;
            var pY = pos.Y;
            var show = "";

            await Task.Run(() =>
            {
                Action action = () =>
                {



                    var results = MyChart.HitTest(pX, pY, false, ChartElementType.DataPoint); // 
                    foreach (var result in results)
                    {
                        if (result.ChartElementType == ChartElementType.DataPoint) // 
                        {
                            var yVal = result.ChartArea.AxisY.PixelPositionToValue(pY);
                            var xVal = DateTime.FromOADate(result.ChartArea.AxisX.PixelPositionToValue(pX));
                            show = $"{xVal}, {(int)yVal}";
                            //labelPosCursor.Text = show;

                            //tooltip.Show(show, myChart, pX, pY - 20);
                        }
                    }
                };
                if (InvokeRequired)
                    BeginInvoke(action);
                else
                    action();
            });

            //labelPosCursor.Text = show;
            tooltip.Show(show, MyChart, pX, pY - 20);
        }

      
    }
}
