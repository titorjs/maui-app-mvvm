using MVVMauiApp.Service;
using MVVMauiApp.Utils;
using MVVMauiApp.Model;
using System.Collections.ObjectModel;
using MVVMauiApp.ViewModel;

namespace MVVMauiApp.View;

public partial class ListaPagosPage : ContentPage
{
    public ListaPagosPage(Estudiante estudiante, ApiService apiService)
	{
		InitializeComponent();
        BindingContext = new ListaPagosViewModel(estudiante, apiService);
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        List<PagoPension> listaCombinada = await ((ListaPagosViewModel)BindingContext).OnPageAppearing();

        if(listaCombinada != null)
        {
            var pagos = new ObservableCollection<PagoPension>(listaCombinada);
            pagosListView.ItemsSource = pagos;
        } else
        {
            await DisplayAlert("Error", "Error al cargar los pagos del estudiante", "Ok");
            await Navigation.PopModalAsync();
        }
    }
}