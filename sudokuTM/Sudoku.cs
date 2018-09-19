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
using System.Windows.Input;
using System.Diagnostics;

namespace sudokuTM
{   /// <summary>
    /// Třída Form3 obstarává funkčnost okna se samotnou hrou Sudoku. Zahrnuje mřížku se hrou, časomíru, tlačítka na pozastavení hry, uložení hry a vyhodnocení hry. Dynamicky také načítá tlačítka pro doplnění Sudoku.
    /// </summary>
    public partial class Sudoku : Form
    {
        /// <summary>
        /// Označuje Button z mřížky Sudoku, který má právě uživatel označený.
        /// </summary>
        public static GridButton WhereIWantToFillInNumber;
        /// <summary>
        /// Button PauseButton slouží po kliknutí k pozastavení hry.
        /// </summary>
        public Button PauseButton;
        /// <summary>
        /// Seznam tlačítek ListOfNumbers v sobě zahrnuje tlačítka, která se generují dynamicky podle zvoleného WordTypeForPole, kam chce uživatel číslo doplnit.
        /// </summary>
        public static List<Button> ListOfNumbers = new List<Button>();
        /// <summary>
        /// Pomocný seznam na předání tlačítka do dalších metod.
        /// </summary>
        public List<Button> ListOfPause = new List<Button>();
        /// <summary>
        /// Číslo označující počet vyplněných políček.
        /// </summary>
        public static int FilledInFields = 0;
        /// <summary>
        /// String, který je generován z úrovně obtížnosti a náhodného čísla. Označuje soubor, ze kterého se bude načítat Sudoku.
        /// </summary>
        public string FileName;
        /// <summary>
        /// Označuje soubor, který se bude reálně načítat do StreamReaderu.
        /// </summary>
        public string SudokuLoading;
        /// <summary>
        /// Označuje stav fáze odchodu z aplikace. Pokud je false, program se zeptá, zda si přeje uživatel uložit Sudoku. Pokud je true, hned se odejde z aplikace.
        /// </summary>
        public static bool Leaving;

        /// <summary>
        /// Pole obsahující 81 tlačítek potřebných k vyplnění Sudoku.
        /// </summary>
        public GridButton[,] SudokuGrid = new GridButton[9, 9];
        /// <summary>
        /// Měří dobu, za kterou uživatel Sudoku vyluštil.
        /// </summary>
        Stopwatch StopWatch = new Stopwatch();
        /// <summary>
        /// Pokud se jedná o pokračování rozehrané hry, TimeLapse označuje již uplynulou dobu z minulé hry a je přičítán k celkovému času doby hraní.
        /// </summary>
        public TimeSpan TimeLapse;
        /// <summary>
        /// Pomocná hodnota pro vypsání uplynulého času do stringu.
        /// </summary>
        public TimeSpan TimeSpanHelpingVariable;

        /// <summary>
        /// Kontroluje správnost dosazení čísel do mřížky Sudoku.
        /// </summary>
        /// <returns>True - pokud je Sudoku vyplněno správně. False - pokud je Sudoku vyplněno nesprávně.</returns>
        public bool CorrectlyFilledIn()
        {
            StreamReader sr = new StreamReader(FileName);
            for (int i = 1; i < 10; i++)
            {
                string Row = sr.ReadLine();
                string[] Numbers = Row.Split(' ');
                for (int j = 1; j < 10; j++)
                {
                    if (Numbers[2 * j - 1] == "0")
                    {
                        if (this.Controls[(i-1).ToString() + (j-1).ToString()].Text != Numbers[2 * j - 2])
                            return false;
                    }
                }
            }
            return true;
        }
       
