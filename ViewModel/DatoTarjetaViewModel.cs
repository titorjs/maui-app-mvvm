using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using MVVMauiApp.Model;
using MVVMauiApp.Service;

namespace MVVMauiApp.ViewModel
{
    public class DatoTarjetaViewModel
    {
        private Estudiante ingreso;
        private readonly ApiService _APIService;
        private Global sistema;
        private Pago pagoActual;
        private Pension pension;
        private Label cuotaSistema;

        public DatoTarjetaViewModel(Estudiante estudiante, ApiService apiService, Global global, Pago pago)
        {
            ingreso = estudiante;
            _APIService = apiService;
            sistema = global;
            pagoActual = pago;
        }

        public async void OnPageAppearing(Label cuotaSistema, Label cuotaEstudiante, Label montoPagar, Label numeroCuota)
        {
            cuotaSistema.Text = sistema.Glo_valor.ToString();
            cuotaEstudiante.Text = pagoActual.Pag_cuota.ToString();
            pension = await _APIService.GetPension(pagoActual.Pension);
            int diferencia = sistema.Glo_valor - pagoActual.Pag_cuota;
            float total = 0;
            if (diferencia < 0)
            {
                diferencia = 0;
            }
            if (pension != null)
            {
                total = pension.Pen_valor * diferencia;
            }
            montoPagar.Text = total.ToString();
            numeroCuota.Text = diferencia.ToString();
        }

        public ICommand OnClickRegresarMenu =>
            new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            });

        public async void OnClickPagarCuota(string numT, string nombreT, string fechaT, string codigoT, string cuotaT)
        {
            int cuotaSistema = sistema.Glo_valor, cuotaEstudiante = pagoActual.Pag_cuota;
            int diferencia = cuotaSistema - cuotaEstudiante;
            if (string.IsNullOrEmpty(numT) || string.IsNullOrEmpty(nombreT) ||
                string.IsNullOrEmpty(fechaT) || string.IsNullOrEmpty(codigoT))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Llene todos los campos", "Ok");
                return;
            }

            if (!int.TryParse(cuotaT, out int cuotaP))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Seleccione un valor para la cuota", "Ok");
                return;
            }

            if (numT.Length < 10)
            {
                await App.Current.MainPage.DisplayAlert("Error", "El número de tarjeta debe tener 16 dígitos", "Ok");
                return;
            }

            if (codigoT.Length < 3)
            {
                await App.Current.MainPage.DisplayAlert("Error", "El código de seguridad debe tener 3 dígitos", "Ok");
                return;
            }

            if (diferencia <= 0)
            {
                await App.Current.MainPage.DisplayAlert("Notificacion", "Usted está al día en el pago de las cuotas", "Ok");
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
            else if (diferencia < cuotaP)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No puede ingresar un número de cuota mayor a las pendientes", "Ok");
                return;
            }
            else if (cuotaP == 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Ingrese un valor mayor a 0", "Ok");
                return;
            }
            else
            {
                bool confirmarPago = await App.Current.MainPage.DisplayAlert("Confirmación", "¿Desea proceder con el pago?", "Sí", "No");
                if (confirmarPago)
                {
                    List<Pago> pagos = await _APIService.pagar(ingreso.Est_id, cuotaP);
                    await App.Current.MainPage.DisplayAlert("Aprobado", "Se registró correctamente su transacción", "Ok");
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Notificación", "Transacción rechazada", "Ok");
                    App.Current.MainPage.Navigation.PopModalAsync();
                }
            }
        }



    }
}
