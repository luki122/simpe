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
using SimPe.Geometry;

namespace SimPe.Plugin.Gmdc
{
	
	/// <summary>
	/// This class contains all Data Needed to import one Bone (Joint)
	/// </summary>
	public class ImportedBone
	{
		/// <summary>
		/// internal Attribute
		/// </summary>
		GmdcImporterAction action;
		/// <summary>
		/// Returns/Sets the action that should be performed
		/// </summary>
		public GmdcImporterAction Action 
		{
			get { return action; }
			set { action = value; }
		}		

		int index;
		/// <summary>
		/// If action is <see cref="GmdcImporterAction.Replace"/> or 
		/// <see cref="GmdcImporterAction.Update"/> this Member stores the 
		/// Index of the Target Joint (read/write)
		/// </summary>
		public int TargetIndex 
		{
			get { return index; }
			set { index = value; }
		}

		/// <summary>
		/// internal Attribute
		/// </summary>
		string name;

		/// <summary>
		/// The name of the Imported Bone		
		/// </summary>
		public string ImportedName 
		{
			get { return name; }
			set { name = value; }
		}

		GmdcJoint bone;
		/// <summary>
		/// The new Bone
		/// </summary>
		public GmdcJoint Bone
		{
			get { return bone; }
		}

		Quaternion q;	
		/// <summary>
		/// The initial Rotation (as Quaternion)
		/// </summary>
		public Quaternion Quaternion
		{
			get { return q; }
			set { q = value; }
		}

		Vector3f trans;	
		/// <summary>
		/// The initial Translation
		/// </summary>
		public Vector3f Translation
		{
			get { return trans; }
		}

		/// <summary>
		/// Returns the color that should be used to display this Group in the "Import Groups" ListView
		/// </summary>
		public System.Drawing.Color MarkColor 
		{
			get 
			{
				if (Action==GmdcImporterAction.Nothing) return System.Drawing.Color.Silver;
				return System.Drawing.Color.DarkBlue;
			}
		}

		/// <summary>
		/// internal Attribute
		/// </summary>
		float scale;
		/// <summary>
		/// Returns/Sets the scale Factor that should be applied to this group
		/// </summary>
		public float Scale
		{
			get { return scale; }
			set { scale = value; }
		}


		/// <summary>
		/// Create a new Instance
		/// </summary>
		/// <param name="parent">The gmdc that should act as Parent</param>
		public ImportedBone(GeometryDataContainer parent)
		{
			bone = new GmdcJoint(parent);			
			name = "";
			index = -1;
			action = GmdcImporterAction.Add;
			q = new Quaternion();
			trans = new Vector3f();
			
			scale = (float)(1.0/AbstractGmdcExporter.SCALE);
		}
	}

	#region Container
	/// <summary>
	/// Typesave ArrayList for <see cref="ImportedGroup"/> Objects
	/// </summary>
	public class ImportedBones : ArrayList 
	{
		/// <summary>
		/// Integer Indexer
		/// </summary>
		public new ImportedBone this[int index]
		{
			get { return ((ImportedBone)base[index]); }
			set { base[index] = value; }
		}

		/// <summary>
		/// unsigned Integer Indexer
		/// </summary>
		public ImportedBone this[uint index]
		{
			get { return ((ImportedBone)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		/// <summary>
		/// add a new Element
		/// </summary>
		/// <param name="item">The object you want to add</param>
		/// <returns>The index it was added on</returns>
		public int Add(ImportedBone item)
		{
			return base.Add(item);
		}

		/// <summary>
		/// insert a new Element
		/// </summary>
		/// <param name="index">The Index where the Element should be stored</param>
		/// <param name="item">The object that should be inserted</param>
		public void Insert(int index, ImportedBone item)
		{
			base.Insert(index, item);
		}

		/// <summary>
		/// remove an Element
		/// </summary>
		/// <param name="item">The object that should be removed</param>
		public void Remove(ImportedBone item)
		{
			base.Remove(item);
		}

		/// <summary>
		/// Checks wether or not the object is already stored in the List
		/// </summary>
		/// <param name="item">The Object you are looking for</param>
		/// <returns>true, if it was found</returns>
		public bool Contains(ImportedBone item)
		{
			return base.Contains(item);
		}		

		/// <summary>
		/// Number of stored Elements
		/// </summary>
		public int Length 
		{
			get { return this.Count; }
		}

		/// <summary>
		/// Create a clone of this Object
		/// </summary>
		/// <returns>The clone</returns>
		public override object Clone()
		{
			ImportedBones list = new ImportedBones();
			foreach (ImportedBone item in this) list.Add(item);

			return list;
		}
	}
	#endregion
}
