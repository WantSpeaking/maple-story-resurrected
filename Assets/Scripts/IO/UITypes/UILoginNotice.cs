﻿#define USE_NX

using System;
using Attributes;
using MapleLib.WzLib;

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
	public class UIKeyConfirm : UIElement
	{
		public const Type TYPE = UIElement.Type.LOGINNOTICE_CONFIRM;
		public const bool FOCUSED = true;
		public const bool TOGGLED = false;

		public UIKeyConfirm (params object[] args) : this ((bool)args[0], (Action)args[1], (bool)args[2])
		{
		}

		public UIKeyConfirm (bool alternate, System.Action oh, bool l)
		{
			this.okhandler = oh;
			this.login = l;
			WzObject alert = nl.nx.wzFile_ui["UIWindow2.img"]["KeyConfig"]["KeyType"]["alert"];
			WzObject background = alternate ? alert["alternate"] : alert["default"];

			sprites.Add (background);

			buttons[(int)Buttons.OK] = new MapleButton (alert["btOk"]);

			position = new Point<short> (276, 229);
			dimension = new Texture (background).get_dimensions ();
		}

		public override void send_key (int keycode, bool pressed, bool escape)
		{
			if (pressed)
			{
				if (keycode == (int)KeyAction.Id.RETURN)
				{
					confirm ();
				}
				else if (!login && escape)
				{
					deactivate ();

					UI.get ().remove (UIElement.Type.LOGINNOTICE);
				}
			}
		}

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: UIElement::Type get_type() const override
		public override UIElement.Type get_type ()
		{
			return TYPE;
		}

		public override Button.State button_pressed (ushort buttonid)
		{
			confirm ();

			return Button.State.NORMAL;
		}

		private void confirm ()
		{
			okhandler ();
			deactivate ();

			UI.get ().remove (UIElement.Type.LOGINNOTICE);
		}

		private enum Buttons
		{
			OK
		}

		private System.Action okhandler;
		private bool login;
	}

	public class UIKeySelect : UIElement
	{
		public const Type TYPE = UIElement.Type.LOGINNOTICE;
		public const bool FOCUSED = true;
		public const bool TOGGLED = false;

		public UIKeySelect (params object[] args) : this ((Action<bool>)args[0], (bool)args[1])
		{
		}

		public UIKeySelect (System.Action<bool> oh, bool l)
		{
			this.okhandler = oh;
			this.login = l;
			WzObject KeyType = nl.nx.wzFile_ui["UIWindow2.img"]["KeyConfig"]["KeyType"];
			WzObject backgrnd = KeyType["backgrnd"];

			sprites.Add (backgrnd);

			buttons[(int)Buttons.CLOSE] = new MapleButton (KeyType["btClose"]);
			buttons[(int)Buttons.TYPEA] = new MapleButton (KeyType["btTypeA"]);
			buttons[(int)Buttons.TYPEB] = new MapleButton (KeyType["btTypeB"], new Point<short> (1, 1));

			if (login)
			{
				buttons[(int)Buttons.CLOSE].set_active (false);
			}

			position = new Point<short> (181, 145);
			dimension = new Texture (backgrnd).get_dimensions ();
		}

		public override void send_key (int keycode, bool pressed, bool escape)
		{
			if (pressed && !login)
			{
				if (escape || keycode == (int)KeyAction.Id.RETURN)
				{
					deactivate ();
				}
			}
		}

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: UIElement::Type get_type() const override
		public override UIElement.Type get_type ()
		{
			return TYPE;
		}

		public override Button.State button_pressed (ushort buttonid)
		{
			switch ((Buttons)buttonid)
			{
				default:
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 TODO TASK: C# does not allow fall-through from a non-empty 'case':
				case Buttons.CLOSE:
					deactivate ();
					break;
				case Buttons.TYPEA:
				case Buttons.TYPEB:
				{
					bool alternate = (buttonid == (int)Buttons.TYPEA) ? false : true;

					if (alternate)
					{
						buttons[(int)Buttons.TYPEA].set_state (Button.State.DISABLED);
					}
					else
					{
						buttons[(int)Buttons.TYPEB].set_state (Button.State.DISABLED);
					}

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 TODO TASK: Lambda expressions cannot be assigned to 'var':
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: var onok = [&, alternate]()
					Action onok = () =>
					{
						okhandler (alternate);
						deactivate ();
					};

					UI.get ().emplace<UIKeyConfirm> (alternate, onok, login);
					break;
				}
			}

			return Button.State.DISABLED;
		}

		private enum Buttons
		{
			CLOSE,
			TYPEA,
			TYPEB
		}

		private System.Action<bool> okhandler;
		private bool login;
	}

	public class UIClassConfirm : UIElement
	{
		public const Type TYPE = UIElement.Type.LOGINNOTICE;
		public const bool FOCUSED = false;
		public const bool TOGGLED = false;

		public UIClassConfirm (params object[] args) : this ((ushort)args[0], (bool)args[1], (Action)args[2])
		{
		}

		public UIClassConfirm (ushort selected_class, bool unavailable, System.Action okhandler)
		{
			this.okhandler = okhandler;
			WzObject RaceSelect = nl.nx.wzFile_ui["Login.img"]["RaceSelect_new"];
			WzObject type = unavailable ? RaceSelect["deny"] : RaceSelect["confirm"];
			WzObject backgrnd = type["backgrnd"];
			WzObject race = type["race"][selected_class.ToString ()];

			short backgrnd_x = new Texture (backgrnd).get_dimensions ().x ();
			short race_x = new Texture (race).get_dimensions ().x ();

			short pos_x = (short)((backgrnd_x - race_x) / 2);

			sprites.Add (backgrnd);
			sprites.Add (new Sprite (race, new Point<short> (pos_x, 10) + (Point<short>)race["origin"]));

			if (unavailable)
			{
				buttons[(int)Buttons.OK] = new MapleButton (type["BtOK"]);
			}
			else
			{
				buttons[(int)Buttons.OK] = new MapleButton (type["BtOK"], new Point<short> (62, 107));
				buttons[(int)Buttons.CANCEL] = new MapleButton (type["BtCancel"], new Point<short> (137, 107));
			}

			position = new Point<short> (286, 189);
			dimension = new Texture (backgrnd).get_dimensions ();
		}

		public override Cursor.State send_cursor (bool clicked, Point<short> cursorpos)
		{
			foreach (var btit in buttons)
			{
				if (btit.Value.is_active () && btit.Value.bounds (position).contains (cursorpos))
				{
					if (btit.Value.get_state () == Button.State.NORMAL)
					{
						new Sound (Sound.Name.BUTTONOVER).play ();

						btit.Value.set_state (Button.State.MOUSEOVER);
					}
					else if (btit.Value.get_state () == Button.State.MOUSEOVER)
					{
						if (clicked)
						{
							new Sound (Sound.Name.BUTTONCLICK).play ();

							btit.Value.set_state (button_pressed ((ushort)btit.Key));
						}
					}
				}
				else if (btit.Value.get_state () == Button.State.MOUSEOVER)
				{
					btit.Value.set_state (Button.State.NORMAL);
				}
			}

			return Cursor.State.LEAF;
		}

		public override void send_key (int keycode, bool pressed, bool escape)
		{
			if (pressed)
			{
				if (escape)
				{
					deactivate ();
				}
				else if (keycode == (int)KeyAction.Id.RETURN)
				{
					button_pressed ((ushort)Buttons.OK);
				}
			}
		}

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: UIElement::Type get_type() const override
		public override UIElement.Type get_type ()
		{
			return TYPE;
		}

		public override Button.State button_pressed (ushort buttonid)
		{
			deactivate ();

			if (buttonid == (int)Buttons.OK)
			{
				okhandler ();
			}

			return Button.State.NORMAL;
		}

		private enum Buttons : ushort
		{
			OK,
			CANCEL
		}

		private System.Action okhandler;
	}

	public class UIQuitConfirm : UIElement
	{
		public const Type TYPE = UIElement.Type.LOGINNOTICE;
		public const bool FOCUSED = true;
		public const bool TOGGLED = false;

		public UIQuitConfirm ()
		{
			WzObject notice = nl.nx.wzFile_ui["Login.img"]["Notice"];
			WzObject backgrnd = notice["backgrnd"]["0"];

			sprites.Add (backgrnd);
			sprites.Add (new Sprite (notice["text"][UILoginNotice.Message.CONFIRM_EXIT.ToString ()], new Point<short> (17, 13)));

			buttons[(int)Buttons.BT_OK] = new MapleButton (notice["BtYes"], new Point<short> (70, 106));
			buttons[(int)Buttons.BT_CANCEL] = new MapleButton (notice["BtNo"], new Point<short> (130, 106));

			position = new Point<short> (275, 209);
			dimension = new Texture (backgrnd).get_dimensions ();
		}

		public override void send_key (int keycode, bool pressed, bool escape)
		{
			if (pressed)
			{
				if (escape)
				{
					deactivate ();
				}
				else if (keycode == (int)KeyAction.Id.RETURN)
				{
					UI.get ().quit ();
					deactivate ();
				}
			}
		}

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: UIElement::Type get_type() const override
		public override UIElement.Type get_type ()
		{
			return TYPE;
		}

		public override Button.State button_pressed (ushort buttonid)
		{
			if (buttonid == (int)Buttons.BT_OK)
			{
				UI.get ().quit ();
			}

			deactivate ();

			return Button.State.PRESSED;
		}

		private enum Buttons
		{
			BT_OK,
			BT_CANCEL
		}
	}

	[UIElement (UIElement.Type.LOGINNOTICE, true, false)]
	public class UILoginNotice : UIElement
	{
		//public override Type TYPE => UIElement.Type.LOGINNOTICE;
		public const Type TYPE = UIElement.Type.LOGINNOTICE;
		public const bool FOCUSED = true;
		public const bool TOGGLED = false;

		public enum Message : ushort
		{
			VULGAR_NAME,
			DELETE_CHAR_ENTER_BIRTHDAY,
			WRONG_EMAIL,
			WRONG_PASSWORD,
			INCORRECT_EMAIL,
			NAME_IN_USE,
			NAME_OK,
			RETURN_TO_FIRST_PAGE,
			NAME_IN_USE2,
			FULL_CHARACTER_SLOTS,
			ILLEGAL_NAME,
			BIRTHDAY_INCORRECT,
			PRESS_CHECK_BUTTON,
			DELETE_CONFIRMATION,
			MATURE_CHANNEL,
			TROUBLE_LOGGING_IN,
			BLOCKED_ID,
			ALREADY_LOGGED_IN,
			UNKNOWN_ERROR,
			TOO_MANY_REQUESTS,
			NOT_REGISTERED,
			UNABLE_TO_LOGIN_WITH_IP,
			UNABLE_TO_LOGIN,
			UNABLE_TO_CONNECT,
			AN_ERROR_OCCURED,
			AN_ERROR_OCCURED_DETAILED,
			CANNOT_ACCESS_ACCOUNT,
			WRONG_GATEWAY,
			INCORRECT_LOGINID,
			INCORRECT_FORM,
			VERIFICATION_NOTICE_7,
			VERIFICATION_NOTICE_30,
			KOREAN,
			VERIFY_EMAIL,
			CANNOT_DELETE_GUILD_LEADER,
			SUSPICIOUS_PROGRAMS,
			POPULATION_TOO_HIGH,
			SELECT_A_CHANNEL,
			GAME_GUARD_UPDATED,
			CANNOT_DELETE_ENGAGED,
			PLEASE_SIGN_UP,
			PASSWORD_IS_INCORRECT,
			Value_PASSWORD_INCORRECT,
			TEMPORARY_IP_BAN,
			DISABLE_SAFETY_MEASURE,
			Value_PASSWORD_NOT_DIFFERENT,
			CANNOT_DELETE_ENGAGED2,
			KOREAN2,
			KOREAN3,
			KOREAN4,
			KOREAN5,
			KOREAN6,
			KOREAN7,
			CONFIRM_EXIT,
			CANNOT_DELETE_FAMILY_LEADER,
			CASH_ITEMS_CONFIRM_DELETION,
			KOREAN8,
			FAMILY_CONFIRM_DELETION,
			FAMILY_AND_CASH_ITEMS_CONFIRM_DELETION,
			KOREAN9,
			KOREAN10,
			IDENTITY_VERIFICATION_REQ_ERROR,
			KOREAN11,
			LOGIN_FAIL_SERVER_OVERBURDEN,
			KOREAN12,
			KOREAN13,
			KOREAN14,
			KOREAN15,
			IDENTITY_VERIFICATION_REQ,
			PART_TIME_JOB_ACTIVE,
			DEL_CHAR_FAIL_HIRED_MERCH_ACTIVE,
			SET_SEC_PASS,
			SEC_PASS_AUTH_FAIL,
			EMAIL_ID_FAIL_USE_MAPLE_ID,
			LOGIN_USING_EMAIL_ID,
			MAPLE_ID_REQ,
			MAPLE_ID_SUCCESS,
			MAPLE_ID_FAIL,
			LOGIN_USING_MAPLE_ID_OR_EMAIL_ID,
			DEL_CHAR_FAIL_ACTIVE_ITEM_GUARD,
			MAPLE_ID_SUCCESS_BOLD,
			CHARACTER_RANGE_FAILED,
			MAPLE_ID_ALREADY_EXISTS,
			MAPLE_ID_ONLY_LETTERS_AND_NUMS,
			MAPLE_ID_EMAIL_ID_NOT_AUTH,
			MAPLE_ID_ALREADY_CREATED_TODAY,
			MAPLE_ID_MAX_LIMIT_REACHED_BLOCK,
			MAPLE_ID_MAX_LIMIT_REACHED_CREATE,
			KOREAN16,
			UNDER_AGE,
			UNDER_AGE2,
			PIC_SAME_AS_PASSWORD,
			PIC_CONTAINS_PIN,
			INCORRECT_PIC,
			CHAR_DEL_FAIL_NO_PIC,
			PIC_REQ,
			OTP_SERVICE_IN_USE,
			CHAR_DEL_FAIL_OTP_SERVICE,
			SEC_PASS_CHANGE_FAIL_OTP_SERVICE,
			CLIENT_ALREADY_RUNNING,
			CHAR_TRANS_SUCCESS = 105,
			CHAR_DEL_FAIL_MAX_LIMIT_REACHED,
			OVERSEAS_LOGIN_BLOCKED,
			DEL_CHAR_FAIL_ITEMS_IN_AUC_HOUSE,
			ENTER_STAR_PLANET_CONF_SEL_CHAR,
			ENTER_STAR_PLANET_CONF_STAR_PLANET_CHAR,
			CREATE_SHINING_STAR_CHAR_CONF,
			CLASS_UNAVAILABLE,
			START_PLANET_FAIL_LV_33_REQ,
			PIC_ACTIVATED = 902,
			PIC_DEACTIVATED,
			CHANNEL_SEL_REQ,
			GAMEGUARD_UPDATE_REQ,
			IP_BLOCK_GMS,
			PIC_USED_TOO_FREQ,
			PIC_CHANGES_REQ_FROM_WEB,
			PIC_UNSECURE,
			PIC_STALE,
			PIC_REPITIVE,
			NEW_PIC_REQ,
			JAPANESE = 10000,
			JAPANESE2
		}

		public UILoginNotice (params object[] args) : this ((ushort)args[0], (Action)args[1], (Action)args[2])
		{
		}

		public UILoginNotice (ushort message, System.Action okhandler, System.Action cancelhandler)
		{
			this.okhandler = okhandler;
			this.cancelhandler = cancelhandler;
			multiple = false;

			WzObject Notice = nl.nx.wzFile_ui["Login.img"]["Notice"];
			WzObject backgrnd;

			switch ((Message)message)
			{
				case Message.NAME_IN_USE:
				case Message.ILLEGAL_NAME:
				case Message.BLOCKED_ID:
				case Message.INCORRECT_PIC:
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
//ORIGINAL LINE: backgrnd = Notice["backgrnd"]["1"];
					backgrnd = (Notice["backgrnd"]["1"]);
					break;
				default:
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
//ORIGINAL LINE: backgrnd = Notice["backgrnd"]["0"];
					backgrnd = (Notice["backgrnd"]["0"]);
					break;
			}

			sprites.Add (backgrnd);
			sprites.Add (new Sprite (Notice["text"][message.ToString ()], new Point<short> (17, 13)));

			if (message == (int)Message.DELETE_CONFIRMATION)
			{
				multiple = true;

				buttons[(int)Buttons.YES] = new MapleButton (Notice["BtYes"], new Point<short> (70, 106));
				buttons[(int)Buttons.NO] = new MapleButton (Notice["BtNo"], new Point<short> (130, 106));
			}
			else
			{
				buttons[(int)Buttons.YES] = new MapleButton (Notice["BtYes"], new Point<short> (100, 106));
			}

			position = new Point<short> (275, 209);
			dimension = new Texture (backgrnd).get_dimensions ();
		}
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 TODO TASK: The implementation of the following method could not be found:
//		UILoginNotice(ushort message, System.Action okhandler) : UILoginNotice(message, okhandler, () => TangibleLambdaToken67UILoginNotice(ushort message);

		public override void send_key (int keycode, bool pressed, bool escape)
		{
			if (pressed)
			{
				if (escape)
				{
					if (!multiple)
					{
						okhandler ();
					}
					else
					{
						cancelhandler ();
					}

					deactivate ();
				}
				else if (keycode == (int)KeyAction.Id.RETURN)
				{
					okhandler ();
					deactivate ();
				}
			}
		}

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: UIElement::Type get_type() const override
		public override UIElement.Type get_type ()
		{
			return TYPE;
		}

		public override Button.State button_pressed (ushort buttonid)
		{
			if (buttonid == (int)Buttons.YES)
			{
				okhandler ();
			}
			else if (buttonid == (int)Buttons.NO)
			{
				cancelhandler ();
			}

			deactivate ();

			return Button.State.NORMAL;
		}

		private enum Buttons : ushort
		{
			YES,
			NO
		}

		private bool saveid;
		private bool multiple;
		private System.Action okhandler;
		private System.Action cancelhandler;
	}
}