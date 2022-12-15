using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float Speed = 1.0f;
    public Vector2 sensitivity;
    public float JumpForce = 1.0f;
    public new Transform camera;

    private Rigidbody Physics;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Physics = GetComponent<Rigidbody>();
    }

    private void UpdateMouseLook()
    {
        float Horizontal = Input.GetAxis("Mouse X");
        float Vertical = Input.GetAxis("Mouse Y");

        if (Horizontal != 0)
        {
            transform.Rotate(0, Horizontal * sensitivity.x, 0);
        }

        if (Vertical != 0)
        {
            Vector3 rotation = camera.localEulerAngles;
            rotation.x = (rotation.x - Vertical * sensitivity.y + 360) % 360;
            if (rotation.x > 80 && rotation.x < 180)
            {
                rotation.x = 80;
            }
            else if (rotation.x < 280 && rotation.x > 180)
            {
                rotation.x = 280;
            }

            camera.localEulerAngles = rotation;
        }
    }

    private void UpdateMovement()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 velocity = Vector3.zero;

        if (Horizontal != 0 || Vertical != 0 )
        {
            Vector3 direction = (transform.forward * Vertical + transform.right * Horizontal).normalized;

            velocity = direction * Speed;
        }
            velocity.y = Physics.velocity.y;
            Physics.velocity = velocity;
       

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            Physics.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateMouseLook();
    }

}
