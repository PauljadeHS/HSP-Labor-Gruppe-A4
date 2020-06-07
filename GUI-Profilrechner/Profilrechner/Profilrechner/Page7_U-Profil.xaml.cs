using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaktionslogik für Page7_U_Profil.xaml
    /// </summary>
    public partial class Page7_U_Profil : Page
    {
        Profil UP;
        public Page7_U_Profil()
        {
            InitializeComponent();
            UP = new UProfil();
        }

        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            UP.AuslesenExcel();
            UP.Breite = UP.ConvToNumber(tb_Breite.Text);
            UP.Länge = UP.ConvToNumber(tb_Laenge.Text);
            UP.Höhe = UP.ConvToNumber(tb_Hoehe.Text);
            UP.Stegdicke = UP.ConvToNumber(tb_Stegdicke.Text);
            UP.Flanschdicke = UP.ConvToNumber(tb_Flanschdicke.Text);
            
            if ( UP.Höhe <= 2 * UP.Flanschdicke || UP.Breite <= UP.Stegdicke)
            {
                MessageBox.Show("Ungültige Eingabe: Profildiche zu groß! Eingabe wiederspricht dem aktuellen Stand des Technisch möglichen.                                                                                                    " +
                                "                                                    " +
                                "Tipp: If Error, change User!  ;-)", "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
                UP.Breite = 0;
                UP.Länge = 0;
                UP.Höhe = 0;
                UP.Stegdicke = 0;
                UP.Flanschdicke = 0;
            }
            double Flächeninhalt = UP.Flächenberechnung();
            UP.Flächeninhalt = Flächeninhalt;
            double Volumeninhalt = UP.Volumen();
            UP.Volumeninhalt = Volumeninhalt;
            double Masse = UP.Massenberechnung();
            double Materialkosten = UP.Materialkosten();
            double SchwerpunktXS = UP.FlächenschwerpunktXS();
            double SchwerpunktYS = UP.FlächenschwerpunktYS();
            double IXX = UP.FlächenträgheitsmomentIXX();
            double IYY = UP.FlächenträgheitsmomentIYY();
            tb_Querschnittsflaeche.Text = Convert.ToString((String.Format("{0:0.00}", Flächeninhalt / 100)) + " cm^2"); //Flächeninhalt umrechnung im cm^2
            tb_Volumen.Text = Convert.ToString((String.Format("{0:0.00}", Volumeninhalt / 1000000)) + " l");       //Querschnittsfläche umgerechnet in dm^3
            tb_Masse.Text = Convert.ToString((String.Format("{0:0.000}", Masse / 1000) + " kg")); //Masse in kg
            tb_Materialkosten.Text = Convert.ToString((String.Format("{0:0.00}", Materialkosten) + " €"));
            tb_Schwerpunktkoordinaten.Text = Convert.ToString("Xs/Ys     = " + (String.Format("{0:0.00}", SchwerpunktXS)) + " mm / " + (String.Format("{0:0.00}", SchwerpunktYS)) + " mm");
            tb_FTMX.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IXX / 10000) + " cm^4"));
            tb_FTMY.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IYY / 10000) + " cm^4"));
            tb_Breite.Text = Convert.ToString(UP.Breite);
            tb_Hoehe.Text = Convert.ToString(UP.Höhe);
            tb_Laenge.Text = Convert.ToString(UP.Länge);
            tb_Stegdicke.Text = Convert.ToString(UP.Stegdicke);
            tb_Flanschdicke.Text = Convert.ToString(UP.Flanschdicke);
          
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            UP.Materialint = 1; // S235 (Stahl)
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            UP.Materialint = 2; // S355 (Stahl)
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            UP.Materialint = 3; // EN AW-6060 (Aluminium)

        }

        private void ComboBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            UP.Materialint = 4; // Aluminium 2
        }

        private void ComboBoxItem_Selected_4(object sender, RoutedEventArgs e)
        {
            UP.Materialint = 5; // Messing
        }
        class UProfil : SP_UProfil
        {
            public override double Flächenberechnung()
            {
                double lkFlächeninhalt;
                lkFlächeninhalt = Breite * Höhe - (Breite - Stegdicke) * (Höhe - 2 * Flanschdicke);
                return lkFlächeninhalt;
            }
            public override double FlächenträgheitsmomentIXX()
            {
                double lkIXX;
                lkIXX = (Breite * Math.Pow(Höhe, 3) / 12) - ((Breite - Stegdicke) * Math.Pow((Höhe - 2 * Flanschdicke), 3) / 12);

                return lkIXX;
            }
            public override double FlächenträgheitsmomentIYY()
            {
                double lkIYY;
                lkIYY = (Höhe * Math.Pow(Breite, 3) / 12) - ((Höhe - 2 * Flanschdicke) * Math.Pow(Breite - Stegdicke, 3) / 12);

                return lkIYY;
            }
        }

        private void but_Catia_Click(object sender, RoutedEventArgs e)
        {
            UP.Breite = UP.ConvToNumber(tb_Breite.Text);
            UP.Länge = UP.ConvToNumber(tb_Laenge.Text);
            UP.Höhe = UP.ConvToNumber(tb_Hoehe.Text);
            UP.Stegdicke = UP.ConvToNumber(tb_Stegdicke.Text);
            UP.Flanschdicke = UP.ConvToNumber(tb_Flanschdicke.Text);
            try
            {
                CatiaUProfil cc = new CatiaUProfil();

                // Finde Catia Prozess
                if (cc.CATIALaeuft())
                {
                    // Öffne ein neues Part
                    cc.ErzeugePart();

                    // Erstelle eine Skizze
                    cc.ErstelleLeereSkizze();

                    // Generiere ein Profil
                    cc.ErzeugeProfil(UP.Breite, UP.Höhe, UP.Stegdicke, UP.Flanschdicke);

                    // Extrudiere Balken
                    cc.ErzeugeBalken(UP.Länge);

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

    abstract class SP_UProfil : Profil
    {
        public override double FlächenschwerpunktXS()
        {
            double lkXS;
            lkXS = Höhe / 2;
            return lkXS;
        }

        public override double FlächenschwerpunktYS()
        {
            double lkYS;
            lkYS = (Höhe * Breite * Breite / 2 - (Höhe - 2 * Flanschdicke) * (Breite - Stegdicke) * (Breite - Stegdicke) / 2) / (Höhe * Breite - (Höhe - 2 * Flanschdicke) * (Breite - Stegdicke));
            return lkYS;
        }
    }

    class CatiaUProfil : CatiaConnection
    {
        public override void ErzeugeProfil(Double b, Double h, Double s, Double f)
        {
            // Skizze umbenennen
            hsp_catiaProfil.set_Name("U-Profil");

            // Rechteck in Skizze einzeichnen
            // Skizze oeffnen
            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();

            // Rechteck erzeugen

            // erst die Punkte
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(0, 0);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(0, h);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(f, h);
            Point2D catPoint2D4 = catFactory2D1.CreatePoint(f, s);
            Point2D catPoint2D5 = catFactory2D1.CreatePoint(b-f, s);
            Point2D catPoint2D6 = catFactory2D1.CreatePoint(b-f, h);
            Point2D catPoint2D7 = catFactory2D1.CreatePoint(b, h);
            Point2D catPoint2D8 = catFactory2D1.CreatePoint(b, 0);

            // dann die Linien
            Line2D catLine2D1 = catFactory2D1.CreateLine(0, 0, 0, h);
            catLine2D1.StartPoint = catPoint2D1;
            catLine2D1.EndPoint = catPoint2D2;

            Line2D catLine2D2 = catFactory2D1.CreateLine(0, h, f, h);
            catLine2D2.StartPoint = catPoint2D2;
            catLine2D2.EndPoint = catPoint2D3;

            Line2D catLine2D3 = catFactory2D1.CreateLine(f, h, f, s);
            catLine2D3.StartPoint = catPoint2D3;
            catLine2D3.EndPoint = catPoint2D4;

            Line2D catLine2D4 = catFactory2D1.CreateLine(f, s, b-f, s);
            catLine2D4.StartPoint = catPoint2D4;
            catLine2D4.EndPoint = catPoint2D5;

            Line2D catLine2D5 = catFactory2D1.CreateLine(b-f, s, b-f, h);
            catLine2D5.StartPoint = catPoint2D5;
            catLine2D5.EndPoint = catPoint2D6;

            Line2D catLine2D6 = catFactory2D1.CreateLine(b-f, h, b, h);
            catLine2D6.StartPoint = catPoint2D6;
            catLine2D6.EndPoint = catPoint2D7;

            Line2D catLine2D7 = catFactory2D1.CreateLine(b, h, b, 0);
            catLine2D7.StartPoint = catPoint2D7;
            catLine2D7.EndPoint = catPoint2D8;

            Line2D catLine2D8 = catFactory2D1.CreateLine(b, 0, 0, 0);
            catLine2D8.StartPoint = catPoint2D8;
            catLine2D8.EndPoint = catPoint2D1;


            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }
    }
}
