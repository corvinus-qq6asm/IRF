using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace UserMaintance
{
    public partial class Form1 : Form
    {
        BindingList<Entities.User> users = new BindingList<Entities.User>();
        public Form1()
        {
            InitializeComponent();

            label1.Text = Resource1.LastName;
            label2.Text = Resource1.FirstName;
            button1.Text = Resource1.Add;
            button2.Text = Resource1.WrtiteIntoFile;



            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new Entities.User()
            {
                LastName = textBox1.Text,
                FirstName = textBox2.Text
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog() == DialogResult.OK) return;
            using (StreamWriter sw=new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                foreach (var item in users)
                {
                    sw.Write(item.ID);
                    sw.Write(";");
                    sw.Write(item.FullName);
                    sw.WriteLine();
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var torlendo = listBox1.SelectedItem;
            if (torlendo!=null)
            {
                users.Remove((Entities.User)torlendo);
            }
        }
    }
}
