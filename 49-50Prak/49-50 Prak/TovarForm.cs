using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _49_50_Prak
{
    public partial class TovarForm : Form
    {
        Tovars tovars = new Tovars();
        public TovarForm()
        {
            InitializeComponent();
            if (File.Exists("Tovars.xml"))
                tovars = Tovars.DeserializeXML();
            else Tovars.SerializeXML(tovars);
            //textBox1.Text = "#";
            //textBox2.Text = "#";
            //textBox3.Text = "#";
            textBox4.Text = "0";
            textBox5.Text = "0";
        }
        public TovarForm(Tovar tovar)
        {
            InitializeComponent();
            if (File.Exists("Tovars.xml"))
                tovars = Tovars.DeserializeXML();
            else Tovars.SerializeXML(tovars);
            textBox1.Text = tovar.Articul_.ToString();
            textBox1.ReadOnly = true;
            textBox2.Text = tovar.Name_;
            textBox3.Text = tovar.PNG_;
            textBox4.Text = tovar.CostRoznicha_.ToString();
            textBox5.Text = tovar.Kolvo_.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void button7_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox3.Text))
            {
                Tovar tovar = new Tovar(int.Parse(textBox1.Text), textBox2.Text, textBox3.Text, int.Parse(textBox4.Text), int.Parse(textBox5.Text));
                ArticulClear(tovar.Articul_);
                tovars.Tovar_list.Add(tovar);
                File.Delete("Operations.xml");
                Tovars.SerializeXML(tovars);
                this.Close();
            }
            else
                MessageBox.Show("Неверный путь до изображения","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
