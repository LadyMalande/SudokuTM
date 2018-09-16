using System.Windows.Forms;

namespace sudokuTM
{
    partial class Sudoku
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Vyhodnot = new System.Windows.Forms.Button();
            this.TimerLabel = new System.Windows.Forms.Label();
            this.pause = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.HighlightSameNumbersCheckBox = new System.Windows.Forms.CheckBox();
            this.DoNotHighlightDumbNumbersCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Vyhodnot
            // 
            this.Vyhodnot.Location = new System.Drawing.Point(602, 32);
            this.Vyhodnot.Name = "Vyhodnot";
            this.Vyhodnot.Size = new System.Drawing.Size(100, 35);
            this.Vyhodnot.TabIndex = 85;
            this.Vyhodnot.Text = "Vyhodnoť!";
            this.Vyhodnot.UseVisualStyleBackColor = true;
            this.Vyhodnot.Click += new System.EventHandler(this.Evaluate_Click);
            // 
            // TimerLabel
            // 
            this.TimerLabel.AutoSize = true;
            this.TimerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TimerLabel.Location = new System.Drawing.Point(497, 42);
            this.TimerLabel.Name = "TimerLabel";
            this.TimerLabel.Size = new System.Drawing.Size(90, 25);
            this.TimerLabel.TabIndex = 87;
            this.TimerLabel.Text = "00:00:00";
            // 
            // pause
            // 
            this.pause.Location = new System.Drawing.Point(502, 85);
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(75, 24);
            this.pause.TabIndex = 88;
            this.pause.Text = "Pauza";
            this.pause.UseVisualStyleBackColor = true;
            this.pause.Click += new System.EventHandler(this.pause_Click);
            // 
            // Save
            // 
            this.Save.Image = global::sudokuTM.Properties.Resources.save;
            this.Save.Location = new System.Drawing.Point(708, 32);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(79, 77);
            this.Save.TabIndex = 89;
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // HighlightSameNumbersCheckBox
            // 
            this.HighlightSameNumbersCheckBox.AutoSize = true;
            this.HighlightSameNumbersCheckBox.Checked = true;
            this.HighlightSameNumbersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HighlightSameNumbersCheckBox.Location = new System.Drawing.Point(502, 115);
            this.HighlightSameNumbersCheckBox.Name = "HighlightSameNumbersCheckBox";
            this.HighlightSameNumbersCheckBox.Size = new System.Drawing.Size(190, 21);
            this.HighlightSameNumbersCheckBox.TabIndex = 90;
            this.HighlightSameNumbersCheckBox.Text = "Podbarvení stejných čísel";
            this.HighlightSameNumbersCheckBox.UseVisualStyleBackColor = true;
            // 
            // DoNotHighlightDumbNumbersCheckBox
            // 
            this.DoNotHighlightDumbNumbersCheckBox.AutoSize = true;
            this.DoNotHighlightDumbNumbersCheckBox.Checked = true;
            this.DoNotHighlightDumbNumbersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DoNotHighlightDumbNumbersCheckBox.Location = new System.Drawing.Point(502, 139);
            this.DoNotHighlightDumbNumbersCheckBox.Name = "DoNotHighlightDumbNumbersCheckBox";
            this.DoNotHighlightDumbNumbersCheckBox.Size = new System.Drawing.Size(289, 21);
            this.DoNotHighlightDumbNumbersCheckBox.TabIndex = 91;
            this.DoNotHighlightDumbNumbersCheckBox.Text = "Vyřazení nehodících se čísel z klávesnice";
            this.DoNotHighlightDumbNumbersCheckBox.UseVisualStyleBackColor = true;
            // 
            // Sudoku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 511);
            this.Controls.Add(this.DoNotHighlightDumbNumbersCheckBox);
            this.Controls.Add(this.HighlightSameNumbersCheckBox);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.pause);
            this.Controls.Add(this.TimerLabel);
            this.Controls.Add(this.Vyhodnot);
            this.Name = "Sudoku";
            this.Text = "Form3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Sudoku_FormClosing);
            this.Load += new System.EventHandler(this.Sudoku_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sudoku_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        /// <summary>
        /// Tlačítka v mřízce Sudoku, souřadnice jdou odshora dolů zleva doprava.
        /// </summary>
        
        private System.Windows.Forms.Timer timer1;
        /// <summary>
        /// Po kliknutí na toto tlačítko se vyhodnotí stav hry.
        /// </summary>
        private System.Windows.Forms.Button Vyhodnot;
        /// <summary>
        /// Vyhrazuje prostor, kde se ukazuje čas strávený řešením aktuálního Sudoku. Aktualizuje se každou seknudu.
        /// </summary>
        private System.Windows.Forms.Label TimerLabel;
        /// <summary>
        /// Tlačítko, které umožňuje pozastavení hry.
        /// </summary>
        private System.Windows.Forms.Button pause;
        /// <summary>
        /// Po stisknutí uloží hru.
        /// </summary>
        private System.Windows.Forms.Button Save;
        /// <summary>
        /// Umožňuje vypnutí/zapnutí zvýrazňování stejných čísel v mřížce.
        /// </summary>
        private System.Windows.Forms.CheckBox HighlightSameNumbersCheckBox;
        /// <summary>
        /// Umožňuje vypnutí/zapnutí rádce v nabídce čísel.
        /// </summary>
        private System.Windows.Forms.CheckBox DoNotHighlightDumbNumbersCheckBox;
    }
}