using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MH_SetScene : MonoBehaviour
{
    public MH_SaveData sd;
    public string t;

    public void sceneLoad(){
        if(sd != null)
            sd.convertToData();
        SceneManager.LoadScene(t);
    }
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Flashback1")
            sceneLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
