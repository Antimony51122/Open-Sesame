using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public void LoadGameBothScene() {
        SceneManager.LoadScene(2);
    }

    public void LoadGameRightScene() {
        SceneManager.LoadScene(4);
    }

    public void LoadGameLeftScene() {
        SceneManager.LoadScene(3);
    }

    public void LoadCalibrateScene() {
        SceneManager.LoadScene(1);
    }

    public void LoadMenuScene() {
        SceneManager.LoadScene(0);
    }

    public void LoadReloadScene() {
        SceneManager.LoadScene(5);
    }
}
