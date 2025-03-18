using UnityEngine;

public class PaddleScript : MonoBehaviour

{
public float speed = 7.6f;
public float offset = 7.6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curPosition = transform.position;
        
        // is the key pressed?  
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // move the paddle left
            curPosition.x += (-speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // move the paddle right
            curPosition.x += (speed * Time.deltaTime);
            
        }

        curPosition.x = Mathf.Clamp(curPosition.x, -offset, offset);
        transform.position = curPosition;


        
    }
}
