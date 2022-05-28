
namespace CheckersUI
{
    public partial class FormSetup
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
            this.buttonDone = new System.Windows.Forms.Button();
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.textBoxPlayer1Name = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2Name = new System.Windows.Forms.TextBox();
            this.radioButton6X6 = new System.Windows.Forms.RadioButton();
            this.radioButton8X8 = new System.Windows.Forms.RadioButton();
            this.radioButton10X10 = new System.Windows.Forms.RadioButton();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(152, 199);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(74, 32);
            this.buttonDone.TabIndex = 0;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Location = new System.Drawing.Point(9, 17);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(81, 17);
            this.labelBoardSize.TabIndex = 1;
            this.labelBoardSize.Text = "Board Size:";
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Location = new System.Drawing.Point(9, 86);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(59, 17);
            this.labelPlayers.TabIndex = 2;
            this.labelPlayers.Text = "Players:";
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Location = new System.Drawing.Point(30, 115);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(60, 17);
            this.labelPlayer1.TabIndex = 3;
            this.labelPlayer1.Text = "Player1:";
            // 
            // textBoxPlayer1Name
            // 
            this.textBoxPlayer1Name.Location = new System.Drawing.Point(126, 110);
            this.textBoxPlayer1Name.Name = "textBoxPlayer1Name";
            this.textBoxPlayer1Name.Size = new System.Drawing.Size(100, 22);
            this.textBoxPlayer1Name.TabIndex = 5;
            this.textBoxPlayer1Name.TextChanged += new System.EventHandler(this.textBoxPlayer1Name_TextChanged);
            // 
            // textBoxPlayer2Name
            // 
            this.textBoxPlayer2Name.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBoxPlayer2Name.Enabled = false;
            this.textBoxPlayer2Name.Location = new System.Drawing.Point(126, 151);
            this.textBoxPlayer2Name.Name = "textBoxPlayer2Name";
            this.textBoxPlayer2Name.Size = new System.Drawing.Size(100, 22);
            this.textBoxPlayer2Name.TabIndex = 6;
            this.textBoxPlayer2Name.TextChanged += new System.EventHandler(this.textBoxPlayer2Name_TextChanged);
            // 
            // radioButton6X6
            // 
            this.radioButton6X6.AutoSize = true;
            this.radioButton6X6.Location = new System.Drawing.Point(12, 49);
            this.radioButton6X6.Name = "radioButton6X6";
            this.radioButton6X6.Size = new System.Drawing.Size(62, 21);
            this.radioButton6X6.TabIndex = 7;
            this.radioButton6X6.TabStop = true;
            this.radioButton6X6.Text = "6 X 6";
            this.radioButton6X6.UseVisualStyleBackColor = true;
            this.radioButton6X6.CheckedChanged += new System.EventHandler(this.radioButton6X6_CheckedChanged);
            // 
            // radioButton8X8
            // 
            this.radioButton8X8.AutoSize = true;
            this.radioButton8X8.Location = new System.Drawing.Point(80, 49);
            this.radioButton8X8.Name = "radioButton8X8";
            this.radioButton8X8.Size = new System.Drawing.Size(62, 21);
            this.radioButton8X8.TabIndex = 8;
            this.radioButton8X8.TabStop = true;
            this.radioButton8X8.Text = "8 X 8";
            this.radioButton8X8.UseVisualStyleBackColor = true;
            this.radioButton8X8.CheckedChanged += new System.EventHandler(this.radioButton8X8_CheckedChanged);
            // 
            // radioButton10X10
            // 
            this.radioButton10X10.AutoSize = true;
            this.radioButton10X10.Location = new System.Drawing.Point(148, 49);
            this.radioButton10X10.Name = "radioButton10X10";
            this.radioButton10X10.Size = new System.Drawing.Size(78, 21);
            this.radioButton10X10.TabIndex = 9;
            this.radioButton10X10.TabStop = true;
            this.radioButton10X10.Text = "10 X 10";
            this.radioButton10X10.UseVisualStyleBackColor = true;
            this.radioButton10X10.CheckedChanged += new System.EventHandler(this.radioButton10X10_CheckedChanged);
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(31, 151);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(86, 21);
            this.checkBoxPlayer2.TabIndex = 10;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // FormSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 241);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.radioButton10X10);
            this.Controls.Add(this.radioButton8X8);
            this.Controls.Add(this.radioButton6X6);
            this.Controls.Add(this.textBoxPlayer2Name);
            this.Controls.Add(this.textBoxPlayer1Name);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.labelBoardSize);
            this.Controls.Add(this.buttonDone);
            this.Name = "FormSetup";
            this.Text = "Game Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.TextBox textBoxPlayer1Name;
        private System.Windows.Forms.TextBox textBoxPlayer2Name;
        private System.Windows.Forms.RadioButton radioButton6X6;
        private System.Windows.Forms.RadioButton radioButton8X8;
        private System.Windows.Forms.RadioButton radioButton10X10;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
    }
}