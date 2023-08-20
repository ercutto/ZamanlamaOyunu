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
    private bool isFilled;
    // first Starting Game by StartGame and Generating board
    // 

    List<GameObject> wrongButtons = new List<GameObject>();
    List<Sprite> LeftSprites = new List<Sprite>();
    List<Sprite> Sprites = new List<Sprite>();

    private void Awake()
    {
        StartGame(xDim,yDim);
    }
    public void StartGame(int xDim,int yDim)
    {
        isFilled = false;
        isClicked = false;
        isCorrect = false;
        boardIsLoading = true;

        buttonList = new List<GameObject>();
        GenerateBoard(xDim, yDim);

        InvokeFilling(3f);
    }
    public void GenerateBoard(int X,int Y)
    {
       
        xDim= X; yDim = Y; 
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
        
    }
    public void InvokeFilling(float time)
    {

        Invoke(nameof(StartFilling), time);

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
                
            }
            else
            {
                otherButtons = buttonList[i];
                wrongButtons.Add(otherButtons);

            }

            
        }

        for (int j = 0; j < sprites.Length; j++)
        {
            if (j != rand)
            {
                LeftSprites.Add(sprites[j]);
            }
        }

        StartCoroutine(AddSprites(0.01f));

       

    }
    IEnumerator AddSprites(float wait)
    {
        isFilled = false;
        for (int y = 0; y < wrongButtons.Count; y++)
        {
            int randSprite = Random.Range(0, LeftSprites.Count);
            wrongButtons[y].GetComponent<SpriteRenderer>().sprite = LeftSprites[randSprite];
            yield return new WaitForSeconds(wait);
        }
        isFilled = true;
        yield return new WaitUntil(() => isFilled);
        AfterClick();
    }

    public void PiecesSpriteSetEmpty()
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
        InvokeFilling(1);

    }
    void AfterClick()
    {
        LeftSprites.Clear();
        wrongButtons.Clear();
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
