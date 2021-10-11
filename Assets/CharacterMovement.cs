using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{
    public float transformXValue = 1f;
    public float transformYValue = 0.06f;

    public bool canMove = false;

    void FixedUpdate()
    {
        if (canMove)
            transform.position += new Vector3(transformXValue, transformYValue, 0);
    }

}