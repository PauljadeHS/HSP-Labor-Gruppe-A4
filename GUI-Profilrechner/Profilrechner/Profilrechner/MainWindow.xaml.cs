using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Office.Interop.Excel;

namespace Profilrechner
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnClickP1Rechteckprofil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page1_Rechteckprofil ();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }
        private void BtnClickP2Rechteckhohlprofil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page2_Rechteckhohlprofil();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }

        private void BtnClickP3Kreisprofil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page3_Kreisprofil();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }

        private void BtnClickP4Kreishohlprofil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page4_Kreishohlprofil();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }

        private void BtnClickP5TProfil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page5_T_Profil();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }

        private void BtnClickP6DoppelTProfil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page6_Doppel_T_Profil();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }

        private void BtnClickP7UProfil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page7_U_Profil();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }

        private void BtnClickP8LProfil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page8_L_Profil();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }
        private void btn_Beenden_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);

        }
    }

    abstract class Profil:Object
    {
        // Eingabevariabeln 
        public double Breite, Höhe, Durchmesser, Länge, Flanschbreite, Flanschdicke, Stegdicke;

        //Berechnungsergebnisse
        public double Flächeninhalt, Volumeninhalt, Masse, Profildicke, SchwerpunktXS, SchwerpunktYS;
        
        //Variabeln zur Massenberechnung /Excel  
        public int Materialint = 1;
        public double S235, S355, AW6060, AW6082, MS63, Materialk;
 
        public double ConvToNumber(string In)
        {
            string Out;
            // ungültige Zeichen entfernen
            Out = Regex.Replace(In, "[^0123456789.,]", "",
                                RegexOptions.None, TimeSpan.FromSeconds(1.5));
            if(Out.Length != In.Length)
            {
                MessageBox.Show("Achtung! Es wurden ungültige Zeichen entfernt.", "Warnung",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            }
            return Convert.ToDouble(Out);
        }
        public abstract double Flächenberechnung();
        public abstract double FlächenschwerpunktXS();
        public abstract double FlächenschwerpunktYS();
        public abstract double FlächenträgheitsmomentIXX();
        public abstract double FlächenträgheitsmomentIYY();

         public double Volumen()
         {
            double lkVolumen;
            lkVolumen = Flächeninhalt * Länge;
            return lkVolumen;
         }

        public void AuslesenExcel()
        {
            string path = "Preisliste.xlsx";
            //FileInfo fi = new FileInfo(Assembly.GetEntryAssembly().Location);
            //path = fi.DirectoryName + "\\" + path;
            path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + path;
            

            //c: \\Users\PPF20\\OneDrive\\DateinAktuell\\Uni\\HSP\\HSP - Labor - Gruppe - A4\\GUI - Profilrechner\\Profilrechner\\Profilrechner\\Preisliste.xlsx"

            Microsoft.Office.Interop.Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();

            Workbook wb;
            Worksheet excelSheet;
            try
            {
                wb = ex.Workbooks.Open(path);
                excelSheet = wb.ActiveSheet;
            }
            catch
            {
                // Fehlermeldung Einlesung der Exel Tabelle fehlgeschlagen 
                MessageBox.Show("Achtung! Preisliste konnte nicht eingelesen werden", "Fehler",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
                return;
            }

            try
            {
                S235 = Convert.ToDouble(excelSheet.Cells[ 2, 3].Value.ToString());
                S355 = Convert.ToDouble(excelSheet.Cells[ 3, 3].Value.ToString());
                AW6060 = Convert.ToDouble(excelSheet.Cells[ 4, 3].Value.ToString());
                AW6082 = Convert.ToDouble(excelSheet.Cells[ 5, 3].Value.ToString());
                MS63 = Convert.ToDouble(excelSheet.Cells[ 6, 3].Value.ToString());
            }
            catch (System.FormatException)
            {

            }
            wb.Close(false);
        }
        

        public double Massenberechnung()
        {
            double Dichte = 0;

            if (Materialint == 1) //S235
            {
                Dichte = 0.00785;
                Materialk = S235;
            }
            else if (Materialint == 2) //S355
            {
                Dichte = 0.00785;
                Materialk = S355;
            }
            else if (Materialint == 3) //AW6060
            {
                Dichte = 0.0027;
                Materialk = AW6060; 
            }
            else if (Materialint == 4)  //AW6082
            {
                Dichte = 0.0027;
                Materialk = AW6082;
            }
            else if (Materialint == 5) //MS63
            {
                Dichte = 0.00873;
                Materialk = MS63;
            }
            double masse = Volumeninhalt * Dichte;
            Masse = masse;
            return masse;

        }
        public double Materialkosten()
        {
            double Kosten;
            Kosten = Masse * Materialk;
            
            return Kosten;
                
        }

    }
}
