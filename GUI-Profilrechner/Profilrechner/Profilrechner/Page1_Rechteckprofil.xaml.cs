using System;
using System.Collections.Generic;
using System.Linq;
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
using INFITF;
using MECMOD;
using PARTITF;

namespace Profilrechner
{
    /// <summary>
    /// Interaktionslogik für Page1_Rechteckprofil.xaml
    /// </summary>
    public partial class Page1_Rechteckprofil : Page
    {
        Profil RP;
        public Page1_Rechteckprofil()
        {
            InitializeComponent();
            RP = new Rechteck();
        }

        //BerechnungsButton
        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            RP.AuslesenExcel();
            RP.Breite = RP.ConvToNumber(tb_Breite.Text);
            RP.Länge = RP.ConvToNumber(tb_Laenge.Text);
            RP.Höhe = RP.ConvToNumber(tb_Hoehe.Text);
            double Flächeninhalt = RP.Flächenberechnung();
            RP.Flächeninhalt = Flächeninhalt;
            double Volumeninhalt = RP.Volumen();
            RP.Volumeninhalt = Volumeninhalt;
            double Masse = RP.Massenberechnung();
            double Materialkosten = RP.Materialkosten();
            double SchwerpunktXS = RP.FlächenschwerpunktXS();
            double SchwerpunktYS = RP.FlächenschwerpunktYS();
            double IXX = RP.FlächenträgheitsmomentIXX();
            double IYY = RP.FlächenträgheitsmomentIYY();
            tb_Querschnittsflaeche.Text = Convert.ToString(Flächeninhalt / 100 + " cm^2");                  //Flächeninhalt umrechnung im cm^2
            tb_Volumen.Text = Convert.ToString((String.Format("{0:0.00}", Volumeninhalt / 1000000)) + " l");//Querschnittsfläche umgerechnet in dm^3
            tb_Masse.Text = Convert.ToString((String.Format("{0:0.000}", Masse /1000) + " kg"));            //Masse in kg
            tb_Materialkosten.Text = Convert.ToString((String.Format("{0:0.00}", Materialkosten) + " €"));
            tb_Schwerpunktkoordinaten.Text = Convert.ToString("Xs/Ys     = " + SchwerpunktXS + " mm / " + SchwerpunktYS + " mm");
            tb_FTMX.Text = Convert.ToString("="+(String.Format(" {0:0.0}", IXX / 10000) + " cm^4"));
            tb_FTMY.Text = Convert.ToString("="+(String.Format(" {0:0.0}", IYY / 10000) + " cm^4"));
            tb_Breite.Text = Convert.ToString(RP.Breite);
            tb_Hoehe.Text = Convert.ToString(RP.Höhe);
            tb_Laenge.Text = Convert.ToString(RP.Länge);

        }
        ///IsSelected="True"
        private void ComboBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            RP.Materialint = 1; //S235 (Stahl)
        }
        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            RP.Materialint = 2; //S355 (Stahl)
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            RP.Materialint = 3; // EN AW-6060 (Aluminium)
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            RP.Materialint = 4; // Aluminium 2
        }

        private void ComboBoxItem_Selected_4(object sender, RoutedEventArgs e)
        {
            RP.Materialint = 5; // Messing
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RP.Breite = RP.ConvToNumber(tb_Breite.Text);
            RP.Länge = RP.ConvToNumber(tb_Laenge.Text);
            RP.Höhe = RP.ConvToNumber(tb_Hoehe.Text);
            try
            {
                CatiaRechteck cc = new CatiaRechteck();

                // Finde Catia Prozess
                if (cc.CATIALaeuft())
                {
                    // Öffne ein neues Part
                    cc.ErzeugePart();
                    
                    // Erstelle eine Skizze
                    cc.ErstelleLeereSkizze();
                   
                    // Generiere ein Profil
                    cc.ErzeugeProfil(RP.Breite, RP.Höhe,0,0);
                    
                    // Extrudiere Balken
                    cc.ErzeugeBalken(RP.Länge);
                    
                }
                else
                {
                    MessageBox.Show("Catia läuft nicht! Bitte starten Sie Catia V5 bevor Sie eine Skizze generieren", "Catia",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception aufgetreten");
            }
            
        }
    }
    abstract class SymmetrischeFLPs : Profil
    {
        public override double FlächenschwerpunktXS()
        {
            double lkXS;
            lkXS = Breite / 2;
            return lkXS;
        }

        public override double FlächenschwerpunktYS()
        {
            double lkYS;
            lkYS = Höhe / 2;
            return lkYS;
        }
    }

    class Rechteck : SymmetrischeFLPs
    {
        public override double Flächenberechnung()
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = Breite * Höhe;
            return lkFlächeninhalt;
        }
        public override double FlächenträgheitsmomentIXX()
        {
            double lkIXX;
            lkIXX = (Breite * Math.Pow(Höhe, 3)) / 12;

            return lkIXX;
        }
        public override double FlächenträgheitsmomentIYY()
        {
            double lkIYY;
            lkIYY = (Math.Pow(Breite, 3) * Höhe) / 12;

            return lkIYY;
        }
    }
    class CatiaRechteck: CatiaConnection
    { 
        public override void ErzeugeProfil(Double b, Double h, Double p, Double q)
        {
            // Skizze umbenennen
            hsp_catiaProfil.set_Name("Rechteck");

            // Rechteck in Skizze einzeichnen
            // Skizze oeffnen
            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();

            // Rechteck erzeugen

            // erst die Punkte
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(0, 0);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(0, h);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(b, h);
            Point2D catPoint2D4 = catFactory2D1.CreatePoint(b, 0);

            // dann die Linien
            Line2D catLine2D1 = catFactory2D1.CreateLine(0, 0, 0, h);
            catLine2D1.StartPoint = catPoint2D1;
            catLine2D1.EndPoint = catPoint2D2;

            Line2D catLine2D2 = catFactory2D1.CreateLine(0, h, b, h);
            catLine2D2.StartPoint = catPoint2D2;
            catLine2D2.EndPoint = catPoint2D3;

            Line2D catLine2D3 = catFactory2D1.CreateLine(b, h, b, 0);
            catLine2D3.StartPoint = catPoint2D3;
            catLine2D3.EndPoint = catPoint2D4;

            Line2D catLine2D4 = catFactory2D1.CreateLine(b, 0, 0, 0);
            catLine2D4.StartPoint = catPoint2D4;
            catLine2D4.EndPoint = catPoint2D1;


            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }
    }
}
