using Ficharv02.SQLite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ficharv03.SQLite
{
    [Table("Fichas")]
    public class PlantillaFichajes
    {
        public static TimeSpan jornada = new TimeSpan(6, 40, 0);

        [PrimaryKey, NotNull, Unique, MaxLength(10)]
        public string Dia { get; set; }
        public bool EstadoDia { get; set; }
        [MaxLength(8)]
        public string Entrada { get; set; }
        public bool EstadoEnt { get; set; }
        [MaxLength(8)]
        public string Salida { get; set; }
        public bool EstadoSal { get; set; }
        [MaxLength(8)]
        public string CalculoES { get; set; }
        public bool EstadoCalES { get; set; }
        [MaxLength(8)]
        public string DEntrada { get; set; }
        public bool EstadoDEnt { get; set; }
        [MaxLength(8)]
        public string DSalida { get; set; }
        public bool EstadoDSal { get; set; }
        [MaxLength(8)]
        public string DCalculoES { get; set; }
        public bool EstadoDCalES { get; set; }
        [MaxLength(8)]
        public string MEntrada { get; set; }
        public bool EstadoMEnt { get; set; }
        [MaxLength(8)]
        public string MSalida { get; set; }
        public bool EstadoMSal { get; set; }
        [MaxLength(8)]
        public string MCalculoES { get; set; }
        public bool EstadoMCalES { get; set; }
        public bool EstadoTerminado { get; set; }
        public string EstaJornada { get; set; }
        public PlantillaFichajes()
        {
            // Este constructor es solo para crear una instacia y cargar los datos de la BBDD
            this.DCalculoES = "00:00:00";
            this.MCalculoES = "00:00:00";
            this.CalculoES = "00:00:00";
            this.EstaJornada = jornada.ToString(BBDD.formatoTS);

        }
        public PlantillaFichajes(string dia)
        {
            this.Dia = dia;
            this.EstadoDia = true;
            this.EstadoEnt = false;
            this.EstadoSal = false;
            this.EstadoCalES = false;
            this.EstadoDEnt = false;
            this.EstadoDSal = false;
            this.EstadoDCalES = false;
            this.EstadoMEnt = false;
            this.EstadoMSal = false;
            this.EstadoMCalES = false;
            this.EstadoTerminado = false;
            this.EstaJornada = jornada.ToString(BBDD.formatoTS);
            this.DCalculoES = "00:00:00";
            this.MCalculoES = "00:00:00";
            this.CalculoES = "00:00:00";
        }
        //public PlantillaFichajes(string dia, string entrada)
        //{
        //    this.Dia = dia;
        //    this.Entrada = entrada;
        //    this.EstadoEnt = true;
        //    this.EstadoTerminado = false;
        //}
        public bool Dcalculo()
        {
            bool resultado = false;
            if (EstadoDEnt && EstadoDSal)
            {
                // DCalculoES = "00:00:00";
                DCalculoES = Restar(DSalida, DEntrada);
                EstadoDCalES = true;
                resultado = true;
            }
            return resultado;
        }
        public bool Mcalculo()
        {
            bool resultado = false;
            if (EstadoMEnt && EstadoMSal)
            {
                // MCalculoES = "00:00:00";
                MCalculoES = Restar(MSalida, MEntrada);
                EstadoMCalES = true;
                resultado = true;
            }
            return resultado;
        }
        public bool Calculo()
        {
            bool resultado = false;
            if (EstadoEnt && EstadoSal)
            {
                // MCalculoES = "00:00:00";
                CalculoES = CalculoGeneral();
                EstadoMCalES = true;
                resultado = true;
            }
            return resultado;
        }
        public String CalculoGeneral()
        {
            // ( Cal 7h - ( CalD + CalM ) ) - jornada
            String resta = Restar(Salida, Entrada);
            if (DCalculoES == null) DCalculoES = "00:00:00";
            if (MCalculoES == null) MCalculoES = "00:00:00";
            String sumaDescansos = ConvertString(ConvertTS(DCalculoES) + ConvertTS(MCalculoES));
            String parcial = Restar(resta, sumaDescansos);
            String Total = Restar(parcial, ConvertString(jornada));
            CalculoMensual(Conexion.bd.GetFichajes());
            return Total;
        }


        public void CalculoMensual(List<PlantillaFichajes> baseDatos)
        {
            TimeSpan suma = new TimeSpan(0, 0, 0);
            foreach (PlantillaFichajes item in baseDatos)
            {
                if (item.CalculoES.Length < 9) suma += ConvertTS(item.CalculoES);
                else suma += new TimeSpan(0, 0, 0) - ConvertTS(item.CalculoES);
            }
            BBDD.calculoTotal = ConvertString(suma);
        }
  
        public TimeSpan ConvertTS(string hora)
        {
            if (hora.Length < 9)
            {
                TimeSpan mihora = TimeSpan.Parse(hora);
                return mihora;
            }

            else return new TimeSpan(0, 0, 0).Subtract(TimeSpan.Parse(hora));
        }
        public String ConvertString(TimeSpan hora)
        {
            String resultado;
            if (hora < new TimeSpan(0, 0, 0)) resultado = "-" + hora.ToString(@"hh\:mm\:ss");
            else resultado = hora.ToString(@"hh\:mm\:ss");
            return resultado;
        }

        public String Restar(String salida, String entrada)
        {
            TimeSpan TSsalida = ConvertTS(salida);
            TimeSpan TSentrada = ConvertTS(entrada);

            return ConvertString(TSsalida.Subtract(TSentrada));
        }
        public String Sumar(String salida, String entrada)
        {
            TimeSpan TSsalida = ConvertTS(salida);
            TimeSpan TSentrada = ConvertTS(entrada);

            return ConvertString(TSsalida.Add(TSentrada));
        }

    }
}
