using Ray.Domain.Model;
using Xunit;
using Xunit.Gherkin.Quick;

namespace Ray.Domain.Test.Model
{
    public class PersonTests
    {
        [Fact]
        public void NonBddTest_InstantiatePerson_VerifyAge()
        {
            // Arrange
            int beforeBirthday = 30;
            var person = new Person {Name = "CM", Age = beforeBirthday };

            // Act
            person.CelebrateBirthday();

            // Assert
            Assert.Equal(++beforeBirthday, person.Age);
        }
    }

    [FeatureFile("./spec/person/Person.feature")]
    public sealed class PersonBddTests : Feature
    {
        private readonly Person _me = new Person();

        [Given(@"I am (\d+) years old now")]
        public void GivenCurrentAge_SetOnPersonInstance(int oldAge)
        {
            _me.Age = oldAge;
        }

        [When(@"I celebrate my birthday")]
        public void PartyTime_CelebrateBirthday()
        {
            _me.CelebrateBirthday();
        }

        [Then(@"my age should increase to (\d+)")]
        public void NextDay_AgeHasIncreased(int newAge)
        {
            var actualResult = _me.Age;

            Assert.Equal(newAge, actualResult);
        }
    }
}
