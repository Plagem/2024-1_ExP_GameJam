using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Monster : MonsterBase
{

    private float limitTime = 5f;
    private float hp = 15;
    private int removeClick = 2;

    protected override void Start()
    { 
        base.Start();
        objectIndex = Random.Range(0, 5);

        Sprite monsterSprite = Resources.Load<Sprite>($"image/Entity/Monster_{objectIndex + 1}");
        if (monsterSprite == null)
        {
            Debug.Log("DoorSprite Null 발생");
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = monsterSprite;

        hp = GameManager.Instance.FloorManager.AllMonsterDataList[objectIndex];

        // 게임 화면에서 게이지 가져오기

        slider.gameObject.SetActive(true);
        warningTab.gameObject.SetActive(true);
        goalHp = 0;
        warningTab.transform.Find("Warning").GetComponent<Image>().sprite = Resources.Load<Sprite>("image/UIs/ui_monster");

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
            SoundManager.Instance.Play("13. monster_battle_bomb");
            
        }

    }

    IEnumerator MonsterAttack()
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        //sprite.color = new Color(200, 255, 255);
        gameObject.transform.localScale = new Vector2(0.95f, 0.90f);
        yield return new WaitForSecondsRealtime(.05f);
        //sprite.color = new Color(255,255,255);
        gameObject.transform.localScale = new Vector2(1f, 1f);

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
            SoundManager.Instance.Play("14. monster_battle_success");
            SoundManager.Instance.StopTick();
        }
        else
        {
            SoundManager.Instance.Play("12. monster_battle_attacked");
        }
        StartCoroutine(MonsterAttack());
    }

    private void UpdateSlideBar()
    {
        slider.value = (hp / 15f);
    }
}
