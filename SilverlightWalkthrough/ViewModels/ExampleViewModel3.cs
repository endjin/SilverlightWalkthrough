namespace SilverlightWalkthrough.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    using SilverlightWalkthrough.Commands;
    using SilverlightWalkthrough.Models;

    public class ExampleViewModel3 : INotifyPropertyChanged
    {
        private ExampleModel selectedModel;

        private BackgroundWorker worker;

        private IEnumerable<ExampleModel> exampleModels;

        private RelayCommand myCommand;

        public ExampleViewModel3()
        {
            IEnumerable<ExampleModel> models = null;

            this.worker = new BackgroundWorker();
            this.worker.DoWork += (s, e) =>
                {
                    IExampleService service = new MockExampleService();
                    models = service.FindExampleModels();
                };

            this.worker.RunWorkerCompleted += (s, e) =>
                {
                    if (e.Error != null)
                    {
                        throw e.Error;
                    }
                    this.ExampleModels = models;
                };
            this.worker.RunWorkerAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public IEnumerable<ExampleModel> ExampleModels
        {
            get
            {
                return this.exampleModels;
            }
            set
            {
                if (this.exampleModels != value)
                {
                    this.exampleModels = value;
                    this.OnPropertyChanged("ExampleModels");
                }
            }
        }

        public ICommand MyCommand
        {
            get
            {
                if( myCommand == null)
                {
                    this.myCommand = new RelayCommand(ExecuteMyCommand, CanExecuteMyCommand);
                }
                return myCommand;
            }
        }

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
                    this.OnPropertyChanged("SelectedViewModel");
                }
            }
        }

        public WrapperViewModel SelectedViewModel
        {
            get
            {
                return (this.SelectedModel == null) ? null :
                                                      new WrapperViewModel { ExampleModel = this.SelectedModel };
            }
        }


        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool CanExecuteMyCommand()
        {
            return this.SelectedModel != null;
        }

        private static void ExecuteMyCommand()
        {
            MessageBox.Show("Executed the command!");
        }

    }
}