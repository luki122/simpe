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
using System.Drawing;
using System.IO;
using SimPe;

namespace SimPe.Cache
{
	/// <summary>
	/// What general class is the Object in
	/// </summary>
	/// 
	public enum ObjectClass : byte
	{
		/// <summary>
		/// It is a real Object (OBJd-Based)
		/// </summary>
		Object = 0x00,
		/// <summary>
		/// It something like a Skin (cpf based)
		/// </summary>
		Skin = 0x01
	}
	/// <summary>
	/// Contains one ObjectCacheItem
	/// </summary>
	public class ObjectCacheItem : ICacheItem
	{
		/// <summary>
		/// The current Version
		/// </summary>
		public const byte VERSION = 3;

		public ObjectCacheItem()
		{			
			version = VERSION;
			name = "";
			modelname = "";
			objname = "";
			use = true;
			pfd = new Packages.PackedFileDescriptor();
		}

		byte version;
		string name;
		string modelname;
		Interfaces.Files.IPackedFileDescriptor pfd;
		uint localgroup;
		Image thumb;
		Data.ObjectTypes objtype;
		string objname;
		short objfuncsort;
		bool use;
		ObjectClass oclass;

		/// <summary>
		/// Returns an (unitialized) FileDescriptor
		/// </summary>
		public Interfaces.Files.IPackedFileDescriptor FileDescriptor
		{
			get { 
				pfd.Tag = this;
				return pfd; 
			}
			set { pfd = value; }
		}

		/// <summary>
		/// Returns the Type Field of the Object
		/// </summary>
		public Data.ObjectTypes ObjectType
		{
			get { return objtype; }
			set { objtype = value; }
		}

		/// <summary>
		/// The class the Object is assigned to
		/// </summary>
		public ObjectClass Class
		{
			get { return oclass; }
			set { oclass = value; }
		}

		/// <summary>
		/// Returns the FunctionSort Field of the Object
		/// </summary>
		public short ObjectFunctionSort
		{
			get { return objfuncsort; }
			set { objfuncsort = value; }
		}

		/// <summary>
		/// Returns the LocalGroup
		/// </summary>
		public uint LocalGroup
		{
			get { return localgroup; }
			set { localgroup = value; }
		}

		/// <summary>
		/// Returns the Name of this Object
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		/// <summary>
		/// Returns the Name of this Object
		/// </summary>
		public string ObjectFileName
		{
			get { return objname; }
			set { objname = value; }
		}

		/// <summary>
		/// Returns the Name of this Object
		/// </summary>
		public bool Useable
		{
			get { return use; }
			set { use = value; }
		}

		/// <summary>
		/// Returns the ModeName for this Object
		/// </summary>
		public string ModelName
		{
			get { return modelname; }
			set { modelname = value; }
		}

		/// <summary>
		/// Returns the Thumbnail
		/// </summary>
		public Image Thumbnail 
		{
			get { return thumb; }
			set { thumb = value; }
		}

		public override string ToString()
		{
			return Name +" (0x"+Helper.HexString(LocalGroup)+")";
		}

		#region ICacheItem Member

		public void Load(System.IO.BinaryReader reader) 
		{
			version = reader.ReadByte();
			if (version>VERSION) throw new CacheException("Unknown CacheItem Version.", null, version);
							
			name = reader.ReadString();
			modelname = reader.ReadString();
			pfd = new Packages.PackedFileDescriptor();
			pfd.Type = reader.ReadUInt32();
			pfd.Group = reader.ReadUInt32();
			localgroup = reader.ReadUInt32();
			pfd.LongInstance = reader.ReadUInt64();
			

			int size = reader.ReadInt32();
			if (size==0) 
			{
				thumb = null;
			} 
			else 
			{
				byte[] data = reader.ReadBytes(size);
				MemoryStream ms = new MemoryStream(data);

				thumb = Image.FromStream(ms);				
			}

			objtype = (Data.ObjectTypes)reader.ReadUInt16();
			objfuncsort = reader.ReadInt16();

			if (version>=2) 
			{
				objname = reader.ReadString();
				use = reader.ReadBoolean();
			} 
			else 
			{
				objname = modelname;
				use = true;
			}

			if (version>=3) oclass = (ObjectClass)reader.ReadByte();
			else oclass = ObjectClass.Object;
		}

		public void Save(System.IO.BinaryWriter writer) 
		{
			version = VERSION;
			writer.Write(version);
			writer.Write(name);
			writer.Write(modelname);			
			writer.Write(pfd.Type);
			writer.Write(pfd.Group);
			writer.Write(localgroup);
			writer.Write(pfd.LongInstance);

			if (thumb==null) 
			{
				writer.Write((int)0);
			} 
			else 
			{
				MemoryStream ms = new MemoryStream();
				thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
				byte[] data = ms.ToArray();
				writer.Write(data.Length);
				writer.Write(data);
			}

			writer.Write((ushort)objtype);
			writer.Write(objfuncsort);

			writer.Write(objname);
			writer.Write(use);

			writer.Write((byte)oclass);
		}

		public byte Version
		{
			get
			{
				return version;
			}
		}

		#endregion
	}
}
