using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week08
{
    public partial class Form1 : Form
    {


        List<Abstractions.Toy> _toys = new List<Abstractions.Toy>();

        private Abstractions.Toy _nextToy;

        Abstractions.IToyFactory _factory;
        Abstractions.IToyFactory Factory
        {
            get {   return _factory; }
            set {   
                    _factory = value;
                    DisplayNext();                    
                }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new Entities.BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            mainPanel.Controls.Add(toy);
            toy.Left = -toy.Width;
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var toy in _toys)
            {
                toy.MoveToy();
                if (toy.Left > maxPosition)
                    maxPosition = toy.Left;
            }

            if (maxPosition > 1000)
            {
                var oldestToy = _toys[0];
                mainPanel.Controls.Add(oldestToy);
                _toys.Remove(oldestToy);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Factory = new Entities.CarFactory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factory = new Entities.BallFactory
            {
                BallColor = button3.BackColor
            };
        }

        private void DisplayNext()
        {
            if (_nextToy!=null)
            {
                Controls.Remove(_nextToy);
            }

            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var colourPicker = new ColorDialog();
            colourPicker.Color = button.BackColor;
            if (colourPicker.ShowDialog() != DialogResult.OK)
                return;
            button3.BackColor = colourPicker.Color;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Factory = new Abstractions.PresentFactory
            {
                Colour1 = button3.BackColor,
                Colour2 = button3.BackColor
            };
        }
    }
}
