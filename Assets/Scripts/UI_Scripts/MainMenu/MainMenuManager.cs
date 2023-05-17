using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public void OnPlay(){
        SceneManager.LoadScene("SampleScene");
    }
    public void OnSettings(){

    }
    public void OnExit(){
        Application.Quit();
    }
}
