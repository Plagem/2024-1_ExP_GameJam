using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIzeEffect : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField]
    float translateSpeed = 0.25f;
    float translateAbs = 0;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        translateAbs += Time.deltaTime * translateSpeed;
        if(translateAbs > 0.5f)
        {
            translateSpeed = -1.5f;
        }
        else if(translateAbs < 0.25f)
        {
            translateSpeed = 1.5f;
        }
        
        rectTransform.localScale = Vector3.one * (0.5f + translateAbs) * 0.75f;
    }
}
