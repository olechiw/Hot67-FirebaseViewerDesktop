using System.IO;

namespace BluetoothScouterPits
{
    partial class Settings
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
            if (disposing)
            {
                if (components != null)
                    components.Dispose();

                WriteSettings(new StreamWriter(ConfigurationFile));
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
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.apiKeyTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.eventNameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.databaseUrlTextBox = new System.Windows.Forms.TextBox();
            this.fetchButton = new System.Windows.Forms.Button();
            this.averageDataGridView = new System.Windows.Forms.DataGridView();
            this.sumDataGridView = new System.Windows.Forms.DataGridView();
            this.minimumDataGridView = new System.Windows.Forms.DataGridView();
            this.maximumDataGridView = new System.Windows.Forms.DataGridView();
            this.retreivedColumnsDataGridView = new System.Windows.Forms.DataGridView();
            this.knownHeaderColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.averageDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sumDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimumDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maximumDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retreivedColumnsDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(36, 37);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(260, 20);
            this.usernameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Email";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(36, 88);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(260, 20);
            this.passwordTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Api Key";
            // 
            // apiKeyTextBox
            // 
            this.apiKeyTextBox.Location = new System.Drawing.Point(36, 139);
            this.apiKeyTextBox.Name = "apiKeyTextBox";
            this.apiKeyTextBox.Size = new System.Drawing.Size(260, 20);
            this.apiKeyTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Event Name";
            // 
            // eventNameTextBox
            // 
            this.eventNameTextBox.Location = new System.Drawing.Point(36, 190);
            this.eventNameTextBox.Name = "eventNameTextBox";
            this.eventNameTextBox.Size = new System.Drawing.Size(260, 20);
            this.eventNameTextBox.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 25);
            this.label5.TabIndex = 8;
            this.label5.Text = "Database Url";
            // 
            // databaseUrlTextBox
            // 
            this.databaseUrlTextBox.Location = new System.Drawing.Point(36, 241);
            this.databaseUrlTextBox.Name = "databaseUrlTextBox";
            this.databaseUrlTextBox.Size = new System.Drawing.Size(260, 20);
            this.databaseUrlTextBox.TabIndex = 9;
            // 
            // fetchButton
            // 
            this.fetchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fetchButton.Location = new System.Drawing.Point(327, 118);
            this.fetchButton.Name = "fetchButton";
            this.fetchButton.Size = new System.Drawing.Size(108, 69);
            this.fetchButton.TabIndex = 10;
            this.fetchButton.Text = "Fetch\r\nColumns";
            this.fetchButton.UseVisualStyleBackColor = true;
            this.fetchButton.Click += new System.EventHandler(this.OnFetchButtonClick);
            // 
            // averageDataGridView
            // 
            this.averageDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.averageDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.averageDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.averageDataGridView.EnableHeadersVisualStyles = false;
            this.averageDataGridView.Location = new System.Drawing.Point(3, 22);
            this.averageDataGridView.MultiSelect = false;
            this.averageDataGridView.Name = "averageDataGridView";
            this.averageDataGridView.Size = new System.Drawing.Size(301, 203);
            this.averageDataGridView.TabIndex = 11;
            // 
            // sumDataGridView
            // 
            this.sumDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.sumDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sumDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sumDataGridView.EnableHeadersVisualStyles = false;
            this.sumDataGridView.Location = new System.Drawing.Point(3, 22);
            this.sumDataGridView.MultiSelect = false;
            this.sumDataGridView.Name = "sumDataGridView";
            this.sumDataGridView.Size = new System.Drawing.Size(301, 204);
            this.sumDataGridView.TabIndex = 14;
            // 
            // minimumDataGridView
            // 
            this.minimumDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.minimumDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.minimumDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minimumDataGridView.EnableHeadersVisualStyles = false;
            this.minimumDataGridView.Location = new System.Drawing.Point(3, 22);
            this.minimumDataGridView.MultiSelect = false;
            this.minimumDataGridView.Name = "minimumDataGridView";
            this.minimumDataGridView.Size = new System.Drawing.Size(301, 203);
            this.minimumDataGridView.TabIndex = 16;
            // 
            // maximumDataGridView
            // 
            this.maximumDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.maximumDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.maximumDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maximumDataGridView.EnableHeadersVisualStyles = false;
            this.maximumDataGridView.Location = new System.Drawing.Point(3, 22);
            this.maximumDataGridView.MultiSelect = false;
            this.maximumDataGridView.Name = "maximumDataGridView";
            this.maximumDataGridView.Size = new System.Drawing.Size(301, 204);
            this.maximumDataGridView.TabIndex = 17;
            // 
            // retreivedColumnsDataGridView
            // 
            this.retreivedColumnsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.retreivedColumnsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.retreivedColumnsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.knownHeaderColumn});
            this.retreivedColumnsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.retreivedColumnsDataGridView.EnableHeadersVisualStyles = false;
            this.retreivedColumnsDataGridView.Location = new System.Drawing.Point(3, 22);
            this.retreivedColumnsDataGridView.MultiSelect = false;
            this.retreivedColumnsDataGridView.Name = "retreivedColumnsDataGridView";
            this.retreivedColumnsDataGridView.ReadOnly = true;
            this.retreivedColumnsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.retreivedColumnsDataGridView.Size = new System.Drawing.Size(393, 184);
            this.retreivedColumnsDataGridView.TabIndex = 19;
            // 
            // knownHeaderColumn
            // 
            this.knownHeaderColumn.HeaderText = "Column";
            this.knownHeaderColumn.Name = "knownHeaderColumn";
            this.knownHeaderColumn.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.averageDataGridView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 228);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Columns to Average";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.minimumDataGridView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(316, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 228);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Columns to Show Minimum";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.maximumDataGridView);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(316, 237);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(307, 229);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Columns to Show Maximum";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.sumDataGridView);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(3, 237);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(307, 229);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Columns to Sum";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(444, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(626, 469);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.retreivedColumnsDataGridView);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(36, 275);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(399, 209);
            this.groupBox5.TabIndex = 26;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Retreived Columns";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 496);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.fetchButton);
            this.Controls.Add(this.databaseUrlTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.eventNameTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.apiKeyTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.usernameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.averageDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sumDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimumDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maximumDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retreivedColumnsDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox apiKeyTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox eventNameTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox databaseUrlTextBox;
        private System.Windows.Forms.Button fetchButton;
        private System.Windows.Forms.DataGridView averageDataGridView;
        private System.Windows.Forms.DataGridView sumDataGridView;
        private System.Windows.Forms.DataGridView minimumDataGridView;
        private System.Windows.Forms.DataGridView maximumDataGridView;
        private System.Windows.Forms.DataGridView retreivedColumnsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn knownHeaderColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}