using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class PlayerInputHandler : MonoBehaviour
{
    // Input variables
    private ArrayList players = new ArrayList();
    public float index;
    private int sceneIndex;
    private PlayerInput playerInput;
    private Player player1;
    private Player2 player2;
    private GameObject cs_holder;
    private PhotonView cs_photonView;
    private CS_Control cs;
    private PhotonView photonView;
    public bool hostClient = false;
    QinyangControls controls;
    Vector2 move;

    // Initialization
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        cs_holder = GameObject.Find("cc_control");
        cs = cs_holder.GetComponent<CS_Control>();

        if(PhotonNetwork.IsMasterClient){
            hostClient = true;
        }

        if(PhotonNetwork.OfflineMode){
            //photonView.TransferOwnership(Photon.Realtime.Player.Get());
        } else{
            cs_photonView = cs_holder.GetPhotonView();
            if (PhotonNetwork.PlayerList.Length == 2)
            {
                index = 1;
            }
            else
            {
                index = 0;
            }

        }

        if (photonView.IsMine)
        {
            DontDestroyOnLoad(gameObject);
            playerInput = GetComponent<PlayerInput>();
            player1 = FindObjectOfType<Player>();
            player2 = FindObjectOfType<Player2>();
            players.Add(player1);
            players.Add(player2);
            //index = playerInput.playerIndex;
            //print("inx:"+index);
            
            controls = new QinyangControls();
        }
    }
    private void Update()
    {
        if (photonView.IsMine)
        {
            if (player1 == null)
            {
                player1 = FindObjectOfType<Player>();
            }

            if (player2 == null)
            {
                player2 = FindObjectOfType<Player2>();
            }
        }
    }

    // onMove function 
    // Allows players to move through callback context from input device
    public void OnMove(CallbackContext context)
    {
        if (photonView.IsMine)
        {
            if (player1 != null && player2 != null)
            {
                if (index == 0)
                {
                    player1.SetInputVector(context.ReadValue<Vector2>());
                }
                else
                {
                    player2.SetInputVector(context.ReadValue<Vector2>());
                }
            }

            if (SceneManager.GetActiveScene().buildIndex == 3 && cs != null && index == 0)
            {
                cs_photonView.RPC("moveMe",RpcTarget.AllBuffered,context.ReadValue<Vector2>());
                //cs.moveMe(context.ReadValue<Vector2>());
            }
        }


    }

    // onAttack function 
    // Allows players to call their attack function through callback context from input device
    public void onAttack(CallbackContext context)
    {
        if (photonView.IsMine)
        {
            if (player1 != null && player2 != null)
            {
                if (index == 0)
                {
                    if (context.performed) player1.photonView.RPC("Attack", RpcTarget.All);//player1.Attack();
                }
                else
                {
                    if (context.performed) player2.photonView.RPC("Attack", RpcTarget.All);//player2.Attack();
                }
            }
        }
    }

    // onHeavyAttack function 
    // Allows players to call their attack function through callback context from input device
    public void onHeavyAttack(CallbackContext context)
    {
        if (photonView.IsMine)
        {
            if (player1 != null && player2 != null)
            {
                if (index == 0)
                {
                    if (context.performed) player1.HeavyAttack();
                }
                else
                {
                    if (context.performed) player2.HeavyAttack();
                }
            }
        }
    }

    // onSpecial function 
    // Allows players to call their attack function through callback context from input device
    public void onSpecial(CallbackContext context)
    {
        if (photonView.IsMine)
        {
            if (player1 != null && player2 != null)
            {
                if (index == 0)
                {
                    if (context.performed) player1.Special();
                }
                else
                {
                    if (context.performed) player2.Special();
                }
            }
        }
    }

    // onJump function 
    // Allows players to call their jump function through callback context from input device
    public void onJump(CallbackContext context)
    {
        if (photonView.IsMine)
        {
            if (player1 != null && player2 != null)
            {
                if (index == 0)
                {
                    if (context.performed) player1.Dash();
                }
                else
                {
                    player2.Jump();
                }
            }

            if (cs != null && (!cs.p1_selected) && SceneManager.GetActiveScene().buildIndex == 3
                    && index == 0)
            {
                cs_photonView.RPC("charSelect",RpcTarget.AllBuffered);
                cs.charSelect();
            }
            else if (cs != null && cs.controls.activeSelf)
            {
                cs_photonView.RPC("charSelect",RpcTarget.AllBuffered);
                cs.charSelect();
            }
        }



    }

    // onJump function 
    // Allows players to call their interact function through callback context from input device
    public void onInteract(CallbackContext context)
    {
        if (photonView.IsMine)
        {
            if (player1 != null && player2 != null)
            {
                if (index == 0)
                {
                    if (player1.interactObj != null)
                    {
                        player1.Interact(player1.interactObj);
                    }

                }
                else
                {

                    if (player2.interactObj != null)
                    {
                        player2.Interact(player2.interactObj);
                    }
                }
            }
        }
    }
}
