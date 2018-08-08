namespace sudokuTM
{
    partial class MainMenu
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button Tezka;
            this.Lehka = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Stredni = new System.Windows.Forms.Button();
            this.Konec = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Pokracovat = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            Tezka = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Tezka
            // 
            Tezka.BackColor = System.Drawing.Color.LightCoral;
            Tezka.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            Tezka.Location = new System.Drawing.Point(46, 283);
            Tezka.Name = "Tezka";
            Tezka.Size = new System.Drawing.Size(180, 50);
            Tezka.TabIndex = 3;
            Tezka.Text = "Těžká";
            Tezka.UseVisualStyleBackColor = false;
            Tezka.Click += new System.EventHandler(this.Hard_Click);
            // 
            // Lehka
            // 
            this.Lehka.BackColor = System.Drawing.Color.LightGreen;
            this.Lehka.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lehka.Location = new System.Drawing.Point(46, 171);
            this.Lehka.Name = "Lehka";
            this.Lehka.Size = new System.Drawing.Size(180, 50);
            this.Lehka.TabIndex = 0;
            this.Lehka.Text = "Lehká";
            this.Lehka.UseVisualStyleBackColor = false;
            this.Lehka.Click += new System.EventHandler(this.Easy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Bauhaus 93", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 57);
            this.label1.TabIndex = 1;
            this.label1.Text = "SUDOKU";
            // 
            // Stredni
            // 
            this.Stredni.BackColor = System.Drawing.Color.LemonChiffon;
            this.Stredni.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Stredni.Location = new System.Drawing.Point(46, 227);
            this.Stredni.Name = "Stredni";
            this.Stredni.Size = new System.Drawing.Size(180, 50);
            this.Stredni.TabIndex = 2;
            this.Stredni.Text = "Střední";
            this.Stredni.UseVisualStyleBackColor = false;
            this.Stredni.Click += new System.EventHandler(this.Normal_Click);
            // 
            // Konec
            // 
            this.Konec.Font = new System.Drawing.Font("Tahoma", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Konec.Location = new System.Drawing.Point(61, 339);
            this.Konec.Name = "Konec";
            this.Konec.Size = new System.Drawing.Size(150, 50);
            this.Konec.TabIndex = 4;
            this.Konec.Text = "Konec";
            this.Konec.UseVisualStyleBackColor = true;
            this.Konec.Click += new System.EventHandler(this.Exit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Moccasin;
            this.label2.Location = new System.Drawing.Point(58, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Vyber úroveň obtížnosti:";
            // 
            // Pokracovat
            // 
            this.Pokracovat.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Pokracovat.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Pokracovat.Location = new System.Drawing.Point(46, 106);
            this.Pokracovat.Name = "Pokracovat";
            this.Pokracovat.Size = new System.Drawing.Size(180, 32);
            this.Pokracovat.TabIndex = 7;
            this.Pokracovat.Text = "Pokračovat";
            this.Pokracovat.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Moccasin;
            this.label3.Location = new System.Drawing.Point(8, 427);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(262, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tereza Miklóšová verze 20180803.1956";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Moccasin;
            this.ClientSize = new System.Drawing.Size(282, 453);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Pokracovat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Konec);
            this.Controls.Add(Tezka);
            this.Controls.Add(this.Stredni);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Lehka);
            this.Name = "Form1";
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        /// <summary>
        /// Po kliknutí načte lehké sudoku.
        /// </summary>
        private System.Windows.Forms.Button Lehka;
        /// <summary>
        /// Název hry.
        /// </summary>
        private System.Windows.Forms.Label label1;
        /// <summary>
        /// Po kliknutí načte středně těžké sudoku.
        /// </summary>
        private System.Windows.Forms.Button Stredni;
        /// <summary>
        /// Po kliknutí ukončí hru.
        /// </summary>
        private System.Windows.Forms.Button Konec;
        /// <summary>
        /// Informační label.
        /// </summary>
        private System.Windows.Forms.Label label2;
        /// <summary>
        /// Objeví se pouze v případě, že uživatel má ve složce lehka uloženou hru. Načte pokračování uložené hry.
        /// </summary>
        private System.Windows.Forms.Button Pokracovat;
        /// <summary>
        /// Obsahuje informace o verzi projektu.
        /// </summary>
        private System.Windows.Forms.Label label3;
    }
}

