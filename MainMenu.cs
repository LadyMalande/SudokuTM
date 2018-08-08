using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace sudokuTM
{   /// <summary>
    /// Třída Form1 umožňuje uživateli zapnout samotnou hru. Obsahuje tlačítka vytvářející hru a tlačítko pro ukončení aplikace.
    /// </summary>
    public partial class MainMenu : Form
    {
        /// <summary>
        /// Parametr AlreadyLoaded zamezuje opakovanému zavolání metody sudoku.LoadDirectory(), která vyžaduje existenci uložené hry.
        /// </summary>
        public static bool AlreadyLoaded;

        /// <summary>
        /// Form3 je okno samotné hry sudoku.
        /// </summary>
        static public Sudoku sudoku;
        /// <summary>
        /// Při zapnutí souboru SudokuTM.exe se spustí právě Form1. Ze základu je parametr AlreadyLoaded nastaven na false, protože tlačítko "Pokračovat" nemohlo být použito.
        /// </summary>
        public MainMenu()
        {
            AlreadyLoaded = false;
            InitializeComponent();
        }

        /// <summary>
        /// Metoda RefreshMenu obnovuje Form1 a zobrazí tlačítko "Pokračovat", pokud zjistí přítomnost uložené hry.
        /// </summary>
        public void RefreshMenu()
        {

            if (File.Exists("./lehka/pokracovani.txt"))
            {
                AlreadyLoaded = false;
                Pokracovat.Show();
                Pokracovat.Click += new EventHandler(Continue_Click);
            }
            else
            {
                Pokracovat.Hide();
            }
        }
        /// <summary>
        /// Timer RefreshTime mapuje čas, kdy se Form1 aktualizuje (volá metodu RefreshMenu).
        /// </summary>
        private Timer RefreshTime;

        /// <summary>
        /// Inicializace časovače RefreshTime pro obnovování Form1.
        /// </summary>
        public void InitRefreshTime()
        {
            RefreshTime = new Timer();
            RefreshTime.Tick += new EventHandler(RefreshTime_Tick);
            RefreshTime.Interval = 1000; // in miliseconds
            RefreshTime.Start();

        }

        /// <summary>
        /// Při uplynutí doby nastavené v metodě InitRefreshTime se zavolá aktualizace Form1 (RefreshMenu).
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void RefreshTime_Tick(object sender, EventArgs e)
        {
            RefreshMenu();
        }

        /// <summary>
        /// Při načtení Form1 se poprvé zjišťuje, zda má uživatel uloženou hru (a jestli se má zobrazit tlačítko "Pokračovat"). Nastaví se časovač RefreshTime.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void MainMenu_Load(object sender, EventArgs e)
        {
            RefreshMenu();
            InitRefreshTime();

        }

        /// <summary>
        /// Tato metoda se zavolá při kliknutí na tlačítko "Pokračovat". Pokud je to její první zavolání, načte uloženou hru. Metoda vždy nastaví parametr AlreadyLoaded na true, aby se už podruhé nepokoušela o načtení uložené hry.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void Continue_Click(object sender, EventArgs e)
        {
           
            if (!AlreadyLoaded)
            {
                sudoku = new Sudoku();

                sudoku.LoadDirectory("pokracovani");
                sudoku.Text = "Sudoku pokračování";
                sudoku.Show();
                Pokracovat.Hide();
            }
            
            AlreadyLoaded = true;
            
        }

        /// <summary>
        /// Tato metoda po kliknutí na tlačítko "Konec" zavře celou aplikaci. (Form1 i případně otevřené instance Form3).
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Tato metoda po kliknutí na tlačítko "Lehká" načte sudoku ze složky "lehka" a objeví se Form3 s načtenou počáteční pozicí hry.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void Easy_Click(object sender, EventArgs e)
        {
            sudoku = new Sudoku();
            sudoku.LoadDirectory("lehka");            
            sudoku.Text = "Sudoku lehké";
            sudoku.Show();
        }

        /// <summary>
        /// Tato metoda po kliknutí na tlačítko "Střední" načte sudoku ze složky "stredni" a objeví se Form3 s načtenou počáteční pozicí hry.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void Normal_Click(object sender, EventArgs e)
        {
            sudoku = new Sudoku();
            sudoku.LoadDirectory("stredni");            
            sudoku.Text = "Sudoku středně těžké";
            sudoku.Show();
        }

        /// <summary>
        /// Tato metoda po kliknutí na tlačítko "Těžká" načte sudoku ze složky "tezka" a objeví se Form3 s načtenou počáteční pozicí hry.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void Hard_Click(object sender, EventArgs e)
        {
            sudoku = new Sudoku();
            sudoku.LoadDirectory("tezka");            
            sudoku.Text = "Sudoku těžké";
            sudoku.Show();
        }
    }
}
