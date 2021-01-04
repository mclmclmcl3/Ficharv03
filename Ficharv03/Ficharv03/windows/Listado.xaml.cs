using Ficharv02.SQLite;
using Ficharv03.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ficharv03.windows
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listado : ContentPage
    {
        private List<PlantillaFichajes> fullListFichajes;
        private List<PlantillaFichajes> ListaInListView;

        // Listas de relleno
        private List<String> meses = new List<String>()
        {"", "Enero", "Febrero", "Marzo", "Abrir", "Mayo", "Junio", "Julio",
            "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
        List<String> anios;
        // Fin listas de relleno

        public static string filtroMes = "", Filtroaño = "";
        public Listado()
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false); // No salga el menu

            //Traigo de la BBDD toda a base de datos y lo guardo en fullListFichajes
            fullListFichajes = Conexion.bd.GetFichajes();
            //MiListview.ItemsSource = fullListFichajes;

            // Relleno los filtros con los datos actualizados
            // Los meses es estatico pero los años solo de los que se han metido.
            rellenoFiltros();
            // Relleno la lista con los filtros realizados
            //SelecA.SelectedIndex = 0;
            //SelecM.SelectedIndex = 0;

            FillListViewFilter();


        }
        public void FillListViewFilter()
        {
            String SmesSelected;

            ListaInListView = new List<PlantillaFichajes>(fullListFichajes); // Creo una copia para no tener que tirar de la BBDD constantemente
            List<PlantillaFichajes> provisional = new List<PlantillaFichajes>();

            int mesSelected = meses.IndexOf(filtroMes);
            if (mesSelected < 10) SmesSelected = "0" + mesSelected;
            else SmesSelected = mesSelected.ToString();

            String misto = SmesSelected + "/" + Filtroaño;

            if (ListaInListView.Count > 0)
            {
                if (Filtroaño != "" && filtroMes == "")
                {
                    DisplayAlert("FILTRO AÑO","if (Filtroaño !=  -  && SmesSelected ==  - ) : "+ filtroMes +"/"+ SmesSelected, "OK");
                    for (int i = 0; i < ListaInListView.Count; i++)
                    {
                        if (Filtroaño == ListaInListView[i].Dia.Substring(6, 4)) provisional.Add(ListaInListView[i]);
                    }
                }

                if (filtroMes != "" && Filtroaño == "")
                {
                    DisplayAlert("FILTRO MES", "SmesSelected !=  -  && Filtroaño ==  - : " + SmesSelected + "/" + filtroMes, "OK");
                    for (int i = 0; i < ListaInListView.Count; i++)
                    {
                        if (SmesSelected == ListaInListView[i].Dia.Substring(3, 2)) provisional.Add(ListaInListView[i]);
                    }
                }

                if (Filtroaño != "" && filtroMes != "")
                {
                    DisplayAlert("AMBOS FILTROS", "Filtroaño !=  -  && SmesSelected !=  -  : " + filtroMes + "/" + SmesSelected, "OK");
                    for (int i = 0; i < ListaInListView.Count; i++)
                    {
                        if( misto == ListaInListView[i].Dia.Substring(3, 7)) provisional.Add(ListaInListView[i]);
                    }
                }
            }

            if (provisional.Count > 0 ) ListaInListView = new List<PlantillaFichajes>(provisional);
           
            MiListview.ItemsSource = ListaInListView;
        }
        public void rellenoFiltros()
        {
            SelecM.ItemsSource = meses;
            SelecM.SetValue(ContentProperty, meses[0]);
            
            anios = new List<string>();
            anios.Add("");

            foreach (PlantillaFichajes item in fullListFichajes)
            {
                String anio = item.Dia.Substring(6, 4);

                if (!anios.Contains(anio))
                {
                    anios.Add(anio);
                }
            }
            SelecA.ItemsSource = anios;
            SelecA.SetValue(ContentProperty, anios[0]);
        }

        private void SeleccionItemA(object sender, EventArgs e)
        {
            var seleccionA = (Picker)sender;
            int selectedIndex = seleccionA.SelectedIndex;

            if (selectedIndex > -1)
            {
                String miA = (string)seleccionA.ItemsSource[selectedIndex];
                Filtroaño = miA;
            }
            else Filtroaño = "";
            FillListViewFilter();
        }

        private void SeleccionItemM(object sender, EventArgs e)
        {
            var seleccionM = (Picker)sender;
            int selectedIndex = seleccionM.SelectedIndex;

            if (selectedIndex > -1)
            {
                String miM = (string)seleccionM.ItemsSource[selectedIndex];
                filtroMes = miM;

            }
            else filtroMes = "";
            FillListViewFilter();
        }

        async void Btn_Volver(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private void MenuSalir(object sender, EventArgs e)
        {
            Thread.CurrentThread.Abort();
        }

        private void MenuInfo(object sender, EventArgs e)
        {

        }

        private async void idSeleccionRow(object sender, ItemTappedEventArgs e)
        {
            var id = e.Item as PlantillaFichajes;
            var miseleccion = e.ItemIndex;
            //await DisplayAlert("ItemTapped", "La seleccion es :" + miseleccion + "\n el indice es: " + id.Dia, "Continuar");
            MiListview.SelectedItem = null;
            await Navigation.PushAsync(new Edicion(id));
        }
    }
}