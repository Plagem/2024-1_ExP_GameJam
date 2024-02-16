
using UnityEngine;
using UnityEngine.UI;

public abstract class MonsterBase : MonoBehaviour
{
    protected Sprite idleSprite;
    protected Sprite attackedSprite;

    [SerializeField]
    protected Slider slider;
    [SerializeField]
    protected GameObject warningTab;

    protected float damage = 1f;
    protected float goalHp;
    protected int objectIndex;

    float maxDistance = 15f;
    Vector3 mousePosition;


    public static GameObject monster;
    
    protected virtual void Start()
    {
        slider = GameManager.Instance.IngameUIManager.monsterSlider;
        warningTab = GameManager.Instance.IngameUIManager.warningTab;
        monster = gameObject;
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }

    private void CastRay()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, maxDistance);

        if (hit.collider == null) return;

        if (hit.collider.gameObject == this.gameObject)
        {
            MonsterGetDamage();
        }
    }

    protected abstract void MonsterGetDamage();

    public virtual void Success()
    {
        Debug.Log("Monster: success");
    }
}
