using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private List<GameObject> Bombs = new List<GameObject>();
    private Vector3 PlayerSize;

    private void Start()
    {
        PlayerSize = GetComponent<Collider>().bounds.size;
    }

    void LateUpdate()
    {

        //// Movement ////

        //set screen Bounds
        Vector3 screenBounds = new Vector3(Screen.width, 0, Screen.height);
        Vector2 screen;

        //set offset
        screen.x = (screenBounds.x) ; //4% of screenBounds.x
        screen.y = Screen.height;
        
        //set players position in screen coordinates
        Vector3 playerPosScreen = Camera.main.WorldToScreenPoint(transform.position);
               

        if (playerPosScreen.x > Screen.width)
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(-PlayerSize.x, playerPosScreen.y, playerPosScreen.z));
        else if (playerPosScreen.x < -PlayerSize.x)
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, playerPosScreen.y, playerPosScreen.z));
    }

    public void FireBombAtMousePosition(Vector3 _MousePosition)
    {
        GameObject bombInstance = (Resources.Load("Prefabs/Bomb", typeof(GameObject)) as GameObject);
        BombController bombInstanceController = bombInstance.GetComponent<BombController>();
        
        bombInstanceController.PlayerControllerReference = this;
        bombInstanceController.SetupBombLocations(Camera.main.ScreenToWorldPoint(_MousePosition));

        Bombs.Add(bombInstance);

        Instantiate(bombInstance, transform.position, transform.rotation);
    }

    public void RemoveBomb (GameObject _Bomb)
    {
        Bombs.Remove(_Bomb);
        Destroy(_Bomb);
    }

    public void PushShipFromExplosion(Vector3 _ExplosionPosition)
    {
        var heading = _ExplosionPosition - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        GetComponent<Rigidbody>().AddForce(direction * speed * -1);
    }
}