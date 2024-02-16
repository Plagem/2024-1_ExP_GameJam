using UnityEngine;using UnityEngine.EventSystems;


public class MonsterBase : ScriptableObject, IPointerClickHandler
{
    public float limitTime;
    public int hp;
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }


    public virtual void Success()
    {
        
    }
}
