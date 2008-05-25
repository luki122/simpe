using System;
using System.Collections;
using System.Xml;
using SimPe.Interfaces.Wrapper;

namespace SimPe
{	
	/// <summary>
	/// Do not use this class direct, use <see cref="SimPe.FileTable"/> instead!
	/// </summary>
	public class FileTableBase
	{
		static Interfaces.Scenegraph.IScenegraphFileIndex fileindex;

		/// <summary>
		/// Returns the FileIndex
		/// </summary>
		/// <remarks>This will be initialized by the RCOL Factory</remarks>
		public static Interfaces.Scenegraph.IScenegraphFileIndex FileIndex
		{
			get { return fileindex; }
			set { fileindex = value; }
		}

		/// <summary>
		/// Returns the Filename for the Folder.xml File
		/// </summary>
		public static string FolderFile
		{
			get { return System.IO.Path.Combine(Helper.SimPeDataPath, "folders.xreg"); }
		}		

		/// <summary>
		/// Returns a List of all Folders the User want's to scan for Content
		/// </summary>
		public static ArrayList DefaultFolders
		{
			get 
			{
				if (!System.IO.File.Exists(FolderFile)) BuildFolderXml();

				try 
				{
                    System.Collections.Generic.Dictionary<string, ExpansionItem> shortmap = new System.Collections.Generic.Dictionary<string, ExpansionItem>();
                    foreach (ExpansionItem ei in PathProvider.Global.Expansions)
                        shortmap[ei.ShortId.ToLower()] = ei;

					ArrayList folders = new ArrayList();

					//read XML File
					System.Xml.XmlDocument xmlfile = new XmlDocument();
					xmlfile.Load(FolderFile);

					//seek Root Node
					XmlNodeList XMLData = xmlfile.GetElementsByTagName("folders");					
#if MAC
					Console.WriteLine("Reading Folders from \""+FolderFile+"\".");
#endif
					//Process all Root Node Entries
					for (int i=0; i<XMLData.Count; i++)
					{
						XmlNode node = XMLData.Item(i);	
						foreach (XmlNode subnode in node) 
						{
							if (subnode.Name == "filetable")
							{
								foreach (XmlNode foldernode in subnode.ChildNodes) 
								{
									if (foldernode.Name != "path" && foldernode.Name != "file") continue;									
									string name = foldernode.InnerText.Trim();		
									int ftiver = -1;
									bool ftiignore = false;
									bool ftirec = false;
                                    FileTableItemType ftitype = FileTablePaths.Absolute;									
									
									
									
									#region add Path Root if needed
                                    foreach (XmlAttribute a in foldernode.Attributes)
                                    {
                                        if (a.Name == "recursive")
                                        {
                                            if (a.Value != "0") ftirec = true;
                                        }

                                        if (a.Name == "ignore")
                                        {
                                            ftiignore = (a.Value != "0");
                                        }

                                        if (a.Name == "version" || a.Name == "epversion")
                                        {
                                            try
                                            {
                                                int ver = Convert.ToInt32(a.Value);
                                                ftiver = ver;
                                            }
                                            catch { }
                                        }

                                        if (a.Name == "root")
                                        {
                                            string root = a.Value.ToLower();

                                            if (shortmap.ContainsKey(root))
                                            {
                                                ExpansionItem ei = shortmap[root];
                                                ftitype = ei.Expansion;
                                                root = ei.InstallFolder;
                                            }
                                            else if (root == "save")
                                            {
                                                root = PathProvider.SimSavegameFolder;
                                                ftitype = FileTablePaths.SaveGameFolder;
                                            }
                                            else if (root == "simpe")
                                            {
                                                root = Helper.SimPePath;
                                                ftitype = FileTablePaths.SimPEFolder;
                                            }
                                            else if (root == "simpedata")
                                            {
                                                root = Helper.SimPeDataPath;
                                                ftitype = FileTablePaths.SimPEDataFolder;
                                            }
                                            else if (root == "simpeplugin")
                                            {
                                                root = Helper.SimPePluginPath;
                                                ftitype = FileTablePaths.SimPEPluginFolder;
                                            }                                                                                       
                                        }
                                    } //foreach


									FileTableItem fti;
									if (foldernode.Name == "file") fti = new FileTableItem(name, ftitype, false, true, ftiver, ftiignore);
									else  fti = new FileTableItem(name, ftitype, ftirec, false, ftiver, ftiignore);

#if MAC
									Console.WriteLine("    -> "+fti.Name);
#endif
									#endregion

									folders.Add(fti);
							
								} //foreach foldernode
							}
						} //foreach subnode
					}	 //for i
	
					return folders;
				}
				catch (Exception ex) 
				{
					Helper.ExceptionMessage("", ex);
					return new ArrayList();
				}
			}
		}

