namespace WindowsFormsApp4
{
    partial class Form2
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btn_ChangeStr = new System.Windows.Forms.Button();
            this.btn_AddStr = new System.Windows.Forms.Button();
            this.btn_DeleteStr = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 45);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(776, 293);
            this.dataGridView1.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Ваши Визиты",
            "Визиты",
            "Сервис",
            "Владельцы животных"});
            this.comboBox1.Location = new System.Drawing.Point(12, 18);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btn_ChangeStr
            // 
            this.btn_ChangeStr.Location = new System.Drawing.Point(139, 18);
            this.btn_ChangeStr.Name = "btn_ChangeStr";
            this.btn_ChangeStr.Size = new System.Drawing.Size(108, 23);
            this.btn_ChangeStr.TabIndex = 2;
            this.btn_ChangeStr.Text = "Изменить запись";
            this.btn_ChangeStr.UseVisualStyleBackColor = true;
            this.btn_ChangeStr.Click += new System.EventHandler(this.btn_ChangeStr_Click);
            // 
            // btn_AddStr
            // 
            this.btn_AddStr.Location = new System.Drawing.Point(253, 18);
            this.btn_AddStr.Name = "btn_AddStr";
            this.btn_AddStr.Size = new System.Drawing.Size(128, 23);
            this.btn_AddStr.TabIndex = 3;
            this.btn_AddStr.Text = "Добавить записсь";
            this.btn_AddStr.UseVisualStyleBackColor = true;
            this.btn_AddStr.Click += new System.EventHandler(this.btn_AddStr_Click);
            // 
            // btn_DeleteStr
            // 
            this.btn_DeleteStr.Location = new System.Drawing.Point(387, 18);
            this.btn_DeleteStr.Name = "btn_DeleteStr";
            this.btn_DeleteStr.Size = new System.Drawing.Size(113, 23);
            this.btn_DeleteStr.TabIndex = 4;
            this.btn_DeleteStr.Text = "Удалить запись";
            this.btn_DeleteStr.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_DeleteStr);
            this.Controls.Add(this.btn_AddStr);
            this.Controls.Add(this.btn_ChangeStr);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btn_ChangeStr;
        private System.Windows.Forms.Button btn_AddStr;
        private System.Windows.Forms.Button btn_DeleteStr;
    }
}