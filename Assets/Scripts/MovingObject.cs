using System;

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
    // Structure that contains all properties for movement calculations
    public class MovingObject
    {
        public Linear<double> x = new Linear<double>();
        public Linear<double> y = new Linear<double>();
        public double hspeed = 0.0;
        public double vspeed = 0.0;

        public void normalize()
        {
            x.normalize();
            y.normalize();
        }

        public void move()
        {
            x += hspeed;
            y += vspeed;
        }

        public void set_x(double d)
        {
            x.set(d);
        }

        public void set_y(double d)
        {
            y.set(d);
        }

        public void limitx(double d)
        {
            x = d;
            hspeed = 0.0;
        }

        public void limity(double d)
        {
            y = d;
            vspeed = 0.0;
        }

        public void movexuntil(double d, ushort delay)
        {
            if (delay != 0)
            {
                double hdelta = d - x.get();
                hspeed = GlobalMembers.TIMESTEP * hdelta / delay;
            }
        }

        public void moveyuntil(double d, ushort delay)
        {
            if (delay != 0)
            {
                double vdelta = d - y.get();
                vspeed = GlobalMembers.TIMESTEP * vdelta / delay;
            }
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: bool hmobile() const
        public bool hmobile()
        {
            return hspeed != 0.0;
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: bool vmobile() const
        public bool vmobile()
        {
            return vspeed != 0.0;
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: bool mobile() const
        public bool mobile()
        {
            return hmobile() || vmobile();
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: double crnt_x() const
        public double crnt_x()
        {
            return x.get();
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: double crnt_y() const
        public double crnt_y()
        {
            return y.get();
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: double next_x() const
        public double next_x()
        {
            return hspeed+x;
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: double next_y() const
        public double next_y()
        {
            return y + vspeed;
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: short get_x() const
        public short get_x()
        {
            double rounded = Math.Round(x.get());
            return (short)rounded;
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: short get_y() const
        public short get_y()
        {
            double rounded = Math.Round(y.get());
            return (short)rounded;
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: short get_last_x() const
        public short get_last_x()
        {
            double rounded = Math.Round(x.last());
            return (short)rounded;
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: short get_last_y() const
        public short get_last_y()
        {
            double rounded = Math.Round(y.last());
            return (short)rounded;
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: Point<short> get_position() const
        public Point<short> get_position()
        {
            return new Point<short> (get_x(), get_y());
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: short get_absolute_x(double viewx, float alpha) const
        public short get_absolute_x(double viewx, float alpha)
        {
            double interx = x.normalized() ? Math.Round(x.get()) : x.get(alpha);

            return (short)Math.Round(interx + viewx);
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: short get_absolute_y(double viewy, float alpha) const
        public short get_absolute_y(double viewy, float alpha)
        {
            double intery = y.normalized() ? Math.Round(y.get()) : y.get(alpha);

            return (short)Math.Round(intery + viewy);
        }

        //C++ TO C# CONVERTER CRACKED BY X-CRACKER 2017 WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: Point<short> get_absolute(double viewx, double viewy, float alpha) const
        public Point<short> get_absolute(double viewx, double viewy, float alpha)
        {
            return new Point<short> (get_absolute_x(viewx, alpha), get_absolute_y(viewy, alpha));
        }
    }
}