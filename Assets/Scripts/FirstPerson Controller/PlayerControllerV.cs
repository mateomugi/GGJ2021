using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV : MonoBehaviour
{
    #region Character Movement

    [Header("Character Config")]
    public float speed = 3f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    [Header("Ground Config")] 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    #endregion

    #region Camera Controller

    [Header("Cam Config")] public float sensitivity = 100f;
    public float maxRange = 60f;
    public float minRange = -60f;

    #endregion

    #region Private

    private CharacterController _controller;
    private Vector3 _velocity;
    private bool isGrounded;

    #endregion

    /*private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var move = transform.right * x + transform.forward * z;

        _controller.Move(move * (speed * Time.deltaTime));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        _velocity.y += gravity * Time.deltaTime;
        
        _controller.Move(_velocity * Time.deltaTime);
    }*/
}
