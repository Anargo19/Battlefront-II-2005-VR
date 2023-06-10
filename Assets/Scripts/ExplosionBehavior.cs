using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {

        ps = GetComponent<ParticleSystem>();
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 2.25f, Vector3.forward);
        if(hits.Length > 0 ) 
        {
            foreach(RaycastHit hit in hits)
            {
                if(hit.collider.tag == "Clone")
                {

                    //hit.transform.GetComponentInParent<SoldierManager>().LoseHealth(30);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        //if(!ps.IsAlive()) { Destroy(gameObject); }
    }



}
