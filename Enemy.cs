using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4f;
    // Start is called before the first frame update
    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    void Start()
    {
        transform.position = new Vector3(Random.Range(-5,5),5,0);
         _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
         if(_player == null){
            Debug.LogError("Player is null");
         }

         //assign the component to anim
         _anim = GetComponent<Animator>();
         
         if(_anim == null){
            Debug.LogError("Animator is null");
         }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Mode down 4 meters per second 
       transform.Translate(Vector3.down  * _enemySpeed * Time.deltaTime);

        //If bottom o screen, respawn at top with a random x position

        float x = Random.Range(-6,6);
        if(transform.position.y < -6f ){
            transform.position = new Vector3(x,7,0);
        }
        
    }

   private void OnTriggerEnter2D(Collider2D other)
{
    //if other is player
    //damage the player
    //destroy enemy
    if (other.tag == "Player")
    {
        Player player = other.transform.GetComponent<Player>();

        if (player != null)
        {
            player.Damage();
        }
        //Trigger anim
        _anim.SetTrigger("OnEnemyDeath");
        _enemySpeed = 0;
        _audioSource.Play();
        Destroy(this.gameObject,2.3f);
        
    }

    //if other is laser
    //destroy laser
    //destroy us
    if (other.tag == "Laser")
    {
        Destroy(other.gameObject);

        if (_player != null)
        {
            _player.updateScore(100);
        }
        //trigger anim
        _anim.SetTrigger("OnEnemyDeath");
        _enemySpeed = 0;
        _audioSource.Play();
        Destroy(this.gameObject,2.3f);
    }
}
}

    

