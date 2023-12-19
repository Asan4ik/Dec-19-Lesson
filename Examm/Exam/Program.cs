using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Exam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary word = new Dictionary("eng-rus");
            DictionaryEditor editor = new DictionaryEditor();

            word.Add("Home", "Дом");
            word.Add("Home", "Жильё");

            word.Add("Tree", "Дерево");
            word.Add("Tree", "Древо");

            word.Add("Bow", "Лук");
            word.Add("Bow", "Поклон");
            word.Add("Bow", "Бант");
            word.Add("Bow", "Смычок");

            word.DeleteWord("Tree");

            //////////////////////////////////////////////////////////////////

            Menu(editor);

            Console.ReadKey();
        }

        public static void Menu(DictionaryEditor editor)
        {
            Console.WriteLine("--СЛОВАРИ--\n" +
                             "============\n" +
                             "  1 - Создать словарь\n" +
                             "  2 - Редактор словарей\n" +
                             "  3 - Выход");

            string input;
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    editor.Create();
                    Console.Clear();
                    Menu(editor);
                    break;
                case "2":
                    Console.Clear();
                    Editor(editor);
                    Menu(editor);
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("Выходим из приложения...");
                    Thread.Sleep(500);
                    Environment.Exit(0); 
                    break;
                default:
                    Console.WriteLine("Некорректный ввод!");
                    break;
            }
        }

        public static void Editor(DictionaryEditor editor)
        {
            Console.WriteLine("  1 - Удалить словарь\n" +
                              "  2 - Вывести все словари\n" +
                              "  3 - Добавить слово в словарь\n" +
                              "  4 - Удалить слово из словаря\n" +
                              "  5 - Экспортировать словарь\n" +
                              "  0 - Вернуться в меню\n");

            string input;
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Введите название словаря из списка ниже, который вы хотите удалить: ");
                    editor.DisplayAll();
                    input = Console.ReadLine();
                    editor.Remove(input);
                    Menu(editor);
                    break;
                case "2":
                    Console.Clear();
                    editor.DisplayAll();
                    Thread.Sleep(1000);
                    Console.Clear(); 
                    Menu(editor);
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Введите название словаря из списка ниже, в который вы хотите добавить слово: ");

                    editor.DisplayAll();
                    input = Console.ReadLine();

                    Console.WriteLine("Введите слово, которое вы хотите добавить в данный словарь: ");

                    string word;
                    word = Console.ReadLine();

                    editor.GetDictionary(input).AddWord(word);

                    Console.WriteLine("Введите хотя-бы один перевод данного слова: ");

                    string translation;
                    translation = Console.ReadLine();

                    editor.GetDictionary(input).AddTranslationForWord(word, translation);
                    Menu(editor);
                    break;
                case "4":

                    break;
                case "5":

                    break;
                case "0":

                    break;
                default:
                    Console.WriteLine("Некорректный ввод!");
                    break;
            }
        }

        public class DictionaryEditor
        {
            private List<Dictionary> dictionaries;

            public DictionaryEditor()
            {
                dictionaries = new List<Dictionary>();
            }

            public void Create()
            {
                Console.Write("Введите имя словаря: ");
                string name = Console.ReadLine();

                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name is null");

                var existingDictionary = dictionaries.Find(d => d.Name == name);

                if (existingDictionary != null)
                {
                    Console.WriteLine($"Словарь с именем '{name}' уже существует!");
                    return;
                }
                else
                {
                    var dictionary = new Dictionary(name);
                    dictionaries.Add(dictionary);
                    Console.WriteLine($"Словарь '{name}' был успешно создан!");
                    Thread.Sleep(500);
                }
            }

            public bool Remove(string name)
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException("name is null");


                var dictionaryToRemove = dictionaries.Find(d => d.Name == name);
                if (dictionaryToRemove != null)
                {
                    dictionaries.Remove(dictionaryToRemove);
                    Console.WriteLine($"Словарь '{name}' был удален.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Словарь с именем '{name}' не существует.");
                    return false;
                }
            }

            public void Rename(string currentName)
            {
                var dictionaryToRename = dictionaries.Find(d => d.Name == currentName);

                if (dictionaryToRename != null)
                {
                    Console.Write("Введите новое имя словаря: ");
                    string newName = Console.ReadLine();
                    if (dictionaries.Any(d => d.Name == newName))
                    {
                        Console.WriteLine($"Словарь с именем '{newName}' уже существует. Не удается переименовать.");
                        return;
                    }

                    dictionaryToRename.Name = newName;
                    Console.WriteLine($"Словарь '{currentName}' был переменован в '{newName}'.");
                }
                else
                {
                    Console.WriteLine($"Словарь с именем '{currentName}' не существует.");
                }
            }

            public Dictionary GetDictionary(string name)
            {
                return dictionaries.Find(d => d.Name == name);
            }

            public void DisplayAll()
            {
                foreach (var dictionary in dictionaries)
                {
                    dictionary.DisplayWords();
                }
            }

            public void Export(string dictionaryName)
            {
                Dictionary dictionary = dictionaries.Find(d => d.Name == dictionaryName);

                if (dictionary != null)
                {
                    string fileName = $"{dictionaryName}.txt";

                    using (StreamWriter writer = new StreamWriter(fileName))
                    {
                        foreach (var kvp in dictionary)
                        {
                            writer.WriteLine($"{kvp.Key} - {string.Join(", ", kvp.Value)}");
                        }
                    }

                    Console.WriteLine($"Словарь '{dictionaryName}' был экспортирован в '{fileName}'.");
                }
                else
                {
                    Console.WriteLine($"Словарь с именем '{dictionaryName}' не существует.");
                }
            }
        }
    }
}
