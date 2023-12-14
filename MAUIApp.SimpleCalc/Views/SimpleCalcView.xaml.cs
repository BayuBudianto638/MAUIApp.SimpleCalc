using MAUIApp.SimpleCalc.ViewModels;

namespace MAUIApp.SimpleCalc.Views;

public partial class SimpleCalcView : ContentPage
{
	public SimpleCalcView()
	{
		InitializeComponent();

        this.BindingContext = new SimpleCalcViewModel();
    }
}