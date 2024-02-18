using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
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

        public static IEnumerator WaitExecute(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }

        public static Vector3 GetSpriteSize(SpriteRenderer sr, Transform transform)
        {
            Vector3 worldSize = Vector3.zero;
            Vector2 sprSize = sr.sprite.rect.size;
            Vector2 localSprSize = sprSize / sr.sprite.pixelsPerUnit;
            Debug.Log(sr.sprite.rect.size.x);
            worldSize = localSprSize;
            worldSize.x *= transform.lossyScale.x;
            worldSize.y *= transform.lossyScale.y;
            Debug.Log($"{localSprSize.x} * {transform.lossyScale.x}=> {worldSize.x}");
            return worldSize;
        }
    }
}