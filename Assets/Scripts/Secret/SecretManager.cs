using System;
using System.Collections;
using UnityEngine;

public class SecretManager : MonoBehaviour
{
    public static SecretManager Instance {get; private set;}
    [SerializeField] private Transform pivot1;
    [SerializeField] private Transform pivot2;
    private float openRotation = 90f;

    [SerializeField] private GameObject piece1;
    [SerializeField] private GameObject piece2;
    [SerializeField] private GameObject piece3;
    [SerializeField] private GameObject piece4;
    [SerializeField] private GameObject lastPiece;

    private float openTime = 10f;
    private bool canOpen = false;
    private int maxPieces = 4;
    private int piecesClick = 0;

    private void Piece1_OnPieceClicked(object sender, System.EventArgs e){
        piecesClick++;
        if(maxPieces == piecesClick) canOpen = true;
    }
    private void Piece2_OnPieceClicked(object sender, System.EventArgs e){
        piecesClick++;
        if(maxPieces == piecesClick) canOpen = true;
    }
    private void Piece3_OnPieceClicked(object sender, System.EventArgs e){
        piecesClick++;
        if(maxPieces == piecesClick) canOpen = true;
    }
    private void Piece4_OnPieceClicked(object sender, System.EventArgs e){
        piecesClick++;
        if(maxPieces == piecesClick) canOpen = true;
    }
    private void LastPiece_OnLastPieceClicked(object sender, System.EventArgs e){
        StartCoroutine(PivotAnimator());
    }
    private void Awake(){
        Instance = this;
    }
    private void Start(){
        SecretPuzzlePiece pieceScript = piece1.GetComponent<SecretPuzzlePiece>();
        pieceScript.OnPieceClicked += Piece1_OnPieceClicked;

        pieceScript = piece2.GetComponent<SecretPuzzlePiece>();
        pieceScript.OnPieceClicked += Piece2_OnPieceClicked;

        pieceScript = piece3.GetComponent<SecretPuzzlePiece>();
        pieceScript.OnPieceClicked += Piece3_OnPieceClicked;

        pieceScript = piece4.GetComponent<SecretPuzzlePiece>();
        pieceScript.OnPieceClicked += Piece4_OnPieceClicked;

        LastPiecePuzzle lastPieceScript = lastPiece.GetComponent<LastPiecePuzzle>();
        lastPieceScript.OnLastPieceClicked += LastPiece_OnLastPieceClicked;
    }

    IEnumerator PivotAnimator(){
        Quaternion pivot1OpenRotation = Quaternion.Euler(pivot1.localRotation.x, openRotation, pivot1.localRotation.z);
        Quaternion pivot2OpenRotation = Quaternion.Euler(pivot1.localRotation.x, -openRotation, pivot1.localRotation.z);
        
        float timer = 0f;
        for(float t = 0f ; t < 1.0f ; t = timer / openTime){
            pivot1.localRotation = Quaternion.Slerp(pivot1.localRotation, pivot1OpenRotation, t);
            pivot2.localRotation = Quaternion.Slerp(pivot2.localRotation, pivot2OpenRotation, t);
            timer += Time.deltaTime;
            yield return null;
        }
        pivot1.localRotation = pivot1OpenRotation;
        pivot2.localRotation = pivot2OpenRotation;

        yield return null;
    }

    public bool GetCanOpen(){
        return canOpen;
    }

}
