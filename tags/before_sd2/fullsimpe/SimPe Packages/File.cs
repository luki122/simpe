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
using System.Collections;
using SimPe.Data;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Plugin.Internal;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;
using SimPe.PackedFiles.Wrapper;
using SimPe.Events;
using SimPe.Collections.IO;

namespace SimPe.Packages 
{
	/// <summary>
	/// Was the package opend by using a Filename?
	/// </summary>
	public enum PackageBaseType:byte
	{
		Stream = 0x00,
		Filename = 0x01
	}

	/// <summary>
	/// Header of a .package File
	/// </summary>
	public class File : IPackageFile
	{	
		/// <summary>
		/// The Type ID for CompressedFile Lists
		/// </summary>
		public const uint FILELIST_TYPE = 0xE86B1EEF;

		/// <summary>
		/// The Binary reader that has opened the .Package file
		/// </summary>
		protected BinaryReader reader;

		/// <summary>
		/// How was the packaged opened
		/// </summary>
		protected PackageBaseType type;

		/// <summary>
		/// The Data of the Header
		/// </summary>
		protected HeaderData header;

		/// <summary>
		/// Contains the PackedFile representing the FileList
		/// </summary>
		protected PackedFileDescriptor filelist = null;

		/// <summary>
		/// Contains the FileListFile
		/// </summary>
		protected CompressedFileList filelistfile = null;

		/// <summary>
		/// Will contain the File Index
		/// </summary>
		protected ResourceIndex fileindex;

		/// <summary>
		/// Will contain the Hole Index
		/// </summary>
		protected HoleIndexItem[] holeindex;

		/// <summary>
		/// The Name of the current File
		/// </summary>
		string flname;

		/// <summary>
		/// true if you want to keep the FileHandle
		/// </summary>
		bool persistent;
		/// <summary>
		/// true if you want to keep the FileHandle
		/// </summary>
		public bool Persistent
		{
			get { return persistent; }
			set 
			{ 
				/*if (!persistent && value) this.OpenReader();
				else if (persistent && !value) this.CloseReader();

				persistent = value; */
			}
		}

		/// <summary>
		/// Returns the Stream used to read the Package
		/// </summary>
		public System.IO.BinaryReader Reader 
		{
			get 
			{ 
				if (reader!=null) 
				{
					if (reader.BaseStream==null) 
						reader = null;
					else
						if (!reader.BaseStream.CanRead) reader = null;
				}
				return reader; 
			}
		}		

		/// <summary>
		/// Creates the Header Datastructure
		/// </summary>
		/// <param name="br">The Binary Reader you want to use</param>
		/// <remarks>
		/// The Reader must be positioned on the first byte of the Header 
		/// (mostly that should be Index 0)
		/// </remarks>
		internal File(BinaryReader br)
		{
			pause = false;
			type = PackageBaseType.Stream;
			OpenByStream(br);
		}

		/// <summary>
		/// Dispose this Instance
		/// </summary>
		~File()
		{
			Close(true);
		}

		/// <summary>
		/// Opens the Package File represented by a Stream
		/// </summary>
		/// <param name="br">The Stream</param>
		protected void OpenByStream(BinaryReader br) 
		{
			fhg = 0;
			reader = br;
			header = new HeaderData();

			if (br!= null) 
			{
				if (br.BaseStream.Length>0) 
				{
					this.LockStream();
					header.Load(reader);
					LoadFileIndex();
					LoadHoleIndex();
					this.UnLockStream();
				}
			} 

			CloseReader();
		}

		/// <summary>
		/// Creats a new Object based on the given File
		/// </summary>
		/// <param name="filename"></param>
		internal File(string filename) 
		{
			pause = false;
			ReloadFromFile(filename);
		}

		/// <summary>
		/// Reload the Data from the File
		/// </summary>
		/// <param name="filename"></param>
		internal void ReloadFromFile(string filename) 
		{
			persistent = true;
			StreamItem si = StreamFactory.UseStream(filename, System.IO.FileAccess.Read);
			
			if (si.StreamState!=StreamState.Removed) 
			{
				type = PackageBaseType.Filename;
				flname = filename;
				System.IO.BinaryReader br = new System.IO.BinaryReader(si.FileStream);
				OpenByStream(br);
			}
			else 
			{
				type = PackageBaseType.Stream;
				OpenByStream(null);
			}
		}

