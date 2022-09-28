using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public PlayerController playerController;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AreaWallCube>())
        {
            if (!other.GetComponent<AreaWallCube>().stacked)
            {
                EventManager.PlayerStackCube(other.transform);
                other.GetComponent<AreaWallCube>().stacked = true;

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<AreaWall>())
        {
            timer += Time.deltaTime;
            if (timer>.2f)
            {
                if (playerController.stack.Count!=0)
                {
                    EventManager.StackWall(playerController.stack.Last(),other.transform);
                }
                timer = 0;

            }
        }
    }
}
