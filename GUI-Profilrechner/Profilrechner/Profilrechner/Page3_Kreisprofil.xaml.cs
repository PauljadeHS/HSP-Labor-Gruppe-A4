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
    /// Interaktionslogik für Page3_Kreisprofil.xaml
    /// </summary>
    public partial class Page3_Kreisprofil : Page
    {
        Profil KP;        
        public Page3_Kreisprofil()
        {
            InitializeComponent();
            KP= new Kreis();            
        }

        //Eingabefeld Durchmesser
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
                        KP.Durchmesser = Convert.ToDouble(tb_Durchmesser.Text);
                    }
                    else
                    {
                        tb_Durchmesser.Text = "";
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
                        KP.Länge = Convert.ToDouble(tb_Laenge.Text);
                    }
                    else
                    {
                        tb_Laenge.Text = "";
                    }
            }
        }

        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            double Flächeninhalt = KP.Flächenberechnung();
            KP.Flächeninhalt = Flächeninhalt;
            double Volumeninhalt = KP.Volumen();
            KP.Volumeninhalt = Volumeninhalt;
            double Masse = KP.Massenberechnung();
            double Materialkosten = KP.Materialkosten();
            double SchwerpunktXS = KP.FlächenschwerpunktXS();
            double SchwerpunktYS = KP.FlächenschwerpunktXS();
            double IXX = KP.FlächenträgheitsmomentIXX();
            double IYY = KP.FlächenträgheitsmomentIYY();
            tb_Querschnittsflaeche.Text = Convert.ToString((String.Format("{0:0.00}", Flächeninhalt / 100)) + " cm^2");                  //Flächeninhalt umrechnung im cm^2
            tb_Volumen.Text = Convert.ToString((String.Format("{0:0.00}", Volumeninhalt / 1000000)) + " l");//Querschnittsfläche umgerechnet in dm^3
            tb_Masse.Text = Convert.ToString((String.Format("{0:0.000}", Masse / 1000) + " kg"));            //Masse in kg
            tb_Materialkosten.Text = Convert.ToString((String.Format("{0:0.00}", Materialkosten) + " €"));
            tb_Schwerpunktkoordinaten.Text = Convert.ToString("Xs/Ys     = " + SchwerpunktXS + " mm / " + SchwerpunktYS + " mm");
            tb_FTMX.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IXX / 10000) + " cm^4"));
            tb_FTMY.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IYY / 10000) + " cm^4"));
        }
      
        private void ComboBoxItem_Selected_5(object sender, RoutedEventArgs e)
        {
            KP.Materialint = 1; //S235 (Stahl)
        }

        private void ComboBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            KP.Materialint = 2; //S355 (Stahl)
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            KP.Materialint = 3; // EN AW-6060 (Aluminium)
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            KP.Materialint = 4; // Aluminium 2
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            KP.Materialint = 5; // Messing
        }
    }
    abstract class Kreisprofile : Profil
    {
        public override double FlächenschwerpunktXS()
        {
            double lkXS;
            lkXS = Durchmesser / 2;
            return lkXS;
        }

        public override double FlächenschwerpunktYS()
        {
            double lkYS;
            lkYS = Durchmesser / 2;
            return lkYS;
        }
    }
    class Kreis : Kreisprofile
    {   
        public override double Flächenberechnung()
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = (Math.Pow((Durchmesser / 2), 2))*Math.PI;
            return lkFlächeninhalt;
        }
        public override double FlächenträgheitsmomentIXX()
        {
            double lkIXX;
            lkIXX = (Math.PI / 64) * ((Math.Pow(Durchmesser, 4)));
            return lkIXX;
        }
        public override double FlächenträgheitsmomentIYY()
        {
            double lkIYY;
            lkIYY = (Math.PI / 64) * ((Math.Pow(Durchmesser, 4)));

            return lkIYY;
        }
    }
}
