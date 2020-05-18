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
                        DTP.Breite = Convert.ToDouble(tb_Breite.Text);
                    }
                    else
                    {
                        tb_Breite.Text = "";
                    }
            }
        }

        //Eingabe Hoehe
        private void tb_Hoehe_TextChanged(object sender, TextChangedEventArgs e)
        {
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
                            DTP.Höhe = Convert.ToDouble(tb_Hoehe.Text);
                        }
                        else
                        {
                            tb_Hoehe.Text = "";
                        }
                }
            }
        }

        //Eingabe Laenge
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
                        DTP.Länge = Convert.ToDouble(tb_Laenge.Text);
                    }
                    else
                    {
                        tb_Laenge.Text = "";
                    }
            }
        }

        //Eingabe Stegdicke
        private void tb_Stegdicke_TextChanged(object sender, TextChangedEventArgs e)
        {
            string test;
            String Zeichen = "0123456789.,";
            if (tb_Stegdicke.Text.Equals(""))
            { }
            else
            {
                test = tb_Stegdicke.Text;
                foreach (char ch in test)
                    if (Zeichen.Contains(ch.ToString()))
                    {
                        DTP.Stegdicke = Convert.ToDouble(tb_Stegdicke.Text);
                    }
                    else
                    {
                        tb_Stegdicke.Text = "";
                    }
            }
        }

        //Eingabe Flanschdicke
        private void tb_Flanschdicke_TextChanged(object sender, TextChangedEventArgs e)
        {
            string test;
            String Zeichen = "0123456789.,";
            if (tb_Flanschdicke.Text.Equals(""))
            { }
            else
            {
                test = tb_Stegdicke.Text;
                foreach (char ch in test)
                    if (Zeichen.Contains(ch.ToString()))
                    {
                        DTP.Flanschdicke = Convert.ToDouble(tb_Flanschdicke.Text);
                    }
                    else
                    {
                        tb_Flanschdicke.Text = "";
                    }
            }
        }

        //Berechnungsbutton
        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            if (DTP.Höhe <= 2 * DTP.Flanschdicke || DTP.Breite <= DTP.Stegdicke)
            {
                MessageBox.Show("Ungültige Eingabe: Profildiche zu groß! Eingabe wiederspricht dem aktuellen Stand des Technisch möglichen.                                                                                                    " +
                                "                                                    " +
                                "Tipp: If Error, change User!  ;-)", "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }

            else
            {
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
            }
            //Plausibilitätsprüfung
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