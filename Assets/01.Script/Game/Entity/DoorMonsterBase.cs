using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DoorMonsterBase : MonsterBase
{
    private float hp = 50f;

    private DoorData doorData;

    protected override void Start()
    {
        base.Start();
        int randomDoorNum = Random.Range(1, 13);
        doorData = GameManager.Instance.FloorManager.AllDoorDataList[randomDoorNum];

        hp = doorData.hp;
        this.gameObject.name = doorData.name;
        objectIndex = doorData.index;
        bool isRare = doorData.isRare;

        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Resources/image/Entity/{objectIndex}");

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
        if(hp <= 0 )
        {
            Debug.Log("잡기 실패. 게임 종료");
            Destroy(this.gameObject);
            // 게임 종료 함수
        }
    }

    private void UpdateSlideBar()
    {
        slider.value = (hp / 100f);
    }
}
