using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance = null;

    public GameObject inventory;
    private InventoryManage invenMng;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        invenMng = inventory.GetComponent<InventoryManage>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1 << 7))
            {
                InteractionObj interObj = hit.transform.GetComponent<InteractionObj>();
                if (interObj != null)
                {
                    if (interObj.canChoose)
                    {
                        if (hit.transform.gameObject.CompareTag("Chair"))
                        {
                            Debug.Log("chair");
                        }
                        else if (hit.transform.gameObject.CompareTag("Desk"))
                        {
                            Debug.Log("Desk");
                        }
                    }
                }
            }
            else if(Physics.Raycast(ray, out hit, 1 << 5))
            {
                ItemManager interObj = hit.transform.GetComponent<ItemManager>();
                if (interObj != null)
                {
                    if(interObj.itemNum == 0)
                    {
                        Debug.Log("RedCircle");
                        invenMng.DelItem(hit.transform.gameObject);
                    }
                }
            }
        }
    }
}
