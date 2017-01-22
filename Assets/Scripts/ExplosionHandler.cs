using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{    
    public int MaxNumberOfRings = 3;

    void Start()
    {
        for(float ii = 0; ii < MaxNumberOfRings; ii++)
        {
            AddInnerCircle(ii / MaxNumberOfRings);
        }
    }

    private void AddInnerCircle(float _TimeDelay)
    {
        GameObject newCircle = (Resources.Load("Prefabs/ExpandingCircle", typeof(GameObject)) as GameObject);

        newCircle = Instantiate<GameObject>(newCircle, transform.position, Quaternion.identity, transform);

        if (_TimeDelay > 0)
            newCircle.GetComponent<CircleExpander>().GrowthSpeed /= _TimeDelay;
    }    
}
