using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriod : MonoBehaviour
{
    private Animator _anim;
    //[SerializeField]
    private float _rockSpeed = 45f;
    [SerializeField]
    private GameObject _rockExplosionPreFab;
    // Start is called before the first frame update
    private spawnManager _spawnManager;
    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<spawnManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Roatate object on z axis 
        transform.Rotate(Vector3.forward * _rockSpeed * Time.deltaTime);
    }

    //Check for laser collison of type trigger
    //INstantiate explosion at the positin of the asteroid 
    //DEstroy the explosion after 3 seconds
     private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Laser"){
            Instantiate(_rockExplosionPreFab,transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.startSpawning();
            Destroy(this.gameObject);

        }
    }
}

