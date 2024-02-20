using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareEffect : MonoBehaviour
{
    [SerializeField]
    private float resizingSpeed;

    private float flareSize;
    private bool isIncreasing;

    private void Start()
    {
        isIncreasing = UnityEngine.Random.Range(0, 2) == 0 ? false : true;
        flareSize = UnityEngine.Random.Range(0f, 1.0f);
    }

    void Update()
    {
        if(flareSize < 0)
        {
            resizingSpeed = Mathf.Abs(resizingSpeed);
        }
        else if(flareSize > 1)
        {
            resizingSpeed = -Mathf.Abs(resizingSpeed);
        }

        flareSize += resizingSpeed * Time.deltaTime;

        this.transform.localScale = Vector3.one * flareSize;
    }
}
