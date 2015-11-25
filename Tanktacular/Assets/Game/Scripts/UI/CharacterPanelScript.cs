using MultiplayerWithBindingsExampleUI;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelScript : MonoBehaviour
{
    public bool PlayerJoined;
    public Player Player;
    public Sprite DefaultSprite;
    public string DefaultName = "";

    public GameObject JoinedGameobject;
    public GameObject NotjoinedGameobject;

    public Text PlayerNumberText;
    public Dropdown PlayerNameDropdown;
    public Image SelectedCharacterImage;
    public Text SelectedCharacterName;
	
	// Update is called once per frame
	void Update ()
    {
	    if (Player != null)
	    {
	        PlayerJoined = true;
	        name = Player.name + " Panel";
            PlayerNumberText.text = Player.PlayerNumber.ToString();

            if (SelectedCharacterName != null && Player.CharacterPrefab != null)
            {
                SelectedCharacterName.text = Player.CharacterPrefab.GetComponent<PlayableCharacterScript>().Name;
            }
            if (SelectedCharacterImage != null && Player.CharacterPrefab != null)
            {
                SelectedCharacterImage.sprite = Player.CharacterPrefab.GetComponent<PlayableCharacterScript>().Portrit;
            }
        }
	    else
	    {
	        PlayerJoined = false;
        }

        if (PlayerJoined)
	    {
	        JoinedGameobject.SetActive(true);
            NotjoinedGameobject.SetActive(false);
        }
        else
	    {
            JoinedGameobject.SetActive(false);
            NotjoinedGameobject.SetActive(true);
        }
	}

    public void Reset()
    {
        SelectedCharacterName.text = DefaultName;
        SelectedCharacterImage.sprite = DefaultSprite;
        Player = null;
        PlayerJoined = false;
    }
}
