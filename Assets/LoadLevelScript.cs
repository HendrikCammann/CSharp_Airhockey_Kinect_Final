using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelScript : MonoBehaviour {

    public void NextLevelButton(string levelName)
    {

        SceneManager.LoadScene("Hockey");
    }

}
