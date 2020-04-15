namespace SilverlightWalkthrough.ViewModels
{
    using SilverlightWalkthrough.Models;

    public class WrapperViewModel
    {
        public ExampleModel ExampleModel { get; set; }

        public double Difference
        {
            get
            {
                return ExampleModel.First - ExampleModel.Second;
            }
        }
    }
}