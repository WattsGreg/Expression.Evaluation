using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Evaluation.Tests.Operations
{
    public class AddOperationTests
    {
        Expression.Evaluation.Operations.Concretion.AddOperation AddOperationService;

        [SetUp]
        public void Setup()
        {
            AddOperationService = new Evaluation.Operations.Concretion.AddOperation();
        }

        [Test]
        [Category("AddOperationService, SuccessPath")]
        public void AddOperationService_ApplyOperation_Success()
        {
            decimal result = AddOperationService.ApplyOperation(1, 2);
            Assert.AreEqual(3, result);
        }
    }
}
