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
            this.label1 = new System.Windows.Forms.Label();
            this.pause = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
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
            this.Vyhodnot.Click += new System.EventHandler(this.Vyhodnot_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(497, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 25);
            this.label1.TabIndex = 87;
            this.label1.Text = "00:00:00";
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
            // save
            // 
            this.save.Image = global::sudokuTM.Properties.Resources.save;
            this.save.Location = new System.Drawing.Point(708, 32);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(79, 77);
            this.save.TabIndex = 89;
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(502, 115);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(190, 21);
            this.checkBox1.TabIndex = 90;
            this.checkBox1.Text = "Podbarvení stejných čísel";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(502, 139);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(289, 21);
            this.checkBox2.TabIndex = 91;
            this.checkBox2.Text = "Vyřazení nehodících se čísel z klávesnice";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // Sudoku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 511);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.save);
            this.Controls.Add(this.pause);
            this.Controls.Add(this.label1);
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
        /// Tlačítka v mřízce sudoku, souřadnice jdou odshora dolů zleva doprava.
        /// </summary>
        
        private System.Windows.Forms.Timer timer1;
        /// <summary>
        /// Po kliknutí na toto tlačítko se vyhodnotí stav hry.
        /// </summary>
        private System.Windows.Forms.Button Vyhodnot;
        /// <summary>
        /// Vyhrazuje prostor, kde se ukazuje čas strávený řešením aktuálního sudoku. Aktualizuje se každou seknudu.
        /// </summary>
        private System.Windows.Forms.Label label1;
        /// <summary>
        /// Tlačítko, které umožňuje pozastavení hry.
        /// </summary>
        private System.Windows.Forms.Button pause;
        /// <summary>
        /// Po stisknutí uloží hru.
        /// </summary>
        private System.Windows.Forms.Button save;
        /// <summary>
        /// Umožňuje vypnutí/zapnutí zvýrazňování stejných čísel v mřížce.
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox1;
        /// <summary>
        /// Umožňuje vypnutí/zapnutí rádce v nabídce čísel.
        /// </summary>
        private System.Windows.Forms.CheckBox checkBox2;
    }
}