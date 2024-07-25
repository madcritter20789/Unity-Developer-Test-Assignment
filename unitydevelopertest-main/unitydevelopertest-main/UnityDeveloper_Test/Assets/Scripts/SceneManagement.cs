using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public GameObject FailedPanel;

    //Load the Scene using string as parameter
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        Destroy(FailedPanel);
    }
}
