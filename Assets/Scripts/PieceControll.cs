using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceControll : MonoBehaviour
{
    public int type = 0;
    private GridBoard board;
    private GameBoard gameBoard;
    private Score score;
    
    private BoxCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        coll=GetComponent<BoxCollider2D>();
        //board=GameObject.Find("Grid").GetComponent<GridBoard>(); 
        gameBoard= GameObject.Find("GameObject").GetComponent<GameBoard>();
        //score =board.GetComponent<Score>();
        score=gameBoard.GetComponent<Score>();
    }

    private void OnMouseDown()
    {
        //if (!board.boardIsLoading)
        //{
        //    coll.enabled = false;
        //    board.isClicked = true;
        //    if (type == 0)
        //    {
        //        Debug.Log("Wrong");
        //        score.UpdateScore(-1);
        //    }
        //    else
        //    {

        //        Debug.Log("Correct");

        //        board.isCorrect = true;
        //        score.UpdateScore(1);
        //    }

            
        //}
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
