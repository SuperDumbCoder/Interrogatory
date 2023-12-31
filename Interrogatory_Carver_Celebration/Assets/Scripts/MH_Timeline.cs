using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class MH_Timeline : MonoBehaviour
{
    //this way we can make scenes, and set up questions along with flashbacks
    [Serializable]
    public class scene {
        public string name;
        public UnityEvent action;
        public bool needsInput;
        public UnityEvent fadeOut;
        public AudioClip recording;
        public float time;
       
    }

 
    public List<scene> scenes = new List<scene>();

    [SerializeField]
    private MH_AudioSourcer source;

    [SerializeField]
    private string buttonTagName;

    public int progression = 0;

    bool inputDetected, start;
    scene obj = null;
    // Start is called before the first frame update
    void Start()
    {
     
     
    }

   IEnumerator gameState(){
        //iterates scenes until the end, allow for questions to play 
       for(int currentScene = progression; currentScene < scenes.Count -1; currentScene++){
       obj = scenes[currentScene];

           if(scenes[currentScene].action != null)
        scenes[currentScene].action.Invoke();

        if(scenes[currentScene].needsInput)
            yield return new WaitUntil(() => inputDetected);
        inputDetected = false;
 
        if(GameObject.FindGameObjectWithTag(buttonTagName) != null)
            Debug.Log("cup exists");
           
        yield return new WaitForSeconds(scenes[currentScene].time);

        yield return new WaitForSeconds(0.25f);
        progression = currentScene;
        }
        
        scenes[scenes.Count - 1].action.Invoke();
        yield return new WaitUntil(() => !source.getAudioSource(0).isPlaying);
        source.playClip(scenes[scenes.Count - 1].recording,0,0.7f,false);
        yield return new WaitForSeconds(scenes[scenes.Count - 1].time);
        scenes[scenes.Count - 1].fadeOut.Invoke();
        
        
    }

    public void isInput(){
      inputDetected = true;
      obj.fadeOut.Invoke();
    }

    public void restartGame(){
        Destroy(GameObject.Find("gameData"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        if(start == false){
       StartCoroutine(gameState());  
          start = true;
        }
    }
}