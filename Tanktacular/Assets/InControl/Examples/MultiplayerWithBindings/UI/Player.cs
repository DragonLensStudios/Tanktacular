using System;
using System.Collections.Generic;
using System.Linq;
using DLS.Utility;
using UnityEngine;
using InControl;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace MultiplayerWithBindingsExampleUI
{
	// This is just a simple "player" script that rotates and colors a cube
	// based on input read from the actions field.
	//
	// See comments in PlayerManager.cs for more details.
	//
	public class Player : MonoBehaviour
	{
	    public PlayerManager Manager;
	    public InputDevice Inputdevice;
		public PlayerActions Actions { get; set; }
	    public string PlayerName = "Player 1";
	    public int PlayerNumber = 1;
	    public float Cursorsensitivity;
	    public GameObject CharacterPrefab;
	    public CharacterPanelScript CharacterPanel;

	    void Awake()
	    {
	        Manager = FindObjectOfType<PlayerManager>();
	    }

		void OnDisable()
		{
			if (Actions != null)
			{
				Actions.Destroy();
			}
		}

        void OnEnable()
        {
        }


        void Start()
		{

        }


		void Update()
		{
            PlayerName = "Player " + (PlayerNumber);
            gameObject.name = PlayerName;
            GetComponentInChildren<Text>().text = (PlayerNumber).ToString();
            if (Actions == null)
			{
				// If no controller exists for this cube, just make it translucent.
			}

            if (Actions.Up.IsPressed)
            {
                transform.position += Vector3.up * Time.deltaTime * Cursorsensitivity;
            }

            if (Actions.Down.IsPressed)
            {
                transform.position += Vector3.down * Time.deltaTime * Cursorsensitivity;
            }

            if (Actions.Left.IsPressed)
            {
                transform.position += Vector3.left * Time.deltaTime * Cursorsensitivity;
            }
            if (Actions.Right.IsPressed)
            {
                transform.position += Vector3.right * Time.deltaTime * Cursorsensitivity;
            }

		    if (Actions.Accept.WasPressed)
		    {
		        for (int i = 0; i < GetItemsClicked().Count; i++)
		        {
		            var go = GetItemsClicked()[i].gameObject;
		            if (go.GetComponent<CharacterButtonScript>() != null)
		            {
                        var cbs = go.GetComponent<CharacterButtonScript>();
                        var pcs = cbs.m_CharacterScript;
                        if (pcs.Unlocked)
                        {
                            CharacterPrefab = cbs.m_CharacterPrefab;
                            Debug.Log(name + " Selected " + pcs.Name);
                            Debug.Log("Start Health: " + pcs.TankHealth.m_StartingHealth);
                            Debug.Log("Tank Speed: " + pcs.TankMovement.m_Speed);
                            Debug.Log("Tank Max Launch Force: " + pcs.TankShooting.m_MaxLaunchForce);
                        }
                        else
                        {
                            Debug.Log("Character Locked!");
                        }
                    }
		        }
		    }

            if (Actions.Cancel.WasPressed)
            {
                var item2 = UIRaycast.CastAll(transform.position, true, gameObject);
//                var item = UIRaycast.GetFirstGameobject(new Vector3(300, 300));
//                Debug.Log(item.name);
                for (int i = 0; i < item2.Count; i++)
                {
                    if (item2[i].gameObject != null)
                    {
                        Debug.Log(item2[i].gameObject.name);
                    }

                }
                //                if (UIRaycast.GetFirstGameobject(gameObject))
                //                {
                //                    var item = UIRaycast.GetFirstGameobject(gameObject);
                //                    Debug.Log(item.name);
                //                }
                //                if (UIRaycast.GetListGameobjects(gameObject) != null)
                //                {
                //                    var items = UIRaycast.GetListGameobjects(gameObject);
                //                    for (int i = 0; i < items.Count; i++)
                //                    {
                //                        Debug.Log("Object found: " + items[i].name);
                //                    }
                //                }

                //                if(UIRaycast.GetFirstComponent(typeof(Image),gameObject))
                //                {
                //                    var item = UIRaycast.GetFirstComponent(typeof (Image), gameObject);
                //                    Debug.Log("Cast Found: " + item.gameObject.name);
                //                }


                //                if (UIRaycast.GetListComponents(gameObject,typeof(Image),typeof(Text)) != null)
                //                {
                //                    var items = UIRaycast.GetListComponents(gameObject, typeof(Image), typeof(Text));
                //                    for (int i = 0; i < items.Count; i++)
                //                    {
                //                        Debug.Log("Item " + i + ": " + items[i].gameObject.name + " Component Type: " + items[i].GetType());
                //                    }
                //                }


                //                if (GetFirstComponent(typeof(CharacterButtonScript)))
                //                {
                //                    var clicked = (CharacterButtonScript)GetFirstComponent(typeof (CharacterButtonScript));
                //                    var pcs = clicked.m_CharacterPrefab.GetComponent<PlayableCharacterScript>();
                //                    Debug.Log("Unlocked? "  + pcs.Unlocked);
                //                }

                //                var cps = GetFirstComponent(typeof(CharacterButtonScript));

                //                var cpsl = GetListComponents(typeof (Button));
                //
                //                if (cpsl != null)
                //                {
                //                    for (int i = 0; i < cpsl.Count; i++)
                //                    {
                //                        Debug.Log("Cpsl Object Name: " + cpsl[i].gameObject.name);
                //                    }
                //                }

                //                if (cps != null)
                //                {
                ////                    Debug.Log("Returned Value: " + cps.name);
                //                }

                //                Debug.Log("Cancel Was Pressed!");
            }
		}

        /// <summary>
        /// Add to Utility class later
        /// </summary>
        /// <returns></returns>
        public List<RaycastResult> GetItemsClicked()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = transform.position;
            List<RaycastResult> Result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, Result);

            return Result;
        }

        /// <summary>
        /// Add to Utility Class later
        /// </summary>
        /// <param name="_objectTransform"></param>
        /// <returns></returns>
        public List<RaycastResult> GetItemsClicked(Transform _objectTransform)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = _objectTransform.position;
            List<RaycastResult> Result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, Result);
            return Result;
        }

	    public Component GetFirstItemClicked(Type _type)
	    {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = transform.position;
            List<RaycastResult> Result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, Result);

	        if (Result.Any(x => x.gameObject.GetComponent(_type)))
	        {
	            var compres = Result.FindAll(x => x.gameObject.GetComponent(_type));

	            for (int i = 0; i < compres.Count; i++)
	            {
	                if (compres[i].gameObject.transform.parent.gameObject == gameObject)
	                {
	                    continue;
	                }

                    return compres[i].gameObject.GetComponent(_type);
	            }
            }

            return null;
        }

        public List<Component> GetAllItemsClicked(Type _type)
        {
            List<Component> CompList = new List<Component>();
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = transform.position;
            List<RaycastResult> Result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, Result);

            if (Result.Any(x => x.gameObject.GetComponent(_type)))
            {
                var compres = Result.FindAll(x => x.gameObject.GetComponent(_type));

                for (int i = 0; i < compres.Count; i++)
                {
                    if (compres[i].gameObject.transform.parent.gameObject == gameObject)
                    {
                        continue;
                    }
                    CompList.Add(compres[i].gameObject.GetComponent(_type));
                }
                return CompList;
            }

            return null;
        }

        public void Reset()
	    {
           CharacterPrefab = null;
           CharacterPanel.Reset();
	       Actions = null;
	    }

    }
}

