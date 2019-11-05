using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;
using System.Drawing;

namespace ZhiMoney.Data
{
    public class MyChart
    {
        public Chart Chart { get; private set; }
        public ChartArea ChartArea { get; private set; }
        public Series FirstSeries { get; private set; }
        public Series SecondSeries { get; private set; }
        public Series ThirdSeries { get; private set; }
        public FontFamily FontFamily { get; private set; }
        private readonly Color[] Colors;

        public MyChart()
        {
            Chart = new Chart();
            ChartArea = new ChartArea();
            FirstSeries = new Series();
            SecondSeries = new Series();
            ThirdSeries = new Series();

            //winhost.Child = Chart;

            Chart.ChartAreas.Add(ChartArea);

            ChartArea.AxisX.MajorGrid.LineColor = Color.Transparent;
            ChartArea.AxisY.MajorGrid.LineColor = Color.Gray;

            ChartArea.AxisX.MinorGrid.Enabled = false;
            ChartArea.AxisY.MinorGrid.Enabled = false;

            ChartArea.AxisX.MajorTickMark.Enabled = false;
            ChartArea.AxisY.MajorTickMark.Enabled = false;

            ChartArea.AxisY.MajorGrid.LineWidth = 2;

            ChartArea.AxisX.MinorTickMark.Enabled = false;
            ChartArea.AxisY.MinorTickMark.Enabled = false;

            ChartArea.AxisX.LabelStyle.ForeColor = Color.Gray;
            ChartArea.AxisY.LabelStyle.ForeColor = Color.Gray;

            ChartArea.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            ChartArea.AxisX.LabelAutoFitMaxFontSize = 8;

            ChartArea.AxisX.LineWidth = 3;
            ChartArea.AxisY.LineWidth = 3;

            ChartArea.AxisX.LineColor = Color.Gray;
            ChartArea.AxisY.LineColor = Color.Gray;

            ChartArea.BackColor = Color.Transparent;

            ChartArea.AlignmentStyle = AreaAlignmentStyles.PlotPosition;

            Colors = new Color[] {
                Color.Green,
                Color.Red,
                Color.Blue
            };

            Chart.BackColor = Color.Transparent;

            Chart.Palette = ChartColorPalette.None;

            Chart.PaletteCustomColors = Colors;

            Chart.Series.Add(nameof(FirstSeries));//ЛИНИЯ ДОХОДОВ
            Chart.Series.Add(nameof(SecondSeries));//ЛИНИЯ РАСХОДОВ
            Chart.Series.Add(nameof(ThirdSeries));//ЛИНИЯ ОБЩАЯ

            Chart.Series[nameof(FirstSeries)].ChartType = SeriesChartType.Line;
            Chart.Series[nameof(SecondSeries)].ChartType = SeriesChartType.Line;
            Chart.Series[nameof(ThirdSeries)].ChartType = SeriesChartType.Line;

            Chart.Series[nameof(FirstSeries)].MarkerStyle = MarkerStyle.Circle;
            Chart.Series[nameof(FirstSeries)].MarkerColor = Colors[0];
            Chart.Series[nameof(FirstSeries)].BorderWidth = 4;
            Chart.Series[nameof(FirstSeries)].MarkerSize = 8;

            Chart.Series[nameof(SecondSeries)].MarkerStyle = MarkerStyle.Circle;
            Chart.Series[nameof(SecondSeries)].MarkerColor = Colors[1];
            Chart.Series[nameof(SecondSeries)].BorderWidth = 4;
            Chart.Series[nameof(SecondSeries)].MarkerSize = 8;

            Chart.Series[nameof(ThirdSeries)].MarkerStyle = MarkerStyle.Circle;
            Chart.Series[nameof(ThirdSeries)].MarkerColor = Colors[2];
            Chart.Series[nameof(ThirdSeries)].BorderWidth = 4;
            Chart.Series[nameof(ThirdSeries)].MarkerSize = 8;

            FontFamily = new FontFamily("Calibri");//выбрали шрифт Calibri
        }
    }
}