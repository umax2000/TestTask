using System;
using Newtonsoft.Json;
using System.IO;
using Collection.Generator;
using Boolean = System.Boolean;

namespace ConsoleApp4
{
    internal class Program
    {
        public class Person
        {
            public Int32 Id { get; set; }
            public Guid TransportId { get; set; }
            public String FirstName { get; set; }
            public String LastName { get; set; }
            public Int32 SequenceId { get; set; }
            public String CreditCardNumbers { get; set; }
            public Int32 Age { get; set; }
            public String Phones { get; set; }
            //не получилось нормальным образом преобразовать POSIX в формат даты
            //поэтому решил использовать структуру для удобства)
            public BirthDate birthDate { get; set; }
            public Double Salary { get; set; }
            public Boolean IsMarred { get; set; }
            public Gender Gender { get; set; }
            public Child[] Children { get; set; }
        }
        public class Child
        {
            public Int32 Id { get; set; }
            public String FirstName { get; set; }
            public String LastName { get; set; }
            public BirthDate BirthDate { get; set; }
            public Gender Gender { get; set; }
        }
        public enum Gender
        {
            Male,
            Female
        }
        public struct BirthDate
        {
            public int Day, Month, Year;
        }
        static void Main(string[] args)
        {
            PersonsGenerator generator = new PersonsGenerator(10);
            //не понятно где именно нужно использовать lowerCamelCase потому что у нас все переменные однословные
            //решил просто использовать функцию ToLower
            File.WriteAllText("Persons.json", JsonConvert.SerializeObject(generator));

            generator.persons.Clear();

            var persons = JsonConvert.DeserializeObject<PersonsGenerator>(File.ReadAllText("Persons.json"));

            Console.WriteLine($"Persons count is {persons.persons.Count}");
            // в задании указано вывести количество карт но нигде не указано что их количество может быть отличным от единицы,
            // в предоставленной модели есть только серийный номер карты но не количество 
            Console.WriteLine($"Credit cards count is {persons.persons.Count}");
            Console.WriteLine($"Average children age is {PersonsGenerator.averageChildAge}");

            Console.ReadKey();
        }
    }
}
