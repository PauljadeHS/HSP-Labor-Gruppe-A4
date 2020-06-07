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
    /// Interaktionslogik für Page2_Rechteckhohlprofil.xaml
    /// </summary>
    public partial class Page2_Rechteckhohlprofil : Page
    {
        Profil RHP;
        public Page2_Rechteckhohlprofil()
        {
            InitializeComponent();
            RHP = new Rechteckhohl();
        }

        //BerechnungsButton
        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            RHP.AuslesenExcel();
            RHP.Breite = RHP.ConvToNumber(tb_Breite.Text);
            RHP.Länge = RHP.ConvToNumber(tb_Laenge.Text);
            RHP.Höhe = RHP.ConvToNumber(tb_Hoehe.Text);
            RHP.Flanschbreite = RHP.ConvToNumber(tb_Profildicke.Text);

            if (RHP.Höhe / 2 <= RHP.Flanschbreite || RHP.Breite / 2 <= RHP.Flanschbreite)
            {
                RHP.Breite = 0;
                RHP.Länge = 0;
                RHP.Höhe = 0;
                RHP.Flanschbreite = 0;
                MessageBox.Show("Ungültige Eingabe: Profildiche zu groß! Eingabe wiederspricht dem aktuellen Stand des Technisch möglichen.                                                                                                    " +
                                "                                                    " +
                                "Tipp: If Error, change User!  ;-)", "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            }
            double Flächeninhalt = RHP.Flächenberechnung();
            RHP.Flächeninhalt = Flächeninhalt;
            double Volumeninhalt = RHP.Volumen();
            RHP.Volumeninhalt = Volumeninhalt;
            double Masse = RHP.Massenberechnung();
            double Materialkosten = RHP.Materialkosten();
            double SchwerpunktXS = RHP.FlächenschwerpunktXS();
            double SchwerpunktYS = RHP.FlächenschwerpunktYS();
            double IXX = RHP.FlächenträgheitsmomentIXX();
            double IYY = RHP.FlächenträgheitsmomentIYY();
            tb_Querschnittsflaeche.Text = Convert.ToString(Flächeninhalt / 100 + " cm^2"); //Flächeninhalt umrechnung im cm^2
            tb_Volumen.Text = Convert.ToString((String.Format("{0:0.00}", Volumeninhalt / 1000000)) + " l");       //Querschnittsfläche umgerechnet in dm^3
            tb_Masse.Text = Convert.ToString((String.Format("{0:0.000}", Masse / 1000) + " kg")); //Masse in kg
            tb_Materialkosten.Text = Convert.ToString((String.Format("{0:0.00}", Materialkosten) + " €"));
            tb_Schwerpunktkoordinaten.Text = Convert.ToString("Xs/Ys     = " + SchwerpunktXS + " mm / " + SchwerpunktYS + " mm");
            tb_FTMX.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IXX / 10000) + " cm^4"));
            tb_FTMY.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IYY / 10000) + " cm^4"));
            tb_Breite.Text = Convert.ToString(RHP.Breite);
            tb_Hoehe.Text = Convert.ToString(RHP.Höhe);
            tb_Laenge.Text = Convert.ToString(RHP.Länge);
            tb_Profildicke.Text = Convert.ToString(RHP.Flanschbreite);

        }
        class Rechteckhohl : SymmetrischeFLPs
        {
           
            public override double Flächenberechnung()
            {
                double lkFlächeninhalt;
                lkFlächeninhalt = Breite * Höhe - (Breite - 2 * Flanschbreite) * (Höhe - 2 * Flanschbreite);
                return lkFlächeninhalt;
            }
            public override double FlächenträgheitsmomentIXX()
            {
                double lkIXX;
                lkIXX = (Breite * Math.Pow(Höhe, 3) - (Breite - 2 * Flanschbreite) * Math.Pow((Höhe - 2 * Flanschbreite), 3)) / 12;

                return lkIXX;
            }
            public override double FlächenträgheitsmomentIYY()
            {
                double lkIYY;
                lkIYY = (Höhe * Math.Pow(Breite, 3) - (Höhe - 2 * Flanschbreite) * Math.Pow((Breite - 2 * Flanschbreite), 3)) / 12;

                return lkIYY;
            }
        }

        private void ComboBoxItem_Selected_5(object sender, RoutedEventArgs e)
        {
            RHP.Materialint = 1; //S235 (Stahl)
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            RHP.Materialint = 2; //S355 (Stahl)
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            RHP.Materialint = 3; // EN AW-6060 (Aluminium)
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            RHP.Materialint = 4; // Aluminium 2
        }

        private void ComboBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            RHP.Materialint = 5; // Messing
        }

        class CatiaRechteckhohl : CatiaConnection
        {
            public override void ErzeugeProfil(Double b, Double h, Double p, Double q)
            {
                // Skizze umbenennen
                hsp_catiaProfil.set_Name("Rechteckhohl");

                // Rechteck in Skizze einzeichnen
                // Skizze oeffnen
                Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();

                // Rechteck erzeugen

                // Punkte außen
                Point2D catPoint2D1 = catFactory2D1.CreatePoint(0, 0);
                Point2D catPoint2D2 = catFactory2D1.CreatePoint(0, h);
                Point2D catPoint2D3 = catFactory2D1.CreatePoint(b, h);
                Point2D catPoint2D4 = catFactory2D1.CreatePoint(b, 0);

                // Punkte innen 
                Point2D catPoint2D5 = catFactory2D1.CreatePoint(p, p);
                Point2D catPoint2D6 = catFactory2D1.CreatePoint(p, h-p);
                Point2D catPoint2D7 = catFactory2D1.CreatePoint(b-p, h-p);
                Point2D catPoint2D8 = catFactory2D1.CreatePoint(b-p, p);


                // Linien innen
                Line2D catLine2D5 = catFactory2D1.CreateLine(p, p, p, h - p);
                catLine2D5.StartPoint = catPoint2D5;
                catLine2D5.EndPoint = catPoint2D6;

                Line2D catLine2D6 = catFactory2D1.CreateLine(p, h - p, b - p, h - p);
                catLine2D6.StartPoint = catPoint2D6;
                catLine2D6.EndPoint = catPoint2D7;

                Line2D catLine2D7 = catFactory2D1.CreateLine(b - p, h - p, b - p, p);
                catLine2D7.StartPoint = catPoint2D7;
                catLine2D7.EndPoint = catPoint2D8;

                Line2D catLine2D8 = catFactory2D1.CreateLine(b - p, p, p, p);
                catLine2D8.StartPoint = catPoint2D8;
                catLine2D8.EndPoint = catPoint2D5;

                // Linien außen
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RHP.Breite = RHP.ConvToNumber(tb_Breite.Text);
            RHP.Länge = RHP.ConvToNumber(tb_Laenge.Text);
            RHP.Höhe = RHP.ConvToNumber(tb_Hoehe.Text);
            RHP.Flanschbreite = RHP.ConvToNumber(tb_Profildicke.Text);
            try
            {
                CatiaRechteckhohl cc = new CatiaRechteckhohl();

                // Finde Catia Prozess
                if (cc.CATIALaeuft())
                {
                    // Öffne ein neues Part
                    cc.ErzeugePart();

                    // Erstelle eine Skizze
                    cc.ErstelleLeereSkizze();

                    // Generiere ein Profil
                    cc.ErzeugeProfil(RHP.Breite, RHP.Höhe, RHP.Flanschbreite,0 );

                    // Extrudiere Balken
                    cc.ErzeugeBalken(RHP.Länge);

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
}
