using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    static SceneManagerScript instance;
    public int type;

    private void Awake()
    {
        if(instance=null)instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StageSelect()
    {
        if (type == 1)
        {
            SceneManager.LoadScene("JGYScene");
        }
        else if (type == 2)
        {
            SceneManager.LoadScene("TeamScene1");
        }
        else if (type == 3)
        {
            SceneManager.LoadScene("TeamScene2");
        }
    }
}
