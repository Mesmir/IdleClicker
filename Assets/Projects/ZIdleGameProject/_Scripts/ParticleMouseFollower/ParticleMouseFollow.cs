using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class ParticleMouseFollow : MonoBehaviour {
     
    [Range(0.0f, 20.0f)]
    public float speed;
    public float distFromMouse;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update () {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = distFromMouse;

        Vector3 mouseScreenPosToWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 psPosition = Vector3.Lerp(transform.position, mouseScreenPosToWorldPos, /*speed * Time.deltaTime);*/  1.0f - Mathf.Exp(-speed * Time.deltaTime));

        transform.position = psPosition;
	}
}
