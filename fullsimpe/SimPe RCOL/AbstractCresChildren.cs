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
using System.Windows.Forms;
using SimPe.Plugin;
using SimPe.Interfaces.Scenegraph;
using SimPe.Geometry;

namespace SimPe.Plugin
{
	/// <summary>
	/// Implemented common Methods of the ICresChildren Interface
	/// </summary>
	public abstract class AbstractCresChildren : AbstractRcolBlock, ICresChildren
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public AbstractCresChildren(Rcol parent) : base(parent) {}

		/// <summary>
		/// Returns the Child Block with the given Index from the Parent Rcol
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public ICresChildren GetBlock(int index) 
		{
			if (Parent==null) return null;

			if (index<0) return null;
			if (index>= this.Parent.Blocks.Length) return null;

			object o = Parent.Blocks[index];

			if (o.GetType().GetInterface("ICresChildren", false) == typeof(ICresChildren)) 
				return (ICresChildren)o;
			
			return null;
		}

		public int Index 
		{
			get 
			{
				if (parent==null) return -1;
				for (int i=0; i<parent.Blocks.Length; i++)
					if (parent.Blocks[i]==this) return i;
				return -1;
			}
		}

		
		public IntArrayList GetParentBlocks()
		{
			IntArrayList l = new IntArrayList();
			for (int i=0; i<parent.Blocks.Length; i++)			
			{
				SimPe.Interfaces.Scenegraph.IRcolBlock irb = (SimPe.Interfaces.Scenegraph.IRcolBlock)parent.Blocks[i];
				if (irb.GetType().GetInterface("ICresChildren", false) == typeof(ICresChildren)) 
				{
					SimPe.Interfaces.Scenegraph.ICresChildren icc = (ICresChildren)irb;
					if (icc.ChildBlocks.Contains(Index)) l.Add(i);
				}
			}
			return l;
		}

		public SimPe.Interfaces.Scenegraph.ICresChildren GetFirstParent()
		{
			IntArrayList l = GetParentBlocks();
			if (l.Length==0) return null;
			return (SimPe.Interfaces.Scenegraph.ICresChildren)parent.Blocks[l[0]];
		}

		/// <summary>
		/// Returns a List of all Child Blocks referenced by this Element
		/// </summary>
		public abstract IntArrayList ChildBlocks 
		{
			get;
		}	
	
		public abstract int ImageIndex 
		{
			get;
		}


		public abstract TransformNode StoredTransformNode
		{
			get;
		}

		/// <summary>
		/// Contains all bones that were seen during the recursin
		/// </summary>
		ArrayList seenbones;

		/// <summary>
		/// Walks the parent Hirarchy to calculate the absolute POsition for thsi Bone
		/// </summary>
		/// <param name="bs">List of known Bones</param>
		/// <param name="b">The bone you want o get the Absolute position for</param>
		/// <param name="v">The offset for the calculation</param>		
		/// <param name="eo">ElementOrder we want to use</param>
		VectorTransformations GetAbsoluteTransformation(ICresChildren node, VectorTransformations v) 
		{
			if (v==null) v = new VectorTransformations();
			if (node == null) return v;
			if (node.StoredTransformNode == null) return v;
			if (seenbones.Contains(node.Index)) return v;
			
			seenbones.Add(node.Index);
			
			/*v.Rotation = node.StoredTransformNode.Rotation * v.Rotation;
			v.Rotation.MakeUnitQuaternion();

			v.Translation = 
				node.StoredTransformNode.Rotation.Rotate(v.Translation + node.StoredTransformNode.Translation);					 */
							
			
			v.Add(node.StoredTransformNode.Transformation);									
			v = GetAbsoluteTransformation(node.GetFirstParent(), v);
			
							
			return v;
		}
		
		/// <summary>
		/// Returns the effective Transformation, that is described by the CresHirarchy
		/// </summary>
		/// <returns></returns>
		public VectorTransformations GetHirarchyTransformations()
		{
			seenbones = new ArrayList();
			return GetAbsoluteTransformation(this, null);				 
		}

		/// <summary>
		/// Returns the effective Transformation, that is described by the CresHirarchy
		/// </summary>
		/// <returns></returns>
		public VectorTransformation GetEffectiveTransformation()
		{
			VectorTransformations list = GetHirarchyTransformations();	
			VectorTransformation v = new VectorTransformation();

			
			for (int i=list.Length-1; i>=0; i--)			
			{
				VectorTransformation t = list[i];
				
				v.Rotation = t.Rotation * v.Rotation;
				v.Translation = t.Rotation.Rotate((v.Translation ) - (t.Translation));
			}

			return v;
		}
	}
}