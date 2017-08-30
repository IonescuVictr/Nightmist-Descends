using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target; //refference to target
    public float lookSmooth = 0.09f; //how fast we want to look at the target
    public Vector3 offsetFromTarget = new Vector3(0, 6, -8); //how far we want te camera to be from the player
    public float xTilt = 10; //xTilt how far we want our camera to be rotated on the X axis. enables us to look downward on our target

    Vector3 destination = Vector3.zero;//where the camera moves to
    CharacterController characterController;//need a CharacterController refference so that we can acces the target rotation of the caracter
    float rotateVel = 0;

    private void Start()
    {
        SetCameraTarget(target);
    }

    void SetCameraTarget(Transform t)//to have te ability to give the camera a new target to look at
    {
        target = t;

        if (target != null)
        {
            if (target.GetComponent<CharacterController>())
            {
                characterController = target.GetComponent<CharacterController>();
            }
            else
                Debug.LogError("Your camera needs a target.");
        }
        else
            Debug.LogError("Your camera needs a target.");
    }

    private void LateUpdate()//Update()-once per frame, FixedUpdate()-multyple times per frame, LateUpdate()-only after the update() calls
        //It's ensuring that we are doing movements and rotations based off on the most previous positions of the character
    {
        //moving
        MoveToTarget();
        //rotating
        LookAtTarget();
    }

    void MoveToTarget()
    {
        destination = characterController.TargetRotation * offsetFromTarget;//offsetfromtarget is how far away we want the camera from the target.
        destination += target.position; //making sure that destination(rotated point) is relative to our target
        transform.position = destination;
    }

    void LookAtTarget()
    {
        float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
        //transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerYAngle, 0);
        transform.rotation = Quaternion.Euler(xTilt, eulerYAngle, 0);
    }

}
