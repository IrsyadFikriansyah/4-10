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
    public List<OperationUsed> OUSlot = new List<OperationUsed>();

    private int slot1,slot2;
    private int NumSelected1, NumSelected2, lastOperandSelected;
    private string OperationSelected;
    private string finalFormula;

    // Start is called before the first frame update
    void Start()
    {
        Init();
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
            slot.SetNumber(number);
            i++;
        }
    }

    private void Init(){
        level = 0;
        slot1=slot2=-1;
        ChangeNumberLists(0);
    }

    public void SelectNumber(int i){
        if(slot1 == -1){
            slot1 = NSlot[i].GetNumber();
            NSlot[i].Selected();
            NumSelected1 = i;
        }
        else if(slot2 == -1){
            slot2 = NSlot[i].GetNumber();
            NumSelected2 = i;
            NSlot[NumSelected2].SetNumber(slot1);
            NSlot[NumSelected1].SetNumber(slot2);
            NSlot[NumSelected1].Selected();
            slot1=slot2=-1;
        }
    }

    public void SelectOperand(int i){
        OperationSelected = OSlot[i].GetOperand();
        lastOperandSelected = i;
    }

    public void UseOperand(int i){
        if(lastOperandSelected == -1){
            Debug.Log("No Operand used");
        }else{
            OUSlot[i].SetOperation(OperationSelected);
            OSlot[lastOperandSelected].Selected();
            lastOperandSelected = -1;
            OperationSelected = null;
        }
    }

    public void submit(){

        string finalFormula = NSlot[0].GetNumber().ToString() + OUSlot[0].GetOperation() +
                      NSlot[1].GetNumber().ToString() + OUSlot[1].GetOperation() +
                      NSlot[2].GetNumber().ToString() + OUSlot[2].GetOperation() +
                      NSlot[3].GetNumber().ToString();
                      
        Debug.Log(finalFormula);
    }

}
