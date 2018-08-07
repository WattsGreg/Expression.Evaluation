using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Evaluation.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal? number1;
            decimal? number2;
            decimal? number3;
            decimal? number4;
            decimal? number5;

            //Grab an instance of the evaludation service
            Expression.Evaluation.Expressions.Contract.IEvaluation evaluationService = GetEvaluationService();

            //Perform examples (only outputting the evaluated amount if successful
            if(evaluationService.EvaluateExpression("1 + 3", out number1))
            {
                Console.WriteLine(number1);
            }
            else
            {
                Console.WriteLine("Error when preforming evaluation: ", "1 + 3");
            }

            if (evaluationService.EvaluateExpression("(1 + 3) + (1 + 3)", out number1))
            {
                Console.WriteLine(number1);
            }
            else
            {
                Console.WriteLine("Error when preforming evaluation: ", "1 + 3");
            }

            if (evaluationService.EvaluateExpression("(1 + 3) * 2", out number2))
            {
                Console.WriteLine(number2);
            }
            else
            {
                Console.WriteLine("Error when preforming evaluation: ", "1 + 3) * 2");
            }

            if (evaluationService.EvaluateExpression("(4 / 2) + 6", out number3))
            {
                Console.WriteLine(number3);
            }
            else
            {
                Console.WriteLine("Error when preforming evaluation: ", "(4 / 2) + 6");
            }

            if (evaluationService.EvaluateExpression("4 + (12 / (1 * 2))", out number4))
            {
                Console.WriteLine(number4);
            }
            else
            {
                Console.WriteLine("Error when preforming evaluation: ", "4 + (12 / (1 * 2))");
            }

            if (evaluationService.EvaluateExpression("(1 + (12 * 2)", out number5))
            {
                Console.WriteLine(number5);
            }
            else
            {
                Console.WriteLine("Error when preforming evaluation: ", "(1 + (12 * 2)");
            }

            Console.ReadLine();
        }   

        private static Expressions.Contract.IEvaluation GetEvaluationService()
        {
            //Return an implentation of the evaluation service. In the real world this could be swapped out and use an IoC container such as Ninject
            return new Expressions.EvaluationService(
                new Operations.Concretion.AddOperation(),
                new Operations.Concretion.SubtractionOperation(),
                new Operations.Concretion.DivisionOperation(),
                new Operations.Concretion.MultiplyOperation());
        }
    }
}
