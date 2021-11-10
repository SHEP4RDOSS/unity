using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public float rotationSpeed = 2f;
    public Transform target, player;
    float mouseX;
    float mouseY;
    float h;
    float v;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        CamControl();
    }

    void CamControl()
    {
        transform.LookAt(target);
       
    }

}
