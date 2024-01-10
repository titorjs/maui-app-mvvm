using MVVMauiApp.Model;
using MVVMauiApp.Service;
using MVVMauiApp.ViewModel;

namespace MVVMauiApp.View;

public partial class ContraseniaNuevaPage : ContentPage
{
    private Estudiante ingreso;
    private readonly ApiService _APIService;

    public ContraseniaNuevaPage(Estudiante estudiante, ApiService apiservice)
	{
		InitializeComponent();
        BindingContext = new ContraseniaNuevaViewModel(estudiante, apiservice);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((ContraseniaNuevaViewModel)BindingContext).OnPageAppearing(Contrasenia);
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        string contr1 = Contr1.Text;
        string contr2 = Contr2.Text;
        string actual = Contrasenia.Text;
        ((ContraseniaNuevaViewModel)BindingContext).OnGuardarClicked(contr1, contr2, actual);
    }
}