using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Area : MonoBehaviour
{
    private Vector3 movementDirection;
    private float timer;
    public float moveSpeed;
    public void RandomMovement()
    {
        timer += Time.deltaTime;
        if (timer>2)
        {
            movementDirection = new Vector3(Random.Range(-1f,1f),0,Random.Range(-1f,1f));
            timer = 0;
        }
        else
        {
            transform.position += movementDirection * Time.deltaTime * moveSpeed;
        }
    }
    void Update()
    {
        RandomMovement();
    }
    
    

  
}
