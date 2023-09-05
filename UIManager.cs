using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //Handle to text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprite;
    [SerializeField]
    private Text _GameOverText;
    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;

    
    // Start is called before the first frame update
    void Start()
    {
        
       _scoreText.text = "Score: " + 0;
       _GameOverText.gameObject.SetActive(false);
       _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

       if(_gameManager == null){
        Debug.LogError("Game Manager is null");
       }
       
    }

    public void UpdateScore(int playerScore){
        _scoreText.text  = "Score: " + playerScore.ToString();
    }

    public void updateLives(int currentLives){
        
        _livesImg.sprite = _liveSprite[currentLives];

        if(currentLives == 0){
            GameOverSequence();
            
        }

    }

    void GameOverSequence(){
        _gameManager.GameOver();
        _GameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine());
            StartCoroutine(RestartTextFlickerRoutine());

    }
    IEnumerator GameOverFlickerRoutine(){
        while(true){
            _GameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.55f);
            _GameOverText.text = "";
            yield return new WaitForSeconds(0.55f);
        }
    }

     IEnumerator RestartTextFlickerRoutine(){
        while(true){
            _restartText.text = "PRESS " + "R" + " TO RESTART THE GAME";
            yield return new WaitForSeconds(0.55f);
            _restartText.text = "";
            yield return new WaitForSeconds(0.55f);
        }
     }
   
    }
    

