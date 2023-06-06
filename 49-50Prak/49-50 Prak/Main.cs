using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _49_50_Prak
{
    public partial class Main : Form
    {
        int lastTovar = 0;
        int ActivePictureBox = 0;
        PictureBox[] pictureArray = new PictureBox[6];
        Operations operations = new Operations();
        Tovars tovars = new Tovars();
        string Email;
        string Name;
        public Main(string email,string name)
        {
            InitializeComponent();
            Email = email;
            Name = name;
            richTextBox1.Text = "Здравствуйте " + name +" " + "\n" + "Сейчас: " + DateTime.Now;
            System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
            //MyTimer.Interval = (1000);
            //MyTimer.Tick += new EventHandler(MyTimer_Tick);
            //MyTimer.Start();
        }
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Text = "Привет " + Name + "\n" + "Сейчас: " + DateTime.Now;
            }
            catch (Exception) { }
        }

    private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form= new Profile(Email);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Refresh(Operations op)
        {
            operations = Operations.DeserializeXML();
        }
        private void Refresh(Tovars tv)
        {
            tovars = Tovars.DeserializeXML();
            LoadPictures();

        }
        private void LoadPictures()
        {
            if(tovars.Tovar_list.Count > 0 && pictureArray[0]!=null)
                for(int i = 0; i < 6; i++)
                {
                    if (lastTovar > tovars.Tovar_list.Count-1)
                        lastTovar = 0;
                    pictureArray[i].Load(tovars.Tovar_list[lastTovar].PNG_);
                    pictureArray[i].Image.Tag = tovars.Tovar_list[lastTovar];
                    lastTovar++;
                }
        }
        private void Main_Load(object sender, EventArgs e)
        {
            pictureArray[0] = pictureBox1;
            pictureArray[1] = pictureBox2;
            pictureArray[2] = pictureBox3;
            pictureArray[3] = pictureBox4;
            pictureArray[4] = pictureBox5;
            pictureArray[5] = pictureBox6;
            if (File.Exists("Operations.xml"))
                Refresh(operations);
            else Operations.SerializeXML(operations);
            if (File.Exists("Tovars.xml"))
                Refresh(tovars);
            else Tovars.SerializeXML(tovars);
            
        }
        private void getInfoPicture(PictureBox pictureBox)
        {
            Tovar tovar = pictureBox.Image.Tag as Tovar;
            textBoxPicture1.Text = tovar.Name_;
            textBoxPicture2.Text = tovar.CostRoznicha_.ToString();
            textBoxPicture3.Text = tovar.Kolvo_.ToString();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            getInfoPicture(pictureBox1);
            ActivePictureBox = 0;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            getInfoPicture(pictureBox2);
            ActivePictureBox = 1;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            getInfoPicture(pictureBox3);
            ActivePictureBox = 2;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            getInfoPicture(pictureBox4);
            ActivePictureBox = 3;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            getInfoPicture(pictureBox5);
            ActivePictureBox = 4;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            getInfoPicture(pictureBox6);
            ActivePictureBox = 5;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Tovar tovar = pictureArray[ActivePictureBox].Image.Tag as Tovar;
            Korzina korzina = new Korzina(KolvoForm(),tovar);
            Add(korzina);
        }
        private int KolvoForm()
        {
            using (var form = new KolvoForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return form.Kolvo;
                }
                else return 0;
            }
        }
        private void Add(Korzina korzina)
        {
            ListViewItem LVI = new ListViewItem(korzina.tovar.Name_);
            LVI.Tag = korzina;

            listView1.Items.Add(LVI);
            KorzinaCost();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                Korzina korzina = (Korzina)listView1.SelectedItems[0].Tag;
                if (korzina != null)
                {
                    textBoxKorzina1.Text = korzina.tovar.Name_;
                    textBoxKorzina2.Text = korzina.tovar.CostRoznicha_.ToString();
                    textBoxKorzina3.Text = korzina.kolvo.ToString();
                    pictureBoxKorzina1.Load(korzina.tovar.PNG_);
                }
            }
        }
        private void KorzinaCost()
        {
            int sum = 0;
            foreach(ListViewItem LVI in listView1.Items)
            {
                Korzina korzina = LVI.Tag as Korzina;
                sum += korzina.kolvo * korzina.tovar.CostRoznicha_;
            }
            labelKozinaCost.Text = sum.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LVI in listView1.Items)
            {
                Korzina korzina = LVI.Tag as Korzina;
                Operation operation = new Operation(Operations.LastId(operations),korzina.tovar.Articul_,DateTime.Now,korzina.tovar.CostRoznicha_,korzina.kolvo,2,Email);
                operations.Operation_list.Add(operation);
                File.Delete("Operations.xml");
                Operations.SerializeXML(operations);


                Tovar tovar = korzina.tovar;
                tovar.Kolvo_ = tovar.Kolvo_ - korzina.kolvo;
                ArticulClear(tovar.Articul_);
                tovars.Tovar_list.Add(tovar);
                File.Delete("Tovars.xml");
                Tovars.SerializeXML(tovars);
                this.Close();

            }
        }
        private void ArticulClear(int Articul) //Если находит совпадающий артикул, то удаляет
        {
            bool otv = true;
            for (int i = 0; i < tovars.Tovar_list.Count; i++)
            {
                if (tovars.Tovar_list[i].Articul_ == Articul)
                {
                    tovars.Tovar_list.RemoveAt(i);
                    return;
                }

            }
        }
        private void KorzinaClean()
        {
            textBoxKorzina1.Text= string.Empty;
            textBoxKorzina2.Text= string.Empty;
            textBoxKorzina3.Text= string.Empty;
            pictureBoxKorzina1.Image = null;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if(ConfirmationForm())
            if (listView1.SelectedItems.Count == 1)
            {
                Korzina korzina = (Korzina)listView1.SelectedItems[0].Tag;
                if (korzina != null)
                {
                    listView1.SelectedItems[0].Remove();
                }
                KorzinaClean();
                KorzinaCost();
            }
        }
        private bool ConfirmationForm()
        {
            using (var form = new Confirmation())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return form.otv;
                }
                else return false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            LoadPictures();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            HistoryRefresh();
        }
        private void HistoryRefresh()
        {
            listViewHistory.Items.Clear();
            foreach(Operation operation in operations.Operation_list)
            {
                if(operation.Other_ == Email)
                {
                    ListViewItem LVI = new ListViewItem(SearchTovar(operation.Articul_).Name_);
                    LVI.Tag = operation;
                    listViewHistory.Items.Add(LVI);
                }
            }
        }
        private Tovar SearchTovar(int articul)
        {
            foreach(Tovar tovar in tovars.Tovar_list)
            {
                if(tovar.Articul_ == articul)
                    return tovar;
            }
            return null;
        }
    }
}
