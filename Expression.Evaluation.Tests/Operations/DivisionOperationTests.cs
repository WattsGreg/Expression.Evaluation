using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Evaluation.Tests.Operations
{
    public class DivisionOperationTests
    {
        Expression.Evaluation.Operations.Concretion.DivisionOperation DivisionOperationService;

        [SetUp]
        public void Setup()
        {
            DivisionOperationService = new Evaluation.Operations.Concretion.DivisionOperation();
        }

        [Test]
        [Category("DivisionOperationService, SuccessPath")]
        public void DivisionOperationService_ApplyOperaion_Success()
        {
            decimal result = DivisionOperationService.ApplyOperation(1, 2);
            Assert.AreEqual(0.5, result);
        }

        [Test]
        [Category("DivisionOperationService, FailurePath")]
        public void DivisionOperationService_ApplyOperation_DivideByZero()
        {   
            Assert.Throws<Expression.Evaluation.Operations.Exceptions.ExpressionEvaluationInvalidOperationException>(() => DivisionOperationService.ApplyOperation(1, 0));
        }
    }
}
