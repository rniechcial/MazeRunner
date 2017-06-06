using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {
    public GameObject loadingImage;

    public void LoadScene(string scene)
    {
        loadingImage.SetActive(true);
        SceneManager.LoadScene(scene);
    }
}
