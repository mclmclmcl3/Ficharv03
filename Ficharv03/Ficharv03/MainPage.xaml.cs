using Ficharv02.SQLite;
using Ficharv03.SQLite;
using Ficharv03.windows;
using System;
using System.Collections.Generic;
using System.Threading;
using Xamarin.Forms;


namespace Ficharv03
{

    public partial class MainPage : ContentPage
    {

        PlantillaFichajes ficha = new PlantillaFichajes();
        public MainPage()
        {
            InitializeComponent();

            ficha.CalculoMensual(Conexion.bd.GetFichajes());
            RelojPantalla();
            Update();
        }

        public void RelojPantalla()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() => reloj.Text = DateTime.Now.ToString("G"));
                return true;
            }
            );
        }
        protected override void OnAppearing()
        {
            Update();
        }

        public void TodosBotonesNormal()
        {
            BtnDiaNormal();
            BtnTrabajoNormal();
            BtnFinTrabajoNormal();
            BtnDesayunoNormal();
            BtnFinDesayunoNormal();
            BtnMedicoNormal();
            BtnFinMedicoNormal();
        }
        public void Update() 
        {
            // DisplayAlert("", "", "OK");
            NuevoDia();
            FichaToday();
            LbCalculoTotal.Text = BBDD.calculoTotal;

            if (ficha.EstadoDia == true) IntroEntrada();
            if (ficha.EstadoEnt == true) DespuesEntrada();
            if (ficha.EstadoDEnt == true) DespuesDesayuno();
            if (ficha.EstadoDSal == true) DespuesFinDesayuno();
            if (ficha.EstadoMEnt == true) DespuesMedico();
            if (ficha.EstadoMSal == true) DespuesFinMedico();
            if (ficha.EstadoSal == true) NuevoDia();
        }

        // FichaToday ACTUALIZA TODAS LAS PROPIEDADES DE FICHA CON LO QUE HAY EN LA BBDD
        public void FichaToday()
        {
            List<PlantillaFichajes> FichaSinTerminar = Conexion.bd.GetEstadoTerminado(false);
            if (FichaSinTerminar.Count > 0)
            {
                //DisplayAlert("", "La ficha en la que estamos\nes: " + FichaSinTerminar[0].Dia, "OK");
                ficha.Dia = FichaSinTerminar[0].Dia;
                ficha.EstadoDia = FichaSinTerminar[0].EstadoDia;
                ficha.Entrada = FichaSinTerminar[0].Entrada;
                ficha.EstadoEnt = FichaSinTerminar[0].EstadoEnt;
                ficha.Salida = FichaSinTerminar[0].Salida;
                ficha.EstadoSal = FichaSinTerminar[0].EstadoSal;
                ficha.CalculoES = FichaSinTerminar[0].CalculoES;
                ficha.EstadoCalES = FichaSinTerminar[0].EstadoCalES;
                ficha.DEntrada = FichaSinTerminar[0].DEntrada;
                ficha.EstadoDEnt = FichaSinTerminar[0].EstadoDEnt;
                ficha.DSalida = FichaSinTerminar[0].DSalida;
                ficha.EstadoDSal = FichaSinTerminar[0].EstadoDSal;
                ficha.DCalculoES = FichaSinTerminar[0].DCalculoES;
                ficha.EstadoDCalES = FichaSinTerminar[0].EstadoDCalES;
                ficha.MEntrada = FichaSinTerminar[0].MEntrada;
                ficha.EstadoMEnt = FichaSinTerminar[0].EstadoMEnt;
                ficha.MSalida = FichaSinTerminar[0].MSalida;
                ficha.EstadoMSal = FichaSinTerminar[0].EstadoMSal;
                ficha.MCalculoES = FichaSinTerminar[0].MCalculoES;
                ficha.EstadoMCalES = FichaSinTerminar[0].EstadoMCalES;
                ficha.EstadoTerminado = FichaSinTerminar[0].EstadoTerminado;
            }

        }
        // OPERADCIONES DE MENUS
        private async void MenuAjustes(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Configuracion());
        }

        private void MenuSalir(object sender, EventArgs e)
        {
            Thread.CurrentThread.Abort();
        }

        async void MenuInfo(object sender, EventArgs e)
        {
            var opcion = await DisplayActionSheet(
                "Vamos a borrar todos los datos de la BBDD",
                "Cancelar", null, "OK");
            if (opcion == "OK")
            {
                TodosBotonesNormal();
                Conexion.bd.DeleteALLFichaje();
                await Navigation.PushAsync(new MainPage());
            }
        }

        private async void MenuEdit(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Edicion(ficha));
        }

        async void MenuLista(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Listado());
        }
        // OPERACIONES DE ENTRADA DATOS

        async void BtnFicharDia(object sender, EventArgs e)
        {
            //var dia = DateTime.Now.ToString(BBDD.formato);
            //ficha = new PlantillaFichajes(dia); // El propico constructor crea el dia la entrada y estadoEnt=true
            //bool realizado = Conexion.bd.AddFichaje(ficha); // si no existe lo mete el la BBDD
            //if (!realizado)
            //{
            //    DisplayAlert("Alerta...", "El dia ya existe. \nIntroduce otro", "Confirmar");
            //}
            //Update();




            string result = await DisplayPromptAsync("Pon un dia", "##/##/####", "OK", "Cancelar", null);
            if (!string.IsNullOrWhiteSpace(result))
            {
                var dia = result;
                ficha = new PlantillaFichajes(dia); // El propico constructor crea el dia la entrada y estadoEnt=true
                bool realizado = Conexion.bd.AddFichaje(ficha); // si no existe lo mete el la BBDD
                if (!realizado)
                {
                    await DisplayAlert("Alerta...", "El dia ya existe. \nIntroduce otro", "Confirmar");
                }
                Update();
            }

        }

        private void Btn_Trabajo(object sender, EventArgs e)
        {
            if (ficha.EstadoDia == true)
            {
                ficha.Entrada = DateTime.Now.ToString(BBDD.formatoDT);
                ficha.EstadoEnt = true;
                bool realizado = Conexion.bd.UpdateFichaje(ficha);
                if (!realizado)
                {
                    ficha.Entrada = "";
                    ficha.EstadoEnt = false;
                }
            }
            Update();
        }

        private void Btn_FinTrabajo(object sender, EventArgs e)
        {
            if (ficha.EstadoEnt == true)
            {
                ficha.Salida = DateTime.Now.ToString(BBDD.formatoDT);
                ficha.EstadoSal = true;
                ficha.EstadoTerminado = true;
                bool resultado = ficha.Calculo();
                bool realizado = Conexion.bd.UpdateFichaje(ficha);
                if (!realizado)
                {
                    ficha.Salida = "";
                    ficha.EstadoSal = false;
                    ficha.EstadoTerminado = false;
                }
            }
            Update();
        }

        private void Btn_Desayuno(object sender, EventArgs e)
        {
            if (ficha.EstadoEnt == true)
            {
                ficha.DEntrada = DateTime.Now.ToString(BBDD.formatoDT);
                ficha.EstadoDEnt = true;
                bool realizado = Conexion.bd.UpdateFichaje(ficha);
                if (!realizado)
                {
                    ficha.DEntrada = "";
                    ficha.EstadoDEnt = false;
                }
            }
            Update();
        }

        private void Btn_FinDesayuno(object sender, EventArgs e)
        {
            ficha.DSalida = DateTime.Now.ToString(BBDD.formatoDT);
            ficha.EstadoDSal = true;
            bool EndDesy = ficha.Dcalculo();
            // si EndDEsy est true, he metido: DSalida, DCalculoES 
            // y he cambiado estados  EstadoDsal, EstadoDCalES a true
            if (EndDesy)
            {
                bool realizado = Conexion.bd.UpdateFichaje(ficha);
                if (!realizado)
                {
                    ficha.DSalida = "";
                    ficha.DCalculoES = "";
                    ficha.EstadoDSal = false;
                    ficha.EstadoDCalES = false;
                    DisplayAlert("Error...", "No se ha podido guardar\nlos datos en la BBDD", "Continuar");
                }
            }
            else
            {
                // limpio poruqe no se ha podido meter
                ficha.DSalida = "";
                ficha.EstadoDSal = false;
                DisplayAlert("Error...", "No se ha podido calcular Desayuno.", "Continuar");
            }
            Update();
        }

        private void Btn_Medico(object sender, EventArgs e)
        {
            if (ficha.EstadoEnt == true)
            {
                ficha.MEntrada = DateTime.Now.ToString(BBDD.formatoDT);
                ficha.EstadoMEnt = true;
                bool realizado = Conexion.bd.UpdateFichaje(ficha);
                if (!realizado)
                {
                    ficha.MEntrada = "";
                    ficha.EstadoMEnt = false;
                }
            }
            Update();
        }

        private void Btn_FinMedico(object sender, EventArgs e)
        {
            ficha.MSalida = DateTime.Now.ToString(BBDD.formatoDT);
            ficha.EstadoMSal = true;
            bool EndDesy = ficha.Mcalculo();
            // si EndDEsy est true, he metido: DSalida, DCalculoES 
            // y he cambiado estados  EstadoDsal, EstadoDCalES a true
            if (EndDesy)
            {
                bool realizado = Conexion.bd.UpdateFichaje(ficha);
                if (!realizado)
                {
                    ficha.MSalida = "";
                    ficha.MCalculoES = "";
                    ficha.EstadoMSal = false;
                    ficha.EstadoMCalES = false;
                    DisplayAlert("Error...", "No se ha podido guardar\nlos datos en la BBDD", "Continuar");
                }
            }
            else
            {
                // limpio poruqe no se ha podido meter
                ficha.MSalida = "";
                ficha.EstadoMSal = false;
                DisplayAlert("Error...", "No se ha podido calcular Desayuno.", "Continuar");
            }
            Update();
        }

   
    }
}
