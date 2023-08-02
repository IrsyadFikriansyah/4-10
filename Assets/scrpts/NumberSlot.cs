using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberSlot : SlotBase
{
    // Start is called before the first frame update
    private int number;
    private bool selected;
    void Start()
    {
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNumber(int newNumber){
        number = newNumber;
        GetComponentInChildren<TMP_Text>().text = newNumber.ToString();
    }

    public int GetNumber(){
        return number;
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
