namespace SilverlightWalkthrough.Views
{
    using System.Windows;
    using System.Windows.Controls;

    using SilverlightWalkthrough.ViewModels;

    public partial class ExampleView2
    {
        public ExampleView2()
        {
            InitializeComponent();
            Loaded += this.ExampleViewLoaded;
        }

        private void ExampleViewLoaded(object sender, RoutedEventArgs e)
        {
            // DataContext = new ExampleViewModel3();

            DataContext = new ExampleViewModel4();
        }
    }
}
