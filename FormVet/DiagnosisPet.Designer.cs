namespace WindowsFormsApp4
{
    partial class DiagnosisPet
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmbBox_Diagnosis = new System.Windows.Forms.ComboBox();
            this.btn_AddDiagnosis = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(775, 425);
            this.dataGridView1.TabIndex = 0;
            // 
            // cmbBox_Diagnosis
            // 
            this.cmbBox_Diagnosis.FormattingEnabled = true;
            this.cmbBox_Diagnosis.Location = new System.Drawing.Point(13, 445);
            this.cmbBox_Diagnosis.Name = "cmbBox_Diagnosis";
            this.cmbBox_Diagnosis.Size = new System.Drawing.Size(121, 21);
            this.cmbBox_Diagnosis.TabIndex = 1;
            this.cmbBox_Diagnosis.SelectedIndexChanged += new System.EventHandler(this.cmbBox_SelectedIndexChangedDiagnosis);
            // 
            // btn_AddDiagnosis
            // 
            this.btn_AddDiagnosis.Location = new System.Drawing.Point(659, 453);
            this.btn_AddDiagnosis.Name = "btn_AddDiagnosis";
            this.btn_AddDiagnosis.Size = new System.Drawing.Size(129, 23);
            this.btn_AddDiagnosis.TabIndex = 2;
            this.btn_AddDiagnosis.Text = "Добавить Диагноз";
            this.btn_AddDiagnosis.UseVisualStyleBackColor = true;
            this.btn_AddDiagnosis.Click += new System.EventHandler(this.AddDiagnosis_Click);
            // 
            // DiagnosisPet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 488);
            this.Controls.Add(this.btn_AddDiagnosis);
            this.Controls.Add(this.cmbBox_Diagnosis);
            this.Controls.Add(this.dataGridView1);
            this.Name = "DiagnosisPet";
            this.Text = "DiagnosisPet";
            this.Load += new System.EventHandler(this.DiagnosisPet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmbBox_Diagnosis;
        private System.Windows.Forms.Button btn_AddDiagnosis;
    }
}