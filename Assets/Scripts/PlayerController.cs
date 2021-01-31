using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private float _velocidadeDeMovimento = 2;
    [SerializeField] private float _forcaDoPulo = 5;
    [SerializeField] private Rigidbody _rb;


    private PlayerController _jogador = null;
    private Camera _camera;
    private Player _photonPlayer;
    private int _id;
    
    #region Camera Controller

    [Header("Cam Config")] public float sensitivity = 100f;
    public float maxRange = 60f;
    public float minRange = -60f;
    private Renderer _render;
    private bool _interacting;
    [SerializeField]private MouseLook _mouseLook;

    #endregion

    public Rigidbody Rb
    {
        get => _rb;
        set => _rb = value;
    }
    public Camera Camera
    {
        get => _camera;
        set => _camera = value;
    }

    [PunRPC]
    public void Inicializa(Player player)
    {
        _photonPlayer = player;
        _id = player.ActorNumber;
        GameManager.Instance.Jogadores.Add(this);

        if (!photonView.IsMine)
        {
            Rb.isKinematic = true;
            Destroy(Camera.gameObject);
            _render.enabled = true;
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = GetComponentInChildren<Camera>();
        _render = GetComponent<Renderer>();
        _render.enabled = false;
        _interacting = false;

    }

    private void Update()
    {
        if (!GroundCheck()) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray();
            RaycastHit hit;
            if (Physics.Raycast(transform.position,transform.forward,out hit,2))
            {
                if (hit.transform.CompareTag("PuzzleDeTilles"))
                {
                    _interacting = !_interacting;
                }
            }
        }
        if (_interacting)
        {
            _mouseLook.Active = false;
            return;
        }
        _mouseLook.Active = true;
        Move();
        Pula();
    }

    private void FixedUpdate()
    {
        if (Rb.velocity.y >0)
        {
            Rb.mass = 3;
        }
        else
        {
            Rb.mass = 4;
        }
    }

    private bool GroundCheck()
    {
        var ray = new Ray(transform.position, Vector3.down);
        return Physics.Raycast(ray, 0.7f);
    }
    private void Pula()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (GroundCheck())
            {
                Rb.AddForce(Vector3.up * _forcaDoPulo, ForceMode.Impulse);
            }
        }
    }

    private void Move()
    {
        var x = Input.GetAxis("Horizontal") * _velocidadeDeMovimento;
        var z = Input.GetAxis("Vertical") * _velocidadeDeMovimento;
        
        var move = (transform.right * x + transform.forward * z);
        move = (move * _velocidadeDeMovimento);
        move.y = Rb.velocity.y;
        Rb.velocity = move;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
            _jogador = other.gameObject.GetComponent<PlayerController>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
            _jogador = null;
    }
}