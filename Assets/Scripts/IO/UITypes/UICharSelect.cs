﻿#define USE_NX

using System;
using System.Collections.Generic;

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
	// The character selection screen
	public class UICharSelect : UIElement
	{
		public static  Type TYPE = UIElement.Type.CHARSELECT;
		public static  bool FOCUSED = false;
		public static  bool TOGGLED = false;

		public UICharSelect(List<CharEntry> characters, sbyte characters_count, int slots, sbyte require_pic)
		{
			this.characters = new List<CharEntry>(characters);
			/*this.characters_count = characters_count;
			this.slots = slots;
			this.require_pic = require_pic;
			burning_character = true;

			string version_text = Configuration.get().get_version();
			version = new Text(Text.Font.A11B, Text.Alignment.LEFT, Color.Name.LEMONGRASS, "Ver. " + version_text);

			pagepos = new Point<short>(247, 462);
			worldpos = new Point<short>(586, 46);
			charinfopos = new Point<short>(671, 339);

			Point<short> character_sel_pos = new Point<short>(601, 393);
			Point<short> character_new_pos = new Point<short>(200, 495);
			Point<short> character_del_pos = new Point<short>(316, 495);

			selected_character = Setting<DefaultCharacter>.get().load();
			selected_page = selected_character / PAGESIZE;
			page_count = Math.Ceiling((double)slots / (double)PAGESIZE);

			tab = nl.nx.ui["Basic.img"]["Cursor"]["18"]["0"];

			tab_index = 0;
			tab_active = false;
			tab_move = false;

			Point<short> tab_adj = new Point<short>(86, 5);

			tab_pos[0] = character_sel_pos + tab_adj + new Point<short>(47, 3);
			tab_pos[1] = character_new_pos + tab_adj;
			tab_pos[2] = character_del_pos + tab_adj;

			tab_move_pos = 0;

			tab_map[0] = (int)Buttons.CHARACTER_SELECT;
			tab_map[1] = (int)Buttons.CHARACTER_NEW;
			tab_map[2] = (int)Buttons.CHARACTER_DELETE;

			nl.node Login = nl.nx.ui["Login.img"];
			nl.node Common = Login["Common"];
			nl.node CharSelect = Login["CharSelect"];
			nl.node selectWorld = Common["selectWorld"];
			nl.node selectedWorld = CharSelect["selectedWorld"];
			nl.node pageNew = CharSelect["pageNew"];

			world_dimensions = new Texture(selectWorld).get_dimensions();

			ushort world;
			byte world_id = Configuration.get().get_worldid();
			byte channel_id = Configuration.get().get_channelid();

			if (auto worldselect = UI.get().get_element<UIWorldSelect>())
			{
				world = worldselect.get_worldbyid(world_id);
			}

			world_sprites.emplace_back(selectWorld, worldpos);
			world_sprites.emplace_back(selectedWorld["icon"][world], worldpos - new Point<short>(12, -1));
			world_sprites.emplace_back(selectedWorld["name"][world], worldpos - new Point<short>(8, 1));
			world_sprites.emplace_back(selectedWorld["ch"][channel_id], worldpos - new Point<short>(0, 1));

			nl.node map = nl.nx.map001["Back"]["login.img"];
			nl.node ani = map["ani"];

			nl.node frame = nl.nx.mapLatest["Obj"]["login.img"]["Common"]["frame"]["2"]["0"];

			sprites.emplace_back(map["back"]["13"], new Point<short>(392, 297));
			sprites.emplace_back(ani["17"], new Point<short>(151, 283));
			sprites.emplace_back(ani["18"], new Point<short>(365, 252));
			sprites.emplace_back(ani["19"], new Point<short>(191, 208));
			sprites.emplace_back(frame, new Point<short>(400, 300));
			sprites.emplace_back(Common["frame"], new Point<short>(400, 300));
			sprites.emplace_back(Common["step"]["2"], new Point<short>(40, 0));

			burning_notice = Common["Burning"]["BurningNotice"];
			burning_count = new Text(Text.Font.A12B, Text.Alignment.LEFT, Color.Name.CREAM, "1");

			charinfo = CharSelect["charInfo"];
			charslot = CharSelect["charSlot"]["0"];
			pagebase = pageNew["base"]["0"];
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: pagenumber = Charset(pageNew["number"], Charset::Alignment::LEFT);
			pagenumber = new Charset(new nl.node(pageNew["number"]), Charset.Alignment.LEFT);
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
//ORIGINAL LINE: pagenumberpos = pageNew["numberpos"];
			pagenumberpos.CopyFrom(pageNew["numberpos"]);

			signpost[0] = CharSelect["adventure"]["0"];
			signpost[1] = CharSelect["knight"]["0"];
			signpost[2] = CharSelect["aran"]["0"];

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created if it does not yet exist:
//ORIGINAL LINE: nametag = CharSelect["nameTag"];
			nametag.CopyFrom(CharSelect["nameTag"]);

			buttons[(int)Buttons.CHARACTER_SELECT] = std::make_unique<MapleButton>(CharSelect["BtSelect"], character_sel_pos);
			buttons[(int)Buttons.CHARACTER_NEW] = std::make_unique<MapleButton>(CharSelect["BtNew"], character_new_pos);
			buttons[(int)Buttons.CHARACTER_DELETE] = std::make_unique<MapleButton>(CharSelect["BtDelete"], character_del_pos);
			buttons[(int)Buttons.PAGELEFT] = std::make_unique<MapleButton>(CharSelect["pageL"], new Point<short>(98, 491));
			buttons[(int)Buttons.PAGERIGHT] = std::make_unique<MapleButton>(CharSelect["pageR"], new Point<short>(485, 491));
			buttons[(int)Buttons.CHANGEPIC] = std::make_unique<MapleButton>(Common["BtChangePIC"], new Point<short>(0, 80));
			buttons[(int)Buttons.RESETPIC] = std::make_unique<MapleButton>(Login["WorldSelect"]["BtResetPIC"], new Point<short>(0, 115));
			buttons[(int)Buttons.EDITCHARLIST] = std::make_unique<MapleButton>(CharSelect["EditCharList"]["BtCharacter"], new Point<short>(-1, 47));
			buttons[(int)Buttons.BACK] = std::make_unique<MapleButton>(Common["BtStart"], new Point<short>(0, 515));

			for (uint i = 0; i < PAGESIZE; i++)
			{
				buttons[(int)Buttons.CHARACTER_SLOT0 + i] = std::make_unique<AreaButton>(get_character_slot_pos(i, 105, 144), new Point<short>(50, 90));
			}

			if (require_pic == 0)
			{
				buttons[(int)Buttons.CHANGEPIC].set_active(false);
				buttons[(int)Buttons.RESETPIC].set_active(false);
			}

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: levelset = Charset(CharSelect["lv"], Charset::Alignment::CENTER);
			levelset = new Charset(new nl.node(CharSelect["lv"]), Charset.Alignment.CENTER);
			namelabel = new OutlinedText(Text.Font.A14B, Text.Alignment.CENTER, Color.Name.WHITE, Color.Name.IRISHCOFFEE);

			for (uint i = 0; i < InfoLabel.NUM_LABELS; i++)
			{
				infolabels[i] = new OutlinedText(Text.Font.A11M, Text.Alignment.RIGHT, Color.Name.WHITE, Color.Name.TOBACCOBROWN);
			}

			foreach (CharEntry entry in characters)
			{
				charlooks.emplace_back(entry.look);
				nametags.emplace_back(nametag, Text.Font.A12M, entry.stats.name);
			}

			emptyslot_effect = CharSelect["character"]["0"];
			emptyslot = CharSelect["character"]["1"]["0"];

			selectedslot_effect[0] = CharSelect["effect"][0];
			selectedslot_effect[1] = CharSelect["effect"][1];

			charslotlabel = new OutlinedText(Text.Font.A12M, Text.Alignment.LEFT, Color.Name.WHITE, Color.Name.JAMBALAYA);
			charslotlabel.change_text(get_slot_text());

			update_buttons();

			if (characters_count > 0)
			{
				if (selected_character < characters_count)
				{
					update_selected_character();
				}
				else
				{
					select_last_slot();
				}
			}

			if (Configuration.get().get_auto_login())
			{
				SelectCharPicPacket(Configuration.get().get_auto_pic(), Configuration.get().get_auto_cid()).dispatch();
			}

			dimension = new Point<short>(800, 600);*/
		}

/*
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void draw(float inter) const override
		public override void draw(float inter)
		{
			base.draw_sprites(inter);

			version.draw(position + new Point<short>(707, 4));

			charslot.draw(position + new Point<short>(589, 106 - charslot_y));
			charslotlabel.draw(position + new Point<short>(702, 111 - charslot_y));

			foreach (Sprite sprite in world_sprites)
			{
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: sprite.draw(position, inter);
				sprite.draw(new ms.Point(position), inter);
			}

			string total = pad_number_with_leading_zero(page_count);
			string current = pad_number_with_leading_zero(selected_page + 1);

			LinkedList<byte> fliplist = {2, 3, 6, 7};

			for (byte i = 0; i < PAGESIZE; i++)
			{
				byte index = i + selected_page * PAGESIZE;
				bool flip_character = fliplist.Contains(i);
				bool selectedslot = index == selected_character;

				if (index < characters_count)
				{
					Point<short> charpos = get_character_slot_pos(i, 130, 234);
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: DrawArgument chararg = DrawArgument(charpos, flip_character);
					DrawArgument chararg = new DrawArgument(new ms.Point(new ms.Point(charpos)), flip_character);

					nametags[index].draw(charpos + new Point<short>(2, 1));

					StatsEntry character_stats = characters[index].stats;

					if (selectedslot)
					{
						selectedslot_effect[1].draw(charpos + new Point<short>(-5, 16), inter);

						sbyte lvy = -115;
						Point<short> pos_adj = new Point<short>(662, 365);

						charinfo.draw(position + charinfopos);

						string levelstr = Convert.ToString(character_stats.stats[(int)MapleStat.Id.LEVEL]);
						short lvx = levelset.draw(levelstr, pos_adj + new Point<short>(12, lvy));
						levelset.draw('l', pos_adj + new Point<short>(1 - lvx / 2, lvy));

						namelabel.draw(pos_adj + new Point<short>(10, -103));

						for (uint i = 0; i < InfoLabel.NUM_LABELS; i++)
						{
							Point<short> labelpos = pos_adj + get_infolabel_pos(i);
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: infolabels[i].draw(labelpos);
							infolabels[i].draw(new ms.Point(labelpos));
						}
					}

					byte j = 0;
					ushort job = character_stats.stats[(int)MapleStat.Id.JOB];

					if (job >= 0 && job < 1000)
					{
						j = 0;
					}
					else if (job >= 1000 && job < 2000)
					{
						j = 1;
					}
					else if (job >= 2000 && job < 2200)
					{
						j = 2;
					}
					else
					{
						j = 0;
					}

					signpost[j].draw(chararg);
					charlooks[index].draw(chararg, inter);

					if (selectedslot)
					{
						selectedslot_effect[0].draw(charpos + new Point<short>(-5, -298), inter);
					}
				}
				else if (i < slots)
				{
					Point<short> emptyslotpos = get_character_slot_pos(i, 130, 234);

					emptyslot_effect.draw(emptyslotpos, inter);
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: emptyslot.draw(DrawArgument(emptyslotpos, flip_character));
					emptyslot.draw(new DrawArgument(new ms.Point(new ms.Point(emptyslotpos)), flip_character));
				}
			}

			base.draw_buttons(inter);

			if (tab_active)
			{
				tab.draw(position + tab_pos[tab_index] + new Point<short>(0, tab_move_pos));
			}

			if (burning_character)
			{
				burning_notice.draw(position + new Point<short>(190, 502), inter);
				burning_count.draw(position + new Point<short>(149, 464));
			}

			pagebase.draw(position + pagepos);
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: pagenumber.draw(current.substr(0, 1), position + pagepos + Point<short>(pagenumberpos[0]));
			pagenumber.draw(current.Substring(0, 1), position + pagepos + new Point<short>(new nl.node(pagenumberpos[0])));
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: pagenumber.draw(current.substr(1, 1), position + pagepos + Point<short>(pagenumberpos[1]));
			pagenumber.draw(current.Substring(1, 1), position + pagepos + new Point<short>(new nl.node(pagenumberpos[1])));
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: pagenumber.draw(total.substr(0, 1), position + pagepos + Point<short>(pagenumberpos[2]));
			pagenumber.draw(total.Substring(0, 1), position + pagepos + new Point<short>(new nl.node(pagenumberpos[2])));
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: pagenumber.draw(total.substr(1, 1), position + pagepos + Point<short>(pagenumberpos[3]));
			pagenumber.draw(total.Substring(1, 1), position + pagepos + new Point<short>(new nl.node(pagenumberpos[3])));
		}
		public override void update()
		{
			base.update();

			if (show_timestamp)
			{
				if (timestamp > 0)
				{
					timestamp -= Constants.GlobalMembers.TIMESTEP;

					if (timestamp <= 176)
					{
						charslot_y += 1;
					}
				}
			}
			else
			{
				if (timestamp <= 176)
				{
					timestamp += Constants.GlobalMembers.TIMESTEP;

					if (charslot_y >= 0)
					{
						charslot_y -= 1;
					}
				}
			}

			if (tab_move && tab_move_pos < 4)
			{
				tab_move_pos += 1;
			}

			if (tab_move && tab_move_pos == 4)
			{
				tab_move = false;
			}

			if (!tab_move && tab_move_pos > 0)
			{
				tab_move_pos -= 1;
			}

			foreach (CharLook charlook in charlooks)
			{
				charlook.update(Constants.GlobalMembers.TIMESTEP);
			}

			foreach (Animation effect in selectedslot_effect)
			{
				effect.update();
			}

			emptyslot_effect.update();

			if (burning_character)
			{
				burning_notice.update();
			}
		}

		public override void doubleclick(Point<short> cursorpos)
		{
			ushort button_index = selected_character + Buttons.CHARACTER_SLOT0;
			auto btit = buttons[button_index];

			if (btit.is_active() && btit.bounds(position).contains(cursorpos) && btit.get_state() == Button.State.NORMAL && button_index >= Buttons.CHARACTER_SLOT0)
			{
				button_pressed(Buttons.CHARACTER_SELECT);
			}
		}
		public override Cursor.State send_cursor(bool clicked, Point<short> cursorpos)
		{
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: Rectangle<short> charslot_bounds = Rectangle<short>(worldpos, worldpos + world_dimensions);
			Rectangle<short> charslot_bounds = new Rectangle<short>(new ms.Point(new ms.Point(worldpos)), worldpos + world_dimensions);

			if (charslot_bounds.contains(cursorpos))
			{
				if (clicked)
				{
					show_timestamp = !show_timestamp;

					return Cursor.State.CLICKING;
				}
			}

			Cursor.State ret = clicked ? Cursor.State.CLICKING : Cursor.State.IDLE;

			foreach (var btit in buttons)
			{
				if (btit.second.is_active() && btit.second.bounds(position).contains(cursorpos))
				{
					if (btit.second.get_state() == Button.State.NORMAL)
					{
						Sound(Sound.Name.BUTTONOVER).play();

						btit.second.set_state(Button.State.MOUSEOVER);
						ret = Cursor.State.CANCLICK;
					}
					else if (btit.second.get_state() == Button.State.MOUSEOVER)
					{
						if (clicked)
						{
							Sound(Sound.Name.BUTTONCLICK).play();

							btit.second.set_state(button_pressed(btit.first));

							if (tab_active && btit.first == tab_map[tab_index])
							{
								btit.second.set_state(Button.State.MOUSEOVER);
							}

							ret = Cursor.State.IDLE;
						}
						else
						{
							if (!tab_active || btit.first != tab_map[tab_index])
							{
								ret = Cursor.State.CANCLICK;
							}
						}
					}
				}
				else if (btit.second.get_state() == Button.State.MOUSEOVER)
				{
					if (!tab_active || btit.first != tab_map[tab_index])
					{
						btit.second.set_state(Button.State.NORMAL);
					}
				}
			}

			return ret;
		}
		public override void send_key(int keycode, bool pressed, bool escape)
		{
			if (pressed)
			{
				if (escape)
				{
					button_pressed(Buttons.BACK);
				}
				else if (keycode == (int)KeyAction.Id.RETURN)
				{
					if (tab_active)
					{
						ushort btn_index = tab_map[tab_index];

						auto btn = buttons[btn_index];
						Button.State state = btn.get_state();

						if (state != Button.State.DISABLED)
						{
							button_pressed(btn_index);
						}
					}
					else
					{
						button_pressed(Buttons.CHARACTER_SELECT);
					}
				}
				else
				{
					if (keycode == (int)KeyAction.Id.TAB)
					{
						byte prev_tab = tab_index;

						if (!tab_active)
						{
							tab_active = true;

							if (!buttons[(int)Buttons.CHARACTER_SELECT].is_active())
							{
								tab_index++;
							}
						}
						else
						{
							tab_index++;

							if (tab_index > 2)
							{
								tab_active = false;
								tab_index = 0;
							}
						}

						tab_move = true;
						tab_move_pos = 0;

						auto prev_btn = buttons[tab_map[prev_tab]];
						Button.State prev_state = prev_btn.get_state();

						if (prev_state != Button.State.DISABLED)
						{
							prev_btn.set_state(Button.State.NORMAL);
						}

						if (tab_active)
						{
							auto btn = buttons[tab_map[tab_index]];
							Button.State state = btn.get_state();

							if (state != Button.State.DISABLED)
							{
								btn.set_state(Button.State.MOUSEOVER);
							}
						}
					}
					else
					{
						byte selected_index = selected_character;
						byte index_total = Math.Min(characters_count, (sbyte)((selected_page + 1) * PAGESIZE));

						byte COLUMNS = 4;
						byte columns = Math.Min(index_total, COLUMNS);

						byte rows = Math.Floor((index_total - 1) / COLUMNS) + 1;

						InfoLabel.int current_col = 0;

						if (columns > 0)
						{
							div_t div = std::div(selected_index, columns);
							current_col = div.rem;
						}

						if (keycode == (int)KeyAction.Id.UP)
						{
							byte next_index = (selected_index - COLUMNS < 0 ? (selected_index - COLUMNS) + rows * COLUMNS : selected_index - COLUMNS);

							if (next_index == selected_character)
							{
								return;
							}

							if (next_index >= index_total)
							{
								button_pressed(next_index - COLUMNS + Buttons.CHARACTER_SLOT0);
							}
							else
							{
								button_pressed(next_index + Buttons.CHARACTER_SLOT0);
							}
						}
						else if (keycode == (int)KeyAction.Id.DOWN)
						{
							byte next_index = (selected_index + COLUMNS >= index_total != 0 ? current_col : selected_index + COLUMNS);

							if (next_index == selected_character)
							{
								return;
							}

							if (next_index > index_total)
							{
								button_pressed(next_index + COLUMNS + Buttons.CHARACTER_SLOT0);
							}
							else
							{
								button_pressed(next_index + Buttons.CHARACTER_SLOT0);
							}
						}
						else if (keycode == (int)KeyAction.Id.LEFT)
						{
							if (selected_index != 0)
							{
								selected_index--;

								if (selected_index >= (selected_page + 1) * PAGESIZE - PAGESIZE)
								{
									button_pressed(selected_index + Buttons.CHARACTER_SLOT0);
								}
								else
								{
									button_pressed(Buttons.PAGELEFT);
								}
							}
						}
						else if (keycode == (int)KeyAction.Id.RIGHT)
						{
							if (selected_index != characters_count - 1)
							{
								selected_index++;

								if (selected_index < index_total)
								{
									button_pressed(selected_index + Buttons.CHARACTER_SLOT0);
								}
								else
								{
									button_pressed(Buttons.PAGERIGHT);
								}
							}
						}
					}
				}
			}
		}

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: UIElement::Type get_type() const override
		public override UIElement.Type get_type()
		{
			return TYPE;
		}

		public void add_character(CharEntry & character)
		{
			charlooks.emplace_back(character.look);
			nametags.emplace_back(nametag, Text.Font.A13M, character.stats.name);
			characters.emplace_back(std::forward<CharEntry>(character));

			characters_count++;
		}
		public void post_add_character()
		{
			bool page_matches = (characters_count - 1) / PAGESIZE == selected_page;

			if (!page_matches)
			{
				button_pressed(Buttons.PAGERIGHT);
			}

			update_buttons();

			if (characters_count > 1)
			{
				select_last_slot();
			}
			else
			{
				update_selected_character();
			}

			makeactive();

			charslotlabel.change_text(get_slot_text());
		}
		public void remove_character(int id)
		{
			for (uint i = 0; i < characters.Count; i++)
			{
				if (characters[i].id == id)
				{
					charlooks.RemoveAt(i);
					nametags.RemoveAt(i);
					characters.RemoveAt(i);

					characters_count--;

					if (selected_page > 0)
					{
						bool page_matches = (characters_count - 1) / PAGESIZE == selected_page;

						if (!page_matches)
						{
							button_pressed(Buttons.PAGELEFT);
						}
					}

					update_buttons();

					if (selected_character < characters_count)
					{
						update_selected_character();
					}
					else
					{
						select_last_slot();
					}

					return;
				}
			}
		}
		*/

		/*public CharEntry get_character(int id)
		{
			foreach (CharEntry character in characters)
			{
				if (character.id == id)
				{
					return character;
				}
			}

			Console.Write("Invalid character id: [");
			Console.Write(id);
			Console.Write("]");
			Console.Write("\n");

			CharEntry null_character = new CharEntry({}, {}, 0);

			return null_character;
		}*/

		/*protected override Button.State button_pressed(ushort buttonid)
		{
			switch (buttonid)
			{
				case Buttons.CHARACTER_SELECT:
				{
					if (characters.Count > 0)
					{
						Setting<DefaultCharacter>.get().save(selected_character);
						InfoLabel.int id = characters[selected_character].id;

						switch (require_pic)
						{
							case 0:
							{
								System.Action onok = () =>
								{
									request_pic();
								};

								UI.get().emplace<UILoginNotice>(UILoginNotice.Message.PIC_REQ, onok);
								break;
							}
							case 1:
							{
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: System.Action<const string&> onok = [id](const string& pic)
								System.Action<GlobalMembers.string> onok = (GlobalMembers.string pic) =>
								{
									SelectCharPicPacket(pic, id).dispatch();
								};

								UI.get().emplace<UISoftKey>(onok);
								break;
							}
							case 2:
							{
								SelectCharPacket(id).dispatch();
								break;
							}
						}
					}

					break;
				}
				case Buttons.CHARACTER_NEW:
				{
					Sound(Sound.Name.SCROLLUP).play();

					deactivate();

					tab_index = 0;
					tab_active = false;
					tab_move = false;
					tab_move_pos = 0;

					UI.get().emplace<UIRaceSelect>();
					break;
				}
				case Buttons.CHARACTER_DELETE:
				{
					InfoLabel.int id = characters[selected_character].id;

					switch (require_pic)
					{
						case 0:
						{
							System.Action onok = () =>
							{
								charslotlabel.change_text(get_slot_text());
							};

							UI.get().emplace<UILoginNotice>(UILoginNotice.Message.CHAR_DEL_FAIL_NO_PIC, onok);
							break;
						}
						case 1:
						{
							System.Action oncancel = () =>
							{
								charslotlabel.change_text(get_slot_text());
							};

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 TODO TASK: Only lambda expressions having all locals passed by reference can be converted to C#:
//ORIGINAL LINE: System.Action onok = [&, id, oncancel]()
							System.Action onok = () =>
							{
								System.Action<string> onok = (string pic) =>
								{
									DeleteCharPicPacket(pic, id).dispatch();
									charslotlabel.change_text(get_slot_text());
								};

								UI.get().emplace<UISoftKey>(onok, oncancel);
							};

							StatsEntry character_stats = characters[selected_character].stats;
							ushort cjob = character_stats.stats[(int)MapleStat.Id.JOB];

							if (cjob < 1000)
							{
								UI.get().emplace<UILoginNotice>(UILoginNotice.Message.DELETE_CONFIRMATION, onok, oncancel);
							}
							else
							{
								UI.get().emplace<UILoginNotice>(UILoginNotice.Message.CASH_ITEMS_CONFIRM_DELETION, onok);
							}

							break;
						}
						case 2:
						{
							DeleteCharPacket(id).dispatch();
							charslotlabel.change_text(get_slot_text());
							break;
						}
					}

					break;
				}
				case Buttons.PAGELEFT:
				{
					byte previous_page = selected_page;

					if (selected_page > 0)
					{
						selected_page--;
					}
					else if (characters_count > PAGESIZE)
					{
						selected_page = page_count - 1;
					}

					if (previous_page != selected_page)
					{
						update_buttons();
					}

					select_last_slot();
					break;
				}
				case Buttons.PAGERIGHT:
				{
					byte previous_page = selected_page;

					if (selected_page < page_count - 1)
					{
						selected_page++;
					}
					else
					{
						selected_page = 0;
					}

					if (previous_page != selected_page)
					{
						update_buttons();

						button_pressed(Buttons.CHARACTER_SLOT0);
					}

					break;
				}
				case Buttons.CHANGEPIC:
				{
					break;
				}
				case Buttons.RESETPIC:
				{
					string url = Configuration.get().get_resetpic();

					ShellExecuteA(null, "open", url, null, null, SW_SHOWNORMAL);
					break;
				}
				case Buttons.EDITCHARLIST:
				{
					break;
				}
				case Buttons.BACK:
				{
					deactivate();

					Sound(Sound.Name.SCROLLUP).play();

					if (auto worldselect = UI.get().get_element<UIWorldSelect>())
					{
						worldselect.makeactive();
					}

					break;
				}
				default:
				{
					if (buttonid >= Buttons.CHARACTER_SLOT0)
					{
						byte previous_character = selected_character;
						selected_character = buttonid - Buttons.CHARACTER_SLOT0 + selected_page * PAGESIZE;

						if (previous_character != selected_character)
						{
							if (previous_character < characters_count)
							{
								charlooks[previous_character].set_stance(Stance.Id.STAND1);
								nametags[previous_character].set_selected(false);
							}

							if (selected_character < characters_count)
							{
								update_selected_character();
							}
						}
					}

					break;
				}
			}

			return Button.State.NORMAL;
		}

		private void update_buttons()
		{
			for (byte i = 0; i < PAGESIZE; i++)
			{
				byte index = i + selected_page * PAGESIZE;

				if (index < characters_count)
				{
					buttons[(int)Buttons.CHARACTER_SLOT0 + i].set_state(Button.State.NORMAL);
				}
				else
				{
					buttons[(int)Buttons.CHARACTER_SLOT0 + i].set_state(Button.State.DISABLED);
				}
			}

			if (characters_count >= slots)
			{
				buttons[(int)Buttons.CHARACTER_NEW].set_state(Button.State.DISABLED);
			}
			else
			{
				buttons[(int)Buttons.CHARACTER_NEW].set_state(Button.State.NORMAL);
			}

			bool character_found = false;

			for (sbyte i = PAGESIZE - 1; i >= 0; i--)
			{
				byte index = i + selected_page * PAGESIZE;

				if (index < characters_count)
				{
					character_found = true;

					break;
				}
			}

			buttons[(int)Buttons.CHARACTER_SELECT].set_active(character_found);
			buttons[(int)Buttons.CHARACTER_DELETE].set_state(character_found ? Button.State.NORMAL : Button.State.DISABLED);
		}
		private void update_selected_character()
		{
			Sound(Sound.Name.CHARSELECT).play();

			charlooks[selected_character].set_stance(Stance.Id.WALK1);
			nametags[selected_character].set_selected(true);

			StatsEntry character_stats = characters[selected_character].stats;

			namelabel.change_text(character_stats.name);

			for (uint i = 0; i < InfoLabel.NUM_LABELS; i++)
			{
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: infolabels[i].change_text(get_infolabel(i, character_stats));
				infolabels[i].change_text(get_infolabel(i, new ms.StatsEntry(character_stats)));
			}
		}
		private void select_last_slot()
		{
			for (sbyte i = PAGESIZE - 1; i >= 0; i--)
			{
				byte index = i + selected_page * PAGESIZE;

				if (index < characters_count)
				{
					button_pressed(i + Buttons.CHARACTER_SLOT0);

					return;
				}
			}
		}
		private string get_slot_text()
		{
			show_timestamp = true;
			timestamp = 7 * 1000;
			charslot_y = 0;

			return pad_number_with_leading_zero(characters_count) + "/" + pad_number_with_leading_zero(slots);
		}
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string pad_number_with_leading_zero(byte value) const
		private string pad_number_with_leading_zero(byte value)
		{
			string return_val = Convert.ToString(value);
			return_val = return_val.insert(return_val.GetEnumerator(), 2 - return_val.Length, '0');

			return return_val;
		}
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Point<short> get_character_slot_pos(uint index, ushort x_adj, ushort y_adj) const
		private Point<short> get_character_slot_pos(uint index, ushort x_adj, ushort y_adj)
		{
			short x = 125 * (index % 4);
			short y = 200 * (index > 3);

			return new Point<short>(x + x_adj, y + y_adj);
		}
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Point<short> get_infolabel_pos(uint index) const
		private Point<short> get_infolabel_pos(uint index)
		{
			switch (index)
			{
				case InfoLabel.JOB:
					return new Point<short>(73, -71);
				case InfoLabel.STR:
					return new Point<short>(1, -47);
				case InfoLabel.DEX:
					return new Point<short>(1, -24);
				case InfoLabel.INT:
					return new Point<short>(72, -47);
				case InfoLabel.LUK:
					return new Point<short>(72, -24);
				case InfoLabel.NUM_LABELS:
					break;
				default:
					break;
			}

			return new Point<short>();
		}
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string get_infolabel(uint index, StatsEntry character_stats) const
		private string get_infolabel(uint index, StatsEntry character_stats)
		{
			switch (index)
			{
				case InfoLabel.JOB:
					return new Job(character_stats.stats[(int)MapleStat.Id.JOB]).get_name();
				case InfoLabel.STR:
					return Convert.ToString(character_stats.stats[(int)MapleStat.Id.STR]);
				case InfoLabel.DEX:
					return Convert.ToString(character_stats.stats[(int)MapleStat.Id.DEX]);
				case InfoLabel.INT:
					return Convert.ToString(character_stats.stats[MapleStat.Id.INT]);
				case InfoLabel.LUK:
					return Convert.ToString(character_stats.stats[(int)MapleStat.Id.LUK]);
				case InfoLabel.NUM_LABELS:
					break;
				default:
					break;
			}

			return "";
		}
		private void request_pic()
		{
			System.Action<GlobalMembers.string> enterpic = (GlobalMembers.string entered_pic) =>
			{
				System.Action<string> verifypic = (string verify_pic) =>
				{
					if (entered_pic == verify_pic)
					{
						RegisterPicPacket(characters[selected_character].id, entered_pic).dispatch();
					}
					else
					{
						System.Action onreturn = () =>
						{
							request_pic();
						};

						UI.get().emplace<UILoginNotice>(UILoginNotice.Message.PASSWORD_IS_INCORRECT, onreturn);
					}
				};

				UI.get().emplace<UISoftKey>(verifypic, () =>
				{
				}, "Please re-enter your new PIC.", new Point<short>(24, 0));
			};

			UI.get().emplace<UISoftKey>(enterpic, () =>
			{
			}, "Your new PIC must at least be 6 characters long.", new Point<short>(24, 0));
		}

		private static const byte PAGESIZE = 8;

		private enum Buttons : ushort
		{
			CHARACTER_SELECT,
			CHARACTER_NEW,
			CHARACTER_DELETE,
			PAGELEFT,
			PAGERIGHT,
			CHANGEPIC,
			RESETPIC,
			EDITCHARLIST,
			BACK,
			CHARACTER_SLOT0
		}*/

		private List<CharEntry> characters = new List<CharEntry>();
		
		/*private sbyte characters_count;
		private InfoLabel.int slots;
		private sbyte require_pic;
		private Text version = new Text();
		private Point<short> pagepos = new Point<short>();
		private Point<short> worldpos = new Point<short>();
		private Point<short> charinfopos = new Point<short>();
		private byte selected_character;
		private byte selected_page;
		private byte page_count;
		private Texture tab = new Texture();
		private byte tab_index;
		private bool tab_active;
		private bool tab_move;
		private Point<short>[] tab_pos = Arrays.InitializeWithDefaultInstances<Point>(3);
		private short tab_move_pos;
		private SortedDictionary<byte, ushort> tab_map = new SortedDictionary<byte, ushort>();
		private Point<short> world_dimensions = new Point<short>();
		private Animation burning_notice = new Animation();
		private Text burning_count = new Text();
		private List<Sprite> world_sprites = new List<Sprite>();
		private Texture charinfo = new Texture();
		private Texture charslot = new Texture();
		private Texture pagebase = new Texture();
		private Charset pagenumber = new Charset();
		private nl.node pagenumberpos = new nl.node();
		private Texture[] signpost = Arrays.InitializeWithDefaultInstances<Texture>(3);
		private nl.node nametag = new nl.node();
		private Charset levelset = new Charset();
		private OutlinedText namelabel = new OutlinedText();
		private List<CharLook> charlooks = new List<CharLook>();
		private List<NameTag> nametags = new List<NameTag>();
		private Animation emptyslot_effect = new Animation();
		private Texture emptyslot = new Texture();
		private Animation[] selectedslot_effect = Arrays.InitializeWithDefaultInstances<Animation>(2);
		private OutlinedText charslotlabel = new OutlinedText();
		private short timestamp;
		private ushort charslot_y;
		private bool show_timestamp;
		private bool burning_character;

		private enum InfoLabel : byte
		{
			JOB,
			STR,
			DEX,
			int,
			LUK,
			NUM_LABELS
		}

		private OutlinedText[] infolabels = Arrays.InitializeWithDefaultInstances<OutlinedText>((int)UICharSelect.InfoLabel.NUM_LABELS);*/
	}
}








#if USE_NX
#endif