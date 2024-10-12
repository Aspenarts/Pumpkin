using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] Image timerImage;
    [SerializeField] float duration;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = duration; 
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown() 
    {
        while(currentTime >= 0) 
        {
            timerImage.fillAmount = Mathf.InverseLerp(0, duration, currentTime);
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        GameOver();
    }    

// Scene 0 -> MainScene: Main Gameplay Scene
// Scene 1 -> GameOver: Game Over Scene
    void GameOver()
    {
        SceneManager.LoadScene(1);
    }
}
