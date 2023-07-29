using System;
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
            ResetUseOperand();
        }else{
            Debug.Log("You Win");
        }
    }

    private void ChangeNumberLists(int index){
        int i = 0;
        foreach (var slot in NSlot)
        {
            int number = puzzleLevel[index][i];
            // Debug.Log(number);
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

    // fungsi untuk menilai prioritas operator matematika
    private int precedence(char c) {
        if (c == '(') return 0;
        if (c == '+' || c == '-') return 1;
        if (c == '*' || c == '/') return 2;
        if (c == '^') return 3;
        return -1;
    } //end precedence

    private string infixToPosfix(string infix)  {
        Stack<char> myStack = new Stack<char>();
        String posfix = string.Empty;

        for (int i = 0; i < infix.Length; i++) {
            char curr = infix[i], c;
            if (curr == '×') curr = '*';
            if (curr == '÷') curr = '/';

            if (Char.IsDigit(curr))
                posfix += curr;

            else if (curr == '(')
                myStack.Push(curr);

            else if (curr == ')')
                while ((c = myStack.Pop()) != '(')
                    posfix += c;

            else {
                while (myStack.Count != 0 && precedence(myStack.Peek()) >= precedence(curr))
                    posfix += myStack.Pop();
                myStack.Push(curr);
            }
        }

        while (myStack.Count != 0)
            posfix += myStack.Pop();

        return posfix;
    }

    float calculatePosfix(string posfix) {
        Stack<float> myStack = new Stack<float>();
        
        for (int i = 0; i < posfix.Length; i++) {
            char curr = posfix[i];

            // jika angka
            if (Char.IsDigit(curr)) {
                float angka = curr - '0';
                myStack.Push(angka);
            }

            // jika operator
            else {
                float num1, num2;
                num2 = myStack.Pop(); 
                num1 = myStack.Pop();

                switch (curr){  //push hasil perhitungan
                    case '+' : 
                        myStack.Push(num1 + num2); 
                        break;
                    case '-' : 
                        myStack.Push(num1 - num2); 
                        break;
                    case '*' : 
                        myStack.Push(num1 * num2); 
                        break;
                    case '/' : 
                        myStack.Push(num1 / num2); 
                        break;
                    case '^' : 
                        myStack.Push((float)Math.Pow(num1, num2)); 
                        break;
                }   
            }
        } 
        return myStack.Peek();
    }

    private void checkAnswer(float answer) {
        bool isInt = Math.Floor(answer) == Math.Ceiling(answer);
        if (!isInt)
            Debug.LogFormat("salah bukan int : {0}", answer);
        else {
            if ((int)answer == 10){
                Debug.Log("bener");
                LevelUp();
            }   
            else
                Debug.LogFormat("salah int : {0}", (int)answer);
        }

    }

    public void submit(){
        string finalFormula = NSlot[0].GetNumber().ToString() + OUSlot[0].GetOperation() + 
                              NSlot[1].GetNumber().ToString() + OUSlot[1].GetOperation() +
                              NSlot[2].GetNumber().ToString() + OUSlot[2].GetOperation() +
                              NSlot[3].GetNumber().ToString();
        
        // Debug.Log(finalFormula);
        string posfix = infixToPosfix(finalFormula);
        float answer = calculatePosfix(posfix);
        Debug.Log(answer);

        checkAnswer(answer);
    }

    public void ResetUseOperand(){
        foreach (var slot in OUSlot)
        {
            slot.SetOperation("");
        }
    }

}
