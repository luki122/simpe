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
using System.IO;
using System.Xml;
#if MAC
#else
using Microsoft.Win32;
#endif


namespace SimPe
{
	/// <summary>
	/// Handles Application Settings stored in the Registry
	/// </summary>
	/// <remarks>You cannot create instance of this class, use the 
	/// <see cref="SimPe.Helper.WindowsRegistry"/> Field to acces the Registry</remarks>
	public class Registry
	{
		#region Attributes
		///Number of Recent Files stored in the Reg
		public const byte RECENT_COUNT = 15;
#if MAC
#else
		/// <summary>
		/// The Root Registry Kex for this Application
		/// </summary>
		private RegistryKey rk;
#endif

		/// <summary>
		/// Contains the Registry
		/// </summary>
		XmlRegistry reg;

		/// <summary>
		/// The Root Registry Key for this Application
		/// </summary>
		XmlRegistryKey xrk;

		LayoutRegistry lr;
		/// <summary>
		/// Returns the LayoutRegistry
		/// </summary>
		public LayoutRegistry Layout 
		{
			get {return lr;}
		}

		int pep, pepct; long pver;
		#endregion

		#region Management
		/// <summary>
		/// Creates a new Instance
		/// </summary>
		internal Registry()
		{
#if MAC			
#else
			rk = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Ambertation\\SimPe");
#endif
			pver = this.GetPreviousVersion();
			pep = this.GetPreviousEp();
			pepct = this.GetPreviousEpCount();
			Reload();
			if (Helper.QARelease) this.WasQAUser=true;
		}

		/// <summary>
		/// Reload the SimPe Registry
		/// </summary>
		public void Reload()
		{			
			reg = new XmlRegistry(System.IO.Path.Combine(Helper.SimPeDataPath, "simpe.xreg"), true);
			xrk = reg.CurrentUser.CreateSubKey("Software\\Ambertation\\SimPe");
			ReloadLayout();
		}

		/// <summary>
		/// Reload the SimPe Registry
		/// </summary>
		public void ReloadLayout()
		{
			lr = new LayoutRegistry(xrk.CreateSubKey("Layout"));		
		}

		/// <summary>
		/// Descturtor 
		/// </summary>
		/// <remarks>
		/// Will flsuh the XmlRegistry to the disk
		/// </remarks>
		~Registry()
		{			
			//Flush();
		}

		/// <summary>
		/// Write the Settings to the Disk
		/// </summary>
		public void Flush() 
		{
			if (lr!=null) lr.Flush();
			if (reg!=null) reg.Flush();
		}

		/// <summary>
		/// Returns the Registry Key you can use to store Optional Plugin Data
		/// </summary>
		public XmlRegistryKey PluginRegistryKey
		{
			get 
			{
				return xrk.CreateSubKey("PluginSettings");
			}
		}

		/// <summary>
		/// Returns the Base Registry Key
		/// </summary>
		public XmlRegistryKey RegistryKey
		{
			get 
			{
				return xrk;
			}
		}
		#endregion

		/// <summary>
		/// Update the SimPE paths
		/// </summary>
		public void UpdateSimPEDirectory()
		{
#if MAC
			XmlRegistryKey rkf = xrk.CreateSubKey("Settings");	
#else
			RegistryKey rkf = rk.CreateSubKey("Settings");			
#endif
			rkf.SetValue("Path", Helper.SimPePath);
			rkf.SetValue("DataPath", Helper.SimPeDataPath);
			rkf.SetValue("PluginPath", Helper.SimPePluginPath);
			rkf.SetValue("LastVersion", Helper.SimPeVersionLong);
		}

		/// <summary>
		/// Returns the DataFolder as set by the last SimPe run
		/// </summary>
		public string PreviousDataFolder
		{
			get
			{
#if MAC
				return "";
#else
				RegistryKey rkf = rk.CreateSubKey("Settings");	
				return rkf.GetValue("DataPath", "").ToString();
#endif
			}
		}
		
		/// <summary>
		/// Returns the DataFolder as set by the last SimPe run
		/// </summary>
		protected long GetPreviousVersion()
		{
			
#if MAC
				return 0;
#else
			RegistryKey rkf = rk.CreateSubKey("Settings");	
			return Convert.ToInt64(rkf.GetValue("LastVersion", (long)0));
#endif			
		}

		
		/// <summary>
		/// Returns the number of the latest EP used, and writes the new Number to the Registry
		/// </summary>
		protected int GetPreviousEp()
		{
			
#if MAC
				return this.EPInstalled;
#else
			RegistryKey rkf = rk.CreateSubKey("Settings");	
			int res = Convert.ToInt32(rkf.GetValue("LatestEP", 0));

			rkf.SetValue("LatestEP", this.EPInstalled);
			return res;
#endif			
		}		

