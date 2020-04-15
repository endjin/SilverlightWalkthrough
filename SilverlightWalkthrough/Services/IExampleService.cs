namespace SilverlightWalkthrough
{
    using System.Collections.Generic;

    using SilverlightWalkthrough.Models;

    public interface IExampleService
    {
        ExampleModel FindExampleModel();

        IEnumerable<ExampleModel> FindExampleModels();
    }
}