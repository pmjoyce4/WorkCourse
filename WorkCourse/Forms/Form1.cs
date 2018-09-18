using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkCourse
{
    public partial class Form1 : Form
    {
        clsCourseManager CourseManager;
        clsCourseDrawer CourseDrawer = new clsCourseDrawer(new Size(2048, 2048));

        private List<clsWaypoint> OnScreenWaypoints = new List<clsWaypoint>();
        private List<clsWaypoint> SelectedWaypoints = new List<clsWaypoint>();
        private bool ControlKeyPressed;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
        }

        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = " BMP|*.bmp";

            if (Properties.Settings.Default.LastMapLocation == null)
            {
                dlg.InitialDirectory = Application.StartupPath;
            }
            else
            {
                dlg.InitialDirectory = Properties.Settings.Default.LastMapLocation;
            }
             
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.LastMapLocation = Path.GetDirectoryName(dlg.FileName);
                Properties.Settings.Default.Save();
                panZoomBox1.Image = Image.FromFile(dlg.FileName);
            }
        }

        private void loadCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = " Xml|courseManager.xml";

            if (Properties.Settings.Default.LastCourseLocation == null)
            {
                dlg.InitialDirectory = Application.StartupPath;
            }
            else
            {
                dlg.InitialDirectory = Properties.Settings.Default.LastCourseLocation;
            }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.LastCourseLocation = Path.GetDirectoryName(dlg.FileName);
                Properties.Settings.Default.Save();
                CourseManager =  clsXmlSaveLoad.DeserializeXmlFromFile<clsCourseManager>(dlg.FileName);
                CourseManager.Path = Path.GetDirectoryName(dlg.FileName);
                CourseManager.LoadCourses();
                panZoomBox1.Invalidate();
            }
        }

        private void panZoomBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (OnScreenWaypoints != null)
                OnScreenWaypoints.Clear();
            // Don't know if these do any good trying to speed this up... Drawing a lot of circles takes a long time.
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            if (CourseManager == null)
                return;
            foreach (clsSlot Slot in CourseManager.saves.slot)
            {
                List<clsWaypoint> NewWaypoints = null;
                if (Slot.course.IsEnabled)
                    NewWaypoints = CourseDrawer.DrawCourse(Slot.course, e.Graphics, panZoomBox1.ClientRectangle, panZoomBox1.ViewPort, (float)panZoomBox1.ZoomFactor, SelectedWaypoints, panZoomBox1.Quality);
                if (NewWaypoints != null && NewWaypoints.Count > 0)
                   OnScreenWaypoints.AddRange(NewWaypoints);
            }
        }

        private PointF CursorToWorldPosition(Point CursorPosition)
        {
            PointF CursorWorldPosition = new PointF();
            CursorWorldPosition.X = (float)Math.Round(((CursorPosition.X / panZoomBox1.ZoomFactor) + panZoomBox1.ViewPort.X) - panZoomBox1.Image.Size.Width / 2, 2);
            CursorWorldPosition.Y = (float)Math.Round(((CursorPosition.Y / panZoomBox1.ZoomFactor) + panZoomBox1.ViewPort.Y) - panZoomBox1.Image.Size.Height / 2, 2);
            return CursorWorldPosition;
        }

        private clsWaypoint GetWaypointUnderCursor(Point CursorPosition)
        {
            List<clsWaypoint> CloseWaypoints = new List<clsWaypoint>();
            PointF CursorMapPosition = CursorToWorldPosition(CursorPosition);

            foreach (clsWaypoint Waypoint in OnScreenWaypoints)
            {
                if (Math.Abs(CursorMapPosition.X - Waypoint.position.X) <= 6 / panZoomBox1.ZoomFactor && Math.Abs(CursorMapPosition.Y - Waypoint.position.Y) <= 6 / panZoomBox1.ZoomFactor)
                    CloseWaypoints.Add(Waypoint);
            }
            if (CloseWaypoints.Count > 0)
            {
                if (CloseWaypoints.Count == 1)
                    return CloseWaypoints[0];

                clsWaypoint ClosestWaypoint = new clsWaypoint();
                double DeltaX = double.MaxValue;
                double DeltaY = double.MaxValue;

                foreach(clsWaypoint Waypoint in CloseWaypoints)
                {
                    if (CursorPosition.X - Waypoint.position.X < DeltaX && CursorPosition.Y - Waypoint.position.Y < DeltaY)
                        ClosestWaypoint = Waypoint;
                }
                return ClosestWaypoint;
            }
            return null;
        }

        private void panZoomBox1_Click(object sender, MouseEventArgs e)
        {
            clsWaypoint ClickedWaypoint = null;
            if (e.Button == MouseButtons.Left)
            {
                ClickedWaypoint = GetWaypointUnderCursor(new Point(e.X, e.Y));
                if (ClickedWaypoint != null)
                {
                    if (!ControlKeyPressed)
                        SelectedWaypoints.Clear();
                    SelectedWaypoints.Add(ClickedWaypoint);
                    panZoomBox1.Invalidate();
                }
            }
        }

        private void panZoomBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void panZoomBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (panZoomBox1.Image != null)
                label1.Text = CursorToWorldPosition(new Point(e.X, e.Y)).ToString();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                this.ControlKeyPressed = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                this.ControlKeyPressed = false;
            }
        }
    }
}
