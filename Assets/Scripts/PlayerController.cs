using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMechanic mechanic;
    public Transform stackParent;
    public List<Transform> stack;

    private void OnEnable()
    {
        EventManager.PlayerStackCube += PlayerStackCube;
        EventManager.StackWall += StackWall;
    }

  

    private void OnDisable()
    {
        EventManager.StackWall -= StackWall;

        EventManager.PlayerStackCube -= PlayerStackCube;
    }
    private void StackWall(Transform cube, Transform wall)
    {
        stack.Remove(cube);
    }
    private void PlayerStackCube(Transform cube)
    {
        cube.parent = stackParent;
        cube.DOLocalMove(new Vector3(0, stack.Count, 0), .5f);
        cube.DOLocalRotate(Vector3.zero, .5f);
        stack.Add(cube);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       mechanic.PlayerMovement();
       mechanic.Shoot();

    }
    
    
}
