using Ficharv02.SQLite;
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
    public partial class Edicion : ContentPage
    {
        private PlantillaFichajes ficha;
        public Edicion(PlantillaFichajes f)
        {
            this.ficha = f;
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false); // No salga el menu
            this.BindingContext = ficha;
        }


        private void EngryBorrar(object sender, ElementEventArgs e)
        {
            Entry dia = (Entry)sender;
            dia.Text = "";
        }
        private void Borrar(object sender, EventArgs e)
        {
            bool corecto = Conexion.bd.DeleteFichaje(ficha);
            if (!corecto)
            {
                DisplayAlert("Error...", "No se a podido borrar...", "Continuar");
            }
            BorrarEntrys();
        }
        private void Grabar(object sender, EventArgs e)
        {
            if (EntryDia.Text != "")
            {
                ficha.Dia = EntryDia.Text;
                ficha.EstadoDia = true;
            }
            if (EntryEntrada.Text != "")
            {
                ficha.Entrada = EntryEntrada.Text;
                ficha.EstadoEnt = true;
            }
            if (EntryDEntrada.Text != "")
            {
                ficha.DEntrada = EntryDEntrada.Text;
                ficha.EstadoDEnt = true;
            }
            if (EntryDSalida.Text != "")
            {
                ficha.DSalida = EntryDSalida.Text;
                ficha.EstadoDSal = true;
            }
            if (EntryMEntrada.Text != "")
            {
                ficha.MEntrada = EntryMEntrada.Text;
                ficha.EstadoMEnt = true;
            }
            if (EntryMSalida.Text != "")
            {
                ficha.MSalida = EntryMSalida.Text;
            }
            if (EntrySalida.Text != "")
            {
                ficha.Salida = EntrySalida.Text;
                ficha.EstadoSal = true;
            }
            ficha.Dcalculo();
            ficha.Mcalculo();
            ficha.Calculo();
            bool corecto = Conexion.bd.UpdateFichaje(ficha);
            if(!corecto)
            {
                DisplayAlert("Error...", "No se a podido realizar la grabacion...", "Continuar");
            }
            BorrarEntrys();
        }
        public void BorrarEntrys()
        {
            EntryDia.Text = "";
            EntryEntrada.Text = "";
            EntryDEntrada.Text = "";
            EntryDSalida.Text = "";
            EntryMEntrada.Text = "";
            EntryMSalida.Text = "";
            EntrySalida.Text = "";

        }
        //private void Nuevo(object sender, EventArgs e)
        //{

        //}
        private async void Volver(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }

    public class MaskedBehavior : Behavior<Entry>
    {
        private string _mask = "";
        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
                SetPositions();
            }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        IDictionary<int, char> _positions;

        void SetPositions()
        {
            if (string.IsNullOrEmpty(Mask))
            {
                _positions = null;
                return;
            }

            var list = new Dictionary<int, char>();
            for (var i = 0; i < Mask.Length; i++)
                if (Mask[i] != 'X')
                    list.Add(i, Mask[i]);

            _positions = list;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Entry;

            var text = entry.Text;

            if (string.IsNullOrWhiteSpace(text) || _positions == null)
                return;

            if (text.Length > _mask.Length)
            {
                entry.Text = text.Remove(text.Length - 1);
                return;
            }

            foreach (var position in _positions)
                if (text.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();
                    if (text.Substring(position.Key, 1) != value)
                        text = text.Insert(position.Key, value);
                }

            if (entry.Text != text)
                entry.Text = text;
        }
    }
}