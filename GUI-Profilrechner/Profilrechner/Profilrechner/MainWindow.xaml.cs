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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnClickP1Rechteckprofil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page1_Rechteckprofil ();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }
        private void BtnClickP2Rechteckhohlprofil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page2_Rechteckhohlprofil();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }

        private void BtnClickP3Kreisprofil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page3_Kreisprofil();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }

        private void BtnClickP4Kreishohlprofil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page4_Kreishohlprofil();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }

        
    }

    abstract class Profil:Object
    {
        public double Breite, Höhe, Durchmesser, Länge, Flanschbreite, Stegbreite, Flächeninhalt, Volumeninhalt;
        public string Material;
        public abstract double Flächenberechnung();
        public abstract double FlächenschwerpunktXS();
        public abstract double FlächenschwerpunktYS();
        public abstract double Volumen();

        public double Massenberechnung()
        {
            double Dichte = 0;
            if (Material == "S235 (Stahl)")
            {
                Dichte = 7.35;
            }
            else if (Material == "EN AW - 6060(Aluminium)")
            {
                Dichte = 2.6;
            }
            else if (Material == "S355")
            {
                Dichte = 7;
            }
            else if (Material == "Aluminium 2")
            {
                Dichte = 7;
            }
            else if (Material == "Messing")
            {
                Dichte = 7;
            }
            double a = Volumeninhalt * Dichte;
            return a;

        }
    }
}
