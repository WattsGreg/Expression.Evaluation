using System;
using System.Collections.Generic;
using System.Text;

namespace Expression.Evaluation.Expressions.Exceptions
{
    public class ExpressionException : Expression.Evaluation.Exceptions.BaseExpressionEvaluationException
    {
        public ExpressionException(string userFriendlyMessage) : base(userFriendlyMessage)
        {
        }
    }
}
