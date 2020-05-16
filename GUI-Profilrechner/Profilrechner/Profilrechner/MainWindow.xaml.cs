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

        private void BtnClickP5TProfil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page5_T_Profil();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }

        private void BtnClickP6DoppelTProfil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page6_Doppel_T_Profil();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }

        private void BtnClickP7UProfil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page7_U_Profil();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }

        private void BtnClickP8LProfil(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page8_L_Profil();
            lbl_Begrüßung.Visibility = Visibility.Hidden;
        }
        private void btn_Beenden_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);

        }
    }

    abstract class Profil:Object
    {
        public double Breite, Höhe, Durchmesser, Länge, Flanschbreite, Stegbreite, Flächeninhalt, Volumeninhalt, Masse, Profildicke;
        public double Materialkf = 1;
        public int Materialint = 1;        
        public abstract double Flächenberechnung();
        public abstract double FlächenschwerpunktXS();
        public abstract double FlächenschwerpunktYS();
        public abstract double FlächenträgheitsmomentIXX();
        public abstract double FlächenträgheitsmomentIYY();
        
        public double Volumen()
        {
            double lkVolumen;
            lkVolumen = Flächeninhalt * Länge;
            return lkVolumen;
        }

        public double Massenberechnung()
        {
            double Dichte = 0;
            
            if (Materialint == 1) //S235
            {
                Dichte = 0.00785;
                Materialkf = 1;
            }
            else if (Materialint == 2) //S355
            {
                Dichte = 0.00785;
                Materialkf = 1.1;
            }
            else if (Materialint == 3) //AW6060
            {
                Dichte = 0.0027;
                Materialkf = 3;
            }
            else if (Materialint == 4)  //AW6082
            {
                Dichte = 0.0027;
                Materialkf = 3.2;
            }
            else if (Materialint == 5) //MS63
            {
                Dichte = 0.00873;
                Materialkf = 8;
            }
            double masse = Volumeninhalt * Dichte;
            Masse = masse;
            return masse;

        }
        public double Materialkosten()
        {
            double Kosten;
            double Grundpreis = 0.0005;//€/g    //500€/Tonne
            Kosten = Masse * Grundpreis* Materialkf;
            return Kosten;
                
        }

    }
}
