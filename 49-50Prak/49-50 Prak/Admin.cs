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
using System.Xml.Serialization;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _49_50_Prak
{
    public partial class Admin : Form
    {
        Tovars tovars = new Tovars();
        Providers providers = new Providers();
        Operations operations = new Operations();
        Actions actions = new Actions();
        public Admin(string email)
        {
            InitializeComponent();

        }
        private void GlobalRefresh()
        {
            Refresh(tovars);
            Refresh(providers);
            Refresh(operations);
            Refresh(actions);
        }
        private void Refresh(Actions ac) //Обновление ListView1
        {
            if (File.Exists("Actions.xml"))
            {
                listView1.Clear();
                actions = DeserializeXML();
                foreach (Action action in actions.Action_list)
                {
                    Add(action);
                }
            }
        }
        private void Refresh(Operations op) //Обновление ListView2
        {
            if (File.Exists("Operations.xml"))
            {
                listView2.Clear();
                operations = Operations.DeserializeXML();
                foreach (Operation operation in operations.Operation_list)
                {
                    Add(operation);
                }
            }
        }
        private void Refresh(Operations op, string searchingSTR) //Обновление ListView2
        {
            if (File.Exists("Operations.xml"))
            {
                listView2.Clear();
                operations = Operations.DeserializeXML();
                foreach (Operation operation in operations.Operation_list)
                {
                    if ((operation.Articul_.ToString() + " " + DateVivod(operation.Prihod_Rashod_)).ToString().IndexOf(searchingSTR)!=-1)
                    Add(operation);
                }
            }
        }
        private void Refresh(Operations op, string searchingSTR, string ProviderNumber) //Обновление ListView2
        {
            if (File.Exists("Operations.xml"))
            {
                listView2.Clear();
                operations = Operations.DeserializeXML();
                foreach (Operation operation in operations.Operation_list)
                {
                    if ((operation.Articul_.ToString() + " " + DateVivod(operation.Prihod_Rashod_)).ToString().IndexOf(searchingSTR) != -1)
                        if (operation.OperationCode_ == 0)
                        if(operation.Other_ == ProviderNumber)
                            Add(operation);
                }
            }
        }
        private void Refresh(Operations op, string searchingSTR, DateTime FirstDate, DateTime SecondDate) //Обновление ListView2
        {
            if (File.Exists("Operations.xml"))
            {
                listView2.Clear();
                operations = Operations.DeserializeXML();
                foreach (Operation operation in operations.Operation_list)
                {
                    if ((operation.Articul_.ToString() + " " + DateVivod(operation.Prihod_Rashod_)).ToString().IndexOf(searchingSTR) != -1)
                        if (operation.Prihod_Rashod_.IsInRange(FirstDate, SecondDate))
                        Add(operation);
                }
            }
        }
        private void Refresh(Operations op, string searchingSTR, string ProviderNumber, DateTime FirstDate, DateTime SecondDate) //Обновление ListView2
        {
            if (File.Exists("Operations.xml"))
            {
                listView2.Clear();
                operations = Operations.DeserializeXML();
                foreach (Operation operation in operations.Operation_list)
                {
                    if ((operation.Articul_.ToString() + " " + DateVivod(operation.Prihod_Rashod_)).ToString().IndexOf(searchingSTR) != -1)
                        if (operation.Prihod_Rashod_.IsInRange(FirstDate,SecondDate))
                        if (operation.OperationCode_ == 0)
                            if (operation.Other_ == ProviderNumber)
                                Add(operation);
                }
            }
        }
        private void Refresh(Tovars to) //Обновление ListView3
        {
            if (File.Exists("Tovars.xml"))
            {
                listView3.Clear();
                tovars = Tovars.DeserializeXML();
                foreach (Tovar tovar in tovars.Tovar_list)
                {
                    Add(tovar);
                }
            }
        }
        private void Refresh(Tovars to,string searchingSTR) //Обновление ListView3
        {
            if (File.Exists("Tovars.xml"))
            {
                listView3.Clear();
                tovars = Tovars.DeserializeXML();
                foreach (Tovar tovar in tovars.Tovar_list)
                {
                    if(tovar.Name_.IndexOf(searchingSTR) !=-1)
                        Add(tovar);
                }
            }
        }
        private void Refresh(Tovars to, string searchingSTR, int MinOst, int MaxOst) //Обновление ListView3
        {
            if (File.Exists("Tovars.xml"))
            {
                listView3.Clear();
                tovars = Tovars.DeserializeXML();
                foreach (Tovar tovar in tovars.Tovar_list)
                {
                    if (tovar.Name_.IndexOf(searchingSTR) != -1 && (tovar.CostRoznicha_ >= MinOst && tovar.CostRoznicha_ <= MaxOst))
                        Add(tovar);
                }
            }
        }
        private void Refresh(Providers pr) //Обновление ListView4
        {
            if (File.Exists("Providers.xml"))
            {
                listView4.Clear();
                providers = Providers.DeserializeXML();
                foreach (Provider provider in providers.Provider_list)
                {
                    Add(provider);
                }
            }
        }
        private void Refresh(Providers pr, string searchingSTR) //Обновление ListView4
        {
            if (File.Exists("Providers.xml"))
            {
                listView4.Clear();
                providers = Providers.DeserializeXML();
                foreach (Provider provider in providers.Provider_list)
                {
                    if (provider.FIO_.IndexOf(searchingSTR) != -1)
                        Add(provider);
                }
            }
        }
        private void Refresh(Providers pr, string searchingSTR, string filterSTR) //Обновление ListView4
        {
            if (File.Exists("Providers.xml"))
            {
                listView4.Clear();
                providers = Providers.DeserializeXML();
                foreach (Provider provider in providers.Provider_list)
                {
                    if (provider.FIO_.IndexOf(searchingSTR) != -1 && provider.City_.IndexOf(filterSTR) != -1)
                        Add(provider);
                }
            }
        }
        private void Add(Action action)
        {
            ListViewItem LVI = new ListViewItem(GambitLib.Encryption.Decrypt(action.Email));
            LVI.Tag = action;

            listView1.Items.Add(LVI);
        }
        private void Add(Operation operation)
        {
            //ListViewItem LVI = new ListViewItem(operation.Articul_.ToString());
            ListViewItem LVI = new ListViewItem(operation.Articul_.ToString() +" "+DateVivod(operation.Prihod_Rashod_));
            LVI.Tag = operation;

            listView2.Items.Add(LVI);
        }
        private void Add(Tovar tovar)
        {
            ListViewItem LVI = new ListViewItem(tovar.Name_.ToString());
            LVI.Tag = tovar;

            listView3.Items.Add(LVI);
        }
        private void Add(Provider provider)
        {
            ListViewItem LVI = new ListViewItem(provider.FIO_.ToString());
            LVI.Tag = provider;

            listView4.Items.Add(LVI);
        }
        private void Clean(Operations operations)
        {
            textBoxOperation1.Text = "";
            textBoxOperation2.Text = "";
            textBoxOperation3.Text = "";
            textBoxOperation4.Text = "";
            textBoxOperation5.Text = "";
            textBoxOperation6.Text = "";
        }
        private void Clean(Tovars tovars)
        {
            textBoxTovar1.Text = "";
            textBoxTovar2.Text = "";
            textBoxTovar3.Text = "";
            textBoxTovar4.Text = "";
            textBoxTovar5.Text = "";
        }
        private void Clean(Providers providers)
        {
            textBoxProvider1.Text = "";
            textBoxProvider2.Text = "";
            textBoxProvider3.Text = "";
            textBoxProvider4.Text = "";
            textBoxProvider5.Text = "";
            textBoxProvider6.Text = "";
            textBoxProvider7.Text = "";
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                Action action = (Action)listView1.SelectedItems[0].Tag;
                if (action != null)
                {
                    
                }
            }
            else if (listView1.SelectedItems.Count == 0)
            {

            }
        }
        private Actions DeserializeXML()
        {
            XmlSerializer xml = new XmlSerializer(typeof(Actions));

            using (FileStream fs = new FileStream("Actions.xml", FileMode.OpenOrCreate))
            {
                return (Actions)xml.Deserialize(fs);
            }
        }
        private void SerializeXML(Actions actions)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Actions));

            using (FileStream fs = new FileStream("Actions.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, actions);
            }
        }
        public class Actions
        {
            public List<Action> Action_list { get; set; } = new List<Action>();
        }

        public class Action
        {
            public string Email;
            public string Password;
            public string Name;
            public string SureName;
            public string Sex;
            public bool Admin;
            public DateTime BirthDate;
            public string Country;
            public Action()
            { // конструктор по умолчанию
                Email = ""; Password = ""; Name = ""; SureName = ""; Sex = ""; Admin = false; BirthDate = DateTime.MinValue; Country = "";
            }
            public Action(string Email, string Password, string Name, string SureName, string Sex, DateTime BirthDate, string Country, bool Admin)
            { // конструктор с параметрами
                this.Email = Email; this.Password = Password; this.Name = Name; this.SureName = SureName; this.Sex = Sex; this.BirthDate = BirthDate; this.Country = Country;
                this.Admin = Admin;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                Action action = (Action)listView1.SelectedItems[0].Tag;
                if (action != null)
                {
                    for (int i = 0; i < actions.Action_list.Count; i++) //Перебор всех пользователей и поиск текущего по Email
                    {
                        if (actions.Action_list[i].Email == GambitLib.Encryption.Decrypt(action.Email))
                        {
                            if (ConfirmationForm())
                            {
                                actions.Action_list.RemoveAt(i);
                                File.Delete("Actions.xml");
                                SerializeXML(actions);
                                Refresh(actions);
                                break;
                            }
                        }
                    }
                }
            }
            else if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Не выбран пользователь", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                Action action = (Action)listView1.SelectedItems[0].Tag;
                if (action != null)
                {
                    Form form = new Profile(GambitLib.Encryption.Decrypt(action.Email));
                    this.Hide();
                    form.ShowDialog();
                    this.Show();
                }
            }
            Refresh(actions);
            
        }

        private void btnClose(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form form = new Reg();
            this.Hide();
            form.ShowDialog();
            this.Show();
            Refresh(actions);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form operationForm = new OperationForm();
            this.Hide();
            operationForm.ShowDialog();
            this.Show();
            GlobalRefresh();
            Clean(operations);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 1)
            {
                Operation operation = (Operation)listView2.SelectedItems[0].Tag;
                if (operation != null)
                {
                    Form operationForm = new OperationForm(operation);
                    this.Hide();
                    operationForm.ShowDialog();
                    this.Show();
                    GlobalRefresh();
                    Clean(operations);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 1)
            {
                Operation operation = (Operation)listView2.SelectedItems[0].Tag;
                if (operation != null)
                {
                    for(int i = 0; i < operations.Operation_list.Count; i++)
                        if (operations.Operation_list[i].Articul_ == operation.Articul_)
                            if (ConfirmationForm())
                            {
                                operations.Operation_list.RemoveAt(i);
                                File.Delete("Operations.xml");
                                Operations.SerializeXML(operations);
                                Refresh(operations);
                                Clean(operations);
                                break;
                            }

                }
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

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 1)
            {
                Operation operation = (Operation)listView2.SelectedItems[0].Tag;
                if (operation != null)
                {
                    textBoxOperation1.Text = operation.Articul_.ToString();
                    textBoxOperation2.Text = DateVivod(operation.Prihod_Rashod_);
                    textBoxOperation3.Text = operation.Cost_.ToString();
                    textBoxOperation4.Text = operation.Kolvo_.ToString();
                    switch (operation.OperationCode_)
                    {
                        case 0:
                            textBoxOperation5.Text = "Списание";
                            break;
                        case 1:
                            textBoxOperation5.Text = "Приход";
                            break;
                        case 2:
                            textBoxOperation5.Text = "Расход";
                            break;
                    }
                    textBoxOperation6.Text = operation.Other_;
                }
            }
        }
        
        private string DateVivod(DateTime DT)
        {
            string str = "";
            str = +DT.Day + "/" + DT.Month + "/" + DT.Year;
            return str;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form TovarForm = new TovarForm();
            this.Hide();
            TovarForm.ShowDialog();
            this.Show();
            Refresh(tovars);
            Clean(tovars);
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count == 1)
            {
                Tovar tovar = (Tovar)listView3.SelectedItems[0].Tag;
                if (tovar != null)
                {
                    textBoxTovar1.Text = tovar.Articul_.ToString();
                    textBoxTovar2.Text = tovar.Name_;
                    textBoxTovar3.Text = tovar.PNG_;
                    textBoxTovar4.Text = tovar.CostRoznicha_.ToString();
                    textBoxTovar5.Text = tovar.Kolvo_.ToString();
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count == 1)
            {
                Tovar tovar = (Tovar)listView3.SelectedItems[0].Tag;
                if (tovar != null)
                {
                    Form TovarForm = new TovarForm(tovar);
                    this.Hide();
                    TovarForm.ShowDialog();
                    this.Show();
                    Refresh(tovars);
                    Clean(tovars);
                }
            }
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count == 1)
            {
                Tovar tovar = (Tovar)listView3.SelectedItems[0].Tag;
                if (tovar != null)
                {
                    for (int i = 0; i < tovars.Tovar_list.Count; i++)
                        if (tovars.Tovar_list[i].Articul_ == tovar.Articul_)
                            if (ConfirmationForm())
                            {
                                tovars.Tovar_list.RemoveAt(i);
                                File.Delete("Tovars.xml");
                                Tovars.SerializeXML(tovars);
                                Refresh(tovars);
                                Clean(tovars);
                            }
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form ProviderForm = new ProviderForm();
            this.Hide();
            ProviderForm.ShowDialog();
            this.Show();
            Refresh(providers);
            Clean(providers);
        }

        private void listView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView4.SelectedItems.Count == 1)
            {
                Provider provider = (Provider)listView4.SelectedItems[0].Tag;
                if (provider != null)
                {
                    textBoxProvider1.Text = provider.Number_.ToString();
                    textBoxProvider2.Text = provider.Name_;
                    textBoxProvider3.Text = provider.Country_;
                    textBoxProvider4.Text = provider.City_;
                    textBoxProvider5.Text = provider.Street_;
                    textBoxProvider6.Text = provider.Phone_;
                    textBoxProvider7.Text = provider.FIO_;
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (listView4.SelectedItems.Count == 1)
            {
                Provider provider = (Provider)listView4.SelectedItems[0].Tag;
                if (provider != null)
                {
                    Form ProviderForm = new ProviderForm(provider);
                    this.Hide();
                    ProviderForm.ShowDialog();
                    this.Show();
                    Refresh(providers);
                    Clean(providers);
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (listView4.SelectedItems.Count == 1)
            {
                Provider provider = (Provider)listView4.SelectedItems[0].Tag;
                if (provider != null)
                {
                    for (int i = 0; i < providers.Provider_list.Count; i++)
                        if (providers.Provider_list[i].Number_ == provider.Number_)
                            if (ConfirmationForm())
                            {
                                providers.Provider_list.RemoveAt(i);
                                File.Delete("Providers.xml");
                                Providers.SerializeXML(providers);
                                Refresh(providers);
                                Clean(providers);
                            }
                }
            }
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            if (File.Exists("Providers.xml"))
                Refresh(providers);
            else Providers.SerializeXML(providers);
            if (File.Exists("Actions.xml"))
                Refresh(actions);
            if (File.Exists("Operations.xml"))
                Refresh(operations);
            else Operations.SerializeXML(operations);
            if (File.Exists("Tovars.xml"))
                Refresh(tovars);
            else Tovars.SerializeXML(tovars);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (textBoxTovarFilter1.Text != string.Empty && textBoxTovarFilter2.Text != string.Empty)
                Refresh(tovars, textBoxTovarSearch.Text,int.Parse(textBoxTovarFilter1.Text),int.Parse(textBoxTovarFilter2.Text));
            else
                Refresh(tovars, textBoxTovarSearch.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && textBoxOperationFilter3.Text != string.Empty)
                Refresh(operations, textBoxOperationSearch.Text, textBoxOperationFilter3.Text, dateTimePicker1.Value, dateTimePicker2.Value);
            else
                if (checkBox1.Checked)
                Refresh(operations, textBoxOperationSearch.Text, dateTimePicker1.Value, dateTimePicker2.Value);
            else
                if (textBoxOperationFilter3.Text != string.Empty)
                Refresh(operations, textBoxOperationSearch.Text, textBoxOperationFilter3.Text);
            else
                Refresh(operations, textBoxOperationSearch.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (comboBoxProviderFilter.Text != string.Empty)
                Refresh(providers, textBoxProviderSearch.Text,comboBoxProviderFilter.Text);
            else
                Refresh(providers,textBoxProviderSearch.Text);
        }
        
    }
    public static class DateTimeExtensions
    {
        public static bool IsInRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck < endDate;
        }
    }
}
