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
        GameObject particleSystem = (Resources.Load("Prefabs/Particle System", typeof(GameObject)) as GameObject);

        GameObject particleSystemMedium = (Resources.Load("Prefabs/Particle System 1", typeof(GameObject)) as GameObject);
        GameObject particleSystemLarge = (Resources.Load("Prefabs/Particle System 2", typeof(GameObject)) as GameObject);


        
        PlayerObject.GetComponent<PlayerController>().PushShipFromExplosion(mousePosition);

        mousePosition.x = mousePosition.x - 3;
        
        Instantiate(particleSystem, mousePosition, Quaternion.identity);
        Instantiate(particleSystemMedium, mousePosition, Quaternion.identity);
        Instantiate(particleSystemLarge, mousePosition, Quaternion.identity);

    }
}
