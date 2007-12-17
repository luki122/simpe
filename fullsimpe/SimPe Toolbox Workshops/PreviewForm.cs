using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SimPe.Updates;

namespace SimPe.Plugin
{
	/// <summary>
	/// Zusammenfassung f�r PreviewForm.
	/// </summary>
	public class PreviewForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		Ambertation.Graphics.DirectXPanel dx = null;

		public PreviewForm()
		{
			//
			// Erforderlich f�r die Windows Form-Designerunterst�tzung
			//
			InitializeComponent();

			dx.Settings.AddAxis = false;
			dx.LoadSettings(Helper.SimPeViewportFile);
			dx.ResetDevice += new EventHandler(dx_ResetDevice);
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode f�r die Designerunterst�tzung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PreviewForm));
			this.dx = new Ambertation.Graphics.DirectXPanel();
			this.SuspendLayout();
			// 
			// dx
			// 
			this.dx.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			this.dx.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dx.Effect = null;
			this.dx.Location = new System.Drawing.Point(0, 0);
			this.dx.Name = "dx";
			this.dx.Size = new System.Drawing.Size(494, 476);
			this.dx.TabIndex = 0;
			this.dx.WorldMatrix = ((Microsoft.DirectX.Matrix)(resources.GetObject("dx.WorldMatrix")));
			// 
			// PreviewForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			this.ClientSize = new System.Drawing.Size(494, 476);
			this.Controls.Add(this.dx);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PreviewForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Recolor Preview";
			this.ResumeLayout(false);

		}
		#endregion

		static void Exception()
		{
			throw new SimPe.Warning("This Item can't be previewed!", "SimPE was unable to build the Scenegraph.");
		}

		public static Ambertation.Scenes.Scene BuildScene(MmatWrapper mmat, SimPe.Interfaces.Files.IPackageFile package)
		{
			SimPe.Interfaces.Scenegraph.IScenegraphFileIndex fii;
			Ambertation.Scenes.Scene scn = BuildScene(out fii, mmat, package);
			fii.Clear();
			return scn;
		}		

		public static Ambertation.Scenes.Scene BuildScene(out SimPe.Interfaces.Scenegraph.IScenegraphFileIndex fii, MmatWrapper mmat, SimPe.Interfaces.Files.IPackageFile package)
		{
			SimPe.Interfaces.Files.IPackageFile npkg;			
			Ambertation.Scenes.Scene scn = BuildScene(out fii, mmat, package, out npkg);

			if (npkg!=null) 
			{
				npkg.Close();
				if (npkg is SimPe.Packages.GeneratableFile)
					((SimPe.Packages.GeneratableFile)npkg).Dispose();
			}
			npkg = null;

			return scn;
		}

		public static Ambertation.Scenes.Scene BuildScene(SimPe.Interfaces.Scenegraph.IScenegraphFileIndex fii, MmatWrapper mmat, SimPe.Interfaces.Files.IPackageFile package)
		{
			SimPe.Interfaces.Files.IPackageFile npkg;
			Ambertation.Scenes.Scene scn = BuildScene(fii, mmat, package, out npkg);

			if (npkg!=null) 
			{
				npkg.Close();
				if (npkg is SimPe.Packages.GeneratableFile)
					((SimPe.Packages.GeneratableFile)npkg).Dispose();
			}
			npkg = null;

			return scn;
		}
		
		public static Ambertation.Scenes.Scene BuildScene(out SimPe.Interfaces.Scenegraph.IScenegraphFileIndex fii, MmatWrapper mmat, SimPe.Interfaces.Files.IPackageFile package, out SimPe.Interfaces.Files.IPackageFile npkg)
		{
			npkg = null;
			Wait.Start();
			fii = FileTable.FileIndex.AddNewChild();			
			try 
			{				
				return BuildScene(fii, mmat, package, out npkg);											
			}
			catch (System.IO.FileNotFoundException)
			{
				Wait.Stop();
				if (MessageBox.Show("The Microsoft Managed DirectX Extensions were not found on your System. Without them, the Preview is not available.\n\nYou can install them manually, by extracting the content of the DirectX\\ManagedDX.CAB on your Sims 2 Installation CD #1. If you double click on the extracted msi File, all needed Files will be installed.\n\nYou can also let SimPE install it automatically. SimPE will download the needed Files (3.5MB) from the SimPE Homepage and install them. Do you want SimPE to download and install the Files?", "Warning", MessageBoxButtons.YesNo)==DialogResult.Yes)
				{
					if (WebUpdate.InstallMDX()) MessageBox.Show("Managed DirectX Extension were installed succesfully!");
				}
					
				return null;
			}
			
			finally 
			{				
				FileTable.FileIndex.RemoveChild(fii);				
				Wait.Stop();
            }
			
			//return null;
		}

		public static Ambertation.Scenes.Scene BuildScene(SimPe.Interfaces.Scenegraph.IScenegraphFileIndex fii, MmatWrapper mmat, SimPe.Interfaces.Files.IPackageFile package, out SimPe.Interfaces.Files.IPackageFile npkg)
		{
			npkg = null;
            try
            {
                FileTable.FileIndex.Load();
                if (System.IO.File.Exists(package.SaveFileName))
                    fii.AddIndexFromFolder(System.IO.Path.GetDirectoryName(package.SaveFileName));

                npkg = SimPe.Plugin.Tool.Dockable.ObjectWorkshopHelper.CreatCloneByCres(mmat.ModelName);
                try
                {
                    foreach (SimPe.Interfaces.Files.IPackedFileDescriptor pfd in package.Index)
                    {
                        SimPe.Interfaces.Files.IPackedFileDescriptor npfd = pfd.Clone();
                        npfd.UserData = package.Read(pfd).UncompressedData;
                        if (pfd == mmat.FileDescriptor)
                            mmat.ProcessData(npfd, npkg);

                        npkg.Add(npfd, true);
                    }

                    fii.AddIndexFromPackage(npkg, true);
                    //fii.WriteContentToConsole();

                    return RenderScene(mmat);
                }
                finally
                {

                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Wait.Stop();
                if (MessageBox.Show("The Microsoft Managed DirectX Extensions were not found on your System. Without them, the Preview is not available.\n\nYou can install them manually, by extracting the content of the DirectX\\ManagedDX.CAB on your Sims 2 Installation CD #1. If you double click on the extracted msi File, all needed Files will be installed.\n\nYou can also let SimPE install it automatically. SimPE will download the needed Files (3.5MB) from the SimPE Homepage and install them. Do you want SimPE to download and install the Files?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (WebUpdate.InstallMDX()) MessageBox.Show("Managed DirectX Extension were installed succesfully!");
                }

                return null;
            }


            //return null;
		}

		public static Ambertation.Scenes.Scene RenderScene(MmatWrapper mmat)
		{			
			try 
			{
				
				try 
				{
			
					GenericRcol rcol = mmat.GMDC;
					if (rcol!=null)
					{
						GeometryDataContainerExt gmdcext = new GeometryDataContainerExt(rcol.Blocks[0] as GeometryDataContainer);	
						gmdcext.UserTxmtMap[mmat.SubsetName] = mmat.TXMT;
						gmdcext.UserTxtrMap[mmat.SubsetName] = mmat.TXTR;
						Ambertation.Scenes.Scene scene = gmdcext.GetScene(new SimPe.Plugin.Gmdc.ElementOrder(Gmdc.ElementSorting.Preview));

						return scene;
					}
					else Exception();

				} 
				finally 
				{
					
				}
			}
			catch (System.IO.FileNotFoundException)
			{
				Wait.Stop();
				if (MessageBox.Show("The Microsoft Managed DirectX Extensions were not found on your System. Without them, the Preview is not available.\n\nYou can install them manually, by extracting the content of the DirectX\\ManagedDX.CAB on your Sims 2 Installation CD #1. If you double click on the extracted msi File, all needed Files will be installed.\n\nYou can also let SimPE install it automatically. SimPE will download the needed Files (3.5MB) from the SimPE Homepage and install them. Do you want SimPE to download and install the Files?", "Warning", MessageBoxButtons.YesNo)==DialogResult.Yes)
				{
					if (WebUpdate.InstallMDX()) MessageBox.Show("Managed DirectX Extension were installed succesfully!");
				}
					
				return null;
			}
			

			return null;
		}

		Ambertation.Scenes.Scene scene;
		//static Ambertation.Panel3D p3d;
		public static void Execute(SimPe.PackedFiles.Wrapper.Cpf cmmat, SimPe.Interfaces.Files.IPackageFile package) 
		{
			if (!(cmmat is MmatWrapper)) return;

			MmatWrapper mmat = cmmat as MmatWrapper;
					
			try 
			{				
				PreviewForm f = new PreviewForm();
				SimPe.Interfaces.Scenegraph.IScenegraphFileIndex fii;
				f.scene = BuildScene(out fii, mmat, package);
				fii.Clear();
				if (f.scene == null) return;
				f.dx.Reset();
				f.dx.ResetDefaultViewport();
				f.ShowDialog();
				f.dx.Meshes.Clear(true);								
			}
			catch (System.IO.FileNotFoundException)
			{
				Wait.Stop();
				if (MessageBox.Show("The Microsoft Managed DirectX Extensions were not found on your System. Without them, the Preview is not available.\n\nYou can install them manually, by extracting the content of the DirectX\\ManagedDX.CAB on your Sims 2 Installation CD #1. If you double click on the extracted msi File, all needed Files will be installed.\n\nYou can also let SimPE install it automatically. SimPE will download the needed Files (3.5MB) from the SimPE Homepage and install them. Do you want SimPE to download and install the Files?", "Warning", MessageBoxButtons.YesNo)==DialogResult.Yes)
				{
					if (WebUpdate.InstallMDX()) MessageBox.Show("Managed DirectX Extension were installed succesfully!");
				}
					
				return;
			}
			catch (Exception ex)
			{
				Wait.Stop();	
				Helper.ExceptionMessage(ex);
			}
			finally 
			{							
			}
				
		}

		private void dx_ResetDevice(object sender, EventArgs e)
		{
			dx.Meshes.Clear();
			dx.AddScene(this.scene);
		}
	}
}
