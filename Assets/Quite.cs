using UnityEngine;

public class Quite : MonoBehaviour
{
    public void QuitGame()
    {
            #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; // ��������� � ���������
            #else
                        Application.Quit(); // �������� �����
            #endif
                }
}
