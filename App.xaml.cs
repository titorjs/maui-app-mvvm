using MVVMauiApp.Service;
using MVVMauiApp.View;

namespace MVVMauiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ApiService apiservice = new ApiService();
            MainPage = new LoginPage(apiservice);
        }
    }
}
