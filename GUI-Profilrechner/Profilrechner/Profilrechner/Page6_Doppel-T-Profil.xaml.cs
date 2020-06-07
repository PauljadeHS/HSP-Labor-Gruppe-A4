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
    /// Interaktionslogik für Page6_Doppel_T_Profil.xaml
    /// </summary>
    public partial class Page6_Doppel_T_Profil : Page
    {
        Profil DTP;
        public Page6_Doppel_T_Profil()
        {
            InitializeComponent();
            DTP = new DTProfil();
        }

        //Berechnungsbutton
        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            DTP.AuslesenExcel();
            DTP.Breite = DTP.ConvToNumber(tb_Breite.Text);
            DTP.Länge = DTP.ConvToNumber(tb_Laenge.Text);
            DTP.Höhe = DTP.ConvToNumber(tb_Hoehe.Text);
            DTP.Stegdicke = DTP.ConvToNumber(tb_Stegdicke.Text);
            DTP.Flanschdicke = DTP.ConvToNumber(tb_Flanschdicke.Text);

            if (DTP.Höhe <= 2 * DTP.Flanschdicke || DTP.Breite <= DTP.Stegdicke)
            {
                DTP.Breite = 0;
                DTP.Länge = 0;
                DTP.Höhe = 0;
                DTP.Stegdicke = 0;
                DTP.Flanschdicke = 0;
                MessageBox.Show("Ungültige Eingabe: Profildiche zu groß! Eingabe wiederspricht dem aktuellen Stand des Technisch möglichen.                                                                                                    " +
                                "                                                    " +
                                "Tipp: If Error, change User!  ;-)", "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
            double Flächeninhalt = DTP.Flächenberechnung();
            DTP.Flächeninhalt = Flächeninhalt;
            double Volumeninhalt = DTP.Volumen();
            DTP.Volumeninhalt = Volumeninhalt;
            double Masse = DTP.Massenberechnung();
            double Materialkosten = DTP.Materialkosten();
            double SchwerpunktXS = DTP.FlächenschwerpunktXS();
            double SchwerpunktYS = DTP.FlächenschwerpunktYS();
            double IXX = DTP.FlächenträgheitsmomentIXX();
            double IYY = DTP.FlächenträgheitsmomentIYY();
            tb_Querschnittsflaeche.Text = Convert.ToString((String.Format("{0:0.00}", Flächeninhalt / 100)) + " cm^2"); //Flächeninhalt umrechnung im cm^2
            tb_Volumen.Text = Convert.ToString((String.Format("{0:0.00}", Volumeninhalt / 1000000)) + " l");       //Querschnittsfläche umgerechnet in dm^3
            tb_Masse.Text = Convert.ToString((String.Format("{0:0.000}", Masse / 1000) + " kg")); //Masse in kg
            tb_Materialkosten.Text = Convert.ToString((String.Format("{0:0.00}", Materialkosten) + " €"));
            tb_Schwerpunktkoordinaten.Text = Convert.ToString("Xs/Ys     = " + (String.Format("{0:0.00}",SchwerpunktXS)) + " mm / " + (String.Format("{0:0.00}", SchwerpunktYS)) + " mm");
            tb_FTMX.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IXX / 10000) + " cm^4"));
            tb_FTMY.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IYY / 10000) + " cm^4"));
            tb_Breite.Text = Convert.ToString(DTP.Breite);
            tb_Hoehe.Text = Convert.ToString(DTP.Höhe);
            tb_Laenge.Text = Convert.ToString(DTP.Länge);
            tb_Stegdicke.Text = Convert.ToString(DTP.Stegdicke);
            tb_Flanschdicke.Text = Convert.ToString(DTP.Flanschdicke);
        }

        #region ComboBox
        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            DTP.Materialint = 1; // S235 (Stahl)
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            DTP.Materialint = 2; // S355 (Stahl)
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            DTP.Materialint = 3; // EN AW-6060 (Aluminium)
        }

        private void ComboBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            DTP.Materialint = 4; // Aluminium 2
        }

        private void ComboBoxItem_Selected_4(object sender, RoutedEventArgs e)
        {
            DTP.Materialint = 5; // Messing
        }
        #endregion
        class DTProfil : SymmetrischeFLPs
        {
            public override double Flächenberechnung()
            {
                double lkFlächeninhalt;
                lkFlächeninhalt = 2 * (Breite * Flanschdicke) + (Höhe - 2 * Flanschdicke) * Stegdicke;
                return lkFlächeninhalt;
            }
            public override double FlächenträgheitsmomentIXX()
            {
                double lkIXX;
                lkIXX = (Breite * Math.Pow(Höhe, 3) - (Breite - Stegdicke) * Math.Pow((Höhe - 2 * Flanschdicke), 3)) / 12;

                return lkIXX;
            }
            public override double FlächenträgheitsmomentIYY()
            {
                double lkIYY;
                lkIYY = ((Höhe - (Höhe - 2 * Flanschdicke)) * Math.Pow(Breite, 3) + (Höhe - 2 * Flanschdicke) * Math.Pow(Breite - (Breite - Stegdicke), 3)) / 12;

                return lkIYY;
            }
        }

        private void but_Catia_Click(object sender, RoutedEventArgs e)
        {
            DTP.Breite = DTP.ConvToNumber(tb_Breite.Text);
            DTP.Länge = DTP.ConvToNumber(tb_Laenge.Text);
            DTP.Höhe = DTP.ConvToNumber(tb_Hoehe.Text);
            DTP.Stegdicke = DTP.ConvToNumber(tb_Stegdicke.Text);
            DTP.Flanschdicke = DTP.ConvToNumber(tb_Flanschdicke.Text);
            try
            {
                CatiaDoppelTProfil cc = new CatiaDoppelTProfil();

                // Finde Catia Prozess
                if (cc.CATIALaeuft())
                {
                    // Öffne ein neues Part
                    cc.ErzeugePart();

                    // Erstelle eine Skizze
                    cc.ErstelleLeereSkizze();

                    // Generiere ein Profil
                    cc.ErzeugeProfil(DTP.Breite, DTP.Höhe, DTP.Stegdicke, DTP.Flanschdicke);

                    // Extrudiere Balken
                    cc.ErzeugeBalken(DTP.Länge);

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
    class CatiaDoppelTProfil : CatiaConnection
    {
        public override void ErzeugeProfil(Double b, Double h, Double s, Double f)
        {
            // Skizze umbenennen
            hsp_catiaProfil.set_Name("Doppel-T-Profil");

            // Rechteck in Skizze einzeichnen
            // Skizze oeffnen
            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();

            // Rechteck erzeugen

            // erst die Punkte
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(0, 0);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(b, 0);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(b, f);
            Point2D catPoint2D4 = catFactory2D1.CreatePoint(b / 2 + s / 2, f);
            Point2D catPoint2D5 = catFactory2D1.CreatePoint(b / 2 + s / 2, h - f);
            Point2D catPoint2D6 = catFactory2D1.CreatePoint(b, h - f);
            Point2D catPoint2D7 = catFactory2D1.CreatePoint(b, h);
            Point2D catPoint2D8 = catFactory2D1.CreatePoint(0, h);
            Point2D catPoint2D9 = catFactory2D1.CreatePoint(0, h-f);
            Point2D catPoint2D10 = catFactory2D1.CreatePoint(b / 2 - s / 2, h-f);
            Point2D catPoint2D11 = catFactory2D1.CreatePoint(b / 2 - s / 2, f);
            Point2D catPoint2D12 = catFactory2D1.CreatePoint(0, f);

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

            Line2D catLine2D4 = catFactory2D1.CreateLine(b / 2 + s / 2, f, b / 2 + s / 2, h - f);
            catLine2D4.StartPoint = catPoint2D4;
            catLine2D4.EndPoint = catPoint2D5;

            Line2D catLine2D5 = catFactory2D1.CreateLine(b / 2 + s / 2, h - f, b, h - f);
            catLine2D5.StartPoint = catPoint2D5;
            catLine2D5.EndPoint = catPoint2D6;

            Line2D catLine2D6 = catFactory2D1.CreateLine(b, h - f, b, h);
            catLine2D6.StartPoint = catPoint2D6;
            catLine2D6.EndPoint = catPoint2D7;

            Line2D catLine2D7 = catFactory2D1.CreateLine(b, h, 0, h);
            catLine2D7.StartPoint = catPoint2D7;
            catLine2D7.EndPoint = catPoint2D8;

            Line2D catLine2D8 = catFactory2D1.CreateLine(0, h, 0, h - f);
            catLine2D8.StartPoint = catPoint2D8;
            catLine2D8.EndPoint = catPoint2D9;

            Line2D catLine2D9 = catFactory2D1.CreateLine(0, h - f, b / 2 - s / 2, h - f);
            catLine2D8.StartPoint = catPoint2D9;
            catLine2D8.EndPoint = catPoint2D10;

            Line2D catLine2D10 = catFactory2D1.CreateLine(b / 2 - s / 2, h - f, b / 2 - s / 2, f);
            catLine2D8.StartPoint = catPoint2D10;
            catLine2D8.EndPoint = catPoint2D11;

            Line2D catLine2D11 = catFactory2D1.CreateLine(b / 2 - s / 2, f, 0, f);
            catLine2D8.StartPoint = catPoint2D11;
            catLine2D8.EndPoint = catPoint2D12;

            Line2D catLine2D12 = catFactory2D1.CreateLine(0, f, 0, 0);
            catLine2D8.StartPoint = catPoint2D12;
            catLine2D8.EndPoint = catPoint2D1;


            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }
    }
}