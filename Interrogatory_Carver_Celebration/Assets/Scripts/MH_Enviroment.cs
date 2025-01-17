using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MH_Enviroment : MonoBehaviour
{
    
    [Range(1,100)]
    public float range;
    private Color firstColor;

    [Range(50,100)]
    public float startBreathing;

    [SerializeField]
    private MH_Responses response;

    [SerializeField]
    private MH_AudioSourcer audio;

    [SerializeField]
    private AudioClip clip, clip1,clip2;

    bool isEnd;


    public void changeColorScene(){
      if(range > 50){
        firstColor.r = (range - 30)/100;
        RenderSettings.ambientLight = new Color((range-50)/100,0.0624f,0.0849f);
        if(range > startBreathing){
            audio.playMultiple(clip1,clip2,null,true);
            audio.getAudioSource(0).volume = 0.2f;
        }
      }else{
        audio.getAudioSource(0).loop = false;
        audio.getAudioSource(1).loop = false;
        firstColor.r = 0.2f;
        RenderSettings.ambientLight = new Color(0.0460f,0.0642f,0.0849f);
      }
      GetComponent<Light>().color = firstColor;
    }

    // Start is called before the first frame update
    void Start()
    {
    firstColor = GetComponent<Light>().color;  
    }

    // Update is called once per frame
    void Update()
    {
        if(!isEnd){
            range = Mathf.Lerp(range,response.worryMeter,Time.deltaTime/2f);
            changeColorScene();  
        }
  
    }

    public void endScene(){
        isEnd = true;
        StartCoroutine(endEvent());
    }
    public IEnumerator endEvent(){
         audio.playClip(clip,0,0.5f,false);
        yield return new WaitUntil(() => !audio.getAudioSource(0).isPlaying);
    }
    
}
