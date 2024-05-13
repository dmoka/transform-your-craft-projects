using FluentAssertions;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;

namespace WarehousingService.Tests.Asserters
{
    public class JsonAsserter
    {

        public string Actual { get; }

        protected JsonAsserter(string actual)
        {
            Actual = actual;
        }

        public static JsonAsserter AssertThat(string actual)
        {
            return new JsonAsserter(actual);
        }


        public void IsEqualToArray(string expected)
        {
            var actualJTokenArray = JArray.Parse(Actual);
            var expectedJTokenArray = JArray.Parse(expected);

            actualJTokenArray.Count.Should().Be(expectedJTokenArray.Count);

            for (int i = 0; i < actualJTokenArray.Count; i++)
            {
                var actualJToken = actualJTokenArray[i];
                var expectedJToken = expectedJTokenArray[i];
                actualJToken.Should().BeEquivalentTo(expectedJToken, $"Index for failing element: {i}");
            }
        }



        public void IsEqualTo(string expected)
        {
            var expectedJToken = JToken.Parse(expected);
            var actualJToken = JToken.Parse(Actual);

            actualJToken.Should().BeEquivalentTo(expectedJToken);
        }

    }
}
