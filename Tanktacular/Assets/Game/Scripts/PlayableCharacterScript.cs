using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayableCharacterScript : MonoBehaviour
{
    public string Name;
    public Sprite Portrit;
    public bool Unlocked;

	// Use this for initialization
	void Start ()
    {
        if (Unlocked == false)
        {
            Name = "???";
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
