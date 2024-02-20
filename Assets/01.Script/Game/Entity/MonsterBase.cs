
using _01.Script;
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
    [SerializeField]
    protected Slider monsterTimer;

    protected float damage = 1f;
    protected float goalHp;
    protected int objectIndex;

    protected float limitTime;
    protected float remainTime;

    float maxDistance = 15f;
    Vector3 mousePosition;


    public static GameObject monster;
    
    protected virtual void Start()
    {
        slider = GameManager.Instance.IngameUIManager.monsterSlider;
        warningTab = GameManager.Instance.IngameUIManager.warningTab;
        monsterTimer = GameManager.Instance.IngameUIManager.monsterTimer;
        monster = gameObject;
        SoundManager.Instance.PlayTick();
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }
    public void AlignGate(BaseGate gate)
    {
        SpriteRenderer gateRenderer = gate.GetComponent<SpriteRenderer>();
        float GateLeftPos = gate.transform.position.x - Utility.GetSpriteSize(gateRenderer,transform).x / 2;
        float monsterXsize = Utility.GetSpriteSize(GetComponent<SpriteRenderer>(),transform).x;
        transform.position =
            new Vector3(GateLeftPos + monsterXsize / 2, transform.position.y, 0);
    }
    private void CastRay()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, maxDistance);

        if (hit.collider == null) return;

        if (GameManager.Instance.IngameUIManager.isGameClickDisabled || GameManager.Instance.IngameUIManager.UiHoverListener.isUIOverride)
            return;
        
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
