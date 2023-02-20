using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    static PauseMenuScript instance;
    public static bool isActive = false;
    public static PauseMenuScript GetInstance()
    {   
        return instance;
    }
    [SerializeField] GameObject myCanvas;
    private void Awake()
    {
        instance = this;
    }
    public void Pause()
    {
        Debug.Log("Pausing");
        Cursor.visible = true;
        //instance.gameObject.SetActive(true);
        myCanvas.SetActive(true);
        isActive = true;
        
    }
    
    // Start is called before the first frame update
    public void Continue() {
        //this.gameObject.SetActive(false);
        myCanvas.SetActive(false);
        Cursor.visible = false;
        isActive = false;
    }
    public  void Settings() { }
    public void MainMenu() { 
        SceneManager.UnloadScene(SceneManager.GetActiveScene());
        SceneManager.LoadScene("MainMenu");
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
