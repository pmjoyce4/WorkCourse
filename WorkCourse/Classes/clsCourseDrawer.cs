using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkCourse
{
    class clsCourseDrawer
    {
        // Can probably make this private... we'll see.
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

        public Size MapSize;

        private Dictionary<pen,Pen> pens = new Dictionary<pen, Pen>();

        public clsCourseDrawer()
        {
            CreatePenSet();
        }

        public clsCourseDrawer(Size MapSize)
        {
            this.MapSize = MapSize;
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

        private void DrawCircle(Pen Pen, Graphics g, PointF Center, float Diameter)
        {
            PointF Origin = new PointF(Center.X - Diameter, Center.Y - Diameter);
            SizeF Size = new SizeF(Diameter * 2, Diameter * 2);
            g.DrawEllipse(Pen, new RectangleF(Origin, Size));
        }

        private Pen GetPenForWaypoint(clsWaypoint Waypoint)
        {
            if (Waypoint.Wait.HasValue)
                return pens[pen.Wait];
            if (Waypoint.Crossing.HasValue)
                return pens[pen.Cross];
            if (Waypoint.Reverse.HasValue)
                return pens[pen.Reverse];
            if (Waypoint.Unload.HasValue)
                return pens[pen.Unload];
            if (Waypoint.TurnStart.HasValue)
                return pens[pen.TurnStart];
            if (Waypoint.TurnEnd.HasValue)
                return pens[pen.TurnEnd];
            return pens[pen.Default];
        }

        public void DrawCourse(clsCourse Course, Graphics g, Rectangle Client, Rectangle ViewPort, float ZoomFactor)
        {
            clsWaypoint PreviousWaypoint;
            clsWaypoint CurrentWaypoint = Course.waypoint[0];
            List<PointF> Lines = new List<PointF>();
            List<Rectangle> Circles = new List<Rectangle>();
            PointF MapPosition;
            for (int i = 0; i < Course.waypoint.Count; i++)
            {
                Pen CurrentPen;
                if (i != 0 && i < Course.waypoint.Count)
                {
                    PreviousWaypoint = CurrentWaypoint;
                    CurrentWaypoint = Course.waypoint[i];
                }
                else
                {
                    PreviousWaypoint = null;
                }

                MapPosition = clsPositionCalculator.MapPositionFromWorldCoordinates(CurrentWaypoint.position, MapSize);
                if (clsPositionCalculator.Visible(MapPosition, ViewPort))
                {
                    if (i == 0)
                    {
                        CurrentPen = pens[pen.Start];
                    }
                    else if (i == Course.waypoint.Count - 1)
                    {
                        CurrentPen = pens[pen.End];
                    }
                    else
                    {
                        CurrentPen = GetPenForWaypoint(CurrentWaypoint);
                    }
                    
                    DrawCircle(CurrentPen, g, clsPositionCalculator.ScreenPosition(MapPosition, ViewPort, ZoomFactor), 6);
                    if (PreviousWaypoint != null)
                    {
                        PointF PreviousPosition = clsPositionCalculator.MapPositionFromWorldCoordinates(PreviousWaypoint.position, MapSize);
                        PointF PrevScreenPosition = clsPositionCalculator.ScreenPosition(PreviousPosition, ViewPort, ZoomFactor);
                        g.DrawLine(pens[pen.Default], PrevScreenPosition, clsPositionCalculator.ScreenPosition(MapPosition, ViewPort, ZoomFactor));
                    }
                }
                else if (PreviousWaypoint != null && clsPositionCalculator.Visible(clsPositionCalculator.MapPositionFromWorldCoordinates(PreviousWaypoint.position, MapSize), ViewPort))
                {
                    PointF PreviousPosition = clsPositionCalculator.MapPositionFromWorldCoordinates(PreviousWaypoint.position, MapSize);
                    PointF CurrentPosition = clsPositionCalculator.MapPositionFromWorldCoordinates(CurrentWaypoint.position, MapSize);
                    PointF PrevScreenPosition = clsPositionCalculator.ScreenPosition(PreviousPosition, ViewPort, ZoomFactor);
                    PointF CurrScreenPosition = clsPositionCalculator.ScreenPosition(CurrentPosition, ViewPort, ZoomFactor);
                    g.DrawLine(pens[pen.Default], PrevScreenPosition, CurrScreenPosition);
                }
            }
        }

        public bool IsVisible(PointF Point, Rectangle ViewPort)
        {
            if (Point.X < ViewPort.X || Point.X > Point.X + ViewPort.Width)
                return false;
            if (Point.Y < ViewPort.Y || Point.Y > Point.Y + ViewPort.Height)
                return false;
            return true;

        }

        public void DrawCourseNew(clsCourse Course, Graphics g, Rectangle Client, Rectangle ViewPort, float ZoomFactor)
        {
            clsWaypoint PreviousWaypoint;
            Matrix TransformMatrix = new Matrix();
            Matrix PenMatrix = new Matrix();
            clsWaypoint CurrentWaypoint = Course.waypoint[0];
            List<PointF> Lines = new List<PointF>();
            List<PointF> Points = new List<PointF>();

            TransformMatrix.Translate(((MapSize.Width / 2) - (ViewPort.X)) * ZoomFactor, ((MapSize.Height / 2) * ZoomFactor - (ViewPort.Y)) * ZoomFactor);
            TransformMatrix.Scale(ZoomFactor, ZoomFactor);
            g.Transform = TransformMatrix;
            
            for (int i = 0; i < Course.waypoint.Count; i++)
            {
                Pen CurrentPen;
                if (i != 0 && i < Course.waypoint.Count)
                {
                    PreviousWaypoint = CurrentWaypoint;
                    CurrentWaypoint = Course.waypoint[i];
                }
                else
                {
                    PreviousWaypoint = null;
                }
                if (i == 0)
                {
                    CurrentPen = pens[pen.Start];
                }
                else if (i == Course.waypoint.Count - 1)
                {
                    CurrentPen = pens[pen.End];
                }
                else
                {
                    CurrentPen = GetPenForWaypoint(CurrentWaypoint);
                }

                PenMatrix = TransformMatrix.Clone();
                PenMatrix.Invert();
                CurrentPen.Transform = PenMatrix;

                DrawCircle(CurrentPen, g, CurrentWaypoint.position, 6 / ZoomFactor);
                Lines.Add(CurrentWaypoint.position);
            }
            g.DrawLines(pens[pen.Default], Lines.ToArray());
            TransformMatrix.Reset();
            TransformMatrix.Translate((MapSize.Width / 2) * -1, (MapSize.Height / 2) * -1);
            g.Transform = TransformMatrix;
        }

    }
}