        /// <summary>
        /// Zavolá se po kliknutí na tlačítko "Vyhodnoť". Oznámí uživateli, v jakém stavu je jeho hrané Sudoku. Umí oznámit úspěšné/neúspěšné doplnění Sudoku nebo chybějící počet vyplněných polí.
        /// </summary>
        public void EvaluateSudoku()
        {
            if (FilledInFields == 81)
            {
                if (CorrectlyFilledIn())
                {
                    StopWatch.Stop();
                    if (TimeLapse != null)
                    { TimeSpanHelpingVariable = StopWatch.Elapsed + TimeLapse; }
                    else { TimeSpanHelpingVariable = StopWatch.Elapsed; }
                    string FormatedTime = String.Format("{0:00}:{1:00}:{2:00}", TimeSpanHelpingVariable.Hours, TimeSpanHelpingVariable.Minutes, TimeSpanHelpingVariable.Seconds);
                    MessageBox.Show(String.Format("Gratulujeme! Správně jste dořešili zadané Sudoku! ☻ \nSudoku jste dořešili v čase {0}.", FormatedTime));
                }
                else MessageBox.Show("Vaše řešení není správné!");

            }
            else if (FilledInFields == 0) MessageBox.Show("Nemáte načtené Sudoku!");

            else
            {
                string WordTypeForPole;
                int zbyvadoplnit = 81 - FilledInFields;
                if (zbyvadoplnit < 5)
                {
                    WordTypeForPole = "pole";
                }
                else
                {
                    WordTypeForPole = "polí";
                }
                MessageBox.Show(string.Format("Nemáte vyplněná všechna pole! Máte vyplněno {0} polí. Zbývá doplnit {1} {2}.", FilledInFields, zbyvadoplnit, WordTypeForPole));
            }
        }
        /// <summary>
        /// Nápověda pro vytváření tlačítek nabídky čísel, zda je stejné číslo v buňce.
        /// </summary>
        /// <param name="number">Číslo, pro které porovnávání provádíme.</param>
        /// <param name="r">Dolní hranice řádku</param>
        /// <param name="s">Dolní hranice sloupce</param>
        /// <returns></returns>
       /* public static bool IsNumberInTheSameCell(int number, int r, int s)
        {
            for (int Row = r; Row < r + 3; Row++)
            {
                for (int Column = s; Column < s + 3; Column++)
                {
                    if (this.Controls[Row.ToString() + Column.ToString()].Text == number.ToString())
                    {
                        return false;
                    }
                }
            }
            return true;
        }*/
        /// <summary>
        /// Kontroluje, zda je na vybrané tlačítko možno doplnit právě zkoumané číslo. Zkoumá se pouze základní návaznost na ostatní čísla tj. unikátnost v řádku, sloupci a buňce.
        /// </summary>
        /// <param name="TestedNumber">Zkoumané číslo (1-9)</param>
        /// <param name="ButtonName">Označení buňky, u které se zkoumá dostupnost</param>
        /// <returns>True - pokud se číslo nevyskytuje ani ve sloupci, řádku, buňce. False - pokud se číslo vyskytuje alespoň ve sloupci, řádku nebo buňce.</returns>
        public static bool CanBeFilledIn(int TestedNumber, GridButton ButtonName)
        { 
            foreach(var GridButton in GridButton.ListOfGridButtons)
            {
                if((WhereIWantToFillInNumber.Row == GridButton.Row)&&(GridButton.Text == TestedNumber.ToString()))
                {
                    return false;
                }
                if ((WhereIWantToFillInNumber.Column == GridButton.Column) && (GridButton.Text == TestedNumber.ToString()))
                {
                    return false;
                }
                if((WhereIWantToFillInNumber.Cell == GridButton.Cell) && (GridButton.Text == TestedNumber.ToString()))
                {
                    return false;
                }
            }
           /* for (int Column = 1; Column < 10; Column++)
            {

                if (this.Controls[chari.ToString() + Column.ToString()].Text == TestedNumber.ToString())
                {
                    return false;
                }
            }
            for (int Row = 0; Row < 9; Row++)
            {
                if (this.Controls[Row.ToString() + charj.ToString()].Text == TestedNumber.ToString())
                {
                    return false;
                }
            }

            int i = (int)Char.GetNumericValue(chari);
            int j = (int)Char.GetNumericValue(charj);

            */
            
           /* if (i > -1 && i < 3)
            {
                if (j > -1 && j < 3)
                {
                    if (!(IsNumberInTheSameCell(TestedNumber, 0, 0))) return false;
                }
                else if (j > 2 && j < 6)
                {
                    if (!(IsNumberInTheSameCell(TestedNumber, 0, 3))) return false;
                }
                else
                {
                    if (!(IsNumberInTheSameCell(TestedNumber, 0, 6))) return false;
                }

            }
            else if (i > 2 && i < 6)
            {
                if (j > -1 && j < 3)
                {
                    if (!(IsNumberInTheSameCell(TestedNumber, 3, 0))) return false;
                }
                else if (j > 2 && j < 6)
                {
                    if (!(IsNumberInTheSameCell(TestedNumber, 3, 3))) return false;
                }
                else
                {
                    if (!(IsNumberInTheSameCell(TestedNumber, 3, 6))) return false;
                }
            }
            else if (i > 5 && i < 9)
            {
                if (j > -1 && j < 3)
                {
                    if (!(IsNumberInTheSameCell(TestedNumber, 6, 0))) return false;
                }
                else if (j > 2 && j < 6)
                {
                    if (!(IsNumberInTheSameCell(TestedNumber, 6, 3))) return false;
                }
                else
                {
                    if (!(IsNumberInTheSameCell(TestedNumber, 6, 6))) return false;
                }
            } */
            return true;
        }
        /// <summary>
        /// Metoda, která doplní číslo do právě označené buňky, nebo číslo v ní vymaže.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private static void NumberMenuButton_Click(object sender, EventArgs e)

