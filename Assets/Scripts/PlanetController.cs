using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    List<GameObject> Planets;

    // Use this for initialization
    void Start()
    {
        Planets = new List<GameObject>();
        CreatePlanet(Random.Range(1, 3));
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    private void CreatePlanet(int _Type)
    {
        string assetToLoad = string.Format("Prefabs/Planets/Planet_{0}", _Type.ToString().PadLeft(3, '0'));
        GameObject newPlanet = (Resources.Load(assetToLoad, typeof(GameObject)) as GameObject);

        float xSeed = Random.Range(0, Screen.width);
        Vector3 newItemPosition = Camera.main.ScreenToWorldPoint(new Vector3(xSeed, Screen.height, 20));

        newItemPosition.y -= newPlanet.GetComponent<Collider>().bounds.size.y;

        Instantiate(newPlanet, newItemPosition, Quaternion.identity, gameObject.transform);

        Planets.Add(newPlanet);
    }
}