		/*/// <summary>
		/// Synchronize the content with Data from the Filesystem
		/// </summary>
		internal void Synchronize()
		{
			if (type==PackageBaseType.Filename) 
			{
				File newfl = new File(this.FileName);

				//Synchronize Descriptors
				foreach (SimPe.Packages.PackedFileDescriptor newpfd in newfl.Index) 
				{
					SimPe.Packages.PackedFileDescriptor pfd = (SimPe.Packages.PackedFileDescriptor)this.FindFile(newpfd);
					if (pfd!=null) 
					{
						pfd.Offset = newpfd.Offset;
						pfd.Size = newpfd.Size;
						pfd.Changed = false;
						pfd.MarkForDelete = false;
						pfd.UserData = null;
					} 
					else this.Add(newpfd);					
				}	
			
				//Remove files that do not exist in the filesystem Version
				foreach (SimPe.Packages.PackedFileDescriptor pfd in Index) 
				{
					SimPe.Packages.PackedFileDescriptor newpfd = (SimPe.Packages.PackedFileDescriptor)newfl.FindFile(pfd);
					if (newpfd==null) this.Remove(pfd);
				}
			}
		}*/

		/// <summary>
		/// Init the Clone for this Package
		/// </summary>
		/// <returns>An INstance of this Class</returns>
		protected virtual Interfaces.Files.IPackageFile NewCloneBase() 
		{
			File fl = new File((BinaryReader)null);
			fl.header = this.header;
			
			return fl;
		}

		/// <summary>
		/// Create a Clone of this Package File
		/// </summary>
		/// <returns>the new Package File</returns>
		public Interfaces.Files.IPackageFile Clone()
		{
			Interfaces.Files.IPackageFile fl = NewCloneBase();
			foreach (Interfaces.Files.IPackedFileDescriptor pfd in this.Index) 
			{
				Interfaces.Files.IPackedFileDescriptor npfd = (Interfaces.Files.IPackedFileDescriptor)pfd.Clone();
				npfd.UserData = Read(pfd).UncompressedData;
				fl.Add(npfd);
			}

			return fl;
		}
		

		#region Lock handling
		protected void OpenReader()
		{			
			if (persistent) 
			{
				StreamItem si = StreamFactory.UseStream(flname, FileAccess.Read);
				si.SetFileAccess(FileAccess.Read);
				//if (si.StreamState==StreamState.Removed) throw new Exception ("The File was moved or deleted whil SimPe was running.", new Exception("Unable to find "+this.FileName));
				if (si.StreamState!=StreamState.Removed) reader = new System.IO.BinaryReader(si.FileStream);
				return;
			}
			if (type == PackageBaseType.Filename)
			{
				CloseReader();	
				StreamItem si = StreamFactory.UseStream(flname, FileAccess.Read);
				if (si.StreamState==StreamState.Removed) throw new Exception ("The File was moved or deleted whil SimPe was running.", new Exception("Unable to find "+this.FileName));
				reader = new System.IO.BinaryReader(si.FileStream);
			}
		}

		protected void CloseReader()
		{			
			if (persistent) return;
			if ((type == PackageBaseType.Filename) && (reader!=null))
			{
				StreamItem si = StreamFactory.FindStreamItem((FileStream)reader.BaseStream);
				if (si!=null) si.Close();
				
				reader = null;
			}
		}
		#endregion

		#region Stream Handling
		/// <summary>
		/// Locks the bas Stream
		/// </summary>
		public void LockStream() 
		{
			//if (reader!=null)
			//	((System.IO.FileStream) reader.BaseStream).Lock(0, reader.BaseStream.Length);
		}

