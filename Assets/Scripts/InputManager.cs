using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject PlayerObject;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            HandleRightMouseClick();
    }

    private void HandleRightMouseClick()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20));

        PlayerObject.GetComponent<PlayerController>().PushShipFromExplosion(mousePosition);

    }
}
