using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OperationUsed : MonoBehaviour
{
    // Start is called before the first frame update
    private string operation;
    public void EmptySlot(){
        GetComponentInChildren<TMP_Text>().text = "";
    }

    public void SetOperation(string newOperation){
        operation = newOperation;
        GetComponentInChildren<TMP_Text>().text = operation;
    }

    public string GetOperation(){
        return operation;
    }
    
}
