using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadaci6i7
{
    public class Class1
    {

        public static async Task<int> FactorialDigitSum(int n)
        {
            return await Task.Run(() =>
            {
                int factorial = 1;
                for (int i = 2; i <= n; i++)
                {
                    factorial *= i;
                }

                int sum = 0;
                while (factorial != 0)
                {
                    sum += factorial % 10;
                    factorial /= 10;
                }
                return sum;
            });
        }

        private static async  Task LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = await GetTheMagicNumber();
            Console.WriteLine(result);
        }

        private static async  Task<int> GetTheMagicNumber()
        {
            return await IKnowIGuyWhoKnowsAGuy();
        }

        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            return await IKnowWhoKnowsThis(10) + await IKnowWhoKnowsThis(5);
        }

        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            return await FactorialDigitSum(n);
        }
    }
}
