using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class jellyCard : MonoBehaviour, IDragHandler,IPointerMoveHandler,IBeginDragHandler,IEndDragHandler,IPointerDownHandler
{
    public Image image;
    public RectTransform rectTransform;
    bool isDragging;
    bool CanDrop;
    Vector3 init;
    Color main, fade;
    public Cards cardType;
    public int TotalFragments,filledFragments;
    jelly_cell selectedCell;
    public int team;
    private void Start()
    {
        main = image.color;
        fade=new Color(main.r, main.g, main.b,0.6f);
        init = rectTransform.anchoredPosition;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        return;
        if (image.fillAmount == 1 && cardType!=Cards.jumpTrap)
        {
            image.color = fade;
            isDragging = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        return ;
        if(isDragging)
        {

            //  rectTransform.anchoredPosition += eventData.delta / GameManager.Instance._canvas.scaleFactor;
            rectTransform.position = eventData.position;
            Ray ray=Camera.main.ScreenPointToRay(eventData.position);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,1000,GameManager.Instance.cellMask))
            {
                var cell = hit.collider.GetComponent<jelly_cell>();
                selectedCell = cell;
                if (!cell.highlighting)
                {
                    CanDrop = false;
                    var rowIndex = 0;
                    var rowLenght=0;
                    foreach(var row in Board.Instance.allrows)
                    {
                        rowLenght = row.Count;
                        foreach(var _cell in row)
                        {
                            _cell.Unhighlight();
                            if(cell==_cell)
                            {
                                rowIndex=row.IndexOf(_cell);
                            }
                        }
                    }
                    if(team==1)
                    {
                        if (rowIndex>0 &&  rowIndex < (rowLenght / 2) && (cell.cellState == Cell.open))
                        {
                            //   print(cell.name);
                            cell.HighLight();
                            CanDrop = true;
                        }
                    }else if(team==2)
                    {
                        if (rowIndex <rowLenght-1 && rowIndex> (rowLenght / 2) && (cell.cellState == Cell.open))
                        {
                            //   print(cell.name);
                            cell.HighLight();
                            CanDrop = true;
                        }
                    }
                    
                   
                }

            }
            else
            {
                if(selectedCell!=null)
                {
                    selectedCell=null;
                    foreach (var row in Board.Instance.allrows)
                    {
                        
                        foreach (var _cell in row)
                        {
                            _cell.Unhighlight();
                            
                        }
                    }
                }
                CanDrop=false;
            }
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        return;
        if(isDragging)
        {
            image.color = main;
            rectTransform.anchoredPosition = init;
            isDragging = false;
            foreach (var row in Board.Instance.allrows)
            {
                foreach (var _cell in row)
                {
                    _cell.Unhighlight();
                }
            }
            if(CanDrop)
            {
                GameManager.Instance.changeStar(1, -TotalFragments);
                filledFragments = 0;
                image.fillAmount = 0;
                Color clr = image.color;
                clr.a = 0.4f;
                image.color = clr;
                selectedCell.AddEffect(cardType,team);
            }
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        return ;
       if(image.fillAmount==1 && cardType==Cards.jumpTrap)
        {
            jelly_dicemaker.Instance.TrapJumpActivator(team);
            GameManager.Instance.changeStar(1, -TotalFragments);
            filledFragments = 0;
            image.fillAmount = 0;
        }

    }
}
