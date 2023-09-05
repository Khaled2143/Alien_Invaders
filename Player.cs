using System.Collections;
using System.Collections.Generic; //Libraries that give us access to coding just like java 
using UnityEngine;
//Colon stands for extends or inherits 
public class Player : MonoBehaviour  //Monobehaviour allows us to drag and drop scripts onto game objects to control them 
                                    //Gives us void start and update 
{
//If you have a private variable naming convention says to use a underscore infront of the name of variable to differentiate between varibles 
    [SerializeField] //Allows us to read and change private stuff in the inspector
    private float _speed = 3.5f;
    [SerializeField]
    private float _newSpeed = 10.5f;
    

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _shieldPreFab;
    [SerializeField]
    private GameObject _visualizeLeftEngine;
    [SerializeField]
    private GameObject _visualizeRightEngine;
    [SerializeField]
    private float _fireRate = 1.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _Score;
    private bool _GameOverText = false;
   
    private spawnManager _spawnManager;
    
    private bool _isTripleShotActive = false;
    [SerializeField]
    private GameObject _tripleShotPreFab;
    private bool _isSpeedPowerUpActive = false;
    private bool _isShieldPowerUpACtive = false;
    [SerializeField]
    private GameObject _speedPreFab;

    //Variable refrence to the shield visualizrer
    [SerializeField]
    private GameObject _shieldVisualizerPreFab;

    private UIManager _uiManager;
    [SerializeField]
    public AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserAudio;
   


    // Start is called before the first frame update
    void Start()
    {
        //Take the current posiiton = new position (0,0,0)
        //VEctor 3 defines all posiiton types in unity, anything that invovles posiitoning of a object in unity is reassigned through vector 3 and utulizes new keyword
        transform.position = new Vector3(0,-2,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<spawnManager>(); //find the object. get the component
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
         _audioSource = GetComponent <AudioSource>();

        if(_spawnManager == null){
            Debug.Log("Spawn Manager is null");
        }
        if(_uiManager == null){
            Debug.LogError("UI Manager is null");
                    }
         if(_audioSource == null){
            Debug.LogError("Audio source on the player is null");
                    } else{
                        _audioSource.clip = _laserAudio;
                    }

    }

    // Update is called once per frame
    void Update()
    {   
        speedUp();
        if(Input.GetKey(KeyCode.Space)&& Time.time > _canFire){
            fireLaser();
            }

    }

    void fireLaser(){
        Vector3 offset = new Vector3(0,1.05f,0);
          _canFire = Time.time + _fireRate;     
        Vector3 newOffSet = new Vector3(-1.18f,-.05f,0);
            

            //Instantiate 3 laser(Triple shot prefab)
            //If space key pressed
            //if tripleshot active is true, fire 3 lasers(triple shot prefab)
            //else fire 1 laser

            if(_isTripleShotActive == false ){
                Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
            }
            else {
                Instantiate(_tripleShotPreFab, transform.position +newOffSet, Quaternion.identity);
            }
            _audioSource.Play();
    }

    void speedUp(){
        if(_isSpeedPowerUpActive == false){
            calculateMovement();
        }
        else
        {
            speedMovement();
        }
    }
    void speedMovement(){
        //Input is in unity, drop down menu, multiple axises lke vertical and horizontal, but we want horizontal so it multiplies by horizontal value
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");




        //Keep things simple: Vector3(1,0,0) * Horizontal value * 3.5f * real time
        transform.Translate(Vector3.right * horizontalInput * _newSpeed * Time.deltaTime); //Didnt use "new vector" this time since "right" is a word unity already defined for us which would be
                                            //new vector3(1,0,0), so itll be constantly moving to the right 
                                            //Time.deltaTime does conversion process form frame rate dependent to real minutes seconds and hours .
                                            //Can be thought of as 1 second
                                            //Essentially whats happening is new Vector3(1,0,0) * 5 * real time - Whats inside parathesis is getting multipled by 5  
        transform.Translate(Vector3.up * verticalInput * _newSpeed * Time.deltaTime); 

        //Or we could have done this
        //transform.Translate(new Vector3(horizontalInput,verticalInput,0) * _speed * Time.deltaTime); <--- This is much more efficient

        //Or we could have done this
        //Vector3 drection = new Vector3(horizontalInputl, verticalInput, 0);
        //transform.Translate(direction * _speed * Time.deltaTime)

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.8f, 0),0); //Clamp y and theres a minimum and max for y

        if(transform.position.x > 11.6f){
            transform.position = new Vector3(-11,transform.position.y,0);
        }
        else if(transform.position.x < -11.6f){
            transform.position = new Vector3(11, transform.position.y,0);
        }
    }
    void calculateMovement(){

        //Input is in unity, drop down menu, multiple axises lke vertical and horizontal, but we want horizontal so it multiplies by horizontal value
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");




        //Keep things simple: Vector3(1,0,0) * Horizontal value * 3.5f * real time
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime); //Didnt use "new vector" this time since "right" is a word unity already defined for us which would be
                                            //new vector3(1,0,0), so itll be constantly moving to the right 
                                            //Time.deltaTime does conversion process form frame rate dependent to real minutes seconds and hours .
                                            //Can be thought of as 1 second
                                            //Essentially whats happening is new Vector3(1,0,0) * 5 * real time - Whats inside parathesis is getting multipled by 5  
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime); 

        //Or we could have done this
        //transform.Translate(new Vector3(horizontalInput,verticalInput,0) * _speed * Time.deltaTime); <--- This is much more efficient

        //Or we could have done this
        //Vector3 drection = new Vector3(horizontalInputl, verticalInput, 0);
        //transform.Translate(direction * _speed * Time.deltaTime)

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.8f, 0),0); //Clamp y and theres a minimum and max for y

        if(transform.position.x > 11.6f){
            transform.position = new Vector3(-11,transform.position.y,0);
        }
        else if(transform.position.x < -11.6f){
            transform.position = new Vector3(11, transform.position.y,0);
        }

    }
  
    public void Damage(){ //Public so enemy could communicate with it 
    if(_isShieldPowerUpACtive == true){
        _isShieldPowerUpACtive = false;
        _shieldVisualizerPreFab.SetActive(false);
        return;
    }
    _lives--;
    //If lives is 2
    //enable right engine 
    //If lives in 1
    //Enable left engine 

    if(_lives == 2){
        _visualizeLeftEngine.SetActive(true);

    } else if(_lives == 1){
        _visualizeLeftEngine.SetActive(true);
        _visualizeRightEngine.SetActive(true);
    }
    _uiManager.updateLives(_lives);
        if(_lives < 1){
            //Communicate with spawn Manager
            _spawnManager.onPlayerDeath();
            //Let them know to stop spawning 
            Destroy(this.gameObject);
        }
        }
    

    public void tripleShotActive(){
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }


    //Ienumerator TripleSHotPowerDOwnRoutine
    //Wait 5 seconds
    //Then set triple shot to false 
    IEnumerator TripleShotPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void speedPowerUpActive(){
        _isSpeedPowerUpActive = true;
        StartCoroutine(speedPowerDownRoutine());
    }

    IEnumerator speedPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        _isSpeedPowerUpActive = false;
    }

    public void shieldPowerUpActive(){
        _isShieldPowerUpACtive = true;
        //Enable visualizer 
        _shieldVisualizerPreFab.SetActive(true);
        
    }

    public void shieldPowerDown(){
        _lives = 4;
    }
//Method to add 100 to the score 
//Communicate with UI to update score 

public void updateScore(int points){
    _Score += points;
    _uiManager.UpdateScore(_Score);
}


}
    

