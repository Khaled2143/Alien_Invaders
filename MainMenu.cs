using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void loadGame(){
        //Load the game scene
        SceneManager.LoadScene(1);
    }
}
