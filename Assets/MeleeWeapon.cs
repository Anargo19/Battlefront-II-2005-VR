using System.Collections;
using System.Collections.Generic;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Manipulation;
using UltimateXR.Mechanics.Weapons;
using UnityEngine;

public class MeleeWeapon : UxrWeapon
{
    [SerializeField] int damage;
    [SerializeField] List<AudioClip> sounds;



    private void OnTriggerEnter(Collider other)
    {
        UxrActor actor = other.GetComponentInParent<UxrActor>();
        if(actor == null) { return; }
        Debug.Log(actor.name);
        actor.ReceiveDamage(damage);
        GetComponent<AudioSource>().PlayOneShot(sounds[Random.Range(0,sounds.Count-1)]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
