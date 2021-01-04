using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ficharv03.SQLite
{
    class BBDD : SQLiteConnection
    {

        public static string formato = "dd/MM/yyyy";
        public static string formatoDT = @"HH\:mm\:ss";
        public static string formatoTS = @"hh\:mm\:ss";
        public static string calculoTotal = "xx:xx:xx";
        public static string debito = "DEBES...";

        public BBDD(string path) : base(path)
        {
            Initialize();
        }
        void Initialize()
        {
            CreateTable<PlantillaFichajes>();
        }
        public List<PlantillaFichajes> GetFichajes()
        {
            return Table<PlantillaFichajes>().ToList();
        }
        public List<PlantillaFichajes> GetEstadoTerminado(bool estado)
        {
            return Table<PlantillaFichajes>().Where(t => t.EstadoTerminado == estado).ToList();
        }
        public List<PlantillaFichajes> GetFichadia(string dia)
        {
            return Table<PlantillaFichajes>().Where(t => t.Dia == dia).ToList();
        }

        public bool AddFichaje(PlantillaFichajes ficha)
        {
            // El constructor es quien lo crea
            var lista = GetFichadia(ficha.Dia);
            if (lista.Count == 0)
            {
                Insert(ficha);
                return true;
            }
            else return false;

        }
        public bool UpdateFichaje(PlantillaFichajes ficha)
        {
            var lista = GetFichadia(ficha.Dia);
            if (lista.Count > 0)
            {
                Update(ficha);
                return true;
            }
            else return false;
        }
        public bool DeleteFichaje(PlantillaFichajes ficha)
        {
            var lista = GetFichadia(ficha.Dia);
            if (lista.Count > 0)
            {
                Delete(ficha);
                return true;
            }
            else return false;
        }
        public void DeleteALLFichaje()
        {
            //DeleteAll<PlantillaFichajes>();
            var lista = GetFichajes();
            foreach (PlantillaFichajes item in lista)
            {
                Delete(item);
            }
        }


    }
}
