using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapMessage : SingleTon<MapMessage>
{
    public TextMeshProUGUI FinalMessage;


    public void  showFinal(string message)
    {
        gameObject.SetActive(true);
        FinalMessage.text = message;
    }

}
