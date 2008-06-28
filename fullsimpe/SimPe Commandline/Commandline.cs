using System;
using System.Collections.Generic;
using SimPe.Plugin;
using System.Drawing;
using SimPe.Interfaces.Scenegraph;

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
		public static bool Start(string[] args)
		{
            SimPe.Splash.Screen.SetMessage(SimPe.Localization.GetString("Checking commandline parameters"));
			if (args.Length<1) return false;

            
            if (Help(args)) return true;
            if (MakeClassic(args)) return true;
            if (MakeModern(args)) return true;
			if (BuildPackage(args)) return true;
			if (BuildTxtr(args)) return true;
			if (FixPackage(args)) return true;
            if (EnableFlags(args)) return true;
			return false;
		}

        /// <summary>
        /// Loaded just befor the GUI is started
        /// </summary>
        /// <param name="args"></param>
        /// <returns>true if the GUI should <b>NOT</b> show up</returns>
        public static bool FullEnvStart(string[] args)
        {
            if (args.Length < 1) return false;
            if (GenerateSemiGlobals(args)) return true;
            return false;
        }

		static bool Help(string[] args) 
		{
			if (args[0]!="-help") return false;

			Console.WriteLine("SimPE Commandline Parameters:");
			Console.WriteLine("-----------------------------");
			Console.WriteLine();
			Console.WriteLine("  -classicpreset");
			Console.WriteLine("  -modernpreset");
			Console.WriteLine("  -build -desc [packag.xml] -out [output].package");
			Console.WriteLine("  -txtr -image [imgfile] -out [output].package -name [textureNam] -format [dxt1|dxt3|dxt5|raw8|raw24|raw32] -levels [nr] -width [max. Width] -height [max. Height]");
            Console.WriteLine("  -gensemiglob");
            Console.WriteLine("  -enable localmode");
            Console.WriteLine("  -enable noplugins");
            Console.WriteLine("  -enable fileformat");
            return true;
        }

        #region Enable flags
        static bool EnableFlags(string[] args)
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
            return false; // Don't exit SimPE!
        }
        #endregion

        #region Theme Presets
        static bool MakeClassic(string[] args) 
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
			if (Message.Show(SimPe.Localization.GetString("PresetChanged").Replace("{name}", SimPe.Localization.GetString("PresetClassic")), SimPe.Localization.GetString("Information"), System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
				return false;
			return true;
		}

		public static void ForceModernLayout()
		{
			Overridelayout("modern_layout.xreg");
		}

		static bool MakeModern(string[] args) 
		{
			if (args!=null)
				if (args[0]!="-modernpreset") return false;			
			
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
			if (Message.Show(SimPe.Localization.GetString("PresetChanged").Replace("{name}", SimPe.Localization.GetString("PresetModern")), SimPe.Localization.GetString("Information"), System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
				return false;
			return true;
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

		static bool FixPackage(string[] args) 
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
		static bool BuildPackage(string[] args) 
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

		#region Build TXTR
		/// <summary>
		/// Assemble a Picture File
		/// </summary>
		/// <param name="data"></param>
		public static void LoadTXTR(ImageData id, string flname, System.Drawing.Size sz, int levels, SimPe.Plugin.ImageLoader.TxtrFormats format) 
		{
			try 
			{
				System.Drawing.Image src = Image.FromFile(flname);
				LoadTXTR(id, src, sz, levels, format);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			}
		}

		/// <summary>
		/// Assemble a Picture File
		/// </summary>
		/// <param name="data"></param>
		public static void LoadTXTR(ImageData id, System.Drawing.Image src, System.Drawing.Size sz, int levels, SimPe.Plugin.ImageLoader.TxtrFormats format) 
		{
			try 
			{					
				id.TextureSize = sz;
				id.Format = format;
				id.MipMapLevels = (uint)levels;

				System.Drawing.Image img = new Bitmap(sz.Width, sz.Height);
				
				Graphics gr = Graphics.FromImage(img);

				gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				gr.DrawImage(src, new Rectangle(new Point(0,0), img.Size), new Rectangle(new Point(0,0), src.Size), GraphicsUnit.Pixel);
						

				MipMap[] maps = new MipMap[levels];
				int wd = 1;
				int hg = 1;

				//build default Sizes
				for (int i=0; i<levels; i++)
				{
					MipMap mm = new MipMap(id);
					mm.Texture = new Bitmap(wd, hg);

					if ((wd==hg) && (wd==1))
					{
						if (id.TextureSize.Width>id.TextureSize.Height) 
						{
							wd = id.TextureSize.Width/id.TextureSize.Height;
							hg = 1;
						}
						else 
						{
							hg = id.TextureSize.Height/id.TextureSize.Width;
							wd = 1;
						}
							

						if ((wd==hg) && (wd==1)) 
						{
							wd *= 2; hg *= 2;
						}
					} 
					else 
					{
						wd *= 2; hg *= 2;
					}

					maps[i] = mm;
				}

				//create a Scaled Version for each testure
				for (int i=0; i< maps.Length; i++) 
				{
					MipMap mm  = maps[i];
					if (img!=null)
					{
						Image bm = mm.Texture;
						gr = Graphics.FromImage(bm);

						gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
						gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
						gr.DrawImage(img, new Rectangle(new Point(0,0), bm.Size), new Rectangle(new Point(0,0), img.Size), GraphicsUnit.Pixel);

						
						id.TextureSize = new Size(bm.Width, bm.Height);
					}
				} // for i

				MipMapBlock[] mmps = new MipMapBlock[1];
				mmps[0] = new MipMapBlock(id);
				mmps[0].MipMaps = maps;

				id.MipMapBlocks = mmps;
			} 
			catch (Exception ex) 
			{
				Console.WriteLine("Exception: "+ex.Message);
			}
		}
		

		/// <summary>
		/// Assemble a Picture File
		/// </summary>
		/// <param name="data"></param>
		public static void LoadDDS(ImageData id, DDSData[] data) 
		{
			if (data==null) return;
			if (data.Length>0) 
			{
				try 
				{					
					id.TextureSize = data[0].ParentSize;
					id.Format = data[0].Format;
					id.MipMapLevels = (uint)data.Length;

					MipMap[] maps = new MipMap[data.Length];
					int ct=0;
					for (int i=data.Length-1; i>=0; i--)
					{
						DDSData item = data[i];
						MipMap mm = new MipMap(id);
						mm.Texture = item.Texture;
						mm.Data = item.Data;

						maps[ct++] = mm;
					}
	
					MipMapBlock[] mmps = new MipMapBlock[1];
					mmps[0] = new MipMapBlock(id);
					mmps[0].MipMaps = maps;

					id.MipMapBlocks = mmps;
				}
				catch (Exception ex) 
				{
					Console.WriteLine("Exception: "+ex.Message);
				}						
			}

		}

		/// <summary>
		/// Build a Texture
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static bool BuildTxtr(string[] args)
		{
			if (args[0]!="-txtr") return false;

			//get Parameters
			string filename = "";
			string output = "";
			string texturename = "";
			int levels = 9;
			System.Drawing.Size sz = new System.Drawing.Size(512, 512);
			SimPe.Plugin.ImageLoader.TxtrFormats format = SimPe.Plugin.ImageLoader.TxtrFormats.DXT1Format;

			#region Parse Arguments
			for (int i=0; i<args.Length; i++) 
			{
				if (args[i]=="-image") 
				{
					if (args.Length>i+1) 
					{
						filename = args[++i];
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

				if (args[i]=="-name") 
				{
					if (args.Length>i+1) 
					{
						texturename = args[++i];
						continue;
					}
				}

				if (args[i]=="-levels") 
				{
					if (args.Length>i+1) 
					{
						levels = Convert.ToInt32(args[++i]);
						continue;
					}
				}

				if (args[i]=="-width") 
				{
					if (args.Length>i+1) 
					{
						sz.Width = Convert.ToInt32(args[++i]);
						continue;
					}
				}

				if (args[i]=="-height") 
				{
					if (args.Length>i+1) 
					{
						sz.Height = Convert.ToInt32(args[++i]);
						continue;
					}
				}

				if (args[i]=="-format") 
				{
					if (args.Length>i+1) 
					{
						switch (args[++i]) 
						{
							case "dxt1": 
							{
								format = SimPe.Plugin.ImageLoader.TxtrFormats.DXT1Format;
								break;
							}
							case "dxt3": 
							{
								format = SimPe.Plugin.ImageLoader.TxtrFormats.DXT3Format;
								break;
							}
							case "dxt5": 
							{
								format = SimPe.Plugin.ImageLoader.TxtrFormats.DXT5Format;
								break;
							}
							case "raw24": 
							{
								format = SimPe.Plugin.ImageLoader.TxtrFormats.Raw24Bit;
								break;
							}
							case "raw32": 
							{
								format = SimPe.Plugin.ImageLoader.TxtrFormats.Raw32Bit;
								break;
							}
							case "raw8": 
							{
								format = SimPe.Plugin.ImageLoader.TxtrFormats.Raw8Bit;
								break;
							}
						}
						continue;
					}
				}
			}
			#endregion

			//check if the File exists
			if (!System.IO.File.Exists(filename)) 
			{
				System.Console.WriteLine(filename+" was not found.");
				return true;
			}
			if (output.Trim()=="") 
			{
				System.Console.WriteLine("Please specify a output File using -out.");
				return true;
			}

			//build TXTR File
			ImageData id = new SimPe.Plugin.ImageData(null);

            if ((System.IO.File.Exists(PathProvider.Global.NvidiaDDSTool)) && ((format == ImageLoader.TxtrFormats.DXT1Format) || (format == ImageLoader.TxtrFormats.DXT3Format) || (format == ImageLoader.TxtrFormats.DXT5Format)))
			{
				LoadDDS(id, DDSTool.BuildDDS(filename, levels, format, "-sharpenMethod Smoothen"));
			} 
			else 
			{
				LoadTXTR(id, filename, sz, levels, format);
			}

			Rcol rcol = new GenericRcol(null, false);
			rcol.FileName = texturename;
			rcol.FileDescriptor = new SimPe.Packages.PackedFileDescriptor();
			rcol.Blocks = new IRcolBlock[1];
			rcol.Blocks[0] = id;

			rcol.SynchronizeUserData();
            System.IO.FileStream fs = System.IO.File.Create(output);
			System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
			bw.Write(rcol.FileDescriptor.UserData);
            bw.Close();
            bw = null;
            fs.Close();
            fs.Dispose();
            fs = null;

			return true;
		}
		#endregion

		#region Import Data
		public static bool ConvertData()
		{
			string layoutname = LayoutRegistry.LayoutFile;
			if (!System.IO.File.Exists(layoutname)) 
				Commandline.MakeModern(null);


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

        #region Semiglobals
        static bool GenerateSemiGlobals(string[] args)
        {
            if (args[0] != "-gensemiglob") return false;

            System.Collections.Generic.List<uint> added = new System.Collections.Generic.List<uint>();
            Console.WriteLine("Loading FileTable...");
            SimPe.FileTable.FileIndex.Load();
            Console.WriteLine("Looking for GLOB Resources...");
            SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem[] resources = SimPe.FileTable.FileIndex.FindFile(SimPe.Data.MetaData.GLOB_FILE, true);

            Console.WriteLine("Found " + resources.Length + " GLOB Resources");
            string fl = Helper.SimPeSemiGlobalFile;
            Console.WriteLine("Opening "+fl);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fl, false);
            sw.WriteLine("<semiglobals>");

            int ct = 0;
            int unq = 0;
            foreach (SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem item in resources)
            {
                if (ct % 23 == 0) Console.WriteLine("Wrote " + ct + " ("+unq+" unique) entries");
                ct++;

                SimPe.Plugin.Glob glb = new SimPe.Plugin.Glob();
                glb.ProcessData(item);

                if (!added.Contains(glb.SemiGlobalGroup))
                {
                    sw.WriteLine("  <semiglobal>");
                    sw.WriteLine("    <known />");
                    sw.WriteLine("    <group>" + Helper.HexString(glb.SemiGlobalGroup) + "</group>");
                    sw.WriteLine("    <name>" + glb.SemiGlobalName + "</name>");
                    sw.WriteLine("  </semiglobal>");
                    added.Add(glb.SemiGlobalGroup);
                    unq++;
                }
            }
            Console.WriteLine("Wrote " + ct + " (" + unq + " unique) entries");
            sw.WriteLine("</semiglobals>");
            Console.WriteLine("Finished writing to "+fl);
            sw.Close();
            sw.Dispose();
            sw = null;
            Console.WriteLine("Closed File");
            Console.WriteLine("DONE!");

            return true;
        }
        #endregion
    }
}
