using System.Collections;
using System.Collections.Generic;
using UltimateXR.Core;
using UltimateXR.Mechanics.Weapons;
using Unity.VisualScripting;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    float lifespan = 5;
    MeshRenderer mr;
    CapsuleCollider collider;
    bool touched;
    Vector3 oldPos;
    Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        //collider= GetComponent<CapsuleCollider>();
        Destroy(gameObject, lifespan);
        oldPos= transform.position;
        
    }
    

    // Update is called once per frame
    /*void FixedUpdate()
    {
        //transform.Translate(Vector3.down * Time.deltaTime * 16);
        //Debug.DrawRay(transform.position, transform.forward, Color.yellow);
        //Debug.DrawRay(transform.position, -transform.forward, Color.magenta);
        newPos= transform.position;
        if(oldPos!= newPos)
        {
            newPos = oldPos + transform.forward * 60 * Time.deltaTime;
            transform.position = newPos;
        }
        oldPos= newPos;

        RaycastHit hit;
        Debug.DrawRay(oldPos, transform.forward, Color.red);
        if (Physics.Raycast(oldPos, -transform.forward, out hit, 1))
        {
            if(hit.collider.tag != "Gun")
                Touched(hit.collider);
        }
    }*/

    public void OnTriggerEnter(Collider other)
    {
        //Touched(other);
    }

    private void Touched(Collider other)
    {
        if (touched) { return; }
        touched = true;
        //collider.enabled= false;
        //Debug.Log(other.name);
        //mr.enabled = false;
        switch (other.tag)
        {
            case "Clone":
                //other.transform.GetComponentInParent<SoldierManager>().LoseHealth(4);
                break;
            case "Head":
               // other.transform.GetComponentInParent<SoldierManager>().LoseHealth(20);
                break;
            case "Player":
                AudioSource.PlayClipAtPoint(other.GetComponentInChildren<testInput>().hurt, other.transform.position);
                break;
        }

        Destroy(gameObject);
    }


}
