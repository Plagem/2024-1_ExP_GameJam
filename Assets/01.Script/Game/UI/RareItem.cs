
using System;
using System.Collections;
using UnityEngine;

public class RareItem:MonoBehaviour
{
    public DoorItem item;
    public Inventory inventory;


    private void Start()
    {
        StartCoroutine(go());
    }

    public IEnumerator go()
    {
        SoundManager.Instance.Play("16. rare_door_revive");
        // yield return StartCoroutine(goLeft());
        GetComponent<Animator>().SetTrigger("gogogo");
        transform.GetComponent<RectTransform>().position = new Vector3(Screen.width*0.5f,Screen.height*0.5f,0f);
        yield return new WaitForSeconds(2.0f);
        Use();
            
    }

    public IEnumerator goLeft()
    {
        for(int i = 0; i < 10; i++)
        {
            transform.Translate(-1.25f, 0, 0);
            yield return null;
        }
    }

    public void OnMouseDown()
    {
        
    }


    public void Use()
    {
        BaseGate gate = GameManager.Instance.FloorManager.Gates[2];

        foreach (var _gate in GameManager.Instance.FloorManager.Gates)
        {
            if (_gate.State != BaseGate.GateState.Close && _gate.State != BaseGate.GateState.Focus)
            {
                gate = _gate;
            }
        }
        gate.SetDisable();
        item.init(inventory.None);
        GameManager.Instance.IngameUIManager.isGameClickDisabled = false;
        
        Destroy(gameObject);
    }
}
