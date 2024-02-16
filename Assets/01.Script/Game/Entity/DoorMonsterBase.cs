using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DoorMonsterBase : MonsterBase
{
    private float hp;

    private DoorData doorData;

    protected override void Start()
    {
        base.Start();
        int randomDoorNum = Random.Range(1, 13);
        Debug.Log($"{randomDoorNum}번 생성");
        doorData = GameManager.Instance.FloorManager.AllDoorDataList[randomDoorNum];

        hp = doorData.hp / 2;
        this.gameObject.name = doorData.name;
        objectIndex = doorData.index;
        bool isRare = doorData.isRare;

        Sprite doorSprite = Resources.Load<Sprite>($"image/Entity/{objectIndex}");
        if(doorSprite == null )
        {
            Debug.Log("DoorSprite Null 발생");
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = doorSprite;

        slider.gameObject.SetActive(true);
        goalHp = doorData.hp;
    }

    protected override void MonsterGetDamage()
    {
        hp += damage;
        if(hp >= goalHp) 
        {
            Debug.Log("몬스터 캐치 성공!");
            Destroy(this.gameObject);
            slider.gameObject.SetActive(false);
            GameManager.Instance.FloorManager.FloorCleared();
            // 아이템 획득 함수 구현하기
        }
    }

    protected override void Update()
    {
        base.Update();

        UpdateSlideBar();

        hp -= Time.deltaTime;
        if(hp <= 0 )
        {
            Debug.Log("잡기 실패. 게임 종료");
            Destroy(this.gameObject);
            GameManager.Instance.IngameUIManager.ShowGameOver();
        }
    }

    private void UpdateSlideBar()
    {
        slider.value = (hp / goalHp);
    }
}