        {
            Button button = sender as Button;

            if (button.Text == "Vymaž")
            {
                if (WhereIWantToFillInNumber.Text != " ")
                {
                    WhereIWantToFillInNumber.Text = " ";
                    FilledInFields--;
                }
            }
            else
            {
                if (WhereIWantToFillInNumber.Text == " ") FilledInFields++;
                WhereIWantToFillInNumber.Text = button.Text;
            }
        }

        /// <summary>
        /// Metoda vytváří nové tlačítko pro doplnění v mřížce.
        /// </summary>
        /// <param name="i">Číslo, které bude na tlačítku vidět</param>
        /// <param name="Left">O kolik pixelů má být tlačítko odsazeno zleva</param>
        /// <param name="Top">O kolik pixelů má být tlačítko odsazeno shora</param>
        /// <param name="Colour">Jakou barvu má tlačítko mít</param>
        public static void CreateNewButton(string i, int Left, int Top, Color Colour)
        {
            Button ListOfNumbersButton = new Button();
            if (i == "Vymaž")
            {
                ListOfNumbersButton.Height = 40;

                ListOfNumbersButton.Width = 120;
            }
            else
            {
                ListOfNumbersButton.Height = 40;

                ListOfNumbersButton.Width = 40;
            }
            ListOfNumbersButton.BackColor = Colour;

            ListOfNumbersButton.ForeColor = Color.Black;

            ListOfNumbersButton.Location = new Point(Left, Top);

            ListOfNumbersButton.Text = i;

            ListOfNumbersButton.Name = i;

            ListOfNumbersButton.Font = new Font("Tahoma", 16);
            ListOfNumbersButton.Click += new EventHandler(NumberMenuButton_Click);
            ListOfNumbersButton.Show();
            ListOfNumbers.Add(ListOfNumbersButton);
        }

