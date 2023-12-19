using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace Quiz
{
    public class User
    {
        private static List<User> users = new List<User>();

        private string _name;
        private string _password;
        private int[] _dateOfBirth = new int[3];

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public void Register()
        {
            Console.WriteLine("Придумайте свой логин: ");

            string input;
            int dateOfBirth;

            while (true)
            {
                input = Console.ReadLine();

                //while (test == false)
                //{


                //    for (int i = 0; i < users.Count; i++)
                //    {
                //        if (login == users[i].Name)
                //        {
                //            Console.WriteLine("Данный логин уже занят! Пожалуйста, повторите ввод: ");
                //        }
                //        else
                //        {
                //            test = true;
                //        }
                //    }
                //}

                if (input != string.Empty)
                {
                    _name = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Ваш логин слишком короткий!");
                }
            }
            Console.WriteLine("Придумайте свой пароль: ");

            while (true)
            {
                input = Console.ReadLine();

                if (input != string.Empty)
                {
                    _password = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Ваш пароль слишком короткий!");
                }
            }

            Console.WriteLine("Введите свою дату рождения(сначала день, потом месяц и потом год: ");

            while (true)
            {
                for (int i = 0; i < 3; i++)
                {
                    dateOfBirth = Convert.ToInt32(Console.ReadLine());

                    if (dateOfBirth > 0 && dateOfBirth < 2023)
                    {
                        _dateOfBirth[i] = dateOfBirth;
                    }
                    else
                    {
                        Console.WriteLine("Была введена некорректная дата!");
                        i--;
                    }
                }
                break;
            }

            Console.WriteLine("Вы были успешно зарегистрированы!");
            Thread.Sleep(500);
        }

        public void Login()
        {
            Console.WriteLine("Введите свой логин: ");
            string input = "";

            for (int i = 0; i < users.Count; i++)
            {
                input = Console.ReadLine();
                while (true)
                {
                    if (input == users[i].Name)
                    {
                        Console.WriteLine("Введите свой пароль: ");
                        input = Console.ReadLine();

                        if (input == users[i].Password)
                        {
                            Console.WriteLine("Вы успешно зашли в свой аккаунт!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Неверный пароль!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Пользователь с таким логином не существует! " +
                            "Пожалуйста, повторите ввод: ");
                    }
                }
            }
        }
        public void Settings()
        {
            Game game = new Game();
            Console.WriteLine("1 - Поменять пароль\n" +
                              "2 - Поменять дату рождения\n\n" +
                              "(Введите 0 чтобы вернуться обратно)");

            string input;
            input = Console.ReadLine();

            if (input == "1")
                ChangePassword();
            else if (input == "2")
                ChangeDateOfBirth();
            else
                game.Menu();

        }

        public void ChangePassword()
        {
            Console.WriteLine("Введите свой старый пароль: ");
            string input;

            while (true)
            {
                input = Console.ReadLine();

                if (input == _password)
                {
                    Console.WriteLine("Вы ввели верный пароль. " +
                        "Теперь придумайте себе новый пароль: ");

                    input = Console.ReadLine();

                    while (true)
                    {
                        if (input != string.Empty && input.Length >= 8)
                        {
                            Console.WriteLine("Ваш пароль был успешно изменён!");
                            _password = input;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Ваш пароль слишком короткий!");
                            input = Console.ReadLine();
                        }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный пароль!");
                    return;
                }
            }
        }

        public void ChangeDateOfBirth()
        {
            Console.WriteLine("Введите новую дату своего рождения(день.месяц.год): ");
            string newDate = Console.ReadLine();
        }
    }
}