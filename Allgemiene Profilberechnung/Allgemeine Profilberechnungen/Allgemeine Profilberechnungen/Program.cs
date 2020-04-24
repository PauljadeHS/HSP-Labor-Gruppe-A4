﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Allgemeine_Profilberechnungen
{
    class Program
    {
        static public void Main(string[] args)
        {
            MessageBoxResult wiederholung;
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
                    #region Eingabge 
                    double Breite, Höhe, Länge, Material;
                    Console.WriteLine("Geben Sie die Breite des Rechteckes in mm an");
                    Breite = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Geben Sie die Höhe des Rechteckes in mm an ");
                    Höhe = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Geben Sie die Länge des Profils in mm an ");
                    Länge = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("Länge  = " + Convert.ToString(Länge) + "mm");
                    Console.WriteLine("");
                    Console.WriteLine("Aus welchem Material ist das Profil? ");
                    Console.WriteLine("1 = Stahl ");
                    Console.WriteLine("2 = Aluminium ");
                    Material = Convert.ToDouble(Console.ReadLine());
                    Console.Clear();

                    #endregion Eingabe 

                    //Berechnung des Flächeninhaltes 
                    double Flächeninhalt  = Flächenberechnung(Breite, Höhe);

                    //Berechnung des Flächenschwerpunktes
                    string Flächenschwerp = Flächenschwerpunkt(Breite, Höhe);

                    //Berechnung des Flächenträgheitsmomentes IXX
                    string IXX = FlächenträgheitsmomentIXX(Breite, Höhe);

                    //Berechnung des Flächenträgheitsmomentes IYY
                    string IYY = FlächentregheitsmomentIYY(Breite, Höhe);

                    //Berechnung des Volumens:
                    double volumen = Volumenberechnung(Flächeninhalt, Länge);

                    //Berechnung Material 
                    double Masse = Materialauswahl(Material, volumen); 
                                       

                    //Ausgabe 
                    Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
                    Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
                    Console.WriteLine("");
                    Console.WriteLine("Flächeninhalt des Querschnittes = " + Convert.ToString(Flächeninhalt) + "mm^2"); // Ausgabe Flächeninhalt 
                    Console.WriteLine(Flächenschwerp); // Ausgabe Flächenschwerpunkt 
                    Console.WriteLine(IXX); // Ausgabe IXX
                    Console.WriteLine(IYY); // Ausgabe IYY 
                    Console.WriteLine("Flächentragheitsmoment IXY = 0 mm^4");
                    Console.WriteLine("Volumen = " + volumen + "mm^3");
                    Console.WriteLine("Masse : " + Masse +" Gramm");

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

                    // Schleife zur Überprüfung auf Glaubwürdigkeit 

                    if (BreiteInnen > Breite && HöheInnen > Höhe)
                    {
                        Console.WriteLine("Error: Außenabmessungen müssen größer als die Innenabmessungen sein");
                    }

                    else
                    {
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
                    }



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
                    double Außendurchmesser, Innendurchmesser, Laenge, Material;
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
                    Material = Convert.ToDouble(Console.ReadKey());
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
                        double vlm = VolumenberrechnungKHP(cc, Laenge);
                        //Berrechnung Material / Masse
                        double Masse = MasseberrechnungKHP(vlm, Material);

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
                else
                {
                    Console.WriteLine("Fehler, Auswahl nicht eindeutig");
                    Console.ReadKey();
                }

                wiederholung = MessageBox.Show("Weitere Berechnung? ", "Wichtige Frage",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
                Console.Clear();
            } while (wiederholung == MessageBoxResult.Yes);
        }
        static double Materialauswahl(double Material,double Volumen )
        {
            double Dichte = 0;
           if (Material==1)//Stahl 
            {
                Dichte =7.85;
            }
           else if (Material ==2)//Aluminium 
            {
                Dichte = 2.7;
            }
           else
            {
                Console.WriteLine("Eingabe nicht korrekt");
            }

            Dichte = (Dichte * Volumen)/1000;

            return Dichte; 
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
            string fflk = String.Format("Flächenträgheitsmoment IYY = {0:00.000}", iy) + "mm^4";
            return fflk;
        }

         static double Volumenberechnung(double cc, double Länge)
        {
            double volumen_lk;
            volumen_lk = cc * Länge;
            return volumen_lk;
 
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
    }

}