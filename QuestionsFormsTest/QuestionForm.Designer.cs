namespace QuestionsFormsTest
{
    partial class QuestionForm
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
            this.questionText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.containerStarQuestions = new System.Windows.Forms.Panel();
            this.containerSmileyQuestions = new System.Windows.Forms.Panel();
            this.input_NumOfSmiley = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.input_NumOfStars = new System.Windows.Forms.NumericUpDown();
            this.textbox4 = new System.Windows.Forms.Label();
            this.questionOrder = new System.Windows.Forms.NumericUpDown();
            this.containerSliderQuestions = new System.Windows.Forms.Panel();
            this.input_EndValueCaption = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.input_StartValueCaption = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.input_EndValue = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.input_StartValue = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.questionTypeCombo = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.controlBtn = new System.Windows.Forms.Button();
            this.containerStarQuestions.SuspendLayout();
            this.containerSmileyQuestions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.input_NumOfSmiley)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.input_NumOfStars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionOrder)).BeginInit();
            this.containerSliderQuestions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.input_EndValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.input_StartValue)).BeginInit();
            this.SuspendLayout();
            // 
            // questionText
            // 
            this.questionText.Location = new System.Drawing.Point(12, 25);
            this.questionText.MaxLength = 250;
            this.questionText.Multiline = true;
            this.questionText.Name = "questionText";
            this.questionText.Size = new System.Drawing.Size(323, 119);
            this.questionText.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Question text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Question order";
            // 
            // containerStarQuestions
            // 
            this.containerStarQuestions.Controls.Add(this.input_NumOfStars);
            this.containerStarQuestions.Controls.Add(this.textbox4);
            this.containerStarQuestions.Location = new System.Drawing.Point(12, 178);
            this.containerStarQuestions.Name = "containerStarQuestions";
            this.containerStarQuestions.Size = new System.Drawing.Size(323, 115);
            this.containerStarQuestions.TabIndex = 4;
            this.containerStarQuestions.Visible = false;
            // 
            // containerSmileyQuestions
            // 
            this.containerSmileyQuestions.Controls.Add(this.input_NumOfSmiley);
            this.containerSmileyQuestions.Controls.Add(this.label3);
            this.containerSmileyQuestions.Location = new System.Drawing.Point(12, 175);
            this.containerSmileyQuestions.Name = "containerSmileyQuestions";
            this.containerSmileyQuestions.Size = new System.Drawing.Size(323, 115);
            this.containerSmileyQuestions.TabIndex = 8;
            this.containerSmileyQuestions.Visible = false;
            // 
            // input_NumOfSmiley
            // 
            this.input_NumOfSmiley.Location = new System.Drawing.Point(250, 4);
            this.input_NumOfSmiley.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.input_NumOfSmiley.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.input_NumOfSmiley.Name = "input_NumOfSmiley";
            this.input_NumOfSmiley.Size = new System.Drawing.Size(73, 20);
            this.input_NumOfSmiley.TabIndex = 7;
            this.input_NumOfSmiley.Tag = "NumOfStars";
            this.input_NumOfSmiley.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.input_NumOfSmiley.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Number of smiley faces (from 2 to 5)";
            // 
            // input_NumOfStars
            // 
            this.input_NumOfStars.Location = new System.Drawing.Point(250, 4);
            this.input_NumOfStars.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.input_NumOfStars.Name = "input_NumOfStars";
            this.input_NumOfStars.Size = new System.Drawing.Size(73, 20);
            this.input_NumOfStars.TabIndex = 7;
            this.input_NumOfStars.Tag = "NumOfStars";
            this.input_NumOfStars.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textbox4
            // 
            this.textbox4.AutoSize = true;
            this.textbox4.Location = new System.Drawing.Point(0, 6);
            this.textbox4.Name = "textbox4";
            this.textbox4.Size = new System.Drawing.Size(129, 13);
            this.textbox4.TabIndex = 5;
            this.textbox4.Text = "Number of stars (up to 10)";
            // 
            // questionOrder
            // 
            this.questionOrder.Location = new System.Drawing.Point(262, 152);
            this.questionOrder.Name = "questionOrder";
            this.questionOrder.Size = new System.Drawing.Size(73, 20);
            this.questionOrder.TabIndex = 6;
            this.questionOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // containerSliderQuestions
            // 
            this.containerSliderQuestions.Controls.Add(this.input_EndValueCaption);
            this.containerSliderQuestions.Controls.Add(this.label7);
            this.containerSliderQuestions.Controls.Add(this.input_StartValueCaption);
            this.containerSliderQuestions.Controls.Add(this.label6);
            this.containerSliderQuestions.Controls.Add(this.input_EndValue);
            this.containerSliderQuestions.Controls.Add(this.label5);
            this.containerSliderQuestions.Controls.Add(this.input_StartValue);
            this.containerSliderQuestions.Controls.Add(this.label4);
            this.containerSliderQuestions.Location = new System.Drawing.Point(12, 175);
            this.containerSliderQuestions.Name = "containerSliderQuestions";
            this.containerSliderQuestions.Size = new System.Drawing.Size(323, 176);
            this.containerSliderQuestions.TabIndex = 9;
            this.containerSliderQuestions.Visible = false;
            // 
            // input_EndValueCaption
            // 
            this.input_EndValueCaption.Location = new System.Drawing.Point(105, 119);
            this.input_EndValueCaption.MaxLength = 250;
            this.input_EndValueCaption.Multiline = true;
            this.input_EndValueCaption.Name = "input_EndValueCaption";
            this.input_EndValueCaption.Size = new System.Drawing.Size(218, 56);
            this.input_EndValueCaption.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-3, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "End value caption";
            // 
            // input_StartValueCaption
            // 
            this.input_StartValueCaption.Location = new System.Drawing.Point(105, 57);
            this.input_StartValueCaption.MaxLength = 250;
            this.input_StartValueCaption.Multiline = true;
            this.input_StartValueCaption.Name = "input_StartValueCaption";
            this.input_StartValueCaption.Size = new System.Drawing.Size(218, 56);
            this.input_StartValueCaption.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-3, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Start value caption";
            // 
            // input_EndValue
            // 
            this.input_EndValue.Location = new System.Drawing.Point(250, 30);
            this.input_EndValue.Name = "input_EndValue";
            this.input_EndValue.Size = new System.Drawing.Size(73, 20);
            this.input_EndValue.TabIndex = 9;
            this.input_EndValue.Tag = "NumOfStars";
            this.input_EndValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.input_EndValue.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "End value";
            // 
            // input_StartValue
            // 
            this.input_StartValue.Location = new System.Drawing.Point(250, 4);
            this.input_StartValue.Name = "input_StartValue";
            this.input_StartValue.Size = new System.Drawing.Size(73, 20);
            this.input_StartValue.TabIndex = 7;
            this.input_StartValue.Tag = "NumOfStars";
            this.input_StartValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.input_StartValue.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Start value";
            // 
            // questionTypeCombo
            // 
            this.questionTypeCombo.FormattingEnabled = true;
            this.questionTypeCombo.Location = new System.Drawing.Point(380, 25);
            this.questionTypeCombo.Name = "questionTypeCombo";
            this.questionTypeCombo.Size = new System.Drawing.Size(161, 21);
            this.questionTypeCombo.TabIndex = 7;
            this.questionTypeCombo.Text = "Select question type...";
            this.questionTypeCombo.DropDownClosed += new System.EventHandler(this.questionTypeCombo_DropDownClosed);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(380, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // controlBtn
            // 
            this.controlBtn.Location = new System.Drawing.Point(380, 99);
            this.controlBtn.Name = "controlBtn";
            this.controlBtn.Size = new System.Drawing.Size(161, 23);
            this.controlBtn.TabIndex = 9;
            this.controlBtn.UseVisualStyleBackColor = true;
            this.controlBtn.Click += new System.EventHandler(this.controlBtn_Click);
            // 
            // QuestionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 366);
            this.Controls.Add(this.containerSliderQuestions);
            this.Controls.Add(this.containerSmileyQuestions);
            this.Controls.Add(this.controlBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.questionTypeCombo);
            this.Controls.Add(this.questionOrder);
            this.Controls.Add(this.containerStarQuestions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.questionText);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(565, 405);
            this.MinimumSize = new System.Drawing.Size(565, 405);
            this.Name = "QuestionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuestionsForm";
            this.Load += new System.EventHandler(this.QuestionForm_Load);
            this.containerStarQuestions.ResumeLayout(false);
            this.containerStarQuestions.PerformLayout();
            this.containerSmileyQuestions.ResumeLayout(false);
            this.containerSmileyQuestions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.input_NumOfSmiley)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.input_NumOfStars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionOrder)).EndInit();
            this.containerSliderQuestions.ResumeLayout(false);
            this.containerSliderQuestions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.input_EndValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.input_StartValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox questionText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel containerStarQuestions;
        private System.Windows.Forms.Label textbox4;
        private System.Windows.Forms.Panel containerSmileyQuestions;
        private System.Windows.Forms.NumericUpDown input_NumOfSmiley;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown input_NumOfStars;
        private System.Windows.Forms.NumericUpDown questionOrder;
        private System.Windows.Forms.Panel containerSliderQuestions;
        private System.Windows.Forms.TextBox input_EndValueCaption;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox input_StartValueCaption;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown input_EndValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown input_StartValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox questionTypeCombo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button controlBtn;
    }
}