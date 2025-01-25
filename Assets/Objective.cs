using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public float turnSpeed;
    public float floatingSpeed;
    private int LoopIndex;
    private int floatDirection;
    private float initialY;
    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;
    }
    public void OnCollisionEnter(Collision collision)
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
        if(transform.position.y > initialY + .5)
        {
            floatDirection = 1;
        }
        else if(transform.position.y < initialY - .5)
        {
            floatDirection = 0;
        }
        if(floatDirection == 0)
        {
            transform.Translate(Vector3.up * floatingSpeed * Time.deltaTime);
        }
        else
        {
             transform.Translate(Vector3.down * floatingSpeed * Time.deltaTime);
        }
    }
}
