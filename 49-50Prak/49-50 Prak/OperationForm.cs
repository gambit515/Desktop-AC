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
    public partial class OperationForm : Form
    {
        Operations operations = new Operations(); //Создание новой операции
        Tovars tovars= new Tovars();  
        Providers providers= new Providers();
        public OperationForm()
        {
            InitializeComponent();
            if (File.Exists("Operations.xml")) //Подгрузка серриализации Operations
                operations = Operations.DeserializeXML();
            else Operations.SerializeXML(operations); 
            if (File.Exists("Tovars.xml")) //Подгрузка серриализации Tovars
                tovars = Tovars.DeserializeXML();
            else Tovars.SerializeXML(tovars);
            if (File.Exists("Providers.xml")) //Подгрузка серриализации Providers
                providers = Providers.DeserializeXML();
            else Providers.SerializeXML(providers);
            foreach (Tovar tovar in tovars.Tovar_list)
            {
                comboBox2.Items.Add(tovar.Articul_);
            }
            foreach (Provider provider in providers.Provider_list)
            {
                comboBox3.Items.Add(provider.Number_);
            }
        }
        public OperationForm(Operation operation_red) //Редактирование уже существующей
        {
            InitializeComponent();
            if (File.Exists("Operations.xml"))
                operations = Operations.DeserializeXML();
            else Operations.SerializeXML(operations);
            if (File.Exists("Tovars.xml")) //Подгрузка серриализации Tovars
                tovars = Tovars.DeserializeXML();
            else Tovars.SerializeXML(tovars);
            if (File.Exists("Providers.xml")) //Подгрузка серриализации Providers
                providers = Providers.DeserializeXML();
            else Providers.SerializeXML(providers);
            comboBox2.Text = operation_red.Articul_.ToString();
            //textBox2.ReadOnly = true;
            dateTimePicker1.Value = operation_red.Prihod_Rashod_;
            textBox4.Text = operation_red.Cost_.ToString();
            textBox5.Text = operation_red.Kolvo_.ToString();
            switch (operation_red.OperationCode_) //Расшифровка код в слова
            {
                case 0:
                    comboBox1.Text = "Списание";
                    break;
                case 1:
                    comboBox1.Text = "Приход";
                    break;
                case 2:
                    comboBox1.Text = "Расход";
                    break;
            }
            textBoxOperation4.Text = operation_red.Other_;
            foreach (Tovar tovar in tovars.Tovar_list)
            {
                comboBox2.Items.Add(tovar.Articul_);
            }
            foreach (Provider provider in providers.Provider_list)
            {
                comboBox3.Items.Add(provider.Number_);
            }
        }
        private void IdClear(int Id) //Если находит совпадающий артикул, то удаляет
        {
            bool otv = true;
            for (int i = 0; i < operations.Operation_list.Count; i++)
            {
                if (operations.Operation_list[i].Id_ == Id)
                {
                    operations.Operation_list.RemoveAt(i);
                    return;
                }

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
        private void button7_Click(object sender, EventArgs e)
        {
            int type = -1;
            Operation operation= null;
            switch (comboBox1.Text)
            {
                case "Списание":
                    type = 0;
                    operation = new Operation(Operations.LastId(operations), int.Parse(comboBox2.Text), dateTimePicker1.Value, int.Parse(textBox4.Text), int.Parse(textBox5.Text), type, textBoxOperation4.Text);
                    break;
                case "Приход":
                    type = 1;
                    operation = new Operation(Operations.LastId(operations), int.Parse(comboBox2.Text), dateTimePicker1.Value, int.Parse(textBox4.Text), int.Parse(textBox5.Text), type, comboBox3.Text);
                    break;
                case "Расход":
                    type = 2;
                    operation = new Operation(Operations.LastId(operations), int.Parse(comboBox2.Text), dateTimePicker1.Value, int.Parse(textBox4.Text), int.Parse(textBox5.Text), type, textBoxOperation4.Text);
                    break;
            }
            
            IdClear(operation.Id_);
            operations.Operation_list.Add(operation);
            File.Delete("Operations.xml");
            Operations.SerializeXML(operations);
            Tovar tovar = SelectTovar(comboBox2.Text); //Поиск ссылки на нужный нам товара
            switch (type)
            {
                case 0:
                    tovar.CostRoznicha_ = operation.Cost_;
                    tovar.Kolvo_ = tovar.Kolvo_ - operation.Kolvo_;
                    break;
                case 1:
                    tovar.CostRoznicha_ = operation.Cost_;
                    tovar.Kolvo_ = tovar.Kolvo_ + operation.Kolvo_;
                    break;
                case 2:
                    tovar.CostRoznicha_ = operation.Cost_;
                    tovar.Kolvo_ = tovar.Kolvo_ - operation.Kolvo_;
                    break;
            }
            ArticulClear(tovar.Articul_);
            tovars.Tovar_list.Add(tovar);
            File.Delete("Tovars.xml");
            Tovars.SerializeXML(tovars);
            this.Close();
        }

        private void OperationForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tovar tovar = SelectTovar(comboBox2.Text);
            textBoxTovar1.Text = tovar.Name_;
            textBoxTovar2.Text = tovar.PNG_;
            textBoxTovar3.Text = tovar.CostRoznicha_.ToString();
            textBoxTovar4.Text = tovar.Kolvo_.ToString();
        }
        private Tovar SelectTovar(string str)
        {
            foreach (Tovar tovar in tovars.Tovar_list)
            {
                if (int.Parse(str) == tovar.Articul_)
                {
                    return tovar;
                }
            }
            return null;
        }
        private Provider SelectProvider(string str)
        {
            foreach (Provider provider in providers.Provider_list)
            {
                if (int.Parse(str) == provider.Number_)
                {
                    return provider;
                }
            }
            return null;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new TovarForm();
            form.ShowDialog();
            Refresh(tovars);
        }
        private void Refresh(Tovars to)
        {
            tovars = Tovars.DeserializeXML();
            comboBox2.Items.Clear();
            textBoxTovar1.Text = "";
            textBoxTovar2.Text = "";
            textBoxTovar3.Text = "";
            textBoxTovar4.Text = "";
            foreach (Tovar tovar in tovars.Tovar_list)
            {
                comboBox2.Items.Add(tovar.Articul_);
            }
        }
        private void Refresh(Providers pr)
        {
            providers = Providers.DeserializeXML();
            comboBox3.Items.Clear();
            textBoxProvider1.Text = "";
            textBoxProvider2.Text = "";
            textBoxProvider3.Text = "";
            textBoxProvider4.Text = "";
            textBoxProvider5.Text = "";
            textBoxProvider6.Text = "";
            foreach (Provider provider in providers.Provider_list)
            {
                comboBox3.Items.Add(provider.Number_);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)//Изменение отображения надписи и checkbox
        {
            if(comboBox1.Text != "Приход")
            {
                comboBox3.Visible = false;
                textBoxOperation4.Visible = true;
                //Provider provider = SelectProvider(comboBox3.Text);
                textBoxProvider1.Text = "";
                textBoxProvider2.Text = "";
                textBoxProvider3.Text = "";
                textBoxProvider4.Text = "";
                textBoxProvider5.Text = "";
                textBoxProvider6.Text = "";
                if(comboBox1.Text == "Списание") 
                {
                    labelProvider1.Visible =false;
                    labelProvider2.Visible = false;
                    labelProvider3.Visible = false;
                    labelProvider4.Visible = true;
                }
                else
                {
                    labelProvider1.Visible = false;
                    labelProvider2.Visible = false;
                    labelProvider3.Visible = true;
                    labelProvider4.Visible = false;
                }
            }
            else
            {
                comboBox3.Visible = true;
                textBoxOperation4.Visible = false;
                labelProvider1.Visible = false;
                labelProvider2.Visible = true;
                labelProvider3.Visible = false;
                labelProvider4.Visible = false;
                if (comboBox3.Text != string.Empty)
                {
                    Provider provider = SelectProvider(comboBox3.Text);
                    ProviderPaste(provider);
                }
            }
        }
        private void ProviderPaste(Provider provider) //Заполнение полей дилера
        {
            textBoxProvider1.Text = provider.Name_;
            textBoxProvider2.Text = provider.Country_;
            textBoxProvider3.Text = provider.City_;
            textBoxProvider4.Text = provider.Street_;
            textBoxProvider5.Text = provider.Phone_;
            textBoxProvider6.Text = provider.FIO_;
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != string.Empty)
            {
                Provider provider = SelectProvider(comboBox3.Text);
                ProviderPaste(provider);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new ProviderForm();
            form.ShowDialog();
            Refresh(providers);
        }
    }
}
