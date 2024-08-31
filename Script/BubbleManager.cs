using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BubbleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bubble;
    private List<GameObject> bubbles;
    CancellationTokenSource cancellationTokenSource;
    public GameObject Ice1, Ice2;
    public Image back,sky;
    public CanvasGroup playBtn;

    public int level;
    public GameObject GetBubble()
    {
        GameObject val = null;
        foreach (var VARIABLE in bubbles)
        {
            if (!VARIABLE.activeInHierarchy)
            {
                val = VARIABLE;
                break;
            }
        }

        return val;
    }
    
    void CreatePool()
    {
        bubbles = new List<GameObject>();
        for (int i = 0; i < 30; i++)
        {
            var temp = Instantiate(bubble);
            temp.SetActive(false);
            bubbles.Add(temp);
        }
    }
    void Start()
    {
        level = PlayerPrefs.GetInt("jlevel");
        Time.timeScale = 1;
        print("BubStart");
        var colors=JuiceUpLevelManager.Instance.getcollor(level);
        back.color = colors.back;
        bubble.GetComponent<Image>().color = colors.bubble;
        CreatePool();
        CreateBublles();
        Ice1.transform
            .DOMove(new Vector3(Ice1.transform.position.x, Ice1.transform.position.y + 22, Ice1.transform.position.z),
                5).SetLoops(-1, LoopType.Yoyo);
        Ice2.transform
            .DOMove(new Vector3(Ice2.transform.position.x, Ice2.transform.position.y + 37, Ice2.transform.position.z),
                6).SetLoops(-1, LoopType.Yoyo);

    }

    public RectTransform liquid;
    void BubbleUp()
    {
        var bubble=GetBubble();
        bubble.SetActive(true);
        var start = Random.Range(0f, 1f);
        var traveltime = Random.Range(1.6f, 5.6f);
        var size = Random.Range(0.3f, 1f);
        print(liquid.anchoredPosition.x+"  is liquid anchorPosX");
        var startX = ((liquid.sizeDelta.x *start )+(liquid.anchoredPosition.x-liquid.sizeDelta.x /2));
        print(startX+"  startX");
        bubble.GetComponent<RectTransform>().anchoredPosition = new Vector2(startX,
            liquid.anchoredPosition.y-(liquid.sizeDelta.y/2));
        bubble.transform.parent = liquid;
        bubble.transform.localScale = size * Vector3.one;
        var height = Random.Range(10, 100);
        bubble.GetComponent<RectTransform>().DOAnchorPos(new Vector2(bubble.GetComponent<RectTransform>().anchoredPosition.x,liquid.sizeDelta.y/2+liquid.anchoredPosition.y+22), traveltime).OnComplete(()=>
        {
            callBack(bubble.GetComponent<bubbleItem>());
        });
    }

  //  private TweenCallback<bubbleItem> tweenCallback;
    void callBack(bubbleItem item)
    {
      item.pop();  
    }
    
    async void CreateBublles()
    {
        cancellationTokenSource = new CancellationTokenSource();
        var time = Random.Range(200, 1200);
        if (liquid.GetComponentsInChildren<bubbleItem>().Length == 0)
        {
            time = 0;
            playBtn.DOFade(4, 1);
        }
        try
        {
            await Task.Delay(time, cancellationTokenSource.Token);
            print("ok");
           BubbleUp();
        }
        catch
        {
            Debug.Log("Task was cancelled!");
            return;
        }
        finally
        {
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }
        CreateBublles();
        Debug.Log("Async Task Ended on: " + gameObject);
        
    }

    private void OnApplicationQuit()
    {
        print("exit");
        cancellationTokenSource?.Cancel();
    }

    private void OnDestroy()
    {
        print("ondestroy");
        cancellationTokenSource?.Cancel();
    }
}
