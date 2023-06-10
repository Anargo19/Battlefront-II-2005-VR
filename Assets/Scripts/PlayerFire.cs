using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] WeaponBehavior weapon;
    [SerializeField] Transform bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fire()
    {
        AudioSource.PlayClipAtPoint(weapon.weaponFire, transform.position);
        Debug.Log(weapon.bulletPoint.position);
        Instantiate(bullet, weapon.bulletPoint.position + Vector3.forward * bullet.localScale.y, weapon.bulletPoint.rotation);
    }
}
