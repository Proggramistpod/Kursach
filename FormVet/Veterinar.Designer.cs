namespace WindowsFormsApp4
{
    partial class Veterinar
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
            this.cmbBox_Table = new System.Windows.Forms.ComboBox();
            this.btn_Sure = new System.Windows.Forms.Button();
            this.btn_Look = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Update = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 54);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(776, 310);
            this.dataGridView1.TabIndex = 0;
            // 
            // cmbBox_Table
            // 
            this.cmbBox_Table.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox_Table.FormattingEnabled = true;
            this.cmbBox_Table.Items.AddRange(new object[] {
            "Ваши Визиты",
            "Визиты",
            "Сервис"});
            this.cmbBox_Table.Location = new System.Drawing.Point(12, 27);
            this.cmbBox_Table.Name = "cmbBox_Table";
            this.cmbBox_Table.Size = new System.Drawing.Size(121, 21);
            this.cmbBox_Table.TabIndex = 1;
            this.cmbBox_Table.SelectedIndexChanged += new System.EventHandler(this.cmbBox1_SelectedIndexChangedTable);
            // 
            // btn_Sure
            // 
            this.btn_Sure.Location = new System.Drawing.Point(149, 25);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(108, 23);
            this.btn_Sure.TabIndex = 2;
            this.btn_Sure.Text = "Подтвердить визит";
            this.btn_Sure.UseVisualStyleBackColor = true;
            this.btn_Sure.Click += new System.EventHandler(this.Sure_Click);
            // 
            // btn_Look
            // 
            this.btn_Look.Location = new System.Drawing.Point(263, 25);
            this.btn_Look.Name = "btn_Look";
            this.btn_Look.Size = new System.Drawing.Size(108, 23);
            this.btn_Look.TabIndex = 3;
            this.btn_Look.Text = "Просмотр";
            this.btn_Look.UseVisualStyleBackColor = true;
            this.btn_Look.Click += new System.EventHandler(this.Look_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(680, 25);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(108, 23);
            this.btn_Update.TabIndex = 5;
            this.btn_Update.Text = "Обновить";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.cmbBox1_SelectedIndexChangedTable);
            // 
            // Veterinar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(800, 376);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.btn_Look);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.cmbBox_Table);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Veterinar";
            this.Text = "Учет";
            this.Load += new System.EventHandler(this.Veterinar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmbBox_Table;
        private System.Windows.Forms.Button btn_Sure;
        private System.Windows.Forms.Button btn_Look;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.Button btn_Update;
    }
}