		/// <summary>
		/// Returns the Version of the latest SimPE used so far
		/// </summary>
		public long PreviousVersion
		{
			get
			{
				return pver;
			}
		}

		/// <summary>
		/// Returns the latest number of the Expansion used so far
		/// </summary>
		public int PreviousEp
		{
			get
			{
				return pep;
			}
		}

		#region EP Handler
		public bool FoundUnknownEP()
		{
			//if (PreviousEpCount<EpCount) return true;

			string[] inst = InstalledEPExecutables;
			if (inst.Length==0) return false;

			foreach (string si in inst)
			{
				if (si=="") continue;
				bool found = false;
				foreach(string s in Registry.EPExecutables)		
				{		
					string n = s.ToLower().Trim();
					if (n=="") continue;
					if (si==n)
					{
						found = true;
						break;
					}
				}

				if (!found) return true;
			}

			return false;
		}

		/// <summary>
		/// Returns the latest number of the Expansion used so far
		/// </summary>
		protected int PreviousEpCount
		{
			get
			{
				return pep;
			}
		}

		protected string[] InstalledEPExecutables
		{
			get 
			{
#if MAC
				return new string[0];
#else
				Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\EA GAMES\The Sims 2\", false);
				if (rk!=null)
				{
					object o = rk.GetValue("EPsInstalled", "");
					if (o==null) return new string[0];

					string s = o.ToString();
				
					string[] ret = s.Split(new char[] {','});
					for (int i=0; i<ret.Length; i++)
						ret[i] = ret[i].ToLower().Trim();

					return ret;
				}
				else return new string[0];
#endif
			}
		}

		protected int EPCount
		{
			get 
			{
#if MAC
				return 0;
#else
				Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\EA GAMES\", false);
				if (rk!=null)
				{
					if (rk!=null) return rk.SubKeyCount;
				}
				return 0;
#endif
			}
		}

		/// <summary>
		/// Returns the number of the latest EP used, and writes the new Number to the Registry
		/// </summary>
		protected int GetPreviousEpCount()
		{
			
#if MAC
				return 0;
#else
			RegistryKey rkf = rk.CreateSubKey("Settings");	
			int res = Convert.ToInt32(rkf.GetValue("LastEPCount", this.EPCount));

			rkf.SetValue("LastEPCount", this.EPCount);
			return res;
#endif			
		}
		#endregion

		/// <summary>
		/// true, if the user wants the Pescado Mode
		/// </summary>
		public  bool Silent
		{
			get 
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("PescadoMode", false);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("PescadoMode", value);
			}
		}

