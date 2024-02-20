using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorMonsterBase : MonsterBase
{
    private float hp;
    public bool isRare;
    private int removeClick = 2;

    private DoorData doorData;

    private Inventory inventory;

    [SerializeField]
    private GameObject flareEffect;

    protected override void Start()
    {
        base.Start();

        inventory = GameManager.Instance.IngameUIManager.inventory;
        if(inventory == null)
        {
            Debug.Log("Inventory null error");
        }
        int randomDoorNum = Random.Range(1, 13);

        

        // 레어 문 생성
        if (isRare)
        {
            randomDoorNum = 12;
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(8, 8);
            idleSprite = Resources.Load<Sprite>($"image/Entity/12");
            attackedSprite = Resources.Load<Sprite>($"image/Entity/12_2");
            // 사이즈 조정
            transform.localScale = new Vector3(0.4f, 0.4f, 1f);
            limitTime = 10f;
            remainTime = limitTime;
            
            // 반짝이 생성
            GameObject flare = Instantiate(flareEffect, Vector3.zero, Quaternion.identity);
            flare.transform.SetParent(this.transform);
            flare.transform.localScale = Vector3.one * 2.5f;
            flare.transform.localPosition = new Vector3(-10, 5, 0);
        }
        // 일반 문 생성
        else
        {
            randomDoorNum  = Random.Range(1, 12);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(4, 4);
            idleSprite = Resources.Load<Sprite>($"image/Entity/{randomDoorNum}");
            attackedSprite = Resources.Load<Sprite>($"image/Entity/{randomDoorNum}_2");
            limitTime = 5f;
            remainTime = limitTime;
        }

        doorData = GameManager.Instance.FloorManager.AllDoorDataList[randomDoorNum];

        // 처음에 반피 깎여있다
        hp = doorData.hp / 2;
        this.gameObject.name = doorData.name;
        objectIndex = doorData.index;

        gameObject.GetComponent<SpriteRenderer>().sprite = idleSprite;

        // 몬스터 체력바와 WarningTab 생성
        slider.gameObject.SetActive(true);
        warningTab.gameObject.SetActive(true);
        monsterTimer.gameObject.SetActive(true);
        warningTab.transform.Find("Warning").GetComponent<Image>().sprite = Resources.Load<Sprite>("image/UIs/ui_catch");
        goalHp = doorData.hp;
    }

    // 문 공격 시 0.05초동안 스프라이트 변경됨 (타격 이미지로)
    IEnumerator DoorAttack()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = attackedSprite;
        yield return new WaitForSecondsRealtime(.05f);
        gameObject.GetComponent<SpriteRenderer>().sprite = idleSprite;
    }

    protected override void MonsterGetDamage()
    {
        removeClick--;
        if(removeClick == 0)
        {
            warningTab.SetActive(false);
        }

        hp += damage;
        
        if(hp >= goalHp) 
        {
            SoundManager.Instance.Play("9. door_catch_success");
            Destroy(this.gameObject);
            slider.gameObject.SetActive(false);
            monsterTimer.gameObject.SetActive(false);
            GameManager.Instance.FloorManager.FloorCleared();
            inventory.AddItem(doorData);
            SoundManager.Instance.StopTick();
        }

        else
        {
            SoundManager.Instance.Play("8. door_catch_touch");
        }
        StartCoroutine(DoorAttack());
    }

    protected override void Update()
    {
        base.Update();

        UpdateSlideBar();

        remainTime -= Time.deltaTime;
        if (remainTime < 0)
        {
            Destroy(this.gameObject);
            GameManager.Instance.IngameUIManager.ShowGameOver();
            SoundManager.Instance.Play("10. door_catch_failure");

        }

        if (isRare)
        {
            hp -= Time.deltaTime * 6.0f;
        }
        else
        {
            hp -= Time.deltaTime * 4.0f;
        }
        if (hp <= 0 )
        {
            Destroy(this.gameObject);
            GameManager.Instance.IngameUIManager.ShowGameOver();
            SoundManager.Instance.Play("10. door_catch_failure");
        }
    }

    private void UpdateSlideBar()
    {
        slider.value = (hp / goalHp);
        monsterTimer.value = (remainTime / limitTime);
    }

    private void MakeParticle()
    {
        gameObject.AddComponent<ParticleSystem>();

    }
}