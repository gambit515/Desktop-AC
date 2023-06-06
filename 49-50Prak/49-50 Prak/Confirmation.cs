using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _49_50_Prak
{
    public partial class Confirmation : Form
    {
        public bool otv { get; set; }
        public Confirmation()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.otv = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.otv = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