		/// <summary>
		/// true, if user wants to activate the Cache
		/// </summary>
		public  bool UseCache
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("UseCache", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("UseCache", value);
			}
		}

		/// <summary>
		/// true, if user wants to show the OBJD Filenames in OW
		/// </summary>
		public  bool ShowObjdNames
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("ShowObjdNames", false);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("ShowObjdNames", value);
			}
		}

		

		/// <summary>
		/// true, if user wants to show the Name of a Joint in the GMDC Plugin
		/// </summary>
		public  bool ShowJointNames
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("ShowJointNames", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("ShowJointNames", value);
			}
		}

		/// <summary>
		/// the Scaling Factor that is used by the Gmdc Importer/Exporter
		/// </summary>
		public  float ImportExportScaleFactor
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("ImExportScale", 1.0f);
				return Convert.ToSingle(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("ImExportScale", value);
			}
		}

		/// <summary>
		/// true, if user wants to activate the Cache
		/// </summary>
		public  bool XPStyle
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("XPStyle", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("XPStyle", value);
			}
		}

		/// <summary>
		/// true, if the user wanted to use the HexViewer
		/// </summary>
		public  bool HexViewState 
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("HexViewEnabled", false);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("HexViewEnabled", value);
			}
		}

		/// <summary>
		/// true, if the HiddenMode is activated
		/// </summary>
		public bool HiddenMode
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("EnableSimPEHiddenMode", false);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("EnableSimPEHiddenMode", value);
			}
		}

		/// <summary>
		/// true, if the user wanted to decode Filenames
		/// </summary>
		public  bool DecodeFilenamesState 
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("DecodeFilenames", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("DecodeFilenames", value);
			}
		}

		/// <summary>
		/// GUID Username
		/// </summary>
		public string Username
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("Username", "");
				return o.ToString();
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("Username", value);
			}
		}

		/// <summary>
		/// the cached UserId
		/// </summary>
		public uint CachedUserId
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("CUi", 0);
				return Convert.ToUInt32(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("CUi", value);
			}
		}

		

		/// <summary>
		/// GUID Username
		/// </summary>
		public Data.MetaData.Languages LanguageCode
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("Language");
				if (o==null) return Helper.GetMatchingLanguage();
				else return (Data.MetaData.Languages)Convert.ToByte(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("Language", (byte)value);
			}
		}

		/// <summary>
		/// GUID Password
		/// </summary>
		public string Password
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("Password", "");
				return o.ToString();
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("Password", value);
			}
		}

		/// <summary>
		/// The Version that was executed last Time SimPE was started
		/// </summary>
		public int Version
		{
			get 
			{
				try 
				{
					XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
					object o = rkf.GetValue("Version", 0);					
					return (int)o;
				} 
				catch (Exception) 
				{
					return 0;
				}
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("Version", value);
			}
		}

		/// <summary>
		/// Returns the Thumbnail Size for Treeview Items in OW
		/// </summary>
		public int OWThumbSize
		{
			get 
			{
				try 
				{
					XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
					object o = rkf.GetValue("OWThumbSize", 16);
					return (int)o;
				} 
				catch (Exception) 
				{
					return 16;
				}
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("OWThumbSize", value);
			}
		}

		

		

		

		/// <summary>
		/// Name of the Nvidia DDS Path
		/// </summary>
		public string NvidiaDDSPath
		{
			get 
			{
				try 
				{
					XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
					object o = rkf.GetValue("NvidiaDDS");
					if (o==null) return "";
					return o.ToString();
				} 
				catch (Exception) 
				{
					return "";
				}
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("NvidiaDDS", value);
			}
		}

		

		/// <summary>
		/// Name of the Sims Application
		/// </summary>
		public string SimsPath
		{
			get 
			{
				try 
				{
					XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
					object o = rkf.GetValue("SimsPath");
					if (o==null) return RealGamePath;
					else 
					{
						string fl = o.ToString();						
						if (!System.IO.Directory.Exists(fl)) return this.RealGamePath;
						return fl;
					}
				} 
				catch (Exception) 
				{
					return this.RealGamePath;;
				}
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("SimsPath", value);
			}
		}

		/// <summary>
		/// Name of the Sims Application
		/// </summary>
		public string SimsEP1Path
		{
			get 
			{
				try 
				{
					XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
					object o = rkf.GetValue("SimsEP1Path");
					if (o==null) return this.RealEP1GamePath;
					else 
					{
						string fl = o.ToString();

						if (!System.IO.Directory.Exists(fl)) return this.RealEP1GamePath;
						return fl;
					}
				} 
				catch (Exception) 
				{
					return this.RealEP1GamePath;
				}
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("SimsEP1Path", value);
			}
		}

		/// <summary>
		/// Name of the Sims Application
		/// </summary>
		public string SimsEP2Path
		{
			get 
			{
				try 
				{
					XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
					object o = rkf.GetValue("SimsEP2Path");
					if (o==null) return this.RealEP2GamePath;
					else 
					{
						string fl = o.ToString();

						if (!System.IO.Directory.Exists(fl)) return this.RealEP2GamePath;
						return fl;
					}
				} 
				catch (Exception) 
				{
					return this.RealEP2GamePath;
				}
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("SimsEP2Path", value);
			}
		}

		/// <summary>
		/// Name of the Sims Application
		/// </summary>
		public string SimsEP3Path
		{
			get 
			{
				try 
				{
					XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
					object o = rkf.GetValue("SimsEP3Path");
					if (o==null) return this.RealEP3GamePath;
					else 
					{
						string fl = o.ToString();

						if (!System.IO.Directory.Exists(fl)) return this.RealEP3GamePath;
						return fl;
					}
				} 
				catch (Exception) 
				{
					return this.RealEP3GamePath;
				}
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("SimsEP3Path", value);
			}
		}

		protected static string[] EPExecutables = new string[] {
			"Sims2.exe",
			"Sims2EP1.exe",
			"Sims2EP2.exe",
			"Sims2EP3.exe"
		};

		public static string GetEpName(int index)
		{
			return SimPe.Localization.GetString("EP NAME "+index);			
		}

		public string CurrentEPName
		{
			get { return GetEpName(this.EPInstalled);}
		}

		public static string GetExecutableName(int index)
		{
			if (index<0) index=0;
			if (index>=EPExecutables.Length) index = EPExecutables.Length-1;
			return EPExecutables[index];
		}

		 
		public string GetExecutableFolder(int index)
		{
			if (index==1) return this.SimsEP1Path;
			if (index==2) return this.SimsEP2Path;
			if (index==3) return this.SimsEP3Path;
			return this.SimsPath;
		}

		/// <summary>
		/// Name of the Sims Application
		/// </summary>
		public string SimsApplication
		{
			get 
			{
				try 
				{
					return System.IO.Path.Combine(GetExecutableFolder(this.EPInstalled), "TSBin\\"+GetExecutableName(this.EPInstalled));
				} 
				catch (Exception) 
				{
					return "";
				}
			}
			
		}

		/// <summary>
		/// This Folder contains al Sims User Data
		/// </summary>
		public string SimSavegameFolder 
		{
			get 
			{
				try 
				{
					XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
					object o = rkf.GetValue("SavegamePath");
					if (o==null) 
					{
						return this.RealSavegamePath;
					}
					else 
					{
						string fl = o.ToString();
						if (!System.IO.Directory.Exists(fl)) return this.RealSavegamePath;
						return fl;
					}
				} 
				catch (Exception) 
				{
					return this.RealSavegamePath;
				}
			}
			set 
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("SavegamePath", value);
			}
		}

		/// <summary>
		/// true, if the user wanted to use the HexViewer
		/// </summary>
		public bool LoadMetaInfo 
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("LoadMetaInfos", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("LoadMetaInfos", value);
			}
		}

		/// <summary>
		/// true, if the user wants to autocheck for Updates
		/// </summary>
		public bool CheckForUpdates 
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
#if MAC
				object o = rkf.GetValue("CheckForUpdates", false);
