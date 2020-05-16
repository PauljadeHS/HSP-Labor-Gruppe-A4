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
    /// Interaktionslogik für Page4_Kreishohlprofil.xaml
    /// </summary>
    public partial class Page4_Kreishohlprofil : Page
    {
        Profil KHP;
        public Page4_Kreishohlprofil()
        {
            InitializeComponent();
            KHP = new Kreishohl();
        }

        //Eingabe Durchmesser
        private void tb_Durchmesser_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string test;
            String Zeichen = "0123456789.,";
            if (tb_Durchmesser.Text.Equals(""))
            { }
            else
            {
                test = tb_Durchmesser.Text;
                foreach (char ch in test)
                    if (Zeichen.Contains(ch.ToString()))
                    {
                        KHP.Durchmesser = Convert.ToDouble(tb_Durchmesser.Text);
                    }
                    else
                    {
                        tb_Durchmesser.Text = "";
                    }
            }

        }

        //Eingabe Länge
        private void tb_Laenge_TextChanged(object sender, TextChangedEventArgs e)
        {
            string test;
            String Zeichen = "0123456789.,";
            if (tb_Laenge.Text.Equals(""))
            { }
            else
            {
                test = tb_Laenge.Text;
                foreach (char ch in test)
                    if (Zeichen.Contains(ch.ToString()))
                    {
                        KHP.Länge = Convert.ToDouble(tb_Laenge.Text);
                    }
                    else
                    {
                        tb_Laenge.Text = "";
                    }
            }

        }

        //Eingabe Profildicke
        private void tb_Profildicke_TextChanged(object sender, TextChangedEventArgs e)
        {
            string test;
            String Zeichen = "0123456789.,";
            if (tb_Profildicke.Text.Equals(""))
            { }
            else
            {
                test = tb_Profildicke.Text;
                foreach (char ch in test)
                    if (Zeichen.Contains(ch.ToString()))
                    {
                        KHP.Profildicke = Convert.ToDouble(tb_Profildicke.Text);
                    }
                    else
                    {
                        tb_Profildicke.Text = "";
                    }
            }
        }

        //Berrechnungsbutton
        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            if (KHP.Durchmesser / 2 <= KHP.Profildicke)
            {
                MessageBox.Show("Ungültige Eingabe: Profildiche zu groß! Eingabe wiederspricht dem aktuellen Stand des Technisch möglichen.                                                                                                    " +
                                "                                                    " +
                                "Tipp: If Error, change User!  ;-)", "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
            else
            {
                double Flächeninhalt = KHP.Flächenberechnung();
                KHP.Flächeninhalt = Flächeninhalt;
                double Volumeninhalt = KHP.Volumen();
                KHP.Volumeninhalt = Volumeninhalt;
                double Masse = KHP.Massenberechnung();
                double Materialkosten = KHP.Materialkosten();
                double SchwerpunktXS = KHP.FlächenschwerpunktXS();
                double SchwerpunktYS = KHP.FlächenschwerpunktYS();
                double IXX = KHP.FlächenträgheitsmomentIXX();
                double IYY = KHP.FlächenträgheitsmomentIYY();
                tb_Querschnittsflaeche.Text = Convert.ToString((String.Format("{0:0.00}", Flächeninhalt / 100)) + " cm^2"); //Flächeninhalt umrechnung im cm^2
                tb_Volumen.Text = Convert.ToString((String.Format("{0:0.00}", Volumeninhalt / 1000000)) + " l");       //Querschnittsfläche umgerechnet in dm^3
                tb_Masse.Text = Convert.ToString((String.Format("{0:0.000}", Masse / 1000) + " kg")); //Masse in kg
                tb_Materialkosten.Text = Convert.ToString((String.Format("{0:0.00}", Materialkosten) + " €"));
                tb_Schwerpunktkoordinaten.Text = Convert.ToString("Xs/Ys     = " + SchwerpunktXS + " mm / " + SchwerpunktYS + " mm");
                tb_FTMX.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IXX / 10000) + " cm^4"));
                tb_FTMY.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IYY / 10000) + " cm^4"));
            }
            //Plausibilitätsprüfung
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            KHP.Materialint = 1; //S235 (Stahl)
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            KHP.Materialint = 2; //S355 (Stahl)
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            KHP.Materialint = 3; // EN AW-6060 (Aluminium)
        }

        private void ComboBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            KHP.Materialint = 4; // Aluminium 2
        }

        private void ComboBoxItem_Selected_4(object sender, RoutedEventArgs e)
        {
            KHP.Materialint = 5; // Messing
        }       
    }
    class Kreishohl : Kreisprofile
    {
        public override double Flächenberechnung()
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = (Math.Pow((Durchmesser / 2), 2) - Math.Pow((Durchmesser/2)-Profildicke,2)) * Math.PI;
            return lkFlächeninhalt;
        }
        public override double FlächenträgheitsmomentIXX()
        {
            double lkIXX;
            lkIXX = (Math.PI / 64) * ((Math.Pow(Durchmesser, 4))- Math.Pow(Durchmesser-(2*Profildicke), 4));
            return lkIXX;
        }
        public override double FlächenträgheitsmomentIYY()
        {
            double lkIYY;
            lkIYY = (Math.PI / 64) * ((Math.Pow(Durchmesser, 4)) - Math.Pow(Durchmesser - (2 * Profildicke), 4));

            return lkIYY;
        }
    }
}
