namespace WindowsFormsApp4.FromAdmin.AddForms
{
    partial class AddService
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
            this.txtBoxName = new System.Windows.Forms.TextBox();
            this.txtBoxPrice = new System.Windows.Forms.TextBox();
            this.txtBoxDescription = new System.Windows.Forms.TextBox();
            this.btnCansel = new System.Windows.Forms.Button();
            this.btnSure = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBoxName
            // 
            this.txtBoxName.Location = new System.Drawing.Point(12, 28);
            this.txtBoxName.Name = "txtBoxName";
            this.txtBoxName.Size = new System.Drawing.Size(262, 20);
            this.txtBoxName.TabIndex = 0;
            // 
            // txtBoxPrice
            // 
            this.txtBoxPrice.Location = new System.Drawing.Point(12, 54);
            this.txtBoxPrice.Name = "txtBoxPrice";
            this.txtBoxPrice.Size = new System.Drawing.Size(139, 20);
            this.txtBoxPrice.TabIndex = 1;
            // 
            // txtBoxDescription
            // 
            this.txtBoxDescription.Location = new System.Drawing.Point(12, 93);
            this.txtBoxDescription.Multiline = true;
            this.txtBoxDescription.Name = "txtBoxDescription";
            this.txtBoxDescription.Size = new System.Drawing.Size(262, 214);
            this.txtBoxDescription.TabIndex = 2;
            // 
            // btnCansel
            // 
            this.btnCansel.Location = new System.Drawing.Point(12, 314);
            this.btnCansel.Name = "btnCansel";
            this.btnCansel.Size = new System.Drawing.Size(75, 23);
            this.btnCansel.TabIndex = 3;
            this.btnCansel.Text = "Отмена";
            this.btnCansel.UseVisualStyleBackColor = true;
            this.btnCansel.Click += new System.EventHandler(this.btnCansel_Click);
            // 
            // btnSure
            // 
            this.btnSure.Location = new System.Drawing.Point(199, 313);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(75, 23);
            this.btnSure.TabIndex = 4;
            this.btnSure.Text = "Потвердить";
            this.btnSure.UseVisualStyleBackColor = true;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Название";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Цена";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Описание";
            // 
            // AddService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 344);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.btnCansel);
            this.Controls.Add(this.txtBoxDescription);
            this.Controls.Add(this.txtBoxPrice);
            this.Controls.Add(this.txtBoxName);
            this.Name = "AddService";
            this.Text = "AddService";
            this.Load += new System.EventHandler(this.AddService_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxName;
        private System.Windows.Forms.TextBox txtBoxPrice;
        private System.Windows.Forms.TextBox txtBoxDescription;
        private System.Windows.Forms.Button btnCansel;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}