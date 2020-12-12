using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        float moveH = Input.GetAxis("Horizontal");
        transform.position += new Vector3(moveH,0f,0f) * 2.0f * Time.deltaTime;
    }
}
