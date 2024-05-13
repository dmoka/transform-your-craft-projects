using System;
using NFluent;
using NUnit.Framework;

namespace FizzBuzz.Tests
{
    public class FizzBuzzGeneratorTests
    {
        [TestCase(1, "1")]
        [TestCase(2, "2")]
        [TestCase(3, "Fizz")]
        [TestCase(6, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(10, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        [TestCase(30, "FizzBuzz")]
        public void ShouldReturnFizzBuzzResult(int number, string expectedFizzBuzzResult)
        {
            string fizzBuzzResult = FizzBuzzGenerator.Generate(number);

            Check.That(fizzBuzzResult).IsEqualTo(expectedFizzBuzzResult);
        }

    }
}