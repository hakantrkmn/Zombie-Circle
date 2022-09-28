using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class AreaInteraction : MonoBehaviour
{
    public Area area;
    public AreaWallController areaWallController;

 



    public void ReScaleWall()
    {
        transform.DOScale(new Vector3(transform.localScale.x - .05f, 2, transform.localScale.z - .05f),.5f);

    }




}
