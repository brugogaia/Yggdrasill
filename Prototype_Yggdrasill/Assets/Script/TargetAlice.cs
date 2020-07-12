using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAlice : MonoBehaviour
{

    private float waitTime = 2f;
    private float timer = 2f;
    private Vector3 position;
    private float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer > waitTime)
        {
            position = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
            Debug.Log(position);
            timer = 0.0f;
        }
        transform.Translate(position * Time.deltaTime * speed);
    }
}