		/// <summary>
		/// Unlocks the BaseStream
		/// </summary>
		public void UnLockStream() 
		{
			//if (reader!=null)
			//	((System.IO.FileStream) reader.BaseStream).Unlock(0, reader.BaseStream.Length);
		}
		#endregion

		#region Header Handling
		/// <summary>
		/// The Structural Data of the Header
		/// </summary>
		public Interfaces.Files.IPackageHeader Header
		{
			get { return header; }
		}

		
		#endregion

		#region FileIndex Handling
		/// <summary>
		/// True if the User has changed a PackedFile
		/// </summary>
		public bool HasUserChanges
		{
			get 
			{
				if (Index==null) return false;
				foreach (Interfaces.Files.IPackedFileDescriptor pfd in Index) 
				{
					if (pfd.Changed) return true;
				}
				return false;
			}
		}

		/// <summary>
		/// Creates a new File descriptor
		/// </summary>
		/// <returns>the new File descriptor</returns>
		public IPackedFileDescriptor NewDescriptor(uint type, uint subtype, uint group, uint instance)
		{
			PackedFileDescriptor pfd = new PackedFileDescriptor();
			pfd.type = type;
			pfd.subtype = subtype;
			pfd.group = group;
			pfd.instance = instance;
			//pfd.ChangedUserData += new SimPe.Events.PackedFileChanged(ResourceChanged);

			return pfd;
		}

		/// <summary>
		/// Temoves the described File from the Index
		/// </summary>
		/// <param name="pfd">A Packed File Descriptor</param>
		public void Remove(IPackedFileDescriptor pfd) 
		{
			if (fileindex==null) return;
			fileindex.RemoveItem(pfd);
			
			header.index.count = fileindex.Count;
			
			((SimPe.Packages.PackedFileDescriptor)pfd).PackageInternalUserDataChange = null;
			pfd.DescriptionChanged -= new EventHandler(ResourceDescriptionChanged);
			FireIndexEvent();			
			this.FireRemoveEvent();
		}

		/// <summary>
		/// Removes all FileDescripotrs that are marked for Deletion
		/// </summary>
		public void RemoveMarked()
		{
			ArrayList list = fileindex.RemoveDeleteMarkedItem();
			foreach (Interfaces.Files.IPackedFileDescriptor pfd in list) 
			{
				((SimPe.Packages.PackedFileDescriptor)pfd).PackageInternalUserDataChange = null;
				pfd.DescriptionChanged -= new EventHandler(ResourceDescriptionChanged);				
			}

			

			bool changed = list.Count>0;			
			header.index.count = fileindex.Count;

						
			if (changed) 
			{
				this.FireRemoveEvent();
				FireIndexEvent();
			}
		}

		/// <summary>
		/// Ads a new Descriptor to the Index
		/// </summary>
		/// <param name="type">The Type of the new File</param>
		/// <param name="subtype">The SubType/classID/ResourceID of the new File</param>
		/// <param name="group">The Group for the File</param>
		/// <param name="instance">The Instance of the FIle</param>
		public IPackedFileDescriptor Add(uint type, uint subtype, uint group, uint instance)
		{
			PackedFileDescriptor pfd = new PackedFileDescriptor();
			pfd.Type = type;
			pfd.SubType = subtype;
			pfd.Group = group;
			pfd.Instance = instance;

			Add(pfd);

			return pfd;
		}

		/// <summary>
		/// Ads a list of Descriptors to the Index
		/// </summary>
		/// <param name="pfds">List of Descriptors</param>
		public void Add(IPackedFileDescriptor[] pfds) 
		{
			foreach (IPackedFileDescriptor pfd in pfds) Add(pfd);
		}

		/// <summary>
		/// Ads a new Descriptor to the Index
		/// </summary>
		/// <param name="pfd">The PackedFile Descriptor</param>
		public void Add(IPackedFileDescriptor pfd)
		{
			if (fileindex==null) 
				fileindex = new ResourceIndex(this);

			fileindex.AddIndexFromPfd(pfd);
			header.index.count = fileindex.Count;			

			((SimPe.Packages.PackedFileDescriptor)pfd).PackageInternalUserDataChange = new SimPe.Events.PackedFileChanged(ResourceChanged);
			pfd.DescriptionChanged += new EventHandler(ResourceDescriptionChanged);
			FireIndexEvent();
			this.FireAddEvent();
		}

