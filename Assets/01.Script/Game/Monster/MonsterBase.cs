using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MonsterBase : MonoBehaviour
{
    [SerializeField]
    protected float limitTime = 5f;
    [SerializeField]
    protected int hp = 5;

    private bool isValidTarget;

    float maxDistance = 15f;
    Vector3 mousePosition;

    protected virtual void Update()
    {
        // 터치시에는 달라지게 변경하자
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

    private void MonsterGetDamage()
    {
        if (--hp <= 0)
        {
            Destroy(this.gameObject);
        }
        Debug.Log($"Enemy 현재 체력 : {hp}");
    }

    public virtual void Success()
    {
        Debug.Log("Monster: success");
    }
}
