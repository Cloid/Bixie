﻿using System.Collections;
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
    private PhotonView torch_photonView;
    private CS_Control cs;
    private PhotonView photonView;
    public bool hostClient = false;
    public Photon_Lit itsLit;
    QinyangControls controls;
    Vector2 move;

    // Initialization
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        cs_holder = GameObject.Find("cc_control");



        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            cs = cs_holder.GetComponent<CS_Control>();
            cs_photonView = cs_holder.GetPhotonView();
        }


        //int i = 0;
        // var myUser = PlayerInput.all[0].user;

        if (PhotonNetwork.IsMasterClient && !PhotonNetwork.OfflineMode)
        {
            hostClient = true;
        }

        if (PhotonNetwork.OfflineMode)
        {
            //var device = myUser.pairedDevices;
            // GameObject go = PhotonNetwork.Instantiate("Player Input Manager", new Vector3(0,0,0), Quaternion.identity);

            //go.GetComponent<PlayerInput>().AddDevice

            //var plop = GameObject.FindObjectOfType<PlayerInputManager>();
            //photonView.TransferOwnership(plop.GetComponent<PhotonView>().Owner);
            //photonView.TransferOwnership(PhotonNetwork.Player.id);
            //photonView.TransferOwnership(Photon.Realtime.Player);
            //pInput = GetComponent<PlayerInput>();
            //pInput.UnpairDeice.
            //myUser.UnpairDevices();
            //myUser.
            playerInput = GetComponent<PlayerInput>();
            index = playerInput.playerIndex;
            DontDestroyOnLoad(gameObject);
            //photonView.RPC("Test", RpcTarget.AllBuffered);

            //InputSystem.UnpairDevice
        }
        else
        {
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
            itsLit = FindObjectOfType<Photon_Lit>();
            players.Add(player1);
            players.Add(player2);
            //index = playerInput.playerIndex;
            //print("inx:"+index);

            controls = new QinyangControls();
        }
    }
    private void Update()
    {
        if (PhotonNetwork.OfflineMode || photonView.IsMine)
        {
            if (player1 == null)
            {
                player1 = FindObjectOfType<Player>();
            }

            if (player2 == null)
            {
                player2 = FindObjectOfType<Player2>();
            }
            
            if(itsLit == null){
                itsLit = FindObjectOfType<Photon_Lit>();
            }

        }

        
    }

    // onMove function 
    // Allows players to move through callback context from input device
    public void OnMove(CallbackContext context)
    {
        //Debug.Log("Vector: " + context.ReadValue<Vector2>());
        if (PhotonNetwork.OfflineMode || photonView.IsMine)
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
                cs_photonView.RPC("moveMe", RpcTarget.AllBuffered, context.ReadValue<Vector2>());
                //cs.moveMe(context.ReadValue<Vector2>());
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3 && cs != null && index == 1)
            {
                cs_photonView.RPC("moveMe2", RpcTarget.AllBuffered, context.ReadValue<Vector2>());
            }
        }


    }

    // onAttack function 
    // Allows players to call their attack function through callback context from input device
    public void onAttack(CallbackContext context)
    {
        if (PhotonNetwork.OfflineMode || photonView.IsMine)
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
        if (PhotonNetwork.OfflineMode || photonView.IsMine)
        {
            if (player1 != null && player2 != null)
            {
                if (index == 0)
                {
                    if (context.performed) player1.photonView.RPC("HeavyAttack", RpcTarget.All);//player1.HeavyAttack();
                }
                else
                {
                    if (context.performed) player2.photonView.RPC("HeavyAttack", RpcTarget.All);//player2.HeavyAttack();
                }
            }
        }
    }

    // onSpecial function 
    // Allows players to call their attack function through callback context from input device
    public void onSpecial(CallbackContext context)
    {
        if (PhotonNetwork.OfflineMode || photonView.IsMine)
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
        if (PhotonNetwork.OfflineMode || photonView.IsMine)
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

            if (cs != null && SceneManager.GetActiveScene().buildIndex == 3)
            {
                cs_photonView.RPC("charSelect", RpcTarget.AllBuffered);
            }
        }



    }

    // onJump function 
    // Allows players to call their interact function through callback context from input device
    public void onInteract(CallbackContext context)
    {
        if (PhotonNetwork.OfflineMode || photonView.IsMine)
        {
            if (player1 != null && player2 != null)
            {
                if (index == 0)
                {

                }
                else
                {

                    if (context.performed)
                    {
                        Debug.Log(itsLit);
                        itsLit.photonView.RPC("litLantern", RpcTarget.AllBuffered);
                        //photonView.RPC("litLantern", RpcTarget.AllBuffered);

                    }
                    if (context.canceled)
                    {
                        itsLit.photonView.RPC("unlitLantern", RpcTarget.AllBuffered);
                        //photonView.RPC("unlitLantern", RpcTarget.AllBuffered);

                    }
                }
            }
        }
    }
 

}
