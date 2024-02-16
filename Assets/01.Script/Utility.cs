using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace _01.Script
{
    public static class Utility
    {


        public static int WeightedRandom(params int[] probalities)
        {
            int sum = 0;
            foreach (var n in probalities)
            {
                sum += n;
            }

            int random = Random.Range(0, sum);

            for (int i = 0; i < probalities.Length; i++)
            {
                if (random < probalities[i])
                    return i;
                random -= probalities[i];
            }

            return - 1;
        }
        
    }
}