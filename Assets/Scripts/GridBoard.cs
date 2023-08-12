using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridBoard : MonoBehaviour
{
    public int xDim;
    public int yDim;
    
    public GameObject button;
    
    public Sprite[] sprites;
    public Sprite emptySprite;
    public GameObject background;
    private GameObject currentButton = null;
    private GameObject otherButtons = null;
    public GameObject reffenceColor;
    private List<GameObject> buttonList;
    public bool isClicked;
    public bool isCorrect;
    public bool boardIsLoading;
    private void Awake()
    {
        isClicked = false;
        isCorrect = false;
        boardIsLoading = false;
        buttonList=new List<GameObject>();

        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                Instantiate(background, GetWorldPosition(x, y), Quaternion.identity);
            }
        }

        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                GameObject currentButton = Instantiate(button, GetWorldPosition(x, y), Quaternion.identity);

                buttonList.Add(currentButton);


            }
        }
        //InvokeRepeating(nameof(Call), 1, 7);

        InvokeFilling();
    }
    
    public void InvokeFilling()
    {

        Invoke(nameof(StartFilling), 1);

    }
    void StartFilling()
    {
        StartCoroutine(Fill());
    }
 
    IEnumerator Fill()
    {
    
        int rand = Random.Range(0, buttonList.Count);
        SetReffenceObject(rand);
        
        
        yield return new WaitUntil(()=>isClicked);
        PiecesSpriteSetEmpty();
        
        

    }

    void SetReffenceObject(int rand)//ana objenin cesidini belirle
    {
        reffenceColor.GetComponent<SpriteRenderer>().sprite = sprites[rand];
        PiecesSpriteSet(rand);
    }
    void PiecesSpriteSet(int rand)//oyun objelere random sprite ata bir 
    {
        currentButton = buttonList[rand];

        for (int i = 0; i < buttonList.Count; i++)
        {
            if (i == rand)
            {
                currentButton.GetComponent<SpriteRenderer>().sprite = sprites[rand];
                currentButton.GetComponent<PieceControll>().type = 1;
                continue;
            }
            else
            {
                int randSprite = Random.Range(0, sprites.Length);
                if (randSprite == rand) { continue; } else
                {
                    
                    otherButtons = buttonList[i];
                    otherButtons.GetComponent<SpriteRenderer>().sprite = sprites[randSprite];
                }
                

            }

        }
        AfterClick();

    }
    void PiecesSpriteSetEmpty()
    {
        boardIsLoading = true;
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].GetComponent<BoxCollider2D>().enabled = false;
            buttonList[i].GetComponent<SpriteRenderer>().sprite = emptySprite;

        }

        ReFill();
    }
    void ReFill()
    {
        currentButton.GetComponent<PieceControll>().type = 0;
        InvokeFilling();

    }
    void AfterClick()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {

            buttonList[i].GetComponent<BoxCollider2D>().enabled = true;

        }
        boardIsLoading = false;
        isClicked = false;
    }
    

    Vector3 GetWorldPosition(int X, int Y)
    {
        return new Vector3(xDim / 2 - X, Y - yDim / 2.0f, 0);
    }

}
