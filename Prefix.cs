using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Polska
{

    public class Prefix
    {
        // Содержит префиксное выражение
        public string prefixExpr { get; private set; }

        // список доступных операций
        private List<char> operations = new List<char> { '+', '-', '*', '/'};

        //	Конструктор класса
        public Prefix(string expression)
        {
            //	Инициализируем поле
            prefixExpr = expression;
        }



        /// <summary>
        /// Парсинг числовых значений
        /// </summary>
        /// <param name="expr">Строка для парсинга</param>
        /// <param name="pos">Позиция</param>
        /// <returns>Число в виде строки</returns>
        public string GetStringNumber(string expr, ref int pos)
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
       

        /// <summary>
        /// Вычисляет значения, согласно оператору
        /// </summary>
        /// <param name="op">Оператор</param>
        /// <param name="first">Первый операнд (перед оператором)</param>
        /// <param name="second">Второй операнд (после оператора)</param>
        private decimal Execute(char op, decimal first, decimal second) => op switch
        {
            '+' => first + second,                  //	Сложение
            '-' => first - second,                  //	Вычитание
            '*' => first * second,                  //	Умножение
            '/' => first / second,                  //	Деление
            _ => 0  //	Возвращает, если не был найден подходящий оператор
        };

        //использую decimal, так как с плавающей точкой теряется точность даже в простом вычитании, например 4,6 - 3 выдает 1,599999996. Поэтому отказался от double и float
        public decimal Calc()
        {
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

                        //	Получаем результат операции и заносим в стек
                        if (c == '/' && second == 0)
                        {
                            Console.WriteLine($"Обнаружено деление на 0!!!  {counter} шаг: {first} {c} {second}");
                            return 0;
                        }
                        else locals.Push(Execute(c, first, second));
                        //	Отчитываемся пользователю о проделанной работе
                        Console.WriteLine($"{counter} шаг: {first} {c} {second} = {locals.Peek()}");
                    }
                }

                decimal res = locals.Pop();

                if (locals.Count != 0) //если стек оказался не пуст, значит выражение не верно.
                {
                    throw new ArgumentException("Не верное выражение! " + prefixExpr);
                }
                //	По завершению цикла возвращаем результат из стека
                return res;
        }

    }

}
