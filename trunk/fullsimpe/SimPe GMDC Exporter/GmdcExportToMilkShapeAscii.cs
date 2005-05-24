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
using System.IO;
using System.Globalization;
using SimPe.Plugin.Gmdc;

namespace SimPe.Plugin.Gmdc.Exporter
{
	/// <summary>
	/// This class provides the functionality to Export Data to the .txt FileFormat
	/// </summary>
	public class GmdcExportToMilkShapeAscii : AbstractGmdcExporter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gmdc">The Gmdc File the Export is based on</param>
		/// <param name="groups">The list of Groups you want to export</param>
		/// <remarks><see cref="AbstractGmdcExporter.FileContent"/> will contain the Exported .txt File</remarks>
		public GmdcExportToMilkShapeAscii(GeometryDataContainer gmdc, GmdcGroups groups) : base(gmdc, groups) {}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gmdc">The Gmdc File the Export is based on</param>
		/// <remarks><see cref="AbstractGmdcExporter.FileContent"/> will contain the Exported .txt File</remarks>
		public GmdcExportToMilkShapeAscii(GeometryDataContainer gmdc) : base(gmdc) {}
		/// <summary>
		/// Default Constructor
		/// </summary>
		/// <remarks>The export has to be started Manual through a call to <see cref="AbstractGmdcExporter.Process"/></remarks>
		public GmdcExportToMilkShapeAscii() : base() {}

		int modelnr, vertexoffset;

		/// <summary>
		/// Returns the suggested File Extension (including the . like .obj or .3ds)
		/// </summary>
		public override string FileExtension
		{
			get {return ".txt";}
		}

		/// <summary>
		/// Returns the File Description (the Name of the exported FileType)
		/// </summary>
		public override string FileDescription
		{
			get {return "Milkshape ASCII File";}
		}		

		/// <summary>
		/// Returns the name of the Author
		/// </summary>
		public override string Author
		{
			get {return "Quaxi";}
		}

		/// <summary>
		/// Called when a new File is started
		/// </summary>
		/// <remarks>
		/// you should use this to write Header Informations. 
		/// Use the writer member to write to the File
		/// </remarks>
		protected override void InitFile()
		{
			modelnr = 0;
			vertexoffset = 0;
			writer.WriteLine("Meshes: "+Gmdc.Groups.Count.ToString());
		}

