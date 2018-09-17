using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkCourse
{
    class clsPositionCalculator
    {
        public static PointF WorldCoordinatesFromSreenPosition(PointF Point, Rectangle ViewPort, double ZoomFactor)
        {
            throw new NotImplementedException();
        }

        public static bool Visible(PointF MapPosition, Rectangle ViewPort)
        {
            if (MapPosition.X < ViewPort.X || MapPosition.X > ViewPort.X + ViewPort.Width)
                return false;
            if (MapPosition.Y < ViewPort.Y || MapPosition.Y > ViewPort.Y + ViewPort.Height)
                return false;
            return true;
        }

        public static PointF MapPositionFromWorldCoordinates(PointF Point, Size MapSize)
        {
            PointF Center = new PointF();
            Center.X = Point.X + MapSize.Width / 2;
            Center.Y = Point.Y + MapSize.Height / 2;
            return Center;
        }

        public static PointF ScreenPosition(PointF Point, Rectangle ViewPort, double ZoomFactor)
        {
            PointF NewPoint = new PointF();
            NewPoint.X = (float)((Point.X - ViewPort.X) * ZoomFactor);
            NewPoint.Y = (float)((Point.Y - ViewPort.Y) * ZoomFactor);
            return NewPoint;
        }
    }
}
