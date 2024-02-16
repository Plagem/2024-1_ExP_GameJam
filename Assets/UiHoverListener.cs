using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiHoverListener : MonoBehaviour
{
    public bool isUIOverride { get; private set; }

    // Update is called once per frame
    void Update()
    {
        isUIOverride = EventSystem.current.IsPointerOverGameObject();
    }
}
