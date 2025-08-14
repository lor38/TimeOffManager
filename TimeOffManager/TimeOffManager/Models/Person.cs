namespace TimeOffManager.Models
{
    public class Person
    {
        public string Name { get; }
        public string Surname { get; }

        public Person(string name, string surname)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));
        }

        public override string ToString() => $"{Name} {Surname}";
    }
}