		/// <summary>
		/// Returns the FileIndexItem for the given File
		/// </summary>
		/// <param name="item">Number of the File within the FileIndex (0-Based)</param>
		/// <returns>The FileIndexItem for this Entry</returns>
		public IPackedFileDescriptor GetFileIndex(uint item)
		{
			PackedFileDescriptors list = fileindex.Flatten();
			if ((item>=list.Count) || (item<0)) return null;
			return list[item];
		}

		public ResourceIndex FileIndex 
		{
			get { 
				if (fileindex==null) fileindex = new ResourceIndex(this);
				return fileindex; 
			}
		}
		/// <summary>
		/// Returns or Changes the stored Fileindex
		/// </summary>
		public IPackedFileDescriptor[] Index
		{
			get 
			{
				if (fileindex==null) return new IPackedFileDescriptor[0];
				IPackedFileDescriptor[] pfds = new IPackedFileDescriptor[fileindex.Flatten().Count];
				fileindex.Flatten().CopyTo(pfds);
				return pfds;
			}
			/*set 
			{
				fileindex = value;
				header.Index.Count = fileindex.Length;
			}*/
		}

		/// <summary>
		/// Returns or Changes the stored Filelist
		/// </summary>
		public PackedFileDescriptor FileList
		{
			get 
			{
				return filelist;
			}
			set 
			{
				filelist = value;

				//get the FileListFile
				if (filelist!=null) 
				{
					filelistfile = new SimPe.PackedFiles.Wrapper.CompressedFileList(filelist, this);
				}				
			}
		}

		/// <summary>
		/// Returns the FileListFile
		/// </summary>
		public CompressedFileList FileListFile
		{
			get 
			{
				//get the FileListFile
				if ((filelist!=null)  && (filelist==null))
				{
					filelistfile = new SimPe.PackedFiles.Wrapper.CompressedFileList(filelist, this);
				}
				return filelistfile;
			}
		}

		/// <summary>
		/// Loads the FileIndex from the Package file
		/// </summary>
		protected void LoadFileIndex()
		{
			if (fileindex==null) fileindex = new ResourceIndex(this);
			else fileindex.Clear();

			uint counter = 0;
			reader.BaseStream.Seek(	header.index.offset, System.IO.SeekOrigin.Begin );

			while (counter<header.Index.Count) 
			{
				/*reader.BaseStream.Seek(	header.index.offset + counter*header.Index.ItemSize, 
										System.IO.SeekOrigin.Begin );*/
				LoadFileIndexItem(counter);

				counter++;
			} //while

			//Load the File Index File
			if (FileList != null) FileList = FileList;
		}

		/// <summary>
		/// Reads the Compressed State for the package
		/// </summary>
		public void LoadCompressedState()
		{
			//Load the File Index File
			if (FileList != null) 
			{
				//setup the compression State
				foreach (PackedFileDescriptor pfd in fileindex.Flatten())
					pfd.WasCompressed = this.GetPackedFile(pfd, new byte[0]).IsCompressed;				
			}
		}

		/// <summary>
		/// Loads the FileIndex from the Package file
		/// </summary>
		/// <param name="position">
		/// the number of the fileindex you want to load from the File (0-based). 
		/// This Parameter will only effect the position of the Item within 
		/// the File:fileindex Attribute. The data will be loaded from the current 
		/// position of the File:reader.
		/// </param>
		protected void LoadFileIndexItem(uint position)
		{
			PackedFileDescriptor item = new PackedFileDescriptor();

			item.type = reader.ReadUInt32();
			item.group = reader.ReadUInt32();
			item.instance = reader.ReadUInt32();
			if ((header.IsVersion0101) && (header.index.ItemSize>=24)) item.subtype = reader.ReadUInt32();
			item.offset = reader.ReadUInt32();
			item.size = reader.ReadInt32();		
			item.PackageInternalUserDataChange = new SimPe.Events.PackedFileChanged(ResourceChanged);
			item.DescriptionChanged += new EventHandler(ResourceDescriptionChanged);

//			fileindex[position] = item;
			fileindex.AddIndexFromPfd(item);

			//remeber the filelist;
			if (item.Type == FILELIST_TYPE) 
			{
				filelist = item;
			}
		}
		#endregion