#else
				object o = rkf.GetValue("CheckForUpdates", true);
#endif
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("CheckForUpdates", value);
			}
		}

		/// <summary>
		/// true, if the user want's to start the Game with Sound
		/// </summary>
		public bool EnableSound 
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("EnableSound", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("EnableSound", value);
			}
		}

		/// <summary>
		/// true, if the user wanted to use the HexViewer
		/// </summary>
		public bool AutoBackup 
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("AutoBackup", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("AutoBackup", value);
			}
		}

		/// <summary>
		/// true, if the user wants the Waiting Screen
		/// </summary>
		public bool WaitingScreen 
		{
			get 
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("WaitingScreen", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("WaitingScreen", value);
			}
		}

		/// <summary>
		/// true, if the user wants the Waiting Screen as a TopMost Window
		/// </summary>
		public bool WaitingScreenTopMost
		{
			get 
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("WaitingScreenTopMost", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("WaitingScreenTopMost", value);
			}
		}

		/// <summary>
		/// true, if the user wants the Waiting Screen
		/// </summary>
		public bool LoadOWFast 
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("LoadOWFast", false);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("LoadOWFast", value);
			}
		}

		/// <summary>
		/// true, if the user wantsto use the package Maintainer
		/// </summary>
		public bool UsePackageMaintainer 
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("UsePkgMaintainer", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("UsePkgMaintainer", value);
			}
		}

		/// <summary>
		/// true, if the user wantsto use the package Maintainer
		/// </summary>
		public bool MultipleFiles
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("MultipleFiles", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("MultipleFiles", value);
			}
		}

		/// <summary>
		/// true, if the user should select a Resource with only one click
		/// </summary>
		public bool SimpleResourceSelect
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("SimpleResourceSelect", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("SimpleResourceSelect", value);
			}
		}		

		/// <summary>
		/// true, if the user want's to control the Tabs like done in FireFox
		/// </summary>
		public bool FirefoxTabbing
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("FirefoxTabbing", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("FirefoxTabbing", value);
			}
		}

		/// <summary>
		/// true, if the user ever started a QA Version
		/// </summary>
		public bool WasQAUser
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("WasQAUser", false);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("WasQAUser", value);
			}
		}

		/// <summary>
		/// Number of Resource Files per package
		/// </summary>
		public int BigPackageResourceCount
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("BigPackageResourceCount", 1000);
				return Convert.ToInt32(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("BigPackageResourceCount", value);
			}
		}

		/// <summary>
		/// The LineMode that we should use for the GraphControls
		/// </summary>
		public int GraphLineMode
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("GraphLineMode", 0x02);
				return Convert.ToInt16(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("GraphLineMode", value);
			}
		}

		/// <summary>
		/// should we use Qulity Mode?
		/// </summary>
		public bool GraphQuality
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("GraphQuality", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("GraphQuality", value);
			}
		}

		/// <summary>
		/// returns the last Extension used during a GMDC import/export
		/// </summary>
		public string GmdcExtension
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("GmdcExtension", ".obj");
				string s = o.ToString();
				return s.Replace("*", "");
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("GmdcExtension", value);
			}
		}

		/// <summary>
		/// true, if the user did want to correct the Joint definitions during the last Export
		/// </summary>
		public bool CorrectJointDefinitionOnExport
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("CorrectJointDefinitionOnExport", false);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("CorrectJointDefinitionOnExport", value);
			}
		}

		/// <summary>
		/// Should we search the objects.package's for Sims?
		/// </summary>
		public bool DeepSimScan
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("DeepSimScan", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("DeepSimScan", value);
			}
		}

		/// <summary>
		/// Should we search the objects.package's for Sims?
		/// </summary>
		public bool DeepSimTemplateScan
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("DeepSimTemplateScan", false);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("DeepSimTemplateScan", value);
			}
		}

		/// <summary>
		/// Schould we load Stuff Asynchron to the main Thread?
		/// </summary>
		public bool AsynchronLoad
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("AsynchronLoad", false);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("AsynchronLoad", value);
			}
		}

		/// <summary>
		/// Schould we lock the Docks?
		/// </summary>
		public bool LockDocks
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("LockDocks", false);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("LockDocks", value);
			}
		}

		/// <summary>
		/// When did whe perform the last UpdateCheck?
		/// </summary>
		public DateTime LastUpdateCheck
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("LastUpdateCheck", DateTime.Now.Subtract(new TimeSpan(2, 0, 0, 0, 0)));
				return Convert.ToDateTime(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("LastUpdateCheck", value);
			}
		}

		#region Report Format
		public enum ReportFormats : int
		{
			Descriptive,
			CSV
		}

		/// <summary>
		/// The Which Format do Reports have
		/// </summary>
		public ReportFormats ReportFormat
		{
			get 
			{
				XmlRegistryKey  rkf = xrk.CreateSubKey("Settings");
				object o = rkf.GetValue("ReportFormat", (int)ReportFormats.Descriptive);
				return (ReportFormats)Convert.ToInt32(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey("Settings");
				rkf.SetValue("ReportFormat", (int)value);
			}
		}
		#endregion

		#region Wrappers
		/// <summary>
		/// Returns the Priority for the Wrapper identified with the given UID
		/// </summary>
		/// <param name="uid">uique id of the Wrapper</param>
		/// <returns>Priority for the Wrapper</returns>
		public int GetWrapperPriority(ulong uid)
		{
			XmlRegistryKey  rkf = xrk.CreateSubKey("Priorities");
			object o = rkf.GetValue(Helper.HexString(uid));
			if (o==null) return 0x00000000;
			else return Convert.ToInt32(o);
		}

		/// <summary>
		/// Stores the Priority of a Wrapper
		/// </summary>
		/// <param name="uid">uique id of the Wrapper</param>
		/// <param name="priority">the new Priority</param>
		public void SetWrapperPriority(ulong uid, int priority) 
		{
			XmlRegistryKey rkf = xrk.CreateSubKey("Priorities");
			rkf.SetValue(Helper.HexString(uid), priority);
		}
		#endregion

		#region recent Files
		public void ClearRecentFileList()
		{
			XmlRegistryKey rkf = xrk.CreateSubKey("Listings");
			rkf.SetValue("RecentFiles", new Ambertation.CaseInvariantArrayList());
		}

		/// <summary>
		/// Returns a List of recently opened Files
		/// </summary>
		/// <returns>List of Filenames</returns>
		public string[] GetRecentFiles() 
		{
			XmlRegistryKey  rkf = xrk.CreateSubKey("Listings");
			Ambertation.CaseInvariantArrayList al = (Ambertation.CaseInvariantArrayList)rkf.GetValue("RecentFiles", new Ambertation.CaseInvariantArrayList());
			

			string[] res = new string[al.Count];
			al.CopyTo(res);
			return res;
		}

		/// <summary>
		/// Adds a File to the List of recently opened Files
		/// </summary>
		/// <param name="filename">The Filename</param>
		public void AddRecentFile(string filename) 
		{
			if (filename==null) return;
			if (filename.Trim()=="") return;
			if (!System.IO.File.Exists(filename)) return;
			
			filename = filename.Trim();
			XmlRegistryKey rkf = xrk.CreateSubKey("Listings");
			
			Ambertation.CaseInvariantArrayList al = (Ambertation.CaseInvariantArrayList)rkf.GetValue("RecentFiles", new Ambertation.CaseInvariantArrayList());	
			if (al.Contains(filename)) 
				al.Remove(filename);
			
			al.Insert(0, filename);			
			while (al.Count>RECENT_COUNT) al.RemoveAt(al.Count-1);
			rkf.SetValue("RecentFiles", al);
		}
		#endregion		

		#region Starup Cheat File
		/// <summary>
		/// Returns true if the Game will start in Debug Mode
		/// </summary>
		public bool GameDebug 
		{
			get 
			{
				if (!System.IO.File.Exists(this.StartupCheatFile)) return false;

				try 
				{
					System.IO.TextReader fs = System.IO.File.OpenText(this.StartupCheatFile);
					string cont = fs.ReadToEnd();
					fs.Close();
					string[] lines = cont.Split("\n".ToCharArray());

					foreach (string line in lines) 
					{
						string pline = line.ToLower().Trim();
						while (pline.IndexOf("  ")!=-1) pline = pline.Replace("  ", " ");
						string[] tokens = pline.Split(" ".ToCharArray());

						if (tokens.Length==3) 
						{
							if ( (tokens[0]=="boolprop") &&
								(tokens[1]=="testingcheatsenabled") &&
								(tokens[2]=="true") 
								) return true;
						}
					}
				} 
				catch (Exception) {}

				return false;
			}

			set 
			{
				if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(this.StartupCheatFile))) return;

				try 
				{
					string newcont = "";
					bool found = false;
					if (System.IO.File.Exists(this.StartupCheatFile)) 
					{
						System.IO.TextReader fs = System.IO.File.OpenText(this.StartupCheatFile);
						string cont = fs.ReadToEnd();
						fs.Close();
						
						string[] lines = cont.Split("\n".ToCharArray());
						

						foreach (string line in lines) 
						{
							string pline = line.ToLower().Trim();
							while (pline.IndexOf("  ")!=-1) pline = pline.Replace("  ", " ");
							string[] tokens = pline.Split(" ".ToCharArray());

							if (tokens.Length==3) 
							{
								if ( (tokens[0]=="boolprop") &&
									(tokens[1]=="testingcheatsenabled") 
									) 
								{
									if (!found) 
									{
										newcont += "boolProp testingCheatsEnabled ";
										if (value) newcont += "true"; else newcont += "false";
										newcont += Helper.lbr;
										found = true;
									}
									continue;
								}
							}
							newcont += line.Trim();
							newcont += Helper.lbr;
						}

						System.IO.File.Delete(this.StartupCheatFile);
					}

					if (!found) 
					{
						newcont += "boolProp testingCheatsEnabled ";
						if (value) newcont += "true"; else newcont += "false";
						newcont += Helper.lbr;
					}

					System.IO.TextWriter fw = System.IO.File.CreateText(this.StartupCheatFile);
					fw.Write(newcont.Trim());
					fw.Close();
				} 
				catch (Exception) {}
			}
		}		
		#endregion

		#region Censor Patch
		/// <summary>
		/// Returns true if the Game will start in Debug Mode
		/// </summary>
		public bool BlurNudity 
		{
			get 
			{
				if (this.EPInstalled<=1) return BlurNudityPreEP2;
				else if (this.EPInstalled>=2) return BlurNudityEP2;					
				else return false;
			}
			set 
			{
				if (this.EPInstalled<=1) 
				{
					BlurNudityEP2 = false;
					BlurNudityEP3 = false;
					BlurNudityPreEP2 = value;
				}
				else if (this.EPInstalled==2) 
				{
					BlurNudityEP2 = value;
				}
				else if (this.EPInstalled>=3) 
				{
					BlurNudityEP3 = value;
				}
				else 
				{
					BlurNudityEP2 = false;
					BlurNudityPreEP2 = false;
				}
			}
		}

		protected string[] CensorFiles
		{
			get 
			{
				return new string[]{				    
					System.IO.Path.Combine(this.SimSavegameFolder, @"Config\quaxi_ofb_censor_v1.package"),
				    System.IO.Path.Combine(this.SimSavegameFolder, @"Config\quaxi_nl_censor_v1.package"),
					System.IO.Path.Combine(this.SimSavegameFolder, @"Downloads\quaxi_nl_censor_v1.package"),										
					System.IO.Path.Combine(this.SimSavegameFolder, @"Downloads\quaxi_nl_censor.package")
								   };
			}
		}

		protected bool BlurNudityEP2 
		{
			get { return GetBlurNudity(); }
			set { SetBlurNudity(value, "quaxi_nl_censor_v1.package", false);	}
		}

		protected bool BlurNudityEP3 
		{
			get { return GetBlurNudity(); }
			set { SetBlurNudity(value, "quaxi_ofb_censor_v1.package", false);	}
		}

		public void BlurNudityUpdate()
		{
			if (EPInstalled>=3 &&  !GetBlurNudity())
			{
				SetBlurNudity(true, System.IO.Path.GetFileName(CensorFiles[0]), true);
				SetBlurNudity(false, System.IO.Path.GetFileName(CensorFiles[0]), true);
			}
		}

		bool GetBlurNudity()
		{
			string[] fls = CensorFiles;
			foreach (string fl in fls)
				if (System.IO.File.Exists(fl)) return false;

			return true;
		}
		void SetBlurNudity(bool value, string resname, bool silent)
		{
			string[] fls = CensorFiles;
			if (!value) 
			{					
				string fl = fls[0];
				string folder = System.IO.Path.GetDirectoryName(fl);

				if (System.IO.File.Exists(fl)) return;

				if (!silent)
					if (System.Windows.Forms.MessageBox.Show(SimPe.Localization.GetString("Censor_Install_Warn").Replace("{filename}", fl), SimPe.Localization.GetString("Warning"), System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.No)
						return;

				try 
				{
					if (!System.IO.Directory.Exists(folder))
						System.IO.Directory.CreateDirectory(folder);

					System.IO.Stream s = typeof(Helper).Assembly.GetManifestResourceStream("SimPe."+resname);
					System.IO.BinaryReader br = new BinaryReader(s);
					try 
					{
						System.IO.BinaryWriter bw = new BinaryWriter(System.IO.File.Create(fl));
						try 
						{

							bw.Write(br.ReadBytes((int)br.BaseStream.Length));
						} 
						finally 
						{
							bw.Close();
						}
					} 
					finally 
					{
						br.Close();
					}
				}
				catch (Exception ex) 
				{
					Helper.ExceptionMessage(ex);
				}
			} 
			else 
			{					
				foreach (string fl in fls)
					if (System.IO.File.Exists(fl)) 
					{
						try 
						{
							if (!silent)
								if (System.Windows.Forms.MessageBox.Show(SimPe.Localization.GetString("Censor_UnInstall_Warn").Replace("{filename}", fl), SimPe.Localization.GetString("Warning"), System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.No)
									return;
							System.IO.File.Delete(fl);
						} 
						catch (Exception ex) 
						{
							Helper.ExceptionMessage(ex);
						}
					}
			}		
		}

		protected bool BlurNudityPreEP2 {
			get 
			{
				if (!System.IO.File.Exists(this.StartupCheatFile)) return true;

				try 
				{
					System.IO.TextReader fs = System.IO.File.OpenText(this.StartupCheatFile);
					string cont = fs.ReadToEnd();
					fs.Close();
					string[] lines = cont.Split("\n".ToCharArray());

					foreach (string line in lines) 
					{
						string pline = line.ToLower().Trim();
						while (pline.IndexOf("  ")!=-1) pline = pline.Replace("  ", " ");
						string[] tokens = pline.Split(" ".ToCharArray());

						if (tokens.Length==3) 
						{
							if ( (tokens[0]=="intprop") &&
								(tokens[1]=="censorgridsize") 
								) return (Convert.ToInt32(tokens[2])!=0);
						}
					}
				} 
				catch (Exception) {}

				return true;
			}

			set 
			{
				if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(this.StartupCheatFile))) return;

				try 
				{
					string newcont = "";
					bool found = false;
					if (System.IO.File.Exists(this.StartupCheatFile)) 
					{
						System.IO.TextReader fs = System.IO.File.OpenText(this.StartupCheatFile);
						string cont = fs.ReadToEnd();
						fs.Close();
						
						string[] lines = cont.Split("\n".ToCharArray());
						

						foreach (string line in lines) 
						{
							string pline = line.ToLower().Trim();
							while (pline.IndexOf("  ")!=-1) pline = pline.Replace("  ", " ");
							string[] tokens = pline.Split(" ".ToCharArray());

							if (tokens.Length==3) 
							{
								if ( (tokens[0]=="intprop") &&
									(tokens[1]=="censorgridsize") 
									) 
								{
									if (!found) 
									{
										if (!value) 
										{
											newcont += "intprop censorgridsize 0";
											newcont += Helper.lbr;
										}
										found = true;
									}
									continue;
								}
							}
							newcont += line.Trim();
							newcont += Helper.lbr;
						}

						System.IO.File.Delete(this.StartupCheatFile);
					}

					if (!found) 
					{
						if (!value) 
						{
							newcont += "intprop censorgridsize 0";
							newcont += Helper.lbr;
						}
					}

					System.IO.TextWriter fw = System.IO.File.CreateText(this.StartupCheatFile);
					fw.Write(newcont.Trim());
					fw.Close();
				} 
				catch (Exception) {}
			}
		}
		#endregion


		#region Getters
		public void SetDefaultPaths()
		{
			this.SimsPath = this.RealGamePath;
			this.SimsEP1Path = this.RealEP1GamePath;
			this.SimsEP2Path = this.RealEP2GamePath;
			this.SimsEP3Path = this.RealEP3GamePath;

			this.SimSavegameFolder = this.RealSavegamePath;
		}
		/// <summary>
		/// Returns the Real Instalation Folder
		/// </summary>
		public string RealEP1GamePath 
		{
			get 
			{
#if MAC
				return "";
#else
				if (this.EPInstalled>=1) 
				{
					try 
					{
						RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\EA Games\\The Sims 2 University");
						object o = rk.GetValue("Install Dir");
						if (o==null) return "";
						else return Helper.ToLongPathName(o.ToString());
					} 
					catch (Exception) 
					{
						return "";
					}
				}
				return "";
#endif
			}
		}

		/// <summary>
		/// Returns the Real Instalation Folder
		/// </summary>
		public string RealEP2GamePath 
		{
			get 
			{
#if MAC
				return "";
#else
				if (this.EPInstalled>=2) 
				{
					try 
					{
						RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\EA Games\\The Sims 2 Nightlife");
						object o = rk.GetValue("Install Dir");
						if (o==null) return "";
						else return Helper.ToLongPathName(o.ToString());
					} 
					catch (Exception) 
					{
						return "";
					}
				}
				return "";
#endif
			}
		}

		/// <summary>
		/// Returns the Real Instalation Folder
		/// </summary>
		public string RealEP3GamePath 
		{
			get 
			{
#if MAC
				return "";
#else
				if (this.EPInstalled>=3) 
				{
					try 
					{
						RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\EA Games\\The Sims 2 Open For Business");
						object o = rk.GetValue("Install Dir");
						if (o==null) return "";
						else return Helper.ToLongPathName(o.ToString());
					} 
					catch (Exception) 
					{
						return "";
					}
				}
				return "";
#endif
			}
		}

		/// <summary>
		/// Returns the highest number of installed EPs
		/// </summary>
		public int EPInstalled
		{
			get 
			{
				try 
				{			
#if MAC
					return 0;
#else
					RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\EA Games\\The Sims 2");
					if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\EA GAMES\The Sims 2 Open For Business", false)!=null) return 3;
					if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\EA GAMES\The Sims 2 Nightlife", false)!=null) return 2;
					if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\EA GAMES\The Sims 2 University", false)!=null) return 1;					
					return 0;
					/*object o = rk.GetValue("EPsInstalled");
					if (o==null) return 0;

					string name = o.ToString().ToLower();
					if (name.IndexOf("sims2ep2.exe")>=0) return 2;
					else return 1; //Sims2EP1.exe*/
#endif
				} 
				catch (Exception) 
				{
					return 2;
				}
			}
		}

		public string RealSavegamePath
		{
			get 
			{
				try 
				{
					string path = System.IO.Path.Combine(this.PersonalFolder, "EA Games");
					path = System.IO.Path.Combine(path, this.DisplayedName);
					return Helper.ToLongPathName(path);
				} 
				catch (Exception) 
				{
					return "";
				}
			}
		}

		/// <summary>
		/// Returns the Displayed Sims 2 name
		/// </summary>
		protected string DisplayedName 
		{
			get 
			{
				try 
				{
#if MAC
					return "The Sims 2";
#else
					RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\EA Games\\The Sims 2");
					object o = rk.GetValue("DisplayName");
					if (o==null) return "The Sims 2";
					else return o.ToString();
#endif
				} 
				catch (Exception) 
				{
					return "The Sims 2";
				}
			}
		}

		/// <summary>
		/// Returns the Location of the Personal Folder
		/// </summary>
		protected string PersonalFolder 
		{
			get 
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			}
		}

		/// <summary>
		/// Returns the Real Instalation Folder
		/// </summary>
		public string RealGamePath 
		{
			get 
			{
				try 
				{
#if MAC
					return "";
#else
					RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\EA Games\\The Sims 2");
					object o = rk.GetValue("Install Dir");
					if (o==null) return "";
					else return Helper.ToLongPathName(o.ToString());
#endif
				} 
				catch (Exception) 
				{
					return "";
				}
			}
		}

		/// <summary>
		/// The location of theNvidia Tool
		/// </summary>
		public string NvidiaDDSTool 
		{
			get 
			{
				return System.IO.Path.Combine(NvidiaDDSPath, "nvdxt.exe");
			}
		}

		/// <summary>
		/// Returns the Name of the Startup Cheat File
		/// </summary>
		public string StartupCheatFile 
		{
			get 
			{
				return System.IO.Path.Combine(this.SimSavegameFolder, "Config\\userStartup.cheat");
			}
		}
		/// <summary>
		/// returns the Fodler where the users Neighborhood is stored
		/// </summary>
		public string NeighborhoodFolder 
		{
			get 
			{
				try 
				{
					return System.IO.Path.Combine(this.SimSavegameFolder, "Neighborhoods");
				} 
				catch (Exception) 
				{
					return "";
				}
			}
		}

		/// <summary>
		/// returns the Fodler where the Backups are stored
		/// </summary>
		public string BackupFolder 
		{
			get 
			{
				try 
				{
					return System.IO.Path.Combine(System.IO.Path.Combine(this.PersonalFolder, "EA Games"), "SimPE Backup");
				} 
				catch (Exception) 
				{
					return "";
				}
			}
		}
		#endregion
	}
}