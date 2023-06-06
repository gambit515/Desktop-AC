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

namespace _49_50_Prak
{
    public partial class ProviderForm : Form
    {
        Providers providers = new Providers();
        public ProviderForm()
        {
            InitializeComponent();
            if (File.Exists("Providers.xml"))
                providers = Providers.DeserializeXML();
            else Providers.SerializeXML(providers);
        }
        public ProviderForm(Provider provider)
        {
            InitializeComponent();
            if (File.Exists("Providers.xml"))
                providers = Providers.DeserializeXML();
            else Providers.SerializeXML(providers);
            textBox1.Text = provider.Number_.ToString();
            textBox2.Text = provider.Name_;
            comboBox2.Text = provider.Country_;
            textBox3.Text = provider.City_;
            textBox4.Text = provider.Street_;
            textBox5.Text = provider.Phone_;
            textBox6.Text = provider.FIO_;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void NumberClear(int Number) //Если находит совпадающий артикул, то удаляет
        {
            bool otv = true;
            for (int i = 0; i < providers.Provider_list.Count; i++)
            {
                if (providers.Provider_list[i].Number_ == Number)
                {
                    providers.Provider_list.RemoveAt(i);
                    return;
                }

            }
        }
        private void button7_Click_1(object sender, EventArgs e)
        {
            Provider provider = new Provider(int.Parse(textBox1.Text),textBox2.Text,comboBox2.Text,textBox3.Text,textBox4.Text,textBox5.Text,textBox6.Text);
            NumberClear(provider.Number_);
            providers.Provider_list.Add(provider);
            File.Delete("Providers.xml");
            Providers.SerializeXML(providers);
            this.Close();
        }
    }
}
