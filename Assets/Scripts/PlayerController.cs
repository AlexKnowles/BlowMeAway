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

    void FixedUpdate()
    {
        //Rigidbody rb = GetComponent<Rigidbody>();
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //rb.velocity = movement * speed;



        //rb.position = new Vector3
        //(
        //    Mathf.Clamp(rb.position.x + 1, boundary.xMin, boundary.xMax),
        //    0.0f,
        //    Mathf.Clamp(rb.position.z + 1, boundary.zMin, boundary.zMax)
        //);

        //rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
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