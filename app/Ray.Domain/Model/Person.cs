namespace Ray.Domain.Model
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public void CelebrateBirthday()
        {
            Age++;
        }
    }
}
