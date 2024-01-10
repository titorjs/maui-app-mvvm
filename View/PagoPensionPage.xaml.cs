using MVVMauiApp.Model;
using MVVMauiApp.Service;
using MVVMauiApp.ViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace MVVMauiApp.View;

public partial class PagoPensionPage : ContentPage
{
    private Estudiante ingreso;
    private readonly ApiService _APIService;
    private Global sistema;
    private Pago pagoActual;
    public PagoPensionPage(Estudiante estudiante, ApiService apiService)
	{
		InitializeComponent();
        BindingContext = new PagoPensionViewModel(estudiante, apiService);
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        ((PagoPensionViewModel)BindingContext).OnPageAppearing(cuotaSistema, cuotaEstudiante, AldiaONo);
        
    }
}