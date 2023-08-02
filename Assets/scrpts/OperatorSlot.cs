using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class OperatorSlot :SlotBase
{
    // Start is called before the first frame update
    private string operand;
    private bool selected;
    void Start()
    {
        selected = false;
        operand = GetComponentInChildren<TMP_Text>().text;
    }

    public string GetOperand(){
        return operand;
    }

    public void Selected(){
        selected = !selected;
        if(selected ==  false){
            Debug.Log("Turn Off");
        }
        else{
            Debug.Log("Turn On");
        }
        ChangeSlotStateImage(selected);
    }

}
