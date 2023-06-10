using System.Collections;
using System.Collections.Generic;
using UltimateXR.Manipulation;
using UnityEngine;
using UnityEngine.InputSystem;



namespace UltimateXR.Mechanics.Weapons
{
    public class GrenadeBehavior : MonoBehaviour
    {
        public AudioClip grenadeLaunch;
        public AudioClip grenadeExplode;
        public ParticleSystem explosion;
        public InputAction action;
        public UxrGrabbableObject uxrGrabbable;

        // Start is called before the first frame update
        void Start()
        {
            //GrenadeThrow();
            uxrGrabbable = GetComponent<UxrGrabbableObject>();
            uxrGrabbable.Released += GrenadeThrow;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void GrenadeThrow(object sender, UxrManipulationEventArgs e)
        {

            AudioSource.PlayClipAtPoint(grenadeLaunch, transform.position);
            //StartCoroutine(GrenadeExplode());
        }

        IEnumerator GrenadeExplode()
        {
            yield return new WaitForSeconds(2);
            AudioSource.PlayClipAtPoint(grenadeExplode, transform.position);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
