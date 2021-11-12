
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageEnd : MonoBehaviour
{
    public string scenename;
    public Stage stage;
    public void GotoStage(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        stage.UpdateEventTriggers();
        GotoStage(scenename);
    }
}
