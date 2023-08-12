using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceControll : MonoBehaviour
{
    public int type = 0;
    private GridBoard board;

    
    private BoxCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        coll=GetComponent<BoxCollider2D>();
        board=GameObject.Find("Grid").GetComponent<GridBoard>();    
    }

    private void OnMouseDown()
    {
        if (!board.boardIsLoading)
        {
            coll.enabled = false;
            board.isClicked = true;
            if (type == 0)
            {
                Debug.Log("Wrong");

            }
            else
            {

                Debug.Log("Correct");

                board.isCorrect = true;

            }

            
        }
        
    }
    private void OnMouseUp()
    {
    }
}
