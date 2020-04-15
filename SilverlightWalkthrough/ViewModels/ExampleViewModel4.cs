namespace SilverlightWalkthrough.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    using SilverlightWalkthrough.Commands;
    using SilverlightWalkthrough.EntityBases;
    using SilverlightWalkthrough.Models;

    public class ExampleViewModel4 : NotifyPropertyChangedBase
    {
        private ExampleModel selectedModel;
        private BackgroundWorker worker;
        private IEnumerable<ExampleModel> exampleModels;
        private RelayCommand myCommand;

        public ExampleViewModel4()
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
                    this.OnPropertyChanged(() => this.ExampleModels);
                }
            }
        }

        public ICommand MyCommand
        {
            get
            {
                if (myCommand == null)
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
                    this.OnPropertyChanged(() => this.SelectedModel);
                    this.OnPropertyChanged(() => this.SelectedViewModel);
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