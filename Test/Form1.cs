using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoControlLib;

namespace Test
{
    public partial class Form1 : Form
    {
        private AutoControl _auto;
        private MyData _data;
        public Form1()
        {
            InitializeComponent();

            _auto = new AutoControl()
            {
                Dock = DockStyle.Fill,
                AutoCommit = false
            };
            tableLayoutPanel1.Controls.Add(_auto, 0, 0);
            tableLayoutPanel1.SetColumnSpan(_auto, 3);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _data = new MyData()
            {
                FirstName = "Corey",
                LastName = "Derochie",
                Age = 29,
                LikesCheese = true
            };

            _auto.DataSource = _data;
            enableButtons();
            _auto.DirtyChanged += new AutoControl.DirtyChangeEvent(_auto_DirtyChanged);
        }

        void _auto_DirtyChanged(AutoControl ctrl)
        {
            enableButtons();
        }

        private void enableButtons()
        {
            _btnReset.Enabled = _auto.Dirty;
            _btnCommit.Enabled = _auto.Dirty;
        }

        private void _btnReset_Click(object sender, EventArgs e)
        {
            _auto.Reset();
        }

        private void _btnCommit_Click(object sender, EventArgs e)
        {
            _auto.Commit();
            string msg = string.Format("Name: {1}, {0}\nAge: {2}\n{3}",
                _data.FirstName, _data.LastName, _data.Age, _data.LikesCheese ? string.Format("They like {0}.", _data.FaveCheese.GetDisplayNameAttribute().DisplayName) : "They don't like cheese.");
            MessageBox.Show(msg);
        }
    }

    class MyData
    {
        [AutoControlLib.DisplayName("Given Name")]
        public string FirstName
        {
            get;
            set;
        }

        [AutoControlLib.DisplayName("Surname")]
        public string LastName
        {
            get;
            set;
        }

        [AutoControlLib.DisplayName("Age")]
        public int Age
        {
            get;
            set;
        }

        [AutoControlLib.DisplayName("Likes cheese")]
        public bool LikesCheese
        {
            get;
            set;
        }

        public enum CheeseType
        {
            [AutoControlLib.DisplayName("Swiss Cheese")] Swiss,
            [AutoControlLib.DisplayName("Mozzarella Cheese")] Mozza,
            [AutoControlLib.DisplayName("Cheddar Cheese")] Cheddar
        }

        [AutoControlLib.DisplayName("Favourite cheese")]
        public CheeseType FaveCheese
        {
            get;
            set;
        }
    }
}
