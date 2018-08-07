using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expression.Evaluation.Expressions
{
    public class EvaluationService : Contract.IEvaluation
    {
        //Create list of allowed characters.
        private List<char> AllowedCharacters = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '(', ')', '*', '/', '+', '-' };
        private List<char> AllowedNumbers = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private List<char> AllowedOperations = new List<char>() { '*', '/', '+', '-' };

        //Operations
        private readonly Operations.Contracts.IOperation _additionOperationService;
        private readonly Operations.Contracts.IOperation _subtractionOperationService;
        private readonly Operations.Contracts.IOperation _divisionOperationService;
        private readonly Operations.Contracts.IOperation _multiplyOperationService;

        public EvaluationService(Operations.Contracts.IOperation additionOperationService, 
            Operations.Contracts.IOperation subtractionOperationService, 
            Operations.Contracts.IOperation divisionOperationService,
            Operations.Contracts.IOperation multiplyOperationService)
        {
            _additionOperationService = additionOperationService;
            _subtractionOperationService = subtractionOperationService;
            _divisionOperationService = divisionOperationService;
            _multiplyOperationService = multiplyOperationService;
        }

        public bool EvaluateExpression(string expression, out decimal? result)
        {
            try
            {
                //Tidy up the expression
                expression = TidyExpression(expression);

                //Check a valid expression was provided
                if (!CheckValidStringProvided(expression))
                {
                    result = null;
                    return false;
                }

                //Format our expression
                expression = FormatExpression(expression);

                //Perform the calculation
                result = Calculate(expression);

                return true;
            }
            catch (Exception)
            {
                //TODO: Log what happened
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Tidy a provided string for processing
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string TidyExpression(string expression)
        {
            //Trim the expression of white space before and after first character
            expression = expression.Trim();

            //Replace any whitespace in the expression
            expression = expression.Replace(" ", String.Empty);

            return expression;
        }

        /// <summary>
        /// Check that a provided expression is valid
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private bool CheckValidStringProvided(string expression)
        {
            //Loop through each character in the expression
            foreach (char character in expression)
            {
                //If the character is not contained in the list of allowed characters, then an invalid string has been provided.
                if (!AllowedCharacters.Contains(character))
                {

                    throw new Exceptions.ExpressionException("Invalid characters were found in the provided expression");
                }
            }            

            //Check that the number of open and close brackets match. This is crude first step check before any processing begins.

            //Grab the number of open brackets
            int openBracketCount = expression.Count(x => x == '(');

            //Grab the number of closed brackets
            int closeBracketCount = expression.Count(x => x == ')');

            if (openBracketCount != closeBracketCount)
            {
                throw new Exceptions.ExpressionException("An invalid number of brackets were evaluated in the provided expression");
            }

            return true;
        }        

        /// <summary>
        /// Formats an expression by expanding all bracketed operations
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string FormatExpression(string expression)
        {
            bool bracketsExpanded = false;
            while (!bracketsExpanded)
            {
                if (expression.Contains('('))
                {
                    //Grab the last closing bracket
                    int openBracketPosition = expression.LastIndexOf('(');

                    //Get a substring from the last open bracket to the end of the string
                    string temporaryExpression = expression.Substring(openBracketPosition + 1, expression.Length - 1 - openBracketPosition);

                    //Get the first closing bracket position
                    int closeBracketPosition = temporaryExpression.IndexOf(')');

                    //Get the expression evaluated between the opening and closing brackets
                    temporaryExpression = temporaryExpression.Substring(0, closeBracketPosition);

                    string firstOperationNumber = string.Empty;
                    string secondOperationNumber = string.Empty;
                    string operationModifier = string.Empty;
                    foreach (var c in temporaryExpression)
                    {
                        //If allowed numbers contains our iteration character
                        if (AllowedNumbers.Contains(c))
                        {
                            //If the operation has been added, then we can add the number to the second operation number
                            if (!string.IsNullOrEmpty(operationModifier))
                            {
                                secondOperationNumber += c.ToString();
                            }
                            ///No operation yet specified, so we're adding this to the first
                            else
                            {
                                firstOperationNumber += c.ToString();
                            }
                        }
                        //If the allowed operations contains the iteration character, then set it
                        else if (AllowedOperations.Contains(c))
                        {
                            operationModifier = c.ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(firstOperationNumber) && !string.IsNullOrEmpty(secondOperationNumber) && !string.IsNullOrEmpty(operationModifier))
                    {
                        int first = System.Convert.ToInt32(firstOperationNumber);
                        int second = System.Convert.ToInt32(secondOperationNumber);
                        decimal value = 0;

                        //Based on which operator is being used, we need to perform something
                        switch (operationModifier)
                        {
                            case "+":
                                value = _additionOperationService.ApplyOperation(first, second);
                                break;
                            case "-":
                                value = _subtractionOperationService.ApplyOperation(first, second);
                                break;
                            case "*":
                                value = _multiplyOperationService.ApplyOperation(first, second);
                                break;
                            case "/":
                                value = _divisionOperationService.ApplyOperation(first, second);
                                break;
                        }

                        //Replace any matching bracketed operations with the evaluated amount
                        expression = expression.Replace(string.Format("({0})", temporaryExpression), value.ToString());
                    }
                }
                else
                {
                    //Brackets have been evaluated, now we can perform the remaining operations
                    bracketsExpanded = true;
                }
            }

            return expression;
        }

        /// <summary>
        /// Calulate the output of an expression
        /// </summary>
        /// <param name="expression">The expression to evaluate</param>
        /// <returns>The value the expression equates to</returns>
        private decimal Calculate(string expression)
        {
            //Grab the order of operations for the expression
            List<string> orderOfOperations = new List<string>();
            bool lastCharacterWasOperation = true;
            foreach (char c in expression)
            {
                //If the character is an operation, add it as an operation
                if (AllowedOperations.Contains(c))
                {
                    orderOfOperations.Add(c.ToString());
                    lastCharacterWasOperation = true;
                }
                else
                {
                    //If the last character added was an operation, we need to add a new number to the order of operations
                    if (lastCharacterWasOperation)
                    {
                        orderOfOperations.Add(c.ToString());
                        lastCharacterWasOperation = false;
                    }
                    //If the last character added was a number, then grab that and append this number to it (i.e 1 becomes 13)
                    else
                    {
                        orderOfOperations[orderOfOperations.Count - 1] += c;
                    }
                }
            }

            decimal result = 0;
            string nextOperation = string.Empty;
            for (int i = 0; i < orderOfOperations.Count; i++)
            {
                //Every other step will be an even number, which will divide by 2 with no remainder,
                if (i % 2 == 0)
                {
                    if (i == 0)
                    {
                        //If first number add straight to result
                        result = System.Convert.ToDecimal(orderOfOperations[i]);
                    }
                    else
                    {
                        //Pull the amount off the list to apply to the total result
                        decimal operationAmount = System.Convert.ToDecimal(orderOfOperations[i]);

                        //Based on which operator is being used, we need to perform an action on the two values
                        switch (nextOperation)
                        {
                            case "+":
                                result = _additionOperationService.ApplyOperation(result, operationAmount);
                                break;
                            case "-":
                                result = _subtractionOperationService.ApplyOperation(result, operationAmount);
                                break;
                            case "*":
                                result = _multiplyOperationService.ApplyOperation(result, operationAmount);
                                break;
                            case "/":
                                result = _divisionOperationService.ApplyOperation(result, operationAmount);
                                break;
                        }
                    }
                }
                else
                {
                    //The step is odd, so it's a calculation
                    nextOperation = orderOfOperations[i];
                }
            }

            return result;
        }
    }
}
