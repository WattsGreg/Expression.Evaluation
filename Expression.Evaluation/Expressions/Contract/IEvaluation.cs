using System;
using System.Collections.Generic;
using System.Text;

namespace Expression.Evaluation.Expressions.Contract
{
    public interface IEvaluation
    {
        /// <summary>
        /// Evaluate an expression, to return a decimal representing the amount the expression equates to. 
        /// </summary>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="result">OUTPUT: value of expression after evaluation. null if error</param>
        /// <returns>Returns true if evaluated successfully, false if an error occurrs</returns>
        bool EvaluateExpression(string expression, out decimal? result);
    }
}
