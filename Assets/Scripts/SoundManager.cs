using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager S; // Singleton pattern

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource audioSource;

    public AudioClip brickHitSound;
    public AudioClip wallHitSound;
    public AudioClip paddleHitSound;

    public AudioClip gameOverSound;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("SoundManager.Awake() - Attempted to assign second SoundManager.S!");
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    public void PlayGameSound(string soundTag){
        switch (soundTag)
        {
            case "Brick":
                audioSource.PlayOneShot(brickHitSound);
                break;
            case "Boundaries":
                audioSource.PlayOneShot(wallHitSound);
                break;
            case "Paddle":
                audioSource.PlayOneShot(paddleHitSound);
                break;
            case "GameOver":
                audioSource.PlayOneShot(gameOverSound);
                break;
            default:
                Debug.Log("Sound not found");
                break;
        }
    }
}
