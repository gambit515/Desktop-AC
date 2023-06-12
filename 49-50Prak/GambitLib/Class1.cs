using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GambitLib
{

    static public class Encryption
    {
        static public string publicxml = "";
        static public string privatexml = "";
        static public string str3 = "";
        static public bool _ispub_xml = true;
        static public byte[] EncryptedData;
        static public byte[] DecryptedData;
        static public bool _ispriv_xml = true;
        static public bool _isdata = true;
        static public bool _isupdate = true;
        static public string txtBase64 = "";
        static public string txtEncryptedText = "";
        static public bool CreateKey()
        { // создание ключей и сохранение их в файле
            try
            {
                RSACryptoServiceProvider RsaKey = new RSACryptoServiceProvider();
                string publickey = RsaKey.ToXmlString(false); string privatekey = RsaKey.ToXmlString(true);
                File.WriteAllText("private.xml", privatekey, Encoding.UTF8);
                File.WriteAllText("public.xml", publickey, Encoding.UTF8);
                return (true);
            }
            catch (Exception ex) {/* MessageBox.Show(ex.Message.ToString()); */return (false); }
        }

        static public bool LoadKey()
        { // загрузка ключей
            try { privatexml = File.ReadAllText("private.xml", Encoding.UTF8); }
            catch (Exception ex) {/* MessageBox.Show("Проблема с закрытым ключом... \n" + ex.Message.ToString()); */return false; }
            try { publicxml = File.ReadAllText("public.xml", Encoding.UTF8); }
            catch (Exception ex)
            {
                /*MessageBox.Show("Проблема с открытым ключом... \n" + ex.Message.ToString());*/ return false;
            }
            return true;
        }
        static public string Incrypt(string DataOriginal, out bool Succes)
        { // шифрование данных
            Succes = true;
            str3 = DataOriginal; // это строка которую следует зашифровать
            byte[] data = new byte[1024];
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            try
            {
                if (publicxml.Length == 0)
                {
                    _ispub_xml = false;/* MessageBox.Show("Неверный открытый ключ...");*/ Succes = false; return "";
                }
                else { rsa.FromXmlString(publicxml); }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Проблема с RSA... \n" + ex.Message.ToString());
            }
            try { data = Encoding.UTF8.GetBytes(str3); }
            catch (Exception ss)
            {
                //MessageBox.Show(ss.ToString()); успех = false; return "";
            }
            try { EncryptedData = rsa.Encrypt(data, false); } // шифрование данных
            catch (CryptographicException ex)
            {
                //MessageBox.Show("Крипто-ошибка... \n" + ex.Message.ToString());
            }
            txtEncryptedText = Encoding.UTF8.GetString(EncryptedData);
            txtBase64 = Convert.ToBase64String(EncryptedData); // данные в таком виде можно хранить в файле (базе данных)
            for (int i = 0; i < data.Length - 1; i++) { data.SetValue((byte)0, i); }
            for (int i = 0; i < EncryptedData.Length - 1; i++) { EncryptedData.SetValue((byte)0, i); }
            //return (txtEncryptedText);
            return (txtBase64);
        }
        static public string Incrypt(string DataOriginal)
        { // шифрование данных
            str3 = DataOriginal; // это строка которую следует зашифровать
            byte[] data = new byte[1024];
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            try
            {
                if (publicxml.Length == 0)
                {
                    _ispub_xml = false;/* MessageBox.Show("Неверный открытый ключ...");*/ return "";
                }
                else { rsa.FromXmlString(publicxml); }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Проблема с RSA... \n" + ex.Message.ToString());
            }
            try { data = Encoding.UTF8.GetBytes(str3); }
            catch (Exception ss)
            {
                //MessageBox.Show(ss.ToString()); успех = false; return "";
            }
            try { EncryptedData = rsa.Encrypt(data, false); } // шифрование данных
            catch (CryptographicException ex)
            {
                //MessageBox.Show("Крипто-ошибка... \n" + ex.Message.ToString());
            }
            txtEncryptedText = Encoding.UTF8.GetString(EncryptedData);
            txtBase64 = Convert.ToBase64String(EncryptedData); // данные в таком виде можно хранить в файле (базе данных)
            for (int i = 0; i < data.Length - 1; i++) { data.SetValue((byte)0, i); }
            for (int i = 0; i < EncryptedData.Length - 1; i++) { EncryptedData.SetValue((byte)0, i); }
            //return (txtEncryptedText);
            return (txtBase64);
        }
        static public string Decrypt(string DecyptedData_txtBase64, out bool Succes)
        { // расшифровка данных
            Succes = true;
            byte[] data = new byte[1024];
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            try
            {
                if (privatexml.Length == 0)
                {
                    Succes = false;
                    _ispriv_xml = false; /*MessageBox.Show("Bad private key...");*/ return "";
                }
                else { rsa.FromXmlString(privatexml); }
            }
            catch (Exception ex)
            {
                Succes = false;
                //MessageBox.Show("Проблема с RSA... \n" + ex.Message.ToString());
            }
            try { data = Convert.FromBase64String(DecyptedData_txtBase64); } // зашифрованные данные
            catch (FileNotFoundException)
            {
                Succes = false;
                /*MessageBox.Show("Файл данных не существует...");*/ return "";
            }
            try { DecryptedData = rsa.Decrypt(data, false); } // расшифровка данных
            catch (CryptographicException ex)
            {
                Succes = false;
                /*MessageBox.Show("Крипто-ошибка... \n" + ex.Message.ToString());*/ return "";
            }
            Succes = true;
            string text = Encoding.UTF8.GetString(DecryptedData);
            for (int i = 0; i < data.Length - 1; i++) { data.SetValue((byte)0, i); }
            for (int i = 0; i < DecryptedData.Length - 1; i++) { DecryptedData.SetValue((byte)0, i); }
            return text;
        }
        static public string Decrypt(string DecyptedData_txtBase64)
        { // расшифровка данных
            byte[] data = new byte[1024];
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            try
            {
                if (privatexml.Length == 0)
                {
                    _ispriv_xml = false; /*MessageBox.Show("Bad private key...");*/ return "";
                }
                else { rsa.FromXmlString(privatexml); }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Проблема с RSA... \n" + ex.Message.ToString());
            }
            try { data = Convert.FromBase64String(DecyptedData_txtBase64); } // зашифрованные данные
            catch (FileNotFoundException)
            {
                /*MessageBox.Show("Файл данных не существует...");*/
                return "";
            }
            try { DecryptedData = rsa.Decrypt(data, false); } // расшифровка данных
            catch (CryptographicException ex)
            {
                /*MessageBox.Show("Крипто-ошибка... \n" + ex.Message.ToString());*/
                return "";
            }
            string text = Encoding.UTF8.GetString(DecryptedData);
            for (int i = 0; i < data.Length - 1; i++) { data.SetValue((byte)0, i); }
            for (int i = 0; i < DecryptedData.Length - 1; i++) { DecryptedData.SetValue((byte)0, i); }
            return text;
        }
    }
}
