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
    /// Interaktionslogik für Page4_Kreishohlprofil.xaml
    /// </summary>
    public partial class Page4_Kreishohlprofil : Page
    {
        Profil KHP;
        public Page4_Kreishohlprofil()
        {
            InitializeComponent();
            KHP = new Kreishohl();
        }

        //Berrechnungsbutton
        private void but_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            KHP.Durchmesser = KHP.ConvToNumber(tb_Durchmesser.Text);
            KHP.Länge = KHP.ConvToNumber(tb_Laenge.Text);
            KHP.Profildicke = KHP.ConvToNumber(tb_Profildicke.Text);
            if (KHP.Durchmesser / 2 <= KHP.Profildicke)
            {
                KHP.Durchmesser = 0;
                KHP.Länge = 0;
                KHP.Profildicke = 0;
                MessageBox.Show("Ungültige Eingabe: Profildiche zu groß! Eingabe wiederspricht dem aktuellen Stand des Technisch möglichen.                                                                                                    " +
                                "                                                    " +
                                "Tipp: If Error, change User!  ;-)", "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
            double Flächeninhalt = KHP.Flächenberechnung();
            KHP.Flächeninhalt = Flächeninhalt;
            double Volumeninhalt = KHP.Volumen();
            KHP.Volumeninhalt = Volumeninhalt;
            double Masse = KHP.Massenberechnung();
            double Materialkosten = KHP.Materialkosten();
            double SchwerpunktXS = KHP.FlächenschwerpunktXS();
            double SchwerpunktYS = KHP.FlächenschwerpunktYS();
            double IXX = KHP.FlächenträgheitsmomentIXX();
            double IYY = KHP.FlächenträgheitsmomentIYY();
            tb_Querschnittsflaeche.Text = Convert.ToString((String.Format("{0:0.00}", Flächeninhalt / 100)) + " cm^2"); //Flächeninhalt umrechnung im cm^2
            tb_Volumen.Text = Convert.ToString((String.Format("{0:0.00}", Volumeninhalt / 1000000)) + " l");       //Querschnittsfläche umgerechnet in dm^3
            tb_Masse.Text = Convert.ToString((String.Format("{0:0.000}", Masse / 1000) + " kg")); //Masse in kg
            tb_Materialkosten.Text = Convert.ToString((String.Format("{0:0.00}", Materialkosten) + " €"));
            tb_Schwerpunktkoordinaten.Text = Convert.ToString("Xs/Ys     = " + SchwerpunktXS + " mm / " + SchwerpunktYS + " mm");
            tb_FTMX.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IXX / 10000) + " cm^4"));
            tb_FTMY.Text = Convert.ToString("=" + (String.Format(" {0:0.0}", IYY / 10000) + " cm^4"));
            tb_Durchmesser.Text = Convert.ToString(KHP.Durchmesser);
            tb_Laenge.Text = Convert.ToString(KHP.Länge);
            tb_Profildicke.Text = Convert.ToString(KHP.Profildicke);

        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            KHP.Materialint = 1; //S235 (Stahl)
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            KHP.Materialint = 2; //S355 (Stahl)
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            KHP.Materialint = 3; // EN AW-6060 (Aluminium)
        }

        private void ComboBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            KHP.Materialint = 4; // Aluminium 2
        }

        private void ComboBoxItem_Selected_4(object sender, RoutedEventArgs e)
        {
            KHP.Materialint = 5; // Messing
        }
       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            KHP.Durchmesser = KHP.ConvToNumber(tb_Durchmesser.Text);
            KHP.Länge = KHP.ConvToNumber(tb_Laenge.Text);
            KHP.Profildicke = KHP.ConvToNumber(tb_Profildicke.Text);

            try
            {
                CatiaKreishohl cc = new CatiaKreishohl();

                // Finde Catia Prozess
                if (cc.CATIALaeuft())
                {
                    // Öffne ein neues Part
                    cc.ErzeugePart();

                    // Erstelle eine Skizze
                    cc.ErstelleLeereSkizze();

                    // Generiere ein Profil
                    cc.ErzeugeProfil(KHP.Durchmesser, KHP.Durchmesser - KHP.Profildicke, 0);

                    // Extrudiere Balken
                    cc.ErzeugeBalken(KHP.Länge);

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
    class Kreishohl : Kreisprofile
    {
        public override double Flächenberechnung()
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = (Math.Pow((Durchmesser / 2), 2) - Math.Pow((Durchmesser/2)-Profildicke,2)) * Math.PI;
            return lkFlächeninhalt;
        }
        public override double FlächenträgheitsmomentIXX()
        {
            double lkIXX;
            lkIXX = (Math.PI / 64) * ((Math.Pow(Durchmesser, 4))- Math.Pow(Durchmesser-(2*Profildicke), 4));
            return lkIXX;
        }
        public override double FlächenträgheitsmomentIYY()
        {
            double lkIYY;
            lkIYY = (Math.PI / 64) * ((Math.Pow(Durchmesser, 4)) - Math.Pow(Durchmesser - (2 * Profildicke), 4));

            return lkIYY;
        }
       
    }
    class CatiaKreishohl : CatiaConnection
    {
        public override void ErzeugeProfil(Double D, Double d, Double p)
        {
            // Skizze umbenennen
            hsp_catiaProfil.set_Name("Kreishohlprofil");

            // Skizze oeffnen
            Factory2D catFactory2D1 = hsp_catiaProfil.OpenEdition();

            //Kreise erzeugen 
            catFactory2D1.CreateClosedCircle(0.000000, 0.000000, D / 2);
            catFactory2D1.CreateClosedCircle(0.000000, 0.000000, d / 2);

            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }
    }
}
