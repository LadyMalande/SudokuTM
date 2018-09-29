namespace sudokuTM
{   
    /// <summary>
    /// 
    /// </summary>
    partial class Solver1
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
            this.SolveButton = new System.Windows.Forms.Button();
            this.HighlightSameNumbersCheckBox = new System.Windows.Forms.CheckBox();
            this.num1 = new System.Windows.Forms.Button();
            this.num2 = new System.Windows.Forms.Button();
            this.num3 = new System.Windows.Forms.Button();
            this.num4 = new System.Windows.Forms.Button();
            this.num5 = new System.Windows.Forms.Button();
            this.num6 = new System.Windows.Forms.Button();
            this.num7 = new System.Windows.Forms.Button();
            this.num8 = new System.Windows.Forms.Button();
            this.num9 = new System.Windows.Forms.Button();
            this.SolverDeleteButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SolveButton
            // 
            this.SolveButton.BackColor = System.Drawing.Color.LimeGreen;
            this.SolveButton.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.SolveButton.Location = new System.Drawing.Point(587, 68);
            this.SolveButton.Name = "SolveButton";
            this.SolveButton.Size = new System.Drawing.Size(183, 64);
            this.SolveButton.TabIndex = 0;
            this.SolveButton.Text = "Doplň zbytek!";
            this.SolveButton.UseVisualStyleBackColor = false;
            this.SolveButton.Click += new System.EventHandler(this.SolveButton_Click);
            // 
            // HighlightSameNumbersCheckBox
            // 
            this.HighlightSameNumbersCheckBox.AutoSize = true;
            this.HighlightSameNumbersCheckBox.Location = new System.Drawing.Point(587, 138);
            this.HighlightSameNumbersCheckBox.Name = "HighlightSameNumbersCheckBox";
            this.HighlightSameNumbersCheckBox.Size = new System.Drawing.Size(240, 21);
            this.HighlightSameNumbersCheckBox.TabIndex = 1;
            this.HighlightSameNumbersCheckBox.Text = "HighlightSameNumbersCheckBox";
            this.HighlightSameNumbersCheckBox.UseVisualStyleBackColor = true;
            // 
            // num1
            // 
            this.num1.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold);
            this.num1.Location = new System.Drawing.Point(587, 198);
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(50, 50);
            this.num1.TabIndex = 2;
            this.num1.Text = "1";
            this.num1.UseVisualStyleBackColor = true;
            this.num1.Click += new System.EventHandler(this.num_Click);
            // 
            // num2
            // 
            this.num2.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold);
            this.num2.Location = new System.Drawing.Point(643, 198);
            this.num2.Name = "num2";
            this.num2.Size = new System.Drawing.Size(50, 50);
            this.num2.TabIndex = 3;
            this.num2.Text = "2";
            this.num2.UseVisualStyleBackColor = true;
            this.num2.Click += new System.EventHandler(this.num_Click);
            // 
            // num3
            // 
            this.num3.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold);
            this.num3.Location = new System.Drawing.Point(699, 198);
            this.num3.Name = "num3";
            this.num3.Size = new System.Drawing.Size(50, 50);
            this.num3.TabIndex = 4;
            this.num3.Text = "3";
            this.num3.UseVisualStyleBackColor = true;
            this.num3.Click += new System.EventHandler(this.num_Click);
            // 
            // num4
            // 
            this.num4.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold);
            this.num4.Location = new System.Drawing.Point(587, 256);
            this.num4.Name = "num4";
            this.num4.Size = new System.Drawing.Size(50, 50);
            this.num4.TabIndex = 5;
            this.num4.Text = "4";
            this.num4.UseVisualStyleBackColor = true;
            this.num4.Click += new System.EventHandler(this.num_Click);
            // 
            // num5
            // 
            this.num5.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold);
            this.num5.Location = new System.Drawing.Point(643, 256);
            this.num5.Name = "num5";
            this.num5.Size = new System.Drawing.Size(50, 50);
            this.num5.TabIndex = 6;
            this.num5.Text = "5";
            this.num5.UseVisualStyleBackColor = true;
            this.num5.Click += new System.EventHandler(this.num_Click);
            // 
            // num6
            // 
            this.num6.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold);
            this.num6.Location = new System.Drawing.Point(699, 256);
            this.num6.Name = "num6";
            this.num6.Size = new System.Drawing.Size(50, 50);
            this.num6.TabIndex = 7;
            this.num6.Text = "6";
            this.num6.UseVisualStyleBackColor = true;
            this.num6.Click += new System.EventHandler(this.num_Click);
            // 
            // num7
            // 
            this.num7.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold);
            this.num7.Location = new System.Drawing.Point(587, 312);
            this.num7.Name = "num7";
            this.num7.Size = new System.Drawing.Size(50, 50);
            this.num7.TabIndex = 8;
            this.num7.Text = "7";
            this.num7.UseVisualStyleBackColor = true;
            this.num7.Click += new System.EventHandler(this.num_Click);
            // 
            // num8
            // 
            this.num8.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold);
            this.num8.Location = new System.Drawing.Point(643, 312);
            this.num8.Name = "num8";
            this.num8.Size = new System.Drawing.Size(50, 50);
            this.num8.TabIndex = 9;
            this.num8.Text = "8";
            this.num8.UseVisualStyleBackColor = true;
            this.num8.Click += new System.EventHandler(this.num_Click);
            // 
            // num9
            // 
            this.num9.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold);
            this.num9.Location = new System.Drawing.Point(699, 312);
            this.num9.Name = "num9";
            this.num9.Size = new System.Drawing.Size(50, 50);
            this.num9.TabIndex = 10;
            this.num9.Text = "9";
            this.num9.UseVisualStyleBackColor = true;
            this.num9.Click += new System.EventHandler(this.num_Click);
            // 
            // SolverDeleteButton
            // 
            this.SolverDeleteButton.Font = new System.Drawing.Font("Tahoma", 15.2F, System.Drawing.FontStyle.Bold);
            this.SolverDeleteButton.Location = new System.Drawing.Point(587, 368);
            this.SolverDeleteButton.Name = "SolverDeleteButton";
            this.SolverDeleteButton.Size = new System.Drawing.Size(162, 45);
            this.SolverDeleteButton.TabIndex = 11;
            this.SolverDeleteButton.Text = "Vymaž";
            this.SolverDeleteButton.UseVisualStyleBackColor = true;
            this.SolverDeleteButton.Click += new System.EventHandler(this.SolverDeleteButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 4.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(841, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "label1";
            // 
            // Solver1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 720);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SolverDeleteButton);
            this.Controls.Add(this.num9);
            this.Controls.Add(this.num8);
            this.Controls.Add(this.num7);
            this.Controls.Add(this.num6);
            this.Controls.Add(this.num5);
            this.Controls.Add(this.num4);
            this.Controls.Add(this.num3);
            this.Controls.Add(this.num2);
            this.Controls.Add(this.num1);
            this.Controls.Add(this.HighlightSameNumbersCheckBox);
            this.Controls.Add(this.SolveButton);
            this.Name = "Solver1";
            this.Text = "Solver1";
            this.Load += new System.EventHandler(this.Solver1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SolveButton;
        private System.Windows.Forms.Button num1;
        private System.Windows.Forms.Button num2;
        private System.Windows.Forms.Button num3;
        private System.Windows.Forms.Button num4;
        private System.Windows.Forms.Button num5;
        private System.Windows.Forms.Button num6;
        private System.Windows.Forms.Button num7;
        private System.Windows.Forms.Button num8;
        private System.Windows.Forms.Button num9;
        /// <summary>
        /// Pomocný zaškrtávací box
        /// </summary>
        public System.Windows.Forms.CheckBox HighlightSameNumbersCheckBox;
        private System.Windows.Forms.Button SolverDeleteButton;
        private System.Windows.Forms.Label label1;
    }
}