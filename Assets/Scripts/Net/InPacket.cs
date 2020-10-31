﻿using System;
using System.Linq;
using ms.Helper;

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
	// A packet received from the server
	// Contains reading functions
	public class InPacket
	{
		private MMO_MemoryStream _memoryStream;

		// Construct a packet from an array of bytes
		public InPacket (byte[] recv, int length)
		{
			arr = recv;
			top = length;
			pos = 0;
			_memoryStream = new MMO_MemoryStream (recv);
			_memoryStream.Position = 0;
		}

		// Check if there are more bytes available
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool available() const
		public bool available ()
		{
			return length () > 0;
		}

		// Return the remaining length in bytes
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint length() const
		public int length ()
		{
			return top - pos;
		}

		// Skip a number of bytes (by increasing the offset)
		public void skip (int count)
		{
			if (count > length ())
			{
				throw new PacketError ("Stack underflow at " + Convert.ToString (pos));
			}

			_memoryStream.Position += count;
			pos += count;
		}

		// Read a byte and check if it is equal to one
		public bool read_bool ()
		{
			return readByte () == 1;
			//return _memoryStream.ReadBool ();
			return read_byte () == 1;
		}

		// Read a byte
		public sbyte read_byte ()
		{
			return unchecked((sbyte)readByte ());
			//return _memoryStream.ReadByte ().ToSByte ();
			return read<sbyte> ();
		}

		// Read a short
		public short read_short ()
		{
			return readShort ();
			//return _memoryStream.ReadShort ();
			return read<short> ();
		}

		// Read a int
		public int read_int ()
		{
			return readInt ();
			//return _memoryStream.ReadInt ();
			return read<int> ();
		}

		// Read a long
		public long read_long ()
		{
			return readLong ();
			//return _memoryStream.ReadLong ();
			return read<long> ();
		}

		// Read a point
		public Point<short> read_point ()
		{
			short x = readShort ();
			short y = readShort ();
			/*short x = read<short> ();
			short y = read<short> ();*/

			return new Point<short> (x, y);
			//short x = _memoryStream.ReadShort ();
			//short y = _memoryStream.ReadShort ();
			return new Point<short> (x, y);
		}

		// Read a string
		public string read_string ()
		{
			short length = readShort ();
			//ushort length = read<ushort> ();

			return read_padded_string (length);
			//var length = _memoryStream.ReadUShort ();

			return read_padded_string (length);
		}

		// Read a fixed-length string
		public string read_padded_string (short count)
		{
			string ret = string.Empty;

			for (short i = 0; i < count; i++)
			{
				var letter = read_byte ();
				//var letter = _memoryStream.ReadByte ();

				if (letter != (sbyte)'\0')
				{
					ret = string.Concat (ret, letter);
				}
			}

			return ret;
		}

		// Skip a byte
		public void skip_bool ()
		{
			skip_byte ();
		}

		// Skip a byte
		public void skip_byte ()
		{
			skip (sizeof (sbyte));
		}

		// Skip a short
		public void skip_short ()
		{
			skip (sizeof (short));
		}

		// Skip a int
		public void skip_int ()
		{
			skip (sizeof (int));
		}

		// Skip a long
		public void skip_long ()
		{
			skip (sizeof (long));
		}

		// Skip a point
		public void skip_point ()
		{
			skip (sizeof (short));
			skip (sizeof (short));
		}

		// Skip a string
		public void skip_string ()
		{
			ushort length = read<ushort> ();

			skip_padded_string (length);
		}

		// Skip a fixed-length string
		public void skip_padded_string (ushort length)
		{
			skip (length);
		}

		// Inspect a byte and check if it is 1. Does not advance the buffer position.
		public bool inspect_bool ()
		{
			return inspect_byte () == 1;
		}

		// Inspect a byte. Does not advance the buffer position.
		public sbyte inspect_byte ()
		{
			return inspect<sbyte> ();
		}

		// Inspect a short. Does not advance the buffer position.
		public short inspect_short ()
		{
			return inspect<short> ();
		}

		// Inspect an int. Does not advance the buffer position.
		public int inspect_int ()
		{
			return inspect<int> ();
		}

		// Inspect a long. Does not advance the buffer position.
		public long inspect_long ()
		{
			return inspect<long> ();
		}

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
		// Read a number and advance the buffer position
		private T read<T> () where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
		{
//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: This 'sizeof' ratio was replaced with a direct reference to the array length:
//ORIGINAL LINE: uint count = sizeof(T) / sizeof(sbyte);
			int count = (int)(Utils.SizeOf<T> (default) / sizeof (sbyte));
			//uint count = T.Length;
			var all = 0;
			/*long tempResult = 0;
			if (count == 1) //sbyte
			{
				tempResult = bytes[pos];
			}
			else if (count == 2) //short
			{
				tempResult = Convert.ToInt16 (bytes[pos]);
			}
			else if (count == 4) //int
			{
				tempResult = Convert.ToInt32 (bytes);
			}
			else if (count == 8) //long
			{
				tempResult = Convert.ToInt64 (bytes[pos]);
			}

			skip (count);
			return tempResult.ToT<T> ();*/
			/*for (int i = 0; i < count; i++)
			{
				/*T val = (dynamic)bytes[(int)pos];
				all += (dynamic)val << (8 * i);#1#

				var val = bytes[pos];
				int temp;
				if (i == 0 && count > 1 && val < 0) //only convert except sbyte with first bit
				{
					temp = val + 256;
				}
				else
				{
					temp = val;
				}

				all += temp << (8 * i);

				skip (1);
			}*/

			if (count == 1) //it's byte
			{
				var tempByte = arr[pos].ToT<T> ();
				skip (1);
				return tempByte;
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					var val = (byte)(arr[pos]);
					all += (val << (8 * i));

					skip (1);
				}

				/*//ulong tempResult = 0;
				if (count == 1) //sbyte
				{
					var tempResult = Convert.ToSByte (all > (ulong)SByte.MaxValue ? all - Byte.MaxValue : all);
					return tempResult.ToT<T> ();
				}
				else if (count == 2) //short
				{
					var tempResult = Convert.ToInt16 (all > (ulong)Int16.MaxValue ? all - UInt16.MaxValue : all);
					return tempResult.ToT<T> ();
				}
				else if (count == 4) //int
				{
					var tempResult = Convert.ToInt32 (all > Int32.MaxValue ? all - UInt32.MaxValue : all);
					return tempResult.ToT<T> ();
				}
				else if (count == 8) //long
				{
					var tempResult = Convert.ToInt64 (all > Int64.MaxValue ? all - UInt64.MaxValue : all);
					return tempResult.ToT<T> ();
				}*/

				return all.ToT<T> ();
			}
		}

