/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *   Copyright (C) 2008 by Peter L Jones                                   *
 *   peter@users.sf.net                                                    *
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
using System.Collections.Generic;
using SimPe.Plugin;
using SimPe.Interfaces;

namespace SimPe
{
	/// <summary>
	/// This class handles the Comandline Arguments of SimPE
	/// </summary>
	public class Commandline
	{
		/// <summary>
		/// Tries to process the CommandLine
		/// </summary>
		/// <param name="args">Commandline Arguments</param>
		/// <returns>true if simpe should stop now</returns>
		public static bool Start(ref string[] args)
		{
            SimPe.Splash.Screen.SetMessage(SimPe.Localization.GetString("Checking commandline parameters"));
			if (args.Length<1) return false;

            if (MakeClassic(ref args)) return true;
            if (MakeModern(ref args)) return true;
			if (BuildPackage(ref args)) return true;
			if (FixPackage(ref args)) return true;
            if (EnableFlags(ref args)) return true;
			return false;
		}

        public static bool Splash(ref string[] args, string opt)
        {
            List<string> argv = new List<string>(args);
            bool res = argv.Contains(opt);
            if (res) argv.RemoveAt(argv.IndexOf(opt));
            args = argv.ToArray();
            return res;
        }

        /// <summary>
        /// Loaded just befor the GUI is started
        /// </summary>
        /// <param name="args"></param>
        /// <returns>true if the GUI should <b>NOT</b> show up</returns>
        public static bool FullEnvStart(ref string[] args)
        {
            if (args.Length < 1) return false;

            try
            {
                SimPe.Splash.Screen.SetMessage(SimPe.Localization.GetString("Checking commandline parameters"));

                // Help() will display plugin command line tools
                if (Help(ref args)) return true;

                ITool[] tools = SimPe.FileTable.ToolRegistry.Tools;
                foreach (ITool tool in tools)
                {
                    ICommandLine cmd = tool as ICommandLine;
                    if (cmd != null && cmd.Parse(ref args)) return true;
                }
                return false;
            }
            finally
            {
                SimPe.Splash.Screen.SetMessage(SimPe.Localization.GetString(""));
            }
        }

        static bool Help(ref string[] args)
        {
            if (args[0] != "-help") return false;

            string pluginHelp = "";
            ITool[] tools = SimPe.FileTable.ToolRegistry.Tools;
            foreach (ITool tool in tools)
            {
                ICommandLine cmd = tool as ICommandLine;
                if (cmd != null)
                {
                    string[] help = cmd.Help();
                    pluginHelp += "\r\n" + "  " + help[0];
                    if (help[1] != null && help[1].Length > 0)
                        pluginHelp += "\r\n" + "      " + help[1];
                }
            }

            SimPe.Splash.Screen.Stop();

            System.Windows.Forms.MessageBox.Show(""
                    + "\r\n" + "  --splash"
                    + "\r\n" + "  --nosplash"
                    + "\r\n" + "  -classicpreset"
                    + "\r\n" + "  -modernpreset"
                    + "\r\n" + "  -build -desc [packag.xml] -out [output].package"
                    + "\r\n" + "  -enable localmode"
                    + "\r\n" + "  -enable noplugins"
                    + "\r\n" + "  -enable fileformat"
                    + pluginHelp
                    + "\r\n"
                    , "SimPE Commandline Parameters"
                    , System.Windows.Forms.MessageBoxButtons.OK
                    , System.Windows.Forms.MessageBoxIcon.Information
                );

            return true;
        }

        #region Enable flags
        static bool EnableFlags(ref string[] args)
        {
            List<string> argv = new List<string>(args);

            if (argv[0].ToLower() == "-localmode") // backward compatibility; uses ".Insert" as there may be trailing unknown stuff
            {
                argv.RemoveAt(0);
                argv.InsertRange(0, new string[] { "-enable", "localmode" });
                if (args.Length > 1 && args[1].ToLower() == "-noplugins")
                {
                    argv.RemoveAt(2);
                    argv.Insert(2, "noplugins");
                }
            }

            if (argv[0].ToLower() != "-enable") return false;

            while (argv.Count > 0)
            {
                if (argv[0].ToLower() == "-enable") { argv.RemoveAt(0); continue; } // allow interspersed "-enables"
                if (argv[0].ToLower() == "localmode") { Helper.LocalMode = true; argv.RemoveAt(0); continue; }
                if (argv[0].ToLower() == "noplugins") { Helper.NoPlugins = true; argv.RemoveAt(0); continue; }
                if (argv[0].ToLower() == "fileformat") { Helper.FileFormat = true; argv.RemoveAt(0); continue; }
                break; // hit an unrecognised "enable" option
            }
            if (Helper.LocalMode || Helper.NoPlugins)
            {
                SimPe.Splash.Screen.Stop();
                string s = "";
                if (Helper.LocalMode) s += Localization.GetString("InLocalMode") + "\r\n";
                if (Helper.NoPlugins) s += "\r\n" + Localization.GetString("NoPlugins");
                Message.Show(s, "Notice", System.Windows.Forms.MessageBoxButtons.OK);
                SimPe.Splash.Screen.Start();
            }

            args = argv.ToArray();
            return false; // Don't exit SimPE!
        }
        #endregion

