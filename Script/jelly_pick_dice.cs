using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jelly_pick_dice : MonoBehaviour
{
    Transform dragingObject;
    LayerMask dice, drag, cell;
    public GameObject dragLayer;
    Vector2 mousePos;
    private void Start()
    {
        dragLayer.SetActive(false);
        drag = LayerMask.GetMask("drag");
        cell = LayerMask.GetMask("cell");
    }
    void useCart(jelly_cell cell)
    {
        var rowIndex = 0;
        var rowLenght = 0;
        foreach (var row in Board.Instance.allrows)
        {
            rowLenght = row.Count;
            foreach (var _cell in row)
            {

                if (cell == _cell)
                {
                    rowIndex = row.IndexOf(_cell);
                }
            }
        }

        if (rowIndex > 0 && rowIndex < (rowLenght / 2) && (cell.cellState == Cell.open))
        {
            //   print(cell.name);
            if (PlayerACardDeck.Instance.cards[0].fillAmount == 1)
            {
                GameManager.Instance.changeStar(1, -PlayerACardDeck.Instance.cards[0].GetComponent<jellyCard>().TotalFragments);
                PlayerACardDeck.Instance.cards[0].GetComponent<jellyCard>().filledFragments = 0;
                PlayerACardDeck.Instance.cards[0].fillAmount = 0;
                cell.AddEffect(GameInfo.Instance.juiceLevelScobjt.card, 1);
                Match_res.Instance.Magic_used_you++;
                PlayerACardDeck.Instance.UnhighLight();
            }

        }

    }
    private void Update()
    {
            // Touch touch1 = Input.GetTouch(0);
            Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit1 = new RaycastHit();



            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray1, out hit1, 1000, cell))
                {
                    if (dragingObject == null)
                    {
                        useCart(hit1.collider.GetComponent<jelly_cell>());
                    }
                }
            }

            if (Physics.Raycast(ray1, out hit1, 1000))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit1.collider.CompareTag("dice") && hit1.collider.GetComponent<jelly_dice>().CanTakeOrder && hit1.collider.GetComponent<jelly_dice>().team == 1)
                    {
                        mousePos = Input.mousePosition;
                        if (hit1.collider.GetComponent<jelly_dice>().pickable)
                        {
                            dragingObject = hit1.collider.transform;
                            dragLayer.SetActive(true);
                            dragingObject.GetComponent<jelly_dice>().Pick();
                        }

                    }

                }
                else if (Input.GetMouseButton(0))
                {
                    if (Physics.Raycast(ray1, out hit1, 1000, drag))
                    {
                        if (dragingObject != null)
                        {
                            dragingObject.transform.position = hit1.point;
                        }

                    }

                }
                else if (Input.GetMouseButtonUp(0))
                {

                    if (dragingObject != null)
                    {
                        // print((mousePos - touch.position).sqrMagnitude);
                        dragingObject.GetComponent<jelly_dice>().CanTakeOrder = false;
                        if ((mousePos - (Vector2)Input.mousePosition).sqrMagnitude < 3000)
                        {

                            float distance = 100;
                            Vector3 landing = Vector3.zero;
                            List<jelly_cell> selectedRow = new List<jelly_cell>();


                            foreach (var row in Board.Instance.allrows)
                            {

                                int i = 0;
                                if (dragingObject.GetComponent<jelly_dice>().team == 2)
                                {
                                    i = row.Count - 1;
                                }
                                if (row[i].GetComponent<jelly_cell>().cellState == Cell.closed)
                                {
                                    continue;
                                }
                                var dist = Vector3.Distance(row[i].transform.position, dragingObject.transform.position);
                                // print(distance+"Dist");
                                if (dist < distance)
                                {

                                    distance = dist;
                                    landing = row[i].GetComponent<jelly_cell>().landingPos;
                                    selectedRow = row;

                                }

                            }
                            if (landing == Vector3.zero)
                            {

                                dragingObject.GetComponent<jelly_dice>().instantKill();
                                dragingObject = null;

                                return;

                            }

                            Vector3 curePoint = new Vector3(landing.x, dragingObject.transform.position.y + 0.9f, landing.z);

                            Vector3[] pt = new Vector3[5];
                            pt[0] = dragingObject.transform.position;
                            pt[1] = dragingObject.transform.position;
                            pt[2] = curePoint;
                            pt[3] = landing;
                            pt[4] = landing;

                            LTSpline sp = new LTSpline(pt);

                            var tomove = dragingObject;
                            LeanTween.moveSpline(tomove.gameObject, sp, 0.3f).setOnComplete(() =>
                            {
                                // print("herex");

                                tomove.GetComponent<jelly_dice>().startRow(selectedRow);
                            });
                            dragingObject = null;
                            //dragingObject.transform.position=row[0].GetComponent<jelly_cell>().landingPos.transform.position;
                            return;



                        }
                        if (Physics.Raycast(ray1, out hit1, 1000, cell))
                        {

                            foreach (var row in Board.Instance.allrows)
                            {
                                if (row.Contains(hit1.collider.GetComponent<jelly_cell>()))
                                {
                                    int i = 0;
                                    if (dragingObject.GetComponent<jelly_dice>().team == 2)
                                    {
                                        i = row.Count - 1;
                                    }

                                    if (row[i].GetComponent<jelly_cell>().cellState == Cell.closed)
                                    {
                                        dragingObject.GetComponent<jelly_dice>().instantKill();
                                        dragingObject = null;

                                        return;
                                    }
                                    Vector3 land = row[i].GetComponent<jelly_cell>().landingPos;

                                    Vector3 curePoint = new Vector3(land.x, dragingObject.transform.position.y + 0.5f, land.z);

                                    Vector3[] pt = new Vector3[5];
                                    pt[0] = dragingObject.transform.position;
                                    pt[1] = dragingObject.transform.position;
                                    pt[2] = curePoint;
                                    pt[3] = land;
                                    pt[4] = land;

                                    LTSpline sp = new LTSpline(pt);

                                    var tomove = dragingObject;
                                    LeanTween.moveSpline(tomove.gameObject, sp, 0.3f).setOnComplete(() =>
                                    {
                                        tomove.GetComponent<jelly_dice>().startRow(row);
                                    });

                                    //dragingObject.transform.position=row[0].GetComponent<jelly_cell>().landingPos.transform.position;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            dragingObject.GetComponent<jelly_dice>().instantKill();
                        }
                    }
                    dragingObject = null;
                    dragLayer.SetActive(false);
                }
                else if (Input.GetMouseButton(0))
                {
                    if (dragingObject != null)
                    {
                        if (Physics.Raycast(ray1, out hit1, 1000, drag))
                        {
                            if (dragingObject != null)
                            {
                                dragingObject.transform.position = hit1.point;
                            }

                        }
                    }
                }

            
                
            /*if (Input.touchCount > 0)
            {

                Touch touch = Input.GetTouch(0);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit = new RaycastHit();



                if (touch.phase == TouchPhase.Began)
                {
                    if (Physics.Raycast(ray, out hit, 1000, cell))
                    {
                        if (dragingObject == null)
                        {
                            useCart(hit.collider.GetComponent<jelly_cell>());
                        }
                    }
                }

                if (Physics.Raycast(ray, out hit, 1000))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (hit.collider.CompareTag("dice") && hit.collider.GetComponent<jelly_dice>().CanTakeOrder && hit.collider.GetComponent<jelly_dice>().team == 1)
                        {
                            mousePos = touch.position;
                            if (hit.collider.GetComponent<jelly_dice>().pickable)
                            {
                                dragingObject = hit.collider.transform;
                                dragLayer.SetActive(true);
                                dragingObject.GetComponent<jelly_dice>().Pick();
                            }

                        }

                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        if (Physics.Raycast(ray, out hit, 1000, drag))
                        {
                            if (dragingObject != null)
                            {
                                dragingObject.transform.position = hit.point;
                            }

                        }

                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {

                        if (dragingObject != null)
                        {
                            // print((mousePos - touch.position).sqrMagnitude);
                            dragingObject.GetComponent<jelly_dice>().CanTakeOrder = false;
                            if ((mousePos - touch.position).sqrMagnitude < 3000)
                            {

                                float distance = 100;
                                Vector3 landing = Vector3.zero;
                                List<jelly_cell> selectedRow = new List<jelly_cell>();


                                foreach (var row in Board.Instance.allrows)
                                {

                                    int i = 0;
                                    if (dragingObject.GetComponent<jelly_dice>().team == 2)
                                    {
                                        i = row.Count - 1;
                                    }
                                    if (row[i].GetComponent<jelly_cell>().cellState == Cell.closed)
                                    {
                                        continue;
                                    }
                                    var dist = Vector3.Distance(row[i].transform.position, dragingObject.transform.position);
                                    // print(distance+"Dist");
                                    if (dist < distance)
                                    {

                                        distance = dist;
                                        landing = row[i].GetComponent<jelly_cell>().landingPos;
                                        selectedRow = row;

                                    }

                                }
                                if (landing == Vector3.zero)
                                {

                                    dragingObject.GetComponent<jelly_dice>().instantKill();
                                    dragingObject = null;

                                    return;

                                }

                                Vector3 curePoint = new Vector3(landing.x, dragingObject.transform.position.y + 0.9f, landing.z);

                                Vector3[] pt = new Vector3[5];
                                pt[0] = dragingObject.transform.position;
                                pt[1] = dragingObject.transform.position;
                                pt[2] = curePoint;
                                pt[3] = landing;
                                pt[4] = landing;

                                LTSpline sp = new LTSpline(pt);

                                var tomove = dragingObject;
                                LeanTween.moveSpline(tomove.gameObject, sp, 0.3f).setOnComplete(() =>
                                {
                                // print("herex");

                                tomove.GetComponent<jelly_dice>().startRow(selectedRow);
                                });
                                dragingObject = null;
                                //dragingObject.transform.position=row[0].GetComponent<jelly_cell>().landingPos.transform.position;
                                return;



                            }
                            if (Physics.Raycast(ray, out hit, 1000, cell))
                            {

                                foreach (var row in Board.Instance.allrows)
                                {
                                    if (row.Contains(hit.collider.GetComponent<jelly_cell>()))
                                    {
                                        int i = 0;
                                        if (dragingObject.GetComponent<jelly_dice>().team == 2)
                                        {
                                            i = row.Count - 1;
                                        }

                                        if (row[i].GetComponent<jelly_cell>().cellState == Cell.closed)
                                        {
                                            dragingObject.GetComponent<jelly_dice>().instantKill();
                                            dragingObject = null;

                                            return;
                                        }
                                        Vector3 land = row[i].GetComponent<jelly_cell>().landingPos;

                                        Vector3 curePoint = new Vector3(land.x, dragingObject.transform.position.y + 0.5f, land.z);

                                        Vector3[] pt = new Vector3[5];
                                        pt[0] = dragingObject.transform.position;
                                        pt[1] = dragingObject.transform.position;
                                        pt[2] = curePoint;
                                        pt[3] = land;
                                        pt[4] = land;

                                        LTSpline sp = new LTSpline(pt);

                                        var tomove = dragingObject;
                                        LeanTween.moveSpline(tomove.gameObject, sp, 0.3f).setOnComplete(() =>
                                        {
                                            tomove.GetComponent<jelly_dice>().startRow(row);
                                        });

                                        //dragingObject.transform.position=row[0].GetComponent<jelly_cell>().landingPos.transform.position;
                                        break;
                                    }
                                }

                            }
                            else
                            {
                                dragingObject.GetComponent<jelly_dice>().instantKill();
                            }
                        }
                        dragingObject = null;
                        dragLayer.SetActive(false);
                    }
                    else if (touch.phase == TouchPhase.Stationary)
                    {
                        if (dragingObject != null)
                        {
                            if (Physics.Raycast(ray, out hit, 1000, drag))
                            {
                                if (dragingObject != null)
                                {
                                    dragingObject.transform.position = hit.point;
                                }

                            }
                        }
                    }

                }
            }*/
        }
    }
}
