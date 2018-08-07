using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Evaluation.Operations.Concretion
{
    public class DivisionOperation : Contracts.IOperation
    {   
        public decimal ApplyOperation(decimal first, decimal second)
        {
            if(second == 0)
            {
                throw new Exceptions.ExpressionEvaluationInvalidOperationException("An invalid division attempt was made when attempting to divide by zero.");
            }

            return first / second;
        }
    }
}
