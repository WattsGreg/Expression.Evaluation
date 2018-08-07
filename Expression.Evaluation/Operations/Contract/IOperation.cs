using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Evaluation.Operations.Contracts
{
    public interface IOperation
    {
        /// <summary>
        /// Apply an operation to two decimals
        /// </summary>
        /// <param name="first">The first number to apply the operation to</param>
        /// <param name="second">The second number to act as the operation amount</param>
        /// <returns></returns>
        decimal ApplyOperation(decimal first, decimal second);
    }
}
