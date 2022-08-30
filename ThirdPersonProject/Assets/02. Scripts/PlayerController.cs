using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigid;
    private Animator playerAnimator;

    private RaycastHit hit; // 1.2
    
    public float moveSpeed = 8f;
    public float jumpForce = 250f;

    private Vector3 movement;

    public GameObject gameManager;
    private GameManager gameMng;
    public GameObject inventory;
    private InventoryManage invenMng;



    private void Awake()
    {
        playerRigid = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        gameMng = gameManager.GetComponent<GameManager>();
        invenMng = inventory.GetComponent<InventoryManage>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어 이동
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        if (h != 0 || v != 0)
        {
            movement = moveDir.normalized * moveSpeed * Time.deltaTime;
            playerRigid.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.1f);
            playerRigid.MovePosition(transform.position + movement);
        }

        Debug.DrawRay(transform.position, transform.up * -1f, Color.red, 0.1f);
        Debug.DrawRay(transform.position, transform.forward, Color.green, 0.5f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, transform.up * -1f, out hit, 0.1f, 1 << LayerMask.NameToLayer("Ground")))
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    playerRigid.AddForce(Vector3.up * jumpForce); ;
                    playerAnimator.SetTrigger("JumpTrigger");
                }
            }
        }

        PlayRunAnim(h, v);
    }

    void PlayRunAnim(float h, float v)
    {
        if (h != 0 || v != 0)
        {
            playerAnimator.SetBool("IsRunning", true);
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            int itemNumber = other.GetComponent<ItemManager>().itemNum;
            if(itemNumber == 0)
            {
                GameObject getItem = gameMng.itemList[0];
                invenMng.PutItem(getItem);

            }
        }
    }
}
