using MVVMauiApp.Model;
using MVVMauiApp.Service;
using System.Windows.Input;

namespace MVVMauiApp.ViewModel
{
    public class ContraseniaNuevaViewModel
    {
        private Estudiante ingreso;
        private readonly ApiService _APIService;

        public ContraseniaNuevaViewModel(Estudiante estudiante, ApiService apiservice)
        {
            ingreso = estudiante;
            _APIService = apiservice;
        }

        public void OnPageAppearing(Entry Contrasenia)
        {
            Contrasenia.Text = ingreso.contrasenia.ToString();
        }

        public async void OnGuardarClicked(string contr1, string contr2, string actual)
        {
            const int longitudMinima = 8;

            if (string.IsNullOrWhiteSpace(contr1) || string.IsNullOrWhiteSpace(contr2))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Llene todos los campos", "Ok");
            }
            else if (contr1.Length < longitudMinima || contr2.Length < longitudMinima)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"La longitud mínima de la contraseña es {longitudMinima} caracteres", "Ok");
            }
            else if (contr1.Equals(contr2))
            {
                if (contr1.Equals(actual))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Ingrese una nueva contraseña", "Ok");
                    return;
                }
                try
                {
                    bool contraseniaNueva = await _APIService.CambioContrasenia(ingreso, contr1);

                    if (contraseniaNueva)
                    {
                        ingreso.contrasenia = contr1;
                        await App.Current.MainPage.DisplayAlert("Aprobado", "Su contraseña se cambió con éxito", "Ok");
                        await App.Current.MainPage.Navigation.PopModalAsync();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Hubo un error en el sistema, intente nuevamente", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Problemas con servidor", "Ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Las contraseñas no coinciden", "Ok");
            }
        }

        public ICommand OnCancelarClicked =>
            new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            });
    }
}
