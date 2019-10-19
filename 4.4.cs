using System;
using System.Collections.Generic;
using System.Text;

namespace Medium4
{
    class Programm
    {
        private static void Main(string[] args)
        {
            User user = new User(1, "Саша", 25, true);

            User user1 = new User(2, "Лёша", 30, true);
            User user2 = new User(3, "Катя", 20, false);
            User user3 = new User(4, "Аня", 35, false);

            Statistics topVisitedUser = new Statistics(new TopVisitedUser());
            Statistics averageAge = new Statistics(new AverageAge());
            Statistics preferredGender = new Statistics(new PreferredGender());

            user.AddUserVisit(user2);
            user.AddUserVisit(user1);
            user.AddUserVisit(user2);
            user.AddUserVisit(user3);
            user.AddUserVisit(user1);

            topVisitedUser.Statist.GetStatistics(user.Visits);
            averageAge.Statist.GetStatistics(user.Visits);
            preferredGender.Statist.GetStatistics(user.Visits);
        }
    }

    class User
    {
        public int ID { get => _id; }
        public string Name { get => _name; }
        public int Age { get => _age; }
        public bool Man { get => _man; }
        public List<User> Visits = new List<User>();

        private int _id;
        private string _name;
        private int _age;
        private bool _man;

        public User(int id, string name, int age, bool man)
        {
            _id = id;
            _name = name;
            _age = age;
            _man = man;
        }

        public void AddUserVisit(User visitedUser)
        {
            Visits.Add(visitedUser);
        }
    }

    class Statistics
    {
        public IStatistics Statist { get => _statistics; }
        private IStatistics _statistics;

        public Statistics(IStatistics statistics)
        {
            _statistics = statistics;
        }
    }

    class TopVisitedUser: IStatistics
    {
        void IStatistics.GetStatistics(List<User> visits)
        {
            int[] kithID = { 0 };
            List<User> topVisitedUser = new List<User>();
            foreach(var i in visits)
            {
                if(!FindInArray(i.ID, kithID))
                {
                    List<User> temp = visits.FindAll(item => item.ID == i.ID);
                    if (temp.Count > topVisitedUser.Count)
                        topVisitedUser = temp;
                }
            }
            Console.WriteLine("Самый посещаемый пользователь: "+ topVisitedUser[0].Name);
        }

        private bool FindInArray(int id, int[] kithID)
        {
            foreach(int i in kithID)
            {
                if(i == id)
                {
                    AddInArray(id, kithID);
                    return true;
                }
            }
            return false;
        }

        private void AddInArray(int id, int[] kithID)
        {
            int[] temp = { kithID.Length + 1 };
            for (int i = 0; i < kithID.Length; i++)
            {
                temp[i] = kithID[i];
            }
            temp[temp.Length - 1] = id;
            kithID = temp;
        }
    }

    class AverageAge: IStatistics
    {
        void IStatistics.GetStatistics(List<User> visits)
        {
            int allAge = 0;
            for (int i = 0; i < visits.Count; i++)
            {
                allAge += visits[i].Age;
            }
            Console.WriteLine("Средний возраст посещаемых пользователей: "+ allAge/visits.Count);
        }
    }

    class PreferredGender: IStatistics
    {
        void IStatistics.GetStatistics(List<User> visits)
        {
            int manCount = 0;
            int womanCount = 0;
            for (int i = 0; i < visits.Count; i++)
            {
                if (visits[i].Man)
                    manCount++;
                else
                    womanCount++;
            }
            Console.WriteLine(GenderCount(manCount, womanCount));
        }

        private string GenderCount(int manCount, int womanCount)
        {
            if (manCount > womanCount)
                return "Предпочитает мужчин";
            else if (womanCount > manCount)
                return "Предпочитает женщин";
            else
                return "Нет предпочтений";
        }
    }

    interface IStatistics
    {
        void GetStatistics(List<User> visits);
    }
}
