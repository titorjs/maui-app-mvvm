using MVVMauiApp.Model;
using MVVMauiApp.Service;
using MVVMauiApp.ViewModel;

namespace MVVMauiApp.View;

public partial class MenuPage : ContentPage
{
    private Estudiante ingreso;

    public MenuPage(Estudiante estudiante, ApiService apiservice)
	{
		InitializeComponent();
        ingreso= estudiante;
        BindingContext = new MenuViewModel(estudiante, apiservice);
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Nombre.Text = ingreso.Est_nombre;
    }
}