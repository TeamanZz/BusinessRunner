using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGenerator : MonoBehaviour
{
    public Vector3 buildOffset;
    public Transform ladderContainer;
    public GameObject ladderPiecePrefab;
    public int questionChance;

    private Transform lastPieceTransform;
    public int stepsUntilNewQuestion;
    public bool needNewQuestion = true;

    private void Awake()
    {
        lastPieceTransform = ladderContainer.GetChild(ladderContainer.childCount - 1);
    }

    private void Start()
    {
        int middleChildIndex = ladderContainer.childCount / 2;

        ladderContainer.GetChild(middleChildIndex).GetComponent<LadderPiece>().MakePieceAsQuestion();
        ladderContainer.GetChild(middleChildIndex + Random.Range(4, 12)).GetComponent<LadderPiece>().MakePieceAsQuestion();
        SetNewQuestion();
    }

    public void SetNewQuestion()
    {
        stepsUntilNewQuestion = Random.Range(4, 12);
    }

    private void CheckOnQuestionPiece(Transform ladderPiece)
    {
        ladderPiece.GetComponent<LadderPiece>().MakePieceAsQuestion();
    }

    public void BuildNewLadderPiece()
    {
        if (this.enabled == false)
            return;
        var newLadderPiece = ladderContainer.GetChild(0);
        newLadderPiece.GetComponent<LadderPiece>().wasStepped = false;

        if (needNewQuestion)
        {
            CheckOnQuestionPiece(newLadderPiece);
            needNewQuestion = false;
        }
        newLadderPiece.position = lastPieceTransform.position + buildOffset;
        lastPieceTransform = newLadderPiece;

        stepsUntilNewQuestion--;
        if (stepsUntilNewQuestion == 0)
        {
            SetNewQuestion();
            needNewQuestion = true;
        }

        lastPieceTransform.SetAsLastSibling();
    }
}