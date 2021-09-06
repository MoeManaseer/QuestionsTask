namespace QuestionsFormsTest
{
    partial class LandingForm
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
            this.allQuestionsGrid = new System.Windows.Forms.DataGridView();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.removeBtn = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.outputLbl = new System.Windows.Forms.Label();
            this.settingsBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.settingsPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.allQuestionsGrid)).BeginInit();
            this.settingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // allQuestionsGrid
            // 
            this.allQuestionsGrid.AllowUserToAddRows = false;
            this.allQuestionsGrid.AllowUserToDeleteRows = false;
            this.allQuestionsGrid.AllowUserToResizeColumns = false;
            this.allQuestionsGrid.AllowUserToResizeRows = false;
            this.allQuestionsGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.allQuestionsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.allQuestionsGrid.GridColor = System.Drawing.SystemColors.Control;
            this.allQuestionsGrid.Location = new System.Drawing.Point(12, 12);
            this.allQuestionsGrid.MultiSelect = false;
            this.allQuestionsGrid.Name = "allQuestionsGrid";
            this.allQuestionsGrid.ReadOnly = true;
            this.allQuestionsGrid.RowHeadersVisible = false;
            this.allQuestionsGrid.Size = new System.Drawing.Size(461, 418);
            this.allQuestionsGrid.TabIndex = 0;
            this.allQuestionsGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.allQuestionsGrid_RowEnter);
            // 
            // refreshBtn
            // 
            this.refreshBtn.Location = new System.Drawing.Point(110, 0);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(75, 23);
            this.refreshBtn.TabIndex = 1;
            this.refreshBtn.Text = "Exit";
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // removeBtn
            // 
            this.removeBtn.Enabled = false;
            this.removeBtn.Location = new System.Drawing.Point(398, 436);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(75, 23);
            this.removeBtn.TabIndex = 2;
            this.removeBtn.Text = "Remove";
            this.removeBtn.UseVisualStyleBackColor = true;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // editBtn
            // 
            this.editBtn.Enabled = false;
            this.editBtn.Location = new System.Drawing.Point(317, 436);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(75, 23);
            this.editBtn.TabIndex = 3;
            this.editBtn.Text = "Edit";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(236, 436);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 4;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // outputLbl
            // 
            this.outputLbl.AutoSize = true;
            this.outputLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.outputLbl.Location = new System.Drawing.Point(12, 421);
            this.outputLbl.Name = "outputLbl";
            this.outputLbl.Size = new System.Drawing.Size(0, 20);
            this.outputLbl.TabIndex = 5;
            // 
            // settingsBtn
            // 
            this.settingsBtn.Location = new System.Drawing.Point(29, 0);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(75, 23);
            this.settingsBtn.TabIndex = 7;
            this.settingsBtn.Text = "Settings";
            this.settingsBtn.UseVisualStyleBackColor = true;
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(12, 462);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(461, 2);
            this.label2.TabIndex = 9;
            // 
            // settingsPanel
            // 
            this.settingsPanel.Controls.Add(this.settingsBtn);
            this.settingsPanel.Controls.Add(this.refreshBtn);
            this.settingsPanel.Location = new System.Drawing.Point(288, 467);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(185, 22);
            this.settingsPanel.TabIndex = 10;
            // 
            // LandingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 497);
            this.Controls.Add(this.settingsPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.outputLbl);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.allQuestionsGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(501, 536);
            this.MinimumSize = new System.Drawing.Size(501, 536);
            this.Name = "LandingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Questions";
            this.Load += new System.EventHandler(this.LandingFrom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.allQuestionsGrid)).EndInit();
            this.settingsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView allQuestionsGrid;
        private System.Windows.Forms.Button refreshBtn;
        private System.Windows.Forms.Button removeBtn;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Label outputLbl;
        private System.Windows.Forms.Button settingsBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel settingsPanel;
    }
}

