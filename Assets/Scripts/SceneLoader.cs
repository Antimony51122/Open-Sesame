using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public void LoadGameScene() {
        SceneManager.LoadScene(2);
    }

    public void LoadCalibrateScene() {
        SceneManager.LoadScene(1);
    }

    public void LoadMenuScene() {
        SceneManager.LoadScene(0);
    }
}
