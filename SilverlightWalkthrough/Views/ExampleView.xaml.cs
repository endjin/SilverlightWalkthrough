namespace SilverlightWalkthrough.Views
{
    using System.Windows;
    using System.Windows.Controls;

    using SilverlightWalkthrough.ViewModels;

    public partial class ExampleView
    {
        public ExampleView()
        {
            InitializeComponent();
            Loaded += this.ExampleViewLoaded;
        }

        private void ExampleViewLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new ExampleViewModel();

            // DataContext = new ExampleViewModel2();
        }
    }
}
