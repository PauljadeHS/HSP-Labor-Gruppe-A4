using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Allgemeine_Profilberechnungen
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Variablen 
            int Auswahl;

            double Material;
            double Breite;
            double Höhe;
            double Länge;
            double Volumen;
            double Masse;
            double Flächeninhalt = 0;

            MessageBoxResult wiederholung;
            do
            {
                Console.WriteLine("Welches Profil möchten Sie berechnen ?");
                Console.WriteLine("");
                Console.WriteLine("1 = Rechteckprofil");
                Console.WriteLine("2 = Rechteckhohlprofil");
                Console.WriteLine("3 = Kreisprofil");
                Console.WriteLine("4 = Kreishohlprofil");
                Console.WriteLine("5 = Doppel-T-Profil");

                Auswahl = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                
                if (Auswahl >= 9 ) 
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
                Material = Convert.ToDouble(Console.ReadLine());
                Console.Clear();

                #region Rechteckeingabe 
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
                    Flächeninhalt  = Flächenberechnung(Breite, Höhe);

                    //Berechnung des Flächenschwerpunktes
                    string Flächenschwerp = Flächenschwerpunkt(Breite, Höhe);

                    //Berechnung des Flächenträgheitsmomentes IXX
                    double IXX = FlächenträgheitsmomentIXX(Breite, Höhe);

                    //Berechnung des Flächenträgheitsmomentes IYY
                    double IYY = FlächentregheitsmomentIYY(Breite, Höhe);

                    //Ausgabe 
                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Länge  = " + Convert.ToString(Länge) + "mm");
                    Console.WriteLine("");
                    Console.WriteLine("Flächeninhalt des Querschnittes = " + Convert.ToString(Flächeninhalt) + "mm^2"); // Ausgabe Flächeninhalt 
                    Console.WriteLine(Flächenschwerp); // Ausgabe Flächenschwerpunkt 
                    Console.WriteLine(String.Format("Flächenträgheitsmoment IXX = {0:00}", IXX) + "mm^4"); // Ausgabe IXX
                    Console.WriteLine(String.Format("Flächenträgheitsmoment IXX = {0:00}", IYY) + "mm^4"); // Ausgabe IYY 
                    Console.WriteLine("Flächentragheitsmoment IXY = 0 mm^4");
                    
                }
                #endregion Rechteckeingabe
                #region Rechteckholprofileingabe 
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
                        Console.WriteLine("Flächeninhalt = " + Convert.ToString(Flächeninhalt) + "mm^2"); // Ausgabe Flächeninhalt 
                        Console.WriteLine(ff); // Ausgabe Flächenschwerpunkt 
                        Console.WriteLine(gg); // Ausgabe IXX
                        Console.WriteLine(hh); // Ausgabe IYY 
                        Console.WriteLine("Flächentragheitsmoment IXY = 0 mm^4");
                        Console.WriteLine("Volumen = " + Volumen + "mm^3");
                        Console.WriteLine("Masse : " + Masse + " Gramm");
                    }



                    Console.ReadKey();

                }
                #endregion Rechteckholprofileingabe 
                #region Kreisprofileingabe 
                else if (Auswahl == 3)
                {
                    #region Eingabe Kreisprofil
                    Console.WriteLine(" Geben Sie den Radius in mm ein ");
                    double Radius = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Radius = " + Radius);
                    Console.WriteLine("Geben Sie die Länge des Profils in mm an ");
                    Länge = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Radius = " + Radius);
                    Console.WriteLine("Länge = " + Länge);
                    Console.WriteLine("");
                    Console.WriteLine("Aus welchem Material ist das Profil? ");
                    Console.WriteLine("1 = Stahl ");
                    Console.WriteLine("2 = Aluminium ");
                    Material = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    #endregion Eingabe Kreisprofil

                    //Berechnung des Flächeninhaltes 
                    double Fläche = FlächenberechnungKP(Radius);

                    //Berechnung des Flächenträgheitsmomentes IXX, IYY
                    string FTM = FlächenträgheitsmomentIXX_IYYKP(Radius);

                    //Berechnung des Volumen
                    double VLM = Volumenberechnung(Fläche, Länge);

                    //Berechnung der Masse
                    Masse = Massenberechnung(Material, VLM);

                    //Ausgabe 
                    Console.WriteLine("Radius = " + Convert.ToString(Radius) + "mm");
                    Console.WriteLine("Länge = " + Convert.ToString(Länge) + "mm^2");
                    Console.WriteLine("Material = " + Convert.ToString(Material));
                    Console.WriteLine("");
                    Console.WriteLine(String.Format("Flächeninhalt = {0:00.000}", Fläche) + "mm^2"); // Ausgabe Flächeninhalt 
                    Console.WriteLine(FTM); // Ausgabe IXX IYY 
                    Console.WriteLine("Flächentragheitsmoment IXY = 0 mm^4");
                    Console.WriteLine(String.Format("Volumen = {0:00}", VLM) + "mm^3");
                    Console.WriteLine(String.Format("Masse = {0:00}", Masse) + "Gramm");



                    Console.ReadKey();

                }
                #endregion Kreisprofileingabe
                #region Kreihohlprofileingabe 
                else if (Auswahl == 4)
                {
                    double Außendurchmesser, Innendurchmesser, Laenge;
                    //Eingabe Außendurchmesser
                    Console.WriteLine("Geben Sie den Außendurchmesser in mm ein");
                    Außendurchmesser = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    //Eingabe Innendurchmesser
                    Console.WriteLine("Außendurchmesser" + Convert.ToDouble(Außendurchmesser) + "mm");
                    Console.WriteLine("Geben sie den Innendurchmesser in mm ein");
                    Innendurchmesser = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    
                    //Abfrage Länge des Profils
                    Console.WriteLine("Außendurchmesser" + Convert.ToDouble(Außendurchmesser) + "mm");
                    Console.WriteLine("Innendurchmesser" + Convert.ToDouble(Innendurchmesser) + "mm");
                    Console.WriteLine("Geben Sie die Länge des Profils in mm ein");
                    Laenge = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    //Abfrage Material
                    Console.WriteLine("Außendurchmesser" + Convert.ToDouble(Außendurchmesser) + "mm");
                    Console.WriteLine("Innendurchmesser" + Convert.ToDouble(Innendurchmesser) + "mm");
                    Console.WriteLine("Laenge des Profils " + Convert.ToDouble(Laenge) + " mm ");
                    Console.WriteLine("");
                    Console.WriteLine("Aus welchem Material ist das Profil ?");
                    Console.WriteLine(" 1 = Stahl");
                    Console.WriteLine(" 2 = Aluminium");
                    Material = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();


                    if (Innendurchmesser > Außendurchmesser)
                    {
                        Console.WriteLine("Error: Außendurchmessser muss kleiner Innendurchmesser ");
                    }
                    else
                    {
                        


                        //Berrechnung Flächeninhalt
                        double cc = FlächenberechnungKHP(Außendurchmesser, Innendurchmesser);
                        //Berechnung Flächenschwerpunkt
                        string fs = FlächenschwerpunktKHP(Außendurchmesser);
                        //Berechnung Flächenträgheitsmoment IXX, IYY
                        string ftm = FlächenträgheitsmomenteKHP(Außendurchmesser, Innendurchmesser);
                        //Berrechnung Volumen
                        double vlm = Volumenberechnung(cc, Laenge);
                        //Berrechnung Material / Masse
                        Masse = Massenberechnung(Material,vlm);

                        //Ausgabe
                        Console.WriteLine("Außendurchmesser " + Convert.ToDouble(Außendurchmesser) + " mm ");
                        Console.WriteLine("Innendurchmesser " + Convert.ToDouble(Innendurchmesser) + " mm ");
                        Console.WriteLine("Laenge des Profils " + Convert.ToDouble(Laenge) + " mm ");
                        Console.WriteLine("");
                        Console.WriteLine("Flächeninhalt "+ Convert.ToString(cc) +"mm^2");//Ausgabe  A
                        Console.WriteLine(fs);//Ausgabe  FSP
                        Console.WriteLine(ftm);//Ausgabe FTM
                        Console.WriteLine("Flächentragheitsmoment IXY = 0 mm^4");
                        Console.WriteLine("Volumen: "+ vlm +" mm^3");//Ausgabe V
                        Console.WriteLine("Masse: " + Masse + " Gramm ");//Ausgabe Masse
                    }


                    Console.ReadKey();
                }
                #endregion Kreisprofileingabe 
                #region Doppel-T-Profileingabe
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

                    // Schleife zur Überprüfung auf Glaubwürdigkeit 

                    if (Stegdicke > Breite && Flanschdicke*2 > Höhe)
                    {
                        Console.WriteLine("Error: Außenabmessungen müssen größer als die Innenabmessungen sein");
                    }

                    else
                    {
                        //Berechnung des Flächeninhaltes 
                        Flächeninhalt = FlächenberechnungDoppelT(Breite, Höhe, Stegdicke, Flanschdicke);

                        //Berechnung des Flächenschwerpunktes
                        string ff = Flächenschwerpunkt(Breite, Höhe);

                        //Berechnung des Flächenträgheitsmomentes IXX
                        string gg = FlächenträgheitsmomentIXXDoppelT(Breite, Höhe, Stegdicke, Flanschdicke);

                        //Berechnung des Flächenträgheitsmomentes IYY
                        string hh = FlächentregheitsmomentIYYDoppelT(Breite, Höhe, Stegdicke, Flanschdicke);
                     
                        //Ausgabe 
                        Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                        Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                        Console.WriteLine("Stegdicke = " + Stegdicke + "mm");
                        Console.WriteLine("Flanschdicke = " + Flanschdicke + "mm");
                        Console.WriteLine("");
                        Console.WriteLine("Flächeninhalt = " + Convert.ToString(Flächeninhalt) + "mm^2"); // Ausgabe Flächeninhalt 
                        Console.WriteLine(ff); // Ausgabe Flächenschwerpunkt 
                        Console.WriteLine(gg); // Ausgabe IXX
                        Console.WriteLine(hh); // Ausgabe IYY 
                        Console.WriteLine("Flächentragheitsmoment IXY = 0 mm^4");
                        Console.WriteLine("Volumen = " + Volumen + "mm^3");
                        Console.WriteLine("Masse : " + Masse + " Gramm");
                    }



                    Console.ReadKey();

                }
                #endregion Doppel-T-Profileingabe

                #region Volumen/Masse

                //Berechnung des Volumens:
                Volumen = Volumenberechnung(Flächeninhalt, Länge);

                //Berechnung Material 
                Masse = Massenberechnung(Material, Volumen);

                Console.WriteLine(String.Format("Volumen = {0:00.000}", Volumen/1000000) + "Liter");
                Console.WriteLine(String.Format("Masse = {0:00.000}", Masse / 1000) + "kg");
                
                Console.ReadKey();

                wiederholung = MessageBox.Show("Weitere Berechnung? ", "Wichtige Frage",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
                Console.Clear();

                #endregion Volumen/Masse

            } while (wiederholung == MessageBoxResult.Yes);
        }
        #region Volumen/Materialauswahl
        static double Volumenberechnung(double lkFläche, double lkLänge)
        {
            double lkVolumen;
            lkVolumen = (lkFläche * lkLänge);
            return lkVolumen;//mm^3
        }

            static double Massenberechnung(double lkMaterial, double lkVolumen)
        {
            double lkDichte=0;
            double lkMasse;
           if (lkMaterial==1)//Stahl 
            {
                lkDichte =7.85;
            }
           else if (lkMaterial ==2)//Aluminium 
            {
                lkDichte = 2.7;
            }
           else
            {
                Console.WriteLine("Eingabe nicht korrekt");
            }

            lkMasse = (lkDichte * lkVolumen)/1000;

            return lkMasse;//Gramm  
        }
        #endregion Volumen/Materialauswahl
        #region Rechteckprofil
        static double Flächenberechnung(double lkBreite, double lkHöhe)
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = lkBreite * lkHöhe;

            return lkFlächeninhalt;
        }

        static string Flächenschwerpunkt(double lkBreite, double lkHöhe)
        {
            double lkhalbeBreite, lkhalbeHöhe;
            lkhalbeBreite = lkBreite / 2;
            lkhalbeHöhe = lkHöhe / 2;
            string lkAusagabe;
            lkAusagabe = "Schwerpunktkoordinaten (x:" + Convert.ToString(lkhalbeBreite) + " y:" + Convert.ToString(lkhalbeHöhe) + ")";
            return lkAusagabe;
        }

        static double  FlächenträgheitsmomentIXX(double lkBreite, double lkHöhe)
        {
            double lkIXX;
            lkIXX = (lkBreite * Math.Pow(lkHöhe, 3)) / 12;
            
            return lkIXX;
        }

        static double FlächentregheitsmomentIYY(double Breite, double Höhe)
        {
            double lkIYY;
            lkIYY = (Math.Pow(Breite, 3) * Höhe) / 12;
            
            return lkIYY;
        }

        #endregion Rechteckprofil
        #region Rechteckhohlprofil
        static double FlächenberechnungRHK(double Breite, double Höhe, double BreiteInnen, double HöheInnen)
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = Breite * Höhe - BreiteInnen * HöheInnen;

            return lkFlächeninhalt;
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
        #region Kreisprofil
        static double FlächenberechnungKP(double Radius)
        {
            double cclokal;

            cclokal = Math.Pow(Radius, 2) * Math.PI;

            return cclokal;

        }

        static string FlächenträgheitsmomentIXX_IYYKP(double Radius)
        {
            double i;
            i = (Math.PI * Math.Pow(Radius, 4)) / 12;

            string fflk;
            fflk = String.Format("Flächenträgheitsmoment IXX = IYY = {0:00.000}", i) + "mm^4";

            return fflk;
        }
        #endregion Kreis#
        #region Kreishohlprofil
        static double FlächenberechnungKHP(double Außendurchmesser, double Innendurchmesser)
        {
            double xylokal;
            xylokal = (Math.PI / 4) * (Außendurchmesser * Außendurchmesser - Innendurchmesser * Innendurchmesser);

            

            return xylokal;
           
        }
        

        static string FlächenschwerpunktKHP(double Außendurchmesser)
        {
            double xs , ys;

            xs = Außendurchmesser / 2;
            ys = Außendurchmesser / 2;

            string fs;

            fs = "Schwerpunktkoordinaten (xs:" + Convert.ToString(xs) + "ys:" + Convert.ToString(ys) + ")";

            return fs;
        }

        static string FlächenträgheitsmomenteKHP(double Außendurchmesser, double Innendurchmesser)
        {
            double ib;

            ib = (Math.PI / 64) * ((Math.Pow(Außendurchmesser, 4)) - Math.Pow(Innendurchmesser, 4));

            string ftm;
            ftm = String.Format("Flächenträgheitsmoment IXX = IYY = {0:00.000}", ib) + "mm^4";

            return ftm;
        }

        static double VolumenberrechnungKHP(double cc, double Laenge)
        {
            double volumen_lk;

            volumen_lk = cc * Laenge;
            return volumen_lk;
        }

        static double MasseberrechnungKHP(double Material, double vlm)
        {
            double Dichte = 0;
                if(Material == 1)//Stahl
            {
                Dichte = 7.85;
            }
                else if (Material == 2)//Aluminium
            {
                Dichte = 2.7;
            }
                else
            {
                Console.WriteLine("Eingabe nicht korrekt");
            }

            double Masse;

            Masse = (Dichte * vlm) / 1000;
            return Masse;
        }
        #endregion Kreishohlprofil
        #region Doppel-T-Profil
        static double FlächenberechnungDoppelT(double Breite, double Höhe, double Stegdicke, double Flanschdicke)
        {
            double lkFlächeninhalt;
            lkFlächeninhalt = Breite * Höhe - (Breite - Stegdicke) * (Höhe - 2 * Flanschdicke);

            return lkFlächeninhalt;
        }

        static string FlächenträgheitsmomentIXXDoppelT(double Breite, double Höhe, double Stegdicke, double Flanschdicke)
        {
            double Ix;
            Ix = (Breite * Math.Pow(Höhe, 3) - (Breite -Stegdicke) * Math.Pow((Höhe - 2 * Flanschdicke), 3)) / 12;

            string fflk;
            fflk = String.Format("Flächenträgheitsmoment IXX = {0:00.000}", Ix) + "mm^4";

            return fflk;
        }

        static string FlächentregheitsmomentIYYDoppelT(double Breite, double Höhe, double Stegdicke, double Flanschdicke)
        {
            double Iy;
            Iy = ((Höhe - (Höhe - 2 * Flanschdicke)) * Math.Pow( Breite , 3) + (Höhe - 2 * Flanschdicke) * Math.Pow(Breite-(Breite-Stegdicke),3)) / 12;

            string fflk;

            fflk = String.Format("Flächenträgheitsmoment IYY = {0:00.000}", Iy) + "mm^4";

            return fflk;
        }        
        #endregion Doppel-T-Profil
    }

}