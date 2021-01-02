 if (MouseDowned)
                    {
                        //Pan Move - Valid only if view is zoomed
                        if (ptrChartArea.AxisX.ScaleView.IsZoomed ||
                            ptrChartArea.AxisY.ScaleView.IsZoomed)
                        {
                            double dx = -selX1 + XStart;
                            double dy = -selY1 + YStart;
                            double dx2 = -selX2 + X2Start;
                            double dy2 = -selY2 + Y2Start;

                            double newX = ptrChartArea.AxisX.ScaleView.Position + dx;
                            double newY = ptrChartArea.AxisY.ScaleView.Position + dy;
                            double newX2 = ptrChartArea.AxisX2.ScaleView.Position + dx2;
                            double newY2 = ptrChartArea.AxisY2.ScaleView.Position + dy2;

                            ptrChartArea.AxisX.ScaleView.Scroll(newX);
                            ptrChartArea.AxisY.ScaleView.Scroll(newY);
                            ptrChartArea.AxisX2.ScaleView.Scroll(newX2);
                            ptrChartArea.AxisY2.ScaleView.Scroll(newY2);

                            ptrChartData.RepaintBufferedData();
                            AdjustAxisIntervalForAllAxes(ptrChartArea);
                        }
                    }


 private static void RepaintBufferedData(this ChartData sender)
        {
            if (!sender.Option.BufferedMode) return;
            foreach (SeriesDataBuffer s in sender.SeriesData)
            {
                if (s.DataBuffer.Count == 0) continue; //ToDo: Shall we allow mixture of series? Buffered and non-buffered series?
                if (s.Series.ChartArea == sender.ActiveChartArea.Name)
                    s.Series.PlotBufferedData();
            }
        }


 public static void PlotBufferedData(this Series sender)
        {
            ChartArea ptrChartArea = GetChartArea(sender);

            //Identify Series Axis
            Axis ptrXAxis = sender.XAxisType == AxisType.Primary ? ptrChartArea.AxisX : ptrChartArea.AxisX2;
            Axis ptrYAxis = sender.YAxisType == AxisType.Primary ? ptrChartArea.AxisY : ptrChartArea.AxisY2;

            //Get Visible Boundary
            RectangleF region = ptrChartArea.GetChartVisibleAreaBoundary(ptrXAxis, ptrYAxis);
            SeriesDataBuffer ptrDataBuffer = GetSeriesDataBuffer(sender);
            if (ptrDataBuffer == null) return;

            sender.ClearPointsInt(clearDataBuffer: false);
            IList<PointD> VisibleDatas = ptrDataBuffer.DataBuffer;
            if (VisibleDatas.Count == 0) return;

            if (ptrXAxis.ScaleView.IsZoomed || ptrYAxis.ScaleView.IsZoomed)
            {
                float width10p = (float)(region.Width * 0.20);

                float leftBoundary = (float)Math.Max(ptrDataBuffer.XMin, region.X - width10p);
                float leftWidth = region.X - leftBoundary; //Get widht increment on left, could be less than 10%
                region.X = leftBoundary;
                region.Width = (float)Math.Min(ptrDataBuffer.XMax - region.X, region.Width + leftWidth + width10p);

                //Get data points in visible area.
                if (ptrDataBuffer.DataBuffer.Count > 0)
                {
                    if (ptrXAxis.IsReversed)
                        VisibleDatas = ptrDataBuffer.DataBuffer.Where(n => ((n.X >= region.Right) && (n.X <= region.Left))).ToList();
                    else
                        VisibleDatas = ptrDataBuffer.DataBuffer.Where(n => ((n.X >= region.Left) && (n.X <= region.Right))).ToList();
                }
            }

            ChartOption ptrOption = GetChartData(sender).Option;
            if (VisibleDatas.Count > ptrOption.DisplayDataSize * 2)
            {
                VisibleDatas = DownSampling.DownsampleLTTB(VisibleDatas.ToArray(), ptrOption.DisplayDataSize);
            }

            //Plot Min Point if out of sight
            if (region.Left > ptrDataBuffer.XMin) sender.Points.AddXY(ptrDataBuffer.XMinPoint.X, ptrDataBuffer.XMinPoint.Y);

            //Plot Data
            foreach (PointD p in VisibleDatas)
            {
                sender.Points.AddXY(p.X, p.Y);
            }

            //Plot Max Point if out of sight
            if (region.Right < ptrDataBuffer.XMax) sender.Points.AddXY(ptrDataBuffer.XMaxPoint.X, ptrDataBuffer.XMaxPoint.Y);

        }

 private static void ClearPointsInt(this Series sender, bool clearDataBuffer = true)
        {
            while (sender.Points.Count > 0)
                sender.Points.RemoveAt(sender.Points.Count - 1);
            sender.Points.Clear(); //Force refresh.

            if (clearDataBuffer)
            {
                if (sender.GetChartData().Option.BufferedMode)
                    sender.GetSeriesDataBuffer(true).Clear();
            }
        }