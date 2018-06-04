using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zidle_RotationManager : MonoBehaviour {

    [Header("+ rotates left, - right")]
    public float rotationSpeed;

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(0, 0, rotationSpeed * 2 * Time.deltaTime);
    }
}