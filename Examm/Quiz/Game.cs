using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace Quiz
{
    public class Game
    {
        public List<Quiz> quizes = new List<Quiz>();

        public Game()
        {
            Question[] algebra = new Question[10];
            Question[] history = new Question[10];

            algebra[0] = new Question("1. 2 + 2 = \n\nA)5 B)0 C)4 D)2", 'C');
            algebra[1] = new Question("2. 23 - 7 = \n\nA)16 B) 2 C)9 D)15", 'A');
            algebra[2] = new Question("3. 5 * 3 = \n\nA)8 B)53 0 C)10 D)15", 'D');
            algebra[3] = new Question("4. 5 + (4-7) = \n\nA)2 B) 4 C)-1 D)0", 'A');
            algebra[4] = new Question("5. 5 -(-7) = \n\nA)-35 B) 35 C)-12 D)12", 'D');
            algebra[5] = new Question("6. x * 5 - 2 = 18. Чему равен x?\n\nA)3 B) 4 C)5 D)6", 'B');
            algebra[6] = new Question("7. 75.3 - 21.6 = \n\nA)51.5 B) 53.7 C)61.7 D)49", 'B');
            algebra[7] = new Question("8. |2-5| = \n\nA)3 B) 7 C)-3 D)-7", 'A');
            algebra[8] = new Question("9. x + |-7| = 32. Чему равен x?\n\nA)21 B) 39 C)25 D)5", 'C');
            algebra[9] = new Question("10. cos(45) = \n\nA)sqrt(2)/2 B) 0.5 C)sqrt(3)/2 D)0", 'A');

            history[0] = new Question("1. В каком году началось освоение целины на территории Казахстана? \n\nA)1962 B)1954 C)1946 D)1969", 'B');
            history[1] = new Question("2. Какие страны были союзницами Германии во время Второй Мировой Войны? \n\nA)Польша, Франция B)Румыния, Англия C)Япония, Италия D)Россия, США", 'C');
            history[2] = new Question("3. Какая самая распространенная религия в мире? \n\nA)Христианство B)Ислам C)Буддизм D)Иудаизм", 'A');
            history[3] = new Question("4. В какой стране произошла <<Великая Депрессия>>? \n\nA)Россия B)Корея C)США D)Китай", 'C');
            history[4] = new Question("5. Кто является первым президентом США? \n\nA)Линкольн B)Трамп C)Обама D)Вашингтон", 'D');
            history[5] = new Question("6. Кто после Ленина был официальным лидером Советского Союза? \n\nA)Хрущев B)Сталин C)Брежнев D)Горбачев", 'B');
            history[6] = new Question("7. Когда была снесена Берлинская стена? \n\nA)1989 B)1965 C)1945 D)1990", 'A');
            history[7] = new Question("8. Какое из этих изобретений НЕ является заслугой Китая? \n\nA)Бумага B)Компас C)Колесо D)Порох", 'C');
            history[8] = new Question("9. Какая страна НЕ является <<Азиатским тигром>>? \n\nA)Сингапур B)Япония C)Тайвань D)Южная Корея", 'B');
            history[9] = new Question("10. Когда было основано Казахское ханство? \n\nA)1655 B)1495 C)1799 D)1465", 'D');

            quizes.Add(new Quiz("Алгебра", algebra));
            quizes.Add(new Quiz("История", history));
        }

        public void Start()
        {
            User user = new User();
            Console.WriteLine("===============ВИКТОРИНА==============\n" +
                            "1 - Логин\n" +
                            "2 - Регистрация\n" +
                            "3 - Выход\n" +
                            "======================================");

            char input = (char)Console.Read();

            switch (input)
            {
                case '1':
                    user.Login();
                    break;

                case '2':
                    user.Register();
                    break;

                case '3':
                    Console.WriteLine("Выходим из игры...");
                    Thread.Sleep(500);
                    Environment.Exit(0);
                    break;
            }

            Console.Clear();
            Menu();
        }

        public void Menu()
        {
            User user = new User();
            Console.WriteLine("===============ВИКТОРИНА==============\n" +
                         "1 - Новая викторина\n" +
                         "2 - Результаты прошлых викторин\n" +
                         "3 - Топ 20 по конкретной викторине\n" +
                         "4 - Настройки\n" +
                         "5 - Выход\n" +
                         "======================================\n");

            string input;
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    StartQuiz();
                    break;

                case "2":
                    /*logic*/
                    break;

                case "3":
                    /*logic*/
                    break;

                case "4":
                    user.Settings();
                    break;

                case "5":
                    Console.WriteLine("Выходим из игры...");
                    Thread.Sleep(500);
                    Environment.Exit(0);
                    break;
            }
        }
        public void StartQuiz()
        {
            Console.WriteLine("Введите название раздела знаний, по которому хотите пройти викторину: \n\n" +
                "Список доступных викторин: ");

            // Вывод всех имеющихся викторин
            for (int i = 0; i < quizes.Count; i++)
                Console.WriteLine(quizes[i].Name);
            //

            string input = "";

            while (input == string.Empty)
            {
                input = Console.ReadLine();
            }

            int correctAnswers = 0;
            char userAnswer;

            for (int i = 0; i < quizes.Count; i++)
            {
                if (input == quizes[i].Name)
                {
                    for(int j = 0; j < 10; j++)
                    {
                        Console.WriteLine(quizes[i].questions[j].Title);
                        userAnswer = Console.ReadLine()[0];

                        if (userAnswer == quizes[i].questions[j].Answer)
                        {
                            correctAnswers++;
                        }
                    }

                    Console.WriteLine($"Вы прошли викторину на {correctAnswers} из 10!");
                    break;
                }
                else if(i == quizes.Count - 1)
                {
                    Console.WriteLine("Такой викторины не существует!");
                    i--;
                }
            }
        }
    }
}
