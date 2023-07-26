using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FourToTenMechanism : MonoBehaviour
{

    private int[][] puzzleLevel = new int[][] {
        new int[] { 1, 4, 7, 2 },
        new int[] { 1, 2, 9, 2 },
        new int[] { 2, 0, 2, 6 }
        };
    private int level;
    public List<NumberSlot> NSlot = new List<NumberSlot>();
    public List<OperatorSlot> OSlot = new List<OperatorSlot>();

    private int slot1,slot2;
    private int NumSelected1, NumSelected2;
    private int OperationSelected;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LevelUp(){
        if(level<2){
            level++;
            ChangeNumberLists(level);
        }else{
            Debug.Log("You Win");
        }
    }

    private void ChangeNumberLists(int index){
        int i = 0;
        foreach (var slot in NSlot)
        {
            int number = puzzleLevel[index][i];
            Debug.Log(number);
            //slot.SetNumber(number);
            i++;
        }
    }

    private void Init(){
        level = 0;
        slot1=slot2=-1;
        ChangeNumberLists(0);
    }

    public void SelectNumber(int i){
        Debug.Log("Pressed");
        if(slot1 == -1){
            slot1 = NSlot[i].GetNumber();
            Debug.Log("Pressed Slot1: "+slot1);
            NSlot[i].Selected();
            NumSelected1 = i;
        }
        else if(slot2 == -1){
            Debug.Log("Pressed Slot2: "+slot2);
            slot2 = NSlot[i].GetNumber();
            NumSelected2 = i;
        }else{
            NSlot[NumSelected2].SetNumber(slot1);
            NSlot[NumSelected1].SetNumber(slot2);
            slot1=slot2=-1;
        }
        
    }
}
