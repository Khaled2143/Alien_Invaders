using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _tripleShotPowerUpSpeed = 3.0f;
    [SerializeField]
    private float _SpeedPowerUp = 3.0f;

    //IUD for powerUps
    //0 = tripleShot
    //1 = speed
    //2 = shields
    [SerializeField]
    private int powerUpID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move down at a speed of 3
        transform.Translate(Vector3.down * _tripleShotPowerUpSpeed *Time.deltaTime);

        if(transform.position.y < -6f){
            Destroy(gameObject);
        }


        
        
    }
     

    //Ontriggercollison
    //Only be collectible by the player(hint :use tags)
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            //Communicate with the player script
            //Create a handle to the component I want 
            //Assign the handle to the component

            Player _player = other.transform.GetComponent<Player>();
            
            switch(powerUpID)
            {
                case 0:
                _player.tripleShotActive();
                break;
                case 1:
                _player.speedPowerUpActive();
                break;
                case 2:
                _player.shieldPowerUpActive();
                break;
            }
            Destroy(this.gameObject);
        }

        
    }


}
