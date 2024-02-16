using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DoorMonsterBase : MonsterBase
{
    Slider slider;

    private void Start()
    {
        hp = 15;
    }

    protected override void Update()
    {
        base.Update();

        limitTime -= Time.deltaTime;
        if(limitTime < 0)
        {
            Debug.Log("타임 오버!");
            Destroy(this.gameObject);
        }
    }
}
