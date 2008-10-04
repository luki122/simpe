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
using SimPe.Interfaces.Plugin;

namespace SimPe
{
	/// <summary>
	/// This class handles the Comandline Arguments of SimPE
	/// </summary>
	public class Commandline
	{
		#region Import Data
		public static bool ConvertData()
		{
			string layoutname = LayoutRegistry.LayoutFile;
			if (!System.IO.File.Exists(layoutname))
                ForceModernLayout();

            if (!System.IO.File.Exists(Helper.LayoutFileName))
                ForceModernLayout();


            if (Helper.WindowsRegistry.PreviousEpCount < 3) 
				Helper.WindowsRegistry.BlurNudityUpdate();

            #region folders.xreg -- removed
            /*if (Helper.WindowsRegistry.PreviousVersion < 279174552515) 
			{
				string name = System.IO.Path.Combine(Helper.SimPeDataPath, "folders.xreg");
				if (System.IO.File.Exists(name)) 
				{
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
			}*/
            #endregion

            #region simpelanguagecache -- removed
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
            #endregion

            if (Helper.WindowsRegistry.FoundUnknownEP())
            {
                if (Message.Show(SimPe.Localization.GetString("Unknown EP found").Replace("{name}", SimPe.PathProvider.Global.GetExpansion(SimPe.PathProvider.Global.LastKnown).Name), SimPe.Localization.GetString("Warning"), System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    return false;
            }

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
			//check if the settings File is available
			string file = System.IO.Path.Combine(Helper.SimPeDataPath, @"simpe.xreg");
			try 
			{
				CheckXML(file);
			}
			catch
			{
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

        internal static ICommandLine[] preSplashCommands = new ICommandLine[] {
            new Splash(),
            new NoSplash(),
            new EnableFlags(),
            new MakeClassic(),
            new MakeModern(),
        };

        public static bool PreSplash(List<string> argv)
        {
            foreach (ICommandLine cmd in preSplashCommands)
                if (cmd.Parse(argv)) return true;
            return false;
        }


        class Splash : ICommandLine
        {
            #region ICommandLine Members
            public bool Parse(List<string> argv) { if (argv.Remove("--splash")) Helper.WindowsRegistry.ShowStartupSplash = true; return false; }
            public string[] Help() { return new string[] { "--splash", null }; }
            #endregion
        }

        class NoSplash : ICommandLine
        {
            #region ICommandLine Members
            public bool Parse(List<string> argv) { if (argv.Remove("--nosplash")) Helper.WindowsRegistry.ShowStartupSplash = false; return false; }
            public string[] Help() { return new string[] { "--nosplash", null }; }
            #endregion
        }

        class EnableFlags : ICommandLine
        {
            #region ICommandLine Members

            public bool Parse(List<string> argv)
            {
                if (argv.Count == 0) return false;

                if (argv[0].ToLower() == "-localmode") // backward compatibility; uses ".Insert" as there may be trailing unknown stuff
                {
                    argv.RemoveAt(0);
                    argv.InsertRange(0, new string[] { "-enable", "localmode" });
                    if (argv.Count > 2 && argv[2].ToLower() == "-noplugins")
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
                    if (argv[0].ToLower() == "noerrors") { Helper.NoErrors = true; argv.RemoveAt(0); continue; }
                    break; // hit an unrecognised "enable" option
                }
                if (Helper.LocalMode || Helper.NoPlugins || Helper.NoErrors)
                {
                    string s = "";
                    if (Helper.LocalMode) s += Localization.GetString("InLocalMode") + "\r\n";
                    if (Helper.NoPlugins) s += "\r\n" + Localization.GetString("NoPlugins") + "\r\n";
                    if (Helper.NoErrors) s += "\r\n" + Localization.GetString("NoErrors");
                    Message.Show(s, "Notice", System.Windows.Forms.MessageBoxButtons.OK);
                }

                return false; // Don't exit SimPE!
            }

            public string[] Help() { return new string[] { "-enable localmode  -enable noplugins  -enable fileformat  -enable noerrors", null }; }

            #endregion
        }

        #region Theme Presets
		public static void ForceModernLayout()
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

        class MakeClassic : ICommandLine
        {
            #region ICommandLine Members
            public bool Parse(List<string> argv)
            {
                if (!argv.Remove("-classicpreset")) return false;

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

                return !(Message.Show(SimPe.Localization.GetString("PresetChanged").Replace("{name}", SimPe.Localization.GetString("PresetClassic")),
                    SimPe.Localization.GetString("Information"), System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes);
            }
            public string[] Help() { return new string[] { "-classicpreset", null }; }
            #endregion
        }

        class MakeModern : ICommandLine
        {
            #region ICommandLine Members
            public bool Parse(List<string> argv)
            {
                if (!argv.Remove("-modernpreset")) return false;

                ForceModernLayout();

                return (Message.Show(SimPe.Localization.GetString("PresetChanged").Replace("{name}",
                    SimPe.Localization.GetString("PresetModern")), SimPe.Localization.GetString("Information"),
                    System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes);
            }
            public string[] Help() { return new string[] { "-modernpreset", null }; }
            #endregion
        }


        /// <summary>
        /// Loaded just befor the GUI is started
        /// </summary>
        /// <param name="args"></param>
        /// <returns>true if the GUI should <b>NOT</b> show up</returns>
        public static bool FullEnvStart(List<string> argv)
        {
            if (argv.Count < 1) return false;

            try
            {
                SimPe.Splash.Screen.SetMessage(SimPe.Localization.GetString("Checking commandline parameters"));
                foreach (ICommandLine cmdline in SimPe.FileTable.CommandLineRegistry.CommandLines)
                    if (cmdline.Parse(argv)) return true;
                return false;
            }
            finally
            {
                SimPe.Splash.Screen.SetMessage(SimPe.Localization.GetString("Checked commandline parameters"));
            }
        }

    }

    public class CommandlineHelp : ICommandLine
    {
        #region ICommandLine Members
        public bool Parse(List<string> argv)
        {
            if (!argv.Remove("-help")) return false;

            string pluginHelp = "";
            foreach (ICommandLine cmdline in Commandline.preSplashCommands)
            {
                string[] help = cmdline.Help();
                pluginHelp += "\r\n" + "  " + help[0];
                if (help[1] != null && help[1].Length > 0)
                    pluginHelp += "\r\n" + "      " + help[1];
            }
            foreach (ICommandLine cmdline in SimPe.FileTable.CommandLineRegistry.CommandLines)
            {
                string[] help = cmdline.Help();
                pluginHelp += "\r\n" + "  " + help[0];
                if (help[1] != null && help[1].Length > 0)
                    pluginHelp += "\r\n" + "      " + help[1];
            }

            SimPe.Splash.Screen.Stop();

            System.Windows.Forms.MessageBox.Show(""
                    + pluginHelp
                    + "\r\n"
                    , "SimPE Commandline Parameters"
                    , System.Windows.Forms.MessageBoxButtons.OK
                    , System.Windows.Forms.MessageBoxIcon.Information
                );

            return true;
        }
        public string[] Help() { return new string[] { "-help", null }; }
        #endregion
    }

    public class CommandlineHelpFactory : AbstractWrapperFactory, ICommandLineFactory
    {
        #region ICommandLineFactory Members

        public ICommandLine[] KnownCommandLines
        {
            get { return new ICommandLine[] { new CommandlineHelp(), }; }
        }

        #endregion
    }
}
