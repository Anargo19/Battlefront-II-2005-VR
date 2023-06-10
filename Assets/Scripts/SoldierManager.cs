using System;
using System.Collections;
using System.Collections.Generic;
using UltimateXR.Mechanics.Weapons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public class SoldierManager : MonoBehaviour
{
    [SerializeField] Image healthBar;
    int health = 10;
    int maxHealth = 10;
    public UnityEvent isDying = new UnityEvent();
    [SerializeField ] List<Collider> colliders= new List<Collider>();
    public AudioClip death;
    [SerializeField] WeaponBehavior weapon;
    [SerializeField] Transform bullet;
    [SerializeField] UxrActor actor;
    float maxLife;
    // Start is called before the first frame update
    
    
    
    void Start()
    {
        actor = GetComponent<UxrActor>();
        maxLife = actor.Life;
        isDying.AddListener(Dying);
        actor.DamageReceived += LoseHealth;
        

    }

    private void LoseHealth(object sender, UxrDamageEventArgs e)
    {
        //Debug.Log($"{e.ActorSource.name} a tiré sur {e.ActorTarget.name} en infligeant {e.Damage} dégats de type {e.DamageType}");
        Debug.Log(health / maxHealth);
        actor.Life -= e.Damage;
        healthBar.fillAmount = (float)actor.Life / (float)maxLife;

        if (actor.Life <= 0)
        {
            isDying.Invoke();
            healthBar.transform.parent.gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Dying()
    {
        
            transform.GetComponent<Animator>().Play("Death");
        foreach(Collider collider in colliders)
        {
            collider.enabled = false;
        }
            AudioSource.PlayClipAtPoint(death, transform.position);
            Destroy(transform.gameObject, 3);

        

    }

    public void Fire()
    {
        AudioSource.PlayClipAtPoint(weapon.weaponFire, transform.position);
        Instantiate(bullet, weapon.bulletPoint.position + Vector3.forward * bullet.localScale.y, weapon.bulletPoint.rotation);
    }
}
