/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 *                                                                         *
 *   This program is distributed in the hope that it will be useful,       *
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of        *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the         *
 *   GNU General Public License for more details.                          *
 *                                                                         *
 *   You should have received a copy of the GNU General Public License     *
 *   along with this program; if not, write to the                         *
 *   Free Software Foundation, Inc.,                                       *
 *   59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.             *
 ***************************************************************************/
// $Id$

#region License
/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is Classless.Hasher - C#/.NET Hash and Checksum Algorithm Library.
 *
 * The Initial Developer of the Original Code is Classless.net.
 * Portions created by the Initial Developer are Copyright (C) 2004 the Initial
 * Developer. All Rights Reserved.
 *
 * Contributor(s):
 *		Jason Simeone (jay@classless.net)
 * 
 * ***** END LICENSE BLOCK ***** */
#endregion

using System;
using System.Collections;

namespace Classless.Hasher {
	/// <summary>Computes the CRC hash for the input data using the managed library.</summary>
	public class CRC : System.Security.Cryptography.HashAlgorithm {
		static private Hashtable lookupTables;

		private CRCParameters parameters;
		private long[] lookup;
		private long checksum;
		private long registerMask;


		/// <summary>Initializes a new instance of the CRC class.</summary>
		/// <param name="param">The parameters to utilize in the CRC calculation.</param>
		public CRC(CRCParameters param) : base() {
			lock (this) {
				if (param == null) { throw new ArgumentNullException("param", "The CRCParameters cannot be null."); }
				parameters = param;
				HashSizeValue = param.Order;

				CRC.BuildLookup(param);
				lookup = (long[])lookupTables[param];
				registerMask = (long)(Math.Pow(2, (param.Order - 8)) - 1);

				Initialize();
			}
		}

		// Pre-build the more popular lookup tables.
		static CRC() {
			lookupTables = new Hashtable();
			BuildLookup(CRCParameters.GetParameters(CRCStandard.CRC32_REVERSED));
		}


		/// <summary>Build the CRC lookup table for a given polynomial.</summary>
		static private void BuildLookup(CRCParameters param) {
			if (lookupTables.Contains(param)) {
				// No sense in creating the table twice.
				return;
			}

			long[] table = new long[256];
			long topBit = (long)1 << (param.Order - 1);
			long widthMask = (((1 << (param.Order - 1)) - 1) << 1) | 1;

			// Build the table.
			for (int i = 0; i < table.Length; i++) {
				table[i] = i;

				if (param.ReflectInput) { table[i] = Reflect((long)i, 8); }
				
				table[i] = table[i] << (param.Order - 8);

				for (int j = 0; j < 8; j++) {
					if ((table[i] & topBit) != 0) {
						table[i] = (table[i] << 1) ^ param.Polynomial;
					} else {
						table[i] <<= 1;
					}
				}

				if (param.ReflectInput) { table[i] = Reflect(table[i], param.Order); }

				table[i] &= widthMask;
			}

			// Add the new lookup table.
			lookupTables.Add(param, table);
		}


		/// <summary>Initializes the algorithm.</summary>
		override public void Initialize() {
			lock (this) {
				State = 0;
				checksum = parameters.InitialValue;
				if (parameters.ReflectInput) {
					checksum = Reflect(checksum, parameters.Order);
				}
			}
		}


		/// <summary>Drives the hashing function.</summary>
		/// <param name="array">The array containing the data.</param>
		/// <param name="ibStart">The position in the array to begin reading from.</param>
		/// <param name="cbSize">How many bytes in the array to read.</param>
		override protected void HashCore(byte[] array, int ibStart, int cbSize) {
			lock (this) {
				for (int i = ibStart; i < (cbSize - ibStart); i++) {
					if (parameters.ReflectInput) {
						checksum = ((checksum >> 8) & registerMask) ^ lookup[(checksum ^ array[i]) & 0xFF];
					} else {
						checksum = (checksum << 8) ^ lookup[((checksum >> (parameters.Order - 8)) ^ array[i]) & 0xFF];
					}
				}
			}
		}


		/// <summary>Performs any final activities required by the hash algorithm.</summary>
		/// <returns>The final hash value.</returns>
		override protected byte[] HashFinal() {
			lock (this) {
				int i, shift, numBytes;
				byte[] temp;

				checksum ^= (uint)parameters.FinalXORValue;

				numBytes = (int)parameters.Order / 8;
				if (((int)parameters.Order - (numBytes * 8)) > 0) { numBytes++; }
				temp = new byte[numBytes];
				for (i = (numBytes - 1), shift = 0; i >= 0; i--, shift += 8) {
					temp[i] = (byte)((checksum >> shift) & 0xFF);
				}

				return temp;
			}
		}


		/// <summary>Reflects the lower bits of the value provided.</summary>
		/// <param name="data">The value to reflect.</param>
		/// <param name="numBits">The number of bits to reflect.</param>
		/// <returns>The reflected value.</returns>
		static private long Reflect(long data, int numBits) {
			long temp = data;

			for (int i = 0; i < numBits; i++) {
				long bitMask = (long)1 << ((numBits - 1) - i);

				if ((temp & (long)1) != 0) {
					data |= bitMask;
				} else {
					data &= ~bitMask;
				}

				temp >>= 1;
			}

			return data;
		}
	}
}
