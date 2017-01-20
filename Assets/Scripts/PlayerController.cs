using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin = -100;
    public float xMax = 100;
    public float zMin = -100;
    public float zMax = 100;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;



        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x + 1, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z + 1, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}