using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    //This class should persist throughout the entire game

    public static GameManager instance = null;
    
    //this should be a list of allyID's
    //that the player selected in some manner
    //the match manager will use this.
    public static List<string> SelectedAllies = null;

    void Awake()
    {        

        if( instance == null)
        {

            instance = this;
            SelectedAllies = new List<string>();
        }
        else if( instance != this)
        {
            Destroy(gameObject);
        }

        SelectedAllies = new List<string>();
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
