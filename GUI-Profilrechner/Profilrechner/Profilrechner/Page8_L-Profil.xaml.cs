using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
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
    /// Interaktionslogik für Page8_L_Profil.xaml
    /// </summary>
    public partial class Page8_L_Profil : Page
    {
        Profil LP;
        public Page8_L_Profil()
        {
            InitializeComponent();
            LP = new LProfil();
        }

        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            LP.AuslesenExcel();
            LP.Breite = LP.ConvToNumber(tb_Breite.Text);
            LP.Länge = LP.ConvToNumber(tb_Laenge.Text);
            LP.Höhe = LP.ConvToNumber(tb_Hoehe.Text);
            LP.Profildicke = LP.ConvToNumber(tb_Profildicke.Text);
            if (LP.Höhe <= LP.Profildicke || LP.Breite <= LP.Profildicke)
            {
                MessageBox.Show("Ungültige Eingabe: Profildiche zu groß! Eingabe wiederspricht dem aktuellen Stand des Technisch möglichen.                                                                                                    " +
                                "                                                    " +
                                "Tipp: If Error, change User!  ;-)", "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }

            else
            {
                double Flächeninhalt = LP.Flächenberechnung();
                LP.Flächeninhalt = Flächeninhalt;
                double Volumeninhalt = LP.Volumen();
                LP.Volumeninhalt = Volumeninhalt;
                double Masse = LP.Massenberechnung();
                double Materialkosten = LP.Materialkosten();
                double SchwerpunktXS = LP.FlächenschwerpunktXS();
                LP.SchwerpunktXS = SchwerpunktXS;
                double SchwerpunktYS = LP.FlächenschwerpunktYS();
                LP.SchwerpunktYS = SchwerpunktYS;
                double IXX = LP.FlächenträgheitsmomentIXX();
                double IYY = LP.FlächenträgheitsmomentIYY();
                tb_Querschnittsflaeche.Text = Convert.ToString((String.Format("{0:0.00}", Flächeninhalt / 100)) + " cm^2"); //Flächeninhalt umrechnung im cm^2
                tb_Volumen.Text = Convert.ToString((String.Format("{0:0.00}", Volumeninhalt / 1000000)) + " l");       //Querschnittsfläche umgerechnet in dm^3
                tb_Masse.Text = Convert.ToString((String.Format("{0:0.000}", Masse / 1000) + " kg")); //Masse in kg
                tb_Materialkosten.Text = Convert.ToString((String.Format("{0:0.00}", Materialkosten) + " €"));
                tb_Schwerpunktkoordinaten.Text = Convert.ToString("Xs/Ys     = " + (String.Format("{0:0.00}", SchwerpunktXS)) + " mm / " + (String.Format("{0:0.00}", SchwerpunktYS)) + " mm");
                tb_FTMX.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IXX / 10000) + " cm^4"));
                tb_FTMY.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IYY / 10000) + " cm^4"));
            }
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            LP.Materialint = 1; // S235 (Stahl)
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            LP.Materialint = 2; // S355 (Stahl)
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            LP.Materialint = 3; // EN AW-6060 (Aluminium)
        }

        private void ComboBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            LP.Materialint = 4; // Aluminium 2
        }

        private void ComboBoxItem_Selected_4(object sender, RoutedEventArgs e)
        {
            LP.Materialint = 5; // Messing
        }
        class LProfil : Profil
        {
            public override double Flächenberechnung()
            {
                double lkFlächeninhalt;
                lkFlächeninhalt = Breite * Höhe - (Breite - Profildicke) * (Höhe - Profildicke);
                return lkFlächeninhalt;
            }
            public override double FlächenträgheitsmomentIXX()
            {
                double lkIXX;
                lkIXX = ((Breite * Math.Pow(Höhe, 3)) / 3) - (((Breite - Profildicke) * Math.Pow(Höhe - Profildicke, 3)) / 3) - Flächeninhalt * Math.Pow(SchwerpunktXS, 2);

                return lkIXX;
            }
            public override double FlächenträgheitsmomentIYY()
            {
                double lkIYY;
                lkIYY = ((Höhe * Math.Pow(Breite, 3)) / 3) - (((Höhe - Profildicke) * Math.Pow(Breite - Profildicke, 3)) / 3) - Flächeninhalt * Math.Pow(SchwerpunktXS, 2);
                return lkIYY;
            }
            public override double FlächenschwerpunktXS()
            {
                double lkXS;
                lkXS = ((Höhe * Breite * (Breite / 2)) - ((Höhe - Profildicke) * (Breite - Profildicke) * ((Breite - Profildicke) / 2))) / Flächeninhalt;
                return lkXS;
            }

            public override double FlächenschwerpunktYS()
            {
                double lkYS;
                lkYS = ((Breite * Höhe * (Höhe / 2)) - ((Breite - Profildicke) * (Höhe - Profildicke) * ((Höhe - Profildicke) / 2))) / Flächeninhalt;
                return lkYS;
            }
        }

        private void but_Catia_Click(object sender, RoutedEventArgs e)
        {
            LP.Breite = LP.ConvToNumber(tb_Breite.Text);
            LP.Länge = LP.ConvToNumber(tb_Laenge.Text);
            LP.Höhe = LP.ConvToNumber(tb_Hoehe.Text);
            LP.Profildicke = LP.ConvToNumber(tb_Profildicke.Text);
            try
            {
                CatiaLProfil cc = new CatiaLProfil();

                // Finde Catia Prozess
                if (cc.CATIALaeuft())
                {
                    // Öffne ein neues Part
                    cc.ErzeugePart();

                    // Erstelle eine Skizze
                    cc.ErstelleLeereSkizze();

                    // Generiere ein Profil
                    cc.ErzeugeProfil(LP.Breite, LP.Höhe, LP.Profildicke, 0);

                    // Extrudiere Balken
                    cc.ErzeugeBalken(LP.Länge);

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
    class CatiaLProfil : CatiaConnection
    {
        public override void ErzeugeProfil(Double b, Double h, Double p, Double q)
        {
            // Skizze umbenennen
            hsp_catiaProfil.set_Name("L-Profil");

            // Rechteck in Skizze einzeichnen
            // Skizze oeffnen
            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();

            // Rechteck erzeugen

            // erst die Punkte
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(0, 0);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(0, h);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(p, h);
            Point2D catPoint2D4 = catFactory2D1.CreatePoint(p, p);
            Point2D catPoint2D5 = catFactory2D1.CreatePoint(b, p);
            Point2D catPoint2D6 = catFactory2D1.CreatePoint(b, 0);

            // dann die Linien
            Line2D catLine2D1 = catFactory2D1.CreateLine(0, 0, 0, h);
            catLine2D1.StartPoint = catPoint2D1;
            catLine2D1.EndPoint = catPoint2D2;

            Line2D catLine2D2 = catFactory2D1.CreateLine(0, h, p, h);
            catLine2D2.StartPoint = catPoint2D2;
            catLine2D2.EndPoint = catPoint2D3;

            Line2D catLine2D3 = catFactory2D1.CreateLine(p, h, p, p);
            catLine2D3.StartPoint = catPoint2D3;
            catLine2D3.EndPoint = catPoint2D4;

            Line2D catLine2D4 = catFactory2D1.CreateLine(p, p, b, p);
            catLine2D4.StartPoint = catPoint2D4;
            catLine2D4.EndPoint = catPoint2D5;

            Line2D catLine2D5 = catFactory2D1.CreateLine(b, p, b, 0);
            catLine2D5.StartPoint = catPoint2D5;
            catLine2D5.EndPoint = catPoint2D6;

            Line2D catLine2D6 = catFactory2D1.CreateLine(b, 0, 0, 0);
            catLine2D6.StartPoint = catPoint2D6;
            catLine2D6.EndPoint = catPoint2D1;



            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }
    }
}
