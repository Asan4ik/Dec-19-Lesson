using System;

namespace Quiz
{
    public class Quiz
    {
        private string _name;
        public Question[] questions = new Question[20];

        public string Name
        {
            get { return _name; }
        } 

        public Quiz(string name, Question[] questions)
        {
            _name = name;
            this.questions = questions;
        }
    }
}
