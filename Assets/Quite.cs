using UnityEngine;

public class Quite : MonoBehaviour
{
    public void QuitGame()
    {
            #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; // Остановка в редакторе
            #else
                        Application.Quit(); // Закрытие билда
            #endif
                }
}
