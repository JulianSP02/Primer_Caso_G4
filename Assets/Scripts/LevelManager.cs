using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    bool circularNavigation = true;

    /// <summary>
    /// Returns Current Scene Index.
    /// </summary>
    /// <returns>Current Scene Index.</returns>
    public int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public int GetLastScene()
    {
        return SceneManager.sceneCountInBuildSettings - 1;
    }

    /// <summary>
    /// Navigates to First Scene
    /// </summary>

    public void FirstScene()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Navigates to last scene
    /// </summary>

    public void LastScene()
    {
        SceneManager.LoadScene(GetLastScene());
    }

    /// <summary>
    /// Navigates to next scene from current one.
    /// </summary>

    public void NextScene()
    {
        // Almacena el valor del indice de la escena actual
        int currentScene = GetCurrentScene();

        // Almacena el valor del indice de la ultima escena
        int lastScene = GetLastScene();

        // Si la escena actual NO ES LA ULTIMA escena
        if (currentScene < lastScene)
        {
            // Cargue la siguiente escena
            SceneManager.LoadScene(currentScene + 1);
        }
        // Si esta permitido navegar circularmente
        else if (circularNavigation)
        {
            // Cargue la primera escena
            FirstScene();
        }
    }

    /// <summary>
    /// Navigates to previous scene from current one.
    /// </summary>

    public void PreviousScene()
    {
        int currentScene = GetCurrentScene();

        if (currentScene > 0)
        {
            SceneManager.LoadScene(currentScene - 1);
        }
    }
}

