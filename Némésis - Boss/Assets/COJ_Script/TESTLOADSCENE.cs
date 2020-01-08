using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TESTLOADSCENE : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(load());
    }

    IEnumerator load()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
        
    }
}
