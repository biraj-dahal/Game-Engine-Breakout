using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ballscript : MonoBehaviour
{
    public float ballSpeed = 1.0f;

    public Text ScoreVal;

    private int score = 0;

    private SoundManager mySoundManager;  // Fixed type from GameObject to SoundManager

    public Text label;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Correctly get the SoundManager component
        mySoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        // get the Rigidbody component
        Rigidbody rb = GetComponent<Rigidbody>();

        // set the direction
        Vector3 direction = new Vector3(5.0f, 4.0f, 0.0f);
        direction.Normalize();

        // set the velocity
        rb.linearVelocity = direction * ballSpeed; 
        ScoreVal.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        mySoundManager.PlayGameSound(collision.gameObject.tag); 
        if (collision.gameObject.tag == "Brick")
        {
            Destroy(collision.gameObject);
            score += collision.gameObject.GetComponent<BrickScript>().brickValue;
            ScoreVal.text = score.ToString();
        }
    
    }
}
