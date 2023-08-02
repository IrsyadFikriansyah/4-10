using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotBase : MonoBehaviour
{
    [SerializeField] private Sprite chooseStateSlot, normalState;
    
    public void ChangeSlotStateImage(bool cState){
        if(cState) this.GetComponent<Image>().sprite = chooseStateSlot;
        else this.GetComponent<Image>().sprite = normalState;
    }
}
