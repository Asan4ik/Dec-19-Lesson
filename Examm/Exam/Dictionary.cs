using System;
using System.Collections;
using System.Collections.Generic;

namespace Exam
{
    public class Dictionary : IEnumerable<KeyValuePair<string, List<string>>>
    {
        private Dictionary<string, List<string>> _words;
        private string _name;

        public IEnumerator<KeyValuePair<string, List<string>>> GetEnumerator()
        {
            return _words.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public List<string> this[string word]
        {
            get
            {
                if (string.IsNullOrEmpty(word))
                    return null;

                if (_words.TryGetValue(word, out List<string> list))
                    return list;

                return null;
            }
        }

        public Dictionary(string name)
        {
            _name = name;
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            _words = new Dictionary<string, List<string>>();
        }


        public void Add(string word, string value)
        {
            if (string.IsNullOrEmpty(word) || string.IsNullOrEmpty(value))
                throw new ArgumentNullException();

            if (_words.TryGetValue(word, out var list))
            {
                list.Add(value);
            }
            else
            {
                List<string> newList = new List<string> { value };
                _words.Add(word, newList);
            }
        }

        public List<string> SearchByTranslation(string translation)
        {
            if (string.IsNullOrEmpty(translation))
                throw new ArgumentNullException(nameof(translation));

            var result = new List<string>();

            foreach (var kvp in _words)
            {
                if (kvp.Value.Contains(translation))
                {
                    result.Add(kvp.Key);
                }
            }

            return result;
        }


        public bool Replace(string word, string a, string b)
        {
            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
                throw new ArgumentNullException();

            if (_words.TryGetValue(word, out var list))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Equals(a))
                    {
                        list[i] = b;
                        return true;
                    }
                }
            }

            throw new InvalidOperationException("В словаре отсутствует данное слово");
        }

        public void Replace(string word1, string word2)
        {
            if (string.IsNullOrEmpty(word1) || string.IsNullOrEmpty(word2))
                throw new ArgumentNullException();

            if (_words.TryGetValue(word1, out var list))
            {
                _words.Remove(word1);
                _words.Add(word2, list);
            }
        }


        public void DisplayWords()
        {
            Console.WriteLine("Словарь: " + _name);
            foreach (var kvp in _words)
            {
                Console.WriteLine($"Слово: {kvp.Key}");
                Console.Write("Переводы: ");
                foreach (var translation in kvp.Value)
                {
                    Console.Write(translation + ", ");
                }

                Console.WriteLine();
            }
        }


        public void DisplayWord(string word)
        {
            if (_words.TryGetValue(word, out var translations))
            {
                Console.WriteLine("Словарь: " + _name);
                Console.WriteLine($"Слово: {word}");
                Console.Write("Переводы: ");
                foreach (var translation in translations)
                {
                    Console.Write(translation + ", ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Слово '{word}' не найдено в словаре.");
            }
        }

        public void AddWord(string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentNullException();

            if (!_words.ContainsKey(word))
            {
                _words.Add(word, new List<string>());
            }
        }


        public void AddTranslationForWord(string word, string translation)
        {
            if (string.IsNullOrEmpty(word) || string.IsNullOrEmpty(translation))
                throw new ArgumentNullException();

            if (_words.ContainsKey(word))
            {
                _words[word].Add(translation);
                Console.WriteLine($"Перевод '{translation}' был добавлен для '{word}' в словаре '{this._name}'.");
            }
            else
            {
                Console.WriteLine($"Слово '{word}' не существует в словаре. Сначала добавьте слово.");
            }
        }

        public void UpdateWord(string currentWord, string newWord)
        {
            if (string.IsNullOrEmpty(currentWord) || string.IsNullOrEmpty(newWord))
                throw new ArgumentNullException();

            if (_words.ContainsKey(currentWord))
            {
                var translations = _words[currentWord];
                _words.Remove(currentWord);
                _words[newWord] = translations;
            }
            else
            {
                Console.WriteLine($"Слово '{currentWord}' не существует в словаре.");
            }
        }


        public void DeleteWord(string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentNullException();


            if (_words.ContainsKey(word))
            {
                _words.Remove(word);
            }
            else
            {
                Console.WriteLine($"Слово '{word}' не существует в словаре.");
            }
        }


        public void DisplayWordAndTranslations(string word)
        {
            if (string.IsNullOrEmpty(word) || string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException();

            if (_words.ContainsKey(word))
            {
                Console.WriteLine($"Слово: {word}");
                Console.Write("Переводы: ");
                foreach (var translation in _words[word])
                {
                    if (translation.Length == 0)
                    {
                        Console.WriteLine("Переводов нет");
                        return;
                    }
                    else if (translation.Length == 1)
                    {
                        Console.Write(translation + ". ");
                        return;
                    }
                    else if (translation.Length > 1)
                    {
                        Console.Write(translation + ", ");
                    }
                }
                Console.WriteLine(".");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Слово '{word}' не существует в словаре.");
            }
        }


        public void DeleteTranslation(string word, string translation)
        {
            if (string.IsNullOrEmpty(word) || string.IsNullOrEmpty(translation))
                throw new ArgumentNullException();

            if (_words.ContainsKey(word))
            {
                var translations = _words[word];
                if (translations.Contains(translation))
                {
                    if (_words[word].Count == 1)
                    {
                        Console.WriteLine($"Перевод '{translation}' не может быть удален для слова '{word}' в словаре" +
                            $", поскольку это последний перевод для слова.");
                        return;
                    }
                    translations.Remove(translation);
                    Console.WriteLine($"Перевод '{translation}' был удален для слова '{word}' в словаре.");
                }
                else
                {
                    Console.WriteLine($"Перевод '{translation}' не существует для слова '{word}' в словаре.");
                }
            }
            else
            {
                Console.WriteLine($"Слово '{word}' не существует в словаре.");
            }
        }
    }
}
