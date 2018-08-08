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
    /// <summary>
    /// Form4 je vyskakovací okno, které se objeví, pokud chce uživatel odejít. Ptá se uživatele, zda chce uložit hru.
    /// </summary>
    public partial class WantToSave:System.Windows.Forms.Form
    {
        /// <summary>
        /// Hodnota uložení. True = uložit. False = neukládat.
        /// </summary>
        public bool YesSave;
        /// <summary>
        /// Vyskakovací okno, které se objeví při první snaze o zavření Form3. Zeptá se uživatele, zda chce svoji hru před odchodem uložit.
        /// </summary>
        public WantToSave()
        {
            
        }
        /// <summary>
        /// Při načtení Form4 se nastaví hodnota uložení YesSave na false.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        public void WantToSave_Load(object sender, EventArgs e)
        {
            YesSave = false;
        }

        /// <summary>
        /// Konstruktor WantToSave. Vytvoří vzhled a výplň Form4.
        /// </summary>
        /// <param name="Header">Název okna</param>
        /// <param name="LeftButtonText">Text na levém tlačítku</param>
        /// <param name="RightButtonText">Text na pravém tlačítku</param>
        /// <param name="Text">Text v okně</param>
        public WantToSave(string Header, string LeftButtonText,string RightButtonText, string Text)
        {
            InitializeComponent();
            this.Text = Header;
            this.Lbutton.Text = LeftButtonText;
            this.Rbutton.Text = RightButtonText;
            this.lblMessage.Text = Text;
            Lbutton.Click += new EventHandler(Lbutton_Click);
            Rbutton.Click += new EventHandler(Rbutton_Click);
        }

        /// <summary>
        /// Provede se po stisknutí tlačítka vlevo. Nastaví ukládací hodnotu YesSave na true a skryje Form4.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        public void Lbutton_Click(object sender, EventArgs e)
        {
            
            YesSave = true;
            this.Hide();
            
        }

        /// <summary>
        /// Provede se po stisknutí tlačítka vpravo. YesSave nastaví na false a skryje Form4.
        /// </summary>
        /// <param name="sender">Obsahuje data o objektu, který událost vyvolal.</param>
        /// <param name="e">Obsahuje informace o události.</param>
        public void Rbutton_Click(object sender, EventArgs e)
        {
            YesSave = false;
            this.Hide();
        }
        
    }
}