		#region HoleIndex Handling
		/// <summary>
		/// Returns the FileIndexItem for the given File
		/// </summary>
		/// <param name="item">Number of the File within the FileIndex (0-Based)</param>
		/// <returns>The FileIndexItem for this Entry</returns>
		public HoleIndexItem GetHoleIndex(uint item)
		{
			return holeindex[item];
		}

		/// <summary>
		/// Loads the HoleIndex from the Package file
		/// </summary>
		protected void LoadHoleIndex()
		{
			holeindex = new HoleIndexItem[header.hole.Count];
			uint counter = 0;
			if (reader==null) OpenReader();
			reader.BaseStream.Seek(	header.hole.offset, System.IO.SeekOrigin.Begin );

			while (counter<holeindex.Length) 
			{
				/*reader.BaseStream.Seek(	header.hole.offset + counter*header.hole.ItemSize, 
					System.IO.SeekOrigin.Begin );*/
				LoadHoleIndexItem(counter);

				counter++;
			} //while
		}

		/// <summary>
		/// Loads the HoleIndex from the Package file
		/// </summary>
		/// <param name="position">
		/// the number of the holeindex you want to load from the File (0-based). 
		/// This Parameter will only affect the position of the Item within 
		/// the File:holeindex Attribute. The data will be loaded from the current 
		/// position of the File:reader.
		/// </param>
		protected void LoadHoleIndexItem(uint position)
		{
			HoleIndexItem item = new HoleIndexItem();

			item.offset = reader.ReadUInt32();
			item.size = reader.ReadInt32();			

			holeindex[position] = item;
		}
		#endregion

		#region File Handling

		/// <summary>
		/// Returns the FileName of the Current Package
		/// </summary>
		public string FileName 
		{
			get 
			{
				return flname;
			}
			set 
			{
				flname = value;
				fhg = 0;
			}
		}

		uint fhg = 0;
		/// <summary>
		/// Returns the hash Group Value for this File
		/// </summary>
		public uint FileGroupHash 
		{
			get  
			{
				if (fhg==0) fhg = (uint)(Hashes.FileGroupHash(System.IO.Path.GetFileNameWithoutExtension(FileName)) | 0x7f000000);
				return fhg;
			}
		}

		/// <summary>
		/// Reads the File specified by the given itemIndex
		/// </summary>
		/// <param name="item">the itemIndex for the File</param>
		/// <returns>The plain Content of the File</returns>
		public IPackedFile Read(uint item)
		{
			IPackedFileDescriptor pfd = fileindex.Flatten()[item];

			return Read(pfd);
		}

		/// <summary>
		/// the packed File Descriptor
		/// </summary>
		/// <param name="pfd"></param>
		/// <returns></returns>
		PackedFile GetPackedFile(IPackedFileDescriptor pfd, byte[] data)
		{
			PackedFile pf = new PackedFile(data);
			try 
			{
				reader.BaseStream.Seek(pfd.Offset, System.IO.SeekOrigin.Begin);
				pf.size = reader.ReadInt32();
				pf.signature = reader.ReadUInt16();			
				Byte[] dummy = reader.ReadBytes(3);
				pf.uncsize = (uint)((dummy[0]<< 0x10) | (dummy[1] << 0x08) | + dummy[2]);
				if (/*(pf.Size == pfd.Size) &&*/ (pf.Signature==MetaData.COMPRESS_SIGNATURE)) pf.headersize = 9;											

				if ((filelistfile!=null) && (pfd.Type!=File.FILELIST_TYPE))
				{
					int pos = filelistfile.FindFile(pfd);
					if (pos!=-1) 
					{
						SimPe.PackedFiles.Wrapper.ClstItem fi = filelistfile[pos];							
						if (header.Version==0x100000001) pf.uncsize = fi.UncompressedSize;
					}
				} 
			} 
			catch (Exception) 
			{
				pf.size = 0;
				pf.data = new byte[0];
			}
			
			return pf;
		}

