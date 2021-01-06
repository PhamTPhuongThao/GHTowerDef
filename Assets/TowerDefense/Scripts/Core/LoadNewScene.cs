using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadNewScene : MonoBehaviour
{
    public string playAgain;

    public void PlayAgain()
    {
        SceneManager.LoadScene(playAgain);
    }

}