        /// <summary>
        /// Vytvoří nabídku čísel pro doplnění i s tlačítkem "Vymaž".
        /// </summary>
        /// <param name="ButtonName">Pole, které je označené v Sudoku mřížce.</param>
       /* public static void ShowNumberMenu(GridButton ButtonName)
        {
            MainMenu.AlreadyLoaded = false;
            foreach (Button button in ListOfNumbers)
            {
                Controls.Remove(button);
            }
            ListOfNumbers.Clear();
            Graphics g = CreateGraphics();
            SolidBrush ppap = new SolidBrush(Color.White);
            g.FillRectangle(ppap, new Rectangle(400, 140, 160, 160));
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

                    if (DoHighlightGoodNumbersCheckBox.Checked == true && CanBeFilledIn(i, ButtonName)) CreateNewButton(i.ToString(), 360 + Column * 50, 100 + Row * 50, Color.LightGreen);
                        else CreateNewButton(i.ToString(), 360 + Column * 50, 100 + Row * 50, Color.LightSlateGray);
                                           
                    CreateNewButton("Vymaž", 420, 300, Color.LightSlateGray);
                }
            }

            foreach (Button button in ListOfNumbers)
            {
                Controls.Add(button);
            }

            ppap.Dispose();
            g.Dispose();
        }
        */
        /// <summary>
        /// Vrací pravdivostní hodnotu, zda jsou dvě tlačítka ve stejné buňce.
        /// </summary>
        /// <param name="Button1i">Řádková poloha tlačítka 1</param>
        /// <param name="Button1j">Sloupcová poloha tlačítka 1</param>
        /// <param name="Button2i">Řádková poloha tlačítka 2</param>
        /// <param name="Button2j">Sloupcová poloha tlačítka 2</param>
        /// <param name="i">Dolní hranice zkoumaného řádku</param>
        /// <param name="j">Dolní hranice zkoumaného sloupce</param>
        /// <returns></returns>
        public bool IsInTheSameCell(int Button1i, int Button1j, int Button2i, int Button2j, int i, int j)
        {
            if ((Button1i > i) && (Button1i < i + 4) && (i < Button2i) && (Button2i < i + 4) && (Button1j > j) && (Button1j < j + 4) && (j < Button2j) && (Button2j < j + 4))
            {
                return true;
            }
            else return false;
        }
        /*
        /// <summary>
        /// Po kliknutí na již vyplněné WordTypeForPole zvýrazní zeleně všechna ostatní stejná čísla, která jsou umístěna správně, a červeně zvýrazní čísla, která jsou umístěna špatně vzhledem ke sloupci, řádku nebo buňce.
        /// </summary>
        /// <param name="ButtonName">Pole, vzhledem ke kterému se vybarvování provádí.</param>
        public void HighlightSameNumbers(Button ButtonName)
        {
            MainMenu.AlreadyLoaded = false;
            List<string> ListOfCellsOfTheSameNumber = new List<string>();
            WhereIWantToFillInNumber = ButtonName;

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if(this.Controls[i.ToString() + j.ToString()].ForeColor == Color.Black)
                    {
                        this.Controls[i.ToString() + j.ToString()].BackColor = Color.WhiteSmoke;
                    }
                    else this.Controls[i.ToString() + j.ToString()].BackColor = Color.White;
            if (this.HighlightSameNumbersCheckBox.Checked == true)
            {
                char Chari = ButtonName.Name[0];
                char Charj = ButtonName.Name[1];
                int Celli = (int)Char.GetNumericValue(Chari);
                int Cellj = (int)Char.GetNumericValue(Charj);
                bool IsInConflict = false;
                ButtonName.BackColor = Color.LightGreen;

                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                    {
                        if (this.Controls[i.ToString() + j.ToString()].Text != " ")
                        {

                            if (this.Controls[i.ToString() + j.ToString()].Text == ButtonName.Text)
                            {
                                ListOfCellsOfTheSameNumber.Add(this.Controls[i.ToString() + j.ToString()].Name);
                                this.Controls[i.ToString() + j.ToString()].BackColor = Color.LightGreen;
                            }
                            
                        }
                        foreach (var Button1 in ListOfCellsOfTheSameNumber)
                        {
                            IsInConflict = false;
                            char CharButton1i = Button1[0];
                            char CharButton1j = Button1[1];
                            int IntButton1i = (int)Char.GetNumericValue(CharButton1i);
                            int IntButton1j = (int)Char.GetNumericValue(CharButton1j);

                            foreach (var Button2 in ListOfCellsOfTheSameNumber)
                            {
                                char CharButton2i = Button2[0];
                                char CharButton2j = Button2[1];
                                int IntButton2i = (int)Char.GetNumericValue(CharButton2i);
                                int IntButton2j = (int)Char.GetNumericValue(CharButton2j);
                                if ((IntButton1i == IntButton2i) || (IntButton1j == IntButton2j))
                                {
                                    if ((IntButton1i == IntButton2i) && (IntButton1j == IntButton2j))
                                    {
                                        if (!IsInConflict) this.Controls[Button1].BackColor = Color.LightGreen;
                                        else this.Controls[Button1].BackColor = Color.Red;
                                    }
                                    else
                                    {
                                        this.Controls[Button2].BackColor = Color.Red;
                                        this.Controls[Button1].BackColor = Color.Red;
                                        IsInConflict = true;
                                    }
                                }
                                else if (IsInTheSameCell(IntButton1i, IntButton1j, IntButton2i, IntButton2j, -1, -1)) { this.Controls[Button1].BackColor = Color.Red; this.Controls[Button2].BackColor = Color.Red; IsInConflict = true; }
                                else if (IsInTheSameCell(IntButton1i, IntButton1j, IntButton2i, IntButton2j, -1, 2)) { this.Controls[Button1].BackColor = Color.Red; this.Controls[Button2].BackColor = Color.Red; IsInConflict = true; }
                                else if (IsInTheSameCell(IntButton1i, IntButton1j, IntButton2i, IntButton2j, -1, 5)) { this.Controls[Button1].BackColor = Color.Red; this.Controls[Button2].BackColor = Color.Red; IsInConflict = true; }
                                else if (IsInTheSameCell(IntButton1i, IntButton1j, IntButton2i, IntButton2j, 2, -1)) { this.Controls[Button1].BackColor = Color.Red; this.Controls[Button2].BackColor = Color.Red; IsInConflict = true; }
                                else if (IsInTheSameCell(IntButton1i, IntButton1j, IntButton2i, IntButton2j, 2, 2)) { this.Controls[Button1].BackColor = Color.Red; this.Controls[Button2].BackColor = Color.Red; IsInConflict = true; }
                                else if (IsInTheSameCell(IntButton1i, IntButton1j, IntButton2i, IntButton2j, 2, 5)) { this.Controls[Button1].BackColor = Color.Red; this.Controls[Button2].BackColor = Color.Red; IsInConflict = true; }
                                else if (IsInTheSameCell(IntButton1i, IntButton1j, IntButton2i, IntButton2j, 5, -1)) { this.Controls[Button1].BackColor = Color.Red; this.Controls[Button2].BackColor = Color.Red; IsInConflict = true; }
                                else if (IsInTheSameCell(IntButton1i, IntButton1j, IntButton2i, IntButton2j, 5, 2)) { this.Controls[Button1].BackColor = Color.Red; this.Controls[Button2].BackColor = Color.Red; IsInConflict = true; }
                                else if (IsInTheSameCell(IntButton1i, IntButton1j, IntButton2i, IntButton2j, 5, 5)) { this.Controls[Button1].BackColor = Color.Red; this.Controls[Button2].BackColor = Color.Red; IsInConflict = true; }


                            }
                        }
                    }
            }
        }
        */
        /// <summary>
        /// Určuje složku, ze které se bude Sudoku načítat.
        /// </summary>
        public static string Directory;

