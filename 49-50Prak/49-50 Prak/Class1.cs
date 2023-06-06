using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _49_50_Prak
{
    public class Tovars
    {
        public List<Tovar> Tovar_list { get; set; } = new List<Tovar>();
        static public Tovars DeserializeXML()
        {
            XmlSerializer xml = new XmlSerializer(typeof(Tovars));

            using (FileStream fs = new FileStream("Tovars.xml", FileMode.OpenOrCreate))
            {
                return (Tovars)xml.Deserialize(fs);
            }
        }
        static public void SerializeXML(Tovars tovars)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Tovars));

            using (FileStream fs = new FileStream("Tovars.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, tovars);
            }
        }
    }
    public class Tovar
    {
        private int Articul;
        private string Name;
        private string PNG;
        private int CostRoznicha;
        private int Kolvo;
        public int Articul_
        {
            get { return Articul; }
            set { Articul = value; }
        }
        public string Name_
        {
            get { return Name; }
            set { Name = value; }
        }
        public string PNG_
        {
            get { return PNG; }
            set { PNG = value; }
        }
        public int CostRoznicha_
        {
            get { return CostRoznicha; }
            set { CostRoznicha = value; }
        }
        public int Kolvo_
        {
            get { return Kolvo; }
            set { Kolvo = value; }
        }
        public Tovar()
        {
            Articul = 0; Name = "";PNG = ""; CostRoznicha = 0; Kolvo = 0;
        }
        public Tovar(int Articul, string Name, string PNG, int CostRoznicha, int Kolvo)
        {
            this.Articul = Articul; this.Name = Name; this.PNG = PNG; this.CostRoznicha = CostRoznicha; this.Kolvo = Kolvo;
        }
    }
    public class Operations
    {
        public List<Operation> Operation_list { get; set; } = new List<Operation>();
        static public int LastId(Operations operations)
        {
            int a = -1;
            foreach(Operation operation in operations.Operation_list)
            {
                if (operation.Id_ > a)
                {
                    a = operation.Id_;
                }
            }
            a++;
            return a;
        }
        static public Operations DeserializeXML()
        {
            XmlSerializer xml = new XmlSerializer(typeof(Operations));

            using (FileStream fs = new FileStream("Operations.xml", FileMode.OpenOrCreate))
            {
                return (Operations)xml.Deserialize(fs);
            }
        }
        static public void SerializeXML(Operations operations)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Operations));

            using (FileStream fs = new FileStream("Operations.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, operations);
            }
        }
    }
    public class Operation
    {
        private int Id;
        private int Articul;
        private DateTime Prihod_Rashod;
        private int Cost;
        private int Kolvo;
        private int OperationCode;
        private string Other;
        public int Id_
        {
            get { return Id; }
            set { Id = value; }
        }
        public int Articul_
        {
            get { return Articul; }
            set { Articul = value; }
        }
        public DateTime Prihod_Rashod_
        {
            get { return Prihod_Rashod; }
            set { Prihod_Rashod = value; }
        }
        public int Cost_
        {
            get { return Cost; }
            set { Cost = value; }
        }
        public int Kolvo_
        {
            get { return Kolvo; }
            set { Kolvo = value; }
        }
        public int OperationCode_
        {
            get { return OperationCode; }
            set { OperationCode = value; }
        }
        public string Other_
        {
            get { return Other; }
            set { Other = value; }
        }
        public Operation()
        {
            this.Id = 0; this.Articul = 0; this.Prihod_Rashod = DateTime.MinValue; this.Cost = 0; this.Kolvo = 0; this.OperationCode = 0; this.Other = "";
        }
        public Operation(int Id,int Articul, DateTime Prihod_Rashod, int Cost, int Kolvo, int OperationCode, string Other)
        {
            this.Id = Id; this.Articul = Articul; this.Prihod_Rashod = Prihod_Rashod; this.Cost = Cost; this.Kolvo = Kolvo; this.OperationCode = OperationCode; this.Other = Other;
        }
    }

    public class Providers
    {
        public List<Provider> Provider_list { get; set; } = new List<Provider>();
        static public Providers DeserializeXML()
        {
            XmlSerializer xml = new XmlSerializer(typeof(Providers));

            using (FileStream fs = new FileStream("Providers.xml", FileMode.OpenOrCreate))
            {
                return (Providers)xml.Deserialize(fs);
            }
        }
        static public void SerializeXML(Providers providers)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Providers));

            using (FileStream fs = new FileStream("Providers.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, providers);
            }
        }
    }
    public class Provider
    {
        private int Number;
        private string Name;
        private string Country;
        private string City;
        private string Street;
        private string Phone;
        private string FIO;
        public int Number_
        {
            get { return Number; }
            set { Number = value; }
        }
        public string Name_
        {
            get { return Name; }
            set { Name = value; }
        }
        public string Country_
        {
            get { return Country; }
            set { Country = value; }
        }
        public string City_
        {
            get { return City; }
            set { City = value; }
        }
        public string Street_
        {
            get { return Street; }
            set { Street = value; }
        }
        public string Phone_
        {
            get { return Phone; }
            set { Phone = value; }
        }
        public string FIO_
        {
            get { return FIO; }
            set { FIO = value; }
        }

        public Provider()
        {
            this.Number = 0; this.Name = ""; this.Country = "";this.City = ""; this.Street = ""; this.Phone = ""; FIO = ""; 
        }
        public Provider(int Number, string Name, string Country,string City, string Street, string Phone, string FIO )
        {
            this.Number = Number; this.Name = Name; this.Country = Country; this.City = City; this.Street = Street; this.Phone = Phone; this.FIO = FIO;
        }
    }
    public class Korzina
    {
        public Tovar tovar;
        public int kolvo;
        public Korzina()
        {
            this.kolvo = 0; this.tovar = new Tovar();
        }
        public Korzina(int kolvo,Tovar tovar)
        {
            this.kolvo = kolvo; this.tovar = tovar;
        }
    }
}
