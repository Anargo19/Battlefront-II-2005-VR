using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class WeaponBehavior : MonoBehaviour
{
    [SerializeField] public Transform bulletPoint;
    [SerializeField] public Transform bullet;
    [SerializeField] public Transform rayOrigin;
    [SerializeField] public int bulletAmount;
    [SerializeField] public int maxAmmo;
    [SerializeField] public int maxAmmoMagazine;
    [SerializeField] public int currentAmmoMagazine;
    [SerializeField] public AudioClip weaponFire;
    [SerializeField] public testInput testInput;
    [SerializeField] private Image magazine;
    [SerializeField] private TextMeshProUGUI magazineText;

    [SerializeField] SteamVR_Input_Sources source;
    [SerializeField] Interactable _interactable;



    // Start is called before the first frame update
    void Start()
    {
        currentAmmoMagazine = maxAmmoMagazine;
        _interactable = GetComponent<Interactable>();
        
    }
    

    public int CheckAmmo()
    {
        int newAmount = 0;
        if (currentAmmoMagazine < bulletAmount && currentAmmoMagazine > 0) {
            newAmount = currentAmmoMagazine;
            return newAmount;
        }
        if (currentAmmoMagazine >= bulletAmount)
        {
            newAmount = bulletAmount;
            return newAmount;

        }
        return 0;
    }

    public void ChangeAmmoCount(int amount)
    {
        Debug.Log("Changed ammount from : " + currentAmmoMagazine + " to : " + currentAmmoMagazine + amount);
        currentAmmoMagazine += amount;
        magazine.fillAmount = (float)currentAmmoMagazine / (float)maxAmmoMagazine;
        magazineText.text = $"{currentAmmoMagazine}/{maxAmmoMagazine}";

    }

    // Update is called once per frame
    void Update()
    {
        if(source == SteamVR_Input_Sources.LeftHand || source == SteamVR_Input_Sources.RightHand)
        {
            if (SteamVR_Actions._default.GrabPinch.GetStateDown(source))
            {
                StartCoroutine(Fire());
            }
        }
    }

    public IEnumerator Fire()
    {
        int bulletAmount = CheckAmmo();
        Debug.Log(bulletAmount);
        if (bulletAmount <= 0)
        {
            yield break;
        }
        Debug.Log("Fire Continue");
        for (int i = 0; i < bulletAmount; i++)
        {
            AudioSource.PlayClipAtPoint(weaponFire, transform.position);
            Instantiate(bullet, bulletPoint.position + Vector3.forward * bullet.localScale.y, bulletPoint.rotation);
            ChangeAmmoCount(-1);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    public void SetSource()
    {
        source = _interactable.attachedToHand.handType;
    }

    public void RemoveSource()
    {
        source = SteamVR_Input_Sources.Keyboard;
    }
}