        /// <summary>
        /// Metoda, která z určeného souboru načte do mřížky počáteční čísla.
        /// </summary>
        public void LoadSudoku()
        {
            if (!MainMenu.AlreadyLoaded)
            {
                if (Directory == "pokracovani")
                {
                    SudokuLoading = "lehka/pokracovani.txt";
                }
                else
                {
                    Random r = new Random();
                    int RandomInt = r.Next(1, 5);
                    SudokuLoading = Directory + "/" + Directory + RandomInt + ".txt";
                    FileName = SudokuLoading;
                }
                FilledInFields = 0;

                StreamReader sr = new StreamReader(SudokuLoading);
                for (int i = 1; i < 10; i++)
                {
                    string Row = sr.ReadLine();
                    string[] Numbers = Row.Split(' ');
                    for (int j = 1; j < 10; j++)
                    {
                        if (Numbers[2 * j - 1] == "1")
                        {
                            SudokuGrid[i - 1, j - 1].Text = Numbers[2 * j - 2];
                            SudokuGrid[i - 1, j - 1].ForeColor = Color.Black;
                            SudokuGrid[i - 1, j - 1].BackColor = Color.WhiteSmoke;
                            FilledInFields++;
                        }
                        else if (Numbers[2 * j - 1] == "0")
                        {
                            SudokuGrid[i - 1, j - 1].Text = " ";
                            SudokuGrid[i - 1, j - 1].ForeColor = Color.MidnightBlue;
                            SudokuGrid[i - 1, j - 1].BackColor = Color.White;
                        }
                        else
                        {
                            SudokuGrid[i - 1, j - 1].Text = Numbers[2 * j - 2];
                            SudokuGrid[i - 1, j - 1].ForeColor = Color.MidnightBlue;
                            SudokuGrid[i - 1, j - 1].BackColor = Color.White;
                            FilledInFields++;
                        }
                        
                    }
                }
                if (Directory == "pokracovani")
                {
                    FileName = sr.ReadLine();
                    string[] LoadedTime = sr.ReadLine().Split(':');
                    int IntHours = Convert.ToInt32(LoadedTime[0]);
                    int IntMinutes = Convert.ToInt32(LoadedTime[1]);
                    int IntSeconds = Convert.ToInt32(LoadedTime[2]);

                    double Miliseconds = IntHours * 360000 + IntMinutes * 60000 + IntSeconds * 1000;
                    TimeLapse = TimeSpan.FromMilliseconds(Miliseconds);
                    sr.Close();
                    File.Delete(SudokuLoading);
                }
                else sr.Close();

                StopWatch.Start();
                InitTimer();
                MainMenu.AlreadyLoaded = true;
            }
        }
        /// <summary>
        /// Umožňuje změnu času na časomíře každou vteřinu.
        /// </summary>
        private Timer ScoreStopwatch;
        /// <summary>
        /// Inicializuje StopWatch, které běží od otevření Form3.
        /// </summary>
        public void InitTimer()
        {
            ScoreStopwatch = new Timer();
            ScoreStopwatch.Tick += new EventHandler(ScoreStopwatch_Tick);
            ScoreStopwatch.Interval = 1000; // in miliseconds
            ScoreStopwatch.Start();

        }

