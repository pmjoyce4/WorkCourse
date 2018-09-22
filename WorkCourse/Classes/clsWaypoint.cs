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
        #region MemberVariables

        #region Public

        #region NonSerialized

        [XmlIgnore]
        public clsWaypoint Previous;

        [XmlIgnore]
        public clsWaypoint Next;

        [XmlIgnore]
        public PointF position;

        [XmlIgnore]
        public double? Angle;

        [XmlIgnore]
        public int? Direction;

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

        #endregion

        #endregion

        #endregion

        #region Properties
        // Because XmlSerializer can't handle ~complex~ values stored as an XmlAttributes, Nullable int, which can be an int or null, is not
        // supported. 
        [XmlAttribute("angle")]
        public string AngleString
        {
            get { if (!Angle.HasValue) return null; return string.Format("{0:0.00}", Angle); }
            set { Angle = Convert.ToDouble(value); }
        }

        [XmlAttribute("dir")]
        public string DirectionString
        {
            get { if (!Direction.HasValue) return null; return string.Format("0:00", Direction); }
            set { Direction = Convert.ToInt32(value); }
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
            get { return NumericBool(Unload); }
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
            get { return NumericBool(Reverse); }
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
            get { return NumericBool(Crossing); }
            set { Crossing = (Convert.ToInt32(value) == 1); }
        }

        [XmlAttribute("wait")]
        public string WaitString
        {
            get { return NumericBool(Wait); }
            set { Wait = (Convert.ToInt32(value) == 1); }
        }

        [XmlAttribute("turnstart")]
        public string TurnStartString
        {
            get { return NumericBool(TurnStart); }
            set { TurnStart = (Convert.ToInt32(value) == 1); }
        }

        [XmlAttribute("turnend")]
        public string TurnEndString
        {
            get { return NumericBool(TurnEnd); }
            set { TurnEnd = (Convert.ToInt32(value) == 1); }
        }

        [XmlAttribute("lane")]
        public string LaneString
        {
            get { if (!Lane.HasValue) return null; return Lane.ToString(); }
            set { Lane = Convert.ToInt32(value); }
        }

        #endregion

        #region XmlSerializeSupportFunctions

        private string NumericBool(bool? varNullBool)
        {
            if (!varNullBool.HasValue)
                return null;
            return varNullBool.Value ? "1" : null;  // False == null so we don't bloat Xml with useless value names. They're considered false already if not present.
        }
        
        #endregion
    }
}
