using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudokuTM
{
    public partial class Solver1 : Form
    {
        /// <summary>
        /// Seznam tlačítek použitých ve třídě Solver1.
        /// </summary>
        static public List<GridButton> ListOfGridButtonsSolver = new List<GridButton>();
        //private bool Stop;
        /// <summary>
        /// Dvojrozměrné pole pro sledování počtu teoreticky doplnitelných čísel v řádku. [číslo řádku, číslo 1-9] = počet doplnitelných čísel v řádku
        /// </summary>
        private int[,] RowNumberCount = new int[9, 9];
        /// <summary>
        /// Dvojrozměrné pole pro sledování počtu teoreticky doplnitelných čísel ve sloupci. [číslo sloupce, číslo 1-9] = počet doplnitelných čísel ve sloupci
        /// </summary>
        private int[,] ColumnNumberCount = new int[9, 9];
        /// <summary>
        /// Dvojrozměrné pole pro sledování počtu teoreticky doplnitelných čísel v buňce 3x3. [číslo buňky, číslo 1-9] = počet doplnitelných čísel v buňce 3x3
        /// </summary>
        private int[,] CellNumberCount = new int[9, 9];
        /*
        /// <summary>
        /// True pokud se do sudoku doplnilo číslo první logickou metodou. (RefreshNumberOfSolutions)
        /// </summary>
        private bool Updated;
        /// <summary>
        /// True pokud nebyla použita první logická metoda doplňování, ale použila se druhá metoda doplňování.
        /// </summary>
        private bool Updated2;
        /// <summary>
        /// False pokud nebylo doplněno žádné pole ani jednou ze dvou logických metod. Pokud je false, aktivuje se řešení hrubou silou metody BruteForceSolving().
        /// </summary>
        private bool Updated3;
        */
        /// <summary>
        /// Seznam tlačítek vyřešených v jedné iteraci RefreshNumberOfSolutions. Tato tlačítka budou odebrána ze seznamu ListOfNotSolvedButtons.
        /// </summary>
        private List<GridButton> ListOfAlreadySolvedButtons = new List<GridButton>();
        /// <summary>
        /// Seznam tlačítek sudoku.
        /// </summary>
        public static List<GridButton> ListOfGridButtons = new List<GridButton>();
        /// <summary>
        /// Seznam užiatelem nevyplněných tlačítek.
        /// </summary>
        private List<GridButton> ListOfNotSolvedButtons = new List<GridButton>();
        /// <summary>
        /// Mřížka tlačítek sudoku třídy Solver1.
        /// </summary>
        public GridButton[,] SolverGrid = new GridButton[9, 9];
        /// <summary>
        /// Aktivuje se po stisknutí numeriských kláves po označení nějakého z políček sudoku. Nereaguje na 0. Doplní do označeného pole stisknutou číslici.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Solver1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Sudoku.WhereIWantToFillInNumber != null)
                if (Char.IsDigit(e.KeyChar) && (e.KeyChar.ToString() != "0"))
                {
                    if (Sudoku.WhereIWantToFillInNumber.ForeColor == Color.DarkGray)
                    {

                        if (Sudoku.WhereIWantToFillInNumber.Text == " ") Sudoku.FilledInFields++;
                        Sudoku.WhereIWantToFillInNumber.Text = e.KeyChar.ToString();
                        Sudoku.WhereIWantToFillInNumber.SolverButtonStatus[Convert.ToInt32(Sudoku.WhereIWantToFillInNumber.Text) - 1] = true;
                        Sudoku.WhereIWantToFillInNumber.NumberOfSolutions = -1;
                        Sudoku.WhereIWantToFillInNumber.Solved = true;
                        ListOfNotSolvedButtons.Remove(Sudoku.WhereIWantToFillInNumber);
                    }
                }
        }

        /// <summary>
        /// Konstruktor formuláře Solver1. Inicializuje tlačítka.
        /// </summary>
        public Solver1()
        {
            ListOfGridButtonsSolver.Clear();
            InitializeComponent();
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Solver1_KeyPress);
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    int jd = 0, id = 0;
                    if (i >= 4) { id = 4; }
                    if (i >= 7) { id = 8; }
                    if (j >= 4) { jd = 4; }
                    if (j >= 7) { jd = 8; }
                    string NameOfButtonstring = (i).ToString() + (j).ToString();
                    GridButton NewGridButton = Sudoku.CreateGridButton(NameOfButtonstring, i, j, 35, j * 35 + jd, i * 35 + id, " ");
                    SolverGrid[i - 1, j - 1] = NewGridButton;
                    SolverGrid[i - 1, j - 1].Text = " ";
                    SolverGrid[i - 1, j - 1].ForeColor = Color.DarkGray;
                    SolverGrid[i - 1, j - 1].Solved = false;
                    Controls.Add(NewGridButton);
                    SolverGrid[i - 1, j - 1].Show();


                    ListOfGridButtonsSolver.Add(SolverGrid[i - 1, j - 1]);
                }
            }
            KeyPreview = true;
            ListOfNotSolvedButtons = new List<GridButton>(ListOfGridButtonsSolver);
            /* SolverGrid[0, 0].Text = " ";
             SolverGrid[0, 1].Text = " ";
             SolverGrid[0, 2].Text = " ";
             SolverGrid[0, 3].Text = " ";
             SolverGrid[0, 4].Text = "4";
             SolverGrid[0, 5].Text = " ";
             SolverGrid[0, 6].Text = " ";
             SolverGrid[0, 7].Text = " ";
             SolverGrid[0, 8].Text = "9";
             SolverGrid[1, 0].Text = " ";
             SolverGrid[1, 1].Text = " ";
             SolverGrid[1, 2].Text = "2";
             SolverGrid[1, 3].Text = " ";
             SolverGrid[1, 4].Text = "1";
             SolverGrid[1, 5].Text = " ";
             SolverGrid[1, 6].Text = " ";
             SolverGrid[1, 7].Text = " ";
             SolverGrid[1, 8].Text = " ";
             SolverGrid[2, 0].Text = "5";
             SolverGrid[2, 1].Text = " ";
             SolverGrid[2, 2].Text = " ";
             SolverGrid[2, 3].Text = " ";
             SolverGrid[2, 4].Text = " ";
             SolverGrid[2, 5].Text = " ";
             SolverGrid[2, 6].Text = " ";
             SolverGrid[2, 7].Text = "7";
             SolverGrid[2, 8].Text = "3";
             SolverGrid[3, 0].Text = " ";
             SolverGrid[3, 1].Text = "9";
             SolverGrid[3, 2].Text = " ";
             SolverGrid[3, 3].Text = " ";
             SolverGrid[3, 4].Text = " ";
             SolverGrid[3, 5].Text = " ";
             SolverGrid[3, 6].Text = " ";
             SolverGrid[3, 7].Text = " ";
             SolverGrid[3, 8].Text = " ";
             SolverGrid[4, 0].Text = " ";
             SolverGrid[4, 1].Text = " ";
             SolverGrid[4, 2].Text = "4";
             SolverGrid[4, 3].Text = " ";
             SolverGrid[4, 4].Text = " ";
             SolverGrid[4, 5].Text = " ";
             SolverGrid[4, 6].Text = "1";
             SolverGrid[4, 7].Text = " ";
             SolverGrid[4, 8].Text = " ";
             SolverGrid[5, 0].Text = " ";

             SolverGrid[5, 1].Text = " ";
             SolverGrid[5, 2].Text = " ";
             SolverGrid[5, 3].Text = "5";
             SolverGrid[5, 4].Text = " ";
             SolverGrid[5, 5].Text = "7";
             SolverGrid[5, 6].Text = " ";
             SolverGrid[5, 7].Text = " ";
             SolverGrid[5, 8].Text = " ";
             SolverGrid[6, 0].Text = " ";
             SolverGrid[6, 1].Text = " ";
             SolverGrid[6, 2].Text = "1";
             SolverGrid[6, 3].Text = " ";
             SolverGrid[6, 4].Text = "2";
             SolverGrid[6, 5].Text = " ";
             SolverGrid[6, 6].Text = " ";
             SolverGrid[6, 7].Text = " ";
             SolverGrid[6, 8].Text = " ";
             SolverGrid[7, 0].Text = " ";
             SolverGrid[7, 1].Text = " ";
             SolverGrid[7, 2].Text = " ";
             SolverGrid[7, 3].Text = " ";
             SolverGrid[7, 4].Text = " ";
             SolverGrid[7, 5].Text = "3";
             SolverGrid[7, 6].Text = " ";
             SolverGrid[7, 7].Text = "8";
             SolverGrid[7, 8].Text = "5";
             SolverGrid[8, 0].Text = " ";
             SolverGrid[8, 1].Text = " ";
             SolverGrid[8, 2].Text = " ";
             SolverGrid[8, 3].Text = " ";
             SolverGrid[8, 4].Text = " ";
             SolverGrid[8, 5].Text = " ";
             SolverGrid[8, 6].Text = " ";
             SolverGrid[8, 7].Text = " ";
             SolverGrid[8, 8].Text = " "; 
              Sudoku.FilledInFields++;
             .Solved = true;
             ListOfNotSolvedButtons.Remove();
             */
        }
        /// <summary>
        /// Vyvolá se postisknutí jednoho z číslených tlačítek ve formuláři Řešitel sudoku. Doplní do označeného pole požadované číslo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void num_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if(Sudoku.WhereIWantToFillInNumber.Text == " ")
            {
                Sudoku.FilledInFields++;
            }
            Sudoku.WhereIWantToFillInNumber.Text = button.Text;
            Sudoku.WhereIWantToFillInNumber.Solved = true;
            ListOfNotSolvedButtons.Remove(Sudoku.WhereIWantToFillInNumber);

        }

        private void Solver1_Load(object sender, EventArgs e)
        {
            HighlightSameNumbersCheckBox.Checked = true;
            HighlightSameNumbersCheckBox.Hide();
            MainMenu.DisposeSudokuSolver = false;

        }
        /*
        /// <summary>
        /// Doplní do mřížky pouze ty buňky, které měly pouze jedno možné číslo na vyplnění zjištěné pomocí funkce Sudoku.CanBeFilledIn(int, GridButton, ListOfGridButtonsSolver)
        /// </summary>
        /// <param name="ListOfNotSolvedButtons">Seznam prázdných, uživatelem nevyplněných polí.</param>
        
         private void RefreshNumberOfSolutions( List<GridButton> ListOfNotSolvedButtons)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    RowNumberCount[i, j] = 0;
                    ColumnNumberCount[i, j] = 0;
                    CellNumberCount[i, j] = 0;
                }
            }
            Updated = false;
            ListOfAlreadySolvedButtons.Clear();
            foreach (GridButton NotSolvedGridButton in ListOfNotSolvedButtons)
            {
                Sudoku.WhereIWantToFillInNumber = NotSolvedGridButton;
                NotSolvedGridButton.NumberOfSolutions = 0;
                for (int i = 1; i < 10; i++)
                {
                    NotSolvedGridButton.SolverButtonStatus[i - 1] = false;
                    if (Sudoku.CanBeFilledIn(i, NotSolvedGridButton, ListOfGridButtonsSolver))
                    {

                        NotSolvedGridButton.SolverButtonStatus[i - 1] = true;
                        NotSolvedGridButton.NumberOfSolutions++;
                        RowNumberCount[NotSolvedGridButton.Row - 1, i - 1]++;
                        ColumnNumberCount[NotSolvedGridButton.Column - 1, i - 1]++;
                        CellNumberCount[NotSolvedGridButton.Cell - 1, i - 1]++;
                    }
                    else NotSolvedGridButton.SolverButtonStatus[i - 1] = false;
                }
                if (NotSolvedGridButton.NumberOfSolutions == 1)
                {
                    for (int i = 1; i < 10; i++)
                    {
                        if (NotSolvedGridButton.SolverButtonStatus[i - 1] == true)
                        {
                            NotSolvedGridButton.Text = i.ToString();
                            ListOfAlreadySolvedButtons.Add(NotSolvedGridButton);
                            Sudoku.FilledInFields++;
                            Updated = true;
                            NotSolvedGridButton.Solved = true;
                            RowNumberCount[NotSolvedGridButton.Row - 1, i - 1] = 0;
                            ColumnNumberCount[NotSolvedGridButton.Column - 1, i - 1] = 0;
                            CellNumberCount[NotSolvedGridButton.Cell - 1, i - 1] = 0;
                        }
                    }
                    
                }
                else if(NotSolvedGridButton.NumberOfSolutions == 0)
                {
                    MessageBox.Show(String.Format("Sudoku nemá řešení, 0 možností do buňky!"));
                    Stop = true;
                }
               
            }

        }
        */
        /// <summary>
        /// Z nevyřešených políček vybere jedno, které vrátí k použití v metodě SolveButton_Click.
        /// </summary>
        /// <returns></returns>
        private GridButton FindEmptyCell()
        {
            foreach(GridButton GridButton in ListOfNotSolvedButtons)
            {
                if (GridButton.Text == " ") return GridButton;
            }
            return null;
        }
        /// <summary>
        /// Metoda řešící sudoku pomocí backtrackingu nevyplněných polí.
        /// </summary>
        public bool BruteForceSolving()
        {
            GridButton NotSolvedButton = FindEmptyCell(); 
            if(NotSolvedButton == null)
            {
                return true;
            }
            if(NotSolvedButton != null)
                Sudoku.WhereIWantToFillInNumber = NotSolvedButton;
            for (int Number = 1; Number < 10; Number++)
            {
                if(Sudoku.CanBeFilledIn(Number, NotSolvedButton, ListOfGridButtonsSolver))
                {
                    NotSolvedButton.Text = Number.ToString();
                    
                    if (BruteForceSolving())
                    {
                        return true;
                    }
                    
                }
                Sudoku.WhereIWantToFillInNumber = NotSolvedButton;
            }
            NotSolvedButton.Text = " ";
            
            
            return false;
        }
        /// <summary>
        /// Doplní uživateli do nevyplněných políček jedno řešení sudoku, pokud uživatel správně zadal vstup.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace od události.</param>
        private void SolveButton_Click(object sender, EventArgs e)
        {
            
            if(!BruteForceSolving())
            {
                MessageBox.Show(String.Format("Sudoku nemá řešení, zjištěno po bruteforce!"));
                foreach(GridButton FalselySolvedButton in ListOfNotSolvedButtons)
                {
                    FalselySolvedButton.Text = " ";      
                }
            }
            /*
            Updated3 = true;
            Stop = false;
            while (Sudoku.FilledInFields < 81)
            {
                

                RefreshNumberOfSolutions(ListOfNotSolvedButtons);
                

                if (!Updated)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (RowNumberCount[i, j] == 1)
                            {
                                foreach(GridButton NotSolvedGridButton in ListOfNotSolvedButtons)
                                {
                                    Sudoku.WhereIWantToFillInNumber = NotSolvedGridButton;
                                    if ((NotSolvedGridButton.Row == (i + 1)) && (Sudoku.CanBeFilledIn(j + 1, NotSolvedGridButton, ListOfGridButtonsSolver)))
                                    {
                                        NotSolvedGridButton.Text = (j + 1).ToString();
                                        ListOfAlreadySolvedButtons.Add(NotSolvedGridButton);
                                        Sudoku.FilledInFields++;                          
                                        RowNumberCount[i, j] = 0;
                                        ColumnNumberCount[i, j] = 0;
                                        CellNumberCount[i, j] = 0;
                                        Updated2 = true;
                                        NotSolvedGridButton.Solved = true;
                                    }
                                }
                                    
                            }
                            else if (ColumnNumberCount[i, j] == 1)
                            {
                                foreach (GridButton NotSolvedGridButton in ListOfNotSolvedButtons)
                                {
                                    Sudoku.WhereIWantToFillInNumber = NotSolvedGridButton;
                                    if ((NotSolvedGridButton.Column == i + 1) && (Sudoku.CanBeFilledIn(j + 1, NotSolvedGridButton, ListOfGridButtonsSolver)))
                                    {
                                        NotSolvedGridButton.Text = (j + 1).ToString();
                                        ListOfAlreadySolvedButtons.Add(NotSolvedGridButton);
                                        Sudoku.FilledInFields++;
                                        RowNumberCount[i, j] = 0;
                                        ColumnNumberCount[i, j] = 0;
                                        CellNumberCount[i, j] = 0;
                                        Updated2 = true;
                                        NotSolvedGridButton.Solved = true;
                                    }
                                }
                            }
                            else if (CellNumberCount[i, j] == 1)
                            {
                                foreach (GridButton NotSolvedGridButton in ListOfNotSolvedButtons)
                                {
                                    Sudoku.WhereIWantToFillInNumber = NotSolvedGridButton;
                                    if ((NotSolvedGridButton.Row == i + 1) && (Sudoku.CanBeFilledIn(j + 1, NotSolvedGridButton, ListOfGridButtonsSolver)))
                                    {
                                        NotSolvedGridButton.Text = (j + 1).ToString();
                                        ListOfAlreadySolvedButtons.Add(NotSolvedGridButton);
                                        Sudoku.FilledInFields++;
                                        RowNumberCount[i, j] = 0;
                                        ColumnNumberCount[i, j] = 0;
                                        CellNumberCount[i, j] = 0;
                                        Updated2 = true;
                                        NotSolvedGridButton.Solved = true;
                                    }
                                }
                            }

                        }
                    }
                    if (!Updated2)
                    {
                        Updated3 = false;
                    }
                }
                
                foreach (GridButton GridButton in ListOfAlreadySolvedButtons)
                {
                    ListOfNotSolvedButtons.Remove(GridButton);
                }
                if (Stop) break;
                if (!Updated3)
                {
                    if(!BruteForceSolving())
                    {
                        MessageBox.Show(String.Format("Sudoku nemá řešení, zjištěno po bruteforce!"));
                    }
                    Sudoku.FilledInFields = 81;
                }
            }
          */
        }
        /// <summary>
        /// Při zavření Řešitele sudoku se připraví Dispose tohoto formuláře.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace od události.</param>
        private void Solver1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainMenu.DisposeSudokuSolver = true;
        }
        /// <summary>
        /// Zavolá se po stisknutí tlačítka "Vymaž" v okně pro doplnění sudoku. Umožňuje uživateli vymazat číslo z buňky, pokud se spletl při zadávání vstupu.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace od události.</param>
        private void SolverDeleteButton_Click(object sender, EventArgs e)
        {
            Sudoku.WhereIWantToFillInNumber.Text = " ";
            Sudoku.FilledInFields--;
            Sudoku.WhereIWantToFillInNumber.Solved = false;
            ListOfNotSolvedButtons.Add(Sudoku.WhereIWantToFillInNumber);
        }
    }
}
