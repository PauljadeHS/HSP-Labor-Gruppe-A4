using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rechteckprofil
{
    class Program
    {
        static void Main(string[] args)
        {
            //Eingabe der Kantenlängen 
            Eingabe(); 
            
            //Berechenen des Flächeninhaltes 


            //Berechnung des Flächenschwerpunktes

            //Berechnung des Flächenträgheitsmomentes 

            // Ausgabe 
        }

        static void Eingabe()
        {
            double aa, bb;
            Console.WriteLine("Geben Sie die Breite des Rechteckes in mm an");
            aa = Convert.ToDouble(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Breite = " + Convert.ToString(aa) + "mm");
            Console.WriteLine("Geben Sie die Höhe des Rechteckes in mm an ");
            bb = Convert.ToDouble(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Breite = " + Convert.ToString(aa) + "mm");
            Console.WriteLine("Höhe = " + Convert.ToString(bb) + "mm");

            Console.ReadKey();
        }
        
    }
}
