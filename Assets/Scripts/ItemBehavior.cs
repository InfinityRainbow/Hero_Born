using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameBehavior GameManager ;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="Player")
        {
            Destroy(this.transform.gameObject);
            Debug.Log("Item collected!!!");
            GameManager.Items += 1;

            GameManager.PrintLootReport();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
