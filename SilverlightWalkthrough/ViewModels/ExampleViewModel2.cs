namespace SilverlightWalkthrough.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using SilverlightWalkthrough.Models;

    public class ExampleViewModel2 : INotifyPropertyChanged
    {
        private ExampleModel selectedModel;

        public ExampleViewModel2()
        {
            IExampleService service = new MockExampleService();
            this.ExampleModels = service.FindExampleModels();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<ExampleModel> ExampleModels { get; set; }


        public ExampleModel SelectedModel
        {
            get
            {
                return this.selectedModel;
            }

            set
            {
                if (this.selectedModel != value)
                {
                    this.selectedModel = value;
                    this.OnPropertyChanged("SelectedModel");
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}