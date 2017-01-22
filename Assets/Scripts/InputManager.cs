using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject PlayerObject;
    public GameManager GameMangerReference;

    // Use this for initialization
    void Start ()
    {
        GameMangerReference = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            HandleRightMouseClick();

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void HandleRightMouseClick()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20);

        Instantiate(Resources.Load("Prefabs/ExpandingCircle"), Camera.main.ScreenToWorldPoint(mousePosition), Quaternion.identity, Camera.main.transform);

        if (GameMangerReference.Paused)
            return;

        //GameObject particleControllerReference = GameObject.FindGameObjectWithTag("ParticleController");

        //if(particleControllerReference != null)
        //    particleControllerReference.GetComponent<ParticleController>().ParticleEffectOnClick();

        PlayerObject.GetComponent<PlayerController>().PushShipFromExplosion(mousePosition);
    }
}
