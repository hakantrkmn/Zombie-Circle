using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{

    public Transform player;

    private bool zombieDied;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (player.transform.position - transform.position).normalized * .02f;
        transform.LookAt(player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>())
        {
            EventManager.PlayerKilledZombie();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.GetComponent<AreaWallCube>())
        {
            if (!zombieDied)
            {
                zombieDied = true;
                Destroy(gameObject);
                other.transform.GetComponentInParent<AreaInteraction>().areaWallController.DestroyWall(other.transform);
            }
            
        }
        else if (other.GetComponent<AreaInteraction>())
        {
            Destroy(gameObject);
            other.transform.GetComponentInParent<AreaInteraction>().ReScaleWall();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
       
    }
}
