using MAUIApp.SimpleCalc.Views;

namespace MAUIApp.SimpleCalc
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new SimpleCalcView();
        }
    }
}
