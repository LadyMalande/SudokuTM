using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace sudokuTM
{

    /// <summary>
    /// Třída pro tlačítka tvořící sudoku mřížku. Obsahuje nezbytné parametry pro snadnou práci v metodách zkoumající kolize čísel, vyhodnocování, nápovědu.
    /// </summary>
    public class GridButton : Button
    {

        private void InitializeComponent()
        {
            // 
            // GridButton
            // 
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GridButton_KeyPress);
        }
        /// <summary>
        /// 
        /// </summary>
        public int NumberOfSolutions;
        /// <summary>
        /// 
        /// </summary>
        public bool[] SolverButtonStatus = new bool[9];
        /// <summary>
        /// 
        /// </summary>
        public bool Solved;
        /// <summary>
        /// Seznam obsahující všechna tlačítka této třídy. Používán pro projíždění čísel pomocí struktury foreach.
        /// </summary>
        public static List<GridButton> ListOfGridButtons = new List<GridButton>();

        /// <summary>
        /// Parametry udávající řádek tlačítka (Row), sloupec tlačítka (Column), příslušnou jednu buňku z devíti, ve které tlačítko leží (Cell).
        /// </summary>
        public int Row, Column, Cell;

        /// <summary>
        /// Řešení políčka (Solution).
        /// </summary>
        public string Solution;

        /// <summary>
        /// Kontroluje, zda jsou dvě tlačítka ve stejné buňce.
        /// </summary>
        /// <param name="Cell1">Buňka prvního tlačítka.</param>
        /// <param name="Cell2">Buňka druhého tlačítka.</param>
        /// <returns></returns>
        public bool IsInTheSameCell(int Cell1, int Cell2)
        {
            if (Cell1 == Cell2)
            {
                return true;
            }
            else return false;

        }
        /// <summary>
        /// Po kliknutí na již vyplněné WordTypeForPole zvýrazní zeleně všechna ostatní stejná čísla, která jsou umístěna správně, a červeně zvýrazní čísla, která jsou umístěna špatně vzhledem ke sloupci, řádku nebo buňce.
        /// </summary>
        /// <param name="ButtonName">Pole, vzhledem ke kterému se vybarvování provádí.</param>

        public void HighlightSameNumbers(GridButton ButtonName)
        {
            MainMenu.AlreadyLoaded = false;
            List<GridButton> ListOfCellsOfTheSameNumber = new List<GridButton>();
            Sudoku.WhereIWantToFillInNumber = ButtonName;

            foreach (var GridButton in ListOfGridButtons)
            {
               
                if (GridButton.ForeColor == Color.Black || GridButton.ForeColor == Color.DarkGray)
                {
                    GridButton.BackColor = Color.WhiteSmoke;
                }
                else GridButton.BackColor = Color.White;
            }
            if (Sudoku.HighlightSameNumbersCheckBox != null)
            {
                if (Sudoku.HighlightSameNumbersCheckBox.Checked == true)
                {

                    bool IsInConflict = false;
                    
                    ButtonName.BackColor = Color.LightGreen;
                    foreach (var GridButton in ListOfGridButtons)

                        if (GridButton.Text != " ")
                        {

                            if (GridButton.Text == ButtonName.Text)
                            {
                                ListOfCellsOfTheSameNumber.Add(GridButton);
                                GridButton.BackColor = Color.LightGreen;
                            }

                        }
                    foreach (var Button1 in ListOfCellsOfTheSameNumber)
                    {
                        IsInConflict = false;


                        foreach (var Button2 in ListOfCellsOfTheSameNumber)
                        {

                            if ((Button1.Row == Button2.Row) || (Button1.Column == Button2.Column))
                            {
                                if ((Button1.Row == Button2.Row) && (Button1.Column == Button2.Column))
                                {
                                    if (!IsInConflict) Button1.BackColor = Color.LightGreen;
                                    else Button1.BackColor = Color.Red;
                                }
                                else
                                {
                                    Button1.BackColor = Color.Red;
                                    Button2.BackColor = Color.Red;
                                    IsInConflict = true;
                                }
                            }

                            else if (IsInTheSameCell(Button1.Cell, Button2.Cell)) { Button1.BackColor = Color.Red; Button2.BackColor = Color.Red; IsInConflict = true; }

                        }
                    }

                }
            }
            else
            {

                bool IsInConflict = false;
                ButtonName.BackColor = Color.LightGreen;
                foreach (var GridButton in ListOfGridButtons)



                    if (GridButton.Text != " ")
                    {

                        if (GridButton.Text == ButtonName.Text)
                        {
                            ListOfCellsOfTheSameNumber.Add(GridButton);
                            GridButton.BackColor = Color.LightGreen;
                        }

                    }


                foreach (var Button1 in ListOfCellsOfTheSameNumber)
                {
                    IsInConflict = false;


                    foreach (var Button2 in ListOfCellsOfTheSameNumber)
                    {

                        if ((Button1.Row == Button2.Row) || (Button1.Column == Button2.Column))
                        {
                            if ((Button1.Row == Button2.Row) && (Button1.Column == Button2.Column))
                            {
                                if (!IsInConflict) Button1.BackColor = Color.LightGreen;
                                else Button1.BackColor = Color.Red;
                            }
                            else
                            {
                                Button1.BackColor = Color.Red;
                                Button2.BackColor = Color.Red;
                                IsInConflict = true;
                            }
                        }

                        else if (IsInTheSameCell(Button1.Cell, Button2.Cell)) { Button1.BackColor = Color.Red; Button2.BackColor = Color.Red; IsInConflict = true; }

                    }
                }
            }
        }
        /// <summary>
        /// Zobrazí novou nabídku pro doplnění čísel po kliknutí na prázdné pole. Pokud má uživatel zaškrtnuto políčko DoNotHighlightDumbNumbersCheckBox, metoda vyřadí čísla, která zjevně nemá podbarvovat.
        /// </summary>
        /// <param name="ButtonName">Tlačítko, na které bylo kliknuto.</param>
        public void ShowNumberMenu(GridButton ButtonName)
        {
            MainMenu.AlreadyLoaded = false;
            foreach (Button button in Sudoku.ListOfNumbers)
            {
                MainMenu.Sudoku.Controls.Remove(button);
            }
            Sudoku.ListOfNumbers.Clear();
            //Graphics g = CreateGraphics();
           // SolidBrush ppap = new SolidBrush(Color.White);
            //g.FillRectangle(ppap, new Rectangle(400, 140, 160, 160));
            for (int i = 1; i < 10; i++)
            {
                if (ButtonName.ForeColor == Color.MidnightBlue)
                {
                    int Row;
                    if (i < 4) { Row = 1; }
                    else if (i > 3 && i < 7) { Row = 2; }
                    else { Row = 3; }

                    int Column;
                    if (36 % i == 0 && i != 4 && i != 1 && i != 2) { Column = 3; }
                    else if (40 % i == 0 && i != 4 && i != 1) { Column = 2; }
                    else { Column = 1; }

                    if (Sudoku.DoHighlightGoodNumbersCheckBox.Checked == true && Sudoku.CanBeFilledIn(i, ButtonName)) Sudoku.CreateNewButton(i.ToString(), 360 + Column * 50, 100 + Row * 50, Color.LightGreen);
                    else Sudoku.CreateNewButton(i.ToString(), 360 + Column * 50, 100 + Row * 50, Color.LightSlateGray);

                    Sudoku.CreateNewButton("Vymaž", 420, 300, Color.LightSlateGray);
                    Sudoku.CreateNewButton("Napověz", 420, 350, Color.LimeGreen);
                }
                
            }

            foreach (Button button in Sudoku.ListOfNumbers)
            {
                MainMenu.Sudoku.Controls.Add(button);
            }

            //ppap.Dispose();
            //g.Dispose();
        }

        /// <summary>
        /// Spustí se při kliknutí na jedno z 81 tlačítek v mřížce Sudoku. Vybarví stejná čísla nebo zobrazí nabídku čísel, která je možno doplnit.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void Cell_Click(object sender, EventArgs e)
        {
            GridButton button = sender as GridButton;

            HighlightSameNumbers(button);
            ShowNumberMenu(button);
           
        }

        private void GridButton_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Sudoku.WhereIWantToFillInNumber != null)
                if (Char.IsDigit(e.KeyChar) && (e.KeyChar.ToString() != "0"))
                {
                    if (Sudoku.WhereIWantToFillInNumber.ForeColor == Color.MidnightBlue)
                    {

                        if (Sudoku.WhereIWantToFillInNumber.Text == " ") Sudoku.FilledInFields++;
                        Sudoku.WhereIWantToFillInNumber.Text = e.KeyChar.ToString();

                    }
                }
        }

        /// <summary>
        /// Konstruktor třídy GridButton.
        /// </summary>
        /// <param name="name">Název tlačítka</param>
        /// <param name="row">Řádek tlačítka</param>
        /// <param name="column">Sloupec tlačítka</param>
        /// <param name="width">Šířka tlačítka</param>
        /// <param name="left">Odsazení zleva</param>
        /// <param name="top">Odsazení zprava</param>
        /// <param name="solution">Řešení políčka</param>
        public GridButton(string name, int row, int column, int width, int left, int top, string solution)
        {
            this.Click += new System.EventHandler(this.Cell_Click);
            this.Name = name;
            this.Row = row;
            this.Column = column;
            this.Width = width;
            this.Height = width;
            this.Left = left;
            this.Top = top;
            this.TabIndex = row * column - 1;
            this.Solution = solution;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            if(row > 0 && row < 4)
            {
                if(column > 0 && column < 4)
                {
                    this.Cell = 1;
                }
                if (column > 3 && column < 7)
                {
                    this.Cell = 2;
                }
                if (column > 6 && column < 10)
                {
                    this.Cell = 3;
                }
            }
            else if(row > 3 && row < 7)
            {
                if (column > 0 && column < 4)
                {
                    this.Cell = 4;
                }
                if (column > 3 && column < 7)
                {
                    this.Cell = 5;
                }
                if (column > 6 && column < 10)
                {
                    this.Cell = 6;
                }
            }
            else if(row > 6 && row < 10)
            {
                if (column > 0 && column < 4)
                {
                    this.Cell = 7;
                }
                if (column > 3 && column < 7)
                {
                    this.Cell = 8;
                }
                if (column > 6 && column < 10)
                {
                    this.Cell = 9;
                }
            }
            this.UseVisualStyleBackColor = true;

            InitializeComponent();

        }
    }
   
}
