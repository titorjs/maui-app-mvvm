using MVVMauiApp.Model;
using MVVMauiApp.Service;
using MVVMauiApp.View;
using System.Windows.Input;

namespace MVVMauiApp.ViewModel
{
    public class CuentaViewModel
    {
        private Estudiante ingreso;
        private readonly ApiService _ApiService;
        
        

        public CuentaViewModel(Estudiante estudiante, ApiService apiservice)
        {
            ingreso = estudiante;
            _ApiService = apiservice;
        }

        public void OnPageAppearing(Label Nombre, Label ID, Label Cedula, Label Direccion) { 
                Nombre.Text = ingreso.Est_nombre.ToString();
                ID.Text = ingreso.Est_id.ToString();
                Cedula.Text = ingreso.Est_cedula.ToString();
                Direccion.Text = ingreso.Est_direccion.ToString();
        }

        public ICommand OnClickRegresar =>
            new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            });

        public ICommand OnClickNuevaContrasenia =>
            new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new ContraseniaNuevaPage(ingreso, _ApiService));
            });
    }
}
