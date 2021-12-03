using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week09.Entities;
using System.IO;

namespace week09
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Users\ADMIN\Desktop\9edihet\nép.csv");
            BirthProbabilities = GetBirthProb(@"C:\Users\ADMIN\Desktop\9edihet\születés.csv");
            DeathProbabilities = GetDeathProb(@"C:\Users\ADMIN\Desktop\9edihet\halál.csv");

            Szimulacio();
        }

        private void Szimulacio()
        {
            for (int year = 2005; year < numericUpDown1.Value; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(year,Population[i]);
                }

                int maledb = (from x in Population
                              where x.Gender == Gender.Male && x.IsAlive == true
                              select x).Count();

                int femaledb = (from x in Population
                                where x.Gender == Gender.Female && x.IsAlive == true
                                select x).Count();
                Console.WriteLine(string.Format("Év:{0} Fiúk:{1} Lányok{2}", year, maledb, femaledb));

                DisplayResult(string.Format("Év:{0} Fiúk:{1} Lányok{2}", year, maledb, femaledb));
            }
        }
        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> popultaion = new List<Person>();
            StreamReader sr = new StreamReader(csvpath, Encoding.Default);
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine().Split(';');
                popultaion.Add(new Person()
                { 
                    BirthYear=int.Parse(line[0]),
                    Gender=(Gender)Enum.Parse(typeof(Gender),line[1]),
                    NumberOfChildren=int.Parse(line[2])
                });
            }
            return popultaion;
        }
        public List<BirthProbability> GetBirthProb(string csvpath)
        {
            List<BirthProbability> BirthProb = new List<BirthProbability>();
            StreamReader sr = new StreamReader(csvpath, Encoding.Default);
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine().Split(';');
                BirthProb.Add(new BirthProbability()
                {
                    kor = int.Parse(line[0]),
                    GyermekekSzama = int.Parse(line[1]),
                    SzValószínűség = double.Parse(line[2])
                });
            }
            return BirthProb;
        }
        public List<DeathProbability> GetDeathProb(string csvpath)
        {
            List<DeathProbability> DeathProb = new List<DeathProbability>();
            StreamReader sr = new StreamReader(csvpath, Encoding.Default);
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine().Split(';');
                DeathProb.Add(new DeathProbability()
                {
                    Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                    kor = int.Parse(line[1]),
                    HValószínűség = double.Parse(line[2])
                });
            }
            return DeathProb;
        }

        Random rng = new Random(1234);

        private void SimStep(int year, Person person)
        {
            if (!person.IsAlive) return;

            byte age = (byte)(year - person.BirthYear);

            double PDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.kor == age
                             select x.HValószínűség).FirstOrDefault();
            if (rng.NextDouble() <= PDeath)
                person.IsAlive = false;

            if (person.IsAlive&&person.Gender==Gender.Female)
            {
                double PBirth = (from x in BirthProbabilities
                                 where x.kor == age
                                 select x.SzValószínűség).FirstOrDefault();

                if (rng.NextDouble() <= PBirth)
                {
                    Person csecsemo = new Person();
                    csecsemo.BirthYear = year;
                    csecsemo.NumberOfChildren = 0;
                    csecsemo.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(csecsemo);
                }
            }


        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Szimulacio();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog odf = new OpenFileDialog();
            if (odf.ShowDialog()==DialogResult.OK)
            {
                var filepath = odf.FileName;
                var fileStream = odf.OpenFile();

                textBox1.Text = filepath;
            }
        }

        private void DisplayResult(string szoveg)
        {
            richTextBox1.Text = (szoveg);
        }

    }
}
