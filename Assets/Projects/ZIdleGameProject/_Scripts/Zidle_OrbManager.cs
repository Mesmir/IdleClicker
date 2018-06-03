using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zidle_OrbManager : MonoBehaviour {

    public Transform parentOfOrbs;
    public Transform adjuster;
    public float rotationOffSet;
    public int spawndOrbs;
    public int distFromParent;
    public List<Transform> orbHolder = new List<Transform>();

    [Header("+ rotates left, - right")]
    public int rotationSpeed;

    void Start()
    {
        parentOfOrbs = transform;
        adjuster = transform.GetChild(0);
    }

    void Update()
    {
        Rotator(); //kan coroutine
    }

    public void SpawnOrbs()
    {   
        Transform tempOrb = Instantiate(orbHolder[3], adjuster.position, Quaternion.Euler(transform.forward), parentOfOrbs);
        tempOrb.transform.position = adjuster.TransformDirection(new Vector3(0, distFromParent, 0));
    }

    private void Adjusting() 
    {    
        rotationOffSet = 360 / transform.childCount - 1;
        for (int i = 0; i < transform.childCount -1; i++)
        {
            adjuster.rotation = adjuster.rotation * Quaternion.Euler(0, 0, rotationOffSet);
            Debug.Log(adjuster.rotation);
        }
    }

    private void Rotator()
    {
        transform.Rotate(0, 0, rotationSpeed * 2 * Time.deltaTime);
    }
}