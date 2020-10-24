using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    // Declare whether Player2 is being controlled
    public static bool isActive;

    // Declare a global variable for speed, can be edited within Unity and accessed everywhere
    public float speed2;

    // Deckare a rigidbody for reference later on
    Rigidbody2D rigidbody2;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        rigidbody2 = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) {
            speed2 = GameController.MoveSpeed;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            rigidbody2.velocity = new Vector3(horizontal * speed2, vertical * speed2, 0);
        }
        
    }
    public static void setActive()
    {
        if (isActive){
            isActive = false;
        }
        else{
            isActive = true;
        }
    }
}
