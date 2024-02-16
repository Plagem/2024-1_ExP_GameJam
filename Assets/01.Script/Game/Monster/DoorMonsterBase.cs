using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DoorMonsterBase : MonsterBase
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    private float damage = 5f;

    protected override void Start()
    {
        base.Start();

        // 게임 화면에서 게이지 가져오기

        slider.gameObject.SetActive(true);
        goalHp = 100;
    }

    protected override void MonsterGetDamage()
    {
        hp += damage;
        if(hp >= goalHp) 
        {
            Debug.Log("몬스터 캐치 성공!");
            Destroy(this.gameObject);
            slider.gameObject.SetActive(false);
            // 아이템 획득 함수 구현하기
        }
    }

    protected override void Update()
    {
        base.Update();

        UpdateSlideBar();

        hp -= Time.deltaTime*10;
        limitTime -= Time.deltaTime;
        if(limitTime < 0)
        {
            Debug.Log("타임 오버!");
            Destroy(this.gameObject);
        }
    }

    private void UpdateSlideBar()
    {
        slider.value = (hp / 100f);
    }
}
