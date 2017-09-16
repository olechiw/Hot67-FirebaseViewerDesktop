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
            this.testButton = new System.Windows.Forms.Button();
            this.averageDataGridView = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.sumDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.averageDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sumDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(12, 37);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(260, 20);
            this.usernameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(84, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(12, 88);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(260, 20);
            this.passwordTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(88, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(98, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Api Key";
            // 
            // apiKeyTextBox
            // 
            this.apiKeyTextBox.Location = new System.Drawing.Point(12, 139);
            this.apiKeyTextBox.Name = "apiKeyTextBox";
            this.apiKeyTextBox.Size = new System.Drawing.Size(260, 20);
            this.apiKeyTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(74, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Event Name";
            // 
            // eventNameTextBox
            // 
            this.eventNameTextBox.Location = new System.Drawing.Point(12, 190);
            this.eventNameTextBox.Name = "eventNameTextBox";
            this.eventNameTextBox.Size = new System.Drawing.Size(260, 20);
            this.eventNameTextBox.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(74, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 25);
            this.label5.TabIndex = 8;
            this.label5.Text = "DatabaseUrl Url";
            // 
            // databaseUrlTextBox
            // 
            this.databaseUrlTextBox.Location = new System.Drawing.Point(12, 241);
            this.databaseUrlTextBox.Name = "databaseUrlTextBox";
            this.databaseUrlTextBox.Size = new System.Drawing.Size(260, 20);
            this.databaseUrlTextBox.TabIndex = 9;
            // 
            // testButton
            // 
            this.testButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testButton.Location = new System.Drawing.Point(103, 296);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(92, 44);
            this.testButton.TabIndex = 10;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            // 
            // averageDataGridView
            // 
            this.averageDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.averageDataGridView.Location = new System.Drawing.Point(377, 37);
            this.averageDataGridView.MultiSelect = false;
            this.averageDataGridView.Name = "averageDataGridView";
            this.averageDataGridView.Size = new System.Drawing.Size(240, 150);
            this.averageDataGridView.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(391, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(206, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Columns to Average";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(410, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 25);
            this.label7.TabIndex = 13;
            this.label7.Text = "Columns to Sum";
            // 
            // sumDataGridView
            // 
            this.sumDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sumDataGridView.Location = new System.Drawing.Point(377, 218);
            this.sumDataGridView.MultiSelect = false;
            this.sumDataGridView.Name = "sumDataGridView";
            this.sumDataGridView.Size = new System.Drawing.Size(240, 150);
            this.sumDataGridView.TabIndex = 14;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 381);
            this.Controls.Add(this.sumDataGridView);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.averageDataGridView);
            this.Controls.Add(this.testButton);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.averageDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sumDataGridView)).EndInit();
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
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.DataGridView averageDataGridView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView sumDataGridView;
    }
}