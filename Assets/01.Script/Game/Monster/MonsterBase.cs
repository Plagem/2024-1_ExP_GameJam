using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;


public abstract class MonsterBase : MonoBehaviour
{
    [SerializeField]
    protected float limitTime = 5f;
    [SerializeField]
    protected float hp = 50f;

    protected float goalHp;
    private bool isValidTarget;

    float maxDistance = 15f;
    Vector3 mousePosition;

    protected virtual void Start()
    {

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
