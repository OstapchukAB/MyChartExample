using MyChartExample.Properties;
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

        bool KeyCtrl = false;
        private double prevXMax = 0;
        private double prevXMin = 0;
        private double prevYMax = 0;
        private double prevYMin = 0;


        private Point mDown = Point.Empty;
        //первичное положение координат при загрузке
        double firstXMax=0, firstXMin=0, firstYMax=0, firstYMin=0;


        System.Windows.Forms.Cursor ZoomIn;
        System.Windows.Forms.Cursor ZoomOut;

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
            for (int i = 0; i < 500; i++) {
                var v = i.ToString("D2");
               
                var a = new A( rnd.Next(1,100), v, dtm.AddMinutes  (i));
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

            MyChart.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours ;
            MyChart.ChartAreas[0].AxisX.LabelStyle.Format = "dd HH:mm:ss";
            //MyChart.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Rotated270;    
            MyChart.ChartAreas[0].AxisX.Interval = 1;







            checkBoxLine.Text = NameSeries[0];
            checkBoxPoint.Text = NameSeries[1];
            this.KeyPreview = true;

            Bitmap BmpZoom_in = new Bitmap(new Bitmap(Resources.zoom_in), 32,32); 
            ZoomIn  = new System.Windows.Forms.Cursor(BmpZoom_in.GetHicon());

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
                //if (checkBoxPan.Checked)
                //{
                //    checkBoxPan.Checked = false;
                //    checkBoxPan_CheckedChanged(checkBoxPan, null);
                //}

                //MyChart.ChartAreas[0].CursorX.IsUserEnabled = false;         // red cursor at SelectionEnd
                //MyChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                //MyChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;      // zoom into SelectedRange
                //MyChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
                //MyChart.ChartAreas[0].CursorX.Interval = 0.001;               // set "resolution" of CursorX


                //MyChart.ChartAreas[0].CursorY.IsUserEnabled = false;         
                //MyChart.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
                //MyChart.ChartAreas[0].AxisY.ScaleView.Zoomable = false;     
                //MyChart.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
                //MyChart.ChartAreas[0].CursorY.Interval = 0.01;              



                //MyChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                //MyChart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
                //MyChart.MouseWheel += new System.Windows.Forms.MouseEventHandler(MyChart_MouseWheel);
                MyChart.KeyDown += MyChart_KeyDown;
                MyChart.KeyUp  += MyChart_KeyUp;
                //MyChart.Focus();  
                //MyChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                //MyChart.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
                //MyChart.MouseDoubleClick   += new MouseEventHandler(MyChart_ZoomReset);
               
                MyChart.SelectionRangeChanging += MyChart_SelectionRangeChanging;
                buttonZoomIn.Click += button_ZoomIn;
                buttonZoomOut.Click += button_ZoomOut; 

            }
            else {

                //MyChart_ZoomReset(null,null);
                MyChart.KeyDown -= MyChart_KeyDown;
                MyChart.KeyUp -= MyChart_KeyUp;
                MyChart.SelectionRangeChanging -= MyChart_SelectionRangeChanging;
                // MyChart.MouseDoubleClick -= new MouseEventHandler(MyChart_ZoomReset);
                //var xAxis = MyChart.ChartAreas[0].AxisX;
                //var yAxis = MyChart.ChartAreas[0].AxisY;
                //xAxis.ScaleView.ZoomReset();
                //yAxis.ScaleView.ZoomReset();
                //MyChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
                //MyChart.ChartAreas[0].AxisY.ScaleView.Zoomable = false;
                ////MyChart.MouseWheel -= new System.Windows.Forms.MouseEventHandler(MyChart_MouseWheel);
                //MyChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                //MyChart.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;

                MyChart.ChartAreas[0].CursorX.IsUserEnabled = false;         // red cursor at SelectionEnd
                MyChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                MyChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;      // zoom into SelectedRange
                MyChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = false;
                //MyChart.ChartAreas[0].CursorX.Interval = 0.001;
            }
        }

       

        private void MyChart_SelectionRangeChanging(object sender, CursorEventArgs e)
        {
            double x1 = e.NewSelectionStart; // or: chart1.ChartAreas[0].CursorX.SelectionStart;
            double x2 = e.NewSelectionEnd;        // or: x2 = chart1.ChartAreas[0].CursorX.SelectionEnd;
                  
            double diffx1x2 = x2 - x1;
            DateTime dtx1 =  DateTime.FromOADate(x1);
            DateTime dtx2 = DateTime.FromOADate(x2);
            label1.Text = $"x1={dtx1}, x2={dtx2}, delta={dtx2-dtx1}";  

        }

        private void button_ZoomIn(object sender, EventArgs e)
        {
            double x1 = MyChart.ChartAreas[0].CursorX.SelectionStart;  // x1 = X1
            double x2 = MyChart.ChartAreas[0].CursorX.SelectionEnd;    // x2 = X2

            if (x2 == x1)
                return;
            if (x2 > x1)
            {
                // hard setting: chart1.ChartAreas[0].AxisX.Minimum = x1;
                // hard setting: chart1.ChartAreas[0].AxisX.Maximum = x2;
                MyChart.ChartAreas[0].AxisX.ScaleView.Zoom(x1, x2); // dynamic approach with scrollbar
            }
            else
            {
                MyChart.ChartAreas[0].AxisX.ScaleView.Zoom(x2, x1);
            }
        }

        private void button_ZoomOut(object sender, EventArgs e)
        {
            MyChart.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            MyChart.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
        }


        void MyChart_KeyDown(object sender, KeyEventArgs e) {
            MyChart.Focus();  
            //var k = (Keys) sender;
            if (e.Control) {


                KeyCtrl = true;

                if (checkBoxZoom.Checked)
                {
                    MyChart.Cursor = ZoomIn;
                    MyChart.ChartAreas[0].CursorX.IsUserEnabled = false;         // red cursor at SelectionEnd
                    MyChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                    MyChart.ChartAreas[0].AxisX.ScaleView.Zoomable = false;      // zoom into SelectedRange
                    MyChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
                    MyChart.ChartAreas[0].CursorX.Interval = 0.001;
                }
                //else {

                //    MyChart.Cursor =Cursors.Default ;
                //       // red cursor at SelectionEnd
                //    MyChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
        
                //    MyChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = false;
     

                //}
            }
        }

        void MyChart_KeyUp(object sender, KeyEventArgs e)
        {

            //var k = (Keys) sender;
            if (!e.Control)
            {
                KeyCtrl = false;
                if (checkBoxPan.Checked)
                    MyChart.Cursor = Cursors.Hand;
                else if (checkBoxMeasurementMode.Checked)
                    MyChart.Cursor = Cursors.Cross;
                else if (checkBoxZoom.Checked) {

                    MyChart.Cursor = Cursors.Default;
                    // red cursor at SelectionEnd
                    MyChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;

                    MyChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = false;

                }

                else
                    MyChart.Cursor = Cursors.Default;
            }
        }


        private void MyChart_ZoomReset(object sender, MouseEventArgs e) {

            var xAxis = MyChart.ChartAreas[0].AxisX;
            var yAxis = MyChart.ChartAreas[0].AxisY;
            xAxis.ScaleView.ZoomReset();
            yAxis.ScaleView.ZoomReset();

            //MyChart.ChartAreas[0].AxisX.Maximum = firstXMax;
            //MyChart.ChartAreas[0].AxisX.Minimum = firstXMin;
            //MyChart.ChartAreas[0].AxisY.Maximum = firstYMax;
            //MyChart.ChartAreas[0].AxisY.Minimum = firstYMin;
            //MyChart.Update();


        }

        private void MyChart_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!KeyCtrl)
                return;

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
                    double k = 2;

                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / k;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / k;
                    var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / k;
                    var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / k;

                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                    yAxis.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
            MyChart.Focus();  
        }



        private void checkBoxMeasurementMode_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPan.Checked) {
                checkBoxPan.Checked = false;
                checkBoxPan_CheckedChanged(checkBoxPan, null);



            }
           

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
            if (KeyCtrl)
                return;
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
                            show = $"{xVal} : {Math.Round((double)yVal,2)}";
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

        private void checkBoxBlackTheme_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBlackTheme.Checked)
            {
                MyChart.BackColor = Color.Gray;
                MyChart.ChartAreas[0].BackColor = Color.LightGray;

                //MyChart.ChartAreas[0].AxisY.LineColor = Color.White;  
                //MyChart.ChartAreas[0].AxisX.LineColor = Color.White;
               

                foreach (Series s in MyChart.Series) {

                   // s.=Color.Red; 
                }

            }
            else {

                MyChart.BackColor = (Color) default;
                MyChart.ChartAreas[0].BackColor = (Color)default;
                //MyChart.ChartAreas[0].AxisY.LineColor = (Color)default;
                //MyChart.ChartAreas[0].AxisX.LineColor = (Color)default;


                foreach (Series s in MyChart.Series)
                {

                    s.LabelBackColor = (Color)default;
                }

            }
        }

        private void checkBoxPan_CheckedChanged(object sender, EventArgs e)
        {
            if (KeyCtrl)
                return;
            CheckBox ch = (CheckBox)sender;
            if (ch.Checked)
            {
                //if (checkBoxZoom.Checked)
                //{
                //    checkBoxZoom.Checked = false;
                //    //Zoom_CheckedChanged(checkBoxZoom, null);
                //}
                //MyChart.MouseDoubleClick += new MouseEventHandler(MyChart_ZoomReset);
                MyChart.Cursor = Cursors.Hand;   
                MyChart.MouseDown += new System.Windows.Forms.MouseEventHandler(chart_MouseDown);
                MyChart.MouseMove  += new System.Windows.Forms.MouseEventHandler(chart_MouseMove);
               
            }
            else {
                MyChart.MouseDown -= new System.Windows.Forms.MouseEventHandler(chart_MouseDown);
                MyChart.MouseMove -= new System.Windows.Forms.MouseEventHandler(chart_MouseMove);
                //MyChart.MouseDoubleClick -= new MouseEventHandler(MyChart_ZoomReset);
                MyChart.Cursor = Cursors.Default;
            }
            MyChart.Focus();
        }

       



        private void chart_MouseDown(object sender, MouseEventArgs e)
        {
            //Это событие происходит при нажатии пользователем кнопки мыши, когда указатель мыши находится на элементе управления.
            //store previous data
            if (e.Button == MouseButtons.Left)
            {
                mDown = e.Location;
                prevXMax = MyChart.ChartAreas[0].AxisX.Maximum;
                prevXMin = MyChart.ChartAreas[0].AxisX.Minimum;
                prevYMax = MyChart.ChartAreas[0].AxisY.Maximum;
                prevYMin = MyChart.ChartAreas[0].AxisY.Minimum;



            }
        }

        private void chart_MouseMove(object sender, MouseEventArgs e)
        {
            if (KeyCtrl)
                return;
            Axis ax = MyChart.ChartAreas[0].AxisX;
            Axis ay = MyChart.ChartAreas[0].AxisY;

            //if mouse was moved and mouse left click
            if (e.Button == MouseButtons.Left)
            {
                double xLast = ax.PixelPositionToValue(mDown.X);
                double xNew = ax.PixelPositionToValue(e.X);
                double yLast = ay.PixelPositionToValue(mDown.Y);
                double yNew = ay.PixelPositionToValue(e.Y);

                ax.Minimum = prevXMin + (xLast - xNew);
                ax.Maximum = prevXMax + (xLast - xNew);
                ay.Minimum = prevYMin + (yLast - yNew);
                ay.Maximum = prevYMax + (yLast - yNew);

                MyChart.ChartAreas[0].AxisX.ScaleView.Scroll(xNew);
                MyChart.ChartAreas[0].AxisY.ScaleView.Scroll(yNew);


                 //MyChart.Update();




            }
        }
    }
}
