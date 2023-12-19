using System;

namespace Quiz
{
    public class Question
    {
        private string _title;
        private char _answer;

        public string Title 
        {
            get { return _title; }
            set { _title = value; }
        }

        public char Answer
        {
            get { return _answer; }
            set { _answer = value; }
        }

        public Question(string title, char answer)
        {
            _title = title;
            _answer = answer;
        }
    }
}
