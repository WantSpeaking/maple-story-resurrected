﻿using System.Collections.Generic;
using Attributes;
using Helper;

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
	public class UI : Singleton<UI>
	{
		/*public void enable()
		{
			
		}
		public void send_key(int keycode, bool pressed)
		{
			if ((is_key_down[GLFW_KEY_LEFT_ALT] || is_key_down[GLFW_KEY_RIGHT_ALT]) && (is_key_down[GLFW_KEY_ENTER] || is_key_down[GLFW_KEY_KP_ENTER]))
			{
				Window.get().toggle_fullscreen();

				is_key_down[GLFW_KEY_LEFT_ALT] = false;
				is_key_down[GLFW_KEY_RIGHT_ALT] = false;
				is_key_down[GLFW_KEY_ENTER] = false;
				is_key_down[GLFW_KEY_KP_ENTER] = false;

				return;
			}

			if (is_key_down[keyboard.capslockcode()])
			{
				caps_lock_enabled = !caps_lock_enabled;
			}#2#

			if (focusedtextfield != null)
			{
				bool ctrl = is_key_down[keyboard.leftctrlcode()] || is_key_down[keyboard.rightctrlcode()];

				if (ctrl && pressed)
				{
					KeyAction.Id action = keyboard.get_ctrl_action(keycode);

					if (action == KeyAction.Id.COPY || action == KeyAction.Id.PASTE)
					{
						if (focusedtextfield.Dereference().can_copy_paste())
						{
							switch (action)
							{
								case KeyAction.Id.COPY:
									Window.get().setclipboard(focusedtextfield.Dereference().get_text());
									break;
								case KeyAction.Id.PASTE:
									focusedtextfield.Dereference().add_string(Window.get().getclipboard());
									break;
							}
						}
					}
				}
				else
				{
					bool shift = is_key_down[keyboard.leftshiftcode()] || is_key_down[keyboard.rightshiftcode()] || caps_lock_enabled;
					Keyboard.Mapping mapping = keyboard.get_text_mapping(keycode, shift);
					focusedtextfield.Dereference().send_key(mapping.type, mapping.action, pressed);
				}
			}
			else
			{
				Keyboard.Mapping mapping = keyboard.get_mapping(keycode);

				bool sent = false;
				LinkedList<UIElement.Type> types = new LinkedList<UIElement.Type>();

				bool escape = keycode == GLFW_KEY_ESCAPE;
				bool tab = keycode == GLFW_KEY_TAB;
				bool enter = keycode == GLFW_KEY_ENTER || keycode == GLFW_KEY_KP_ENTER;
				bool up_down = keycode == GLFW_KEY_UP || keycode == GLFW_KEY_DOWN;
				bool left_right = keycode == GLFW_KEY_LEFT || keycode == GLFW_KEY_RIGHT;
				bool arrows = up_down || left_right;

				var statusbar = UI.get().get_element<UIStatusBar>();
				var channel = UI.get().get_element<UIChannel>();
				var worldmap = UI.get().get_element<UIWorldMap>();
				var optionmenu = UI.get().get_element<UIOptionMenu>();
				var shop = UI.get().get_element<UIShop>();
				var joypad = UI.get().get_element<UIJoypad>();
				var rank = UI.get().get_element<UIRank>();
				var quit = UI.get().get_element<UIQuit>();
				var npctalk = UI.get().get_element<UINpcTalk>();
				//var report = UI::get().get_element<UIReport>();
				//var whisper = UI::get().get_element<UIWhisper>();

				if (npctalk != null && npctalk.Dereference().is_active())
				{
					npctalk.Dereference().send_key(mapping.action, pressed, escape);
					sent = true;
				}
				else if (statusbar != null && statusbar.Dereference().is_menu_active())
				{
					statusbar.Dereference().send_key(mapping.action, pressed, escape);
					sent = true;
				}
				else if (channel != null && channel.Dereference().is_active() && mapping.action != (int)KeyAction.Id.CHANGECHANNEL)
				{
					channel.Dereference().send_key(mapping.action, pressed, escape);
					sent = true;
				}
				else if (worldmap != null && worldmap.Dereference().is_active() && mapping.action != (int)KeyAction.Id.WORLDMAP)
				{
					worldmap.Dereference().send_key(mapping.action, pressed, escape);
					sent = true;
				}
				else if (optionmenu != null && optionmenu.Dereference().is_active())
				{
					optionmenu.Dereference().send_key(mapping.action, pressed, escape);
					sent = true;
				}
				else if (shop != null && shop.Dereference().is_active())
				{
					shop.Dereference().send_key(mapping.action, pressed, escape);
					sent = true;
				}
				else if (joypad != null && joypad.Dereference().is_active())
				{
					joypad.Dereference().send_key(mapping.action, pressed, escape);
					sent = true;
				}
				else if (rank != null && rank.Dereference().is_active())
				{
					rank.Dereference().send_key(mapping.action, pressed, escape);
					sent = true;
				}
				else if (quit != null && quit.Dereference().is_active())
				{
					quit.Dereference().send_key(mapping.action, pressed, escape);
					sent = true;
				}
				else
				{
					// All
					if (escape || tab || enter || arrows)
					{
						// Login
						types.Add(UIElement.Type.WORLDSELECT);
						types.Add(UIElement.Type.CHARSELECT);
						types.Add(UIElement.Type.RACESELECT); // No tab
						types.Add(UIElement.Type.CLASSCREATION); // No tab (No arrows, but shouldn't send else where)
						types.Add(UIElement.Type.LOGINNOTICE); // No tab (No arrows, but shouldn't send else where)
						types.Add(UIElement.Type.LOGINNOTICE_CONFIRM); // No tab (No arrows, but shouldn't send else where)
						types.Add(UIElement.Type.LOGINWAIT); // No tab (No arrows, but shouldn't send else where)
					}

					if (escape)
					{
						// Login
						types.Add(UIElement.Type.SOFTKEYBOARD);

						// Game
						types.Add(UIElement.Type.NOTICE);
						types.Add(UIElement.Type.KEYCONFIG);
						types.Add(UIElement.Type.CHAT);
						types.Add(UIElement.Type.EVENT);
						types.Add(UIElement.Type.STATSINFO);
						types.Add(UIElement.Type.ITEMINVENTORY);
						types.Add(UIElement.Type.EQUIPINVENTORY);
						types.Add(UIElement.Type.SKILLBOOK);
						types.Add(UIElement.Type.QUESTLOG);
						types.Add(UIElement.Type.USERLIST);
						types.Add(UIElement.Type.NPCTALK);
						types.Add(UIElement.Type.CHARINFO);
					}
					else if (enter)
					{
						// Login
						types.Add(UIElement.Type.SOFTKEYBOARD);

						// Game
						types.Add(UIElement.Type.NOTICE);
					}
					else if (tab)
					{
						// Game
						types.Add(UIElement.Type.ITEMINVENTORY);
						types.Add(UIElement.Type.EQUIPINVENTORY);
						types.Add(UIElement.Type.SKILLBOOK);
						types.Add(UIElement.Type.QUESTLOG);
						types.Add(UIElement.Type.USERLIST);
					}

					if (types.Count > 0)
					{
						var element = state.get_front(types);

						if (element && element != null)
						{
							element.send_key(mapping.action, pressed, escape);
							sent = true;
						}
					}
				}

				if (!sent)
				{
					var chatbar = UI.get().get_element<UIChatBar>();

					if (escape)
					{
						if (chatbar != null && chatbar.Dereference().is_chatopen())
						{
							chatbar.Dereference().send_key(mapping.action, pressed, escape);
						}
						else
						{
							state.send_key(mapping.type, mapping.action, pressed, escape);
						}
					}
					else if (enter)
					{
						if (chatbar != null)
						{
							chatbar.Dereference().send_key(mapping.action, pressed, escape);
						}
						else
						{
							state.send_key(mapping.type, mapping.action, pressed, escape);
						}
					}
					else
					{
						state.send_key(mapping.type, mapping.action, pressed, escape);
					}
				}
			}

			is_key_down[keycode] = pressed;#2#
			}
			//Keyboard.Mapping mapping = keyboard.get_mapping(keycode);
			//state.send_key(mapping.type, mapping.action, pressed, escape);
			state.send_key(KeyType.Id.ACTION, (int)KeyAction.Id.LEFT, pressed, false);
		}

		public void send_key (KeyType.Id type, int action,bool pressed)
		{
			state.send_key(type, action, pressed, false);
		}*/
		public enum State
		{
			LOGIN,
			GAME,
			CASHSHOP
		}

		public UI ()
		{
			state = new UIStateNull ();
			enabled = true;
		}

		public void init ()
		{
			cursor.init ();
			change_state (State.LOGIN);
		}

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void draw(float alpha) const
		public void draw (float alpha)
		{
			state.draw (alpha, cursor.get_position ());

			scrollingnotice.draw (alpha);

			cursor.draw (alpha);
		}

		public void update ()
		{
			state.update ();

			scrollingnotice.update ();

			cursor.update ();
		}

		public void enable ()
		{
			enabled = true;
		}

		public void disable ()
		{
			enabled = false;
		}

		//UIStateLogin _uiStateLogin = new UIStateLogin ();
		//UIStateGame _uiStateGame = new UIStateGame ();
		//todo UIStateCashShop _uiStateCashShop = new UIStateCashShop ();

		public void change_state (State id)
		{
			switch (id)
			{
				case State.LOGIN:
					state = new UIStateLogin ();
					break;
				case State.GAME:
					state = new UIStateGame ();
					break;
				case State.CASHSHOP:
					//todo state = _uiStateCashShop;
					break;
			}
		}

		public void quit ()
		{
			quitted = true;
		}

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool not_quitted() const
		public bool not_quitted ()
		{
			return !quitted;
		}

		public void send_cursor (Point<short> pos)
		{
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: send_cursor(pos, cursor.get_state());
			send_cursor (pos, cursor.get_state ());
		}

		public void send_cursor (bool pressed)
		{
			Cursor.State cursorstate = (pressed && enabled) ? Cursor.State.CLICKING : Cursor.State.IDLE;
			Point<short> cursorpos = cursor.get_position ();
			send_cursor (cursorpos, cursorstate);

			if (focusedtextfield && pressed)
			{
				Cursor.State tstate = focusedtextfield.get ().send_cursor (cursorpos, pressed);

				switch (tstate)
				{
					case Cursor.State.IDLE:
						focusedtextfield = new Optional<Textfield> ();
						break;
				}
			}
		}

		public void send_cursor (Point<short> cursorpos, Cursor.State cursorstate)
		{
			Cursor.State nextstate = state.send_cursor (cursorstate, cursorpos);
			cursor.set_state (nextstate);
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: cursor.set_position(cursorpos);
			cursor.set_position (cursorpos);
		}

		public void send_focus (int focused)
		{
			if (focused != 0)
			{
				// The window gained input focus
				byte sfxvolume = Setting<SFXVolume>.get ().load ();
				Sound.set_sfxvolume (sfxvolume);

				byte bgmvolume = Setting<BGMVolume>.get ().load ();
				Music.set_bgmvolume (bgmvolume);
			}
			else
			{
				// The window lost input focus
				Sound.set_sfxvolume (0);
				Music.set_bgmvolume (0);
			}
		}

		public void send_scroll (double yoffset)
		{
			state.send_scroll (yoffset);
		}

		public void send_close ()
		{
			state.send_close ();
		}

		public void rightclick ()
		{
			Point<short> pos = cursor.get_position ();
			state.rightclick (pos);
		}

		public void doubleclick ()
		{
			Point<short> pos = cursor.get_position ();
			state.doubleclick (pos);
		}

		public void send_key (int keycode, bool pressed)
		{
			/*todo if ((is_key_down[GLFW_KEY_LEFT_ALT] || is_key_down[GLFW_KEY_RIGHT_ALT]) && (is_key_down[GLFW_KEY_ENTER] || is_key_down[GLFW_KEY_KP_ENTER]))
			{
				Window.get().toggle_fullscreen();

				is_key_down[GLFW_KEY_LEFT_ALT] = false;
				is_key_down[GLFW_KEY_RIGHT_ALT] = false;
				is_key_down[GLFW_KEY_ENTER] = false;
				is_key_down[GLFW_KEY_KP_ENTER] = false;

				return;
			}*/

			if (is_key_down[(GLFW_KEY)keyboard.capslockcode ()])
			{
				caps_lock_enabled = !caps_lock_enabled;
			}

			if (focusedtextfield)
			{
				bool ctrl = is_key_down[(GLFW_KEY)keyboard.leftctrlcode ()] || is_key_down[(GLFW_KEY)keyboard.rightctrlcode ()];

				if (ctrl && pressed)
				{
					KeyAction.Id action = keyboard.get_ctrl_action (keycode);

					if (action == KeyAction.Id.COPY || action == KeyAction.Id.PASTE)
					{
						if (focusedtextfield.get ().can_copy_paste ())
						{
							switch (action)
							{
								case KeyAction.Id.COPY:
									//todo Window.get().setclipboard(focusedtextfield.Dereference().get_text());
									break;
								case KeyAction.Id.PASTE:
									//todo focusedtextfield.Dereference().add_string(Window.get().getclipboard());
									break;
							}
						}
					}
				}
				else
				{
					bool shift = is_key_down[(GLFW_KEY)keyboard.leftshiftcode ()] || is_key_down[(GLFW_KEY)keyboard.rightshiftcode ()] || caps_lock_enabled;
					Keyboard.Mapping mapping = keyboard.get_text_mapping (keycode, shift);
					focusedtextfield.get ().send_key (mapping.type, mapping.action, pressed);
				}
			}
			else
			{
				Keyboard.Mapping mapping = keyboard.get_mapping (keycode);

				bool sent = false;
				LinkedList<UIElement.Type> types = new LinkedList<UIElement.Type> ();

				bool escape = keycode == (int)GLFW_Util.GLFW_KEY_ESCAPE;
				bool tab = keycode == (int)GLFW_Util.GLFW_KEY_TAB;
				bool enter = keycode == (int)GLFW_Util.GLFW_KEY_ENTER || keycode == (int)GLFW_Util.GLFW_KEY_KP_ENTER;
				bool up_down = keycode == (int)GLFW_Util.GLFW_KEY_UP || keycode == (int)GLFW_Util.GLFW_KEY_DOWN;
				bool left_right = keycode == (int)GLFW_Util.GLFW_KEY_LEFT || keycode == (int)GLFW_Util.GLFW_KEY_RIGHT;
				bool arrows = up_down || left_right;

				var statusbar = UI.get ().get_element<UIStatusBar> ();
				var channel = UI.get ().get_element<UIChannel> ();
				var worldmap = UI.get ().get_element<UIWorldMap> ();
				var optionmenu = UI.get ().get_element<UIOptionMenu> ();
				var shop = UI.get ().get_element<UIShop> ();
				var joypad = UI.get ().get_element<UIJoypad> ();
				var rank = UI.get ().get_element<UIRank> ();
				var quit = UI.get ().get_element<UIQuit> ();
				var npctalk = UI.get ().get_element<UINpcTalk> ();
				//var report = UI::get().get_element<UIReport>();
				//var whisper = UI::get().get_element<UIWhisper>();

				if (npctalk && npctalk.get ().is_active ())
				{
					npctalk.get ().send_key (mapping.action, pressed, escape);
					sent = true;
				}
				else if (statusbar && statusbar.get ().is_menu_active ())
				{
					statusbar.get ().send_key (mapping.action, pressed, escape);
					sent = true;
				}
				else if (channel && channel.get ().is_active () && mapping.action != (int)KeyAction.Id.CHANGECHANNEL)
				{
					channel.get ().send_key (mapping.action, pressed, escape);
					sent = true;
				}
				else if (worldmap && worldmap.get ().is_active () && mapping.action != (int)KeyAction.Id.WORLDMAP)
				{
					worldmap.get ().send_key (mapping.action, pressed, escape);
					sent = true;
				}
				else if (optionmenu  && optionmenu.get ().is_active ())
				{
					optionmenu.get ().send_key (mapping.action, pressed, escape);
					sent = true;
				}
				else if (shop  && shop.get ().is_active ())
				{
					shop.get ().send_key (mapping.action, pressed, escape);
					sent = true;
				}
				else if (joypad  && joypad.get ().is_active ())
				{
					joypad.get ().send_key (mapping.action, pressed, escape);
					sent = true;
				}
				else if (rank && rank.get ().is_active ())
				{
					rank.get ().send_key (mapping.action, pressed, escape);
					sent = true;
				}
				else if (quit && quit.get ().is_active ())
				{
					quit.get ().send_key (mapping.action, pressed, escape);
					sent = true;
				}
				else
				{
					// All
					if (escape || tab || enter || arrows)
					{
						// Login
						types.AddLast (UIElement.Type.WORLDSELECT);
						types.AddLast (UIElement.Type.CHARSELECT);
						types.AddLast (UIElement.Type.RACESELECT); // No tab
						types.AddLast (UIElement.Type.CLASSCREATION); // No tab (No arrows, but shouldn't send else where)
						types.AddLast (UIElement.Type.LOGINNOTICE); // No tab (No arrows, but shouldn't send else where)
						types.AddLast (UIElement.Type.LOGINNOTICE_CONFIRM); // No tab (No arrows, but shouldn't send else where)
						types.AddLast (UIElement.Type.LOGINWAIT); // No tab (No arrows, but shouldn't send else where)
					}

					if (escape)
					{
						// Login
						types.AddLast (UIElement.Type.SOFTKEYBOARD);

						// Game
						types.AddLast (UIElement.Type.NOTICE);
						types.AddLast (UIElement.Type.KEYCONFIG);
						types.AddLast (UIElement.Type.CHAT);
						types.AddLast (UIElement.Type.EVENT);
						types.AddLast (UIElement.Type.STATSINFO);
						types.AddLast (UIElement.Type.ITEMINVENTORY);
						types.AddLast (UIElement.Type.EQUIPINVENTORY);
						types.AddLast (UIElement.Type.SKILLBOOK);
						types.AddLast (UIElement.Type.QUESTLOG);
						types.AddLast (UIElement.Type.USERLIST);
						types.AddLast (UIElement.Type.NPCTALK);
						types.AddLast (UIElement.Type.CHARINFO);
					}
					else if (enter)
					{
						// Login
						types.AddLast (UIElement.Type.SOFTKEYBOARD);

						// Game
						types.AddLast (UIElement.Type.NOTICE);
					}
					else if (tab)
					{
						// Game
						types.AddLast (UIElement.Type.ITEMINVENTORY);
						types.AddLast (UIElement.Type.EQUIPINVENTORY);
						types.AddLast (UIElement.Type.SKILLBOOK);
						types.AddLast (UIElement.Type.QUESTLOG);
						types.AddLast (UIElement.Type.USERLIST);
					}

					if (types.Count > 0)
					{
						var element = state.get_front (types);

						if (element != null)
						{
							element.send_key (mapping.action, pressed, escape);
							sent = true;
						}
					}
				}

				if (!sent)
				{
					var chatbar = UI.get ().get_element<UIChatBar> ();

					if (escape)
					{
						if (chatbar && chatbar.get ().is_chatopen ())
						{
							chatbar.get ().send_key (mapping.action, pressed, escape);
						}
						else
						{
							state.send_key (mapping.type, mapping.action, pressed, escape);
						}
					}
					else if (enter)
					{
						if (chatbar)
						{
							chatbar.get ().send_key (mapping.action, pressed, escape);
						}
						else
						{
							state.send_key (mapping.type, mapping.action, pressed, escape);
						}
					}
					else
					{
						//change_state (State.GAME);//todo remove later
						state.send_key (mapping.type, mapping.action, pressed, escape);
					}
				}
			}

			is_key_down[(GLFW_KEY)keycode] = pressed;
		}

		public void set_scrollnotice (string notice)
		{
			scrollingnotice.setnotice (notice);
		}

		public void focus_textfield (Textfield tofocus)
		{
			if (focusedtextfield)
			{
				focusedtextfield.get ().set_state (Textfield.State.NORMAL);
			}

			focusedtextfield = tofocus;
		}

		public void remove_textfield ()
		{
			if (focusedtextfield)
			{
				focusedtextfield.get ().set_state (Textfield.State.NORMAL);
			}

			focusedtextfield = new Optional<Textfield> ();
		}

		public void drag_icon (Icon icon)
		{
			state.drag_icon (icon);
		}

		public void add_keymapping (byte no, byte type, int action)
		{
			keyboard.assign (no, type, action);
		}

		public void clear_tooltip (Tooltip.Parent parent)
		{
			state.clear_tooltip (parent);
		}

		public void show_equip (Tooltip.Parent parent, short slot)
		{
			state.show_equip (parent, slot);
		}

		public void show_item (Tooltip.Parent parent, int item_id)
		{
			state.show_item (parent, item_id);
		}

		public void show_skill (Tooltip.Parent parent, int skill_id, int level, int masterlevel, long expiration)
		{
			state.show_skill (parent, skill_id, level, masterlevel, expiration);
		}

		public void show_text (Tooltip.Parent parent, string text)
		{
			state.show_text (parent, text);
		}

		public void show_map (Tooltip.Parent parent, string name, string description, int mapid, bool bolded)
		{
			state.show_map (parent, name, description, mapid, bolded);
		}

		public Keyboard get_keyboard ()
		{
			return keyboard;
		}

		public Optional<T> emplace<T> (params object[] args) where T : UIElement
		{
			var type = typeof (T);
			var uiElementType = (UIElement.Type)type.GetField ("TYPE").GetRawConstantValue ();
			var toggled = (bool)type.GetField ("TOGGLED").GetRawConstantValue ();
			var focused = (bool)type.GetField ("FOCUSED").GetRawConstantValue ();

			var iter = state.pre_add (uiElementType, toggled, focused);
			//iter.values.TryAdd (uiElementType, (T)System.Activator.CreateInstance (type, args),true);

			if (iter.TryGetValue (uiElementType, out var uiElement) && uiElement != null)
			{
				uiElement.makeactive ();
			}
			else
			{
				iter.TryAdd (uiElementType, (UIElement)System.Activator.CreateInstance (type, args));
			}

			return (T)state.get (uiElementType);


			/*
			var attributes = type.GetCustomAttributes (false);

			if (attributes.Length > 1)
			{
				var uiElementAttribute = (UIElementAttribute)attributes[0];
				var map = state.pre_add (uiElementAttribute.TYPE, uiElementAttribute.TOGGLED, uiElementAttribute.FOCUSED);
				map.values.TryAdd (uiElementAttribute.TYPE, (T)System.Activator.CreateInstance (type, args));
				/*var iter = state.pre_add (T.TYPE, T.TOGGLED, T.FOCUSED);
				if (iter)
				{ 
					iter.values =  <T > (forward<Args> (args)...);
				}#1#

				return (Optional<T>)state.get (uiElementAttribute.TYPE);
			}
			return new Optional<T> ();
			*/
		}

		public Optional<T> get_element<T> () where T : UIElement
		{
			var type = typeof (T);
			var uiElementType = (UIElement.Type)type.GetField ("TYPE").GetRawConstantValue ();
			UIElement element = state.get (uiElementType);

			return (T)element;
		}

		public void remove (UIElement.Type type)
		{
			focusedtextfield = new Optional<Textfield> ();

			state.remove (type);
		}

		private UIState state;

		private Keyboard keyboard = new Keyboard ();
		private Cursor cursor = new Cursor ();
		private ScrollingNotice scrollingnotice = new ScrollingNotice ();

		private Optional<Textfield> focusedtextfield = new Optional<Textfield> ();
		private EnumMap<GLFW_KEY, bool> is_key_down = new EnumMap<GLFW_KEY, bool> ();

		private bool enabled;
		private bool quitted;
		private bool caps_lock_enabled = false;
	}
}