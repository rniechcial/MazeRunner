using UnityEngine;
using System.Collections;
using TGK.Communication;

public class Key : MonoBehaviour
{
    bool isTrigger = false;
    public bool gotKey = false;
    public string keyName = "KeyName";
    ItemManager player;
    public float _Radius2 = 2.0f;

    void OnTriggerEnter(Collider other)
    {
        isTrigger = true;

        if(player == null)
        player = other.gameObject.GetComponent<ItemManager>();
        Debug.Log("PLAYER" + player);
        Debug.Log("Detected collision between " + gameObject.name + " and " + other.gameObject.name);
    }

    void OnTriggerExit(Collider other)
    {
        isTrigger = false;
        Debug.Log("ExitDetected collision between " + gameObject.name + " and " + other.name);
    }

    private void OnDrawGizmos()
    {
        // Draw Action message send radius
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _Radius2);
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gotKey = true;
                Debug.Log("Picked object!!"+gotKey);
                var message = new TGK.Communication.Messages.KeyAction()
                {
                    key = this
                };
                var objects = Utility.OverlapSphere(transform.position, _Radius2, false);
                
                MessageDispatcher.Send(message, objects);
                var enume = objects.GetEnumerator();
                while (enume.MoveNext())
                {
                    Debug.Log("ENUM"+enume.Current.name);
               
                }
                //gameObject.SetActive(false);
                /*if (player != null)
                {
                    player.PickUpItem(this.gameObject);
                    this.gameObject.SetActive(false);
                    Debug.Log("Picked object!!");
                }*/
                //Destroy(this.gameObject);
            }
        }
    }

    void OnGUI()
    {
        if (isTrigger)
        {
            GUI.Box(new Rect(0, 60, 200, 25), "Press Space to take key");
        }
    }
}
