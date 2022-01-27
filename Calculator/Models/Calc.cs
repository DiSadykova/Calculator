using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    internal class Calc
    {
        public static double Calcul(string exp)
        {
            char[] array = exp.ToCharArray();
            List<char> charList = new List<char>();
            charList.AddRange(array);
            List<string> priority1 = new List<string>();
            Stack<int> g = new Stack<int>();
            string priorityStr ="";


            for (int i = 0; i < charList.Count; i++)
            {
                switch (charList[i])
                {
                    case '(':
                        
                        g.Push(i);
                        break;
                    case ')':
                        if (g.Count!=0)
                        {
                            for (int j = g.Pop(); j < i-1; j++)
                            {
                                priorityStr+=charList[j];
                            }
                            priority1.Add(priorityStr);
                            break;
                        }
                        else


                        
                }
            }
            {
                double result=0;
                return result;
            }
        }

    }
}
