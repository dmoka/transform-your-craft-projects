namespace FizzBuzz
{
    public class FizzBuzzGenerator
    {
        public static string Generate(int number)
        {
            if (IsFizz(number) && IsBuzz(number))
            {
                return "FizzBuzz";
            }

            if (IsFizz(number))
            {
                return "Fizz";
            }

            if (IsBuzz(number))
            {
                return "Buzz";
            }

            return number.ToString();
        }


        private static bool IsFizz(int number)
        {
            return number % 3 == 0;
        }

        private static bool IsBuzz(int number)
        {
            return number % 5 == 0;
        }
    }
}