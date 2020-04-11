using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Allgemeine_Profilberechnungen
{
    class Program
    {
        static public void Main(string[] args)
        {
            int wiederholung;
            do
            {
                Console.WriteLine("Welches Profil möchten Sie berechnen ?");
                Console.WriteLine("");
                Console.WriteLine("1 = Rechteckprofil");
                Console.WriteLine("2 = Rechteckhohlprofil");
                Console.WriteLine("3 = Kreisprofil");
                Console.WriteLine("4 = Kreishohlprofil");

                int Auswahl;

                Auswahl = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                #region Rechteckeingabe 
                if (Auswahl == 1)
                {
                    double Breite, Höhe;
                    Console.WriteLine("Geben Sie die Breite des Rechteckes in mm an");
                    Breite = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Geben Sie die Höhe des Rechteckes in mm an ");
                    Höhe = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    //Berechnung des Flächeninhaltes 
                    double cc = Flächenberechnung(Breite, Höhe);

                    //Berechnung des Flächenschwerpunktes
                    string ff = Flächenschwerpunkt(Breite, Höhe);

                    //Berechnung des Flächenträgheitsmomentes IXX
                    string gg = FlächenträgheitsmomentIXX(Breite, Höhe);

                    //Berechnung des Flächenträgheitsmomentes IYY
                    string hh = FlächentregheitsmomentIYY(Breite, Höhe);

                    //Ausgabe 
                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("");
                    Console.WriteLine("Flächeninhalt = " + Convert.ToString(cc) + "mm^2"); // Ausgabe Flächeninhalt 
                    Console.WriteLine(ff); // Ausgabe Flächenschwerpunkt 
                    Console.WriteLine(gg); // Ausgabe IXX
                    Console.WriteLine(hh); // Ausgabe IYY 
                    Console.WriteLine("Flächentragheitsmoment IXY = 0 mm^4");

                    Console.ReadKey();

                }
                #endregion Rechteckeingabe
                #region Rechteckholprofileingabe 
                else if (Auswahl == 2)
                {
                    double Breite, Höhe, BreiteInnen, HöheInnen;
                    Console.WriteLine("Geben Sie die Breite des Rechteckes in mm an");
                    Breite = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Geben Sie die Höhe des Rechteckes in mm an ");
                    Höhe = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Geben Sie die Innenbreite des Rechteckes in mm an ");
                    BreiteInnen = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();


                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Breite Innen = " + BreiteInnen + "mm");
                    Console.WriteLine("Geben Sie die InnenHöhe des Rechteckes in mm an ");
                    HöheInnen = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    //Berechnung des Flächeninhaltes 
                    double cc = FlächenberechnungRHK(Breite, Höhe, BreiteInnen, HöheInnen);

                    //Berechnung des Flächenschwerpunktes
                    string ff = Flächenschwerpunkt(Breite, Höhe);

                    //Berechnung des Flächenträgheitsmomentes IXX
                    string gg = FlächenträgheitsmomentIXXRHK(Breite, Höhe, BreiteInnen, HöheInnen);

                    //Berechnung des Flächenträgheitsmomentes IYY
                    string hh = FlächentregheitsmomentIYYRHK(Breite, Höhe, BreiteInnen, HöheInnen);

                    //Ausgabe 
                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Breite Innen = " + BreiteInnen + "mm");
                    Console.WriteLine("Höhe Innen = " + HöheInnen + "mm");
                    Console.WriteLine("");
                    Console.WriteLine("Flächeninhalt = " + Convert.ToString(cc) + "mm^2"); // Ausgabe Flächeninhalt 
                    Console.WriteLine(ff); // Ausgabe Flächenschwerpunkt 
                    Console.WriteLine(gg); // Ausgabe IXX
                    Console.WriteLine(hh); // Ausgabe IYY 
                    Console.WriteLine("Flächentragheitsmoment IXY = 0 mm^4");

                    Console.ReadKey();

                }
                #endregion Rechteckholprofileingabe 
                #region Kreisprofileingabe 
                else if (Auswahl == 3)
                {
                    double Radius;
                    Console.WriteLine(" Geben Sie den Radius in mm ein ");
                    Radius = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    //Berechnung des Flächeninhaltes 
                    string cc = FlächenberechnungKP(Radius);

                    //Berechnung des Flächenträgheitsmomentes IXX, IYY
                    string gg = FlächenträgheitsmomentIXX_IYYKP(Radius);

                    //Ausgabe 
                    Console.WriteLine("Radius = " + Convert.ToString(Radius) + "mm");
                    Console.WriteLine("");
                    Console.WriteLine(cc); // Ausgabe Flächeninhalt 
                    Console.WriteLine(gg); // Ausgabe IXX IYY
                    Console.WriteLine("Flächentragheitsmoment IXY = 0 mm^4");

                    Console.ReadKey();

                }
                #endregion Kreisprofileingabe
                #region Kreihohlprofileingabe 
                else if (Auswahl == 4)
                {
                    // Luca 
                }
                #endregion Kreisprofileingabe 
                else
                {
                    Console.WriteLine("Fehler, Auswahl nicht eindeutig");
                    Console.ReadKey();
                }
                Console.Clear();
                Console.WriteLine("Möchten sie erneut berechenen?");
                Console.WriteLine("1 = Ja");
                Console.WriteLine("2 = Nein");
                wiederholung = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            } while (wiederholung == 1);

        }
        #region Rechteckprofil
        static double Flächenberechnung(double Breite, double Höhe)
        {
            double cclokal;
            cclokal = Breite * Höhe;

            return cclokal;
        }

        static string Flächenschwerpunkt(double Breite, double Höhe)
        {
            double dd, ee;

            dd = Breite / 2;
            ee = Höhe / 2;

            string ff;

            ff = "Schwerpunktkoordinaten (x:" + Convert.ToString(dd) + " y:" + Convert.ToString(ee) + ")";

            return ff;
        }

        static string FlächenträgheitsmomentIXX(double Breite, double Höhe)
        {
            double ix;
            ix = (Breite * Math.Pow(Höhe, 3)) / 12;

            string fflk;
            fflk = String.Format("Flächenträgheitsmoment IXX = {0:00.000}", ix) + "mm^4";

            return fflk;
        }

        static string FlächentregheitsmomentIYY(double Breite, double Höhe)
        {
            double iy;
            iy = (Math.Pow(Breite, 3) * Höhe) / 12;

            string fflk;

            fflk = String.Format("Flächenträgheitsmoment IYY = {0:00.000}", iy) + "mm^4";

            return fflk;
        }
        #endregion Rechteckprofil
        #region Rechteckhohlprofil
        static double FlächenberechnungRHK(double Breite, double Höhe, double BreiteInnen, double HöheInnen)
        {
            double cclokal;
            cclokal = Breite * Höhe - BreiteInnen * HöheInnen;

            return cclokal;
        }

        static string FlächenträgheitsmomentIXXRHK(double Breite, double Höhe, double BreiteInnen, double HöheInnen)
        {
            double ix;
            ix = (Breite * Math.Pow(Höhe, 3) - BreiteInnen * Math.Pow(HöheInnen, 3)) / 12;

            string fflk;
            fflk = String.Format("Flächenträgheitsmoment IXX = {0:00.000}", ix) + "mm^4";

            return fflk;
        }

        static string FlächentregheitsmomentIYYRHK(double Breite, double Höhe, double BreiteInnen, double HöheInnen)
        {
            double iy;
            iy = (Breite * Breite * Breite * Höhe - BreiteInnen * BreiteInnen * BreiteInnen * HöheInnen) / 12;

            string fflk;

            fflk = String.Format("Flächenträgheitsmoment IYY = {0:00.000}", iy) + "mm^4";

            return fflk;
        }
        #endregion Rechteckhohlprofil
        #region Kreis
        static string FlächenberechnungKP(double Radius)
        {
            double cclokal;

            cclokal = Math.Pow(Radius, 2) * Math.PI;

            string aalokal;

            aalokal = String.Format("Flächeninhalt = {0:00.000}", cclokal) + "mm^2";


            return aalokal;

        }

        static string FlächenträgheitsmomentIXX_IYYKP(double Radius)
        {
            double i;
            i = (Math.PI * Math.Pow(Radius, 4)) / 12;

            string fflk;
            fflk = String.Format("Flächenträgheitsmoment IXX = IYY = {0:00.000}", i) + "mm^4";

            return fflk;
        }
        #endregion Kreis
        #region Kreisprofil
        //Luca 
        #endregion Kreisprofil

    }

}