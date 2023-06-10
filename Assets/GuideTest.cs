using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Guides;
using UltimateXR.Haptics;
using UltimateXR.Mechanics.Weapons;
using UnityEngine;

public class GuideTest : MonoBehaviour
{

    public GameObject compassTarget;
    public UxrFirearmWeapon weapon;
    public GameObject grenade;
        public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        UxrCompass.Instance.SetTarget(compassTarget.transform, UxrCompassDisplayMode.Look);
        UxrCompass.Instance.SetTarget(weapon.transform, UxrCompassDisplayMode.Grab);
        weapon.ProjectileShot += Weapon_ProjectileShot;
    }

    private void Weapon_ProjectileShot(int obj)
    {
        UxrAvatar.LocalAvatar.ControllerInput.SendHapticFeedback(UxrHandSide.Right, UxrHapticClipType.Shot, 1.0f);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
