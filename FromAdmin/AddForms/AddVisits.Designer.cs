namespace WindowsFormsApp4.FromAdmin.AddForms
{
    partial class AddVisits
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridViewEmployees = new System.Windows.Forms.DataGridView();
            this.dataGridViewPet = new System.Windows.Forms.DataGridView();
            this.DatePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cmbBoxService = new System.Windows.Forms.ComboBox();
            this.TimePicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPet)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Привезать работника";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(735, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Привезать питомца";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 428);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Дата визита";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 441);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 23);
            this.button1.TabIndex = 46;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(709, 441);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(134, 23);
            this.button2.TabIndex = 47;
            this.button2.Text = "Потвердить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridViewEmployees
            // 
            this.dataGridViewEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployees.Location = new System.Drawing.Point(12, 40);
            this.dataGridViewEmployees.MultiSelect = false;
            this.dataGridViewEmployees.Name = "dataGridViewEmployees";
            this.dataGridViewEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEmployees.Size = new System.Drawing.Size(407, 330);
            this.dataGridViewEmployees.TabIndex = 50;
            // 
            // dataGridViewPet
            // 
            this.dataGridViewPet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPet.Location = new System.Drawing.Point(436, 41);
            this.dataGridViewPet.MultiSelect = false;
            this.dataGridViewPet.Name = "dataGridViewPet";
            this.dataGridViewPet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPet.Size = new System.Drawing.Size(407, 329);
            this.dataGridViewPet.TabIndex = 51;
            // 
            // DatePicker1
            // 
            this.DatePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DatePicker1.Location = new System.Drawing.Point(271, 441);
            this.DatePicker1.Name = "DatePicker1";
            this.DatePicker1.Size = new System.Drawing.Size(98, 24);
            this.DatePicker1.TabIndex = 52;
            // 
            // cmbBoxService
            // 
            this.cmbBoxService.FormattingEnabled = true;
            this.cmbBoxService.Location = new System.Drawing.Point(436, 441);
            this.cmbBoxService.Name = "cmbBoxService";
            this.cmbBoxService.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxService.TabIndex = 53;
            // 
            // TimePicker
            // 
            this.TimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.TimePicker.Location = new System.Drawing.Point(171, 441);
            this.TimePicker.Name = "TimePicker";
            this.TimePicker.ShowUpDown = true;
            this.TimePicker.Size = new System.Drawing.Size(94, 24);
            this.TimePicker.TabIndex = 54;
            // 
            // AddVisits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 476);
            this.Controls.Add(this.TimePicker);
            this.Controls.Add(this.cmbBoxService);
            this.Controls.Add(this.DatePicker1);
            this.Controls.Add(this.dataGridViewPet);
            this.Controls.Add(this.dataGridViewEmployees);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddVisits";
            this.Text = "AddVisits";
            this.Load += new System.EventHandler(this.AddVisits_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridViewEmployees;
        private System.Windows.Forms.DataGridView dataGridViewPet;
        private System.Windows.Forms.ComboBox cmbBoxService;
        private System.Windows.Forms.DateTimePicker DatePicker1;
        private System.Windows.Forms.DateTimePicker TimePicker;
    }
}