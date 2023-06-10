using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapBehaviour : MonoBehaviour
{
    [SerializeField] Animator trapDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.name);
        if(collision.gameObject.layer == 7)
        {
            return;
        }
        Debug.Log("Collision");
        trapDoor.Play("OpenTrap");

    }
}
