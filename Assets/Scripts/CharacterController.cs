using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float inputDelay = 0.1f; //if the player instantly presses the input button to move forward there will be a delay
    public float forwardVelocity = 12;
    public float rotateVelocity = 100;

    Quaternion targetRotation; // Quaternion objects that hold rotations on a scale from -1 to 1 on each axis. Also has a 4th axis.
    Rigidbody rBody; //rigidbody refference(need it for unity 5)
    float forwardInput, turnInput;

    public Quaternion TargetRotation//property for target rotation
        {
         get{return targetRotation;}//need to accces from camera controller. to base camera rot of character rot
        }

    private void Start()// Awake() & Start(). Like the Awake function, Start is called exactly once in the lifetime of the script.However,
        //Awake is called when the script object is initialised, regardless of whether or not the script is enabled.
        //Start may not be called on the same frame as Awake if the script is not enabled at initialisation time
    {

        targetRotation = transform.rotation; //sets targetrot equal to initial rotation, when we first spawn

        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();
        else
            Debug.LogError("The character needs a rigidboyd.");

        forwardInput = turnInput = 0;
    }

    void GetInput()//method for getting keyboard inputs
    {
        forwardInput = Input.GetAxis("Vertical"); //get axis return a value from -1 to 1
        turnInput = Input.GetAxis("Horizontal");
    }

    private void Update()//without private in tutorial. Update is called every frame
    {
        GetInput();
        Turn();
    }

    private void FixedUpdate()//has the same time between calls. Used for anything that affects a rigid body(recommended)
    {
        Run();
    }

    void Run()
    {
        if (Mathf.Abs(forwardInput) > inputDelay)//checks for deadzone. Abs(value)because forward input can be negative
        {
            //move
            rBody.velocity = transform.forward * forwardInput * forwardVelocity; 
        }
        else
            //zero  velocity
            rBody.velocity = Vector3.zero; //if computer doesn't recognize our imput, we make sure the character isn't moving at all
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputDelay)//checks for deadzone
        {
            targetRotation *= Quaternion.AngleAxis(rotateVelocity * turnInput * Time.deltaTime, Vector3.up);//AngleAxis(what angle to we want to rotate to, on what axis are we making this rotation)
            //multiplying quaternions is like adding angles
        }
        transform.rotation = targetRotation; //attributing the news values to rotation

    }

}
