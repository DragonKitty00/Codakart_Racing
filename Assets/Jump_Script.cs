using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//require a rigbody to run the code
[RequireComponent(typeof(Rigidbody))]
public class Jump_Script : MonoBehaviour
{
    //variables for grounded, force values, and rigidbody
    public bool isGrounded = false;
    public float jumpForce = 1500.0f;
    public Vector3 jumpValue = new Vector3(0.0f, 2.0f, 0.0f);
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        //set the rigidbody to the component rigidbody
        rigidBody = GetComponent< Rigidbody >();
    }

    //function that is called when colliders hit another collider
    void OnCollisionStay()
    {
        isGrounded = true;
    }

    // Update is called once per frame. Best for pysics
    void FixedUpdate()
    {
        //if space bar pressed and grounded is true
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //add a force to the kart that ignors mass  
            rigidBody.AddForce(jumpValue * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}
