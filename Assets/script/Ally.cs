using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class AllyData
{
    public string ID {get;set;}
}

public class Ally : MonoBehaviour {

    public Image allyImage;
    public Text allyName;
    public Text allySpellText;

    public int placerHolderNum;

    protected spell AllySpell;

    //public Dictionary<string, Dictionary<string, string>> allyLookup;
    public static Dictionary<string, allyData> allyLookup;

    protected class allyEntry
    {
        string index;
        AllyData data;
    }
    public class allyData
    {
        public string name { get; set; }
        public string spell1 { get; set; }
        public string spell1_text { get; set; }
        public string HP { get; set; }
        public string thumbnail { get; set; }
        public string index { get; set; }
    }

    public void Awake()
    {
        //allyLookup = new Dictionary<string, Dictionary<string, string>>();
        if (allyLookup == null)
        {
            allyLookup = new Dictionary<string, allyData>();
        }
        else
        {
            Debug.LogWarning("allyLookup already created, returning from Awake()");
            return;
        }

        List<allyEntry> entires = new List<allyEntry>();

        TextAsset txtAsset = Resources.Load<TextAsset>("AllyData");
       
        List<allyData> ad = Newtonsoft.Json.JsonConvert.DeserializeObject<List<allyData>>(txtAsset.text);        
        ///TODO: Add check for indexes used multiple times...        
        foreach( allyData a in ad)
        {            
            allyLookup.Add(a.index, a);
        }
    }

    public Ally( string id)
    {
        //if given an ID we should look through the ally dicitonay
        //to get the related ally data

        if(allyLookup[id] != null)
        {
            allyImage = Resources.Load<Image>(allyLookup[id].thumbnail);
            allyName.text = allyLookup[id].name;
        }

    }

    public bool setAlly( string index)
    {

        //look for index in dictionary 
        //then set up stats and image for this ally
        if (allyLookup[index] != null)
        {            
            Sprite newSprite = Resources.Load<Sprite>(allyLookup[index].thumbnail);
            allyImage.sprite = newSprite;
            allyName.text = allyLookup[index].name;
            return true;
        }
        
        return false;
    }
   

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
