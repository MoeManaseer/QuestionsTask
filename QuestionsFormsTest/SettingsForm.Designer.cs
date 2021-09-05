namespace QuestionsFormsTest
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.testBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.connectionContainer = new System.Windows.Forms.Panel();
            this.input_Password = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.input_Username = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.input_IntegratedSecurity = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.input_DatabaseName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.input_DataSource = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.connectionTestOutput = new System.Windows.Forms.Label();
            this.connectionContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // testBtn
            // 
            resources.ApplyResources(this.testBtn, "testBtn");
            this.testBtn.Name = "testBtn";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.testBtn_Click);
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // saveBtn
            // 
            resources.ApplyResources(this.saveBtn, "saveBtn");
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // exitBtn
            // 
            resources.ApplyResources(this.exitBtn, "exitBtn");
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // connectionContainer
            // 
            this.connectionContainer.Controls.Add(this.input_Password);
            this.connectionContainer.Controls.Add(this.label6);
            this.connectionContainer.Controls.Add(this.input_Username);
            this.connectionContainer.Controls.Add(this.label5);
            this.connectionContainer.Controls.Add(this.input_IntegratedSecurity);
            this.connectionContainer.Controls.Add(this.label4);
            this.connectionContainer.Controls.Add(this.input_DatabaseName);
            this.connectionContainer.Controls.Add(this.label3);
            this.connectionContainer.Controls.Add(this.input_DataSource);
            this.connectionContainer.Controls.Add(this.label2);
            resources.ApplyResources(this.connectionContainer, "connectionContainer");
            this.connectionContainer.Name = "connectionContainer";
            // 
            // input_Password
            // 
            resources.ApplyResources(this.input_Password, "input_Password");
            this.input_Password.Name = "input_Password";
            this.input_Password.Tag = "Password";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // input_Username
            // 
            resources.ApplyResources(this.input_Username, "input_Username");
            this.input_Username.Name = "input_Username";
            this.input_Username.Tag = "Username";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // input_IntegratedSecurity
            // 
            this.input_IntegratedSecurity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.input_IntegratedSecurity.FormattingEnabled = true;
            this.input_IntegratedSecurity.Items.AddRange(new object[] {
            resources.GetString("input_IntegratedSecurity.Items"),
            resources.GetString("input_IntegratedSecurity.Items1")});
            resources.ApplyResources(this.input_IntegratedSecurity, "input_IntegratedSecurity");
            this.input_IntegratedSecurity.Name = "input_IntegratedSecurity";
            this.input_IntegratedSecurity.Tag = "Integrated Security";
            this.input_IntegratedSecurity.DropDownClosed += new System.EventHandler(this.input_IntegratedSecurity_DropDownClosed);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // input_DatabaseName
            // 
            resources.ApplyResources(this.input_DatabaseName, "input_DatabaseName");
            this.input_DatabaseName.Name = "input_DatabaseName";
            this.input_DatabaseName.Tag = "DatabaseName ";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // input_DataSource
            // 
            resources.ApplyResources(this.input_DataSource, "input_DataSource");
            this.input_DataSource.Name = "input_DataSource";
            this.input_DataSource.Tag = "DataSource";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // connectionTestOutput
            // 
            resources.ApplyResources(this.connectionTestOutput, "connectionTestOutput");
            this.connectionTestOutput.Name = "connectionTestOutput";
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.connectionTestOutput);
            this.Controls.Add(this.connectionContainer);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.testBtn);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.connectionContainer.ResumeLayout(false);
            this.connectionContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button testBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Panel connectionContainer;
        private System.Windows.Forms.TextBox input_Password;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox input_Username;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox input_IntegratedSecurity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox input_DatabaseName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox input_DataSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label connectionTestOutput;
    }
}