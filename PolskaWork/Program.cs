using System;
using Polska;

namespace PolskaWork
{
    public class Program
    {

        public static void UseCalc()
        {
            try
            {
                //тестовые строки
                //string expression1 = " - * / 15 - 7 + 1 1 3 + 2 + 1 1";
                //string expression2 = "+ 3 4";

                Console.WriteLine("Введите выражение в польской (префиксной) нотации");
                Console.WriteLine("Поддерживаются сложение '+', вычитание '-', умножение '*' и деление '/'");
                Console.WriteLine("Десятичные дроби указывайте через ','");
                string expression = Console.ReadLine();

                Prefix prefix = new(expression);

                

                Console.WriteLine("Введена строка " + prefix.prefixExpr);
                Console.WriteLine(expression + " = " + prefix.Calc());
                Console.WriteLine();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
            }

            KeepWorking();
        }

        static void KeepWorking() 
        {
            //переменная для управления
            string x;
            Console.WriteLine("Введите 1 для ввода нового выражения");
            Console.WriteLine("Введите 2 чтобы закрыть калькулятор");

            //Уточняем будет ли пользователь вводить новое выражение, или закрываем приложение
            x = Console.ReadLine();
            if (x == "1")
            {
                UseCalc();
            }
            else if (x == "2")
            {
                return;
            }
        }


        static void Main(string[] args)
        {
            KeepWorking();
        }



    }
}
