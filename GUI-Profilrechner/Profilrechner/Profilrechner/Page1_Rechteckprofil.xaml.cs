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
            RP.Breite = Convert.ToDouble(tb_Breite.Text);
        }

        //Eingabefeld Länge
        private void tb_Laenge_TextChanged(object sender, TextChangedEventArgs e)
        {
            RP.Länge = Convert.ToDouble(tb_Laenge.Text);
        }

        //Eingabefeld Höhe 
        private void tb_Hoehe_TextChanged(object sender, TextChangedEventArgs e)
        {
            RP.Höhe = Convert.ToDouble(tb_Hoehe.Text);
        }

        //BerechnungsButton
        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            double Flächeninhalt = RP.Flächenberechnung();
            RP.Flächeninhalt = Flächeninhalt;
            double SchwerpunktXS = RP.FlächenschwerpunktXS();
            double SchwerpunktYS = RP.FlächenschwerpunktYS();
            double Volumeninhalt = RP.Volumen();
            tb_Querschnittsflaeche.Text = Convert.ToString(Flächeninhalt);
            tb_Volumen.Text = Convert.ToString(Volumeninhalt);

        }

        
    }

    class Rechteck : Profil
    {
        public override double Flächenberechnung()
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = Breite * Höhe;
            return lkFlächeninhalt;
        }

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

        public override double Volumen()
        {
            double lkVolumen;
            lkVolumen = Flächeninhalt * Länge;
            return lkVolumen;
        }


    }
}
