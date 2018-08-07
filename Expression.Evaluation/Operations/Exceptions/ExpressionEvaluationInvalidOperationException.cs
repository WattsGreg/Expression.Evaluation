using System;
using System.Collections.Generic;
using System.Text;

namespace Expression.Evaluation.Operations.Exceptions
{
    public class ExpressionEvaluationInvalidOperationException : Expression.Evaluation.Exceptions.BaseExpressionEvaluationException
    {
        public ExpressionEvaluationInvalidOperationException(string userFriendlyMessage) : base(userFriendlyMessage)
        {
        }
    }
}
