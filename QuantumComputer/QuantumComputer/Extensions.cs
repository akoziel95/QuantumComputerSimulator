using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumComputer
{
    public static class Extensions
    {

        public static int Pow(this int numeric, int pow)
        {
            if (pow == 0)
                return 1;
            if (pow == 1)
                return numeric;
            return numeric * (Pow(numeric, --pow));
        }

        public static bool[] ToBinary(this int numeric)
        {
            return Convert
                .ToString(numeric, 2)
                .Select(o => o == '0' ? false : true)
                .ToArray();
        }

        public static void Normalize(this double[] vector)
        {
            var R = Math.Sqrt(vector.Sum(o => Math.Pow(o, 2)));
            vector = vector.Select(o => Math.Pow(o / R, 2)).ToArray();
            if (!vector.IsNormalized())
                throw new ArgumentOutOfRangeException("Vector normalisation failed");
        }
        public static bool IsNormalized(this double[] vector)
        {
            var sum = vector.Sum();
            var difference = Math.Abs(sum * .00001);
            return (Math.Abs(sum - 1) <= difference);
        }
    }
}
