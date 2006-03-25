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
using System;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces;
using SimPe.PackedFiles.Wrapper.Supporting;
using SimPe.Data;
using System.Collections;

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// An Item as stored in a Scor Resource
	/// </summary>
	public class ScorItem
	{			
		string name;
		public string Name
		{
			get {return name;}
			set {name = value;}
		}
		Scor parent;
		
		byte[] data;
		/// <summary>
		/// Constructor
		/// </summary>
		public ScorItem(string name, Scor parent) : this(parent)
		{
			if (name==null) name="";
			this.name = name;
		}

		internal ScorItem(Scor parent) 
		{
			this.data = new byte[0];
			this.parent = parent;		
		}
						
		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		internal void Unserialize(System.IO.BinaryReader reader)
		{
			name = StreamHelper.ReadString(reader);			
			if (reader.BaseStream.Position > reader.BaseStream.Length-1) return;
			System.Collections.ArrayList bytes = new ArrayList();
			
			byte test = reader.ReadByte();			
			byte last = test;
			while (last!=0x00 || test!=0x04) 
			{				
				bytes.Add(test);
				if (reader.BaseStream.Position > reader.BaseStream.Length-1) break;
				last = test;
				test = reader.ReadByte();
			}		
	
			if (reader.BaseStream.Position <= reader.BaseStream.Length-1)
				if (bytes.Count>0) 
					bytes.RemoveAt(bytes.Count-1);

			data = new byte[bytes.Count];
			bytes.CopyTo(data);
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		internal  void Serialize(System.IO.BinaryWriter writer, bool last)
		{
			StreamHelper.WriteString(writer, name);
			writer.Write(data);
			if (!last) writer.Write((ushort)0x0400);			
		}		

		public override string ToString()
		{
			if (Helper.WindowsRegistry.HiddenMode) 
			{
				string s = "";
				s += Helper.BytesToHexList(data);
				s +=  " [" + Name + "]";
				return s;
			} 
			else 
			{
				string s = Name+ " [";
				s += Helper.BytesToHexList(data);
				s += "]";
				return s;
			}
		}

	}

	public class ScorItems : System.Collections.IEnumerable, System.IDisposable
	{
		ArrayList list;
		public ScorItems()
		{
			list = new ArrayList();
		}

		public void Add(ScorItem si)
		{
			list.Add(si);
		}

		public void Remove(ScorItem si)
		{
			list.Remove(si);
		}

		public void Clear()
		{
			list.Clear();
		}

		public int Count
		{
			get {return list.Count;}
		}

		public bool Contains(ScorItem si)
		{
			return list.Contains(si);
		}

		protected int FindIndex(string name)
		{
			for (int i=0; i< list.Count; i++)
				if (this[i].Name==name) return i;

			return -1;
		}

		public ScorItem this[string name]
		{
			get { return list[FindIndex(name)] as ScorItem;}
			set 
			{
				list[FindIndex(name)] = value;
			}
		}

		public ScorItem this[int index]
		{
			get { return list[index] as ScorItem;}
			set 
			{
				list[index] = value;
			}
		}

		#region IEnumerable Member

		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		#endregion

		#region IDisposable Member

		public void Dispose()
		{
			list.Clear();
			list = null;
		}

		#endregion
	}

}
