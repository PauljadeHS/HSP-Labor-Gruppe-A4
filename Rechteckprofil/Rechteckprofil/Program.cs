﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rechteckprofil
{
    class Program
    {
        static public void Main(string[] args)
        {
            //Eingabe 
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

            //Berechnung des Flächenträgheitsmomentes IX
            string gg = Flächenträgheitsmomentix(Breite,Höhe);

            //Berechnung des Flächenträgheitsmomentes Iy
            string hh = Flächentregheitsmomentiy(Breite,Höhe);

            //Ausgabe 
            Console.WriteLine("Breite = " + Convert.ToString(Breite) + "mm");
            Console.WriteLine("Höhe = " + Convert.ToString(Höhe) + "mm");
            Console.WriteLine("");
            Console.WriteLine("Flächeninhalt = " + Convert.ToString(cc) + "mm^2"); // Ausgabe Flächeninhalt 
            Console.WriteLine(ff); // Ausgabe Flächenschwerpunkt 
            Console.WriteLine(gg); //Ausgabe IX
            Console.WriteLine(hh); // Ausgabe IY 
            Console.WriteLine("Flächentragheitsmoment IXY = 0 mm^4");

            Console.ReadKey();
        }

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

        static string Flächenträgheitsmomentix(double Breite, double Höhe)
        {
            double ix;
            ix = (Breite * Höhe * Höhe * Höhe) / 12;

            string fflk;            
            fflk = String.Format("Flächenträgheitsmoment IY = {0:00.000}", ix) + "mm^4";

            return fflk;
        }

        static string Flächentregheitsmomentiy(double Breite, double Höhe)
        {
            double iy;
            iy = (Breite * Breite * Breite * Höhe) / 12;

            string fflk;

            fflk = String.Format("Flächenträgheitsmoment IY = {0:00.000}", iy) + "mm^4";

            return fflk;
        }
        
              
    }
}
