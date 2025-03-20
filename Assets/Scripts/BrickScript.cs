using UnityEngine;

public class BrickScript : MonoBehaviour


{
    public int brickValue;
    public bool isStrong;
    public int hitsLeft;
    public int STRONG_HITS = 2;

    public int ScoreVal;

    private void Start()
    {
        if (isStrong)
        {
            hitsLeft = STRONG_HITS;
        }
    }


    private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.tag == "Ball")
    {
        if (isStrong)
        {
            if (hitsLeft > 0)
            {
                hitsLeft--;
            }
            else
            {
                GameManager.S.IncreaseScore(brickValue);
                GameManager.S._checkBricks = true; 
                Destroy(this.gameObject);
            }
        }
        else
        {
            GameManager.S.IncreaseScore(brickValue);
            GameManager.S._checkBricks = true;  
            Destroy(this.gameObject);
        }
    }
}

}
