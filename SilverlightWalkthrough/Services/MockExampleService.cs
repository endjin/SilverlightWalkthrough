namespace SilverlightWalkthrough
{
    using System;
    using System.Collections.Generic;

    using SilverlightWalkthrough.Models;

    public class MockExampleService : IExampleService
    {
        private readonly Random random = new Random();

        public ExampleModel FindExampleModel()
        {
            return new ExampleModel { First = this.random.NextDouble() * 20, Second = this.random.NextDouble() * 20 };
        }

        public IEnumerable<ExampleModel> FindExampleModels()
        {
            List<ExampleModel> models = new List<ExampleModel>
                {
                    this.FindExampleModel(), 
                    this.FindExampleModel(), 
                    this.FindExampleModel(), 
                    this.FindExampleModel()
                };
            return models;
        }
    }
}