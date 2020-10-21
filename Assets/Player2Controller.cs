using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    // Declare a global variable for speed, can be edited within Unity and accessed everywhere
    public float speed2;
    // Deckare a rigidbody for reference later on
    Rigidbody2D rigidbody2;

    // Start is called before the first frame update
    void Start()
    {

        rigidbody2 = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("p2Horizontal");
        float vertical = Input.GetAxis("p2Vertical");

        rigidbody2.velocity = new Vector3(horizontal * speed2, vertical * speed2, 0);
        
    }
}
