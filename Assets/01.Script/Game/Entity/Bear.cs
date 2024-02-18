
using System.Collections;
using UnityEngine;

public class Bear: MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;

    public static GameObject bear;

    public IEnumerator BearRoutine(float time, float sizeMultiplier)
    {
        bear = gameObject;
        yield return StartCoroutine(EnlargeRoutine(time, sizeMultiplier));

        GameManager.Instance.IngameUIManager.ShowGameOver(); //"5. game_over");
        // SoundManager.Instance.Play("5. game_over");
    }
    
    public IEnumerator EnlargeRoutine(float time, float sizeMultiplier)
    {
        float currentTime = 0;
        Vector3 original = transform.localScale;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            transform.localScale = original * Mathf.Lerp(1,sizeMultiplier,currentTime/time);
            yield return null;
        }
    }
}
