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
        #region MemberVariables

        clsCourseManager CourseManager;
        clsCourseDrawer CourseDrawer = new clsCourseDrawer(new Size(2048, 2048));

        private List<clsWaypoint> OnScreenWaypoints = new List<clsWaypoint>();
        private List<clsWaypoint> SelectedWaypoints = new List<clsWaypoint>();
        private Queue<Rectangle> PreviousInvalidationRectangle = new Queue<Rectangle>();
        private clsWaypoint ClickedWaypoint = null;
        private Rectangle SelectionArea = new Rectangle();
        private Rectangle PreviousSelectionArea = new Rectangle();
        private Point PreviousCursorPosition;
        private Point StartSelectionPoint;
        private Point EndSelectionPoint;
        private bool SelectionStarted = false;
        private bool ShowedContext = false;
        private int WaypointBuffer = 10;

        #endregion

        #region Constructor

        public Form1()
        {
            InitializeComponent();
            SelectionArea = Rectangle.Empty;
            PreviousSelectionArea = Rectangle.Empty;
            PreviousCursorPosition = Point.Empty;
            
        #if !Debug
            this.label1.Visible = false;
            this.label2.Visible = false;
            this.label3.Visible = false;
        #endif
        }

        #endregion

        #region EventHandlers

        #region MouseEvents

        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenBitmap();
        }

        private void loadCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenCourseManager();
        }

        private void panZoomBox1_Click(object sender, MouseEventArgs e)
        {
            Point CursorPosition = new Point(e.X, e.Y);

            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    ClickedWaypoint = GetWaypointAtClientPosition(CursorPosition);
                    if (ClickedWaypoint != null || SelectedWaypoints.Count > 0)
                    {
                        if (SelectedWaypoints.Count == 0 && !SelectedWaypoints.Contains(ClickedWaypoint))
                            SelectWaypoint();
                        if (SelectedWaypoints.Count > 1)
                        {
                            MultiWaypointContextMenu.Show(panZoomBox1, CursorPosition);
                        }
                        else
                        {
                            WaypointContexMenu.Show(panZoomBox1, CursorPosition);
                        }
                        
                        ShowedContext = true;
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        private void panZoomBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Point CursorPosition = new Point(e.X, e.Y);

            try
            {
                if (e.Button == MouseButtons.Left)
                    ClickedWaypoint = GetWaypointAtClientPosition(CursorPosition);
            }
            catch (ArgumentNullException)
            {
                return;
            }
        }

        private void panZoomBox1_MouseMove(object sender, MouseEventArgs e)
        {
            #if Debug
            try
            {
                UpdateDebugLabels(e);
            }
            catch (ArgumentNullException)
            {
                return;
            }

            #endif

            Point CursorPosition = new Point(e.X, e.Y);

            if (SelectionStarted)
            {
                UpdateSelection(CursorPosition);
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                WaypointSelectionAndMovement(CursorPosition);
            }
            ShowedContext = false;
        }

        private void panZoomBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Point CursorPosition = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                if (ClickedWaypoint != null && ClickedWaypoint == GetWaypointAtClientPosition(CursorPosition))
                {
                    SelectWaypoint();
                }
                else
                {
                    EndSelection(CursorPosition);
                }
                ClickedWaypoint = null;
                if (SelectionStarted)
                {
                    SelectionStarted = false;
                    UpdateSelection(CursorPosition);
                }
            }
        }

        #endregion

        #region KeyboardEvents


        private void panZoomBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode) 
                {
                    case Keys.Down:
                    case Keys.Up:
                    case Keys.Left:
                    case Keys.Right:
                    e.IsInputKey = true;
                    break;
                }
        }

        private void panZoomBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Left & Keys.Right & Keys.Up & Keys.Down) > 0 && SelectedWaypoints.Count > 0)
                MoveWaypoints(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        #endregion

        #region DrawingEvents

        private void panZoomBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawCourses(e);
            if (SelectionStarted)
                DrawSelection(e);
        }

        #endregion

        #region ContextMenuEvents

        private void insertWaypointBeforeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void insertWaypointAfterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteWaypointToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #endregion

        #region KeyHelperFunctions

        private bool ControlKeyPressed()
        {
            return (ModifierKeys & Keys.Control) == Keys.Control;
        }

        #endregion

        #region FileSupportFunctions

        private void OpenBitmap()
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
                panZoomBox1.Quality = 2;
            }
        }

        private void OpenCourseManager()
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
                CourseManager = clsXmlSaveLoad.DeserializeXmlFromFile<clsCourseManager>(dlg.FileName);
                CourseManager.Path = Path.GetDirectoryName(dlg.FileName);
                CourseManager.LoadCourses();
                panZoomBox1.Invalidate();
            }
        }

        private void LoadTreeView()
        {

        }

        #endregion

        #region WaypointFunctions

        private void StartSelection(Point Point)
        {
            ClearSelectedWaypoints();
            if (!SelectionStarted)
            {
                StartSelectionPoint = Point;
                SelectionStarted = true;
            }
        }

        private void EndSelection(Point Point)
        {
            EndSelectionPoint = Point;
        }

        private void WaypointSelectionAndMovement(Point CursorPosition)
        {

            if (ClickedWaypoint != null)
            {
                SelectWaypoint();
                ClickedWaypoint = null;
            }
            else if (!ControlKeyPressed())
            {
                StartSelection(CursorPosition);
                return;
            }

            if (SelectedWaypoints.Count > 0 && !ShowedContext && !ControlKeyPressed())
            {
                try
                {
                    MoveWaypoints(CursorPosition);
                    PreviousCursorPosition.X = CursorPosition.X; PreviousCursorPosition.Y = CursorPosition.Y;
                }
                catch (ArgumentNullException)
                {
                    return;
                }
            }
        }

        private void UpdateSelection(Point Point)
        {
            SelectionArea.X = (StartSelectionPoint.X < EndSelectionPoint.X) ? StartSelectionPoint.X : EndSelectionPoint.X;
            SelectionArea.Y = (StartSelectionPoint.Y < EndSelectionPoint.Y) ? StartSelectionPoint.Y : EndSelectionPoint.Y;
            SelectionArea.Size = new Size(Math.Abs(StartSelectionPoint.X - EndSelectionPoint.X), Math.Abs(StartSelectionPoint.Y - EndSelectionPoint.Y));

            if (!SelectionStarted)
            {
                SelectMultipleWaypoints();
                SelectionArea = Rectangle.Empty;
                PreviousSelectionArea = Rectangle.Empty;
            }
            else
            {
                if (!PreviousSelectionArea.IsEmpty)
                {
                    panZoomBox1.Invalidate(Rectangle.Union(PreviousSelectionArea, SelectionArea));
                }
                else
                {
                    panZoomBox1.Invalidate(SelectionArea);
                }
                PreviousSelectionArea = new Rectangle(SelectionArea.X, SelectionArea.Y, SelectionArea.Width, SelectionArea.Height);
            }
        }

        private void SelectMultipleWaypoints()
        {
            RectangleF WorldRectangle = WorldRectangleFromClientRectangle(SelectionArea);
            foreach (clsWaypoint Waypoint in OnScreenWaypoints)
            {
                if (WorldRectangle.Contains(Waypoint.position))
                    SelectedWaypoints.Add(Waypoint);
            }
        }

        private void SelectWaypoint()
        {
            if (ClickedWaypoint != null)
            {
                if (SelectedWaypoints.Contains(ClickedWaypoint) && !ControlKeyPressed())
                    return;
                if (!ControlKeyPressed())
                    ClearSelectedWaypoints();
                if (!SelectedWaypoints.Contains(ClickedWaypoint))
                {
                    SelectedWaypoints.Add(ClickedWaypoint);
                    InvalidateWaypoint(ClickedWaypoint);
                }
            }
            else
            {
                if (!ControlKeyPressed())
                    ClearSelectedWaypoints();
            }
        }

        private void ClearSelectedWaypoints()
        {
            while (SelectedWaypoints.Count > 0)
            {
                clsWaypoint CurrentWaypoint = SelectedWaypoints[0];
                SelectedWaypoints.Remove(CurrentWaypoint);
                InvalidateWaypoint(CurrentWaypoint);
            }
        }

        private void MoveWaypoints(KeyEventArgs e)
        {
            float LargeMove = (float)0.5;
            float SmallMove = (float)0.1;

            Rectangle NewInvalidationRectangle;
            float delta = ((ModifierKeys & Keys.Shift) == Keys.Shift) ? LargeMove : SmallMove;
            foreach (clsWaypoint CurrentWaypoint in SelectedWaypoints)
            {
                switch (e.KeyCode)
                {
                    case Keys.Right :
                        CurrentWaypoint.position.X += delta;
                        break;
                    case Keys.Left:
                        CurrentWaypoint.position.X -= delta;
                        break;
                    case Keys.Up:
                        CurrentWaypoint.position.Y -= delta;
                        break;
                    case Keys.Down:
                        CurrentWaypoint.position.Y += delta;
                        break;                        
                }
            }


            NewInvalidationRectangle = CalculateInvalidationRectangle(SelectedWaypoints);

            if (PreviousInvalidationRectangle.Count > 0)
            {
                panZoomBox1.Invalidate(Rectangle.Union(NewInvalidationRectangle, PreviousInvalidationRectangle.Dequeue()));
            }
            else
            {
                panZoomBox1.Invalidate(NewInvalidationRectangle);
            }

            PreviousInvalidationRectangle.Enqueue(NewInvalidationRectangle);
        }

        private void MoveWaypoints(Point CursorPosition)
        {
            float deltaX;
            float deltaY;
            Rectangle NewInvalidationRectangle;
            try
            {
                deltaX = ClientToWorldPosition(CursorPosition).X - ClientToWorldPosition(new Point(PreviousCursorPosition.X, PreviousCursorPosition.Y)).X;
                deltaY = ClientToWorldPosition(CursorPosition).Y - ClientToWorldPosition(new Point(PreviousCursorPosition.X, PreviousCursorPosition.Y)).Y;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            
            foreach (clsWaypoint CurrentWaypoint in SelectedWaypoints)
            {
                CurrentWaypoint.position.X += deltaX;
                CurrentWaypoint.position.Y += deltaY;
            }
             

            NewInvalidationRectangle = CalculateInvalidationRectangle(SelectedWaypoints);

            if (PreviousInvalidationRectangle.Count > 0)
            {
                panZoomBox1.Invalidate(Rectangle.Union(NewInvalidationRectangle, PreviousInvalidationRectangle.Dequeue()));
            }
            else
            {
                panZoomBox1.Invalidate(NewInvalidationRectangle);
            }

            PreviousInvalidationRectangle.Enqueue(NewInvalidationRectangle);
        }

        #endregion
        
        #region DrawingFunctions

        private void DrawCourses(PaintEventArgs e)
        {
            if (OnScreenWaypoints != null)
                OnScreenWaypoints.Clear();

            if (CourseManager == null)
                return;

            CourseDrawer.ZoomFactor = (float)panZoomBox1.ZoomFactor;
            foreach (clsSlot Slot in CourseManager.saves.slot)
            {
                List<clsWaypoint> NewWaypoints = null;
                if (Slot.course.IsDisplayed)
                    NewWaypoints = CourseDrawer.DrawCourse(Slot.course, e.Graphics, panZoomBox1.ClientRectangle, panZoomBox1.ViewPort, SelectedWaypoints, panZoomBox1.Quality);
                if (NewWaypoints != null && NewWaypoints.Count > 0)
                    OnScreenWaypoints.AddRange(NewWaypoints);
            }
        }

        private void DrawSelection(PaintEventArgs e)
        {
            if (SelectionArea.IsEmpty)
                return;
            using (Brush brush = new SolidBrush(Color.FromArgb(128, Color.AliceBlue)))
            {
                e.Graphics.FillRectangle(brush, SelectionArea);
            }
        }

        #endregion

        #region DrawingSupportFunctions

        private static Rectangle BufferedRectangleFromPoint(Point Point, int Buffer)
        {
            return new Rectangle(Point.X - Buffer, Point.Y - Buffer, Buffer * 2, Buffer * 2);
        }

        private void InvalidateWaypoint(clsWaypoint Waypoint)
        {
            panZoomBox1.Invalidate(BufferedRectangleFromPoint(WorldToClientPosition(Waypoint.position), WaypointBuffer));
        }

        private Rectangle CalculateInvalidationRectangle(List<clsWaypoint> Waypoints)
        {
            // Can't we just union a 6X6 rectangle around every point? Let's try that.
            Rectangle InvalidationRectangle = Rectangle.Empty;

            if (Waypoints.Count == 0)
                throw new ArgumentException();

            foreach (clsWaypoint CurrentWaypoint in Waypoints)
            {
                if (InvalidationRectangle == Rectangle.Empty)
                {
                    InvalidationRectangle = BufferedRectangleFromPoint(WorldToClientPosition(CurrentWaypoint.position), WaypointBuffer);
                }
                else
                {
                    InvalidationRectangle = Rectangle.Union(InvalidationRectangle, BufferedRectangleFromPoint(WorldToClientPosition(CurrentWaypoint.position), WaypointBuffer));
                }

                if (CurrentWaypoint.Previous != null && !Waypoints.Contains(CurrentWaypoint.Previous))
                    InvalidationRectangle = Rectangle.Union(InvalidationRectangle, BufferedRectangleFromPoint(WorldToClientPosition(CurrentWaypoint.Previous.position), WaypointBuffer));
                if (CurrentWaypoint.Next != null && !Waypoints.Contains(CurrentWaypoint.Next))
                    InvalidationRectangle = Rectangle.Union(InvalidationRectangle, BufferedRectangleFromPoint(WorldToClientPosition(CurrentWaypoint.Next.position), WaypointBuffer));
            }
            return InvalidationRectangle;
        }

#endregion
        
        #region PointManipulationFunctions

        private Point WorldToClientPosition(PointF WorldPosition)
        {
            Point ClientPosition = new Point();
            ClientPosition.X = (int)Math.Round((WorldPosition.X + (panZoomBox1.Image.Size.Width / 2) - panZoomBox1.ViewPort.X) * panZoomBox1.ZoomFactor);
            ClientPosition.Y = (int)Math.Round((WorldPosition.Y + (panZoomBox1.Image.Size.Height / 2) - panZoomBox1.ViewPort.Y) * panZoomBox1.ZoomFactor);
            return ClientPosition;
        }

        private PointF ClientToWorldPosition(Point ClientPosition)
        {
            PointF CursorWorldPosition = new PointF();
            if (panZoomBox1.Image != null)
            {
                try
                {
                    CursorWorldPosition.X = (float)Math.Round(((ClientPosition.X / panZoomBox1.ZoomFactor) + panZoomBox1.ViewPort.X) - panZoomBox1.Image.Size.Width / 2, 2);
                    CursorWorldPosition.Y = (float)Math.Round(((ClientPosition.Y / panZoomBox1.ZoomFactor) + panZoomBox1.ViewPort.Y) - panZoomBox1.Image.Size.Height / 2, 2);
                    return CursorWorldPosition;
                }
                catch (ArgumentNullException ex)
                {
                    throw ex;
                }
            }
            throw new ArgumentNullException("panZoomBox1.Image", "No Image Loaded");
        }

        private clsWaypoint GetWaypointAtClientPosition(Point ClientPosition)
        {
            try
            {
                List<clsWaypoint> CloseWaypoints = new List<clsWaypoint>();
                PointF CursorMapPosition = ClientToWorldPosition(ClientPosition);

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

                    foreach (clsWaypoint Waypoint in CloseWaypoints)
                    {
                        if (ClientPosition.X - Waypoint.position.X < DeltaX && ClientPosition.Y - Waypoint.position.Y < DeltaY)
                            ClosestWaypoint = Waypoint;
                    }
                    return ClosestWaypoint;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {

                throw ex;
            }
            
        }

        private RectangleF WorldRectangleFromClientRectangle(Rectangle ClientRectangle)
        {
            PointF WorldOrigin = ClientToWorldPosition(new Point(ClientRectangle.X, ClientRectangle.Y));
            PointF WorldOpposite = ClientToWorldPosition(new Point(ClientRectangle.Size.Width - ClientRectangle.X, ClientRectangle.Size.Height - ClientRectangle.Y));
            SizeF WorldSize = new SizeF(Math.Abs(WorldOrigin.X - WorldOpposite.X), Math.Abs(WorldOrigin.Y - WorldOpposite.Y));

            return new RectangleF(WorldOrigin, WorldSize);
        }
        #endregion

        #region ContexMenuFunctions

        #endregion

        #region DebugFunctions

        #if Debug

        private void UpdateDebugLabels(MouseEventArgs e)
        {
            if (panZoomBox1.Image != null)
            {
                try
                {
                    label1.Text = ClientToWorldPosition(new Point(e.X, e.Y)).ToString();
                    label2.Text = new Point(e.X, e.Y).ToString();
                    label3.Text = WorldToClientPosition(ClientToWorldPosition(new Point(e.X, e.Y))).ToString();
                }
                catch (ArgumentNullException ex)
                {
                    throw ex;
                }
            }
        }

        #endif

        #endregion
    }
}
