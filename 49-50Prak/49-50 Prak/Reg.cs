using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using GambitLib;

namespace _49_50_Prak
{
    public partial class Reg : Form
    {
        Actions actions= new Actions();
        public Reg()
        {
            InitializeComponent();
            textBox3.UseSystemPasswordChar = true;
            try
            {
            actions = DeserializeXML();
            }
            catch(Exception) { }
            if (File.Exists("private.xml") && File.Exists("public.xml")) //Проверка есть ли файлы ключей и при их остуствии создание новых
                GambitLib.Encryption.LoadKey();
            else
            {
                GambitLib.Encryption.CreateKey();
                GambitLib.Encryption.LoadKey();
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool EmailIsFree = true;
            for(int i=0;i<actions.Action_list.Count; i++)
                if (GambitLib.Encryption.Decrypt(actions.Action_list[i].Email) == textBox1.Text)
                    EmailIsFree = false;
            if (dateTimePicker1.Value.AddYears(18) < DateTime.Now)
            {
                if (EmailIsFree)
                {
                    if(PasswordIsValid(textBox2.Text))
                    {
                        if(textBox2.Text == textBox3.Text)
                        {
                            if (IsValidEmail(textBox1.Text))
                            {
                                if (textBox4.Text != string.Empty && textBox5.Text != string.Empty && comboBox1.Text != string.Empty && comboBox2.Text != string.Empty)
                                {
                                    Action action = new Action(GambitLib.Encryption.Incrypt(textBox1.Text), GambitLib.Encryption.Incrypt(textBox2.Text),textBox4.Text,textBox5.Text,comboBox1.Text,dateTimePicker1.Value,comboBox2.Text,checkBox1.Checked);
                                    actions.Action_list.Add(action);
                                    SerializeXML(actions);
                                    this.Close();
                                }
                                else
                                    MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show("Неверно введен email!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show("Пароли должны совпадать!", "Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Пароль не соответствует требованиям!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Этот Email уже занят!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Вам должно быть не меньше 18 лет!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private bool PasswordIsValid(string password)
        {
            var hasSymbols = new Regex(@"[!@#$%^]");
            if (password == null)
                return false;
            if (password.Length<6)
                return false;
            if (!password.Any(x => char.IsDigit(x)))
                return false;
            if (!password.Any(x => char.IsUpper(x)))
                return false;
            if (!hasSymbols.IsMatch(password))
                return false;
            return true;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    Action action = new Action(textBox1.Text, textBox2.Text, textBox3.Text);
        //    Add(action);
        //    clean();
        //}
        public void clean()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
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

        bool IsValidEmail(string email)
        {
            if(email.IndexOf(".") == -1) 
                return false;
            if (email.IndexOf("@") == -1)
                return false;
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
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

    }

}
