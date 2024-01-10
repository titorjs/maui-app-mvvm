using MVVMauiApp.Model;
using MVVMauiApp.Service;
using MVVMauiApp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMauiApp.ViewModel
{
    public class ListaPagosViewModel
    {
        private Estudiante ingreso;
        private readonly ApiService _APIService;
        public ListaPagosViewModel(Estudiante estudiante, ApiService apiService)
        {
            ingreso = estudiante;
            _APIService = apiService;
        }

        public async Task<List<PagoPension>> OnPageAppearing()
        {
            try
            {
                List<Pago> listapagos = await _APIService.GetPagosEstudiante(ingreso.Est_id);
                List<Pension> listaPension = new List<Pension>();
                foreach (Pago pago in listapagos)
                {
                    Pension p = await _APIService.GetPension(pago.Pension);
                    if (p != null)
                    {
                        listaPension.Add(p);
                    }
                }
                List<PagoPension> listaCombinada = new List<PagoPension>();
                foreach (Pago pago in listapagos)
                {
                    Pension pension = listaPension.FirstOrDefault(p => p.Pen_id == pago.Pension);
                    if (pension != null)
                    {
                        PagoPension viewModel = new PagoPension
                        {
                            Pago = pago,
                            Pension = pension
                        };
                        listaCombinada.Add(viewModel);
                    }
                }
                return listaCombinada;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ICommand OnClickRegresarMenu =>
            new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            });
    }
}
