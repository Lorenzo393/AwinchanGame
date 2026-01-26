using System.Collections;
using UnityEngine;

public class SecretManager : MonoBehaviour
{
    public static SecretManager Instance {get; private set;}
    [SerializeField] private Transform pivot1;
    [SerializeField] private Transform pivot2;

    [SerializeField] private GameObject piece1;
    [SerializeField] private GameObject piece2;
    [SerializeField] private GameObject piece3;
    [SerializeField] private GameObject piece4;
    [SerializeField] private GameObject lastPiece;

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
        Debug.Log("Open");
        yield return null;
    }

    public bool GetCanOpen(){
        return canOpen;
    }

}
