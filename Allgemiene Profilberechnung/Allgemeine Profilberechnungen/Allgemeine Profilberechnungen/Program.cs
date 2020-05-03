using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Allgemeine_Profilberechnungen
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Variablen 
            int Auswahl;
            int Material;

            double Breite;
            double Höhe;
            double Länge;
            double Volumen;
            double Masse;
            double Flächeninhalt =0;
            double FlächenschwerpunktXS=0;
            double FlächenschwerpunktYS=0;
            double IXX=0;
            double IYY=0;
            double IXY=0;
            string Materialnahme; 
              

            MessageBoxResult wiederholung;
            do
            {
                Console.WriteLine("Welches Profil möchten Sie berechnen ?");
                Console.WriteLine("");
                Console.WriteLine("1 = Rechteckprofil");
                Console.WriteLine("2 = Rechteckhohlprofil");
                Console.WriteLine("3 = Kreisprofil");
                Console.WriteLine("4 = Kreishohlprofil");
                Console.WriteLine("5 = T-Profil");
                Console.WriteLine("6 = Doppel-T-Profil");
                Console.WriteLine("7 = U-Profil");
                Console.WriteLine("8 = L-Profil");

                Auswahl = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                if (Auswahl >= 9)
                {
                    Console.WriteLine("Fehler, Auswahl nicht eindeutig");
                    Console.ReadKey();
                } //Plausibilitätsprüfung

                //Eingabe Länge 
                Console.WriteLine("Geben Sie die Länge des Profils in mm an ");
                Länge = Convert.ToDouble(Console.ReadLine());
                Console.Clear();

                //Eingabe Material 
                Console.WriteLine("Aus welchem Material ist das Profil? ");
                Console.WriteLine("1 = Stahl ");
                Console.WriteLine("2 = Aluminium ");
                Material = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                #region Rechteck 
                if (Auswahl == 1)
                {
                    #region Eingabe 

                    Console.WriteLine("Geben Sie die Breite des Rechteckes in mm an");
                    Breite = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Geben Sie die Höhe des Rechteckes in mm an ");
                    Höhe = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    #endregion Eingabe 

                    //Berechnung des Flächeninhaltes 
                    Flächeninhalt = Flächenberechnung(Breite, Höhe);

                    //Berechnung des Flächenschwerpunktes XS
                    FlächenschwerpunktXS = FlächenschwerpunktRP(Breite);

                    //Berechnung des Flächenschwerpunktes YS
                    FlächenschwerpunktYS = FlächenschwerpunktRP(Höhe);

                    //Berechnung des Flächenträgheitsmomentes IXX
                    IXX = FlächenträgheitsmomentIXX(Breite, Höhe);

                    //Berechnung des Flächenträgheitsmomentes IYY
                    IYY = FlächentregheitsmomentIYY(Breite, Höhe);

                    //Berechnung des Flächenträgheitsmomentes IXY
                    IXY = 0;

                    //Ausgabe 
                    Console.WriteLine("Rechteckprofil : ");
                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Länge  = " + Convert.ToString(Länge) + "mm");


                }
                #endregion Rechteck
                #region Rechteckholprofil
                else if (Auswahl == 2)
                {
                    #region Eingabe
                    double BreiteInnen, HöheInnen;
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
                    #endregion Eingabe

                    // Schleife zur Überprüfung auf Glaubwürdigkeit 

                    if (BreiteInnen > Breite && HöheInnen > Höhe)
                    {
                        Console.WriteLine("Error: Außenabmessungen müssen größer als die Innenabmessungen sein");
                    }

                    else
                    {
                        //Berechnung des Flächeninhaltes 
                        Flächeninhalt = FlächenberechnungRHK(Breite, Höhe, BreiteInnen, HöheInnen);

                        //Berechnung des Flächenschwerpunktes XS
                        FlächenschwerpunktXS = FlächenschwerpunktRP(Breite);

                        //Berechnung des Flächenschwerpunktes YS
                        FlächenschwerpunktYS = FlächenschwerpunktRP(Höhe);

                        //Berechnung des Flächenträgheitsmomentes IXX
                        IXX = FlächenträgheitsmomentIXXRHP(Breite, Höhe, BreiteInnen, HöheInnen);

                        //Berechnung des Flächenträgheitsmomentes IYY
                        IYY = FlächentregheitsmomentIYYRHP(Breite, Höhe, BreiteInnen, HöheInnen);

                        //Berechnung des Flächenträgheitsmomentes IXY
                        IXY = 0;

                        //Ausgabe 
                        Console.WriteLine("Rechteckholprofil : ");
                        Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                        Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                        Console.WriteLine("Breite Innen = " + BreiteInnen + "mm");
                        Console.WriteLine("Höhe Innen = " + HöheInnen + "mm");
                    }
                }
                #endregion Rechteckholprofil 
                #region Kreisprofil
                else if (Auswahl == 3)
                {
                    #region Eingabe Kreisprofil
                    Console.WriteLine(" Geben Sie den Außendurchmesser in mm ein ");
                    double Außendurchmesser = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    #endregion Eingabe Kreisprofil

                    //Berechnung des Flächeninhaltes 
                    Flächeninhalt = FlächenberechnungKP(Außendurchmesser);

                    //Berechnung Flächenschwerpunkt XS
                    FlächenschwerpunktXS = FlächenschwerpunktRP(Außendurchmesser);
                    //Berechnung Flächenschwerpunkt YS
                    FlächenschwerpunktYS = FlächenschwerpunktRP(Außendurchmesser);
                    //Berechnung Flächenträgheitsmoment IXX
                    IXX = FlächenträgheitsmomentKP(Außendurchmesser);
                    //Berechnung Flächenträgheitsmoment IYY
                    IYY = FlächenträgheitsmomentKP(Außendurchmesser);
                    //Berechnung Flächenträgheitsmoment IXY
                    IXY = 0;

                    //Ausgabe 
                    Console.WriteLine("Kreiprofil : ");
                    Console.WriteLine("Außendurchmesser = " + Convert.ToString(Außendurchmesser) + "mm");
                    Console.WriteLine("Länge = " + Convert.ToString(Länge) + "mm^2");


                }
                #endregion Kreisprofil
                #region Kreihohlprofil
                else if (Auswahl == 4)
                {
                    #region Eingabe
                    //Eingabe Außendurchmesser
                    Console.WriteLine("Geben Sie den Außendurchmesser in mm ein");
                    double Außendurchmesser = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    //Eingabe Innendurchmesser
                    Console.WriteLine("Außendurchmesser" + Convert.ToDouble(Außendurchmesser) + "mm");
                    Console.WriteLine("Geben sie den Innendurchmesser in mm ein");
                    double Innendurchmesser = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();
                    #endregion Eingabe
                    if (Innendurchmesser > Außendurchmesser)
                    {
                        Console.WriteLine("Error: Außendurchmessser muss kleiner Innendurchmesser ");
                    }

                    else
                    {
                        //Berrechnung Flächeninhalt
                        Flächeninhalt = FlächenberechnungKHP(Außendurchmesser, Innendurchmesser);
                        //Berechnung Flächenschwerpunkt XS
                        FlächenschwerpunktXS = FlächenschwerpunktRP(Außendurchmesser);
                        //Berechnung Flächenschwerpunkt YS
                        FlächenschwerpunktYS = FlächenschwerpunktRP(Außendurchmesser);
                        //Berechnung Flächenträgheitsmoment IXX
                        IXX = FlächenträgheitsmomentKHP(Außendurchmesser, Innendurchmesser);
                        //Berechnung Flächenträgheitsmoment IYY
                        IYY = FlächenträgheitsmomentKHP(Außendurchmesser, Innendurchmesser);
                        //Berechnung Flächenträgheitsmoment IXY
                        IXY = 0;

                        //Ausgabe
                        Console.WriteLine("Kreihohlprofil : ");
                        Console.WriteLine("Außendurchmesser " + Convert.ToDouble(Außendurchmesser) + " mm ");
                        Console.WriteLine("Innendurchmesser " + Convert.ToDouble(Innendurchmesser) + " mm ");
                        Console.WriteLine("Länge des Profils " + Convert.ToDouble(Länge) + " mm ");
                    }
                }
                #endregion Kreisprofil 
                #region T-Profi 
                else if (Auswahl == 5)
                {
                    #region Eingabe
                    double Stegdicke, Flanschdicke;
                    Console.WriteLine("Geben Sie die Breite des Doppel-T-Profils in mm an");
                    Breite = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Geben Sie die Höhe des Doppel-T-Profils in mm an ");
                    Höhe = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Geben Sie die Stegdicke des Doppel-T-Profils in mm an ");
                    Stegdicke = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Stegdicke = " + Stegdicke + "mm");
                    Console.WriteLine("Geben Sie die Flanschdicke des Doppel-T-Profils in mm an ");
                    Flanschdicke = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();
                    #endregion Eingabe

                    //Berechnung des Flächeninhaltes 
                    Flächeninhalt = FlächenberechnungT(Breite, Höhe, Stegdicke, Flanschdicke);

                    //Berechnung des Flächenschwerpunktes XS
                    FlächenschwerpunktXS = FlächenschwerpunktRP(Breite);

                    //Berechnung des Flächenschwerpunktes YS
                    FlächenschwerpunktYS = FlächenschwerpunktYST(Höhe, Flanschdicke, Breite, Stegdicke);

                    //Berechnung des Flächenträgheitsmomentes IXX
                    IXX = FlächenträgheitsmomentIXXT(Höhe, Flanschdicke, Breite, Stegdicke);

                    //Berechnung des Flächenträgheitsmomentes IYY
                    IYY = FlächentregheitsmomentIYYT(Höhe, Flanschdicke, Breite, Stegdicke);

                    //Berechnung des Flächenträgheitsmomentes IYY
                    IXY = 0;

                    //Ausgabe 
                    Console.WriteLine("T-Profil : ");
                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Stegdicke = " + Stegdicke + "mm");
                    Console.WriteLine("Flanschdicke = " + Flanschdicke + "mm");
                }
                #endregion T-Profil 
                #region Doppel-T-Profil
                else if (Auswahl == 6)
                {
                    #region Eingabe
                    double Stegdicke, Flanschdicke;
                    Console.WriteLine("Geben Sie die Breite des Doppel-T-Profils in mm an");
                    Breite = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Geben Sie die Höhe des Doppel-T-Profils in mm an ");
                    Höhe = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Geben Sie die Stegdicke des Doppel-T-Profils in mm an ");
                    Stegdicke = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();


                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Stegdicke = " + Stegdicke + "mm");
                    Console.WriteLine("Geben Sie die Flanschdicke des Doppel-T-Profils in mm an ");
                    Flanschdicke = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();


                    #endregion Eingabe

                    // Schleife zur Überprüfung auf Glaubwürdigkeit
                    if (Stegdicke > Breite && Flanschdicke * 2 > Höhe)
                    {
                        Console.WriteLine("Error: Außenabmessungen müssen größer als die Innenabmessungen sein");
                    }

                    else
                    {
                        //Berechnung des Flächeninhaltes 
                        Flächeninhalt = FlächenberechnungDoppelT(Breite, Höhe, Stegdicke, Flanschdicke);

                        //Berechnung des Flächenschwerpunktes XS
                        FlächenschwerpunktXS = FlächenschwerpunktRP(Breite);

                        //Berechnung des Flächenschwerpunktes YS
                        FlächenschwerpunktYS = FlächenschwerpunktRP(Höhe);

                        //Berechnung des Flächenträgheitsmomentes IXX
                        IXX = FlächenträgheitsmomentIXXDoppelT(Breite, Höhe, Stegdicke, Flanschdicke);

                        //Berechnung des Flächenträgheitsmomentes IYY
                        IYY = FlächentregheitsmomentIYYDoppelT(Breite, Höhe, Stegdicke, Flanschdicke);

                        //Berechnung des Flächenträgheitsmomentes IXY
                        IXY = 0;

                        //Ausgabe 
                        Console.WriteLine("Doppel T-Profil : ");
                        Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                        Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                        Console.WriteLine("Stegdicke = " + Stegdicke + "mm");
                        Console.WriteLine("Flanschdicke = " + Flanschdicke + "mm");
                    }


                }
                #endregion Doppel-T-Profil
                #region U-Profil
                else if (Auswahl == 7)
                {
                    #region Eingabe

                    double Profildicke;
                    Console.WriteLine("Geben Sie die Breite des U-Profils in mm an");
                    Breite = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Geben Sie die Höhe des U-Profils in mm an ");
                    Höhe = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Geben Sie die Profildicke in mm an ");
                    Profildicke = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    #endregion Eingabe
                    //Berechnung des Flächeninhaltes 
                    Flächeninhalt = FlächenberechnungUP(Breite, Höhe, Profildicke);

                    //Berechnung des Flächenschwerpunktes XS
                    FlächenschwerpunktXS = FlächenschwerpunktXSUP(Breite, Höhe, Profildicke);

                    //Berechnung des Flächenschwerpunktes YS
                    FlächenschwerpunktYS = FlächenschwerpunktRP(Höhe);

                    //Berechnung des Flächenträgheitsmomentes IXX
                    IXX = FlächenträgheitsmomentIXXUP(Breite, Höhe, Profildicke);

                    //Berechnung des Flächenträgheitsmomentes IYY
                    IYY = FlächenträgheitsmomentIYYUP(Breite, Höhe, Profildicke);

                    //Berechnung des Flächenträgheitsmomentes IXY
                    IXY = 0;

                    //Ausgabe 
                    Console.WriteLine("U-Profil : ");
                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Profildicke = " + Profildicke + "mm");

                }
                #endregion U-Profil
                #region L-Profil
                else if (Auswahl == 8)
                {
                    #region Eingabe
                    double Profildicke;
                    Console.WriteLine("Geben Sie die Breite ihres L-Profils in mm an");
                    Breite = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite =" + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Geben Sie die Höhe ihres L-Profils in mm an");
                    Höhe = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite =" + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe =" + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Geben Sie die Profildicke in mm an");
                    Profildicke = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    #endregion Eingabe
                    //Berechnung des Flächeninhaltes
                    Flächeninhalt = FlächenberechnungLP(Breite, Höhe, Profildicke);

                    //Berechnung Flächenschwerpunkt XS
                    FlächenschwerpunktXS = FlächenschwerpunktXSLP(Breite, Höhe, Profildicke);

                    //Berechnung Flächenschwerpunkt YS
                    FlächenschwerpunktYS = FlächenschwerpunktYSLP(Breite, Höhe, Profildicke);

                    //Berechnung Flächenträgheitsmoment IXX
                    IXX = FlächenträgheitsmomentIXXLP(Breite, Höhe, Profildicke);

                    //Berechnung Flächenträgheitsmoment IYY
                    IYY = FlächenträgheitsmomentIYYLP(Breite, Höhe, Profildicke);

                    //Berechnung Flächenträgheitsmoment IXY
                    IXY = 0;

                    //Ausgabe
                    Console.WriteLine("L-Profil : ");
                    Console.WriteLine("Breite =" + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe =" + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Profildicke = " + Profildicke + "mm");
                }
                    #endregion L-Profil
                
                #region Berechnung_Volumen/Masse

                //Berechnung des Volumens:
                Volumen = Volumenberechnung(Flächeninhalt, Länge);

                //Materialnahme
                Materialnahme = Namensdeklaration(Material);

                //Berechnung Material 
                Masse = Massenberechnung(Material, Volumen);
                #endregion Berechnung_Volumen/Masse
                #region Allgemeine_Ausgabe
                Console.WriteLine("Material: "+Materialnahme);
                Console.WriteLine("");
                Console.WriteLine("Flächeninhalt des Querschnittes = " + Convert.ToString(Flächeninhalt) + "mm^2"); // Ausgabe Flächeninhalt 
                Console.WriteLine("Flächenschwerpunkt:          XS = "+FlächenschwerpunktXS+"/YS = "+FlächenschwerpunktYS); // Ausgabe Flächenschwerpunkt 
                Console.WriteLine(String.Format("Flächenträgheitsmoment      IXX = {0:00}", IXX) + "mm^4"); // Ausgabe IXX
                Console.WriteLine(String.Format("Flächenträgheitsmoment      IYY = {0:00}", IYY) + "mm^4"); // Ausgabe IYY 
                Console.WriteLine(String.Format("Flächenträgheitsmoment      IXY = {0:00}", IXY) + "mm^4"); // Ausgabe IXY
                Console.WriteLine(String.Format("Volumen                         = {0:00.000}", Volumen / 1000000) + "Liter");
                Console.WriteLine(String.Format("Masse                           = {0:00.000}", Masse / 1000) + "kg");

                Console.ReadKey();

                wiederholung = MessageBox.Show("Weitere Berechnung? ", "Wichtige Frage",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
                Console.Clear();

                #endregion Allgemeine_Ausgabe 

            } while (wiederholung == MessageBoxResult.Yes);
        }
    #region Unterprogramme 
        #region UP_Allgemein
        static double Volumenberechnung(double lkFläche, double lkLänge)
        {
            double lkVolumen;
            lkVolumen = (lkFläche * lkLänge);
            return lkVolumen;//mm^3
        }
        
        static string Namensdeklaration(int lkMaterial)
        { 
            string lkName="0";
            if (lkMaterial == 1)//Stahl 
            {
                lkName= "Stahl" ;

            }
            else if (lkMaterial == 2)//Aluminium 
            {
                lkName = "Aluminium ";
            }

            return lkName;
        }
        static double Massenberechnung(int lkMaterial, double lkVolumen)
        {
            double lkDichte = 0;
            double lkMasse;
            if (lkMaterial == 1)//Stahl 
            {
                lkDichte = 7.85;
                
            }
            else if (lkMaterial == 2)//Aluminium 
            {
                lkDichte = 2.7;
            }
            else
            {
                Console.WriteLine("Eingabe nicht korrekt");
            }

            lkMasse = (lkDichte * lkVolumen) / 1000;

            return lkMasse;//Gramm  
        }
        #endregion UP_Allgemein 
        #region UP_Rechteckprofil
        static double Flächenberechnung(double lkBreite, double lkHöhe)
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = lkBreite * lkHöhe;

            return lkFlächeninhalt;
        }

        static double FlächenschwerpunktRP(double lkBreite)
        {
            
            double lkhalbeBreite = lkBreite / 2;
                       
            return lkhalbeBreite;
        }
        

        static double FlächenträgheitsmomentIXX(double lkBreite, double lkHöhe)
        {
            double lkIXX;
            lkIXX = (lkBreite * Math.Pow(lkHöhe, 3)) / 12;

            return lkIXX;
        }

        static double FlächentregheitsmomentIYY(double lkBreite, double lkHöhe)
        {
            double lkIYY;
            lkIYY = (Math.Pow(lkBreite, 3) * lkHöhe) / 12;

            return lkIYY;
        }

        #endregion UP_Rechteckprofil
        #region UP_Rechteckhohlprofil
        static double FlächenberechnungRHK(double lkBreite, double lkHöhe, double lkBreiteInnen, double lkHöheInnen)
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = lkBreite * lkHöhe - lkBreiteInnen * lkHöheInnen;

            return lkFlächeninhalt;
        }

        static double FlächenträgheitsmomentIXXRHP(double lkBreite, double lkHöhe, double lkBreiteInnen, double lkHöheInnen)
        {
            double lkIXX;
            lkIXX = (lkBreite * Math.Pow(lkHöhe, 3) - lkBreiteInnen * Math.Pow(lkHöheInnen, 3)) / 12;

            
            return lkIXX;
        }

        static double FlächentregheitsmomentIYYRHP(double lkBreite, double lkHöhe, double lkBreiteInnen, double lkHöheInnen)
        {
            double lkIYY;
            lkIYY = (lkHöhe * Math.Pow(lkBreite, 3) - lkHöheInnen * Math.Pow(lkBreiteInnen, 3)) / 12;
            
            return lkIYY;
        }

        #endregion UP_Rechteckhohlprofil
        #region UP_Kreisprofil
        static double FlächenberechnungKP(double lkAußendurchmesser)
        {
            double lkFlächeninhalt= Math.Pow((lkAußendurchmesser/2), 2) * Math.PI;

            return lkFlächeninhalt;

        }

        static double FlächenträgheitsmomentKP(double lkAußendurchmesser)
        {
            
            double kIXXIYY= (Math.PI * Math.Pow((lkAußendurchmesser/2), 4)) / 12;

            return kIXXIYY;
        }
        #endregion UP_Kreisprofil
        #region UP_Kreishohlprofil
        static double FlächenberechnungKHP(double lkAußendurchmesser, double lkInnendurchmesser)
        {
            double lkFlächeninhalt ;
            lkFlächeninhalt = (Math.PI / 4) * (lkAußendurchmesser * lkAußendurchmesser - lkInnendurchmesser * lkInnendurchmesser);

            return lkFlächeninhalt;

        }
        static double FlächenträgheitsmomentKHP(double lkAußendurchmesser, double lkInnendurchmesser)
        {
            double lkIXXIYY = (Math.PI / 64) * ((Math.Pow(lkAußendurchmesser, 4)) - Math.Pow(lkInnendurchmesser, 4));
            return lkIXXIYY;
        }

        #endregion UP_Kreishohlprofil
        #region UP_T-Profil
        static double FlächenberechnungT(double lkBreite, double lkHöhe, double lkStegdicke, double lkFlanschdicke)
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = lkBreite * lkFlanschdicke + (lkHöhe - lkFlanschdicke) * lkStegdicke;

            return lkFlächeninhalt;
        }
        static double FlächenschwerpunktYST(double lkHöhe, double lkFlanschdicke, double lkBreite, double lkStegdicke)
        {
            double lkYS;
            lkYS = ((lkHöhe * lkBreite * lkBreite / 2) + (lkHöhe - lkFlanschdicke) * (lkBreite - lkStegdicke) * ((lkBreite - lkStegdicke) / 2)) / (lkBreite * lkHöhe - (lkBreite - lkStegdicke) * (lkHöhe - lkFlanschdicke));

            return lkYS;
        }
        static double FlächenträgheitsmomentIXXT(double lkHöhe, double lkFlanschdicke, double lkBreite, double lkStegdicke)
        {
            double lkIXX = ((lkBreite * Math.Pow(lkFlanschdicke, 3)/12)+ (lkStegdicke* Math.Pow((lkHöhe - lkFlanschdicke), 3)) / 12);

            return lkIXX;
        }

        static double FlächentregheitsmomentIYYT(double lkHöhe, double lkFlanschdicke, double lkBreite, double lkStegdicke)
        {
            double lkIYY = ((lkFlanschdicke* Math.Pow(lkBreite, 3))/12 + ((lkHöhe-lkFlanschdicke) * Math.Pow(lkStegdicke,3))/ 12);

            return lkIYY;

        }
        #endregion UP_T-Profil
        #region UP_Doppel-T-Profil
            static double FlächenberechnungDoppelT(double lkBreite, double lkHöhe, double lkStegdicke, double lkFlanschdicke)
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = lkBreite * lkHöhe - (lkBreite - lkStegdicke) * (lkHöhe - 2 * lkFlanschdicke);

            return lkFlächeninhalt;
        }

        static double FlächenträgheitsmomentIXXDoppelT(double lkBreite, double lkHöhe, double lkStegdicke, double lkFlanschdicke)
        {
            double lkIXX = (lkBreite * Math.Pow(lkHöhe, 3) - (lkBreite - lkStegdicke) * Math.Pow((lkHöhe - 2 * lkFlanschdicke), 3)) / 12;

             return lkIXX;
        }

        static double FlächentregheitsmomentIYYDoppelT(double lkBreite, double lkHöhe, double lkStegdicke, double lkFlanschdicke)
        {
            double lkIYY = ((lkHöhe - (lkHöhe - 2 * lkFlanschdicke)) * Math.Pow(lkBreite, 3) + (lkHöhe - 2 * lkFlanschdicke) * Math.Pow(lkBreite - (lkBreite - lkStegdicke), 3)) / 12;

             return lkIYY;
        }

        #endregion UP_Doppel-T-Profil
        #region UP_U-Profil
        static double FlächenberechnungUP(double lkBreite, double lkHöhe, double lkProfildicke)
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = lkBreite * lkHöhe - (lkBreite - lkProfildicke) * (lkHöhe - 2 * lkProfildicke);

            return lkFlächeninhalt;
        }
        static double FlächenschwerpunktXSUP(double lkBreite, double lkHöhe, double lkProfildicke)
        {
            double lkxs;
            lkxs = (lkHöhe* lkBreite * lkBreite / 2 - (lkHöhe - 2 * lkProfildicke) * (lkBreite - lkProfildicke) * (lkBreite - lkProfildicke) / 2) / (lkHöhe* lkBreite - (lkHöhe - 2 * lkProfildicke) * (lkBreite - lkProfildicke));

            return lkxs;
        }
       static double FlächenträgheitsmomentIXXUP(double lkBreite, double lkHöhe, double lkProfildicke)
        {
            double lkIXX;
            lkIXX = (lkBreite * Math.Pow(lkHöhe, 3) / 12) - ((lkBreite - lkProfildicke) * Math.Pow((lkHöhe - 2 * lkProfildicke), 3) / 12);

            return lkIXX;
        }
        static double FlächenträgheitsmomentIYYUP(double lkBreite, double lkHöhe, double lkProfildicke)
        {
            double lkIYY;
            lkIYY = (lkHöhe * Math.Pow(lkBreite, 3) / 12) - ((lkHöhe - 2 * lkProfildicke) * Math.Pow(lkBreite - lkProfildicke, 3) / 12);

            return lkIYY;
        }
        #endregion UP_U-Profil
        #region UP_L-Profil
        static double FlächenberechnungLP(double lkBreite, double lkHöhe, double lkProfildicke)
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = lkBreite + lkHöhe - (lkBreite - lkProfildicke) * (lkHöhe - lkProfildicke);

            return lkFlächeninhalt;
        }
        static double FlächenschwerpunktXSLP(double lkBreite, double lkHöhe,double lkProfildicke)
        {
            double lkxs, lka1, lka2;
            lka1 = lkHöhe * lkProfildicke;
            lka2 = (lkBreite - lkProfildicke) * lkProfildicke;
            lkxs = (lka1 * (lkProfildicke / 2) + lka2 * (lkBreite/2)) / (lka1 + lka2);

            return lkxs;
        }
        static double FlächenschwerpunktYSLP(double lkBreite, double lkHöhe, double lkProfildicke)
        {
            double lkys, lka1, lka2;
            lka1 = lkHöhe * lkProfildicke;
            lka2 = (lkBreite - lkProfildicke) * lkProfildicke;
            lkys = (lka1 * (lkHöhe / 2) + lka2 * (lkProfildicke / 2))/(lka1 + lka2);

            return lkys;
        }
        static double FlächenträgheitsmomentIXXLP(double lkBreite, double lkHöhe, double lkProfildicke)
        {
            double lkFlächenträgheitsmomentIXX, e2;
            e2 = 1 / 2 * (lkProfildicke * Math.Pow(lkHöhe, 2) + (lkBreite - lkProfildicke) * Math.Pow(lkProfildicke, 2) / (lkProfildicke * lkHöhe + (lkBreite - lkProfildicke) * lkProfildicke));
            lkFlächenträgheitsmomentIXX = (((lkProfildicke * Math.Pow(lkHöhe, 3) + (lkBreite - lkProfildicke) * Math.Pow(lkProfildicke, 3)) / 3) - (lkProfildicke * lkHöhe + (lkBreite - lkProfildicke) * lkProfildicke) * Math.Pow(e2, 2));

            return lkFlächenträgheitsmomentIXX;
        }

        static double FlächenträgheitsmomentIYYLP(double lkBreite, double lkHöhe, double lkProfildicke)
        {
            double lkFlächenträgheitsmomentIYY, e3;
            e3 = 1 / 2 * ((lkProfildicke * Math.Pow(lkBreite, 2) + (lkHöhe - lkProfildicke) * Math.Pow(lkProfildicke, 2)) / (lkProfildicke * lkBreite + (lkHöhe - lkProfildicke) * lkProfildicke));
            lkFlächenträgheitsmomentIYY = ((lkProfildicke*Math.Pow(lkBreite, 3) + (lkHöhe-lkProfildicke)*Math.Pow(lkProfildicke, 2))/3) - (lkProfildicke*lkBreite + (lkHöhe-lkProfildicke)*lkProfildicke)*Math.Pow(e3, 2);

            return lkFlächenträgheitsmomentIYY;
        }
        #endregion UP_L-Profil
    }
    #endregion Unterprogramme 

}