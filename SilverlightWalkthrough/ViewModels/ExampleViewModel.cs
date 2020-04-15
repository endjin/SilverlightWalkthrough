namespace SilverlightWalkthrough.ViewModels
{
    using System.Collections.Generic;

    using SilverlightWalkthrough.Models;

    public class ExampleViewModel
    {
        public ExampleViewModel()
        {
            IExampleService service = new MockExampleService();
            this.ExampleModels = service.FindExampleModels();
        }

        public IEnumerable<ExampleModel> ExampleModels { get; set; }

        public ExampleModel SelectedModel { get; set; }
    }
}