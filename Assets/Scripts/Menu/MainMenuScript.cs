using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{   private RaycastHit rayHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
        Cursor.visible = false;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Settings()
    {

    }


}