		/// <summary>
		/// Reads a File specified by a FileIndexItem
		/// </summary>
		/// <param name="pfd">The PackedFileDescriptor</param>
		/// <returns>The plain Content of the File</returns>
		public IPackedFile Read(IPackedFileDescriptor pfd)
		{
			if (pfd.HasUserdata) //Deliver Userdefined Data
			{
				IPackedFile pf = new PackedFile(pfd.UserData);
				return pf;
			} 
			else //no Userdefine data available
			{
				#region Reload Stream
				OpenReader();
				/*if (reader==null) 
				{
					if (this.flname!=null) if (System.IO.File.Exists(flname)) reader = new System.IO.BinaryReader(System.IO.File.OpenRead(flname));						
				}
				if (reader==null) 
				{
					CloseReader();
					return new PackedFile(new byte[0]);
				}
				if (reader.BaseStream==null) 
				{
					if (this.flname!=null) if (System.IO.File.Exists(flname)) reader = new System.IO.BinaryReader(System.IO.File.OpenRead(flname));
				}*/
				if (reader==null) return new PackedFile(new byte[0]);
				if (reader.BaseStream==null) 
				{
					CloseReader();
					return new PackedFile(new byte[0]);
				}
				#endregion

				try 
				{
					this.LockStream();
					reader.BaseStream.Seek(pfd.Offset, System.IO.SeekOrigin.Begin);
					Byte[] data = reader.ReadBytes(pfd.Size);
					//for (int i=0; i<data.Length; i++) data[i] = reader.ReadByte();

					PackedFile pf = GetPackedFile(pfd, data);
					return (IPackedFile)pf;
				}
				catch (Exception ex) 
				{
					Helper.ExceptionMessage(new Warning("Unabled to Read/Decode \""+pfd.ExceptionString+"\". Do not save this package!!!", ex.Message, ex));
				}
				finally 
				{
					this.UnLockStream();
					CloseReader();
				}

				return new PackedFile(new byte[0]);
			} // if HasUserdata
		}

		/// <summary>
		/// Returns a List ofa all Files matching the passed type
		/// </summary>
		/// <param name="type">Type you want to look for</param>
		/// <returns>A List of Files</returns>
		public IPackedFileDescriptor[] FindFiles(uint type) 
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList();

			if (fileindex!=null) 
			{
				list = fileindex.FindFile(type);
			}
			

			IPackedFileDescriptor[] ret = new IPackedFileDescriptor[list.Count];
			list.CopyTo(ret);
			return ret;
		}

		/// <summary>
		/// Returns a List ofa all Files matching the passed group
		/// </summary>
		/// <param name="group">Group you want to look for</param>
		/// <returns>A List of Files</returns>
		public IPackedFileDescriptor[] FindFilesByGroup(uint group) 
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList();

			if (fileindex!=null) 
				list = fileindex.FindFileByGroup(group);			