        #region Theme Presets
        static bool MakeClassic(ref string[] args) 
		{
			if (args[0]!="-classicpreset") return false;
			
			Overridelayout("classic_layout.xreg");

			Helper.WindowsRegistry.Layout.SelectedTheme = 0;
			Helper.WindowsRegistry.AsynchronLoad = false;
			Helper.WindowsRegistry.DecodeFilenamesState = false;
			Helper.WindowsRegistry.DeepSimScan = false;
			Helper.WindowsRegistry.DeepSimTemplateScan = false;

			Helper.WindowsRegistry.SimpleResourceSelect = true;
			Helper.WindowsRegistry.MultipleFiles = false;
			Helper.WindowsRegistry.FirefoxTabbing = false;
            Helper.WindowsRegistry.ShowWaitBarPermanent = false;

			Helper.WindowsRegistry.LockDocks = true;
            Helper.WindowsRegistry.Flush();


            SimPe.Splash.Screen.Stop();

            List<string> argv = new List<string>(args);
            argv.RemoveAt(0);
            args = argv.ToArray();
            if (Message.Show(SimPe.Localization.GetString("PresetChanged").Replace("{name}", SimPe.Localization.GetString("PresetClassic")), SimPe.Localization.GetString("Information"), System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
				return false;
			return true;
		}

		public static void ForceModernLayout()
		{
			Overridelayout("modern_layout.xreg");
		}

		static bool MakeModern(ref string[] args) 
		{
			if (args!=null)
				if (args[0]!="-modernpreset") return false;

            List<string> argv = new List<string>(args);
            argv.RemoveAt(0);
            args = argv.ToArray();

            return !MakeModern();
        }

        static bool MakeModern()
        {
			Overridelayout("modern_layout.xreg");

			Helper.WindowsRegistry.Layout.SelectedTheme = 3;
			Helper.WindowsRegistry.AsynchronLoad = false;
			Helper.WindowsRegistry.DecodeFilenamesState = true;
			Helper.WindowsRegistry.DeepSimScan = true;
			Helper.WindowsRegistry.DeepSimTemplateScan = false;

			Helper.WindowsRegistry.SimpleResourceSelect = true;
			Helper.WindowsRegistry.MultipleFiles = true;
			Helper.WindowsRegistry.FirefoxTabbing = true;

            Helper.WindowsRegistry.LockDocks = false;
            Helper.WindowsRegistry.ShowWaitBarPermanent = true;
            Helper.WindowsRegistry.Flush();

            SimPe.Splash.Screen.Stop();

            return (Message.Show(SimPe.Localization.GetString("PresetChanged").Replace("{name}",
                SimPe.Localization.GetString("PresetModern")), SimPe.Localization.GetString("Information"),
                System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes);
        }

		static void Overridelayout(string name)
		{
            
			System.IO.Stream s = typeof(Commandline).Assembly.GetManifestResourceStream("SimPe."+name);
			if (s!=null) 
			{
				try 
				{
                    System.IO.StreamWriter sw = System.IO.File.CreateText(LayoutRegistry.LayoutFile);
                    sw.BaseStream.SetLength(0);
					try 
					{
						System.IO.StreamReader sr = new System.IO.StreamReader(s);
						sw.Write(sr.ReadToEnd());
						sw.Flush();
					} 
					finally 
					{
						sw.Close();
						sw.Dispose();
						sw = null;
					}
				} 
				catch (Exception ex) 
				{
					Helper.ExceptionMessage(ex);
				}
			}

            string name2 = name.Replace("_layout.xreg", ".layout");
            s = typeof(Commandline).Assembly.GetManifestResourceStream("SimPe." + name2);
            if (s != null)
            {
                try
                {
                    System.IO.FileStream fs = System.IO.File.OpenWrite(Helper.LayoutFileName);
                    System.IO.BinaryWriter sw = new System.IO.BinaryWriter(fs);
                    sw.BaseStream.SetLength(0);
                    try
                    {
                        System.IO.BinaryReader sr = new System.IO.BinaryReader(s);                        
                        sw.Write(sr.ReadBytes((int)sr.BaseStream.Length));
                        sw.Flush();
                    }
                    finally
                    {
                        sw.Close();
                        sw = null;
                        fs.Close();
                        fs.Dispose();
                        fs = null;
                        s.Close();
                        s.Dispose();
                        s = null;
                    }

                    Helper.WindowsRegistry.ReloadLayout();
                }
                catch (Exception ex)
                {
                    Helper.ExceptionMessage(ex);
                }
            }	
		}
		#endregion

		#region Fix
		public static void FixPackage(string flname, string modelname, FixVersion ver)
		{
			if (System.IO.File.Exists(flname))
			{
				SimPe.Packages.GeneratableFile pkg = SimPe.Packages.GeneratableFile.LoadFromFile(flname);

				System.Collections.Hashtable map = RenameForm.GetNames((modelname.Trim()!=""), pkg, null, modelname);
				FixObject fo = new FixObject(pkg, ver, false);
				fo.Fix(map, false);
				fo.CleanUp();
				fo.FixGroup();

				pkg.Save();
			}
		}

		static bool FixPackage(ref string[] args) 
		{
			if (args[0]!="-fix") return false;

			string modelname = "";
			string prefix = "";
			string package = "";
			FixVersion ver = FixVersion.UniversityReady;

			#region Parse Arguments
			for (int i=0; i<args.Length; i++) 
			{
				if (args[i]=="-package") 
				{
					if (args.Length>i+1) 
					{
						package = args[++i];
						continue;
					}
				}

				if (args[i]=="-modelname") 
				{
					if (args.Length>i+1) 
					{
						modelname = args[++i];
						continue;
					}
				}

				if (args[i]=="-prefix") 
				{
					if (args.Length>i+1) 
					{
						prefix = args[++i];
						continue;
					}
				}

				if (args[i]=="-fixversion") 
				{
					if (args.Length>i+1) 
					{
						if (args[++i].Trim().ToLower()=="uni1") ver = FixVersion.UniversityReady;
						else if (args[++i].Trim().ToLower()=="uni2") ver = FixVersion.UniversityReady2;
						continue;
					}
				}
			}
			#endregion

			FixPackage(package, prefix+modelname,  ver);
			return true;
		}
		#endregion

		#region Build Package
		static bool BuildPackage(ref string[] args) 
		{
			if (args[0]!="-build") return false;

			string output = "";
			string input = "";

			#region Parse Arguments
			for (int i=0; i<args.Length; i++) 
			{
				if (args[i]=="-desc") 
				{
					if (args.Length>i+1) 
					{
						input = args[++i];
						continue;
					}
				}
				if (args[i]=="-out") 
				{
					if (args.Length>i+1) 
					{
						output = args[++i];
						continue;
					}
				}
			}
			#endregion

			if (!System.IO.File.Exists(input)) 
			{
				Console.WriteLine(input + " existiert nicht.");
				return true;
			}

			SimPe.Packages.GeneratableFile pkg = SimPe.Packages.GeneratableFile.LoadFromStream(XmlPackageReader.OpenExtractedPackage(null, input));
			pkg.Save(output);

			return true;
		}
		#endregion

		#region Import Data
		public static bool ConvertData()
		{
			string layoutname = LayoutRegistry.LayoutFile;
			if (!System.IO.File.Exists(layoutname)) 
				Commandline.MakeModern();


            if (Helper.WindowsRegistry.PreviousEpCount < 3) 
				Helper.WindowsRegistry.BlurNudityUpdate();

            if (Helper.WindowsRegistry.PreviousVersion < 279174552515) 
			{
				string name = System.IO.Path.Combine(Helper.SimPeDataPath, "folders.xreg");
				if (System.IO.File.Exists(name)) 
				{
                    SimPe.Splash.Screen.Stop();
					if (Message.Show(SimPe.Localization.GetString("Reset Filetable").Replace("{flname}", name), "Update", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
					{
						try 
						{						
							System.IO.File.Delete(name);							
						} 
						catch (Exception ex)
						{
							Helper.ExceptionMessage(ex);
						}
					}
				}
			}

			/*if (Helper.WindowsRegistry.PreviousVersion<236370882908) 
			{
				string name = Helper.SimPeLanguageCache;
				if (System.IO.File.Exists(name)) 
				{
					if (Message.Show(SimPe.Localization.GetString("Reset Cache").Replace("{flname}", name), "Update", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
					{
						try 
						{						
							System.IO.File.Delete(name);
							
						} 
						catch (Exception ex)
						{
							Helper.ExceptionMessage(ex);
						}
					}
				}
			}*/

            if (Helper.WindowsRegistry.FoundUnknownEP())
            {
                SimPe.Splash.Screen.Stop();
                if (Message.Show(SimPe.Localization.GetString("Unknown EP found").Replace("{name}", SimPe.PathProvider.Global.GetExpansion(SimPe.PathProvider.Global.LastKnown).Name), SimPe.Localization.GetString("Warning"), System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    return false;
            }

            if (!System.IO.File.Exists(Helper.LayoutFileName)) ForceModernLayout();

			return true;
		}

		static void CheckXML(string file)
		{
			System.Xml.XmlDocument xmlfile = new System.Xml.XmlDocument();
						
			if (System.IO.File.Exists(file)) 
			{
				xmlfile.Load(file);
				System.Xml.XmlNodeList XMLData = xmlfile.GetElementsByTagName("registry");	
			}
		}

		public static void CheckFiles()
		{
            SimPe.Splash.Screen.SetMessage(SimPe.Localization.GetString("Validating SimPE registry"));
			//check if the settings File is available
			string file = System.IO.Path.Combine(Helper.SimPeDataPath, @"simpe.xreg");
			try 
			{
				CheckXML(file);
			}
			catch
			{
                SimPe.Splash.Screen.Stop();
				if (System.Windows.Forms.MessageBox.Show("The Settings File was not readable. SimPE will generate a new one, which means that all your Settings made in \"Extra->Preferences\" get lost.\n\nShould SimPe reset the Settings File?", "Error", System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.Yes)
					System.IO.File.Delete(file);
			}

			//check if the layout File is available
			file = System.IO.Path.Combine(Helper.SimPeDataPath, @"layout.xreg");
			try 
			{
				CheckXML(file);
			}
			catch
			{
                SimPe.Splash.Screen.Stop();
				if (System.Windows.Forms.MessageBox.Show("The Layout File was not readable. SimPE will generate a new one, which means that your Window Layout will be reset to the Default.\n\nShould SimPe reset the Settings File?", "Error", System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.Yes)
					System.IO.File.Delete(file);
			}
		}

		public static bool ImportOldData()
		{
            try
            {
                if (!System.IO.Directory.Exists(Helper.SimPeDataPath))
                    System.IO.Directory.CreateDirectory(Helper.SimPeDataPath);

                if (!System.IO.File.Exists(System.IO.Path.Combine(Helper.SimPeDataPath, "simpe.xreg")))
                {
                    if (System.IO.Directory.Exists(Helper.WindowsRegistry.PreviousDataFolder))
                        if (Helper.WindowsRegistry.PreviousDataFolder.Trim().ToLower() != Helper.SimPeDataPath.Trim().ToLower())
                            if (Helper.SimPeVersionLong > Helper.WindowsRegistry.PreviousVersion && Helper.WindowsRegistry.PreviousVersion > 0)
                            {
                                SimPe.Splash.Screen.Stop();
                                if (Message.Show("Should SimPE import old Settings from \"" + Helper.WindowsRegistry.PreviousDataFolder + "\"?", "Import Settings", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    WaitingScreen.Wait();
                                    try
                                    {
                                        int ct = 0;
                                        string[] files = System.IO.Directory.GetFiles(Helper.WindowsRegistry.PreviousDataFolder, "*.*");
                                        foreach (string file in files)
                                        {
                                            string name = System.IO.Path.GetFileName(file).Trim().ToLower();
                                            if (name == "tgi.xml") continue;

                                            string newfile = file.Trim().ToLower().Replace(Helper.WindowsRegistry.PreviousDataFolder.Trim().ToLower(), Helper.SimPeDataPath.Trim());
                                            WaitingScreen.UpdateMessage((ct++).ToString() + " / " + files.Length);
                                            System.IO.File.Copy(file, newfile, true);
                                        }

                                        Helper.WindowsRegistry.Reload();
                                        ThemeManager.Global.CurrentTheme = (SimPe.GuiTheme)Helper.WindowsRegistry.Layout.SelectedTheme;
                                    }
                                    catch (Exception ex)
                                    {
                                        Helper.ExceptionMessage(new Warning("Unable to import Settings.", ex.Message, ex));
                                    }
                                    finally
                                    {
                                        WaitingScreen.Stop();
                                    }
                                }
                            }
                }

                return ConvertData();
            }
            finally
            {

            }

            //return true;
        }
		#endregion

    }
}
