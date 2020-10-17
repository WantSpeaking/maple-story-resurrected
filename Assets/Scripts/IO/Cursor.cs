﻿/*
#define USE_NX

//////////////////////////////////////////////////////////////////////////////////
//	This file is part of the continued Journey MMORPG client					//
//	Copyright (C) 2015-2019  Daniel Allendorf, Ryan Payton						//
//																				//
//	This program is free software: you can redistribute it and/or modify		//
//	it under the terms of the GNU Affero General Public License as published by	//
//	the Free Software Foundation, either version 3 of the License, or			//
//	(at your option) any later version.											//
//																				//
//	This program is distributed in the hope that it will be useful,				//
//	but WITHOUT ANY WARRANTY; without even the implied warranty of				//
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the				//
//	GNU Affero General Public License for more details.							//
//																				//
//	You should have received a copy of the GNU Affero General Public License	//
//	along with this program.  If not, see <https://www.gnu.org/licenses/>.		//
//////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////
//	This file is part of the continued Journey MMORPG client					//
//	Copyright (C) 2015-2019  Daniel Allendorf, Ryan Payton						//
//																				//
//	This program is free software: you can redistribute it and/or modify		//
//	it under the terms of the GNU Affero General Public License as published by	//
//	the Free Software Foundation, either version 3 of the License, or			//
//	(at your option) any later version.											//
//																				//
//	This program is distributed in the hope that it will be useful,				//
//	but WITHOUT ANY WARRANTY; without even the implied warranty of				//
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the				//
//	GNU Affero General Public License for more details.							//
//																				//
//	You should have received a copy of the GNU Affero General Public License	//
//	along with this program.  If not, see <https://www.gnu.org/licenses/>.		//
//////////////////////////////////////////////////////////////////////////////////


namespace ms
{
	// Class that represents the mouse cursor
	public class Cursor
	{
		// Maple cursor states that are linked to the cursor's animation
		public enum State
		{
			IDLE,
			CANCLICK,
			GAME,
			HOUSE,
			CANCLICK2,
			CANGRAB,
			GIFT,
			VSCROLL,
			HSCROLL,
			VSCROLLIDLE,
			HSCROLLIDLE,
			GRABBING,
			CLICKING,
			RCLICK,
			LEAF = 18,
			CHATBARVDRAG = 67,
			CHATBARHDRAG,
			CHATBARBLTRDRAG,
			CHATBARMOVE = 72,
			CHATBARBRTLDRAG,
			LENGTH
		}

		public Cursor()
		{
			state = Cursor.State.IDLE;
			hide_counter = 0;
		}

		public void init()
		{
			nl.node src = nl.nx.ui["Basic.img"]["Cursor"];

			foreach (var iter in animations)
			{
				iter.second = src[iter.first];
			}
		}

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void draw(float alpha) const
		public void draw(float alpha)
		{
			const long HIDE_AFTER = HIDE_TIME / Constants.GlobalMembers.TIMESTEP;

			if (hide_counter < HIDE_AFTER)
			{
				animations[(int)state].draw(position, alpha);
			}
		}
		public void update()
		{
			animations[(int)state].update();

			switch (state)
			{
			case Cursor.State.CANCLICK:
			case Cursor.State.CANCLICK2:
			case Cursor.State.CANGRAB:
			case Cursor.State.CLICKING:
			case Cursor.State.GRABBING:
				hide_counter = 0;
				break;
			default:
				hide_counter++;
				break;
			}
		}
		public void set_state(State s)
		{
			if (state != s)
			{
				state = s;

				animations[(int)state].reset();
				hide_counter = 0;
			}
		}
		public void set_position(Point<short> pos)
		{
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
//ORIGINAL LINE: position = pos;
			position.CopyFrom(pos);
			hide_counter = 0;
		}
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Cursor::State get_state() const
		public Cursor.State get_state()
		{
			return state;
		}
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Point<short> get_position() const
		public Point<short> get_position()
		{
			return position;
		}

		private EnumMap<State, Animation> animations = new EnumMap<State, Animation>();

		private State state;
		private Point<short> position = new Point<short>();
		private int hide_counter;

		private static const long HIDE_TIME = 15_000;
	}
}


#if USE_NX
#endif
*/