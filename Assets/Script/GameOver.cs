using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;

    private void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }
    
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        // used to restart the level, can also do SceneManager.LoadScene("nameofscene");
        // but if you change scene names it might be a problem. can also do index 0 since we only have this scene but if we add another scene
        // before this scene, then it will mess the code up. This is a better solution to keep things nice and clean without changing anything.
    }

    public void Menu()
    {
        Debug.Log("Go to Menu.");
    }
}
