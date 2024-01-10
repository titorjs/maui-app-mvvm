using MVVMauiApp.Model;
using MVVMauiApp.Service;
using MVVMauiApp.ViewModel;
using Newtonsoft.Json;

namespace MVVMauiApp.View;

public partial class LoginPage : ContentPage
{
	public LoginPage(ApiService apiservice)
	{
		InitializeComponent();
        BindingContext = new LoginViewModel(apiservice);
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        id.Text = string.Empty;
        contrasenia.Text = string.Empty;
        ((LoginViewModel)BindingContext).OnPageAppearing();
    }
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string contra = contrasenia.Text;
        string idt = id.Text;

        ((LoginViewModel)BindingContext).OnLoginClicked(contra, idt);

        contrasenia.Text = "";
    }
}