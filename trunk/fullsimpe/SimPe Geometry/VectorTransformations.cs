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

namespace SimPe.Geometry
{
	/// <summary>
	/// One basic Vector Transformation
	/// </summary>
	public class VectorTransformation 
	{
		/// <summary>
		/// What Order should the Transformation be applied
		/// </summary>
		public enum TransformOrder : byte 
		{
			RotateTranslate = 0,
			TranslateRotate = 1
		};

		#region Attributes
		TransformOrder o;
		/// <summary>
		/// Returns / Sets the current Order
		/// </summary>
		public TransformOrder Order
		{
			get { return o; }
			set { o = value; }
		}
		Vector3f trans;
		/// <summary>
		/// The Translation
		/// </summary>
		public Vector3f Translation
		{
			get { return trans; }
			set {trans = value; }
		}

		Quaternion quat;
		/// <summary>
		/// The Rotation
		/// </summary>
		public Quaternion Rotation  
		{
			get { return quat; }
			set {quat = value; }
		}
		#endregion

		/// <summary>
		/// Create a new Instance
		/// </summary>
		/// <param name="o">The order of the Transform</param>
		public VectorTransformation(TransformOrder o) 
		{
			this.o = o;
			trans = new Vector3f();
			quat = new Quaternion();
		}

		public override string ToString()
		{
			return "trans="+trans.ToString()+"    rot="+quat.ToString();
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public virtual void Unserialize(System.IO.BinaryReader reader)
		{
			if (o==TransformOrder.RotateTranslate) 
			{
				quat.Unserialize(reader);
				trans.Unserialize(reader);
			} 
			else 
			{
				trans.Unserialize(reader);
				quat.Unserialize(reader);				
			}
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		public virtual void Serialize(System.IO.BinaryWriter writer)
		{
			if (o==TransformOrder.RotateTranslate) 
			{
				quat.Serialize(writer);
				trans.Serialize(writer);
			} 
			else 
			{
				trans.Serialize(writer);
				quat.Serialize(writer);
			}
		}

		/// <summary>
		/// Applies the Transformation to the passed Vertex
		/// </summary>
		/// <param name="v">The Vertex you want to Transform</param>
		/// <returns>Transformed Vertex</returns>
		public Vector3f Transform(Vector3f v) 
		{
			if (o==TransformOrder.RotateTranslate) 
			{
				v = quat.Rotate(v);
				return v + trans;
			} 
			else 
			{
				v += trans;
				return quat.Rotate(v);
			}
		}	

	}

	#region container
	/// <summary>
	/// Typesave ArrayList for VectorTransformation Objects
	/// </summary>
	public class VectorTransformations : ArrayList 
	{
		/// <summary>
		/// Integer Indexer
		/// </summary>
		public new VectorTransformation this[int index]
		{
			get { return ((VectorTransformation)base[index]); }
			set { base[index] = value; }
		}

		/// <summary>
		/// unsigned Integer Indexer
		/// </summary>
		public VectorTransformation this[uint index]
		{
			get { return ((VectorTransformation)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		/// <summary>
		/// add a new Element
		/// </summary>
		/// <param name="item">The object you want to add</param>
		/// <returns>The index it was added on</returns>
		public int Add(VectorTransformation item)
		{
			return base.Add(item);
		}

		/// <summary>
		/// insert a new Element
		/// </summary>
		/// <param name="index">The Index where the Element should be stored</param>
		/// <param name="item">The object that should be inserted</param>
		public void Insert(int index, VectorTransformation item)
		{
			base.Insert(index, item);
		}

		/// <summary>
		/// remove an Element
		/// </summary>
		/// <param name="item">The object that should be removed</param>
		public void Remove(VectorTransformation item)
		{
			base.Remove(item);
		}

		/// <summary>
		/// Checks wether or not the object is already stored in the List
		/// </summary>
		/// <param name="item">The Object you are looking for</param>
		/// <returns>true, if it was found</returns>
		public bool Contains(VectorTransformation item)
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
			VectorTransformations list = new VectorTransformations();
			foreach (VectorTransformation item in this) list.Add(item);

			return list;
		}
	}
	#endregion
}