		/// <summary>
		/// Creates a default Folder xml
		/// </summary>
		public static void BuildFolderXml()
		{
			try 
			{
				System.IO.TextWriter tw = System.IO.File.CreateText(FolderFile);

				try 
				{
					tw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
					tw.WriteLine("<folders>");
					tw.WriteLine("  <filetable>");
					tw.WriteLine("    <file root=\"save\">Downloads"+Helper.PATH_SEP+"_EnableColorOptionsGMND.package</file>");
					tw.WriteLine("    <file root=\"game\">TSData"+Helper.PATH_SEP+"Res"+Helper.PATH_SEP+"Sims3D"+Helper.PATH_SEP+"_EnableColorOptionsMMAT.package</file>");


                    for (int i=PathProvider.Global.Expansions.Count-1; i>=0; i--)
                    {
                        ExpansionItem ei = PathProvider.Global.Expansions[i];
                        string s = ei.ShortId.ToLower();
                        string ign = "";
                        if ((ei.Group & 1) != 1) ign = " ignore=\"1\" ";
                        foreach (string folder in ei.PreObjectFileTableFolders)
                        {
                            tw.WriteLine("    <path " + ign + " root=\"" + s + "\">"+folder+"</path>");
                        }

                        if (ei.Flag.SimStory || !ei.Flag.FullObjectsPackage) tw.WriteLine("    <path " + ign + " root=\"" + s + "\">" + ei.ObjectsSubFolder.Replace("\\\\", "\\") + "</path>");
                        else tw.WriteLine("    <path " + ign + " root=\"" + s + "\" version=\"" + ei.Version + "\">"+ei.ObjectsSubFolder.Replace("\\\\", "\\")+"</path>");
                        
                        foreach (string folder in ei.FileTableFolders)
                        {
                            tw.WriteLine("    <path " + ign + " root=\"" + s + "\">" + folder + "</path>");
                        }
                    }
             
					tw.WriteLine("  </filetable>");
					tw.WriteLine("</folders>");

				} 
				finally 
				{
					tw.Close();
				}
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("Unable to create default Folder File!", ex);
			}
		}

		static SimPe.Interfaces.IWrapperRegistry wreg;
		/// <summary>
		/// Returns/Sets a WrapperRegistry (can be null)
		/// </summary>
		public static SimPe.Interfaces.IWrapperRegistry WrapperRegistry
		{
			get { return wreg; }
			set { wreg = value;}
		}		

		static SimPe.Interfaces.IProviderRegistry preg;
		/// <summary>
		/// Returns/Sets a ProviderRegistry (can be null)
		/// </summary>
		public static SimPe.Interfaces.IProviderRegistry ProviderRegistry
		{
			get { return preg; }
			set { preg = value;}
		}
		
		static IGroupCache gc;
		/// <summary>
		/// Returns The Group Cache used to determin local Groups
		/// </summary>
		public static IGroupCache GroupCache 
		{
			get { return gc; }
			set { gc = value;}
		}		
	}
}