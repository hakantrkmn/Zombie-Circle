using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanic : MonoBehaviour
{
    public float walkSpeed;
    public float shootSpeed;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    public Transform bulletsParent;
    public FloatingJoystick joystick;
    private Transform area;

    private float timer;

    private void Start()
    {
        area = GameObject.FindObjectOfType<AreaInteraction>().transform;
    }

    private void OnEnable()
    {
        EventManager.PlayerKilledZombie += PlayerKilledZombie;
    }

    private void OnDisable()
    {
        EventManager.PlayerKilledZombie -= PlayerKilledZombie;
    }

    public void PlayerKilledZombie()
    {
        shootSpeed -= .005f;
    }

    public void PlayerMovement()
    {
        if (Mathf.Abs(transform.position.x-area.transform.position.x)<area.GetComponent<Collider>().bounds.size.x/2 &&Mathf.Abs(transform.position.z-area.transform.position.z)<area.GetComponent<Collider>().bounds.size.x/2)
        {
                transform.position += new Vector3(joystick.Horizontal, 0, joystick.Vertical) * walkSpeed;


        }
        else
        {
           

            transform.position += (area.position - transform.position).normalized * .1f;
        }
        if (joystick.Horizontal!=0&&joystick.Vertical!=0)
        {
            transform.forward= (new Vector3(joystick.Horizontal, 0, joystick.Vertical));

        }

    }

    public void Shoot()
    {
        timer += Time.deltaTime;
        if (timer>shootSpeed)
        {
            timer = 0;
            var temp = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity,bulletsParent);
            temp.GetComponent<Bullet>().direction = new Vector3(bulletSpawnPoint.forward.x,0,bulletSpawnPoint.forward.z).normalized;
            Destroy(temp,3);
        }
    }
}
