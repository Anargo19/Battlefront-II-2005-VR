using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class testInput : MonoBehaviour
{
    public InputActionManager inputActions;
    testInput test;
    XRRayInteractor xRRayInteractor;
    public AudioClip fireSound;
    public AudioClip hurt;
    public AudioClip grenadeLaunch;
    public Transform bullet;
    public Transform bulletPoint;
   [SerializeField] List<WeaponBehavior> weapons = new List<WeaponBehavior>();
    int currentWeapon = 0;
    WeaponBehavior currentWeaponBehavior;

    bool waiting;
    // Start is called before the first frame update
    void Start()
    {
        test = GetComponent<testInput>();
        xRRayInteractor = GetComponent<XRRayInteractor>();
        inputActions.actionAssets[0].FindActionMap("XRI RightHand Interaction").FindAction("Activate").performed += context => { if (!waiting) { StartCoroutine(Fire()); StartCoroutine(Wait()); } } ;
        inputActions.actionAssets[0].FindActionMap("XRI RightHand Interaction").FindAction("ChangePrimaryWeapon").performed += context => ChangeWeapon(1);

        currentWeaponBehavior = weapons[0];
        ChangeWeapon(0);
    }

    private IEnumerator Wait()
    {
        waiting = true;
        yield return new WaitForSeconds(0.5f);
        waiting = false;
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
       // InputAction fire = inputActions.actionAssets[0].FindAction("Fire");

    }

    public IEnumerator Fire()
    {
        int bulletAmount = currentWeaponBehavior.CheckAmmo();
        Debug.Log(bulletAmount);
        if (bulletAmount <= 0)
        {
            yield break;
        }
        Debug.Log("Fire Continue");
        for (int i =0; i < bulletAmount; i++)
        {
            AudioSource.PlayClipAtPoint(weapons[currentWeapon].weaponFire, transform.position);
            Instantiate(bullet, bulletPoint.position+Vector3.forward*bullet.localScale.y, bulletPoint.rotation);
            currentWeaponBehavior.ChangeAmmoCount(-1);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    public void ChangeWeapon(int amount)
    {
        currentWeaponBehavior.gameObject.SetActive(false);
        currentWeapon += amount;
        if (currentWeapon == weapons.Count) { currentWeapon = 0; }
        else if (currentWeapon < 0) { currentWeapon = weapons.Count-1; }

        currentWeaponBehavior = weapons[currentWeapon];
        currentWeaponBehavior.gameObject.SetActive(true);
        xRRayInteractor.attachTransform = currentWeaponBehavior.rayOrigin;
        xRRayInteractor.rayOriginTransform = currentWeaponBehavior.rayOrigin;
        test.bulletPoint = currentWeaponBehavior.bulletPoint;
        currentWeaponBehavior.ChangeAmmoCount(0);



    }

    public void Grenade()
    {
        AudioSource.PlayClipAtPoint(grenadeLaunch, transform.position);
        Debug.Log("GRENADE!");
    }
}
