using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkCourse
{
    class clsCourseDrawer
    {
        public enum pen
        {
            Selected,
            Start,
            End,
            Wait,
            Cross,
            Reverse,
            Unload,
            TurnStart,
            TurnEnd,
            Default
        }

        private Dictionary<pen,Pen> pens = new Dictionary<pen, Pen>();
        public clsCourseDrawer()
        {
            CreatePenSet();
        }

        private void CreatePenSet()
        {
            pens.Add(pen.Selected, new Pen(Brushes.WhiteSmoke, 2));
            pens.Add(pen.Start, new Pen(Brushes.LightGreen, 2));
            pens.Add(pen.End, new Pen(Brushes.Red, 2));
            pens.Add(pen.Wait, new Pen(Brushes.Blue, 2));
            pens.Add(pen.Cross, new Pen(Brushes.Yellow, 2));
            pens.Add(pen.Reverse, new Pen(Brushes.Pink, 2));
            pens.Add(pen.Unload, new Pen(Brushes.Purple, 2));
            pens.Add(pen.TurnStart, new Pen(Brushes.Orange, 2));
            pens.Add(pen.TurnEnd, new Pen(Brushes.Salmon, 2));
            pens.Add(pen.Default, new Pen(Brushes.DarkBlue));
        }

        private bool Visible(PointF Point, Rectangle ViewPort, double ZoomFactor)
        {
            return false;
        }

        private PointF GetMapPosition(PointF Point, double ZoomFactor)
        {
            return Point;
        }

        public void DrawCourse(ref clsCourse Course, ref Graphics g, Rectangle ViewPort, double ZoomFactor)
        {
            PointF MapPosition = new PointF();
            for (int i = 0; i < Course.waypoint.Count; i++)
            {
                MapPosition = GetMapPosition(Course.waypoint[i].position, ZoomFactor);
                if (Visible(MapPosition, ViewPort, ZoomFactor))
                {

                }
            }
        }
    }
}
