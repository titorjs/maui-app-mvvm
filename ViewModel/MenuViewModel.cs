using MVVMauiApp.Model;
using MVVMauiApp.Service;
using MVVMauiApp.View;
using System.Windows.Input;

namespace MVVMauiApp.ViewModel
{
    public class MenuViewModel
    {
        ApiService _ApiService;
        private Estudiante ingreso;

        public MenuViewModel(Estudiante estudiante, ApiService apiservice) 
        {
            ingreso = estudiante;
            _ApiService = apiservice;
        }

        public ICommand OnClickListaPagos =>
           new Command(async () =>
           {
               await App.Current.MainPage.Navigation.PushModalAsync(new ListaPagosPage(ingreso, _ApiService));
           });

        public ICommand OnClickCuenta =>
           new Command(async () =>
           {
               await App.Current.MainPage.Navigation.PushModalAsync(new CuentaPage(ingreso, _ApiService));
           });

        public ICommand OnClickSalir =>
            new Command(async () =>
            {
                bool confirmacionCerrarSesion = await App.Current.MainPage.DisplayAlert("Confirmación", "¿Desea cerrar sesión?", "Sí", "No");
                if (confirmacionCerrarSesion)
                {
                    SecureStorage.Remove("estudiante");
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
            });

        public ICommand OnClickPagoPension =>
              new Command(async () =>
              {
                  await App.Current.MainPage.Navigation.PushModalAsync(new PagoPensionPage(ingreso, _ApiService));
              });
    }
}