//C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <typename T>
		// Read without advancing the buffer position
		private T inspect<T> () where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
		{
			int before = pos;
			T value = read<T> ();
			pos = before;

			return value;
		}

		public readonly byte[] arr;
		private int top;
		private int pos;
		private long bytesRead = 0;

		public byte readByte ()
		{
			return unchecked ((byte)readByteInternal ());
		}

		public int readInt ()
		{
			return unchecked (readByte () + (readByte () << 8) + (readByte () << 16) + (readByte () << 24));
		}

		/**
     * Reads a short integer from the stream.
     *
     * @return The short read.
     */
		public short readShort ()
		{
			return unchecked ((short)(readByte () + (readByte () << 8)));
		}

		/**
     * Reads a single character from the stream.
     *
     * @return The character read.
     */
		public char readChar ()
		{
			return (char)readShort ();
		}

		/**
     * Reads a long integer from the stream.
     *
     * @return The long integer read.
     */
		public long readLong ()
		{
			long byte1 = readByte ();
			long byte2 = readByte ();
			long byte3 = readByte ();
			long byte4 = readByte ();
			long byte5 = readByte ();
			long byte6 = readByte ();
			long byte7 = readByte ();
			long byte8 = readByte ();
			return unchecked ((byte8 << 56) + (byte7 << 48) + (byte6 << 40) + (byte5 << 32) + (byte4 << 24) + (byte3 << 16) + (byte2 << 8) + byte1);
		}

		private int readByteInternal ()
		{
			bytesRead++;
			return arr[pos++] & 0xFF;
			return ((int)arr[pos++]) & 0xFF;
		}
	}
}