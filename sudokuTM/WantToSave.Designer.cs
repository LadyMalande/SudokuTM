namespace sudokuTM
{
    partial class WantToSave
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
            this.Lbutton = new System.Windows.Forms.Button();
            this.Rbutton = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lbutton
            // 
            this.Lbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Lbutton.Location = new System.Drawing.Point(164, 91);
            this.Lbutton.Name = "Lbutton";
            this.Lbutton.Size = new System.Drawing.Size(100, 50);
            this.Lbutton.TabIndex = 0;
            this.Lbutton.Text = "Lbutton";
            this.Lbutton.UseVisualStyleBackColor = true;
            // 
            // Rbutton
            // 
            this.Rbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Rbutton.Location = new System.Drawing.Point(270, 91);
            this.Rbutton.Name = "Rbutton";
            this.Rbutton.Size = new System.Drawing.Size(100, 50);
            this.Rbutton.TabIndex = 1;
            this.Rbutton.Text = "Rbutton";
            this.Rbutton.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblMessage.Location = new System.Drawing.Point(19, 29);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(62, 25);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Text1";
            // 
            // WantToSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 153);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.Rbutton);
            this.Controls.Add(this.Lbutton);
            this.Name = "WantToSave";
            this.Load += new System.EventHandler(this.WantToSave_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        /// <summary>
        /// Levé tlačítko okna Form4
        /// </summary>
        private System.Windows.Forms.Button Lbutton;
        /// <summary>
        /// Pravé tlačítko okna Form4
        /// </summary>
        private System.Windows.Forms.Button Rbutton;
        /// <summary>
        /// Text na okně
        /// </summary>
        private System.Windows.Forms.Label lblMessage;
    }
}