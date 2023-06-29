using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    public Label score; float sc;
    public Label level; float le;

    public Button mainmenu; public bool GAov;
    public Label gameover;

    public bool pause = false;

    public GameObject dogo;
    //public Dogo dog;
    

    public void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        score = root.Q("sc") as Label;
        level = root.Q("le") as Label;

        gameover = root.Q("GameOver") as Label; 
        mainmenu = root.Q("MainMenu") as Button;

    }

    void Start()
    {
        mainmenu.clickable.clicked += mainbuttonclick;
    }

   
    void Update()
    {
        ScoreLevelSetter();
        GameOverScreen();
        gamepause();
    }


    public void ScoreLevelSetter()
    {
        //score ---
        sc = dogo.GetComponent<Dogo>().highestZpos;
        //sc = dog.highestZpos;
        sc = Mathf.RoundToInt(sc);
        score.text = sc.ToString();
        //------

        //level ---
        le = dogo.GetComponent<Dogo>().levelUI;
        le = Mathf.RoundToInt(le);
        level.text = le.ToString();
        //------
    }

    public void GameOverScreen()
    {
        GAov = dogo.GetComponent<Dogo>().GameOOver;
        if (GAov == true)
        {
            gameover.visible = true;
            mainmenu.visible = true;
                      
        }
    }

    public void mainbuttonclick()
    {
        //Debug.Log(sc);
        SceneManager.LoadScene("mainmenu");

    }

    public void gamepause()
    {
        //we can also check GAov to to be true or false in update method with if condition on gamepause(); > if(GAov == false){gamepause();}
        if(Input.GetKeyDown(KeyCode.Escape) && (GAov == false) && (pause == false))
        {
            mainmenu.visible = true;
            Time.timeScale = 0f;
            pause = true;
            return;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && (GAov == false) && (pause == true))
        {
            mainmenu.visible = false;
            Time.timeScale = 1f;
            pause = false;
        }

    }
}
