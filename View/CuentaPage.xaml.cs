using MVVMauiApp.Model;
using MVVMauiApp.Service;
using MVVMauiApp.ViewModel;

namespace MVVMauiApp.View;

public partial class CuentaPage : ContentPage
{
	public CuentaPage(Estudiante estudiante, ApiService apiservice)
	{
		InitializeComponent();
        BindingContext = new CuentaViewModel(estudiante, apiservice);
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((CuentaViewModel)BindingContext).OnPageAppearing(Nombre, ID, Cedula, Direccion);
    }
}