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

}
