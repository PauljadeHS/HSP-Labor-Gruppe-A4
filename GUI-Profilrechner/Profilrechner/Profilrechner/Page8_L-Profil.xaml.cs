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

        private void tb_Breite_TextChanged(object sender, TextChangedEventArgs e)
        {
            string test;
            String Zeichen = "0123456789.,";
            if (tb_Breite.Text.Equals(""))
            { }
            else
            {
                test = tb_Breite.Text;
                foreach (char ch in test)
                    if (Zeichen.Contains(ch.ToString()))
                    {
                        LP.Breite = Convert.ToDouble(tb_Breite.Text);
                    }
                    else
                    {
                        tb_Breite.Text = "";
                    }
            }
        }

        private void tb_Hoehe_TextChanged(object sender, TextChangedEventArgs e)
        {
            string test;
            String Zeichen = "0123456789.,";
            if (tb_Hoehe.Text.Equals(""))
            { }
            else
            {
                test = tb_Hoehe.Text;
                foreach (char ch in test)
                    if (Zeichen.Contains(ch.ToString()))
                    {
                        LP.Höhe = Convert.ToDouble(tb_Hoehe.Text);
                    }
                    else
                    {
                        tb_Hoehe.Text = "";
                    }
            }
        }

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
                        LP.Länge = Convert.ToDouble(tb_Laenge.Text);
                    }
                    else
                    {
                        tb_Laenge.Text = "";
                    }
            }
        }

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
                        LP.Profildicke = Convert.ToDouble(tb_Profildicke.Text);
                    }
                    else
                    {
                        tb_Profildicke.Text = "";
                    }
            }
        }

        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
