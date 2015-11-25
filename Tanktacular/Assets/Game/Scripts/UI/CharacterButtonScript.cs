using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonScript : MonoBehaviour
{
    public GameObject m_CharacterPrefab;
    [HideInInspector]public PlayableCharacterScript m_CharacterScript;
    private Text m_CharacterNameText;
    private Image m_CharacterImage;

    void Awake()
    {
        m_CharacterScript = m_CharacterPrefab.GetComponent<PlayableCharacterScript>();
        m_CharacterNameText = GetComponentInChildren<Text>();
        m_CharacterImage = transform.GetChild(0).GetComponent<Image>();
        
    }

//    void OnEnable()
//    {
//        if (m_CharacterScript.Unlocked)
//        {
//            m_CharacterNameText.text = m_CharacterScript.Name;
//            m_CharacterImage.sprite = m_CharacterScript.Portrit;
//        }
//    }

    void Update()
    {
        if (m_CharacterScript.Unlocked)
        {
            m_CharacterNameText.text = m_CharacterScript.Name;
            m_CharacterImage.sprite = m_CharacterScript.Portrit;
        }
    }

}
