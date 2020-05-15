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
                        RHP.Breite = Convert.ToDouble(tb_Breite.Text);
                    }
                    else
                    {
                        tb_Breite.Text = "";
                    }
            }
        }
        //Eingabefeld Länge
        private void tb_Laenge_TextChanged(object sender, TextChangedEventArgs e)
        {
            string test;
            String Zeichen = "0123456789.,";
           if (tb_Laenge.Text.Equals(""))
            {}
            else
            {
                test = tb_Laenge.Text;
                foreach (char ch in test)
                    if (Zeichen.Contains(ch.ToString()))
                    {
                        RHP.Länge = Convert.ToDouble(tb_Laenge.Text);
                    }
                    else
                    {
                        tb_Laenge.Text = "";
                    }
            }  
        }

        //Eingabefeld Höhe 
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
                        RHP.Höhe = Convert.ToDouble(tb_Hoehe.Text);
                    }
                    else
                    {
                        tb_Hoehe.Text = "";
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
                        RHP.Flanschbreite = Convert.ToDouble(tb_Profildicke.Text);
                    }
                    else
                    {
                        tb_Profildicke.Text = "";
                    }
            }
        }
        
        //BerechnungsButton
        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            
            if (RHP.Höhe / 2 <= RHP.Flanschbreite || RHP.Breite / 2 <= RHP.Flanschbreite )
            {
                MessageBox.Show("Ungültige Eingabe: Profildiche zu groß! Eingabe wiederspricht dem aktuellen Stand des Technisch möglichen.                                                                                                    " +
                                "                                                    " +
                                "Tipp: If Error, change User!  ;-)", "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }

            else
            {
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
            }
            //Plausibilitätsprüfung
            
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

        private void tb_Querschnittsflaeche_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
