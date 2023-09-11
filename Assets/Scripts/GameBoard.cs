
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameObject referenceObject;
    public List<Sprite> SpritesList;

    public UiMetters metters;

    public GameObject background;
    public GameObject emptyButton;
    private GameObject wrongButton = null;
    private List<Sprite> wrongSpritesList;
    private Sprite correctSprite;
    
    private List<GameObject> buttons;
    private List<GameObject> wrongbuttonsList;
    private GameObject correctButton=null;
    private List<GameObject> gridBoard;
    public Score _score;
    public int _xDim;
    public int _yDim;
    public bool gridboardGenerated;
    public bool buttonsAreAdded;
    public bool correctMatchSelected;
    public bool spritesAreAdded;
    public bool showReference;
    public bool colorOrAnimActionComplated;
    public bool nextLevel;
    public bool reFilling;
    private void Awake()
    {
        

    }
    public void AwakeBoard()
    {
        reFilling = false;
        nextLevel = false;
        gridBoard = new List<GameObject>();
        buttons = new List<GameObject>();
        wrongbuttonsList = new List<GameObject>();
        wrongSpritesList = new List<Sprite>();
        
    }
    public void StartBoard()
    {
        
        StartGame(_xDim, _yDim);
    }
    public void StartGame(int xDim, int yDim)
    {
        nextLevel = false;
        for (int i = 0; i < gridBoard.Count; i++)
        {
            Destroy(gridBoard[i]);
            //gridBoard.RemoveAt(i);
        }
        //for (int i = 0; i < wrongbuttonsList.Count; i++)
        //{
        //    Destroy(wrongbuttonsList[i]);
        //    wrongbuttonsList.RemoveAt(i);
        //}
        gridBoard.Clear();
        wrongSpritesList.Clear();
        wrongbuttonsList.Clear();
        correctButton = null;
        correctSprite = null;
        gridBoard = new List<GameObject>();
        buttons = new List<GameObject>();
        wrongbuttonsList = new List<GameObject>();
        wrongSpritesList = new List<Sprite>();

        StartCoroutine(BuildGame(xDim, yDim));
    }

    IEnumerator BuildGame(int xDim,int yDim)
    {
        _xDim = xDim;
        _yDim = yDim;
        yield return new WaitForSeconds(1);
        StartCoroutine(GenerateGridBackGround(_xDim, _yDim));
        yield return new WaitUntil(() => gridboardGenerated);
        StartCoroutine(AddButtonToBoard());
        yield return new WaitUntil(() => buttonsAreAdded);
        int choseCorrect = Random.Range(0, SpritesList.Count);
        StartCoroutine(SelectCorrects());
        yield return new WaitUntil(() => correctMatchSelected);
        StartCoroutine(AddSprites());
        StartCoroutine(ShowReferenceOBject());
        yield return new WaitUntil(() => spritesAreAdded);
        wrongButton.GetComponent<BoxCollider2D>().enabled = true;
        correctButton.GetComponent<BoxCollider2D>().enabled = true;
        for(int i = 0; i < wrongbuttonsList.Count; i++) {
            wrongbuttonsList[i].GetComponent<BoxCollider2D>().enabled = true;
        }
        nextLevel = true;
        //StartCoroutine(PlayerCouldntFind());
    }
    IEnumerator GenerateGridBackGround(int newxDim, int newyDim)
    {
        gridboardGenerated = false;
        for (int x = 0; x < newxDim; x++)
        {
            for (int y = 0; y < newyDim; y++)
            {
                GameObject bg = Instantiate(background, GetWorldPosition(x, y), Quaternion.identity);
                gridBoard.Add(bg);

                yield return new WaitForSeconds(0.01f);
            }
        }
        gridboardGenerated = true;

    }


    IEnumerator AddButtonToBoard()
    {
        buttonsAreAdded = false;

        for (int i = 0; i < gridBoard.Count; i++)
        {
            GameObject empty = Instantiate(emptyButton, new Vector3(gridBoard[i].transform.position.x, gridBoard[i].transform.position.y,0), Quaternion.identity);
            empty.GetComponent<BoxCollider2D>().enabled = false;
            empty.transform.parent = gridBoard[i].transform;
            buttons.Add(empty);
            yield return new WaitForSeconds(0.01f);
        }

        buttonsAreAdded = true;
    }

    IEnumerator SelectCorrects()
    {
        wrongbuttonsList.Clear();
        correctMatchSelected = false;
        int rand=Random.Range(0, buttons.Count);
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i == rand)
            {
                correctButton = buttons[i];
                correctButton.GetComponent<BoxCollider2D>().enabled = false;
                correctButton.GetComponent<PieceControll>().type = 1;
                
            }
            else
            {
                wrongButton = buttons[i];
                wrongButton.GetComponent<BoxCollider2D>().enabled = false;
                wrongButton.GetComponent<PieceControll>().type = 0;
                wrongbuttonsList.Add(wrongButton);
            }

            wrongbuttonsList.Add(buttons[i]);
        }

        yield return new WaitForSeconds(0.01f);
        correctMatchSelected = true;
    }
   
    IEnumerator AddSprites()
    {
        spritesAreAdded= false;
        wrongSpritesList.Clear();
        int randCorrect = Random.Range(0, SpritesList.Count);
        correctSprite= SpritesList[randCorrect];
        for (int i = 0; i < SpritesList.Count; i++)
        {
            if (i != randCorrect)
            {
                wrongSpritesList.Add(SpritesList[i]);
            }

        }

        for (int x = 0; x < buttons.Count; x++)
        {
            int rand = Random.Range(0, wrongSpritesList.Count);

            if (buttons[x].GetComponent<PieceControll>().type==1) {
                buttons[x].GetComponent<SpriteRenderer>().sprite = correctSprite;

            }
            else
            {
                buttons[x].GetComponent<SpriteRenderer>().sprite = wrongSpritesList[rand];
            }

            yield return new WaitForSeconds(0.01f);
        }
        
        spritesAreAdded = true;


    }
    IEnumerator ShowReferenceOBject()
    {
        
        for (int x = 0; x < SpritesList.Count; x++)
        {
            int rand = Random.Range(0, SpritesList.Count);
            Sprite randSprite = SpritesList[rand];
            referenceObject.GetComponent<SpriteRenderer>().sprite = randSprite;
            yield return new WaitForSeconds(0.01f);
        }
        
        referenceObject.GetComponent<SpriteRenderer>().sprite = correctSprite;
            

    }

    Vector3 GetWorldPosition(int X, int Y)
    {
        int x = X * 2;
        int y=Y*2;
        int ycor = -3;
        //return new Vector3(xDim / 2f - X, Y - yDim / 2f, 0);
        return new Vector3(-_xDim+1f +x, ycor+_yDim-y-1f,0);
    }
    public void Clicked()
    {   
        StartCoroutine(Refill());
    }
    IEnumerator Refill()
    {
        reFilling=true;
        StartCoroutine(ChangeSpriteColorOrAnimation(Color.red));
        yield return new WaitUntil(()=>colorOrAnimActionComplated);
        StartCoroutine(ChangeSpriteColorOrAnimation(Color.white));
        yield return new WaitUntil(() => colorOrAnimActionComplated);
        wrongbuttonsList.Clear();
        
        int choseCorrect = Random.Range(0, SpritesList.Count);
        StartCoroutine(SelectCorrects());
        yield return new WaitUntil(() => correctMatchSelected);
        StartCoroutine(AddSprites());
        StartCoroutine(ShowReferenceOBject());
        yield return new WaitUntil(() => spritesAreAdded);
        //wrongButton.GetComponent<BoxCollider2D>().enabled = true;
        if (correctButton !=null)
        {
            correctButton.GetComponent<BoxCollider2D>().enabled = true;
        }
        
        for (int i = 0; i < wrongbuttonsList.Count; i++)
        {
            wrongbuttonsList[i].GetComponent<BoxCollider2D>().enabled = true;
        }

        reFilling= false;
       
        //StartCoroutine(PlayerCouldntFind());

    }

    IEnumerator ChangeSpriteColorOrAnimation(Color _color)
    {
        colorOrAnimActionComplated = false;
        for (int i = 0; i < wrongbuttonsList.Count; i++)
        {
           
            if (wrongbuttonsList[i].GetComponent<PieceControll>().type == 1) { 
                wrongbuttonsList[i].GetComponent<SpriteRenderer>().color = Color.green;
                }
            else
            {
                wrongbuttonsList[i].GetComponent<SpriteRenderer>().color = _color;
            }
            
            
            yield return new WaitForSeconds(0.01f);
            wrongbuttonsList[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
        colorOrAnimActionComplated = true;

    }

    public void ResetVar()
    {
        metters.ResetVar();
        _score.ResetVar();
        _xDim = 2;
        _yDim = 1;
        Invoke(nameof(StartBoard), 1);
    }

    public void Paused(bool falseOrTrue)
    {
        for (int i = 0; i < gridBoard.Count; i++)
        {
            gridBoard[i].transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = falseOrTrue;
        }
    }
}
