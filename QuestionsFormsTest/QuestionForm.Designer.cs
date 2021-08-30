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
            this.input_Text = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.containerStarQuestions = new System.Windows.Forms.Panel();
            this.input_NumOfStars = new System.Windows.Forms.NumericUpDown();
            this.textbox4 = new System.Windows.Forms.Label();
            this.containerSmileyQuestions = new System.Windows.Forms.Panel();
            this.input_NumOfSmiley = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.input_QOrder = new System.Windows.Forms.NumericUpDown();
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
            this.exitButton = new System.Windows.Forms.Button();
            this.controlBtn = new System.Windows.Forms.Button();
            this.headerLbl = new System.Windows.Forms.Label();
            this.containerQuestion = new System.Windows.Forms.Panel();
            this.containerStarQuestions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.input_NumOfStars)).BeginInit();
            this.containerSmileyQuestions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.input_NumOfSmiley)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.input_QOrder)).BeginInit();
            this.containerSliderQuestions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.input_EndValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.input_StartValue)).BeginInit();
            this.containerQuestion.SuspendLayout();
            this.SuspendLayout();
            // 
            // input_Text
            // 
            this.input_Text.Location = new System.Drawing.Point(0, 19);
            this.input_Text.MaxLength = 250;
            this.input_Text.Multiline = true;
            this.input_Text.Name = "input_Text";
            this.input_Text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.input_Text.Size = new System.Drawing.Size(345, 119);
            this.input_Text.TabIndex = 3;
            this.input_Text.Tag = "Question text";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Question text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-3, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Question order";
            // 
            // containerStarQuestions
            // 
            this.containerStarQuestions.Controls.Add(this.input_NumOfStars);
            this.containerStarQuestions.Controls.Add(this.textbox4);
            this.containerStarQuestions.Location = new System.Drawing.Point(0, 170);
            this.containerStarQuestions.Name = "containerStarQuestions";
            this.containerStarQuestions.Size = new System.Drawing.Size(345, 123);
            this.containerStarQuestions.TabIndex = 12;
            this.containerStarQuestions.Visible = false;
            // 
            // input_NumOfStars
            // 
            this.input_NumOfStars.Location = new System.Drawing.Point(272, 2);
            this.input_NumOfStars.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.input_NumOfStars.Name = "input_NumOfStars";
            this.input_NumOfStars.Size = new System.Drawing.Size(73, 20);
            this.input_NumOfStars.TabIndex = 15;
            this.input_NumOfStars.Tag = "Number of stars";
            this.input_NumOfStars.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textbox4
            // 
            this.textbox4.AutoSize = true;
            this.textbox4.Location = new System.Drawing.Point(-3, 4);
            this.textbox4.Name = "textbox4";
            this.textbox4.Size = new System.Drawing.Size(129, 13);
            this.textbox4.TabIndex = 8;
            this.textbox4.Text = "Number of stars (up to 10)";
            // 
            // containerSmileyQuestions
            // 
            this.containerSmileyQuestions.Controls.Add(this.input_NumOfSmiley);
            this.containerSmileyQuestions.Controls.Add(this.label3);
            this.containerSmileyQuestions.Location = new System.Drawing.Point(0, 170);
            this.containerSmileyQuestions.Name = "containerSmileyQuestions";
            this.containerSmileyQuestions.Size = new System.Drawing.Size(345, 126);
            this.containerSmileyQuestions.TabIndex = 5;
            this.containerSmileyQuestions.Visible = false;
            // 
            // input_NumOfSmiley
            // 
            this.input_NumOfSmiley.Location = new System.Drawing.Point(272, 2);
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
            this.input_NumOfSmiley.TabIndex = 10;
            this.input_NumOfSmiley.Tag = "Number of smiley";
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
            this.label3.Location = new System.Drawing.Point(-3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Number of smiley faces (from 2 to 5)";
            // 
            // input_QOrder
            // 
            this.input_QOrder.Location = new System.Drawing.Point(272, 144);
            this.input_QOrder.Name = "input_QOrder";
            this.input_QOrder.Size = new System.Drawing.Size(73, 20);
            this.input_QOrder.TabIndex = 4;
            this.input_QOrder.Tag = "Question order";
            this.input_QOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.containerSliderQuestions.Location = new System.Drawing.Point(0, 170);
            this.containerSliderQuestions.Name = "containerSliderQuestions";
            this.containerSliderQuestions.Size = new System.Drawing.Size(345, 181);
            this.containerSliderQuestions.TabIndex = 7;
            this.containerSliderQuestions.Visible = false;
            // 
            // input_EndValueCaption
            // 
            this.input_EndValueCaption.Location = new System.Drawing.Point(103, 117);
            this.input_EndValueCaption.MaxLength = 250;
            this.input_EndValueCaption.Multiline = true;
            this.input_EndValueCaption.Name = "input_EndValueCaption";
            this.input_EndValueCaption.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.input_EndValueCaption.Size = new System.Drawing.Size(241, 56);
            this.input_EndValueCaption.TabIndex = 13;
            this.input_EndValueCaption.Tag = "End value caption";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-1, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "End value caption";
            // 
            // input_StartValueCaption
            // 
            this.input_StartValueCaption.Location = new System.Drawing.Point(103, 55);
            this.input_StartValueCaption.MaxLength = 250;
            this.input_StartValueCaption.Multiline = true;
            this.input_StartValueCaption.Name = "input_StartValueCaption";
            this.input_StartValueCaption.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.input_StartValueCaption.Size = new System.Drawing.Size(242, 56);
            this.input_StartValueCaption.TabIndex = 11;
            this.input_StartValueCaption.Tag = "End value caption";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-3, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Start value caption";
            // 
            // input_EndValue
            // 
            this.input_EndValue.Location = new System.Drawing.Point(272, 27);
            this.input_EndValue.Name = "input_EndValue";
            this.input_EndValue.Size = new System.Drawing.Size(73, 20);
            this.input_EndValue.TabIndex = 9;
            this.input_EndValue.Tag = "End value";
            this.input_EndValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.input_EndValue.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.input_EndValue.ValueChanged += new System.EventHandler(this.input_EndStartValues_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-3, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "End value";
            // 
            // input_StartValue
            // 
            this.input_StartValue.Location = new System.Drawing.Point(272, 1);
            this.input_StartValue.Name = "input_StartValue";
            this.input_StartValue.Size = new System.Drawing.Size(73, 20);
            this.input_StartValue.TabIndex = 7;
            this.input_StartValue.Tag = "Start value";
            this.input_StartValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.input_StartValue.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.input_StartValue.ValueChanged += new System.EventHandler(this.input_EndStartValues_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Start value";
            // 
            // questionTypeCombo
            // 
            this.questionTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.questionTypeCombo.FormattingEnabled = true;
            this.questionTypeCombo.Location = new System.Drawing.Point(200, 12);
            this.questionTypeCombo.Name = "questionTypeCombo";
            this.questionTypeCombo.Size = new System.Drawing.Size(161, 21);
            this.questionTypeCombo.TabIndex = 0;
            this.questionTypeCombo.DropDownClosed += new System.EventHandler(this.questionTypeCombo_DropDownClosed);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(283, 399);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(78, 23);
            this.exitButton.TabIndex = 20;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // controlBtn
            // 
            this.controlBtn.Location = new System.Drawing.Point(16, 399);
            this.controlBtn.Name = "controlBtn";
            this.controlBtn.Size = new System.Drawing.Size(78, 23);
            this.controlBtn.TabIndex = 19;
            this.controlBtn.UseVisualStyleBackColor = true;
            this.controlBtn.Click += new System.EventHandler(this.controlBtn_Click);
            // 
            // headerLbl
            // 
            this.headerLbl.AutoSize = true;
            this.headerLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLbl.Location = new System.Drawing.Point(12, 13);
            this.headerLbl.Name = "headerLbl";
            this.headerLbl.Size = new System.Drawing.Size(130, 20);
            this.headerLbl.TabIndex = 10;
            this.headerLbl.Text = "Update Question";
            // 
            // containerQuestion
            // 
            this.containerQuestion.Controls.Add(this.label1);
            this.containerQuestion.Controls.Add(this.input_Text);
            this.containerQuestion.Controls.Add(this.containerStarQuestions);
            this.containerQuestion.Controls.Add(this.containerSmileyQuestions);
            this.containerQuestion.Controls.Add(this.label2);
            this.containerQuestion.Controls.Add(this.input_QOrder);
            this.containerQuestion.Controls.Add(this.containerSliderQuestions);
            this.containerQuestion.Location = new System.Drawing.Point(16, 39);
            this.containerQuestion.Name = "containerQuestion";
            this.containerQuestion.Size = new System.Drawing.Size(345, 354);
            this.containerQuestion.TabIndex = 11;
            // 
            // QuestionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 432);
            this.Controls.Add(this.containerQuestion);
            this.Controls.Add(this.headerLbl);
            this.Controls.Add(this.questionTypeCombo);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.controlBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(389, 471);
            this.MinimumSize = new System.Drawing.Size(389, 471);
            this.Name = "QuestionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuestionsForm";
            this.Load += new System.EventHandler(this.QuestionForm_Load);
            this.containerStarQuestions.ResumeLayout(false);
            this.containerStarQuestions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.input_NumOfStars)).EndInit();
            this.containerSmileyQuestions.ResumeLayout(false);
            this.containerSmileyQuestions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.input_NumOfSmiley)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.input_QOrder)).EndInit();
            this.containerSliderQuestions.ResumeLayout(false);
            this.containerSliderQuestions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.input_EndValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.input_StartValue)).EndInit();
            this.containerQuestion.ResumeLayout(false);
            this.containerQuestion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox input_Text;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel containerStarQuestions;
        private System.Windows.Forms.Label textbox4;
        private System.Windows.Forms.Panel containerSmileyQuestions;
        private System.Windows.Forms.NumericUpDown input_NumOfSmiley;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown input_NumOfStars;
        private System.Windows.Forms.NumericUpDown input_QOrder;
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
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button controlBtn;
        private System.Windows.Forms.Label headerLbl;
        private System.Windows.Forms.Panel containerQuestion;
    }
}