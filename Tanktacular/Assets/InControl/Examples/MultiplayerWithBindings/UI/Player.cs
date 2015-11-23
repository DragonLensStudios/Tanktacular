using System;
using System.Collections.Generic;
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
	    public float Cursorsensitivity;

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

            PlayerName = "Player " + (Manager.players.Count + 1);
            gameObject.name = PlayerName;
            GetComponentInChildren<Text>().text = (Manager.players.Count + 1).ToString();
        }


        void Start()
		{

        }


		void Update()
		{
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
//		        GetItemsClicked();
		        foreach (RaycastResult item in GetItemsClicked())
		        {
		            if (item.gameObject.GetComponent<Button>())
		            {
		                var button = item.gameObject.GetComponent<Button>();
		                Debug.Log(item.gameObject.name);
		                button.onClick.Invoke();;
		            }
                }

//                Ray ray = Camera.main.ScreenPointToRay(transform.position);
//                Debug.Log("current pos: " + transform.position);
//                RaycastHit2D hit;
//                if (Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y),Vector2.zero))
//                {
////                    Quaternion q = Quaternion.FromToRotation(-transform.forward, hit.normal);
////                    Instantiate(inventoryControl.activeBomb, hit.point + hit.normal * 0.01f, q);
//                }


		        //                                var pointer = new PointerEventData(EventSystem.current);
		        //                                // convert to a 2D position
		        //                                pointer.position = Camera.main.WorldToScreenPoint(transform.position);
		        //                                var raycastResults = new List<RaycastResult>();
		        //                                EventSystem.current.RaycastAll(pointer, raycastResults);
		        //                                Debug.Log(raycastResults.Count);
		        //                                if (raycastResults.Count > 0)
		        //                                {
		        //                                    Debug.Log(raycastResults[0].gameObject.name);
		        //                                    // Do anything to the hit objects. Here, I simply disable the first one.
		        //                //                    raycastResults[0].gameObject.SetActive(false);
		        //                                }

		        //                GraphicRaycaster graphicraycaster = GetComponent<GraphicRaycaster>();
		        //                PointerEventData ped = new PointerEventData(EventSystem.current);
		        //		        ped.position = Camera.main.WorldToScreenPoint(transform.position);
		        //                List<RaycastResult> results = new List<RaycastResult>();
		        //                graphicraycaster.Raycast(ped,results);
		        //                Debug.Log(results[0].gameObject.name);

		        //                graphicraycaster.Raycast();
		        //                //		        Debug.Log();
		        //                var ray = Camera.main.ScreenPointToRay(transform.position);
		        //		        RaycastHit hit;
		        //		        if (Physics.Raycast(ray, out hit, 100))
		        //		        {
		        //		            ray.
		        //		        }
		    }

            if (Actions.Cancel.WasPressed)
		    {
//		        Debug.Log ((PlayerName == "Player 1") ? "YOU ARE THE MASTER PLAYER" : "YOU ARE Player 2");
		        Debug.Log("Cancel Was Pressed!");
		    }
		}

        private List<RaycastResult> GetItemsClicked()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.Reset();
            pointerData.position = transform.position;
            List<RaycastResult> Result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, Result);

            return Result;
        }


    }
}

