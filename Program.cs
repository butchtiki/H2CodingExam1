namespace H2CodingExam1
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        List<Question> questions;
        EndUser user;

        public void Init(EndUser user)
        {
            this.user = user;
            questions = new List<Question>();

            //Init questions
            Question question1 = new Question("You went to a party " +
                "last night and when you arrived to school the next day," +
                " everybody is talking about something you didn’t do.What will you do? ");
            question1.AddChoice('a', "Avoid everything and go with your friends");
            question1.AddChoice('b', "Go and talk with the person that started the rumors");
            question1.AddChoice('c', "Go and talk with the teacher");

            Question question2 = new Question("What quality do you excel the most?");
            question2.AddChoice('a', "Empathy");
            question2.AddChoice('b', "Curiosity");
            question2.AddChoice('c', "Perseverance");

            Question question3 = new Question("You are walking down the street when you " +
                "see an old lady trying to cross, what will you do?");
            question3.AddChoice('a', "Go and help her");
            question3.AddChoice('b', "Go for a policeman and ask him to help");
            question3.AddChoice('c', "Keep walking ahead");

            Question question4 = new Question("You had a very difficult day at school, " +
                "you will maintain a ____ attitude.");
            question4.AddChoice('a', "Depends on the situation");
            question4.AddChoice('b', "Positive");
            question4.AddChoice('c', "Negative");

            Question question5 = new Question("You are at a party and a friend of yours" +
                " comes over and offers you a drink, what do you do? ");
            question5.AddChoice('a', "Say no thanks");
            question5.AddChoice('b', "Drink it until it is finished");
            question5.AddChoice('c', "Ignore him and get angry at him");

            Question question6 = new Question("You just started in a new school, you will...");
            question6.AddChoice('a', "Go and talk with the person next to you");
            question6.AddChoice('b', "Wait until someone comes over you");
            question6.AddChoice('c', "Not talk to anyone");

            Question question7 = new Question("In a typical Friday, you would like to...");
            question7.AddChoice('a', "Go out with your close friends to eat");
            question7.AddChoice('b', "Go to a social club and meet more people");
            question7.AddChoice('c', "Invite one of your friends to your house");

            Question question8 = new Question("Your relationship with your parents is...");
            question8.AddChoice('a', "I like both equally");
            question8.AddChoice('b', "I like my Dad the most");
            question8.AddChoice('c', "I like my Mom the most");

            questions.Add(question1);
            questions.Add(question2);
            questions.Add(question3);
            questions.Add(question4);
            questions.Add(question5);
            questions.Add(question6);
            questions.Add(question7);
            questions.Add(question8);
        }

        public void StartExamination()
        {
            List<int> randomQuestionIndexList = new List<int>();
            List<int> tempChoices = new List<int>();

            for(int index = 1; index <= questions.Count; index++)
            {
                tempChoices.Add(index);
            }

            //Setup random question list
            while (tempChoices.Count != 0)
            {
                int currentIndex = 0;
                Random random = new Random();
                currentIndex = random.Next(1, tempChoices.Count);
                randomQuestionIndexList.Add(tempChoices[currentIndex-1]);
                tempChoices.RemoveAt(currentIndex - 1);
            }

            //Display questions, end-user answers
            foreach(int questionIndex in randomQuestionIndexList)
            {
                Question currentQuestion = questions[questionIndex - 1];
                Dictionary<char, string> currentChoices = currentQuestion.GetChoices();
                Console.WriteLine(questionIndex + ". " + currentQuestion.GetDescription());
                foreach(KeyValuePair<char, string> choice in currentChoices)
                {
                    Console.WriteLine(choice.Key + ". " + choice.Value);
                }

                char answer;
                answer = Convert.ToChar(Console.ReadKey().KeyChar);
                

                this.user.AddAnswerKey(questionIndex, answer);
                this.user.AddEvaluationPoints(answer);

                Console.WriteLine();
            }
            
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            Evaluator evaluator = new Evaluator();
            EndUser examiner = new EndUser(evaluator);
            program.Init(examiner);
            program.StartExamination();

            evaluator.Evaluate();
        }
    }

    class Question
    {
        string description;
        Dictionary<char, string> choices;

        public Question(string descr)
        {
            this.description = descr;
            this.choices = new Dictionary<char, string>();
        }

        public void AddChoice(char choiceName, string choiceDescription)
        {
            this.choices.Add(choiceName, choiceDescription);
        }

        public string GetDescription()
        {
            return this.description;
        }

        public Dictionary<char, string> GetChoices()
        {
            return this.choices;
        }
    }

    class Evaluator
    {
        Dictionary<char, int> evaluationPoints;

        public Evaluator()
        {
            evaluationPoints = new Dictionary<char, int>();
        }

        public void AddEvaluationPoints(char choiceName)
        {
            if (evaluationPoints.ContainsKey(choiceName))
            {
                evaluationPoints[choiceName] += 1;
            }
            else
            {
                evaluationPoints.Add(choiceName, 1);
            }
        }

        public void Evaluate()
        {
            int APoints = this.evaluationPoints['a'];
            int BPoints = this.evaluationPoints['b'];
            int CPoints = this.evaluationPoints['c'];

            if (APoints > BPoints && APoints > CPoints)
            {
                Console.WriteLine("You are emphatic.You see yourself in someone" +
                    " else’s situation before doing rash decisions. " +
                    "You tend to listen to other’s voices.");
            }

            else if (BPoints > APoints && BPoints > CPoints)
            {
                Console.WriteLine("You are conscious of your own character, feelings," +
                    " motives, and desires. The process can be painful but it" +
                    " leads to greater self-awareness.");
            }

            else if (CPoints > APoints && CPoints > BPoints)
            {
                Console.WriteLine("You manage yourself well; You take responsibility" +
                    " for your own behavior and well-being.");
            }

            else if (APoints == BPoints && BPoints == CPoints)
            {
                Console.WriteLine("You are conscious of your own character, feelings," +
                    " motives, and desires. The process can be painful but it" +
                    " leads to greater self-awareness.");
            }

            else if (APoints == CPoints)
            {
                Console.WriteLine("You are emphatic.You see yourself in someone" +
                    " else’s situation before doing rash decisions. " +
                    "You tend to listen to other’s voices.");
            }
            else if (BPoints == CPoints)
            {
                Console.WriteLine("You are conscious of your own character, feelings," +
                    " motives, and desires. The process can be painful but it" +
                    " leads to greater self-awareness.");
            }

            else if (APoints == BPoints)
            {
                Console.WriteLine("You are conscious of your own character, feelings," +
                    " motives, and desires. The process can be painful but it" +
                    " leads to greater self-awareness.");
            }
        }

    }

    class EndUser
    {
        Dictionary<int, char> answerKeys;
        Evaluator evaluator;

        public EndUser(Evaluator evaluator)
        {
            answerKeys = new Dictionary<int, char>();
            this.evaluator = evaluator;
        }

        public void AddAnswerKey(int questionNumber, char answer)
        {

            answerKeys.Add(questionNumber, answer);
        }

        public void AddEvaluationPoints(char choiceName)
        {
            evaluator.AddEvaluationPoints(choiceName);
        }

        public void Evaluate()
        {
            evaluator.Evaluate();
        }
    }
}
