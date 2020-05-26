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
    }
}