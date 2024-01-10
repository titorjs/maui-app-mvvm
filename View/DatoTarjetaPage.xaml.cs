using MVVMauiApp.Model;
using MVVMauiApp.Service;
using MVVMauiApp.ViewModel;

namespace MVVMauiApp.View;

public partial class DatoTarjetaPage : ContentPage
{
    private Estudiante ingreso;
    private readonly ApiService _APIService;
    private Global sistema;
    private Pago pagoActual;
    private Pension pension;
    public DatoTarjetaPage(Estudiante estudiante, ApiService apiService, Global global, Pago pago)
	{
		InitializeComponent();
        ingreso=estudiante;
        _APIService = apiService;
        sistema = global;
        pagoActual= pago;
        BindingContext = new DatoTarjetaViewModel(estudiante, apiService, global, pago);
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        ((DatoTarjetaViewModel)BindingContext).OnPageAppearing(cuotaSistema, cuotaEstudiante, montoPagar, numeroCuota);
    }

    private async void OnClickPagarCuota(object sender, EventArgs e)
    {
        string numT = numeroTarjeta.Text, nombreT=nombreTarjeta.Text, fechaT=fechaTarjeta.Text,
            codigoT=codigoTarjeta.Text, cuotaT = cuotaPagar.Text;
        ((DatoTarjetaViewModel)BindingContext).OnClickPagarCuota(numT, nombreT, fechaT, codigoT, cuotaT);
    }
}