using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RedCircle : MonoBehaviour, IPointerClickHandler
{
    private InventoryManage invenMng;

    private void Start()
    {
        invenMng = GameObject.Find("InventoryPanel").GetComponent<InventoryManage>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        invenMng.DelItem(this.gameObject);
    }
}
