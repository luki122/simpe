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
using SimPe.Geometry;

namespace SimPe.Plugin.Gmdc.Exporter
{
	/// <summary>
	/// This class provides the functionality to Export Data to the .x (DirectX) FileFormat
	/// </summary>
	public class GmdcExportToX : AbstractGmdcExporter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gmdc">The Gmdc File the Export is based on</param>
		/// <param name="groups">The list of Groups you want to export</param>
		/// <remarks><see cref="AbstractGmdcExporter.FileContent"/> will contain the Exported .x File</remarks>
		public GmdcExportToX(GeometryDataContainer gmdc, GmdcGroups groups) : base(gmdc, groups) {}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gmdc">The Gmdc File the Export is based on</param>
		/// <remarks><see cref="AbstractGmdcExporter.FileContent"/> will contain the Exported .x File</remarks>
		public GmdcExportToX(GeometryDataContainer gmdc) : base(gmdc) {}
		/// <summary>
		/// Default Constructor
		/// </summary>
		/// <remarks>The export has to be started Manual through a call to <see cref="AbstractGmdcExporter.Process"/></remarks>
		public GmdcExportToX() : base()  {}

		System.Collections.ArrayList modelnames;
		/// <summary>
		/// Returns a unique Modelname
		/// </summary>
		/// <param name="name">The name of the Model</param>
		/// <returns>the unique Name</returns>
		string GetUniqueGroupName(string name) 
		{			
			return name;			
		}

		/// <summary>
		/// Returns the suggested File Extension (including the . like .obj or .3ds)
		/// </summary>
		public override string FileExtension
		{
			get {return ".x";}
		}

		/// <summary>
		/// Returns the File Description (the Name of the exported FileType)
		/// </summary>
		public override string FileDescription
		{
			get {return "Direct X Mesh";}
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
			writer.WriteLine("xof 0303txt 0032");
			writer.WriteLine();
			writer.WriteLine("# This DirectX File was generated by SimPE");
			writer.WriteLine();
			writer.WriteLine("Frame SCENE_ROOT{");

			modelnames=new System.Collections.ArrayList();
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
			string txtrname = Group.Name;
			string umodelname = GetUniqueGroupName(Group.Name);

			writer.WriteLine("Frame " + umodelname + " {");			
			writer.WriteLine("Mesh {");
			writer.WriteLine();

					
			//firts, write the availabel Vertices
			int vertexcount = 0;
			writer.WriteLine(Link.ReferencedSize.ToString() + "; //Vertex Count");
			int nr = Link.GetElementNr(VertexElement);
			for (int i = 0; i < Link.ReferencedSize; i++)
			{
				vertexcount++;					
				if (i!=0) writer.WriteLine(",");
				Vector3f v = new Vector3f(Link.GetValue(nr, i).Data[0], Link.GetValue(nr, i).Data[1], Link.GetValue(nr, i).Data[2]);
				v = Component.Transform(v);
				writer.Write(
					v.X.ToString("N6", AbstractGmdcExporter.DefaultCulture) + ";"+
					v.Y.ToString("N6", AbstractGmdcExporter.DefaultCulture) + ";"+
					v.Z.ToString("N6", AbstractGmdcExporter.DefaultCulture) + "; "
					);
			}
			writer.WriteLine(";");
			

			//now build a Face list
			string faces = (Group.Faces.Count / 3).ToString() + "; //Face Count";
			for (int i = 0; i < Group.Faces.Count; i++)
			{
				int vertexnr = Group.Faces[i];				
				if (i%3 == 0)
				{
					if (i!=0) faces += ", ";
					faces += Helper.lbr + "3;" +
						vertexnr.ToString() + ",";
				} 
				else if (i%3 == 1) faces +=  vertexnr.ToString() + ",";
				else faces +=  vertexnr.ToString() + ";";
			}

			faces += ";";
			writer.WriteLine(faces);
			writer.WriteLine();

			//Add a MeshNormal Section if available
			if (this.NormalElement!=null) 
			{				
				vertexcount = 0;
				writer.WriteLine("MeshNormals {");
				writer.WriteLine(Link.ReferencedSize.ToString() + "; //Vertex Count");
				nr = Link.GetElementNr(NormalElement);
				for (int i = 0; i < Link.ReferencedSize; i++)
				{
					vertexcount++;					
					if (i!=0) writer.WriteLine(",");
					Vector3f v = new Vector3f(Link.GetValue(nr, i).Data[0], Link.GetValue(nr, i).Data[1], Link.GetValue(nr, i).Data[2]);
					v = Component.TransformNormal(v);
					writer.Write(
						v.X.ToString("N6", AbstractGmdcExporter.DefaultCulture) + ";"+
						v.Y.ToString("N6", AbstractGmdcExporter.DefaultCulture) + ";"+
						v.Z.ToString("N6", AbstractGmdcExporter.DefaultCulture) + ";"
					);
					
				}
				writer.WriteLine(";");
				writer.WriteLine(faces);
				writer.WriteLine("}");
				writer.WriteLine();
			}
			

			//now Material Definitions 
			writer.WriteLine("MeshMaterialList{");
			writer.WriteLine("1;");
			writer.WriteLine((Group.Faces.Length / 3).ToString() + "; //Face Count");
			for (int i = 0; i < Group.Faces.Length; i+=3)
			{
				if (i!=0) writer.Write(",");
				if (i%10==0) writer.WriteLine();
				writer.Write("0");
			}
			writer.WriteLine(";;");
			//add a Meterial
			writer.WriteLine("Material {");
			writer.WriteLine("0.300000;0.300000;0.300000;"+((float)(Group.Opacity&0xff)).ToString("N6", AbstractGmdcExporter.DefaultCulture)+";;");
			writer.WriteLine("0.300000;");
			writer.WriteLine("1.000000;1.000000;1.000000;;");
			writer.WriteLine("0.100000;0.100000;0.100000;;");
			if ((txtrname!=null) && (this.UVCoordinateElement!=null))
			{
				//
				//TODO: Remove the following Line if you found out how to texture two subsets with te same name
				//
				txtrname = umodelname;
				writer.WriteLine("TextureFilename{\""+txtrname.Replace("\\", "\\\\")+"\";}");
			}
			writer.WriteLine("}");
			writer.WriteLine("}");

			//now the Texture Cords //iv available
			if (this.UVCoordinateElement!=null) 
			{								
				writer.WriteLine("MeshTextureCoords {");
				writer.WriteLine(Link.ReferencedSize.ToString() + "; //Vertex Count");
				nr = Link.GetElementNr(UVCoordinateElement);
				for (int i = 0; i < Link.ReferencedSize; i++)
				{
					writer.WriteLine(
						(Link.GetValue(nr, i).Data[0]).ToString("N6", AbstractGmdcExporter.DefaultCulture) + ";"+
						(Link.GetValue(nr, i).Data[1]).ToString("N6", AbstractGmdcExporter.DefaultCulture) + ";"
						);
					
				}
				writer.WriteLine(";");
				writer.WriteLine("}"); 
			}
			

			//close all opend Containers
			writer.WriteLine("} //Mesh");
			writer.WriteLine("} //Frame");				
			writer.WriteLine();
		}

		/// <summary>
		/// Called when the export was finished
		/// </summary>
		/// <remarks>you should use this to write Footer Informations. 
		/// Use the writer member to write to the File</remarks>
		protected override void FinishFile()
		{
			writer.WriteLine("}");			
		}
	}
}
