using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MoveCube : MonoBehaviour
{
    public Rigidbody rb;
    public bool game_end;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        game_end = false;
    }

    void Update()
    {
        if (game_end == false)
        {
           if (Input.GetMouseButtonDown(0))
           {
        	rb.AddForce(transform.right * -100f);
        	Debug.Log("mouse 0");
           }
           if (Input.GetMouseButtonDown(1))
           {
        	rb.AddForce(transform.right * 100f);
        	Debug.Log("mouse 1");
           }
        }
        	
        if (transform.position.x >= 3.3)
        {
           Debug.Log("Victory for blue");
           game_end = true;
           transform.position = new Vector3(3.3f,1f,-6f);
        }
        if (transform.position.x <= -3.3)
        {
           Debug.Log("Victory for red");
           game_end = true;
           transform.position = new Vector3(-3.3f,1f,-6f);
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
    }
}
