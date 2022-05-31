using System;
using System.Threading;

namespace Bulls_and_Cows
{
    class Program
    {
        static void Main()
        {
            int hidden_number, n_one, n_two, n_three, n_four, 
                user_number, u_one, u_two, u_three, u_four, bulls, cows, lang = 1;

            bool game_over;

            // Загадать рандомное число
            void Get_random_number()
            {
                Random random_number = new Random();

                hidden_number = random_number.Next(1000, 9999);
                
                n_one = hidden_number / 1000;

                n_two = ( hidden_number / 100 ) - ( hidden_number / 1000 * 10 );

                n_three = (hidden_number / 10) - (hidden_number / 100 * 10 );

                n_four = hidden_number - ( hidden_number / 10 * 10 );

                if (Check_condition(n_one, n_two, n_three, n_four)) Get_random_number();
            }

            // Выводит значения всех переменных
            void Get_all_variables()
            {
                Console.WriteLine("\n// ************************");
                Console.WriteLine( $"// hidden_number  |   {hidden_number}");
                Console.WriteLine( $"// n_one          |   {n_one}");
                Console.WriteLine( $"// n_two          |   {n_two}");
                Console.WriteLine( $"// n_three        |   {n_three}");
                Console.WriteLine( $"// n_four         |   {n_four}");
                Console.WriteLine(  "// ************************");
            }

            // Делаем предположение
            void Give_an_answer()
            {
                Out("\n Ваш ответ: ", "\n Your answer is: ");

                user_number = Convert.ToInt32(Console.ReadLine());

                u_one = user_number / 1000;

                u_two = (user_number / 100) - (user_number / 1000 * 10);

                u_three = (user_number / 10) - (user_number / 100 * 10);

                u_four = user_number - (user_number / 10 * 10);

                switch (user_number)
                {
                    case -1:

                        Get_all_variables();

                        Give_an_answer();

                        break;

                    case 0:

                        Out("\n !Вы завершили игру!\n", "\n !You have completed the game!\n");

                        game_over = true;

                        break;

                    default:

                        if (user_number == hidden_number)
                        {
                            Out("\n !Вы отгадали число!\n", "\n !You guessed the number!\n");

                            game_over = true;

                            break;
                        }

                        if (user_number >= 1000 && user_number <= 9999)
                        {
                            if (Check_condition(u_one, u_two, u_three, u_four))
                            {
                                Out("\n !В ответе есть схожие цифры!\n", "\n !There are similar numbers in the answer!\n");

                                Give_an_answer();
                            }
                            else 
                            {
                                bulls = 0; cows = 0;
                                
                                // Первое число
                                if (u_one == n_one) ++bulls; 
                                else if (u_one == n_two || u_one == n_three || u_one == n_four) ++cows; 

                                // Второе число
                                if (u_two == n_two) ++bulls;
                                else if (u_two == n_one || u_two == n_three || u_two == n_four) ++cows;

                                // Третье число
                                if (u_three == n_three) ++bulls;
                                else if (u_three == n_one || u_three == n_two || u_three == n_four) ++cows;

                                // Четвертое число
                                if (u_four == n_four) ++bulls;
                                else if (u_four == n_one || u_four == n_two || u_four == n_three) ++cows;

                                Out($"\n Коров: {cows}    Быков: {bulls}\n", $"\n Cows: {cows} Bulls: {bulls}\n");

                                Give_an_answer();
                            }
                        }
                        else
                        {
                            Out("\n !Число не четырехзначное!\n", "\n !The number is not four-digit!\n");

                            Give_an_answer();
                        }

                        break;
                }
            }

            // Проверяем, есть ли схожие цифры
            bool Check_condition(int one, int two, int three, int four)
            {
                if (one == two || one == three || one == four ||
                    two == three || two == four || three == four)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // Говорим, мол, загадываем число // Сделано для красоты
            void Alert()
            {
                Out("\n Загадываю число", "\n Guessing a number");

                Thread.Sleep(1000);

                for (int k = 0; k < 3; ++k)
                {
                    Console.Write(".");

                    Thread.Sleep(1000);
                }

                    Console.Clear();

                Out("\n !Загадал!\n", "\n !I made a wish!\n");

                Thread.Sleep(1000);
            }

            // Выбрать язык
            void Change_Language()
            {
                Console.Write("\n Выбери язык: 1 - Русский, 2 - English\n\n ");

                lang = Convert.ToInt32(Console.ReadLine());

                if (lang != 1 && lang != 2)
                {
                    Console.WriteLine("\n Такого пункта нет!");

                    Change_Language();
                }
            }

            // Вывод в зависимости от выбранного языка
            void Out(string rus, string eng)
            {
                if (lang == 1)
                {
                    Console.Write($"{rus}");
                }
                else
                {
                    if (lang == 2)
                        Console.Write($"{eng}");
                }
            }

            // Вот тут уже начинается игра
            void Game()
            {
                game_over = false;

                Get_random_number();

                Console.Clear();

                Alert();

                Out("\n Пишите четырехзначное число либо 0 для выхода из игры\n", "\n Write a four-digit number or 0 to exit the game\n");

                Thread.Sleep(1000);

                while (!game_over) Give_an_answer();

                Out("\n Желаете сыграть еще раз? \n\n 0 - Выйти, Любое число - Начать с начала\n\n",
                    "\n Would you like to play again? \n\n 0 - Exit, Any number - Start from the beginning\n\n");

                Console.Write(" ");

                int i = Convert.ToInt32(Console.ReadLine());

                if (i != 0) Game();
            }

            Change_Language();

            // Запускаем игру
            Game();
        }
    }
}
