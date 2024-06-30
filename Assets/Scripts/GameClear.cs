using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(OnClear), 2.0f);
    }
    private void OnClear()
    {
        SceneManager.LoadScene("GameClear");
    }
}
