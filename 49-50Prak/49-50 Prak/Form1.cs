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
using static _49_50_Prak.Reg;
using System.Xml.Serialization;
using static _49_50_Prak.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;

namespace _49_50_Prak
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
        bool IsBanned = false;
        int TimeToUnbanned = 60;
        int popitka =0;
        string text;
        Actions actions = new Actions();
        public Form1()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
            if (File.Exists("Actions.xml"))
                actions = DeserializeXML();
            GambitLib.Encryption.LoadKey();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form reg = new Reg();
            reg.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Form form = new Main("amoskin515@gmail.com", "какашка");
            //Form form = new Admin("amoskin3515@gmail.com");
            //form.ShowDialog();
            if (!IsBanned)
                if(textBox3.Text == text)
                {
                    actions = DeserializeXML();
                    string email = textBox1.Text;
                    string password = textBox2.Text;
                    bool vhod = true;
                
                    foreach(var action in actions.Action_list)
                    {
                        if (GambitLib.Encryption.Decrypt(action.Email) == email && GambitLib.Encryption.Decrypt(action.Password) == password)
                        {
                            vhod = false;
                            if (action.Admin)
                            {
                                this.Hide();
                                Form main = new Admin(email);
                                main.ShowDialog();
                                try
                                {
                                    Show();
                                }
                                catch (Exception) { }
                                break;
                            }
                            else
                            {
                                this.Hide();
                                Form main = new Main(email,action.Name);
                                main.ShowDialog();
                                try
                                {
                                        Show();
                                }
                                catch(Exception) { }
                                break;
                            }
                        }
                    }
                    if (vhod)
                    {
                        popitka++;
                        MessageBox.Show("Неверный Email или пароль\n Осталось попыток: " + (3-popitka), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (popitka == 3)
                    {
                        IsBanned = true;
                        popitka = 0;
                        MyTimer.Interval = (1000);
                        MyTimer.Tick += new EventHandler(MyTimer_Tick);
                        MyTimer.Start();
                    }
                
                }
                else
                {
                    MessageBox.Show("Неверно введена CAPTCHA", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            else
                MessageBox.Show("Вход заблокирован\nВремени до разблокировки: " +TimeToUnbanned, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            TimeToUnbanned--;
            if (TimeToUnbanned < 1)
            {
                IsBanned = false;
                MyTimer.Stop();
                TimeToUnbanned = 60;
            }
        }
        private void SHow()
        {
            this.Show();
        }
        private Actions DeserializeXML()
        {
            XmlSerializer xml = new XmlSerializer(typeof(Actions));

            using (FileStream fs = new FileStream("Actions.xml", FileMode.OpenOrCreate))
            {
                return (Actions)xml.Deserialize(fs);
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
                Email = ""; Password = ""; Name = ""; SureName = ""; Sex = ""; Admin = false; BirthDate= DateTime.MinValue; Country = "";
            }
            public Action(string Email, string Password, string Name, string SureName, string Sex, DateTime BirthDate, string Country, bool Admin)
            { // конструктор с параметрами
                this.Email = Email; this.Password = Password; this.Name = Name; this.SureName = SureName; this.Sex = Sex; this.BirthDate = BirthDate; this.Country = Country;
                this.Admin = Admin;
            }
        }
        private Bitmap CreateImage1(int Width, int Height)
        {
            Random rnd = new Random(); Bitmap result = new Bitmap(Width, Height); // Создать изображение
            int Xpos = 10; int Ypos = 10; // Вычислить позицию текста
            Brush[] colors = { Brushes.Black, Brushes.Red, Brushes.RoyalBlue, Brushes.Green, Brushes.Yellow, Brushes.White,
Brushes.Tomato, Brushes.Sienna, Brushes.Pink }; //Добавить различные цвета для текста
            Pen[] colorpens = { Pens.Black, Pens.Red, Pens.RoyalBlue, Pens.Green, Pens.Yellow, Pens.White, Pens.Tomato, Pens.Sienna,
Pens.Pink }; // Добавить различные цвета линий // Сделать случайный стиль текста
            FontStyle[] fontstyle = { FontStyle.Bold, FontStyle.Italic, FontStyle.Regular, FontStyle.Strikeout, FontStyle.Underline };
            Int16[] rotate = { 1, -1, 2, -2, 3, -3, 4, -4, 5, -5, 6, -6 }; // Добавить различные углы поворота текста
            Graphics g = Graphics.FromImage((System.Drawing.Image)result); // Указать где рисовать
            g.Clear(Color.LightGray); // Пусть фон картинки будет светлосерым
            g.RotateTransform(rnd.Next(rotate.Length)); // Сделать случайный угол поворота текста
            text = String.Empty; // Генерировать текст
            string ALF = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
            for (int i = 0; i < 5; ++i) text += ALF[rnd.Next(ALF.Length)];
            g.DrawString(text, new Font("Arial", 25, fontstyle[rnd.Next(fontstyle.Length)]),
            colors[rnd.Next(colors.Length)], new PointF(Xpos, Ypos)); // Нарисовать сгенерированный текст
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)], new Point(0, 0), new Point(Width - 1, Height - 1)); // Добавить немного помех
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)], new Point(0, Height - 1), new Point(Width - 1, 0)); // Линии из углов
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j) if (rnd.Next() % 20 == 0) result.SetPixel(i, j, Color.White); // Белые точки
            return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = this.CreateImage1(pictureBox1.Width, pictureBox1.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = this.CreateImage1(pictureBox1.Width, pictureBox1.Height);
        }

        
    }
}
