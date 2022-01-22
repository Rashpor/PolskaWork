using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Polska;

namespace PolskaWorkTest
{
    [TestClass]
    public class PolskaTest
    {



        private List<char> operations = new List<char> { '+', '-', '*', '/' };
        private string GetStringNumber(string expr, ref int pos)
        {
            //	Хранит число
            string strNumber = "";

            //	Перебираем строку
            for (; pos >= 0; pos--)
            {
                //	Разбираемый символ строки
                char num = expr[pos];

                //	Проверяем, является символ числом
                if (Char.IsDigit(num) || num == ',')
                    //	Если да - прибавляем к строке
                    strNumber = num + strNumber;
                else
                {
                    //	Если нет, то перемещаем счётчик к предыдущему символу
                    pos++;
                    //	И выходим из цикла
                    break;
                }
            }
            //	Возвращаем число
            return strNumber;
        }

        private decimal Execute(char op, decimal first, decimal second) => op switch
        {
            '+' => first + second,                  //	Сложение
            '-' => first - second,                  //	Вычитание
            '*' => first * second,                  //	Умножение
            '/' => first / second,                  //	Деление
            _ => 0  //	Возвращает, если не был найден подходящий оператор
        };


        [TestMethod]
        public void CalcTest()
        {

            string prefixExpr = "";

            //	Стек для хранения чисел
            Stack<decimal> locals = new();
            //	Счётчик действий - для удобства отображения
            int counter = 0;


            //	Проходим по строке
            for (int i = prefixExpr.Length - 1; i >= 0; i--)
            {
                //	Текущий символ
                char c = prefixExpr[i];

                //	Если символ число
                if (Char.IsDigit(c))
                {
                    //	Парсим
                    string number = GetStringNumber(prefixExpr, ref i);
                    //	Заносим в стек, преобразовав из String в Decimal-тип
                    locals.Push(Convert.ToDecimal(number));
                }
                //	Если символ есть в списке операторов
                else if (operations.Contains(c))
                {
                    //	Прибавляем значение счётчику
                    counter += 1;

                    //	Получаем значения из стека в обратном порядке
                    decimal first = locals.Count > 0 ? locals.Pop() : 0,
                    second = locals.Count > 0 ? locals.Pop() : 0;

                    locals.Push(Execute(c, first, second));
                    //	Отчитываемся пользователю о проделанной работе
                    Console.WriteLine($"{counter} шаг: {first} {c} {second} = {locals.Peek()}");
                }
            }

            decimal res = locals.Pop();
        }
    }
}
