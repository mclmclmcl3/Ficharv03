using Ficharv03.SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ficharv02.SQLite
{
    static class Conexion
    {
        readonly private static string ruta = (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Datos.db"));

        public static BBDD bd = new BBDD(ruta);

    }
}
