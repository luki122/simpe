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
using System.Collections;
using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
	public class ResourceNodeItem 
	{
		short unknown1;
		public short Unknown1 
		{
			get { return unknown1; }
			set { unknown1 = value; }
		}

		int unknown2;
		public int Unknown2 
		{
			get { return unknown2; }
			set { unknown2= value; }
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public void Unserialize(System.IO.BinaryReader reader)
		{
			unknown1 = reader.ReadInt16();
			unknown2 = reader.ReadInt32();
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		public void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(unknown1);
			writer.Write(unknown2);
		}

		public override string ToString()
		{
			return "0x"+Helper.HexString((ushort)unknown1) + " 0x" + Helper.HexString((uint)unknown2);
		}

	}

	/// <summary>
	/// This is the actual FileWrapper
	/// </summary>
	/// <remarks>
	/// The wrapper is used to (un)serialize the Data of a file into it's Attributes. So Basically it reads 
	/// a BinaryStream and translates the data into some userdefine Attributes.
	/// </remarks>
	public class ResourceNode
		: AbstractRcolBlock
	{
		#region Attributes
		byte typecode;
		public byte TypeCode 
		{
			get { return typecode; }
		}

		ObjectGraphNode ogn;
		public ObjectGraphNode GraphNode 
		{
			get { return ogn; }
		}

		CompositionTreeNode ctn;
		public CompositionTreeNode TreeNode 
		{
			get { return ctn; }
		}

		ResourceNodeItem[] items;
		public ResourceNodeItem[] Items 
		{
			get { return items; }
			set { items = value; }
		}

		int unknown1;
		public int Unknown1 
		{
			get { return unknown1; }
			set { unknown1 = value; }
		}

		int unknown2;
		public int Unknown2 
		{
			get { return unknown2; }
			set { unknown2 = value; }
		}
		#endregion
		

		/// <summary>
		/// Constructor
		/// </summary>
		public ResourceNode(Interfaces.IProviderRegistry provider, Rcol parent) : base(provider, parent)
		{
			sgres = new SGResource(provider, null);
			ogn = new ObjectGraphNode(provider, null);
			ctn = new CompositionTreeNode(provider, null);
			items = new ResourceNodeItem[0];

			version = 0x07;
			typecode = 0x01;
			BlockID = 0xE519C933;
		}
		
		#region IRcolBlock Member

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public override void Unserialize(System.IO.BinaryReader reader)
		{
			version = reader.ReadUInt32();
			typecode = reader.ReadByte();

			string fldsc = reader.ReadString();
			uint myid = reader.ReadUInt32();

			if (typecode==0x01) 
			{
				sgres.Unserialize(reader);
				sgres.BlockID = myid;

				fldsc = reader.ReadString();
				myid = reader.ReadUInt32();
				ctn.Unserialize(reader);
				ctn.BlockID = myid;

				fldsc = reader.ReadString();
				myid = reader.ReadUInt32();
				ogn.Unserialize(reader);
				ogn.BlockID = myid;

				items = new ResourceNodeItem[reader.ReadByte()];
				for (int i=0; i<items.Length; i++) 
				{
					items[i] = new ResourceNodeItem();
					items[i].Unserialize(reader);
				}
				unknown1 = reader.ReadInt32();
			} 
			else if (typecode==0x00) 
			{
				ogn.Unserialize(reader);
				ogn.BlockID = myid;

				items = new ResourceNodeItem[1];
				items[0] = new ResourceNodeItem();
				items[0].Unserialize(reader);
			} 
			else 
			{
				throw new Exception("Unknown ResourceNode 0x"+Helper.HexString(version)+", 0x"+Helper.HexString(typecode));
			}
			unknown2 = reader.ReadInt32();
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		public override void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(version);
			writer.Write(typecode);

			if (typecode==0x01) 
			{
				writer.Write(sgres.BlockName);
				writer.Write(sgres.BlockID);
				sgres.Serialize(writer);

				writer.Write(ctn.BlockName);
				writer.Write(ctn.BlockID);
				ctn.Serialize(writer);

				writer.Write(ogn.BlockName);
				writer.Write(ogn.BlockID);
				ogn.Serialize(writer);

				writer.Write((byte)items.Length);
				for (int i=0; i<items.Length; i++) items[i].Serialize(writer);
				
				writer.Write(unknown1);
			} 
			else if (typecode==0x00) 
			{
				writer.Write(ogn.BlockName);
				writer.Write(ogn.BlockID);
				ogn.Serialize(writer);

				if (items.Length<1) items = new ResourceNodeItem[1];
				items[0].Serialize(writer);
			} 
			else 
			{
				throw new Exception("Unknown ResourceNode 0x"+Helper.HexString(version)+", 0x"+Helper.HexString(typecode));
			}
			writer.Write(unknown2);
		}

		fShapeRefNode form = null;
		public override System.Windows.Forms.TabPage TabPage
		{
			get
			{
				if (form==null) form = new fShapeRefNode(); 
				return form.tResourceNode;
			}
		}
		#endregion

		/// <summary>
		/// You can use this to setop the Controls on a TabPage befor it is dispplayed
		/// </summary>
		protected override void InitTabPage() 
		{
			if (form==null) form = new fShapeRefNode(); 
			
			form.lb_rn.Items.Clear();
			for(int i=0; i<this.items.Length; i++) form.lb_rn.Items.Add(items[i]);

			form.tb_rn_uk1.Text = "0x"+Helper.HexString((uint)this.unknown1);
			form.tb_rn_uk2.Text = "0x"+Helper.HexString((uint)this.unknown2);
			form.tb_rn_ver.Text = "0x"+Helper.HexString(this.version);
		}

		public override void ExtendTabControl(System.Windows.Forms.TabControl tc)
		{
			base.ExtendTabControl (tc);
			if (typecode==0x1)this.ctn.AddToTabControl(tc);
			this.ogn.AddToTabControl(tc);
		}
	}
}
