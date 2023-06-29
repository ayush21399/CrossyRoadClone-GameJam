using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Mainmenuui : MonoBehaviour
{
    public Button playgame;
    public Button exit;


    public void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        playgame = root.Q("playgame") as Button;
        exit = root.Q("exit") as Button;

    }

    void Start()
    {
        playgame.clickable.clicked += playgamebuttonclick;
        exit.clickable.clicked += exitbuttonclick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playgamebuttonclick()
    {
        //Debug.Log("playgame button click");
        SceneManager.LoadScene("SampleScene");
    }
    public void exitbuttonclick()
    {
        //Debug.Log("exit");
        Application.Quit();
    }
}
