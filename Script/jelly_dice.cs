using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jelly_dice : MonoBehaviour
{
    // Start is called before the first frame update
    
    public bool pickable;
    jelly_cell CurrentCell,lastCell,nextCell;
    public int Val;
    public int team;
    public bool counted;
    public bool TrapWatch;
    public bool move;
    public bool CanTakeOrder;
    public List<Transform>  _1th, _2th, _3th, _v2_1th, _v2_2th;
    public List<MeshRenderer> numbers;
    public void slide(float dir)
    {
        Dir = dir;  
        move = true;
        pickable = true;
        direction = new Vector3(-1 * (Dir), 0, 0);
    }
    Vector3 direction;
    float Dir;
    public void Pick()
    {
        move=false;
    }

    private void Start()
    {
        CanTakeOrder = true;
        //pp
    }

    public Transform Couter;
    public SpriteRenderer number;
    [ContextMenu("test val")]
    public void Test()
    {
        SetValue(Val);
    }
    public void SetValue(int val)
    {
        
        this.Val=val;
        _1th.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        }); 
        _2th.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        }); 
        _3th.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        }); 
        _v2_1th.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        }); 
        _v2_2th.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        }); 
      
        if (val <= 9 || val > 99)
        {
            if (val is <= 9 and > 0)
            {
                _2th[val].gameObject.SetActive(true);
            }else if (val > 9)
            {
                var _100t = val / 100;
                var _10t = (val%100) / 10;
                var _1t = (val%100) % 10;
                _1th[_100t].gameObject.SetActive(true);
                _2th[_10t].gameObject.SetActive(true);
                _3th[_1t].gameObject.SetActive(true);
            }
            
        }
        else 
        {
            var _10t = (val) / 10;
            var _1t = (val) % 10;
            _v2_1th[_10t].gameObject.SetActive(true);
            _v2_2th[_1t].gameObject.SetActive(true);
        }
       
    }


    void sysTick()
    {
       // jumpInRow();
    }

    Collider lastCollider;
    private void OnTriggerEnter(Collider other)
    {
        if(team == 1)
        {
            
            if(other.CompareTag("dice"))
            {
                if(other.GetComponent<jelly_dice>().team == 2)
                {
                    if (lastCollider == other) return;
                    lastCollider = other;
                    if (Val> other.GetComponent<jelly_dice>().Val)
                    {
                        var val = Val - other.GetComponent<jelly_dice>().Val;
                        SetValue(val);
                        other.GetComponent<jelly_dice>().instantKill();
                        Match_res.Instance.Opp_kill_you++;
                        StadiumSound.Instance.PlayHitEnemy();
                    }
                    else if(Val< other.GetComponent<jelly_dice>().Val)
                    {
                        var val =  other.GetComponent<jelly_dice>().Val-Val;
                        print(other.GetComponent<jelly_dice>().Val+" "+Val+"  "+ "TouchVal" +  val);
                        other.GetComponent<jelly_dice>().SetValue(val);
                        instantKill();
                        Match_res.Instance.Opp_kill_opp++;
                    }
                    else
                    {
                        //TO-DO
                        //maybe here is a trap
                        if(CurrentCell!=null)
                        {
                            CurrentCell.cellState = Cell.open;
                            if(other!=null && other.GetComponent<jelly_dice>().CurrentCell!=null)
                            {

                            }
                            
                        }
                        
                        other.GetComponent<jelly_dice>().instantKill();
                        instantKill();
                    }
                }
            }
        }
    }



    private void OnEnable()
    {
        jelly_dicemaker.SystemTick += sysTick;
        jelly_AI.OnCheckingRows += checkRow;
        jelly_AI.OnCheckingRowsForReward+=checkRowForReward;
    }

    private void OnDisable()
    {
        jelly_dicemaker.SystemTick -= sysTick;
        jelly_AI.OnCheckingRows -= checkRow;
        jelly_AI.OnCheckingRowsForReward -= checkRowForReward;
    }
    int rowLenght;
    int currentstep;
    bool Inrow;

    List<jelly_cell> row;

    int checkRow(List<jelly_cell> _row)
    {

        if(_row==row)
        {
            if(team==1)
            {
                jelly_AI.Instance.currentrow -= Val;
            }
            else
            {
                jelly_AI.Instance.currentrow += Val;
            }
            
            return Val;
        }
        else
        {
            return 0;
        }
        
    }
    int checkRowForReward(List<jelly_cell> _row)
    {

        if (_row == row)
        {
            if (team == 1)
            {
                jelly_AI.Instance.currentRowForREwardChk -= Val;
            }
            else
            {
                jelly_AI.Instance.currentRowForREwardChk += Val;
            }

            return Val;
        }
        else
        {
            return 0;
        }

    }



    public void startRow(List<jelly_cell> row)
    {
        move = false;
        this.row = row;
        currentstep++;
        rowLenght = row.Count;
        Inrow = true;
        int dirStart = 0;
        if(team == 1)
        {
            dirStart = 0;
            jelly_dicemaker.Instance.PlayerADices.Remove(this);
            for (int i = 1; i < row.Count; i++)
            {
                queue.Enqueue(row[i]);
            }
        }else if(team == 2)
        {
            jelly_dicemaker.Instance.PlayerBdices.Remove(this);
            dirStart = row.Count - 1;
            for (int i = row.Count-2; i >-1; i--)
            {
                queue.Enqueue(row[i]);
            }
        }
        
        if(queue.Count > 0)
        {
            CurrentCell = row[dirStart];

            jumpInRow();
            //  jumpInRow();
        }
    }
   

    bool trapped;
    Coroutine co_trapped;
    IEnumerator Co_Trapped()
    {
        trapped = true;
        yield return new WaitForSeconds(10);
        trapped = false;
    }

    Cell cachedState;

    IEnumerator hideGlue(GameObject glue)
    {
        yield return new WaitForSeconds(1);
        glue.SetActive(false);
    }


    void jumpInRow()
    {
        if(nextCell != null)
        {
            CurrentCell = nextCell;
            nextCell = null;
        }
       
        if(CurrentCell.cellState==Cell.glu && CurrentCell.teamEffect!=team)
        {
            StartCoroutine(hideGlue(CurrentCell.glue));
            if(team == 2)
            {
                StadiumSound.Instance.PlayMagic();
                Match_res.Instance.Magic_succ_you++;
            }
            else
            {
                Match_res.Instance.Magic_succ_opp++;
            }
            co_trapped=StartCoroutine(Co_Trapped());
            //StopCoroutine(CurrentCell.Cart);
            CurrentCell.trapTime = 10;
            
        }
        bool outOfGame=false;
        if (CurrentCell.cellState == Cell.fanar && CurrentCell.teamEffect != team)
        {

            if (team == 2)
            {
                Match_res.Instance.Magic_succ_you++;
            }
            else
            {
                Match_res.Instance.Magic_succ_opp++;
            }
            StadiumSound.Instance.PlayFanar();
            print("trapped");
            //co_trapped = StartCoroutine(Co_Trapped());
            //StopCoroutine(CurrentCell.Cart);
            CurrentCell.OpenTrapAnim(Cell.fanar);

            Vector3 midPoint = new Vector3(transform.position.x - 1f, transform.position.y + 9, transform.position.z);
            Vector3 outPos = new Vector3(midPoint.x , midPoint.y, midPoint.z+16);
            Vector3[] rt = new Vector3[5];
            rt[0] = transform.position;
            rt[1] = transform.position;
            rt[2] = midPoint;
            rt[3] = outPos;
            rt[4] = outPos;
            LTSpline sp = new LTSpline(rt);
            LeanTween.rotate(gameObject, new Vector3(transform.eulerAngles.x+90, transform.eulerAngles.y, transform.eulerAngles.z ), 0.2f).setDelay(0.3f);
            LeanTween.move(gameObject, sp,4).setEaseInExpo().setEaseOutExpo().setDelay(0.3f). setOnComplete(instantKill);
            CurrentCell.trapTime = 0.9f;
            CurrentCell.cellState = Cell.open;
           // LeanTween.move(gameObject, new Vector3(transform.position.x, transform.position.y - 5, transform.position.z), 3).setOnComplete(instantKill);
            outOfGame = true;
        }
        if (CurrentCell.cellState == Cell.hole && CurrentCell.teamEffect != team)
        {
            if (team == 2)
            {
                Match_res.Instance.Magic_succ_you++;
                StadiumSound.Instance.PlayMechanical();
            }
            else
            {
                Match_res.Instance.Magic_succ_opp++;
            }
            print("trapped");
            //co_trapped = StartCoroutine(Co_Trapped());
            //StopCoroutine(CurrentCell.Cart);
            CurrentCell.OpenTrapAnim(Cell.hole);           

            LeanTween.move(gameObject, new Vector3(transform.position.x, transform.position.y - 5, transform.position.z), 1).setOnComplete(instantKill);
            outOfGame = true;
        }
        cachedState = CurrentCell.cellState;
        if(CurrentCell.cellState!=Cell.hole)
        {
            CurrentCell.cellState = Cell.closed;
        }
       if(CurrentCell.cellState==Cell.open)
        {
            CurrentCell.teamEffect = team;
        }
        currentstep++;
       // print(currentstep + " " + " " + rowLenght);
        if(currentstep <= rowLenght)
        {
            
            var passedSinceMiddle = Mathf.Clamp(currentstep - ((rowLenght / 2) + 1), 0, (rowLenght / 2));
           
           
            if(passedSinceMiddle>0)
            {
                GameManager.Instance.changeStar(team,1);
                if(passedSinceMiddle==1)
                {
                    if (team == 2)
                    {
                        Match_res.Instance.MidPass_opp++;
                    }
                    else
                    {
                        print(passedSinceMiddle+"vxz"+currentstep+"vv"+rowLenght);
                        Match_res.Instance.Midpass_you++;
                    }
                }
            }
           
        }
        
        
        if (queue.Count == 0)
        {
            
            GameManager.Instance.changeScore(team, Val);
            if (team == 2)
            {
                Match_res.Instance.Touch_d_opp++;
            }
            else
            {
                Match_res.Instance.Touch_d_you++;
            }
            //StadiumSound.Instance.PlayTouchDown();
            Board.Instance.PlayTouchDown(transform.position,team);
            counted = true;
            CurrentCell.cellState = Cell.open;    
            instantKill();
            return;
        }
        
        pickable = false;   
        move = false;
        
        if(!outOfGame)
        {
            var nextLand = queue.Dequeue();
            if(TrapWatch)
            {
                while ((nextLand.cellState==Cell.glu || nextLand.cellState == Cell.fanar || nextLand.cellState == Cell.hole) && nextLand.teamEffect!=team )
                {
                    nextLand = queue.Dequeue();
                }
            }
            while ((nextLand.cellState == Cell.glu || nextLand.cellState == Cell.fanar || nextLand.cellState == Cell.hole) && nextLand.teamEffect == team)
            {
                nextLand = queue.Dequeue();
            }
            StartCoroutine(moveToNextCell(nextLand));
        }
       

    }

    public Animator CubeJump;
    private Vector3 NextLand;
    IEnumerator moveToNextCell(jelly_cell cell)
    {
        var nextLand = cell.landingPos;
        NextLand = nextLand;
        // var mid = Random.Range(0.05f, 0.95f);
        Vector3 midPoint = transform.position + (0.5f * (nextLand - transform.position));
        // var height = Random.Range(0.5f, 2.0f);
        //  print(mid+" "+height);
        midPoint += new Vector3(0, 1, 0);
        Vector3[] rt = new Vector3[5];
        rt[0] = transform.position;
        rt[1] = transform.position;
        rt[2] = midPoint;
        rt[3] = nextLand;
        rt[4] = nextLand;
        LTSpline sp = new LTSpline(rt);
        while((cell.cellState==Cell.closed && cell.teamEffect!=team ) || CurrentCell.cellState==Cell.glu || trapped)
        {
            yield return null;
        }

        /*LeanTween.rotateAround(gameObject,Vector3.right,25, jelly_AI.Instance.MoveSpeed / 4).setDelay(jelly_AI.Instance.MoveSpeed / 2).setEaseInSine().setOnComplete(() =>
           {
               LeanTween.rotateAround(gameObject, Vector3.right, -25, jelly_AI.Instance.MoveSpeed / 4).setEaseInSine();
           });*/
        CubeJump.SetTrigger("jump");
        CubeJump.speed = jelly_AI.Instance.animSpeed;
       // LeanTween.moveSpline(gameObject, sp, jelly_AI.Instance.MoveSpeed).setDelay(jelly_AI.Instance.MoveDelay).setEaseInCirc().setOnComplete(jumpInRow);
       
        yield return new WaitForSeconds(0.1f);
        if (CurrentCell != null )
        {
            //CurrentCell.cellState = cachedState;
            if(CurrentCell.cellState==Cell.glu && CurrentCell.teamEffect==team )
            {

            }
            else
            {
                print("hoho");
                CurrentCell.cellState = Cell.open;
            }
           
        }
        nextCell = cell;   

    }

    public void moveByANim()
    {
        LeanTween.move(gameObject, NextLand, jelly_AI.Instance.MoveSpeed).setOnComplete(jumpInRow); 
    }
     
    //ss
    Queue<jelly_cell> queue = new Queue<jelly_cell>(); 
    public void stopSlide()
    {
        StartCoroutine(co_Kill());
    }

    public void instantKill()
    {
        if(CurrentCell!=null)
        {
            if(CurrentCell.cellState == Cell.closed)
            {
                CurrentCell.cellState = Cell.open;
            }

            
        }
        if (team==1)
        {
            // CurrentCell.ResetCell();
            jelly_dicemaker.Instance.PlayerADices.Remove(this);
        }
        else
        {
            jelly_dicemaker.Instance.PlayerBdices.Remove(this);
        }
       // print("here");
        Destroy(gameObject);
    }

    IEnumerator co_Kill()
    {
        pickable = false;   
        
        yield return new WaitForSeconds(1);
        instantKill();
        pickable = false;
    }
    // Update is called once per frame
    public float Speed;
    void Update()
    {
      //  print(Dir);
        if(move)
        {
            transform.Translate(Speed*direction*Time.deltaTime,Space.World);    
        }
        
    }
}
