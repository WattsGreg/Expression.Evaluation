using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Evaluation.Tests.Operations
{
    public class SubtractOperationTests
    {
        Expression.Evaluation.Operations.Concretion.SubtractionOperation SubtractOperationService;

        [SetUp]
        public void Setup()
        {
            SubtractOperationService = new Evaluation.Operations.Concretion.SubtractionOperation();
        }

        [Test]
        [Category("SubtractOperationService, SuccessPath")]
        public void SubtractOperationService_ApplyOperaion_Success()
        {
            decimal result = SubtractOperationService.ApplyOperation(80, 2);
            Assert.AreEqual(78, result);
        }
    }
}
