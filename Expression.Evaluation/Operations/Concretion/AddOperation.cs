﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Evaluation.Operations.Concretion
{
    public class AddOperation : Contracts.IOperation
    {
        public decimal ApplyOperation(decimal first, decimal second)
        {
            return first + second;
        }
    }
}
