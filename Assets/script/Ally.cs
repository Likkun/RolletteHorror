using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AllyData
{
    public string ID {get;set;}
}

public class Ally : MonoBehaviour {

    //public Dictionary<string, AllyData> AllyList = new Dictionary<string, AllyData>()
    //{
    //    {"1a", new AllyData { ID="1a" } },
    //};


    /// <summary>
    ///This class will contain definitions and methods for allies. 
    ///How much HP they have
    ///Their Spell sets
    ///etc
    ///When created they must be given an ID which they will use the gather the correct data.
    /// </summary>

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
