using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject PlayerReference;
    private Camera CameraReference;

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        PlayerReference = GameObject.FindGameObjectWithTag("Player");
        CameraReference = GetComponent<Camera>();
    }
    
    void Update()
    {
        if (PlayerReference)
        {
            Vector3 point = CameraReference.WorldToViewportPoint(PlayerReference.transform.position);
            Vector3 delta = PlayerReference.transform.position - CameraReference.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;

            destination.x = 0;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }

}
