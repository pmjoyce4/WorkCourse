using System;
using System.Drawing;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkCourse
{
    [XmlRoot("waypoint")]
    public class clsWaypoint
    {
        #region Properties
        
        [XmlIgnore]
        public PointF position;
        [XmlIgnore]
        public double? Angle;
        [XmlIgnore]
        public double? Speed;
        [XmlIgnore]
        public bool? Wait;
        [XmlIgnore]
        public bool? Crossing;
        [XmlIgnore]
        public bool? TurnStart;
        [XmlIgnore]
        public bool? TurnEnd;
        [XmlIgnore]
        public bool? Reverse;
        [XmlIgnore]
        public bool? Unload;
        [XmlIgnore]
        public int? RidgeMarker;
        [XmlIgnore]
        public bool? Generated;
        [XmlIgnore]
        public int? Lane;

        #region XmlSerializeValues
        // Because XmlSerializer can't handle ~complex~ values stored as an XmlAttributes, Nullable int, which can be an int or null, is not
        // supported. 
        [XmlAttribute("angle")]
        public string AngleString
        {
            get { if (!Angle.HasValue) return null; return string.Format("{0:0.00}", Angle); }
            set { Angle = Convert.ToDouble(value); }
        }
        [XmlAttribute("generated")]
        public string GeneratedString
        {
            get { if (!Generated.HasValue) return null; return (Generated.Value) ? "true" : null; }
            set { Generated = value == "true"; }
        }
        [XmlAttribute("ridgemarker")]
        public string RidgeMarkerString
        {
            get { if (!RidgeMarker.HasValue) return null; return RidgeMarker.Value.ToString(); }
            set { RidgeMarker = Convert.ToInt32(value); }
        }
        [XmlAttribute("unload")]
        public string UnloadString
        {
            get { if (!Unload.HasValue) return null; return (Unload.Value) ? "1" : null; }
            set { Unload = (Convert.ToInt32(value) == 1); }
        }
        [XmlAttribute]
        public string SpeedString
        {
            get { if (!Speed.HasValue) return null; return string.Format("{0:0}", Speed); }
            set { Speed = Convert.ToDouble(value); }
        }
        [XmlAttribute("rev")]
        public string ReverseString
        {
            get { if (!Reverse.HasValue) return null; return (Reverse.Value) ? "1" : null; }
            set { Reverse = (Convert.ToInt32(value) == 1); }
        }
        [XmlAttribute("pos")]
        public string PositionString
        {
            get { return string.Format("{0:0.00}", position.X) + " " + string.Format("{0:0.00}", position.Y); }

            set
            {
                string[] values = value.Split(' ');
                position.X = (float)Convert.ToDouble(values[0]);
                position.Y = (float)Convert.ToDouble(values[1]);
            }
        }
        [XmlAttribute("crossing")]
        public string CrossingString
        {
            get { if (!Crossing.HasValue) return null; return (Crossing.Value) ? "1" : null; }
            set { Crossing = (Convert.ToInt32(value) == 1); }
        }
        [XmlAttribute("wait")]
        public string WaitString
        {
            get { if (!Wait.HasValue) return null; return (Wait.Value) ? "1" : null; }
            set { Wait = (Convert.ToInt32(value) == 1); }
        }
        [XmlAttribute("turnstart")]
        public string TurnStartString
        {
            get { if (!TurnStart.HasValue) return null; return (TurnStart.Value) ? "1" : null; }
            set { TurnStart = (Convert.ToInt32(value) == 1); }
        }
        [XmlAttribute("turnend")]
        public string TurnEndString
        {
            get { if (!TurnEnd.HasValue) return null; return (TurnEnd.Value) ? "1" : null; }
            set { TurnEnd = (Convert.ToInt32(value) == 1); }
        }
        [XmlAttribute("lane")]
        public string LaneString
        {
            get { if (!Lane.HasValue) return null;  return Lane.ToString(); }
            set { Lane = Convert.ToInt32(value); }
        }
        #endregion
        #endregion
    }
}
