using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSc(string sceneName)
    {
        // Cargar la nueva escena
        SceneManager.LoadScene(sceneName);
    }
    public void DestroyCurrentScene()
    {
        // Obtiene la escena activa (actual)
        Scene currentScene = SceneManager.GetActiveScene();
        
        // Descarga la escena de forma asíncrona
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
