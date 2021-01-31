using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    #region Player Config

    [Header("Player")]
    public Transform player;
    private PlayerController _pController;

    #endregion

    #region Rotate Configs

    private float _xRotation = 0f;
    private bool _active;

    public bool Active
    {
        private get => _active;
        set => _active = value;
    }

    #endregion

    private void Start()
    {
        _pController = player.GetComponent<PlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!Active)
        {
            Cursor.lockState = CursorLockMode.None;
            return;
        }else
            Cursor.lockState = CursorLockMode.Locked;
        var mouseX = Input.GetAxis("Mouse X") * _pController.sensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * _pController.sensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, _pController.minRange, _pController.maxRange);
        
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
