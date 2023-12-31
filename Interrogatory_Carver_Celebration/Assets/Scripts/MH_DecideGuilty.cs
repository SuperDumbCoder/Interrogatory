using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MH_DecideGuilty : MonoBehaviour
{
    private MH_Responses r;
    [SerializeField]
    private GameObject textObj;

    public bool decide(float s, List<MH_Responses.questionList> l){
        float sum =0;
        for(int i = 0; i < l.Count; i++){
            float c = Mathf.Abs(l[i].score - 100);
            if(l[i].score != 0)
            sum += c;
        }
        sum = sum/l.Count;
        return ((s + sum) > 65);
    }


    public void verdict(){
        string name = "";
        if(decide(r.suspicionMeter,r.list)){
            RenderSettings.ambientLight = new Color(1,0.0282f,0);
            name = "Guilty";
        }else{
            RenderSettings.ambientLight = new Color(0.63f,0.63f,0.63f);
            name = "Innocent";
        }
        r.FadeChoice(textObj.GetComponent<CanvasGroup>(),1);
        textObj.GetComponent<Text>().text = name;
    }

  
    // Start is called before the first frame update
    void Start()
    {
      r = GetComponent<MH_Responses>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
