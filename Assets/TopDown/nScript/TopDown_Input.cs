using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class TopDown_Input : MonoBehaviour
{
    private PlayerInput playerInput;
    private TopDown_Mover mover;

    private void Awake() 
    {
        playerInput = GetComponent<PlayerInput>();
        var movers = FindObjectsOfType<TopDown_Mover>();
        var index = playerInput.playerIndex;
        
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
    }
    // Start is called before the first frame update
    public void OnMove(CallbackContext context)
    {
        if(mover != null){
            mover.SetInputVector(context.ReadValue<Vector2>());
        }
    }
}
