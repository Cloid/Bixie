using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    // Input variables
    private ArrayList players = new ArrayList();
    public float index;
    private int sceneIndex;
    private PlayerInput playerInput;
    private Player player1;
    private Player2 player2;
    private CS_Control cs;
    QinyangControls controls;
    Vector2 move;


    // Initialization
    void Awake() {
        DontDestroyOnLoad(gameObject);
        cs = GameObject.FindObjectOfType<CS_Control>();
        playerInput = GetComponent<PlayerInput>();
        player1 = FindObjectOfType<Player>();
        player2 = FindObjectOfType<Player2>();
        players.Add(player1);
        players.Add(player2);
        index = playerInput.playerIndex;
        print("inx:"+index);
        controls = new QinyangControls();
    }
    private void Update() {
        if(player1 == null && SceneManager.GetActiveScene().buildIndex > 3){
            player1 = FindObjectOfType<Player>();
        }

        if(player2 == null && SceneManager.GetActiveScene().buildIndex > 3){
            player2 = FindObjectOfType<Player2>();
        }
    }

    // onMove function 
    // Allows players to move through callback context from input device
    public void OnMove(CallbackContext context) 
    {
        if(player1 != null && player2 != null)
        {
            if(index == 0)
            {
                player1.SetInputVector(context.ReadValue<Vector2>());
            }else{
                player2.SetInputVector(context.ReadValue<Vector2>());
            }
        }

        if(SceneManager.GetActiveScene().buildIndex == 3 && cs!=null && index == 0){
            cs.moveMe(context.ReadValue<Vector2>());
        }

    }

    // onAttack function 
    // Allows players to call their attack function through callback context from input device
    public void onAttack(CallbackContext context) 
    {
        if(player1 != null && player2 != null)
        {
            if (index == 0)
            {
                if (context.performed)  player1.Attack();
            }
            else
            {
                if (context.performed) player2.Attack();
            }
        }
    }

    // onHeavyAttack function 
    // Allows players to call their attack function through callback context from input device
    public void onHeavyAttack(CallbackContext context)
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

    // onSpecial function 
    // Allows players to call their attack function through callback context from input device
    public void onSpecial(CallbackContext context)
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

    // onJump function 
    // Allows players to call their jump function through callback context from input device
    public void onJump(CallbackContext context) 
    {
        if(player1 != null && player2 != null)
        {
            if(index == 0)
            {
                if(context.performed) player1.Dash();
            }else{
                player2.Jump();
            }
        }

        if( cs!= null && (!cs.p1_selected) && SceneManager.GetActiveScene().buildIndex == 3 
                && index == 0 ){
            cs.charSelect();
        } else if(cs!=null && cs.controls.activeSelf){
            cs.charSelect();
        }
        
        
        
    }

    // onJump function 
    // Allows players to call their interact function through callback context from input device
    public void onInteract(CallbackContext context)
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
