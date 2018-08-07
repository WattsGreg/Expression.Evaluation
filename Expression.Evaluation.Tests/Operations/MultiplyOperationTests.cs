using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Evaluation.Tests.Operations
{
    public class MultiplyOperationTests
    {
        Expression.Evaluation.Operations.Concretion.MultiplyOperation MultiplyOperationService;

        [SetUp]
        public void Setup()
        {
            MultiplyOperationService = new Evaluation.Operations.Concretion.MultiplyOperation();
        }

        [Test]
        [Category("MultiplyOperationService, SuccessPath")]
        public void MultiplyOperationService_ApplyOperaion_Success()
        {
            decimal result = MultiplyOperationService.ApplyOperation(80, 2);
            Assert.AreEqual(160, result);
        }
    }
}
