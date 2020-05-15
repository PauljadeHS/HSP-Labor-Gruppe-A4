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
    /// Interaktionslogik für Page1_Rechteckprofil.xaml
    /// </summary>
    public partial class Page1_Rechteckprofil : Page
    {
        Profil RP;
        public Page1_Rechteckprofil()
        {
            InitializeComponent();
            RP = new Rechteck();
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
                        RP.Breite = Convert.ToDouble(tb_Breite.Text);
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
            { }
            else
            {
                test = tb_Laenge.Text;
                foreach (char ch in test)
                    if (Zeichen.Contains(ch.ToString()))
                    {
                        RP.Länge = Convert.ToDouble(tb_Laenge.Text);
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
                        RP.Höhe = Convert.ToDouble(tb_Hoehe.Text);
                    }
                    else
                    {
                        tb_Hoehe.Text = "";
                    }
            }
        }

        //BerechnungsButton
        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            double Flächeninhalt = RP.Flächenberechnung();
            RP.Flächeninhalt = Flächeninhalt;
            double Volumeninhalt = RP.Volumen();
            RP.Volumeninhalt = Volumeninhalt;
            double Masse = RP.Massenberechnung();
            double Materialkosten = RP.Materialkosten();
            double SchwerpunktXS = RP.FlächenschwerpunktXS();
            double SchwerpunktYS = RP.FlächenschwerpunktYS();
            double IXX = RP.FlächenträgheitsmomentIXX();
            double IYY = RP.FlächenträgheitsmomentIYY();
            tb_Querschnittsflaeche.Text = Convert.ToString(Flächeninhalt / 100 + " cm^2");                  //Flächeninhalt umrechnung im cm^2
            tb_Volumen.Text = Convert.ToString((String.Format("{0:0.00}", Volumeninhalt / 1000000)) + " l");//Querschnittsfläche umgerechnet in dm^3
            tb_Masse.Text = Convert.ToString((String.Format("{0:0.000}", Masse /1000) + " kg"));            //Masse in kg
            tb_Materialkosten.Text = Convert.ToString((String.Format("{0:0.00}", Materialkosten) + " €"));
            tb_Schwerpunktkoordinaten.Text = Convert.ToString("Xs/Ys     = " + SchwerpunktXS + " mm / " + SchwerpunktYS + " mm");
            tb_FTMX.Text = Convert.ToString("="+(String.Format(" {0:0.0}", IXX / 10000) + " cm^4"));
            tb_FTMY.Text = Convert.ToString("="+(String.Format(" {0:0.0}", IYY / 10000) + " cm^4"));

        }
        ///IsSelected="True"
        private void ComboBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            RP.Materialint = 1; //S235 (Stahl)
        }
        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            RP.Materialint = 2; //S355 (Stahl)
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            RP.Materialint = 3; // EN AW-6060 (Aluminium)
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            RP.Materialint = 4; // Aluminium 2
        }

        private void ComboBoxItem_Selected_4(object sender, RoutedEventArgs e)
        {
            RP.Materialint = 5; // Messing
        }
    }
    abstract class SymmetrischeFLPs : Profil
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
            lkYS = Höhe / 2;
            return lkYS;
        }
    }
        

    class Rechteck : SymmetrischeFLPs
    {
        public override double Flächenberechnung()
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = Breite * Höhe;
            return lkFlächeninhalt;
        }
        public override double FlächenträgheitsmomentIXX()
        {
            double lkIXX;
            lkIXX = (Breite * Math.Pow(Höhe, 3)) / 12;

            return lkIXX;
        }
        public override double FlächenträgheitsmomentIYY()
        {
            double lkIYY;
            lkIYY = (Math.Pow(Breite, 3) * Höhe) / 12;

            return lkIYY;
        }
    }
}
