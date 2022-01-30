using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public interface IOperations
    {
        void Insert(string digit);
        void InsertOperation(Operations operation);
    }

    public enum Operations
    {
        Clear,
        Equal,
        Backspace,
        BracketOpen,
        BracketClouse
    }

    public class CalculatorModel : IOperations
    {
        public string Exp { get; private set; } = string.Empty;
        public string Result { get; private set; } = string.Empty;

        private const string OpenBracketSign = "(";
        private const string EncloseBracketSign = ")";

        private readonly Stack<string> encloseBracketStack;

        public CalculatorModel()
        {
            encloseBracketStack = new Stack<string>();
        }
        private void Clear()
        {
            Exp = string.Empty;
            Result = string.Empty;
        }
        private void Equal()
        {
            Exp = Result;
        }
        private void Backspace()
        {
            if (string.IsNullOrEmpty(Exp)) return;
            if (Regex.IsMatch(Exp, $@"\{EncloseBracketSign}&"))
                encloseBracketStack.Push(EncloseBracketSign);
            else if (Regex.IsMatch(Exp, $@"\{OpenBracketSign}"))
                encloseBracketStack.Pop();

            Exp = Exp.Remove(Exp.Length - 1, 1);
            Result = CalculateExpression();
        }


        public void Insert(string element)
        {
            if (Regex.IsMatch(element, @"[+\-*/,]"))
                Exp += string.IsNullOrEmpty(Exp) || Regex.IsMatch(Exp, @"[+\-*/,]$") ? string.Empty : element;
            else
                Exp += element;

            Result = Exp;
        }

        private void AddBracketOpen()
        {

        }
        private void AddBracketClose()
        {

        }

        public void InsertOperation(Operations operation)
        {
            switch (operation)
            {
                case Operations.Backspace:
                    Backspace();
                    break;
                case Operations.Clear:
                    Clear();
                    break;
                case Operations.BracketOpen:
                    AddBracketOpen();
                    break;
                case Operations.BracketClouse:
                    AddBracketClose();
                    break;
                case Operations.Equal:
                    Equal();
                    break;
            }
        }

        public string CalculateExpression()
        {
            if (string.IsNullOrEmpty(Exp) || !Regex.IsMatch(Exp.Last().ToString(), @"(\d|\))") || encloseBracketStack.Any())
                return string.Empty;
            Expression expression = new Expression(Exp);
            return expression.calculate().ToString(CultureInfo.InvariantCulture);
        }



    }


    //internal class Calc
    //{
    //    public static double Calcul(string exp)
    //    {
    //        char[] array = exp.ToCharArray();
    //        List<char> charList = new List<char>();
    //        charList.AddRange(array);
    //        List<string> priority1 = new List<string>();
    //        Stack<int> g = new Stack<int>();
    //        string priorityStr ="";


    //        for (int i = 0; i < charList.Count; i++)
    //        {
    //            switch (charList[i])
    //            {
    //                case '(':

    //                    g.Push(i);
    //                    break;
    //                case ')':
    //                    if (g.Count!=0)
    //                    {
    //                        for (int j = g.Pop(); j < i-1; j++)
    //                        {
    //                            priorityStr+=charList[j];
    //                        }
    //                        priority1.Add(priorityStr);
    //                        break;
    //                    }
    //                    else



    //            }
    //        }
    //        {
    //            double result=0;
    //            return result;
    //        }
    //    }

    //}
}
