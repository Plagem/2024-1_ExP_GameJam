using UnityEngine;using UnityEngine.EventSystems;


public class MonsterBase : ScriptableObject, IPointerClickHandler
{
    public float limitTime;
    public int hp;
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(--hp <= 0)
            Success();
    }


    public virtual void Success()
    {
        Debug.Log("Monster: success");
    }
}