        /// <summary>
        /// Každou vteřinu inicializuje čas měřený na časomíře.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void ScoreStopwatch_Tick(object sender, EventArgs e)
        {
            if (TimeLapse != null)
            { TimeSpanHelpingVariable = StopWatch.Elapsed + TimeLapse; }
            else { TimeSpanHelpingVariable = StopWatch.Elapsed; }
            string FormatedTime = String.Format("{0:00}:{1:00}:{2:00}", TimeSpanHelpingVariable.Hours, TimeSpanHelpingVariable.Minutes, TimeSpanHelpingVariable.Seconds);
            this.TimerLabel.Text = FormatedTime;
            
        }

        /// <summary>
        /// Metoda nejprve změní adresář, ze kterého se hra bude načítat, poté zavolá samotné načtené Sudoku v metodě LoadSudoku().
        /// </summary>
        /// <param name="Level">Určuje název složky, která se bude ukládat do parametru Directory.</param>
        public void LoadDirectory(string Level)
        {
            if (Level == "lehka")
            {
                Directory = "lehka";

            }
            else if (Level == "stredni")
            {
                Directory = "stredni";
            }
            else if (Level == "tezka")
            {
                Directory = "tezka";
            }
            else Directory = "pokracovani";
            LoadSudoku();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Row"></param>
        /// <param name="Column"></param>
        /// <param name="Width"></param>
        /// <param name="Left"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        public GridButton CreateGridButton(string Name, int Row, int Column, int Width, int Left, int Top)
        {
            GridButton NewGridButton = new GridButton(Name, Row, Column, Width, Left, Top);
            return NewGridButton;
        } 

        /// <summary>
        /// Okno se samotnou hrou Sudoku. Detekuje čísla stisknutá na klávesnici.
        /// </summary>
        public Sudoku()
        {
            InitializeComponent();
            /*
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    int jd = 0, id = 0;
                    if (i >= 4) { id = 4; }
                    if (i >= 7) { id = 8; }
                    if (j >= 4) { jd = 4; }
                    if (j >= 7) { jd = 8; }
                    string NameOfButtonstring = (i - 1).ToString() + (j - 1).ToString();
                    SudokuGrid[i - 1, j - 1] = CreateNewButton(NameOfButtonstring, 35, j * 35 + jd, i * 35 + id, NameOfButtonstring, i, j);
                    SudokuGrid[i - 1, j - 1].Show();
                }
            }
            */
           
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
                    GridButton NewGridButton = CreateGridButton(NameOfButtonstring, i, j, 35, j * 35 + jd, i * 35 + id);
                    SudokuGrid[i - 1, j - 1] = NewGridButton;
                    Controls.Add(NewGridButton);
                    SudokuGrid[i - 1, j - 1].Show();

                    
                    GridButton.ListOfGridButtons.Add(NewGridButton);
                   
                }
            }
            

