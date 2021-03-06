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

        private int closeBacketCont = 0;
        private int openBacketCont = 0;
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

            //действия при скобках
            if (Regex.IsMatch(Exp, $@"\{EncloseBracketSign}&"))
            {
                //проверка корректности скобок перед очисткой стека и удалении 
                if (closeBacketCont <= openBacketCont)
                {
                    encloseBracketStack.Push(EncloseBracketSign);
                    closeBacketCont--;
                }
                else
                    closeBacketCont--;
            }

            if (Regex.IsMatch(Exp, $@"\{OpenBracketSign}"))
            {
                if (closeBacketCont <= openBacketCont)
                {

                    encloseBracketStack.Pop();
                    openBacketCont--;
                }
            }
            Exp = Exp.Remove(Exp.Length - 1, 1);
            Result = CalculateExpression();
        }

        //ввод
        public void Insert(string element)
        {
            if ((element != "/") || (element != "*") || (element != "+") || (element != "-") || (element != "(") || (element != ")"))
            {
                Exp += element;
                Result = CalculateExpression();
            }
            Result = CalculateExpression();
        }

        private void BracketOpen()
        {
            Exp += OpenBracketSign;
            encloseBracketStack.Push(EncloseBracketSign);
            openBacketCont++;
            Result = string.Empty;
        }
        private void BracketClose()
        {
            Exp += EncloseBracketSign;
            if (encloseBracketStack.Count > 0)
            {
                encloseBracketStack.Pop();
                closeBacketCont++;
                openBacketCont--;
                Result = CalculateExpression();
            }
            else
                Result = "некорректно расставлены скобки";


        }
        //операции 
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
                    BracketOpen();
                    break;
                case Operations.BracketClouse:
                    BracketClose();
                    break;
                case Operations.Equal:
                    Equal();
                    break;
            }
        }
        //подсчет
        public string CalculateExpression()
        {
            if (string.IsNullOrEmpty(Exp) || !Regex.IsMatch(Exp.Last().ToString(), @"(\d|\))") || encloseBracketStack.Any())
                return string.Empty;

            Expression expression = new Expression(Exp);
            return expression.calculate().ToString(CultureInfo.InvariantCulture);
        }

        //проверка корректности скобок и введенных операций

        //public bool BracketCheck()
        //{
        //    char[] array = Exp.ToCharArray();
        //    List<char> charList = new List<char>();
        //    charList.AddRange(array);
        //    Stack<char> que = new Stack<char>();
        //    for (int i = 0; i < charList.Count; i++)
        //    {
        //        if (charList[i] == '(')
        //            que.Push(')');
        //        if (charList[i] == ')')
        //            que.Pop();
        //    }
        //    if (que.Count == 0)
        //        return true;
        //    else
        //        return false;

        //}
        //public bool OperationCheck()
        //{
        //    char[] array = Exp.ToCharArray();
        //    List<char> charList = new List<char>();
        //    charList.AddRange(array);
        //    int n = 0;
        //    for (int i = 1; i < charList.Count; i++)
        //    {

        //        switch (charList[i])
        //        {
        //            case '(':
        //                if (charList[i - 1] == '+' || charList[i - 1] == '-' || charList[i - 1] == '*' || charList[i - 1] == '/' || charList[i - 1] == '(')
        //                    return true;
        //                else
        //                {
        //                    n++;
        //                    return false;
        //                }
        //            case '*':
        //                if (charList[i - 1] != '+' || charList[i - 1] != '-' || charList[i - 1] != '*' || charList[i - 1] != '/' || charList[i - 1] != '(')
        //                    return true;
        //                else
        //                {
        //                    n++;
        //                    return false;
        //                }
        //            case '/':
        //                if (charList[i - 1] != '+' || charList[i - 1] != '-' || charList[i - 1] != '*' || charList[i - 1] != '/' || charList[i - 1] != '(')
        //                    return true;
        //                else
        //                {
        //                    n++;
        //                    return false;
        //                }
        //            case '+':
        //                if (charList[i - 1] != '+' || charList[i - 1] != '-' || charList[i - 1] != '*' || charList[i - 1] != '/' || charList[i - 1] != '(')
        //                    return true;
        //                else
        //                {
        //                    n++;
        //                    return false;
        //                }
        //            case '-':
        //                if (charList[i - 1] != '+' || charList[i - 1] != '-' || charList[i - 1] != '*' || charList[i - 1] != '/')
        //                    return true;
        //                else
        //                {
        //                    n++;
        //                    return false;
        //                }
        //            case '.':
        //                if (charList[i - 1] != '.' || charList[i - 1] != '+' || charList[i - 1] != '-' || charList[i - 1] != '*' || charList[i - 1] != '/' || charList[i - 1] != '(' || charList[i - 1] != ')')
        //                    return true;
        //                else
        //                {
        //                    n++;
        //                    return false;
        //                };
        //        }

        //    }
        //    if (n>0)
        //        return false;
        //    else
        //        return true;
        //}

    }
}
