using UnityEngine;
using UnityEngine.UI;

public class Monster : MonsterBase
{
    private float limitTime = 5f;
    private float hp = 100;

    protected override void Start()
    {
        base.Start();

        // 게임 화면에서 게이지 가져오기

        slider.gameObject.SetActive(true);
        hp = 100;
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
            // 게임 종료 함수
            Destroy(this.gameObject);
        }
    }

    protected override void MonsterGetDamage()
    {
        hp -= damage;
        if (hp <= goalHp)
        {
            Debug.Log("몬스터 사살 성공!");
            Destroy(this.gameObject);
            slider.gameObject.SetActive(false);
            // 아이템 획득 함수 구현하기
        }
    }

    private void UpdateSlideBar()
    {
        slider.value = (hp / 100f);
    }


}
