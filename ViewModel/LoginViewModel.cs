using MVVMauiApp.Model;
using MVVMauiApp.Service;
using MVVMauiApp.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMauiApp.ViewModel
{
    public class LoginViewModel
    {
        private readonly ApiService _APIService;
        public LoginViewModel(ApiService apiservice)
        {
            _APIService = apiservice;
        }

        public async void OnPageAppearing()
        {

            try
            {
                string storedJson = SecureStorage.GetAsync("estudiante").Result;

                if (storedJson != null)
                {
                    Estudiante estudiante = JsonConvert.DeserializeObject<Estudiante>(storedJson);
                    bool ingreso = await _APIService.Login(estudiante);
                    if (ingreso)
                    {
                        await App.Current.MainPage.Navigation.PushModalAsync(new MenuPage(estudiante, _APIService));
                    }
                    else
                    {
                        SecureStorage.Remove("estudiante");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async void OnLoginClicked(string contra, string idt)
        {
            if (string.IsNullOrEmpty(idt) || string.IsNullOrEmpty(contra))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Llene todos los campos", "Ok");
                return;
            }

            if (!int.TryParse(idt, out int ID))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Seleccione un ID válido", "Ok");
                return;
            }

            try
            {
                Estudiante existe = await _APIService.GetEstudiante(ID);
                existe.contrasenia = contra;
                bool ingreso = await _APIService.Login(existe);
                if (!ingreso)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Contraseña Incorrecta", "Ok");
                }
                else
                {
                    string estudiante = JsonConvert.SerializeObject(existe);
                    SecureStorage.SetAsync("estudiante", estudiante);
                    await App.Current.MainPage.Navigation.PushModalAsync(new MenuPage(existe, _APIService));
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "El usuario no existe", "Ok");
            }
        }
    }   
}
