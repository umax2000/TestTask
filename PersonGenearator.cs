using static ConsoleApp4.Program;
using System.Collections.Generic;
using System;
using System.Text;

namespace Collection.Generator
{

    internal class PersonsGenerator
    {
        public List<Person> persons = new List<Person>();
        static Random rand = new Random();
        public static double averageChildAge;
        public PersonsGenerator(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                persons.Add(new Person
                {
                    Id = i,
                    //решил использовать Faker для упрощения генерации некоторых переменных
                    TransportId = Guid.NewGuid(),
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    //честно говоря, не знаю что такое SequenceID, в интернете не смог ничего найти
                    SequenceId = i,
                    CreditCardNumbers = GetRandomCardNumber(),
                    Age = rand.Next(21, 65),
                    Phones = GetRandomTelNumber(),
                    birthDate = GetRandomAdultBirthDate(),
                    Salary = rand.Next(10000, 100000),
                    IsMarred = Faker.Boolean.Random(),
                    Gender = Faker.Enum.Random<Gender>(),
                    Children = CreateChild(),
                }); ;
            }
            int childCount = 0;
            int allChildYears = 0;
            for (int i = 0; i < count; ++i)
            {              
                if(persons[i].Children != null)
                {
                    childCount = childCount + persons[i].Children.Length;
                    for (int j = 0; j < persons[i].Children.Length; ++j)
                    {
                        if (persons[i].Children[j] != null)
                        {
                            allChildYears = allChildYears + (2022 - persons[i].Children[j].BirthDate.Year);
                        }
                    }
                }
            }
            if(childCount != 0)
            {
                averageChildAge = (double)(allChildYears / childCount);
            }
        }
        public string GetRandomTelNumber()
        {
            StringBuilder telNumber = new StringBuilder(13);
            int number;
            telNumber = telNumber.Append("+7 ");
            for (int i = 0; i < 2; i++)
            {
                number = rand.Next(100, 999);
                telNumber.Append(number.ToString() + " ");
            }
            for (int i = 0; i < 2; i++)
            {
                number = rand.Next(10, 99);
                telNumber.Append(number.ToString() + " ");
            }
            return telNumber.ToString();
        }
        public BirthDate GetRandomAdultBirthDate()
        {
            BirthDate birthDate = new BirthDate();
            birthDate.Day = rand.Next(0, 31);
            birthDate.Month = rand.Next(0, 12);
            birthDate.Year = rand.Next(1957, 2001);
            return birthDate;

        }
        public BirthDate GetRandomAChildBirthDate()
        {
            BirthDate birthDate = new BirthDate();
            birthDate.Day = rand.Next(0, 31);
            birthDate.Month = rand.Next(0, 12);
            //огрничим возраст шестью годами
            birthDate.Year = rand.Next(2016, 2022);
            return birthDate;

        }
        public string GetRandomCardNumber()
        {
            StringBuilder CardNumber = new StringBuilder(20);
            int number;
            for (int i = 0; i < 4; i++)
            {
                number = rand.Next(1000, 9999);
                CardNumber.Append(number.ToString() + " ");
            }
            return CardNumber.ToString();
        }
        public Child[] CreateChild()
        {
            //Ограничим количество детей двумя
            int childCount = rand.Next(0, 2);
            if(childCount == 0)
                return null;
            Child[] Children = new Child[childCount];
            for (int i = 0; i < childCount; ++i)
            {
                Children[i] = new Child();
                Children[i].Id = i;
                Children[i].FirstName = Faker.Name.First();
                Children[i].LastName = Faker.Name.Last();
                Children[i].BirthDate = GetRandomAChildBirthDate();
                Children[i].Gender = Faker.Enum.Random<Gender>();
            }
            return Children;
        }
    }
}