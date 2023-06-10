using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private KeyboardPlayer keyboardPlayer;

    private Animator m_animator;

    private float velocity;

    public Camera m_camera;
    public float m_turnSpeed = 10f;

    private Vector3 m_directionOfMove;

    public enum STATE
    {
        DEFAULT,
        AIMING
    }

    void Awake()
    {
        keyboardPlayer = new KeyboardPlayer();
        m_animator = GetComponent<Animator>();

    }

    private void OnEnable()
    {
        keyboardPlayer.Enable();
    }

    private void OnDisable()
    {
        keyboardPlayer.Disable();
    }

    // Update is called once per frame
    void Update()
    {


        Vector2 value = keyboardPlayer.Movement.Movement.ReadValue<Vector2>();
        float run = keyboardPlayer.Movement.Run.ReadValue<float>();
        float fire = keyboardPlayer.Movement.Fire.ReadValue<float>();
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), -Vector3.up * 2, Color.red);
        bool isGrounded = Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), -Vector3.up, 2f);

        m_animator.SetFloat("Fire", fire);
        m_animator.SetBool("isGrounded", isGrounded);

        Debug.Log(value.x + "  " + value.y);
        float speed = Mathf.Abs(value.x) + Mathf.Abs(value.y);
                speed = Mathf.Clamp(speed, 0, 1f);
                speed += run;
                speed = Mathf.SmoothDamp(m_animator.GetFloat("Speed"), speed, ref velocity, .1f);
                m_animator.SetFloat("Speed", speed);

                m_directionOfMove = ExtractDirectionFromCamera();

                if (!(value.magnitude > .1f)) return;

                Vector3 moveVector = new Vector3(value.x, 0, value.y);
                Quaternion rotation = Quaternion.LookRotation(m_directionOfMove, Vector3.up) * Quaternion.LookRotation(moveVector, Vector3.up);
                Quaternion lerpRotation = Quaternion.Lerp(transform.rotation, rotation, m_turnSpeed * Time.deltaTime);

                transform.rotation = lerpRotation;
        


    }

    private Vector3 ExtractDirectionFromCamera()
    {
        return Vector3.ProjectOnPlane(m_camera.transform.forward, Vector3.up);
    }

}