using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceControll : MonoBehaviour
{
    public int type = 0;
    
    private GameBoard gameBoard;
    private Score score;
    
    private BoxCollider2D coll;
    
    void Start()
    {
        coll=GetComponent<BoxCollider2D>();
        
        gameBoard= GameObject.Find("GameObject").GetComponent<GameBoard>();
        
        score=gameBoard.GetComponent<Score>();
    }

    private void OnMouseDown()
    {
       
        coll.enabled = false;
        if (type == 1)
        {
            score.UpdateScore(1);
            gameBoard.Clicked();
        }
        else
        {
            score.UpdateScore(-1);
            gameBoard.Clicked();

        }

    }
    private void OnMouseUp()
    {
    }
}