			IPackedFileDescriptor[] ret = new IPackedFileDescriptor[list.Count];
			list.CopyTo(ret);
			return ret;
		}

		/// <summary>
		/// Returns all Files that could contain a RCOL with the passed Filename
		/// </summary>
		/// <param name="filename">The Filename you are looking for</param>
		/// <returns>List of matching Files</returns>
		/// <remarks>Removes Forced Relocation Strings like #0x12345678!</remarks>
		public IPackedFileDescriptor[] FindFile(string filename) 
		{
			filename = Hashes.StripHashFromName(filename);
			uint inst = Hashes.InstanceHash(filename);
			uint st = Hashes.SubTypeHash(filename);

			IPackedFileDescriptor[] ret = FindFile(st, inst);
			if (ret.Length==0) ret = FindFile(0, inst);
			return ret;
		}

		/// <summary>
		/// Returns all Files that could contain a RCOL with the passed Filename
		/// </summary>
		/// <param name="filename">The Filename you are looking for</param>
		/// <returns>List of matching Files</returns>
		public IPackedFileDescriptor[] FindFile(string filename, uint type) 
		{
			filename = Hashes.StripHashFromName(filename);
			uint inst = Hashes.InstanceHash(filename);
			uint st = Hashes.SubTypeHash(filename);

			IPackedFileDescriptor[] ret = FindFile(type, st, inst);
			if (ret.Length==0) ret = FindFile(type, 0, inst);
			return ret;
		}

		/// <summary>
		/// Returns the first File matching 
		/// </summary>
		/// <param name="subtype">SubType you want to look for</param>
		/// <returns>The descriptor for the matching Dile or null</returns>
		public IPackedFileDescriptor[] FindFile(uint subtype, uint instance) 
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList();

			if (fileindex!=null) 
				list = fileindex.FindFileByInstance(subtype, instance);						

			IPackedFileDescriptor[] ret = new IPackedFileDescriptor[list.Count];
			list.CopyTo(ret);
			return ret;
		}

		/// <summary>
		/// Returns the first File matching 
		/// </summary>
		/// <param name="type">Type you want to look for</param>
		/// <returns>The descriptor for the matching Dile or null</returns>
		public IPackedFileDescriptor[] FindFile(uint type, uint subtype, uint instance) 
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList();

			if (fileindex!=null) 
				list = fileindex.FindFileByInstance(type, subtype, instance);							

			IPackedFileDescriptor[] ret = new IPackedFileDescriptor[list.Count];
			list.CopyTo(ret);
			return ret;
		}

		/// <summary>
		/// Returns the first File matching 
		/// </summary>
		/// <param name="pfd">Type you want to look for</param>
		/// <returns>The descriptor for the matching Dile or null</returns>
		public IPackedFileDescriptor FindFile(Interfaces.Files.IPackedFileDescriptor pfd) 
		{
			return FindFile(pfd.Type, pfd.SubType, pfd.Group, pfd.Instance);
		}

		/// <summary>
		/// Returns the first File matching 
		/// </summary>
		/// <param name="type">Type you want to look for</param>
		/// <returns>The descriptor for the matching Dile or null</returns>
		public IPackedFileDescriptor FindFile(uint type, uint subtype, uint group, uint instance) 
		{
			if (fileindex!=null) 
			{
				PackedFileDescriptors list = fileindex.FindFile(type, group, subtype, instance);
				if (list.Length>0) return list[0];				
			}

			return null;
		}
		#endregion		

		/// <summary>
		/// Close this Instance, leaving the FileDescripors valid
		/// </summary>
		public void Close()
		{
			Close(false);
		}

		/// <summary>
		/// Close this Instance
		/// </summary>
		/// <param name="total">true, if the FileDescriptors should be marked invalid</param>
		public void Close(bool total)
		{
			if (this.Reader!=null) Reader.Close();
			if (total) 
			{
				if (this.Index != null) 
				{
					foreach (Interfaces.Files.IPackedFileDescriptor pfd in this.Index) 
						if (pfd!=null) pfd.MarkInvalid();	
				}
			}
		}

		/// <summary>
		/// Converts the given Char Array into a String
		/// </summary>
		/// <param name="array">The Char Array</param>
		/// <returns>The String represented by the Chars stored in the Array</returns>
		public static string CharArrayToString(char[] array)
		{
			string s = "";
			foreach(char c in array) s+=c.ToString();			
			return s;
		}

		/// <summary>
		/// Create a new GeneratableFile
		/// </summary>
		/// <param name="flname"></param>
		/// <returns></returns>
		public static GeneratableFile LoadFromFile(string flname) 
		{
			return PackageMaintainer.Maintainer.LoadPackageFromFile(flname, false);
		}

		/// <summary>
		/// Create a new GeneratableFile
		/// </summary>
		/// <param name="flname"></param>
		/// <param name="sync">true, if the content should be synced with the FileSystem</param>
		/// <returns></returns>
		public static GeneratableFile LoadFromFile(string flname, bool sync) 
		{
			return PackageMaintainer.Maintainer.LoadPackageFromFile(flname, sync);
		}

		/// <summary>
		/// Create a new File
		/// </summary>
		/// <param name="flname"></param>
		/// <returns></returns>
		public static  GeneratableFile LoadFromStream(BinaryReader br) 
		{
			return new GeneratableFile(br);
		}

		/// <summary>
		/// Creates a new Empty Package File
		/// </summary>
		/// <returns></returns>
		public static GeneratableFile CreateNew() 
		{
			return SimPe.Packages.GeneratableFile.LoadFromStream(new System.IO.BinaryReader(SimPe.Packages.GeneratableFile.LoadFromStream(null).Build()));
		}

		public override int GetHashCode()
		{
			if (this.FileName==null) 
			{
				if (this.Reader==null) return base.GetHashCode ();
				else return this.Reader.GetHashCode();		
			}
			else return FileName.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj==null) return false;
			if (!(obj is File)) return false;

			File f = (File)obj;

			if (f.FileName==null) 
			{
				if (this.FileName!=null) return false;				
			} else if (this.FileName==null) return false;

			if (f.FileName==null && this.FileName==null) 
			{
				if (this.Reader==null) 
				{
					return f.Reader==null;
				} if (f.Reader==null) return false;

				return this.Reader.Equals(f.Reader);
			} 
			else 
			{
#if MAC
			return this.FileName.Trim()==f.FileName.Trim();
#else
				return this.FileName.Trim().ToLower()==f.FileName.Trim().ToLower();
#endif
			}
	
		}

		#region Events
		bool pause;
		bool indexevent, addevent, remevent;
		protected void FireAddEvent() 
		{
			if (pause) { addevent = true; return; }
			if (this.AddedResource!=null) AddedResource(this, new System.EventArgs());
		}
		protected void FireRemoveEvent() 
		{
			if (pause) { remevent = true;  return; }
			if (this.RemovedResource!=null) RemovedResource(this, new System.EventArgs());
		}
		protected void FireIndexEvent() 
		{			
			FireIndexEvent(new System.EventArgs());
		}

		protected void FireIndexEvent(System.EventArgs e)
		{
			if (pause) { indexevent = true; return; }
			if (this.IndexChanged!=null) IndexChanged(this, e);
		}

		public void BeginUpdate()
		{
			if (pause) return;
			indexevent = false;
			addevent = false;
			remevent = false;
			pause = true;
			foreach (SimPe.Interfaces.Files.IPackedFileDescriptor pfd in Index)
				pfd.BeginUpdate();
		}

		public void EndUpdate()
		{
			if (!pause) return;
			pause = false;
			foreach (SimPe.Interfaces.Files.IPackedFileDescriptor pfd in Index)
				pfd.EndUpdate();

			if (indexevent) FireIndexEvent();
			if (remevent) FireRemoveEvent();
			if (addevent) FireAddEvent();
		}
		/// <summary>
		/// Called whenever the content represented by this descripotr was changed
		/// </summary>
		public event PackedFileChanged ChangedResource;

		/// <summary>
		/// Pass the Event fired by one of the attached Resources (pfds) to assigned listeners
		/// </summary>
		/// <param name="sender"></param>
		void ResourceChanged(SimPe.Interfaces.Files.IPackedFileDescriptor sender)
		{
			if (ChangedResource!=null) ChangedResource(sender);
		}

		/// <summary>
		/// Triggered whenever the Content of the Package was changed
		/// </summary>
		public event System.EventHandler IndexChanged;

		/// <summary>
		/// Triggered whenever a new Resource was added
		/// </summary>
		public event System.EventHandler AddedResource;

		/// <summary>
		/// Triggered whenever a Resource was Removed
		/// </summary>
		public event System.EventHandler RemovedResource;
		
		/// <summary>
		/// Fired when a Description gets Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResourceDescriptionChanged(object sender, EventArgs e)
		{
			FireIndexEvent(e);
		}
		#endregion
	}
}