using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapHandler : MonoBehaviour
{
    public static TapHandler Instance;

    public CharacterMovement characterMovement;
    public CameraFollowing cameraFollowing;
    public LadderGenerator ladderGenerator;

    private void Awake()
    {
        Instance = this;
        cameraFollowing.enabled = false;
        ladderGenerator.enabled = false;
        Time.timeScale = 0;
    }

    public void CanMoveOn()
    {
        characterMovement.canMove = true;
        characterMovement.gameObject.GetComponent<Animator>().enabled = true;
        cameraFollowing.enabled = true;
        ladderGenerator.enabled = true;
        Time.timeScale = 1;
    }

    public void CanMoveOf()
    {
        characterMovement.canMove = false;
        characterMovement.gameObject.GetComponent<Animator>().enabled = false;
        cameraFollowing.enabled = false;
        ladderGenerator.enabled = false;
        Time.timeScale = 0;
    }
}