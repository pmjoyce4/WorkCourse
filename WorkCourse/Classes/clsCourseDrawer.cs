//#define PreRender

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
        public enum PenModifier
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

        private Size MapSize;
        private float _ZoomFactor = 1;
        public float ZoomFactor
        {
            get { return _ZoomFactor; }
            set
            {
                _ZoomFactor = value;
#if PreRender
                CreateWaypointMarkers();
#endif
            }
        }

        public Dictionary<PenModifier,Pen> pens = new Dictionary<PenModifier, Pen>();

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
            pens.Add(PenModifier.Selected, new Pen(Brushes.WhiteSmoke, 2));
            pens.Add(PenModifier.Start, new Pen(Brushes.LightGreen, 2));
            pens.Add(PenModifier.End, new Pen(Brushes.Red, 2));
            pens.Add(PenModifier.Wait, new Pen(Brushes.Blue, 2));
            pens.Add(PenModifier.Cross, new Pen(Brushes.Yellow, 2));
            pens.Add(PenModifier.Reverse, new Pen(Brushes.Pink, 2));
            pens.Add(PenModifier.Unload, new Pen(Brushes.Purple, 2));
            pens.Add(PenModifier.TurnStart, new Pen(Brushes.Orange, 2));
            pens.Add(PenModifier.TurnEnd, new Pen(Brushes.Salmon, 2));
            pens.Add(PenModifier.Default, new Pen(Brushes.DarkBlue));
        }

        private void DrawCircle(Pen Pen, Graphics g, PointF Center, float Diameter)
        {
            PointF Origin = new PointF(Center.X - Diameter, Center.Y - Diameter);
            SizeF Size = new SizeF(Diameter * 2, Diameter * 2);
            g.DrawEllipse(Pen, new RectangleF(Origin, Size));
        }

        private PenModifier GetPenModifierForWaypoint(clsWaypoint Waypoint)
        {
            if (Waypoint.Wait.HasValue)
                return PenModifier.Wait;
            if (Waypoint.Crossing.HasValue)
                return PenModifier.Cross;
            if (Waypoint.Reverse.HasValue)
                return PenModifier.Reverse;
            if (Waypoint.Unload.HasValue)
                return PenModifier.Unload;
            if (Waypoint.TurnStart.HasValue)
                return PenModifier.TurnStart;
            if (Waypoint.TurnEnd.HasValue)
                return PenModifier.TurnEnd;
            return PenModifier.Default;
        }

        private Pen GetPenForWaypoint(clsWaypoint Waypoint)
        {
            return pens[GetPenModifierForWaypoint(Waypoint)];
        }

        public bool IsOnScreen(PointF Point, Rectangle ViewPort)
        {
            PointF ViewPortCoordinates = new PointF(Point.X + MapSize.Width / 2, Point.Y + MapSize.Height / 2);
            if (ViewPortCoordinates.X < ViewPort.X || ViewPortCoordinates.X > ViewPort.X + ViewPort.Width)
                return false;
            if (ViewPortCoordinates.Y < ViewPort.Y || ViewPortCoordinates.Y > ViewPort.Y + ViewPort.Height)
                return false;
            return true;

        }
        
        #if !PreRender

        public List<clsWaypoint> DrawCourse(clsCourse Course, Graphics g, Rectangle Client, Rectangle ViewPort, List<clsWaypoint> SelectedWaypoints, int Quality)
        {
            //TODO: Draw On seperate surface? Prerender a waypoint marker in each color. Blit to screen.
            clsWaypoint PreviousWaypoint = null;
            Matrix TransformMatrix = new Matrix();
            Matrix PenMatrix = new Matrix();
            clsWaypoint CurrentWaypoint = Course.Waypoints[0];
            List<clsWaypoint> ReturnList = new List<clsWaypoint>();

            TransformMatrix.Translate(((MapSize.Width / 2) - (ViewPort.X)) * ZoomFactor, ((MapSize.Height / 2) - (ViewPort.Y)) * ZoomFactor);
            TransformMatrix.Scale(ZoomFactor, ZoomFactor);
            g.Transform = TransformMatrix;

            for (int i = 0; i < Course.Waypoints.Count; i++)
            {
                Pen CurrentPen;
                if (i != 0 && i < Course.Waypoints.Count)
                {
                    PreviousWaypoint = CurrentWaypoint;
                    CurrentWaypoint = Course.Waypoints[i];
                }

                if (IsOnScreen(CurrentWaypoint.position, ViewPort))
                {
                    ReturnList.Add(CurrentWaypoint);
                    if (i == 0)
                    {
                        CurrentPen = pens[PenModifier.Start];
                    }
                    else if (i == Course.Waypoints.Count - 1)
                    {
                        CurrentPen = pens[PenModifier.End];
                    }
                    else
                    {
                        CurrentPen = GetPenForWaypoint(CurrentWaypoint);
                    }

                    if (SelectedWaypoints != null && SelectedWaypoints.Contains(CurrentWaypoint))
                    {
                        CurrentPen = pens[PenModifier.Selected];
                    }

                    PenMatrix = TransformMatrix.Clone();
                    PenMatrix.Invert();
                    CurrentPen.Transform = PenMatrix;

                    DrawCircle(CurrentPen, g, CurrentWaypoint.position, 6 / ZoomFactor);
                    if (PreviousWaypoint != null)
                    {
                        g.DrawLine(pens[PenModifier.Default], PreviousWaypoint.position, CurrentWaypoint.position);
                    }
                }
                else
                {
                    if (PreviousWaypoint != null && IsOnScreen(PreviousWaypoint.position, ViewPort))
                    {
                        g.DrawLine(pens[PenModifier.Default], PreviousWaypoint.position, CurrentWaypoint.position);
                    }
                }
                if (Quality < 2)
                    i += 2;
            }

            //TransformMatrix.Reset();
            TransformMatrix.Invert();//TransformMatrix.Translate((MapSize.Width / 2) * -1, (MapSize.Height / 2) * -1);
            TransformMatrix.Scale(1, 1);
            g.Transform = TransformMatrix;
            return ReturnList;
        }
        
        #endif

        #region PreRenderingTest

        #if PreRender
        
        private Dictionary<PenModifier, Bitmap> WaypointMarkers = new Dictionary<PenModifier, Bitmap>();
        
        private void CreateWaypointMarkers()
        {
            WaypointMarkers.Clear();
            Bitmap Background = new Bitmap(6, 6, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            GraphicsUnit units = GraphicsUnit.Pixel;

            using (Graphics g = Graphics.FromImage(Background))
            {
                g.FillRectangle(new SolidBrush(Color.Transparent), Background.GetBounds(ref units));
            }
            foreach (PenModifier Modifier in pens.Keys)
            {
                Bitmap NewMarker = new Bitmap(Background);
                using (Graphics g = Graphics.FromImage(NewMarker))
                {
                    g.DrawEllipse(pens[Modifier], NewMarker.GetBounds(ref units));
                }
                WaypointMarkers.Add(Modifier, NewMarker);
            }
        }

        public List<clsWaypoint> DrawCourse(clsCourse Course, Graphics g, Rectangle Client, Rectangle ViewPort, List<clsWaypoint> SelectedWaypoints, int Quality)
        {
            //TODO: Draw On seperate surface? Prerender a waypoint marker in each color. Blit to screen.
            clsWaypoint PreviousWaypoint = null;
            Matrix TransformMatrix = new Matrix();
            Matrix PenMatrix = new Matrix();
            Pen CurrentPen;
            clsWaypoint CurrentWaypoint = Course.waypoint[0];
            List<clsWaypoint> ReturnList = new List<clsWaypoint>();

            TransformMatrix.Translate(((MapSize.Width / 2) - (ViewPort.X)) * ZoomFactor, ((MapSize.Height / 2) - (ViewPort.Y)) * ZoomFactor);
            TransformMatrix.Scale(ZoomFactor, ZoomFactor);
            g.Transform = TransformMatrix;

            PenMatrix = TransformMatrix.Clone();
            PenMatrix.Invert();

            CurrentPen = pens[PenModifier.Default];
            CurrentPen.Transform = PenMatrix;

            for (int i = 0; i < Course.waypoint.Count; i++)
            {
                PenModifier CurrentModifier;
                if (i != 0 && i < Course.waypoint.Count)
                {
                    PreviousWaypoint = CurrentWaypoint;
                    CurrentWaypoint = Course.waypoint[i];
                }

                if (IsOnScreen(CurrentWaypoint.position, ViewPort))
                {
                    ReturnList.Add(CurrentWaypoint);
                    if (i == 0)
                    {
                        CurrentModifier = PenModifier.Start;
                    }
                    else if (i == Course.waypoint.Count - 1)
                    {
                        CurrentModifier = PenModifier.End;
                    }
                    else
                    {
                        CurrentModifier = GetPenModifierForWaypoint(CurrentWaypoint);
                    }

                    if (SelectedWaypoints != null && SelectedWaypoints.Contains(CurrentWaypoint))
                    {
                        CurrentModifier = PenModifier.Selected;
                    }

                    g.DrawImage(WaypointMarkers[CurrentModifier], CurrentWaypoint.position);

                    if (PreviousWaypoint != null)
                    {
                        g.DrawLine(CurrentPen, PreviousWaypoint.position, CurrentWaypoint.position);
                    }
                }
                else
                {
                    if (PreviousWaypoint != null && IsOnScreen(PreviousWaypoint.position, ViewPort))
                    {
                        g.DrawLine(CurrentPen, PreviousWaypoint.position, CurrentWaypoint.position);
                    }
                }
                if (Quality < 2)
                    i += 2;
            }

            //TransformMatrix.Reset();
            TransformMatrix.Invert();//TransformMatrix.Translate((MapSize.Width / 2) * -1, (MapSize.Height / 2) * -1);
            TransformMatrix.Scale(1, 1);
            g.Transform = TransformMatrix;
            return ReturnList;
        }
#endif

        #endregion
    }
}
