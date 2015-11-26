using InControl;


namespace MultiplayerWithBindingsExampleUI
{
	public class PlayerActions : PlayerActionSet
	{
		public PlayerAction Accept;
		public PlayerAction Cancel;
		public PlayerAction Start;
	    public PlayerAction Select;
        public PlayerAction Up;
        public PlayerAction Down;
        public PlayerAction Left;
        public PlayerAction Right;

//        public PlayerTwoAxisAction Move;


		public PlayerActions()
		{
		    Accept = CreatePlayerAction("Accept");
		    Cancel = CreatePlayerAction("Cancel");
		    Start = CreatePlayerAction("Start");
		    Select = CreatePlayerAction("Select");
            Up = CreatePlayerAction("Up");
            Down = CreatePlayerAction("Down");
            Left = CreatePlayerAction("Left");
            Right = CreatePlayerAction("Right");
		}


		public static PlayerActions CreateWithKeyboardBindings()
		{
			var actions = new PlayerActions();

			actions.Accept.AddDefaultBinding( Key.Z );
			actions.Cancel.AddDefaultBinding( Key.X );
			actions.Start.AddDefaultBinding( Key.Return );
            actions.Select.AddDefaultBinding(Key.Escape);


            actions.Up.AddDefaultBinding( Key.UpArrow );
			actions.Down.AddDefaultBinding( Key.DownArrow );
			actions.Left.AddDefaultBinding( Key.LeftArrow );
			actions.Right.AddDefaultBinding( Key.RightArrow );

			return actions;
		}


		public static PlayerActions CreateWithJoystickBindings()
		{
			var actions = new PlayerActions();

			actions.Accept.AddDefaultBinding( InputControlType.Action1 );
			actions.Cancel.AddDefaultBinding( InputControlType.Action2 );
			actions.Start.AddDefaultBinding( InputControlType.Start );
			actions.Select.AddDefaultBinding( InputControlType.Back );
            actions.Select.AddDefaultBinding(InputControlType.Select);


            actions.Up.AddDefaultBinding( InputControlType.LeftStickUp );
			actions.Down.AddDefaultBinding( InputControlType.LeftStickDown );
			actions.Left.AddDefaultBinding( InputControlType.LeftStickLeft );
			actions.Right.AddDefaultBinding( InputControlType.LeftStickRight );

			actions.Up.AddDefaultBinding( InputControlType.DPadUp );
			actions.Down.AddDefaultBinding( InputControlType.DPadDown );
			actions.Left.AddDefaultBinding( InputControlType.DPadLeft );
			actions.Right.AddDefaultBinding( InputControlType.DPadRight );

			return actions;
		}
	}
}

