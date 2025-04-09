using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelector : MonoBehaviour
{
    public void SetMode(string mode)
    {
        if (GameSettings.Instance == null) return;

        switch (mode)
        {
            case "Easy":
                GameSettings.Instance.SelectedMode = GameSettings.GameMode.Easy;
                break;
            case "Hard":
                GameSettings.Instance.SelectedMode = GameSettings.GameMode.Hard;
                break;
            case "Classic":
                GameSettings.Instance.SelectedMode = GameSettings.GameMode.Classic;
                break;
        }

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
