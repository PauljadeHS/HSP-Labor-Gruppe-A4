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

        //Eingabefeld Breite
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
                        TP.Breite = Convert.ToDouble(tb_Breite.Text);
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
                        TP.Höhe = Convert.ToDouble(tb_Hoehe.Text);
                    }
                    else
                    {
                        tb_Hoehe.Text = "";
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
                        TP.Länge = Convert.ToDouble(tb_Laenge.Text);
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
                        TP.Stegdicke = Convert.ToDouble(tb_Stegdicke.Text);
                    }
                    else
                    {
                        tb_Stegdicke.Text = "";
                    }
            }
        }

        //EingabeFlanschdicke
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
                        TP.Flanschdicke = Convert.ToDouble(tb_Flanschdicke.Text);
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
            if (TP.Höhe <= TP.Flanschdicke || TP.Breite <= TP.Stegdicke)
            {
                MessageBox.Show("Ungültige Eingabe: Profildiche zu groß! Eingabe wiederspricht dem aktuellen Stand des Technisch möglichen.                                                                                                    " +
                                "                                                    " +
                                "Tipp: If Error, change User!  ;-)", "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }

            else
            {
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
            }
            //Plausibilitätsprüfung

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
}
