using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartGame()
    {
        // Scene 0 -> MainScene: Main Gameplay Scene
        // Scene 1 -> GameOver: Game Over Scene
        SceneManager.LoadScene(0);
    }
}
