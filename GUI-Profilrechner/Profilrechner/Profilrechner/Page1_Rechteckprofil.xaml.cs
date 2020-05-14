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
            RP.Material = "S235 (Stahl)";
        }

        //Eingabefeld Breite 
        private void tb_Breite_TextChanged(object sender, TextChangedEventArgs e)
        {

            string test;
            String Zeichen = "0123456789.,";
            //StringBuilder Zugelassen = new StringBuilder();
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
            //StringBuilder Zugelassen = new StringBuilder();
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
            //StringBuilder Zugelassen = new StringBuilder();
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
        
        //Eingeabe Material 
        //private void cb_Material_Selected(object sender, TextChangedEventArgs e)
        //{
        //RP.Material = cb_Material.SelectedItem.ToString();
        //}

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
            tb_Querschnittsflaeche.Text = Convert.ToString(Flächeninhalt/100); //Flächeninhalt umrechnung im cm^2
            tb_Volumen.Text = Convert.ToString(Volumeninhalt / 1000000);       //Querschnittsfläche umgerechnet in dm^3
            tb_Masse.Text = Convert.ToString(Masse);
            tb_Materialkosten.Text = Convert.ToString(Materialkosten);
            tb_Schwerpunktkoordinaten.Text = Convert.ToString("Xs/Ys = " +SchwerpunktXS)+"/"+ Convert.ToString(SchwerpunktYS);
            tb_FTMX.Text = Convert.ToString((String.Format(" {0:0.0}", IXX/10000) + "cm^4"));
            tb_FTMY.Text = Convert.ToString((String.Format(" {0:0.0}", IYY/10000) + "cm^4"));


        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)sender;
            RP.Material = Convert.ToString(cbi);
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