            KeyPreview = true;

        }
        /*
        /// <summary>
        /// Spustí se při kliknutí na jedno z 81 tlačítek v mřížce Sudoku. Vybarví stejná čísla nebo zobrazí nabídku čísel, která je možno doplnit.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void Cell_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;


            HighlightSameNumbers(button);
            ShowNumberMenu(button);


        }

    */

        /// <summary>
        /// Vyvolá se po stisknutí tlačítka "Vyhodnoť". Zkontroluje správnost vyplnění Sudoku.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void Evaluate_Click(object sender, EventArgs e)
        {
            EvaluateSudoku();
        }

        /// <summary>
        /// Vyvolá se po stisknutí klávesy. Klávesy kromě numerických kláves 1-9 jsou ignorovány. Vyplní příslušné číslo do onačeného WordTypeForPole v mřížce.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
       private void Sudoku_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (WhereIWantToFillInNumber != null)
                if (Char.IsDigit(e.KeyChar) && (e.KeyChar.ToString() != "0"))
                {
                    if (WhereIWantToFillInNumber.ForeColor == Color.MidnightBlue)
                    {

                        if (WhereIWantToFillInNumber.Text == " ") FilledInFields++;
                        WhereIWantToFillInNumber.Text = e.KeyChar.ToString();

                    }
                }
        }

        /// <summary>
        /// Vyvolá se při klinutí na tlačítko "Pauza". Zastaví StopWatch, skryje mřížku Sudoku a vytvoří nové tlačítko "Pokračovat", která působí opačně než tlačítko "Pauza".
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void pause_Click(object sender, EventArgs e)
        {

            StopWatch.Stop();
            ScoreStopwatch.Stop();

            Button ButtonContinue = new Button();
            ButtonContinue.Width = 75;
            ButtonContinue.Height = 23;
            ButtonContinue.BackColor = Color.Beige;
            ButtonContinue.Text = "Pokračovat";
            ButtonContinue.Left = 452;
            ButtonContinue.Top = 67;

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    this.Controls[i.ToString() + j.ToString()].Hide();
                }
            this.Controls.Add(ButtonContinue);
            ButtonContinue.Click += new EventHandler(Continue_Click);
            ListOfPause.Add(ButtonContinue);
        }

        /// <summary>
        /// Vyvolá se po kliknutí na tlačítko "Pokračovat", které bylo vygenerováno metodou pause_Click. Zobrazí Sudoku, znovu spustí čas a vymaže tlačítko "Pokračovat".
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void Continue_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    this.Controls[i.ToString() + j.ToString()].Show();
                }

            this.Controls.Remove(PauseButton);
            foreach (Button button in ListOfPause)
            {

                button.Dispose();
            }
            ListOfPause.Clear();
            StopWatch.Start();
            ScoreStopwatch.Start();
            
        }

        /// <summary>
        /// Po kliknutí na tlačítko s ikonou diskety se hra uloží do souboru pokracovani.txt do složky lehka. Zaznamenává se stav hry ve mřízce, aktuální čas na stopkách a zdrojový soubor hry.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        public void Save_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("./lehka/pokracovani.txt");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (this.Controls[i.ToString() + j.ToString()].ForeColor == Color.Black)
                        sw.Write("{0} 1 ", this.Controls[i.ToString() + j.ToString()].Text);
                    else if (this.Controls[i.ToString() + j.ToString()].ForeColor == Color.MidnightBlue && this.Controls[i.ToString() + j.ToString()].Text != " ")
                        sw.Write("{0} 2 ", this.Controls[i.ToString() + j.ToString()].Text);
                    else sw.Write("- 0 ");
                }
                sw.WriteLine();
            }
            sw.WriteLine(FileName);
            if (TimeLapse != null)
            { TimeSpanHelpingVariable = StopWatch.Elapsed + TimeLapse; }
            else { TimeSpanHelpingVariable = StopWatch.Elapsed; }
            string FormatedTime = String.Format("{0:00}:{1:00}:{2:00}", TimeSpanHelpingVariable.Hours, TimeSpanHelpingVariable.Minutes, TimeSpanHelpingVariable.Seconds);
            sw.WriteLine(FormatedTime);
            sw.Close();
            MessageBox.Show("Hra uložena.");
        }

        /// <summary>
        /// Při ukončování Form3 je poprvé uživatel dotázán, zda chce uložit hru. Po druhém ukončování se hra již vypne.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void Sudoku_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Leaving)
            {
                WantToSave Ask = new WantToSave("Odcházíte?", "Ano", "Ne", "Přejete si před odchodem uložit hru?");
                Ask.ShowDialog();
                if (Ask.YesSave == true)
                {
                    Save_Click(sender, e);

                }
                Leaving = true;
                
            }
            Application.Exit();
        }

        /// <summary>
        /// Vytvoří tlačítko pro mřížku Sudoku.
        /// </summary>
        /// <param name="Name">Název pro obsluhu tlačítka</param>
        /// <param name="Width">Požadovaná velikost tlačítka. Výška a šířka bude stejná.</param>
        /// <param name="Left">Odsazení od levého okraje.</param>
        /// <param name="Top">Odsazení od horního okraje.</param>
        /// <param name="Text">Vyplnění tlačítka.</param>
        /// <param name="i">Pořadí řádky.</param>
        /// <param name="j">Pořadí sloupce.</param>
        /// <returns>Vrací hotové tlačítko, které je schopné reagovat na příkazy uživatele.</returns>
        public  Button CreateNewButton(string Name, int Width, int Left, int Top, string Text, int i, int j)
        {
            Button Newbutton = new Button();
            Newbutton.Name = Name;
            Newbutton.Width = Width;
            Newbutton.Height = Width;
            Newbutton.Left = Left;
            Newbutton.Top = Top;
            Newbutton.Text = Text;
            Newbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            Newbutton.TabIndex = i * j - 1;
            Newbutton.UseVisualStyleBackColor = true;
          
            Controls.Add(Newbutton);
            return Newbutton;

        }

        /// <summary>
        /// Při načtení Sudoku se inicializuje hodnota Leaving na false, aby byl uživatel vyzván, zda chce před odchodem uložit hru.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void Sudoku_Load(object sender, EventArgs e)
        {

            Leaving = false;
            
            
        }
    }

}
