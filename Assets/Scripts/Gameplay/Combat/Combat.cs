﻿using System.Collections.Generic;
using SD.Tools.Algorithmia.GeneralDataStructures;

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
	/// <summary>
	///  Draw bullets, damage numbers etc.void draw(double viewx, double viewy, float alpha);Tangible Method Implementation Not Foundms.Combat-Combat Poll attacks, damage effects, etc.
	/// </summary>
	public class Combat
	{
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Combat(Player& in_player, MapChars& in_chars, MapMobs& in_mobs, MapReactors& in_reactors) : player(in_player), chars(in_chars), mobs(in_mobs), reactors(in_reactors), attackresults([&](const AttackResult& attack)
		public Combat (Player in_player /*, MapChars in_chars*/, MapMobs in_mobs /*, MapReactors in_reactors*/)
		{
			//player = in_player;
			//chars =in_chars);
			mobs = in_mobs;
			attackresults = new TimedQueue<AttackResult> ( apply_attack);
			bulleteffects = new TimedQueue<BulletEffect> ( apply_bullet_effect);
			damageeffects = new TimedQueue<DamageEffect> ( apply_damage_effect);
			//reactors =in_reactors;
			/*attackresults ((AttackResult attack) =>
			{
				apply_attack (attack);
			};*/
		}

		public void draw(double viewx, double viewy, float alpha) 
		{
			foreach (var be in bullets)
			{
				be.bullet.draw(viewx, viewy, alpha);
			}
			foreach (var dn in damagenumbers)
			{
				dn.draw(viewx, viewy, alpha);
			}
		}
		
		public void update ()
		{
			attackresults.update ();
			bulleteffects.update ();
			damageeffects.update ();

			bullets.remove_if ((BulletEffect mb) =>
			{
				int target_oid = mb.damageeffect.target_oid;

				if (mobs.contains (target_oid))
				{
					mb.target = (mobs.get_mob_head_position (target_oid));
					bool apply = mb.bullet.update (mb.target);

					if (apply)
					{
						apply_damage_effect (mb.damageeffect);
					}

					return apply;
				}
				else
				{
					return mb.bullet.update (mb.target);
				}
			});

			damagenumbers.remove_if ((DamageNumber dn) => dn.update ());
		}

		// Make the player use a special move
		public void use_move (int move_id)
		{
			if (!player.can_attack ())
			{
				return;
			}

			SpecialMove move = get_move (move_id);
			SpecialMove.ForbidReason reason = player.can_use (move);
			Weapon.Type weapontype = player.get_stats ().get_weapontype ();

			switch (reason)
			{
				case SpecialMove.ForbidReason.FBR_NONE:
					apply_move (move);
					break;
				default:
					//ForbidSkillMessage(reason, weapontype).drop();
					break;
			}
		}

		// Add an attack to the attack queue
		public void push_attack (AttackResult attack)
		{
			attackresults.push (400, attack);
		}
		/*// Show a buff effect
		public void show_buff(int cid, int skillid, sbyte level)
		{
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 TODO TASK: Variables cannot be declared in if/while/switch conditions in C#:
			if (Optional<OtherChar> ouser = chars.get_char(cid))
			{
				OtherChar user = ouser;
				user.update_skill(skillid, level);

				SpecialMove move = get_move(skillid);
				move.apply_useeffects(ref user);
				move.apply_actions(ref user, Attack.Type.MAGIC);
			}
		}
		// Show a buff effect
		public void show_player_buff(int skillid)
		{
			get_move(skillid).apply_useeffects(ref player);
		}*/

		private struct DamageEffect
		{
			public AttackUser user;
			public DamageNumber number;
			public int damage;
			public bool toleft;
			public int target_oid;
			public int move_id;

			public DamageEffect (AttackUser user, DamageNumber number, int damage, bool toleft, int target_oid, int move_id)
			{
				this.user = new AttackUser ();
				this.user = user;
				this.number = number;
				this.damage = damage;
				this.toleft = toleft;
				this.target_oid = target_oid;
				this.move_id = move_id;
			}
		}

		private struct BulletEffect
		{
			public DamageEffect damageeffect;
			public Bullet bullet;
			public Point<short> target;

			public BulletEffect (DamageEffect damageeffect, Bullet bullet, Point<short> target)
			{
				this.damageeffect = damageeffect;
				this.bullet = bullet;
				this.target = target;
			}
		}

		private void apply_attack (AttackResult attack)
		{
			/*Optional < OtherChar > ouser = chars.get_char (attack.attacker);
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 TODO TASK: Variables cannot be declared in if/while/switch conditions in C#:
			if (ouser)
			{
				OtherChar user = ouser;
				user.update_skill (attack.skill, attack.level);
				user.update_speed (attack.speed);

				SpecialMove move = get_move (attack.skill);
				move.apply_useeffects ( user);

				Stance.Id stance = Stance.by_id (attack.stance);
				if (stance!=0)
				{
					user.attack (stance);
				}
				else
				{
					move.apply_actions ( user, attack.type);
				}

				user.set_afterimage (attack.skill);

				extract_effects (user, move, attack);
			}*/
		}

		private void apply_move (SpecialMove move)
		{
			if (move.is_attack ())
			{
				Attack attack = player.prepare_attack (move.is_skill ());

				move.apply_useeffects (player);
				move.apply_actions (player, attack.type);

				player.set_afterimage (move.get_id ());

				move.apply_stats (player, attack);


				Point<short> origin = attack.origin;
				Rectangle<short> range = attack.range;
				short hrange = (short)(range.left () * attack.hrange);

				if (attack.toleft)
				{
					range = new Rectangle<short> ((short)(origin.x () + hrange), (short)(origin.x () + range.right ()), (short)(origin.y () + range.top ()), (short)(origin.y () + range.bottom ()));
				}
				else
				{
					range = new Rectangle<short> ((short)(origin.x () - range.right ()), (short)(origin.x () - hrange), (short)(origin.y () + range.top ()), (short)(origin.y () + range.bottom ()));
				}

				// This approach should also make it easier to implement PvP
				byte mobcount = attack.mobcount;
				AttackResult result = new AttackResult (attack);

				MapObjects mob_objs = mobs.get_mobs ();
				//MapObjects reactor_objs = reactors.get_reactors ();

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: ClassicVector<int> mob_targets = find_closest(mob_objs, range, origin, mobcount, true);
				List<int> mob_targets = find_closest (mob_objs, range, origin, mobcount, true);
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: ClassicVector<int> reactor_targets = find_closest(reactor_objs, range, origin, mobcount, false);
				//List<int> reactor_targets = find_closest (reactor_objs, new ms.Rectangle (new ms.Rectangle (range)), new ms.Point (new ms.Point (origin)), mobcount, false);

				mobs.send_attack (result, attack, mob_targets, mobcount);
				result.attacker = player.get_oid ();
				extract_effects (player, move, result);

				apply_use_movement (move);
				apply_result_movement (move, result);

				//AttackPacket (result).dispatch ();

				/*if (reactor_targets.Count != 0)
				{
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 TODO TASK: Variables cannot be declared in if/while/switch conditions in C#:
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: if (Optional<Reactor> reactor = reactor_objs->get(reactor_targets.at(0)))
					if (Optional < Reactor > GlobalMembers.reactor = reactor_objs.get (new List (reactor_targets[0])))
					{
						DamageReactorPacket (GlobalMembers.reactor.get_oid (), player.get_position (), 0, 0).dispatch ();
					}
				}*/
			}
			else
			{
				move.apply_useeffects (player);
				move.apply_actions (player, Attack.Type.MAGIC);

				int moveid = move.get_id ();
				//int level = player.get_skills ().get_level (moveid);
				//UseSkillPacket (moveid, level).dispatch ();
			}
		}

		MultiValueDictionary<ushort, int> distances = new MultiValueDictionary<ushort, int> ();

		List<int> targets = new List<int> ();

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ClassicVector<int> find_closest(MapObjects* objs, Rectangle<short> range, Point<short> origin, byte objcount, bool use_mobs) const
		private List<int> find_closest (MapObjects objs, Rectangle<short> range, Point<short> origin, byte objcount, bool use_mobs)
		{
			foreach (var mmo in objs)
			{
				if (use_mobs)
				{
					Mob mob = (Mob)mmo.Value;

					if (mob != null && mob.is_alive () && mob.is_in_range (range))
					{
						int oid = mob.get_oid ();
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: ushort distance = mob->get_position().distance(origin);
						ushort distance = (ushort)mob.get_position ().distance (origin);
						distances.Add (distance, oid);
					}
				}
				else
				{
					/*// Assume Reactor
					Reactor reactor = (Reactor)mmo.second.get ();

					if (reactor != null && reactor.is_hittable () && reactor.is_in_range (range))
					{
						int oid = reactor.get_oid ();
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: The following line was determined to be a copy constructor call - this should be verified and a copy constructor should be created if it does not yet exist:
//ORIGINAL LINE: ushort distance = reactor->get_position().distance(origin);
						ushort distance = reactor.get_position ().distance (new ms.Point (origin));
						distances.emplace (distance, oid);
					}*/
				}
			}

			targets.Clear ();
			foreach (var iter in distances)
			{
				if (targets.Count >= objcount)
				{
					break;
				}

				targets.AddRange (iter.Value);
			}

			return targets;
		}

		private void apply_use_movement (SpecialMove move)
		{
			switch ((SkillId.Id)move.get_id ())
			{
				case SkillId.Id.TELEPORT_FP:
				case SkillId.Id.IL_TELEPORT:
				case SkillId.Id.PRIEST_TELEPORT:
				case SkillId.Id.FLASH_JUMP:
				default:
					break;
			}
		}

		private void apply_result_movement (SpecialMove move, AttackResult result)
		{
			switch ((SkillId.Id)move.get_id ())
			{
				case SkillId.Id.RUSH_HERO:
				case SkillId.Id.RUSH_PALADIN:
				case SkillId.Id.RUSH_DK:
					apply_rush (result);
					break;
				default:
					break;
			}
		}

		private void apply_rush (AttackResult result)
		{
			if (result.mobcount == 0)
			{
				return;
			}

			Point<short> mob_position = mobs.get_mob_position (result.last_oid);
			short targetx = mob_position.x ();
			player.rush (targetx);
		}

		private void apply_bullet_effect (BulletEffect effect)
		{
			bullets.AddLast (effect);

			if (bullets.Last.Value.bullet.settarget (effect.target))
			{
				apply_damage_effect (effect.damageeffect);
				bullets.RemoveLast ();
			}
		}

		private void apply_damage_effect (DamageEffect effect)
		{
			Point<short> head_position = mobs.get_mob_head_position (effect.target_oid);
			damagenumbers.AddLast (effect.number);
			damagenumbers.Last.Value.set_x (head_position.x ());

			SpecialMove move = get_move (effect.move_id);
			mobs.apply_damage (effect.target_oid, effect.damage, effect.toleft, effect.user, move);
		}

		private void extract_effects (Char user, SpecialMove move, AttackResult result)
		{
			AttackUser attackuser = new AttackUser (user.get_skilllevel (move.get_id ()), user.get_level (), user.is_twohanded (), !result.toleft);

			if (result.bullet != 0)
			{
				Bullet bullet = new Bullet (move.get_bullet (user, result.bullet), user.get_position (), result.toleft);

				foreach (var line in result.damagelines)
				{
					int oid = line.Key;

					if (mobs.contains (oid))
					{
						List<DamageNumber> numbers = place_numbers (oid, line.Value);
						Point<short> head = mobs.get_mob_head_position (oid);

						uint i = 0;

						foreach (var number in numbers)
						{
							DamageEffect effect = new DamageEffect (attackuser, number, line.Value[(int)i].Item1, result.toleft, oid, move.get_id ());
							bulleteffects.push (user.get_attackdelay (i), new BulletEffect (effect, bullet, head));
							i++;
						}
					}
				}

				if (result.damagelines.Count == 0)
				{
					short xshift = (short)(result.toleft ? -400 : 400);
					Point<short> target = user.get_position () + new Point<short> (xshift, -26);

					for (byte i = 0; i < result.hitcount; i++)
					{
						DamageEffect effect = new DamageEffect (attackuser, new DamageNumber (), 0, false, 0, 0);
						bulleteffects.push (user.get_attackdelay (i), new BulletEffect (effect, bullet, target));
					}
				}
			}
			else
			{
				foreach (var line in result.damagelines)
				{
					int oid = line.Key;

					if (mobs.contains (oid))
					{
						List<DamageNumber> numbers = place_numbers (oid, line.Value);

						uint i = 0;

						foreach (var number in numbers)
						{
							damageeffects.push (user.get_attackdelay (i), new DamageEffect (attackuser, number, line.Value[(int)i].Item1, result.toleft, oid, move.get_id ()));
							i++;
						}
					}
				}
			}
		}

		private List<DamageNumber> place_numbers (int oid, List<System.Tuple<int, bool>> damagelines)
		{
			List<DamageNumber> numbers = new List<DamageNumber> ();
			short head = mobs.get_mob_head_position (oid).y ();

			foreach (var line in damagelines)
			{
				int amount = line.Item1;
				bool critical = line.Item2;
				DamageNumber.Type type = critical ? DamageNumber.Type.CRITICAL : DamageNumber.Type.NORMAL;
				numbers.Add (new DamageNumber (type, amount, head));

				head -= DamageNumber.rowheight (critical);
			}

			return numbers;
		}

		private SpecialMove get_move (int move_id)
		{
			if (move_id == 0)
			{
				return regularattack;
			}

			if (!skills.TryGetValue (move_id, out var skill))
			{
				skill = new Skill (move_id);
				skills.Add (move_id, skill);
			}

			return skill;
		}

		private Player player => Stage.get ().get_player ();

		//private MapChars chars;
		private MapMobs mobs;
		//private MapReactors reactors;

		private Dictionary<int, Skill> skills = new Dictionary<int, Skill> ();
		private RegularAttack regularattack = new RegularAttack ();

		private TimedQueue<AttackResult> attackresults;
		private TimedQueue<BulletEffect> bulleteffects;
		private TimedQueue<DamageEffect> damageeffects;

		private LinkedList<BulletEffect> bullets = new LinkedList<BulletEffect> ();
		private LinkedList<DamageNumber> damagenumbers = new LinkedList<DamageNumber> ();
	}
}


//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: void Combat::draw(double viewx, double viewy, float alpha) const