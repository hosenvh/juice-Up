using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jelly_cell : MonoBehaviour
{
    public Vector3 landingPos;
    public GameObject highlight;
    public bool highlighting = false;
    [SerializeField]
    GameObject fanar;
    Jelly_Hole hole;
    public float trapTime;
    public bool CanHighLight;

    private void OnEnable()
    {
        PlayerACardDeck.CartEnabled += changeHighlight;
    }


    private void OnDisable()
    {
        PlayerACardDeck.CartEnabled -= changeHighlight;
    }

    public void changeHighlight(bool val)
    {
        if(CanHighLight)
        highlight.SetActive(val);
    }

    public void ActivateHole()
    {
        hole.gameObject.SetActive(true);
        //fanar.gameObject.SetActive(true);  
        GetComponent<MeshRenderer>().enabled = false;
        cellState = Cell.hole;
    }

    public void ActivateFanar()
    {
        //trap.gameObject.SetActive(true);
        fanar.gameObject.SetActive(true);
        //GetComponent<MeshRenderer>().enabled = false;
        cellState = Cell.fanar;
    }

    public void DisAbleFanar(Cell LastState)
    {
        fanar.gameObject.SetActive(false);
        GetComponent<MeshRenderer>().enabled = true;
        cellState = LastState;
    }



    public void DisableHole(Cell LastState)
    {
        hole.gameObject.SetActive(false);
        GetComponent<MeshRenderer>().enabled = true;
        cellState = LastState;
    }

    public Coroutine Cart;
    Action<Cell> CartEnd;
    IEnumerator ActivateCart(Action start,Action<Cell> end,Cell lastState,float duration)
    {
        
        start();
        trapTime = duration;
        float lasttime=Time.time;
        while(trapTime>0)
        {
            var elapsed=Time.time-lasttime;
            trapTime -= elapsed;
            lasttime = Time.time;
            yield return null;
            
        }
        end(Cell.open);
    }


    public void HighLight()
    {
        highlight.SetActive(true);
        highlighting = true;
    }

    public Cell cellState;

    private void Start()
    {
        landingPos=transform.position +new Vector3 (0,0.5f,0);
        cellState = Cell.open;
        hole=Instantiate(Board.Instance.hole);
        hole.transform.parent = transform;
        hole.transform.transform.localPosition = Vector3.zero;
        hole.gameObject.SetActive (false);
        //
        highlight=Instantiate(Board.Instance.highlight);
        highlight.transform.parent = transform;
        highlight.transform.localPosition = new Vector3(0,0,0.000339f);   
        highlight.gameObject.SetActive (false);
        highlight.transform.localScale = 0.2372315f * Vector3.one;
        //
        glue = Instantiate(Board.Instance.Glue);
        glue.transform.parent = transform;
        glue.transform.localPosition = Vector3.zero;
        glue.gameObject.SetActive(false);
        //
        fanar = Instantiate(Board.Instance.Fanar);
        fanar.transform.parent = transform;
        fanar.transform.localPosition = Vector3.zero;
        fanar.gameObject.SetActive(false);

    }
    public GameObject glue;
    public void AddEffect(Cards card,int team)
    {
        teamEffect = team;
        if (card==Cards.glu)
        {
            if (Cart != null)
            {
                StopCoroutine(Cart);
            }
            if (CartEnd != null)
            {
                CartEnd(cellState);
            }
            CartEnd = new Action<Cell>(DisAbleGlu);
          //  print("glu");
            Cart = StartCoroutine(ActivateCart(ActivateGlu, CartEnd, cellState,10));

        }
        else if(card==Cards.fanar)
        {
            if(Cart!=null)
            {
                StopCoroutine(Cart);
            }
            if(CartEnd!=null)
            {
                CartEnd(cellState);
            }
            CartEnd=new Action<Cell>(DisAbleFanar);
            
            Cart = StartCoroutine(ActivateCart(ActivateFanar, CartEnd,cellState, 10));          
        }else if(card==Cards.hole)
        {
            if (Cart != null)
            {
                StopCoroutine(Cart);
            }
            if (CartEnd != null)
            {
                CartEnd(cellState);
            }
            CartEnd = new Action<Cell>(DisableHole);

            Cart = StartCoroutine(ActivateCart(ActivateHole, CartEnd, cellState,10));
        }
    }


    public Coroutine glue_Co;

    public  int teamEffect;

    public void ResetCell()
    {
        cellState=Cell.open;
        glue.SetActive(false);
    }


    public void ActivateGlu()
    {
        cellState = Cell.glu;
        glue.SetActive(true);
    }

    public void DisAbleGlu(Cell LastState)
    {
        cellState = LastState;
        glue.SetActive(false);
    }


   public IEnumerator Co_GlueState(Cell LastState,bool trapped=false)
    {
        yield return new WaitForSeconds(10);
        
        cellState = LastState;  
        glue.SetActive(false);

    }

    public void OpenTrapAnim(Cell TrapType)
    {
        if(TrapType==Cell.fanar)
        {

        }
        else if(TrapType==Cell.hole) 
        {
            hole.OpenHole();
        }
       
    }
    public void Unhighlight()
    {
        highlight.SetActive(false);
        highlighting = false;   
    }

    public GameObject open;
    

}
