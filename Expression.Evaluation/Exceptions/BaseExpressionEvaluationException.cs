using System;
using System.Collections.Generic;
using System.Text;

namespace Expression.Evaluation.Exceptions
{
    public abstract class BaseExpressionEvaluationException : Exception 
    {

        public BaseExpressionEvaluationException(string userFriendlyMessage)
        {
            UserFriendlyMessage = userFriendlyMessage;
        }

        public string UserFriendlyMessage { get; set; }
    }
}
