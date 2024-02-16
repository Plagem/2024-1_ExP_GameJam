using UnityEngine;
using UnityEngine.UI;

public class Monster : MonsterBase
{
    private float limitTime = 5f;
    private float hp = 15;

    protected override void Start()
    {
        base.Start();
        objectIndex = Random.Range(0, 5);

        hp = GameManager.Instance.FloorManager.AllMonsterDataList[objectIndex];
        
        // 게임 화면에서 게이지 가져오기

        slider.gameObject.SetActive(true);
        goalHp = 0;
    }

    protected override void Update()
    {
        base.Update();

        UpdateSlideBar();

        limitTime -= Time.deltaTime;
        if (limitTime < 0)
        {
            Debug.Log("타임 오버. 게임 종료");
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
        hp -= damage;
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
