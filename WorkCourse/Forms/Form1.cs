﻿using System;
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
        public Form1()
        {
            InitializeComponent();
        }

        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = " BMP|*.bmp";
            dlg.InitialDirectory = Application.StartupPath;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                panZoomBox1.Image = Image.FromFile(dlg.FileName);
            }
        }

        private void loadCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = " Xml|courseManager.xml";
            dlg.InitialDirectory = Application.StartupPath;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CourseManager =  clsXmlSaveLoad.DeserializeXmlFromFile<clsCourseManager>(dlg.FileName);
                CourseManager.Path = Path.GetDirectoryName(dlg.FileName);
                CourseManager.LoadCourses();
            }
        }
    }
}
