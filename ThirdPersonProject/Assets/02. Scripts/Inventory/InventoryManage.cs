using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManage : MonoBehaviour
{
    private bool isOpen = false;

    public bool[] fullCheck;
    public GameObject[] slots;
    private int emptySlotNum;
    private GameObject instItem;

    private void Awake()
    {
        fullCheck = new bool[16];
    }

    public void InventoryCheck()
    {
        isOpen = !isOpen;
        if(isOpen == false)
        {
            GetComponent<RectTransform>().position = new Vector3(2158f, 750.1f, 0f);
        }
        else
        {
            GetComponent<RectTransform>().position = new Vector3(1605.8f, 750.1f, 0f);
        }
    }
    
    public GameObject PutItem(GameObject item) // slot에 아이템 넣기
    {
        for(int i = 0; i < fullCheck.Length; i++)
        {
            if(fullCheck[i] == false)
            {
                instItem = Instantiate(item, slots[i].transform.position, Quaternion.identity);
                instItem.transform.SetParent(slots[i].transform);
                fullCheck[i] = true;

                break;
            }
        }

        return instItem;
    }

    public void DelItem(GameObject currItem) // slot에서 아이템 삭제
    {
        int currSlotNum = currItem.GetComponentInParent<SlotManage>().slotNum;
        fullCheck[currSlotNum] = false;
        for(int i = 0; i < fullCheck.Length; i++)
        {
            if(fullCheck[i] == false)
            {
                emptySlotNum = i;
                break;
            }
        }

        Destroy(currItem);

        for (int j = emptySlotNum + 1; j < fullCheck.Length; j++)
        {
            if(fullCheck[j] == true)
            {
                GameObject otherItem = slots[j].transform.GetChild(0).gameObject;
                otherItem.transform.SetParent(slots[j - 1].transform);
                otherItem.transform.position = slots[j - 1].transform.position;
            }

        }
    }
}
