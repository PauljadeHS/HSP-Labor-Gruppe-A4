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

            if (RHP.Höhe / 2 <= RHP.Flanschbreite || RHP.Breite / 2 <= RHP.Flanschbreite )
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
    }
}
