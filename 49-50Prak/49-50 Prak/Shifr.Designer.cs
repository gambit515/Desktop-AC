namespace _49_50_Prak
{
    partial class Shifr
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.btnCreatKeys = new System.Windows.Forms.Button();
            this.btnLoadKeys = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(417, 242);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(435, 90);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 4;
            // 
            // btnCreatKeys
            // 
            this.btnCreatKeys.Location = new System.Drawing.Point(459, 227);
            this.btnCreatKeys.Name = "btnCreatKeys";
            this.btnCreatKeys.Size = new System.Drawing.Size(75, 23);
            this.btnCreatKeys.TabIndex = 5;
            this.btnCreatKeys.Text = "btnCreatKeys";
            this.btnCreatKeys.UseVisualStyleBackColor = true;
            this.btnCreatKeys.Click += new System.EventHandler(this.btnCreatKeys_Click);
            // 
            // btnLoadKeys
            // 
            this.btnLoadKeys.Location = new System.Drawing.Point(540, 227);
            this.btnLoadKeys.Name = "btnLoadKeys";
            this.btnLoadKeys.Size = new System.Drawing.Size(75, 23);
            this.btnLoadKeys.TabIndex = 6;
            this.btnLoadKeys.Text = "btnLoadKeys";
            this.btnLoadKeys.UseVisualStyleBackColor = true;
            this.btnLoadKeys.Click += new System.EventHandler(this.btnLoadKeys_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(459, 256);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 7;
            this.btnEncrypt.Text = "btnEncrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(540, 256);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btnDecrypt.TabIndex = 8;
            this.btnDecrypt.Text = "btnDecrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // Shifr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.btnLoadKeys);
            this.Controls.Add(this.btnCreatKeys);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Shifr";
            this.Text = "Shifr";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button btnCreatKeys;
        private System.Windows.Forms.Button btnLoadKeys;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
    }
}