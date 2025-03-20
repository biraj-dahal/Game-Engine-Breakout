using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ballscript : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        SoundManager.S.PlayGameSound(collision.gameObject.tag);
        if (collision.gameObject.tag == "GameOver" && GameManager.S.curState == GameState.Playing)
        {
           GameManager.S.BallLeftPlay();
        }
    
    }
}
