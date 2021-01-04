using Ficharv03.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ficharv03.windows
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Configuracion : ContentPage
    {
        public Configuracion()
        {
            InitializeComponent();

            Lbjornada.Text = PlantillaFichajes.jornada.ToString(BBDD.formatoTS);
        }
   
        private void EntryEventEnd(object sender, EventArgs e)
        {
            Entry jor = (Entry)sender;
            String fecha = jor.Text;
            //DisplayAlert("", "La fecha introducida es: " + fecha, "OK");
            PlantillaFichajes.jornada = TimeSpan.Parse(fecha);
            Lbjornada.Text = PlantillaFichajes.jornada.ToString(BBDD.formatoTS);
            jor.Text = "";
        }
    }
}