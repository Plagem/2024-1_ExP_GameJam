using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Monster : MonsterBase
{
    private float limitTime = 5f;
    private float hp = 15;
    private int removeClick = 2;

    protected override void Start()
    {
        base.Start();
        objectIndex = Random.Range(0, 5);

        hp = GameManager.Instance.FloorManager.AllMonsterDataList[objectIndex];
        
        // 게임 화면에서 게이지 가져오기

        slider.gameObject.SetActive(true);
        warningTab.gameObject.SetActive(true);
        warningTab.transform.Find("Warning").Find("WarningMessage").GetComponent<TMP_Text>().text = "Monster";
        goalHp = 0;

    }

    protected override void Update()
    {
        base.Update();

        UpdateSlideBar();

        limitTime -= Time.deltaTime;
        if (limitTime < 0)
        {
            Destroy(this.gameObject);
            GameManager.Instance.IngameUIManager.ShowGameOver();
        }

        Sprite monsterSprite = Resources.Load<Sprite>($"image/Entity/Monster_{objectIndex+1}");
        if (monsterSprite == null)
        {
            Debug.Log("DoorSprite Null 발생");
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = monsterSprite;
    }

    protected override void MonsterGetDamage()
    {
        removeClick--;
        hp -= damage;
        if(removeClick == 0)
        {
            warningTab.gameObject.SetActive(false);
        }
        if (hp <= goalHp)
        {
            Debug.Log("몬스터 사살 성공!");
            Destroy(this.gameObject);
            slider.gameObject.SetActive(false);
            GameManager.Instance.FloorManager.FloorCleared();
        }
    }

    private void UpdateSlideBar()
    {
        slider.value = (hp / 15f);
    }


}
