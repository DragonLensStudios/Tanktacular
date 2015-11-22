namespace DLS.Games.Tanktacular
{
    using UnityEngine;
    using UnityEngine.UI;
    using Utility; //DLS.Utility
    
    public class OptionsItemToggle : MonoBehaviour
    {
        public string Name;
        public bool Enabled;
        public GameEvent EnabledEvent;
        public GameEvent DisabledEvent;

        private Text optionText;
        private Switch optionSwitch;

        void Awake()
        {
            optionText = GetComponentInChildren<Text>();
            optionSwitch = GetComponentInChildren<Switch>();
        }

        void Start()
        {
            optionText.text = Name;
            optionSwitch.isOn = Enabled;
            InvokeEvents();
        }

        void Update()
        {
        }

        public void ToggleOption()
        {
            Enabled = !Enabled;
            InvokeEvents();
        }

        void InvokeEvents()
        {
            if (Enabled && EnabledEvent != null)
            {
                EnabledEvent.Invoke();
                Debug.Log("Enabled");
            }

            if (Enabled == false && DisabledEvent != null)
            {
                DisabledEvent.Invoke();
                Debug.Log("Disabled");
            }
        }
    }
}

