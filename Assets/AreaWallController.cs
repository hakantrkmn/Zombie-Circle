using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class AreaWallController : MonoBehaviour
{
    public Transform leftWallParent;
    public Transform rightWallParent;
    public Transform upWallParent;
    public Transform bottomWallParent;
    public List<Transform> leftWallCubePositions;
    public List<Transform> leftWallCubes;
    public List<Transform> rightWallCubePositions;
    public List<Transform> rightWallCubes;
    public List<Transform> upWallCubePositions;
    public List<Transform> upWallCubes;
    public List<Transform> bottomWallCubePositions;
    public List<Transform> bottomWallCubes;

    private int leftWallIndex;
    private int rightWallIndex;
    private int upWallIndex;
    private int bottomWallIndex;
    private float timer;
    public GameObject cubePrefab;
    public Transform spawnedCubeParent;
    private void OnValidate()
    {
        leftWallCubePositions.Clear();
        rightWallCubePositions.Clear();
        upWallCubePositions.Clear();
        bottomWallCubePositions.Clear();

        for (int i = 0; i < upWallParent.childCount; i++)
        {
            upWallCubePositions.Add(upWallParent.GetChild(i));
        }
        for (int i = 0; i < bottomWallParent.childCount; i++)
        {
            bottomWallCubePositions.Add(bottomWallParent.GetChild(i));
        }
        for (int i = 0; i < leftWallParent.childCount; i++)
        {
            leftWallCubePositions.Add(leftWallParent.GetChild(i));
        }
        for (int i = 0; i < rightWallParent.childCount; i++)
        {
            rightWallCubePositions.Add(rightWallParent.GetChild(i));
        }
    }


    private void OnEnable()
    {
        EventManager.StackWall += StackWall;
    }

    private void OnDisable()
    {
        EventManager.StackWall -= StackWall;

    }

    private void StackWall(Transform cube, Transform wall)
    {
        if (leftWallCubePositions[0].parent==wall)
        {
            cube.parent = leftWallCubePositions[leftWallIndex];
            leftWallCubes.Add(cube);
            cube.DOLocalMove(Vector3.zero, .5f);
            cube.DOLocalRotate(Vector3.zero, .5f);
            leftWallIndex++;
        }
        else if (rightWallCubePositions[0].parent==wall)
        {
            cube.parent = rightWallCubePositions[rightWallIndex];
            rightWallCubes.Add(cube);
            cube.DOLocalMove(Vector3.zero, .5f);
            cube.DOLocalRotate(Vector3.zero, .5f);

            rightWallIndex++;
        }
        else if (upWallCubePositions[0].parent==wall)
        {
            cube.parent = upWallCubePositions[upWallIndex];
            upWallCubes.Add(cube);
            cube.DOLocalMove(Vector3.zero, .5f);
            cube.DOLocalRotate(Vector3.zero, .5f);

            upWallIndex++;
        }
        else if (bottomWallCubePositions[0].parent==wall)
        {
            cube.parent = bottomWallCubePositions[bottomWallIndex];
            bottomWallCubes.Add(cube);
            cube.DOLocalMove(Vector3.zero, .5f);
            cube.DOLocalRotate(Vector3.zero, .5f);

            bottomWallIndex++;
        }
    }

    public void DestroyWall(Transform cube)
    {
        if (leftWallCubePositions.Contains(cube.parent))
        {
            var topCube = FindTopCube(leftWallCubes);
            leftWallIndex--;
            leftWallCubes.Remove(topCube);
            Destroy(topCube.gameObject);
        }
        else if (rightWallCubePositions.Contains(cube.parent))
        {
            var topCube = FindTopCube(rightWallCubes);
            rightWallIndex--;

            rightWallCubes.Remove(topCube);

            Destroy(topCube.gameObject);
        }
        else if (upWallCubePositions.Contains(cube.parent))
        {
            var topCube = FindTopCube(upWallCubes);
            upWallCubes.Remove(topCube);
            upWallIndex--;

            Destroy(topCube.gameObject);
        }
        else if (bottomWallCubePositions.Contains(cube.parent))
        {
            var topCube = FindTopCube(bottomWallCubes);
            bottomWallCubes.Remove(topCube);
            bottomWallIndex--;

            Destroy(topCube.gameObject);
        }
    }
    
    public Transform FindTopCube(List<Transform> cubes)
    {
        var topCube = cubes.Last();
        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i].position.y>topCube.position.y)
            {
                topCube = cubes[i];
            }
        }

        return topCube;
    }


    public void SpawnCube()
    {
        timer += Time.deltaTime;
        if (timer>.5f)
        {
            timer = 0;
            var cube = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity);
            cube.transform.parent = spawnedCubeParent;
            cube.transform.localPosition = new Vector3(Random.Range(-4, 4), 0, Random.Range(-4, 4));
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCube();
    }
}
