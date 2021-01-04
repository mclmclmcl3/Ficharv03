using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaList
{
    class Fichar
    {
        public String Entrada { get; set; }
        public String Salida { get; set; }
        public String EDesy { get; set; }
        public String SDesy { get; set; }
        public String EMed { get; set; }
        public String SMed { get; set; }
        public String CDesy { get; set; }
        public String CMed { get; set; }
        public String Calculo { get; set; }
    }
    static class GestionBBDD
    {
        public static List<Fichar> BBDDF = new List<Fichar>();
    }
    class Program
    {
        

        public static TimeSpan jornada =  new TimeSpan(6,40,0);
        public static String CalculoMes = "00:00:00";
        static void Main(string[] args)
        {
            // Creo fechas para probar
            GestionBBDD.BBDDF.Add(new Fichar() { Entrada = "07:00:00", EDesy = "09:00:00", SDesy = "09:20:00", EMed = "11:00:00", SMed = "12:00:00", Salida = "14:00:00" });
            GestionBBDD.BBDDF.Add(new Fichar() { Entrada = "07:00:00", EDesy = "09:30:00", SDesy = "09:20:00", EMed = "00:00:00", SMed = "00:00:00", Salida = "14:00:00" });
            GestionBBDD.BBDDF.Add(new Fichar() { Entrada = "07:10:00", EDesy = "09:00:00", SDesy = "09:15:00", EMed = "01:00:00", SMed = "00:00:00", Salida = "14:20:00" });
            GestionBBDD.BBDDF.Add(new Fichar() { Entrada = "07:00:00", EDesy = "09:00:00", SDesy = "09:10:00", EMed = "00:00:00", SMed = "00:00:00", Salida = "14:00:00" });

            // Hago todos los calculos de cada fichaje
            GestionBBDD.BBDDF[0].CDesy = Restar(GestionBBDD.BBDDF[0].SDesy, GestionBBDD.BBDDF[0].EDesy);
            GestionBBDD.BBDDF[0].CMed = Restar(GestionBBDD.BBDDF[0].SMed, GestionBBDD.BBDDF[0].EMed);
            GestionBBDD.BBDDF[0].Calculo = CalculoGeneral(GestionBBDD.BBDDF[0]);

            GestionBBDD.BBDDF[1].CDesy = Restar(GestionBBDD.BBDDF[1].SDesy, GestionBBDD.BBDDF[1].EDesy);
            GestionBBDD.BBDDF[1].CMed = Restar(GestionBBDD.BBDDF[1].SMed, GestionBBDD.BBDDF[1].EMed);
            GestionBBDD.BBDDF[1].Calculo = CalculoGeneral(GestionBBDD.BBDDF[1]);

            GestionBBDD.BBDDF[2].CDesy = Restar(GestionBBDD.BBDDF[2].SDesy, GestionBBDD.BBDDF[2].EDesy);
            GestionBBDD.BBDDF[2].CMed = Restar(GestionBBDD.BBDDF[2].SMed, GestionBBDD.BBDDF[2].EMed);
            GestionBBDD.BBDDF[2].Calculo = CalculoGeneral(GestionBBDD.BBDDF[2]);

            GestionBBDD.BBDDF[3].CDesy = Restar(GestionBBDD.BBDDF[3].SDesy, GestionBBDD.BBDDF[3].EDesy);
            GestionBBDD.BBDDF[3].CMed = Restar(GestionBBDD.BBDDF[3].SMed, GestionBBDD.BBDDF[3].EMed);
            GestionBBDD.BBDDF[3].Calculo = CalculoGeneral(GestionBBDD.BBDDF[3]);

            // Imprimir
            Console.WriteLine("Jornada:" + jornada);
            Console.WriteLine();
            Console.WriteLine(GestionBBDD.BBDDF[0].Entrada + "\t" + GestionBBDD.BBDDF[1].Entrada + "\t" + GestionBBDD.BBDDF[2].Entrada + "\t" + GestionBBDD.BBDDF[3].Entrada);
            Console.WriteLine();
            Console.WriteLine(GestionBBDD.BBDDF[0].EDesy + "\t" + GestionBBDD.BBDDF[1].EDesy + "\t" + GestionBBDD.BBDDF[2].EDesy + "\t" + GestionBBDD.BBDDF[3].EDesy);
            Console.WriteLine(GestionBBDD.BBDDF[0].SDesy + "\t" + GestionBBDD.BBDDF[1].SDesy + "\t" + GestionBBDD.BBDDF[2].SDesy + "\t" + GestionBBDD.BBDDF[3].SDesy);
            Console.WriteLine(GestionBBDD.BBDDF[0].CDesy + "\t" + GestionBBDD.BBDDF[1].CDesy + "\t" + GestionBBDD.BBDDF[2].CDesy + "\t" + GestionBBDD.BBDDF[3].CDesy);
            Console.WriteLine();
            Console.WriteLine(GestionBBDD.BBDDF[0].EMed + "\t" + GestionBBDD.BBDDF[1].EMed + "\t" + GestionBBDD.BBDDF[2].EMed + "\t" + GestionBBDD.BBDDF[3].EMed);
            Console.WriteLine(GestionBBDD.BBDDF[0].SMed + "\t" + GestionBBDD.BBDDF[1].SMed + "\t" + GestionBBDD.BBDDF[2].SMed + "\t" + GestionBBDD.BBDDF[3].SMed);
            Console.WriteLine(GestionBBDD.BBDDF[0].CMed + "\t" + GestionBBDD.BBDDF[1].CMed + "\t" + GestionBBDD.BBDDF[2].CMed + "\t" + GestionBBDD.BBDDF[3].CMed);
            Console.WriteLine();
            Console.WriteLine(GestionBBDD.BBDDF[0].Salida + "\t" + GestionBBDD.BBDDF[1].Salida + "\t" + GestionBBDD.BBDDF[2].Salida + "\t" + GestionBBDD.BBDDF[3].Salida);
            Console.WriteLine();
            Console.WriteLine(GestionBBDD.BBDDF[0].Calculo + "\t" + GestionBBDD.BBDDF[1].Calculo + "\t" + GestionBBDD.BBDDF[2].Calculo + "\t" + GestionBBDD.BBDDF[3].Calculo);

            CalculoMensual(GestionBBDD.BBDDF);

            Console.WriteLine("El calculo del mes es: " + CalculoMes);
            Console.ReadKey();
        }

        public static void CalculoMensual(List<Fichar> baseDatos)
        {
            TimeSpan suma = new TimeSpan(0,0,0);
            foreach (Fichar item in baseDatos)
            {
                if (item.Calculo.Length < 9) suma += ConvertTS(item.Calculo);
                else suma += new TimeSpan(0, 0, 0) - ConvertTS(item.Calculo);
            }
            CalculoMes = ConvertString(suma);
        }
        public static String CalculoGeneral(Fichar lista)
        {
            // ( Cal 7h - ( CalD + CalM ) ) - jornada
            String resta = Restar(lista.Salida, lista.Entrada);
            String sumaDescansos = ConvertString(ConvertTS(lista.CDesy) + ConvertTS(lista.CMed));
            String parcial = Restar(resta, sumaDescansos);
            String Total = Restar(parcial, ConvertString(jornada));
            return Total;
        }

        public static TimeSpan ConvertTS(string hora)
        {
            if (hora.Length < 9) return TimeSpan.Parse(hora);
            else return new TimeSpan(0,0,0).Subtract(TimeSpan.Parse(hora));
        }
        public static String ConvertString(TimeSpan hora)
        { 
            String resultado;
            if (hora <new TimeSpan(0,0,0)) resultado = "-" + hora.ToString(@"hh\:mm\:ss");
            else resultado = hora.ToString(@"hh\:mm\:ss");
            return resultado;
        }

        public static String Restar (String salida, String entrada)
        {
            TimeSpan TSsalida = ConvertTS(salida);
            TimeSpan TSentrada = ConvertTS(entrada);

            return ConvertString(TSsalida.Subtract(TSentrada));
        }
        public static String Sumar(String salida, String entrada)
        {
            TimeSpan TSsalida = ConvertTS(salida);
            TimeSpan TSentrada = ConvertTS(entrada);

            return ConvertString(TSsalida.Add(TSentrada));
        }
    }
}
