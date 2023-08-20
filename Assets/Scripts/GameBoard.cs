
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameObject referenceObject;
    public List<Sprite> SpritesList;

    public GameObject background;
    public GameObject emptyButton;
    private GameObject wrongButton = null;
    private List<Sprite> wrongSpritesList;
    private Sprite correctSprite;
    
    private List<GameObject> buttons;
    private List<GameObject> wrongbuttonsList;
    private GameObject correctButton=null;
    private List<GameObject> gridBoard;
    public int xDim;
    public int yDim;
    public bool gridboardGenerated;
    public bool buttonsAreAdded;
    public bool correctMatchSelected;
    public bool spritesAreAdded;
    public bool colorOrAnimActionComplated;
    private void Awake()
    {
        gridBoard = new List<GameObject>();
        buttons = new List<GameObject>();
        wrongbuttonsList = new List<GameObject>();
        wrongSpritesList= new List<Sprite>();

    }
    private void Start()
    {
        StartCoroutine(BuildGame());

    }

    IEnumerator BuildGame()
    {
        StartCoroutine(GenerateGridBackGround(xDim, yDim));
        yield return new WaitUntil(() => gridboardGenerated);
        StartCoroutine(AddButtonToBoard());
        yield return new WaitUntil(() => buttonsAreAdded);
        int choseCorrect = Random.Range(0, SpritesList.Count);
        StartCoroutine(SelectCorrects(choseCorrect));
        yield return new WaitUntil(() => correctMatchSelected);
        StartCoroutine(AddSprites());
        StartCoroutine(ShowReferenceOBject());
        yield return new WaitUntil(() => spritesAreAdded);
        wrongButton.GetComponent<BoxCollider2D>().enabled = true;
        correctButton.GetComponent<BoxCollider2D>().enabled = true;
        for(int i = 0; i < wrongbuttonsList.Count; i++) {
            wrongbuttonsList[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    IEnumerator GenerateGridBackGround(int xDim, int yDim)
    {
        gridboardGenerated = false;
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
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

        for (int x = 0; x < xDim; x++)
        {

            for (int y = 0; y < yDim; y++)
            {
                GameObject empty = Instantiate(emptyButton, GetWorldPosition(x, y), Quaternion.identity);
                empty.GetComponent<BoxCollider2D>().enabled = false;
                buttons.Add(empty);
                yield return new WaitForSeconds(0.01f);

            }
        }

        buttonsAreAdded = true;
    }

    IEnumerator SelectCorrects(int correct)
    {
        correctMatchSelected = false;
        for (int x = 0; x < buttons.Count; x++)
        {
            if (x == correct)
            {
                correctButton = buttons[x];
                correctButton.GetComponent<BoxCollider2D>().enabled = false;
               
            }
            else
            {
                wrongButton = buttons[x];
                wrongButton.GetComponent<BoxCollider2D>().enabled = false;
                wrongButton.GetComponent<PieceControll>().type = 0;
                wrongbuttonsList.Add(wrongButton);
            }
        }
        for (int x = 0; x < SpritesList.Count; x++)
        {
            if (x == correct)
            {
                correctSprite = SpritesList[x];
                
            }
            else
            {
                Sprite wrongSprite = SpritesList[x];
                wrongSpritesList.Add(wrongSprite);
            }
        }

        yield return new WaitForSeconds(0.01f);
        correctMatchSelected = true;
    }
   
    IEnumerator AddSprites()
    {
        spritesAreAdded= false;
        for (int x = 0; x < wrongbuttonsList.Count; x++)
        {
            
            int rand = Random.Range(0, wrongSpritesList.Count);
            wrongbuttonsList[x].GetComponent<SpriteRenderer>().sprite = wrongSpritesList[rand];
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
        if (correctSprite != null)
        {
            referenceObject.GetComponent<SpriteRenderer>().sprite = correctSprite;
            correctButton.GetComponent<PieceControll>().type = 1;
            correctButton.GetComponent<SpriteRenderer>().sprite = correctSprite;
        }


    }

    Vector3 GetWorldPosition(int X, int Y)
    {
        return new Vector3(xDim / 2 - X, Y - yDim / 2.0f, 0);
    }
    public void Clicked()
    {   
        StartCoroutine(Refill());
    }
    IEnumerator Refill()
    {
        StartCoroutine(ChangeSpriteColorOrAnimation(Color.red));
        yield return new WaitUntil(()=>colorOrAnimActionComplated);
        StartCoroutine(ChangeSpriteColorOrAnimation(Color.white));
        yield return new WaitUntil(() => colorOrAnimActionComplated);
        wrongbuttonsList.Clear();
        wrongSpritesList.Clear();
        int choseCorrect = Random.Range(0, SpritesList.Count);
        StartCoroutine(SelectCorrects(choseCorrect));
        yield return new WaitUntil(() => correctMatchSelected);
        StartCoroutine(AddSprites());
        StartCoroutine(ShowReferenceOBject());
        yield return new WaitUntil(() => spritesAreAdded);
        
        wrongButton.GetComponent<BoxCollider2D>().enabled = true;
        correctButton.GetComponent<BoxCollider2D>().enabled = true;
        for (int i = 0; i < wrongbuttonsList.Count; i++)
        {
            wrongbuttonsList[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    IEnumerator ChangeSpriteColorOrAnimation(Color _color)
    {
        colorOrAnimActionComplated = false;
        for (int i = 0; i < wrongbuttonsList.Count; i++)
        {
            wrongbuttonsList[i].GetComponent<SpriteRenderer>().color= _color;
            
            yield return new WaitForSeconds(0.1f);
        }
        colorOrAnimActionComplated = true;

    }


}
