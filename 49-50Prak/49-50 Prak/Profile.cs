using GambitLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace _49_50_Prak
{
    
    public partial class Profile : Form
    {
        string Session; //уникальный ключ (Email) профиля
        Actions actions = new Actions();
        public Profile(string session)
        {
            InitializeComponent();
            actions = DeserializeXML();
            //Session = session;
            //  string conf = "Server=localhost;Database=49-50prak;Uid=root;pwd=;charset=utf-8;";
            //  SqlConnection connection = new SqlConnection(conf);
            //connection.Open();
            //if (connection.State != ConnectionState.Open)
            //  Console.WriteLine("Рот ебал этого соединения блять");
            if (File.Exists("private.xml") && File.Exists("public.xml")) //Проверка есть ли файлы ключей и при их остуствии создание новых
                GambitLib.Encryption.LoadKey();
            else GambitLib.Encryption.CreateKey();
            foreach (var action in actions.Action_list) //Перебор всех пользователей и поиск текущего по Email
            {
                if (Encryption.Decrypt(action.Email) == session)
                {
                    textBox1.Text = GambitLib.Encryption.Decrypt(action.Email);
                    textBox2.Text = GambitLib.Encryption.Decrypt(action.Password);
                    textBox4.Text = action.Name;
                    textBox5.Text = action.SureName;
                    comboBox1.Text = action.Sex;
                    dateTimePicker1.Value = action.BirthDate;
                    comboBox2.Text= action.Country;
                    checkBox1.Checked = action.Admin;
                    if (!action.Admin)
                        checkBox1.Visible = false;

                }
            }
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
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.AddYears(18) < DateTime.Now)
            {
                    if (PasswordIsValid(textBox2.Text))
                    {
                            if (IsValidEmail(textBox1.Text))
                            {
                                if (textBox4.Text != string.Empty && textBox5.Text != string.Empty && comboBox1.Text != string.Empty && comboBox2.Text != string.Empty)
                                {
                                    for (int i=0;i<actions.Action_list.Count ;i++) //Перебор всех пользователей и поиск текущего по Email
                                    {
                                        if (actions.Action_list[i].Email == textBox1.Text)
                                        {
                                            Action action2 = new Action(GambitLib.Encryption.Incrypt(textBox1.Text), GambitLib.Encryption.Incrypt(textBox2.Text),textBox4.Text,textBox5.Text,comboBox1.Text,dateTimePicker1.Value,comboBox2.Text,checkBox1.Checked);
                                            //bool IsNotExist = true; //Проверка на существование этого Email
                                            //foreach (Action action3 in actions.Action_list)
                                            //if (action2.Email == action3.Email)
                                            //      IsNotExist = false;
                                            //if(IsNotExist)
                                            actions.Action_list.RemoveAt(i);
                                            actions.Action_list.Add(action2);
                                            File.Delete("Actions.xml");
                                            SerializeXML(actions);
                                            this.Close();
                                            //break;
                                        }
                                    }
                                }
                                else
                                    MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show("Неверно введен email!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Пароль не соответствует требованиям!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Вам должно быть не меньше 18 лет!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private bool PasswordIsValid(string password)
        {
            var hasSymbols = new Regex(@"[!@#$%^]");
            if (password == null)
                return false;
            if (password.Length < 6)
                return false;
            if (!password.Any(x => char.IsDigit(x)))
                return false;
            if (!password.Any(x => char.IsUpper(x)))
                return false;
            if (!hasSymbols.IsMatch(password))
                return false;
            return true;
        }
        bool IsValidEmail(string email)
        {
            if (email.IndexOf(".") == -1)
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
    }
}
