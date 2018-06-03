using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrackerRotateTester : MonoBehaviour {

    public Vector3 rotater;

	void Update () {

        transform.Rotate(rotater * Time.deltaTime);
	}
}
