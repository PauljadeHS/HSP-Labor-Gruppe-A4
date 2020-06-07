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
    /// Interaktionslogik für Page5_T_Profil.xaml
    /// </summary>
    public partial class Page5_T_Profil : Page
    {
        Profil TP;
        public Page5_T_Profil()
        {
            InitializeComponent();
            TP = new TProfil();
        }

        //Berechnungsbutton
        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            TP.Breite = TP.ConvToNumber(tb_Breite.Text);
            TP.Länge = TP.ConvToNumber(tb_Laenge.Text);
            TP.Höhe = TP.ConvToNumber(tb_Hoehe.Text);
            TP.Stegdicke = TP.ConvToNumber(tb_Stegdicke.Text);
            TP.Flanschdicke = TP.ConvToNumber(tb_Flanschdicke.Text);

            if (TP.Höhe <= TP.Flanschdicke || TP.Breite <= TP.Stegdicke)
            {
                TP.Breite = 0;
                TP.Länge = 0;
                TP.Höhe = 0;
                TP.Stegdicke = 0;
                TP.Flanschdicke = 0;
                MessageBox.Show("Ungültige Eingabe: Profildiche zu groß! Eingabe wiederspricht dem aktuellen Stand des Technisch möglichen.                                                                                                    " +
                                "                                                    " +
                                "Tipp: If Error, change User!  ;-)", "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
            double Flächeninhalt = TP.Flächenberechnung();
            TP.Flächeninhalt = Flächeninhalt;
            double Volumeninhalt = TP.Volumen();
            TP.Volumeninhalt = Volumeninhalt;
            double Masse = TP.Massenberechnung();
            double Materialkosten = TP.Materialkosten();
            double SchwerpunktXS = TP.FlächenschwerpunktXS();
            double SchwerpunktYS = TP.FlächenschwerpunktYS();
            double IXX = TP.FlächenträgheitsmomentIXX();
            double IYY = TP.FlächenträgheitsmomentIYY();
            tb_Querschnittsflaeche.Text = Convert.ToString((String.Format("{0:0.00}", Flächeninhalt / 100)) + " cm^2"); //Flächeninhalt umrechnung im cm^2
            tb_Volumen.Text = Convert.ToString((String.Format("{0:0.00}", Volumeninhalt / 1000000)) + " l");       //Querschnittsfläche umgerechnet in dm^3
            tb_Masse.Text = Convert.ToString((String.Format("{0:0.000}", Masse / 1000) + " kg")); //Masse in kg
            tb_Materialkosten.Text = Convert.ToString((String.Format("{0:0.00}", Materialkosten) + " €"));
            tb_Schwerpunktkoordinaten.Text = Convert.ToString("Xs/Ys     = " + (String.Format("{0:0.00}", SchwerpunktXS)) + " mm / " + (String.Format("{0:0.00}", SchwerpunktYS)) + " mm");
            tb_FTMX.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IXX / 10000) + " cm^4"));
            tb_FTMY.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IYY / 10000) + " cm^4"));
            tb_Breite.Text = Convert.ToString(TP.Breite);
            tb_Hoehe.Text = Convert.ToString(TP.Höhe);
            tb_Laenge.Text = Convert.ToString(TP.Länge);
            tb_Stegdicke.Text = Convert.ToString(TP.Stegdicke);
            tb_Flanschdicke.Text = Convert.ToString(TP.Flanschdicke);
            
        }

        //ComboBox
        #region ComboBox
        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            TP.Materialint = 1; // S235 (Stahl)
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            TP.Materialint = 2; // S355 (Stahl)
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            TP.Materialint = 3; // EN AW-6060 (Aluminium)
        }

        private void ComboBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            TP.Materialint = 4; // Aluminium 2
        }

        private void ComboBoxItem_Selected_4(object sender, RoutedEventArgs e)
        {
            TP.Materialint = 5; // Messing
        }
        #endregion

        class TProfil : SP_TProfil
        {
            public override double Flächenberechnung()
            {
                double lkFlächeninhalt;
                lkFlächeninhalt = Breite * Flanschdicke + (Höhe - Flanschdicke) * Stegdicke;
                return lkFlächeninhalt;
            }
            public override double FlächenträgheitsmomentIXX()
            {
                double lkIXX;
                lkIXX = ((Breite * Math.Pow(Flanschdicke, 3) / 12) + (Stegdicke * Math.Pow((Höhe - Flanschdicke), 3)) / 12);

                return lkIXX;
            }
            public override double FlächenträgheitsmomentIYY()
            {
                double lkIYY;
                lkIYY = ((Flanschdicke * Math.Pow(Breite, 3)) / 12 + ((Höhe - Flanschdicke) * Math.Pow(Stegdicke, 3)) / 12);

                return lkIYY;
            }
        }

        private void but_Catia_Click(object sender, RoutedEventArgs e)
        {
            TP.Breite = TP.ConvToNumber(tb_Breite.Text);
            TP.Länge = TP.ConvToNumber(tb_Laenge.Text);
            TP.Höhe = TP.ConvToNumber(tb_Hoehe.Text);
            TP.Stegdicke = TP.ConvToNumber(tb_Stegdicke.Text);
            TP.Flanschdicke = TP.ConvToNumber(tb_Flanschdicke.Text);
            try
            {
                CatiaTProfil cc = new CatiaTProfil();

                // Finde Catia Prozess
                if (cc.CATIALaeuft())
                {
                    // Öffne ein neues Part
                    cc.ErzeugePart();

                    // Erstelle eine Skizze
                    cc.ErstelleLeereSkizze();

                    // Generiere ein Profil
                    cc.ErzeugeProfil(TP.Breite, TP.Höhe, TP.Stegdicke, TP.Flanschdicke);

                    // Extrudiere Balken
                    cc.ErzeugeBalken(TP.Länge);

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
    abstract class SP_TProfil : Profil
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
            lkYS = ((Breite * Flanschdicke * (Höhe - Flanschdicke / 2)) + (Höhe-Flanschdicke*Stegdicke*(Höhe-Flanschdicke)/2))/ (Breite * Flanschdicke + (Höhe - Flanschdicke) * Stegdicke);
            return lkYS;
        }
        
    }
    class CatiaTProfil : CatiaConnection
    {
        public override void ErzeugeProfil(Double b, Double h, Double s, Double f)
        {
            // Skizze umbenennen
            hsp_catiaProfil.set_Name("T-Profil");

            // Rechteck in Skizze einzeichnen
            // Skizze oeffnen
            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();

            // Rechteck erzeugen

            // erst die Punkte
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(0, 0);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(b, 0);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(b,f);
            Point2D catPoint2D4 = catFactory2D1.CreatePoint(b/2 +s/2,f);
            Point2D catPoint2D5 = catFactory2D1.CreatePoint(b / 2 + s / 2, h);
            Point2D catPoint2D6 = catFactory2D1.CreatePoint(b / 2 - s / 2, h);
            Point2D catPoint2D7 = catFactory2D1.CreatePoint(b / 2 - s / 2, f);
            Point2D catPoint2D8 = catFactory2D1.CreatePoint(0,f);

            // dann die Linien
            Line2D catLine2D1 = catFactory2D1.CreateLine(0, 0, b, 0);
            catLine2D1.StartPoint = catPoint2D1;
            catLine2D1.EndPoint = catPoint2D2;

            Line2D catLine2D2 = catFactory2D1.CreateLine(b, 0, b, f);
            catLine2D2.StartPoint = catPoint2D2;
            catLine2D2.EndPoint = catPoint2D3;

            Line2D catLine2D3 = catFactory2D1.CreateLine(b, f, b / 2 + s / 2, f);
            catLine2D3.StartPoint = catPoint2D3;
            catLine2D3.EndPoint = catPoint2D4;

            Line2D catLine2D4 = catFactory2D1.CreateLine(b / 2 + s / 2, f, b / 2 + s / 2, h);
            catLine2D4.StartPoint = catPoint2D4;
            catLine2D4.EndPoint = catPoint2D5;

            Line2D catLine2D5 = catFactory2D1.CreateLine(b / 2 + s / 2, h, b / 2 - s / 2, h);
            catLine2D5.StartPoint = catPoint2D5;
            catLine2D5.EndPoint = catPoint2D6;

            Line2D catLine2D6 = catFactory2D1.CreateLine(b / 2 - s / 2, h, b / 2 - s / 2, f);
            catLine2D6.StartPoint = catPoint2D6;
            catLine2D6.EndPoint = catPoint2D7;

            Line2D catLine2D7 = catFactory2D1.CreateLine(b / 2 - s / 2, f, 0, f);
            catLine2D7.StartPoint = catPoint2D7;
            catLine2D7.EndPoint = catPoint2D8;

            Line2D catLine2D8 = catFactory2D1.CreateLine(0, f, 0, 0);
            catLine2D8.StartPoint = catPoint2D8;
            catLine2D8.EndPoint = catPoint2D1;


            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }
    }
}
