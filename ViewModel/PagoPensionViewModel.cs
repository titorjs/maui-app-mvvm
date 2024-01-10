using MVVMauiApp.Model;
using MVVMauiApp.Service;
using MVVMauiApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMauiApp.ViewModel
{
    public class PagoPensionViewModel
    {
        private Estudiante ingreso;
        private readonly ApiService _APIService;
        private Global sistema;
        private Pago pagoActual;
        public PagoPensionViewModel(Estudiante estudiante, ApiService apiService)
        {
            ingreso = estudiante;
            _APIService = apiService;
        }

        public async void OnPageAppearing(Label cuotaSistema, Label cuotaEstudiante, Label AldiaONo)
        {
            sistema = await _APIService.obtenerCuota();
            List<Pago> pagosEstudiante = await _APIService.GetPagosEstudiante(ingreso.Est_id);
            pagoActual = pagosEstudiante.Last();
            cuotaSistema.Text = sistema.Glo_valor.ToString();
            cuotaEstudiante.Text = pagoActual.Pag_cuota.ToString();

            int diferencia = sistema.Glo_valor - pagoActual.Pag_cuota;
            if (diferencia <= 0)
            {
                diferencia = 0;
                AldiaONo.Text = "¡Estás al día en el pago de Pensión!";
            }
            else
            {
                AldiaONo.Text = "Tienes pendiente el pago de " + diferencia + " cuota/s";
            }
        }

        public ICommand OnClickRegresarMenu =>
            new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            });

        public ICommand OnClickPagarPension =>
            new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new DatoTarjetaPage(ingreso, _APIService, sistema, pagoActual));
            });
    }
}
