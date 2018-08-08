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
    /// Třída Form3 obstarává funkčnost okna se samotnou hrou sudoku. Zahrnuje mřížku se hrou, časomíru, tlačítka na pozastavení hry, uložení hry a vyhodnocení hry. Dynamicky také načítá tlačítka pro doplnění sudoku.
    /// </summary>
    public partial class Sudoku : Form
    {
        /// <summary>
        /// Označuje Button z mřížky sudoku, který má právě uživatel označený.
        /// </summary>
        public Button WhereIWantToFillInNumber;
        /// <summary>
        /// Button pauza slouží po kliknutí k pozastavení hry.
        /// </summary>
        public Button pauza;
        /// <summary>
        /// Seznam tlačítek ListOfNumbers v sobě zahrnuje tlačítka, která se generují dynamicky podle zvoleného pole, kam chce uživatel číslo doplnit.
        /// </summary>
        public List<Button> ListOfNumbers = new List<Button>();
        /// <summary>
        /// Pomocný seznam na předání tlačítka do dalších metod.
        /// </summary>
        public List<Button> LPauza = new List<Button>();
        /// <summary>
        /// Číslo označující počet vyplněných políček.
        /// </summary>
        public int FilledInFields = 0;
        /// <summary>
        /// String, který je generován z úrovně obtížnosti a náhodného čísla. Označuje soubor, ze kterého se bude načítat sudoku.
        /// </summary>
        public string FileName;
        /// <summary>
        /// Označuje soubor, který se bude reálně načítat do StreamReaderu.
        /// </summary>
        public string SudokuLoading;
        /// <summary>
        /// Označuje stav fáze odchodu z aplikace. Pokud je false, program se zeptá, zda si přeje uživatel uložit sudoku. Pokud je true, hned se odejde z aplikace.
        /// </summary>
        public static bool Leaving;

        /// <summary>
        /// Pole obsahující 81 tlačítek potřebných k vyplnění sudoku.
        /// </summary>
        public Button[,] SudokuGrid = new Button[9, 9];
        /// <summary>
        /// Měří dobu, za kterou uživatel sudoku vyluštil.
        /// </summary>
        Stopwatch StopWatch = new Stopwatch();
        /// <summary>
        /// Pokud se jedná o pokračování rozehrané hry, TimeLapse označuje již uplynulou dobu z minulé hry a je přičítán k celkovému času doby hraní.
        /// </summary>
        public TimeSpan TimeLapse;
        /// <summary>
        /// Pomocná hodnota pro vypsání uplynulého času do stringu.
        /// </summary>
        public TimeSpan ts;

        /// <summary>
        /// Kontroluje správnost dosazení čísel do mřížky sudoku.
        /// </summary>
        /// <returns>True - pokud je sudoku vyplněno správně. False - pokud je sudoku vyplněno nesprávně.</returns>
        public bool CorrectlyFilledIn()
        {
            StreamReader sr = new StreamReader(FileName);
            for (int i = 1; i < 10; i++)
            {
                string radka = sr.ReadLine();
                string[] cisla = radka.Split(' ');
                for (int j = 1; j < 10; j++)
                {
                    if (cisla[2 * j - 1] == "0")
                    {
                        if (this.Controls[(i-1).ToString() + (j-1).ToString()].Text != cisla[2 * j - 2])
                            return false;
                    }
                }
            }
            return true;
        }
       
        /// <summary>
        /// Zavolá se po kliknutí na tlačítko "Vyhodnoť". Oznámí uživateli, v jakém stavu je jeho hrané sudoku. Umí oznámit úspěšné/neúspěšné doplnění sudoku nebo chybějící počet vyplněných polí.
        /// </summary>
        public void EvaluateSudoku()
        {
            if (FilledInFields == 81)
            {
                if (CorrectlyFilledIn())
                {
                    StopWatch.Stop();
                    if (TimeLapse != null)
                    { ts = StopWatch.Elapsed + TimeLapse; }
                    else { ts = StopWatch.Elapsed; }
                    string formatovanycas = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
                    MessageBox.Show(String.Format("Gratulujeme! Správně jste dořešili zadané sudoku! ☻ \nSudoku jste dořešili v čase {0}.", formatovanycas));
                }
                else MessageBox.Show("Vaše řešení není správné!");

            }
            else if (FilledInFields == 0) MessageBox.Show("Nemáte načtené sudoku!");

            else
            {
                string pole;
                int zbyvadoplnit = 81 - FilledInFields;
                if (zbyvadoplnit < 5)
                {
                    pole = "pole";
                }
                else
                {
                    pole = "polí";
                }
                MessageBox.Show(string.Format("Nemáte vyplněná všechna pole! Máte vyplněno {0} polí. Zbývá doplnit {1} {2}.", FilledInFields, zbyvadoplnit, pole));
            }
        }
        /// <summary>
        /// Nápověda pro vytváření tlačítek nabídky čísel, zda je stejné číslo v buňce.
        /// </summary>
        /// <param name="number">Číslo, pro které porovnávání provádíme.</param>
        /// <param name="r">Dolní hranice řádku</param>
        /// <param name="s">Dolní hranice sloupce</param>
        /// <returns></returns>
        public bool IsNumberInTheSameCell(int number, int r, int s)
        {
            for (int radek = r; radek < r + 3; radek++)
            {
                for (int sloupec = s; sloupec < s + 3; sloupec++)
                {
                    if (this.Controls[radek.ToString() + sloupec.ToString()].Text == number.ToString())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
            /// <summary>
            /// Kontroluje, zda je na vybrané tlačítko možno doplnit právě zkoumané číslo. Zkoumá se pouze základní návaznost na ostatní čísla tj. unikátnost v řádku, sloupci a buňce.
            /// </summary>
            /// <param name="cislo">Zkoumané číslo (1-9)</param>
            /// <param name="ButtonName">Označení buňky, u které se zkoumá dostupnost</param>
            /// <returns>True - pokud se číslo nevyskytuje ani ve sloupci, řádku, buňce. False - pokud se číslo vyskytuje alespoň ve sloupci, řádku nebo buňce.</returns>
            public bool CanBeFilledIn(int cislo, Button ButtonName)
        {

            char chari = ButtonName.Name[0];
            char charj = ButtonName.Name[1];


            for (int sloupec = 0; sloupec < 9; sloupec++)
            {

                if (this.Controls[chari.ToString() + sloupec.ToString()].Text == cislo.ToString())
                {
                    return false;
                }
            }
            for (int radek = 0; radek < 9; radek++)
            {
                if (this.Controls[radek.ToString() + charj.ToString()].Text == cislo.ToString())
                {
                    return false;
                }
            }

            int i = (int)Char.GetNumericValue(chari);
            int j = (int)Char.GetNumericValue(charj);



            if (i > -1 && i < 3)
            {
                if (j > -1 && j < 3)
                {
                    if (!(IsNumberInTheSameCell(cislo, 0, 0))) return false;
                }
                else if (j > 2 && j < 6)
                {
                    if (!(IsNumberInTheSameCell(cislo, 0, 3))) return false;
                }
                else
                {
                    if (!(IsNumberInTheSameCell(cislo, 0, 6))) return false;
                }

            }
            else if (i > 2 && i < 6)
            {
                if (j > -1 && j < 3)
                {
                    if (!(IsNumberInTheSameCell(cislo, 3, 0))) return false;
                }
                else if (j > 2 && j < 6)
                {
                    if (!(IsNumberInTheSameCell(cislo, 3, 3))) return false;
                }
                else
                {
                    if (!(IsNumberInTheSameCell(cislo, 3, 6))) return false;
                }
            }
            else if (i > 5 && i < 9)
            {
                if (j > -1 && j < 3)
                {
                    if (!(IsNumberInTheSameCell(cislo, 6, 0))) return false;
                }
                else if (j > 2 && j < 6)
                {
                    if (!(IsNumberInTheSameCell(cislo, 6, 3))) return false;
                }
                else
                {
                    if (!(IsNumberInTheSameCell(cislo, 6, 6))) return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Metoda, která doplní číslo do právě označené buňky, nebo číslo v ní vymaže.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void NumberMenuButton_Click(object sender, EventArgs e)

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
        /// <param name="left">O kolik pixelů má být tlačítko odsazeno zleva</param>
        /// <param name="top">O kolik pixelů má být tlačítko odsazeno shora</param>
        /// <param name="barva">Jakou barvu má tlačítko mít</param>
        public void CreateNewButton(string i, int left, int top, Color barva)
        {
            Button Nabidka = new Button();
            if (i == "Vymaž")
            {
                Nabidka.Height = 40;

                Nabidka.Width = 120;
            }
            else
            {
                Nabidka.Height = 40;

                Nabidka.Width = 40;
            }
            Nabidka.BackColor = barva;

            Nabidka.ForeColor = Color.Black;

            Nabidka.Location = new Point(left, top);

            Nabidka.Text = i;

            Nabidka.Name = i;

            Nabidka.Font = new Font("Tahoma", 16);
            Nabidka.Click += new EventHandler(NumberMenuButton_Click);

            ListOfNumbers.Add(Nabidka);
        }

        /// <summary>
        /// Vytvoří nabídku čísel pro doplnění i s tlačítkem "Vymaž".
        /// </summary>
        /// <param name="ButtonName">Pole, které je označené v sudoku mřížce.</param>
        public void ShowNumberMenu(Button ButtonName)
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

                    switch (i)
                    {
                        case 1:
                            if (this.checkBox2.Checked == true && CanBeFilledIn(i, ButtonName)) CreateNewButton("1", 410, 150, Color.LightGreen);
                            else CreateNewButton("1", 410, 150, Color.LightSlateGray);
                            break;
                        case 2:
                            if (this.checkBox2.Checked == true && CanBeFilledIn(i, ButtonName)) CreateNewButton("2", 460, 150, Color.LightGreen);
                            else CreateNewButton("2", 460, 150, Color.LightSlateGray);
                            break;
                        case 3:
                            if (this.checkBox2.Checked == true && CanBeFilledIn(i, ButtonName)) CreateNewButton("3", 510, 150, Color.LightGreen);
                            else CreateNewButton("3", 510, 150, Color.LightSlateGray);
                            break;
                        case 4:
                            if (this.checkBox2.Checked == true && CanBeFilledIn(i, ButtonName)) CreateNewButton("4", 410, 200, Color.LightGreen);
                            else CreateNewButton("4", 410, 200, Color.LightSlateGray);
                            break;
                        case 5:
                            if (this.checkBox2.Checked == true && CanBeFilledIn(i, ButtonName)) CreateNewButton("5", 460, 200, Color.LightGreen);
                            else CreateNewButton("5", 460, 200, Color.LightSlateGray);
                            break;
                        case 6:
                            if (this.checkBox2.Checked == true && CanBeFilledIn(i, ButtonName)) CreateNewButton("6", 510, 200, Color.LightGreen);
                            else CreateNewButton("6", 510, 200, Color.LightSlateGray);
                            break;
                        case 7:
                            if (this.checkBox2.Checked == true && CanBeFilledIn(i, ButtonName)) CreateNewButton("7", 410, 250, Color.LightGreen);
                            else CreateNewButton("7", 410, 250, Color.LightSlateGray);
                            break;
                        case 8:
                            if (this.checkBox2.Checked == true && CanBeFilledIn(i, ButtonName)) CreateNewButton("8", 460, 250, Color.LightGreen);
                            else CreateNewButton("8", 460, 250, Color.LightSlateGray);
                            break;
                        case 9:
                            if (this.checkBox2.Checked == true && CanBeFilledIn(i, ButtonName)) CreateNewButton("9", 510, 250, Color.LightGreen);
                            else CreateNewButton("9", 510, 250, Color.LightSlateGray);
                            break;
                    }

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
        /// <summary>
        /// Vrací pravdivostní hodnotu, zda jsou dvě tlačítka ve stejné buňce.
        /// </summary>
        /// <param name="button1i">Řádková poloha tlačítka 1</param>
        /// <param name="button1j">Sloupcová poloha tlačítka 1</param>
        /// <param name="button2i">Řádková poloha tlačítka 2</param>
        /// <param name="button2j">Sloupcová poloha tlačítka 2</param>
        /// <param name="i">Dolní hranice zkoumaného řádku</param>
        /// <param name="j">Dolní hranice zkoumaného sloupce</param>
        /// <returns></returns>
        public bool IsInTheSameCell(int button1i, int button1j, int button2i, int button2j, int i, int j)
        {
            if ((button1i > i) && (button1i < i + 4) && (i < button2i) && (button2i < i + 4) && (button1j > j) && (button1j < j + 4) && (j < button2j) && (button2j < j + 4))
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Po kliknutí na již vyplněné pole zvýrazní zeleně všechna ostatní stejná čísla, která jsou umístěna správně, a červeně zvýrazní čísla, která jsou umístěna špatně vzhledem ke sloupci, řádku nebo buňce.
        /// </summary>
        /// <param name="ButtonName">Pole, vzhledem ke kterému se vybarvování provádí.</param>
        public void HighlightSameNumbers(Button ButtonName)
        {
            MainMenu.AlreadyLoaded = false;
            List<string> SeznamPoliSeStejnymCislem = new List<string>();
            WhereIWantToFillInNumber = ButtonName;

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if(this.Controls[i.ToString() + j.ToString()].ForeColor == Color.Black)
                    {
                        this.Controls[i.ToString() + j.ToString()].BackColor = Color.WhiteSmoke;
                    }
                    else this.Controls[i.ToString() + j.ToString()].BackColor = Color.White;
            if (this.checkBox1.Checked == true)
            {
                char chari = ButtonName.Name[0];
                char charj = ButtonName.Name[1];
                int celli = (int)Char.GetNumericValue(chari);
                int cellj = (int)Char.GetNumericValue(charj);
                bool JeVKonfliktu = false;
                ButtonName.BackColor = Color.LightGreen;

                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                    {
                        if (this.Controls[i.ToString() + j.ToString()].Text != " ")
                        {


                            if (this.Controls[i.ToString() + j.ToString()].Text == ButtonName.Text)
                            {
                                SeznamPoliSeStejnymCislem.Add(this.Controls[i.ToString() + j.ToString()].Name);
                                this.Controls[i.ToString() + j.ToString()].BackColor = Color.LightGreen;
                            }
                            
                        }
                        foreach (var button1 in SeznamPoliSeStejnymCislem)
                        {
                            JeVKonfliktu = false;
                            char charbutton1i = button1[0];
                            char charbutton1j = button1[1];
                            int intbutton1i = (int)Char.GetNumericValue(charbutton1i);
                            int intbutton1j = (int)Char.GetNumericValue(charbutton1j);

                            foreach (var button2 in SeznamPoliSeStejnymCislem)
                            {
                                char charbutton2i = button2[0];
                                char charbutton2j = button2[1];
                                int intbutton2i = (int)Char.GetNumericValue(charbutton2i);
                                int intbutton2j = (int)Char.GetNumericValue(charbutton2j);
                                if ((intbutton1i == intbutton2i) || (intbutton1j == intbutton2j))
                                {
                                    if ((intbutton1i == intbutton2i) && (intbutton1j == intbutton2j))
                                    {
                                        if (!JeVKonfliktu) this.Controls[button1].BackColor = Color.LightGreen;
                                        else this.Controls[button1].BackColor = Color.Red;
                                    }
                                    else
                                    {
                                        this.Controls[button2].BackColor = Color.Red;
                                        this.Controls[button1].BackColor = Color.Red;
                                        JeVKonfliktu = true;
                                    }
                                }
                                else if (IsInTheSameCell(intbutton1i, intbutton1j, intbutton2i, intbutton2j, -1, -1)) { this.Controls[button1].BackColor = Color.Red; this.Controls[button2].BackColor = Color.Red; JeVKonfliktu = true; }
                                else if (IsInTheSameCell(intbutton1i, intbutton1j, intbutton2i, intbutton2j, -1, 2)) { this.Controls[button1].BackColor = Color.Red; this.Controls[button2].BackColor = Color.Red; JeVKonfliktu = true; }
                                else if (IsInTheSameCell(intbutton1i, intbutton1j, intbutton2i, intbutton2j, -1, 5)) { this.Controls[button1].BackColor = Color.Red; this.Controls[button2].BackColor = Color.Red; JeVKonfliktu = true; }
                                else if (IsInTheSameCell(intbutton1i, intbutton1j, intbutton2i, intbutton2j, 2, -1)) { this.Controls[button1].BackColor = Color.Red; this.Controls[button2].BackColor = Color.Red; JeVKonfliktu = true; }
                                else if (IsInTheSameCell(intbutton1i, intbutton1j, intbutton2i, intbutton2j, 2, 2)) { this.Controls[button1].BackColor = Color.Red; this.Controls[button2].BackColor = Color.Red; JeVKonfliktu = true; }
                                else if (IsInTheSameCell(intbutton1i, intbutton1j, intbutton2i, intbutton2j, 2, 5)) { this.Controls[button1].BackColor = Color.Red; this.Controls[button2].BackColor = Color.Red; JeVKonfliktu = true; }
                                else if (IsInTheSameCell(intbutton1i, intbutton1j, intbutton2i, intbutton2j, 5, -1)) { this.Controls[button1].BackColor = Color.Red; this.Controls[button2].BackColor = Color.Red; JeVKonfliktu = true; }
                                else if (IsInTheSameCell(intbutton1i, intbutton1j, intbutton2i, intbutton2j, 5, 2)) { this.Controls[button1].BackColor = Color.Red; this.Controls[button2].BackColor = Color.Red; JeVKonfliktu = true; }
                                else if (IsInTheSameCell(intbutton1i, intbutton1j, intbutton2i, intbutton2j, 5, 5)) { this.Controls[button1].BackColor = Color.Red; this.Controls[button2].BackColor = Color.Red; JeVKonfliktu = true; }


                            }
                        }
                    }
            }
        }
        /// <summary>
        /// Určuje složku, ze které se bude sudoku načítat.
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
                    string radka = sr.ReadLine();
                    string[] cisla = radka.Split(' ');
                    for (int j = 1; j < 10; j++)
                    {
                        if (cisla[2 * j - 1] == "1")
                        {
                            SudokuGrid[i - 1, j - 1].Text = cisla[2 * j - 2];
                            SudokuGrid[i - 1, j - 1].ForeColor = Color.Black;
                            SudokuGrid[i - 1, j - 1].BackColor = Color.WhiteSmoke;
                            FilledInFields++;
                        }
                        else if (cisla[2 * j - 1] == "0")
                        {
                            SudokuGrid[i - 1,j -1 ].Text = " ";
                            SudokuGrid[i - 1, j - 1].ForeColor = Color.MidnightBlue;
                            SudokuGrid[i - 1, j - 1].BackColor = Color.White;
                        }
                        else
                        {
                            SudokuGrid[i - 1, j - 1].Text = cisla[2 * j - 2];
                            SudokuGrid[i - 1, j - 1].ForeColor = Color.MidnightBlue;
                            SudokuGrid[i - 1, j - 1].BackColor = Color.White;
                            FilledInFields++;
                        }
                        
                    }
                }
                if (Directory == "pokracovani")
                {
                    FileName = sr.ReadLine();
                    string[] nactenycas = sr.ReadLine().Split(':');
                    int inthodiny = Convert.ToInt32(nactenycas[0]);
                    int intminuty = Convert.ToInt32(nactenycas[1]);
                    int intsekundy = Convert.ToInt32(nactenycas[2]);

                    double milisekundy = inthodiny * 360000 + intminuty * 60000 + intsekundy * 1000;
                    TimeLapse = TimeSpan.FromMilliseconds(milisekundy);
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
            { ts = StopWatch.Elapsed + TimeLapse; }
            else { ts = StopWatch.Elapsed; }
            string formatovanycas = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            this.label1.Text = formatovanycas;
            
        }

        /// <summary>
        /// Metoda nejprve změní adresář, ze kterého se hra bude načítat, poté zavolá samotné načtené sudoku v metodě LoadSudoku().
        /// </summary>
        /// <param name="uroven">Určuje název složky, která se bude ukládat do parametru Directory.</param>
        public void LoadDirectory(string uroven)
        {
            if (uroven == "lehka")
            {
                Directory = "lehka";

            }
            else if (uroven == "stredni")
            {
                Directory = "stredni";
            }
            else if (uroven == "tezka")
            {
                Directory = "tezka";
            }
            else Directory = "pokracovani";
            LoadSudoku();
        }

        /// <summary>
        /// Okno se samotnou hrou sudoku. Detekuje čísla stisknutá na klávesnici.
        /// </summary>
        public Sudoku()
        {
            InitializeComponent();

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

            KeyPreview = true;

        }

        /// <summary>
        /// Spustí se při kliknutí na jedno z 81 tlačítek v mřížce sudoku. Vybarví stejná čísla nebo zobrazí nabídku čísel, která je možno doplnit.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void Cell_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            
            HighlightSameNumbers(button);
            ShowNumberMenu(button);


        }



        /// <summary>
        /// Vyvolá se po stisknutí tlačítka "Vyhodnoť". Zkontroluje správnost vyplnění sudoku.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void Vyhodnot_Click(object sender, EventArgs e)
        {
            EvaluateSudoku();
        }

        /// <summary>
        /// Vyvolá se po stisknutí klávesy. Klávesy kromě numerických kláves 1-9 jsou ignorovány. Vyplní příslušné číslo do onačeného pole v mřížce.
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
        /// Vyvolá se při klinutí na tlačítko "Pauza". Zastaví StopWatch, skryje mřížku sudoku a vytvoří nové tlačítko "Pokračovat", která působí opačně než tlačítko "Pauza".
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void pause_Click(object sender, EventArgs e)
        {

            StopWatch.Stop();
            ScoreStopwatch.Stop();

            Button pauza = new Button();
            pauza.Width = 75;
            pauza.Height = 23;
            pauza.BackColor = Color.Beige;
            pauza.Text = "Pokračovat";
            pauza.Left = 452;
            pauza.Top = 67;

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    this.Controls[i.ToString() + j.ToString()].Hide();
                }
            this.Controls.Add(pauza);
            pauza.Click += new EventHandler(pauza_Click);
            LPauza.Add(pauza);
        }

        /// <summary>
        /// Vyvolá se po kliknutí na tlačítko "Pokračovat", které bylo vygenerováno metodou pause_Click. Zobrazí sudoku, znovu spustí čas a vymaže tlačítko "Pokračovat".
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        private void pauza_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    this.Controls[i.ToString() + j.ToString()].Show();
                }

            this.Controls.Remove(pauza);
            foreach (Button button in LPauza)
            {

                button.Dispose();
            }
            LPauza.Clear();
            StopWatch.Start();
            ScoreStopwatch.Start();
            
        }

        /// <summary>
        /// Po kliknutí na tlačítko s ikonou diskety se hra uloží do souboru pokracovani.txt do složky lehka. Zaznamenává se stav hry ve mřízce, aktuální čas na stopkách a zdrojový soubor hry.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        public void save_Click(object sender, EventArgs e)
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
            { ts = StopWatch.Elapsed + TimeLapse; }
            else { ts = StopWatch.Elapsed; }
            string formatovanycas = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            sw.WriteLine(formatovanycas);
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
                WantToSave ask = new WantToSave("Odcházíte?", "Ano", "Ne", "Přejete si před odchodem uložit hru?");
                ask.ShowDialog();
                if (ask.YesSave == true)
                {
                    save_Click(sender, e);

                }
                Leaving = true;
                
            }
            Application.Exit();
        }

        /// <summary>
        /// Vytvoří tlačítko pro mřížku sudoku.
        /// </summary>
        /// <param name="name">Název pro obsluhu tlačítka</param>
        /// <param name="width">Požadovaná velikost tlačítka. Výška a šířka bude stejná.</param>
        /// <param name="left">Odsazení od levého okraje.</param>
        /// <param name="top">Odsazení od horního okraje.</param>
        /// <param name="text">Vyplnění tlačítka.</param>
        /// <param name="i">Pořadí řádky.</param>
        /// <param name="j">Pořadí sloupce.</param>
        /// <returns>Vrací hotové tlačítko, které je schopné reagovat na příkazy uživatele.</returns>
        public Button CreateNewButton(string name, int width, int left, int top, string text, int i, int j)
        {
            Button newbutton = new Button();
            newbutton.Name = name;
            newbutton.Width = width;
            newbutton.Height = width;
            newbutton.Left = left;
            newbutton.Top = top;
            newbutton.Text = text;
            newbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            newbutton.TabIndex = i * j - 1;
            newbutton.UseVisualStyleBackColor = true;
            newbutton.Click += new System.EventHandler(this.Cell_Click);
            Controls.Add(newbutton);
            return newbutton;

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