		/// <summary>
		/// This is called whenever a Group (=subSet) needs to processed
		/// </summary>
		/// <remarks>
		/// You can use the UVCoordinateElement, NormalElement, 
		/// VertexElement, Group and Link Members in this Method. 
		/// 
		/// This Method is only called, when the Group, Link and 
		/// Vertex Members are set (not null). The other still can 
		/// be Null!
		/// 
		/// Use the writer member to write to the File.
		/// </remarks>
		protected override void ProcessGroup()
		{	
			//Find the BoneAssignment
			GmdcElement boneelement = this.Link.FindElementType(ElementIdentity.BoneAssignment);
							

			writer.WriteLine("\""+Group.Name+"\" 0 -1");

					
			//first, write the availabel Vertices
			int vertexcount = 0;
			int nr = Link.GetElementNr(VertexElement);
			int nnr = -1;
			if (this.NormalElement!=null) nnr = Link.GetElementNr(UVCoordinateElement);
			writer.WriteLine(Link.ReferencedSize.ToString());
			for (int i = 0; i < Link.ReferencedSize; i++)
			{				
				writer.Write("0 " + 
					(Link.GetValue(nr, i).Data[0]).ToString("N6", AbstractGmdcExporter.DefaultCulture) + " "+
					(Link.GetValue(nr, i).Data[1]).ToString("N6", AbstractGmdcExporter.DefaultCulture) + " "+
					(Link.GetValue(nr, i).Data[2]).ToString("N6", AbstractGmdcExporter.DefaultCulture) + " ");

				if (nnr!=-1) 
				{
					writer.Write(
						Link.GetValue(nnr, i).Data[0].ToString("N6", AbstractGmdcExporter.DefaultCulture) + " "+
						Link.GetValue(nnr, i).Data[1].ToString("N6", AbstractGmdcExporter.DefaultCulture )+ " ");
				} 
				else 
				{
					writer.Write(" 0.000000 0.000000");
				}


				if (boneelement==null) 
				{
					writer.WriteLine("-1");
				} 
				else 
				{
					int bnr = Link.GetRealIndex(nr, i);
					if (bnr==-1) writer.WriteLine("-1");
					else 
					{
						bnr = ((SimPe.Plugin.Gmdc.GmdcElementValueOneInt)boneelement.Values[bnr]).Value;
						if (bnr == -1) writer.WriteLine("-1");
						else writer.WriteLine((bnr&0xff).ToString());
					}
						
				}
			}			
			
			//Add a MeshNormal Section if available
			if (this.NormalElement!=null) 
			{				
				nr = Link.GetElementNr(NormalElement);
				writer.WriteLine(Link.ReferencedSize.ToString());
				for (int i = 0; i < Link.ReferencedSize; i++)
				{
					writer.WriteLine( 
						(Link.GetValue(nr, i).Data[0]).ToString("N6", AbstractGmdcExporter.DefaultCulture) + " "+
						(Link.GetValue(nr, i).Data[1]).ToString("N6", AbstractGmdcExporter.DefaultCulture) + " "+
						(Link.GetValue(nr, i).Data[2]).ToString("N6", AbstractGmdcExporter.DefaultCulture));
				}				
			} 
			else 
			{
				writer.WriteLine("0");
			}
						
			
			//Export Faces
			writer.WriteLine(Group.FaceCount.ToString());
			for (int i = 0; i < Group.Faces.Count; i +=3)
			{
				writer.WriteLine("0 " + 
					Group.Faces[i+0].ToString() + " " +
					Group.Faces[i+1].ToString() + " " +
					Group.Faces[i+2].ToString() + " " +
					Group.Faces[i+0].ToString() + " " +
					Group.Faces[i+1].ToString() + " " +
					Group.Faces[i+2].ToString() + " 1");
			}			
			
			vertexoffset += vertexcount;
			modelnr++;
		}

		/// <summary>
		/// Called when the export was finished
		/// </summary>
		/// <remarks>you should use this to write Footer Informations. 
		/// Use the writer member to write to the File</remarks>
		protected override void FinishFile()
		{		
			writer.WriteLine("Materials: 0");
			
			//Export Bones
			writer.WriteLine("Bones: "+Gmdc.Bones.Count.ToString());
			for (int i=0; i<Gmdc.Bones.Length; i++)
			{
				if (i>=Gmdc.Model.Rotations.Length || i>=Gmdc.Model.Transformations.Length) break;
	
				writer.WriteLine("\"Joint"+i.ToString()+"\"");
				writer.WriteLine("\"\"");
				writer.WriteLine("0 " + 
					Gmdc.Model.Transformations[i].X.ToString("N6", AbstractGmdcExporter.DefaultCulture) + " " +
					Gmdc.Model.Transformations[i].Y.ToString("N6", AbstractGmdcExporter.DefaultCulture) + " " +
					Gmdc.Model.Transformations[i].Z.ToString("N6", AbstractGmdcExporter.DefaultCulture) + " " +
					Gmdc.Model.Rotations[i].X.ToString("N6", AbstractGmdcExporter.DefaultCulture) + " " +
					Gmdc.Model.Rotations[i].X.ToString("N6", AbstractGmdcExporter.DefaultCulture) + " " +
					Gmdc.Model.Rotations[i].X.ToString("N6", AbstractGmdcExporter.DefaultCulture));

				writer.WriteLine("1");
				writer.WriteLine("1.000000 0.000000 0.000000 0.000000");
				writer.WriteLine("1");
				writer.WriteLine("1.000000 0.000000 0.000000 0.000000");
			}

			//Write Footer
			writer.WriteLine("GroupComments: 0");
			writer.WriteLine("MaterialComments: 0");
			writer.WriteLine("BoneComments: 0");
			writer.WriteLine("ModelComment: 0");
		}
	}
}
