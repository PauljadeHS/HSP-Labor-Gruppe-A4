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
using INFITF;
using MECMOD;
using PARTITF;

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
        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            KP.Länge = KP.ConvToNumber(tb_Laenge.Text);
            KP.Durchmesser = KP.ConvToNumber(tb_Durchmesser.Text);
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
            tb_Laenge.Text = Convert.ToString(KP.Länge);
            tb_Durchmesser.Text = Convert.ToString(KP.Durchmesser);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            KP.Breite = KP.ConvToNumber(tb_Durchmesser.Text);
            KP.Länge = KP.ConvToNumber(tb_Laenge.Text);
            
            try
            {
                CatiaKreis cc = new CatiaKreis();

                // Finde Catia Prozess
                if (cc.CATIALaeuft())
                {
                    // Öffne ein neues Part
                    cc.ErzeugePart();

                    // Erstelle eine Skizze
                    cc.ErstelleLeereSkizze();

                    // Generiere ein Profil
                    cc.ErzeugeProfil(KP.Breite, 0, 0, 0);

                    // Extrudiere Balken
                    cc.ErzeugeBalken(KP.Länge);
                }
                else
                {
                    MessageBox.Show("Catia läuft nicht! Bitte starten Sie Catia V5 bevor Sie eine Skizze generieren", "Catia",
                    MessageBoxButton.OK,
                     MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception aufgetreten");
            }

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
    class CatiaKreis : CatiaConnection
    {
        public override void ErzeugeProfil(Double d, Double h, Double p, Double q)
        {
            // Skizze umbenennen
            hsp_catiaProfil.set_Name("Kreisprofil");

           // Skizze oeffnen
            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();

           //Kreis erzeugen 
            catFactory2D1.CreateClosedCircle(0.000000, 0.000000, d/2);
          
            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }
    }
}
