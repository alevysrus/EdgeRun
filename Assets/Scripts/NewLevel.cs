using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevel : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        Activators.level++;
        SceneManager.LoadScene(Activators.level);
        PlayerPrefs.SetInt("level", Activators.level);
        PlayerPrefs.Save();
    }
}
