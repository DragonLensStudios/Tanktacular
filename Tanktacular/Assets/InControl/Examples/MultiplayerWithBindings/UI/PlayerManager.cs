using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using InControl;


namespace MultiplayerWithBindingsExampleUI
{
	// This example iterates on the basic multiplayer example by using action sets with
	// bindings to support both joystick and keyboard players. It would be a good idea
	// to understand the basic multiplayer example first before looking a this one.
	//
	public class PlayerManager : MonoBehaviour
	{
		public GameObject playerPrefab;
	    public GameObject canvas;
	    public GameObject playerpanels;

		const int maxPlayers = 4;


		public List<Player> players = new List<Player>( maxPlayers );

		PlayerActions keyboardListener;
		PlayerActions joystickListener;


		void OnEnable()
		{
			InputManager.OnDeviceDetached += OnDeviceDetached;
			keyboardListener = PlayerActions.CreateWithKeyboardBindings();
			joystickListener = PlayerActions.CreateWithJoystickBindings();
		}


		void OnDisable()
		{
			InputManager.OnDeviceDetached -= OnDeviceDetached;
			joystickListener.Destroy();
			keyboardListener.Destroy();
		}


		void Update()
		{
			if (JoinButtonWasPressedOnListener( joystickListener ))
			{
				var inputDevice = InputManager.ActiveDevice;

				if (ThereIsNoPlayerUsingJoystick( inputDevice ))
				{
					CreatePlayer( inputDevice );
				}
			}

			if (JoinButtonWasPressedOnListener( keyboardListener ))
			{
				if (ThereIsNoPlayerUsingKeyboard())
				{
					CreatePlayer( null );
				}
			}

//            if (ExitButtonWasPressedOnListener(joystickListener))
//            {
//                var inputDevice = InputManager.ActiveDevice;
//
//                var player = FindPlayerUsingJoystick(inputDevice);
//                if (player != null)
//                {
//                    RemovePlayer(player);
//                }
////                Debug.Log("Select Was Pressed on " + inputDevice.Name);
//
//            }
//
//            if (ExitButtonWasPressedOnListener(keyboardListener))
//            {
//                var player = FindPlayerUsingKeyboard();
//                if (player != null)
//                {
//                    RemovePlayer(player);
//                }
//            }

		    Player player = players.Find(x => x.Actions.Select.WasPressed);
		    if (player != null)
		    {
                RemovePlayer(player);
            }
            //		    if (players.Find(x => x.Actions.Cancel.WasPressed))
            //		    {
            //                player = 
            ////		        var player = players.Find(x => x.Actions.Cancel.WasPressed);
            //		        Debug.Log(player.name + " Pressed Cancel");
            //		    }
        }


		bool JoinButtonWasPressedOnListener( PlayerActions actions )
		{
			return actions.Accept.WasPressed || actions.Start.WasPressed;
		}

//        bool ExitButtonWasPressedOnListener(PlayerActions actions)
//        {
//            return actions.Select.WasPressed;
//        }


        Player FindPlayerUsingJoystick( InputDevice inputDevice )
		{
			var playerCount = players.Count;
			for (int i = 0; i < playerCount; i++)
			{
				var player = players[i];
				if (player.Actions.Device == inputDevice)
				{
					return player;
				}
			}

			return null;
		}


		bool ThereIsNoPlayerUsingJoystick( InputDevice inputDevice )
		{
			return FindPlayerUsingJoystick( inputDevice ) == null;
		}


		Player FindPlayerUsingKeyboard()
		{
			var playerCount = players.Count;
			for (int i = 0; i < playerCount; i++)
			{
				var player = players[i];
				if (player.Actions == keyboardListener)
				{
					return player;
				}
			}

			return null;
		}


		bool ThereIsNoPlayerUsingKeyboard()
		{
			return FindPlayerUsingKeyboard() == null;
		}


		void OnDeviceDetached( InputDevice inputDevice )
		{
			var player = FindPlayerUsingJoystick( inputDevice );
			if (player != null)
			{
				RemovePlayer( player );
			}
		}


		Player CreatePlayer( InputDevice inputDevice )
		{
			if (players.Count < maxPlayers)
			{
				var gameObject = (GameObject) Instantiate( playerPrefab, canvas.transform.position, Quaternion.identity);
                gameObject.transform.SetParent(canvas.GetComponent<RectTransform>().transform);
             
				var player = gameObject.GetComponent<Player>();

				if (inputDevice == null)
				{
					// We could create a new instance, but might as well reuse the one we have
					// and it lets us easily find the keyboard player.
					player.Actions = keyboardListener;
                    var charpanelscript = playerpanels.transform.GetChild(players.Count).gameObject.GetComponent<CharacterPanelScript>();
                    player.CharacterPanel = charpanelscript;
                    player.CharacterPanel.Reset();
                    charpanelscript.Player = player;
//                    player.CharacterPanel = playerpanels.transform.GetChild(players.Count).gameObject.GetComponent<CharacterPanelScript>();
				}
				else
				{
					// Create a new instance and specifically set it to listen to the
					// given input device (joystick).
					var actions = PlayerActions.CreateWithJoystickBindings();
					actions.Device = inputDevice;

					player.Actions = actions;
				    player.Inputdevice = inputDevice;
				    player.PlayerNumber = players.Count + 1;
                    var charpanelscript = playerpanels.transform.GetChild(players.Count).gameObject.GetComponent<CharacterPanelScript>();
				    player.CharacterPanel = charpanelscript;
                    player.CharacterPanel.Reset();
				    charpanelscript.Player = player;
				}

                players.Add( player );
                return player;
			}

			return null;
		}


		void RemovePlayer( Player player )
		{
			players.Remove( player );
			player.Reset();
			Destroy( player.gameObject );
		}


		void OnGUI()
		{
			const float h = 22.0f;
			var y = 10.0f;

			GUI.Label( new Rect( 10, y, 300, y + h ), "Active players: " + players.Count + "/" + maxPlayers );
			y += h;

			if (players.Count < maxPlayers)
			{
				GUI.Label( new Rect( 10, y, 300, y + h ), "Press a button or a/s/d/f key to join!" );
				y += h;
			}
		}
	}
}