using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MH_Responses : MonoBehaviour
{
    [Serializable]
    public class questionList {
        [TextAreaAttribute]
        public string question;
        public bool isImportant;
        public float score;
        public List<answerObj> answers = new List<answerObj>();
    }
    [Serializable]
    public class answerObj{
        [TextAreaAttribute]
        public string answer;
        public UnityEvent action;
        public float suspicion;
    }
  

    public List<questionList> list = new List<questionList>();

    [Header("The UI objects needed to be assigned")]
    [SerializeField]
    private GameObject[] buttonList;
   
    [SerializeField]
    private GameObject textObj;
    [Header("gameProgression")]
    public int progression;
    [Header("The meters that influence the game state")]
    public float suspicionMeter;
    public float worryMeter;
    
    private CanvasGroup cg;

    questionList obj;
 
    public void assignValue(){
   
         if(progression < list.Count){
            FadeChoice(cg,1);
            obj = list[progression];
            //loops to plaster the answers on the buttons 
            textObj.GetComponent<Text>().text = obj.question;
            for(int i = 0; i < obj.answers.Count; i++){
                buttonList[i].transform.GetChild(0).GetComponent<Text>().text = obj.answers[i].answer;
            }
            StartCoroutine(scareMeter());
        }
    }

    public IEnumerator scareMeter(){
        while(cg.alpha == 1 && obj.isImportant == true){
            if(worryMeter < 100)
            worryMeter += 2f;
            obj.score = ((worryMeter > 50) ? Mathf.Abs(worryMeter - 100)/50 : worryMeter/50) * 100;
            yield return new WaitForSeconds(0.25f);
        }  
        worryMeter = 0;
    }

    public void minusSuspicion(){
    //the button object
        GameObject o = EventSystem.current.currentSelectedGameObject;
        string name = o.transform.GetChild(0).GetComponent<Text>().text;
       //determines whether a name similar to that of the buttons 
        answerObj results = (name != "" || name != null) ? obj.answers.Single(s => s.answer == name): null;
        //minus the suspicion based on the suspicion value of the question
        if(results != null && progression < list.Count){
           suspicionMeter = suspicionMeter + results.suspicion;
           results.action.Invoke();         
           progression++;
           if(progression < list.Count)
                obj = list[progression];
        }
        FadeChoice(cg,0);   
    }

    void Awake(){
        cg = GetComponent<CanvasGroup>();
        FadeChoice(cg,0);   
    }
    
    public void FadeChoice(CanvasGroup cg, int num){
        cg.alpha = num;
        cg.interactable = (num == 1) ? true:false;
        cg.blocksRaycasts = (num == 1) ? true:false;
    }
}
