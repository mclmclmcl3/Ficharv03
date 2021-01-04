using Ficharv02.SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ficharv03
{ 
    public partial class MainPage : ContentPage
    {
        public void IntroEntrada()
        {
            BtnDiaOn();
            BtnTrabajoNormal();
            BtnFinTrabajoOff();
            BtnDesayunoOff();
            BtnFinDesayunoOff();
            BtnMedicoOff();
            BtnFinMedicoOff();
        }
        public void DespuesEntrada() // Este es el normal para meter desyuno, medico o salida
        {
            BtnDiaOn();
            BtnTrabajoOn();
            BtnDesayunoNormal();
            BtnFinDesayunoOff();
            BtnMedicoNormal();
            BtnFinMedicoOff();
            BtnFinTrabajoNormal();
        }
        public void DespuesDesayuno()
        {
            BtnDiaOn();
            BtnTrabajoOn();
            BtnDesayunoOn();
            BtnFinDesayunoNormal();

            if (ficha.EstadoMEnt) // si ya esta metido Medico
            {
                BtnMedicoOn();
                BtnFinMedicoOn();
            }
            if (!ficha.EstadoMEnt) // si no esta metido Medico
            {
                BtnMedicoOff();
                BtnFinMedicoOff();
            }
            BtnFinTrabajoOff();
        }
        public void DespuesFinDesayuno()
        {
            BtnDiaOn();
            BtnTrabajoOn();
            BtnDesayunoOn();
            BtnFinDesayunoOn();

            if (ficha.EstadoMEnt) // si ya esta metido Medico
            {
                BtnMedicoOn();
                BtnFinMedicoOn();
            }
            if (!ficha.EstadoMEnt) // si no esta metido Medico
            {
                BtnMedicoNormal();
                BtnFinMedicoOff();
            }
            BtnFinTrabajoNormal();
        }
        public void DespuesMedico()
        {
            BtnDiaOn();
            BtnTrabajoOn();
            BtnDesayunoOn();
            BtnFinDesayunoNormal();

            if (ficha.EstadoDEnt) // si ya esta metido Desayuno
            {
                BtnDesayunoOn();
                BtnFinDesayunoOn();
            }
            if (!ficha.EstadoDEnt) // si no esta metido Desayuno
            {
                BtnDesayunoOff();
                BtnFinDesayunoOff();
            }
            BtnMedicoOn();

            BtnFinMedicoNormal();
            BtnFinTrabajoOff();

        }
        public void DespuesFinMedico()
        {
            BtnDiaOn();
            BtnTrabajoOn();
            BtnDesayunoOn();
            BtnFinDesayunoNormal();

            if (ficha.EstadoDEnt) // si ya esta metido Desayuno
            {
                BtnDesayunoOn();
                BtnFinDesayunoOn();
            }
            if (!ficha.EstadoDEnt) // si no esta metido Desayuno
            {
                BtnDesayunoNormal();
                BtnFinDesayunoOff();
            }
            BtnMedicoOn();

            BtnFinMedicoOn();
            BtnFinTrabajoNormal();

        }
        public void SoloSAlida()
        {
            BtnDiaOn();
            BtnTrabajoOn();
            BtnDesayunoOn();
            BtnFinDesayunoOn();
            BtnMedicoOn();
            BtnFinMedicoOn();
            BtnFinTrabajoNormal();
        }
        public void NuevoDia()
        {
            BtnDiaNormal();
            BtnTrabajoOff();
            BtnDesayunoOff();
            BtnFinDesayunoOff();
            BtnMedicoOff();
            BtnFinMedicoOff();
            BtnFinTrabajoOff();
        }

        public void BtnDiaNormal()
        {
            FicharDia.Source = "iconoFicharNormal.png";
            FicharDia.IsEnabled = true;
        }
        public void BtnTrabajoNormal()
        {
            BtnTrabajo.Source = "iconotrabajo.png";
            BtnTrabajo.IsEnabled = true;
        }
        public void BtnFinTrabajoNormal()
        {
            BtnFinTrabajo.Source = "iconofintrabajo.png";
            BtnFinTrabajo.IsEnabled = true;
        }
        public void BtnDesayunoNormal()
        {
            BtnDesayuno.Source = "iconocafe.png";
            BtnDesayuno.IsEnabled = true;
        }
        public void BtnFinDesayunoNormal()
        {
            BtnFinDesayuno.Source = "iconofincafe.png";
            BtnFinDesayuno.IsEnabled = true;
        }
        public void BtnMedicoNormal()
        {
            BtnMedico.Source = "iconomedico.png";
            BtnMedico.IsEnabled = true;
        }
        public void BtnFinMedicoNormal()
        {
            BtnFinMedico.Source = "iconofinmedico.png";
            BtnFinMedico.IsEnabled = true;
        }

        public void BtnDiaMal()
        {
            FicharDia.Source = "iconoFicharMal.png";
            FicharDia.IsEnabled = true;
        }
        public void BtnTrabajoMal()
        {
            BtnTrabajo.Source = "iconotrabajomal.png";
            BtnTrabajo.IsEnabled = true;
        }
        public void BtnFinTrabajoMal()
        {
            BtnFinTrabajo.Source = "iconofintrabajomal.png";
            BtnFinTrabajo.IsEnabled = true;
        }
        public void BtnDesayunoMal()
        {
            BtnDesayuno.Source = "iconodesayunomal.png";
            BtnDesayuno.IsEnabled = true;
        }
        public void BtnFinDesayunoMal()
        {
            BtnFinDesayuno.Source = "iconofincafemal.png";
            BtnFinDesayuno.IsEnabled = true;
        }
        public void BtnMedicoMal()
        {
            BtnMedico.Source = "iconomedicomal.png";
            BtnMedico.IsEnabled = true;
        }
        public void BtnFinMedicoMal()
        {
            BtnFinMedico.Source = "iconofinmedicomal.png";
            BtnFinMedico.IsEnabled = true;
        }

        public void BtnDiaOn()
        {
            FicharDia.Source = "iconoFicharOk.png";
            FicharDia.IsEnabled = false;
        }
        public void BtnTrabajoOn()
        {
            BtnTrabajo.Source = "iconotrabajoon.png";
            BtnTrabajo.IsEnabled = false;
        }
        public void BtnFinTrabajoOn()
        {
            BtnFinTrabajo.Source = "iconofintrabajook.png";
            BtnFinTrabajo.IsEnabled = false;
        }
        public void BtnDesayunoOn()
        {
            BtnDesayuno.Source = "iconodesayunoon.png";
            BtnDesayuno.IsEnabled = false;
        }
        public void BtnFinDesayunoOn()
        {
            BtnFinDesayuno.Source = "iconofincafeok.png";
            BtnFinDesayuno.IsEnabled = false;
        }
        public void BtnMedicoOn()
        {
            BtnMedico.Source = "iconomedicoon.png";
            BtnMedico.IsEnabled = false;
        }
        public void BtnFinMedicoOn()
        {
            BtnFinMedico.Source = "iconofinmedicook.png";
            BtnFinMedico.IsEnabled = false;
        }

        public void BtnDiaOff()
        {
            FicharDia.Source = "iconoFicharoff.png";
            FicharDia.IsEnabled = false;
        }
        public void BtnTrabajoOff()
        {
            BtnTrabajo.Source = "iconotrabajooff.png";
            BtnTrabajo.IsEnabled = false;
        }
        public void BtnFinTrabajoOff()
        {
            BtnFinTrabajo.Source = "iconofintrabajooff.png";
            BtnFinTrabajo.IsEnabled = false;
        }
        public void BtnDesayunoOff()
        {
            BtnDesayuno.Source = "iconodesayunooff.png";
            BtnDesayuno.IsEnabled = false;
        }
        public void BtnFinDesayunoOff()
        {
            BtnFinDesayuno.Source = "iconofincafeoff.png";
            BtnFinDesayuno.IsEnabled = false;
        }
        public void BtnMedicoOff()
        {
            BtnMedico.Source = "iconomedicooff.png";
            BtnMedico.IsEnabled = false;
        }
        public void BtnFinMedicoOff()
        {
            BtnFinMedico.Source = "iconofinmedicooff.png";
            BtnFinMedico.IsEnabled = false;
        }
    }
}