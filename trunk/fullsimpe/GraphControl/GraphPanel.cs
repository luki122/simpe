using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ambertation.Windows.Forms.Graph;

namespace Ambertation.Windows.Forms
{
	/// <summary>
	/// This is a Dragable Panel
	/// </summary>
	public class GraphPanel : System.Windows.Forms.Panel
	{
		/// <summary> 
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		Ambertation.Collections.GraphElements li;
		internal Ambertation.Collections.GraphElements LinkItems
		{
			get { return li;}
		}
		
		public GraphPanel()
		{
			// Dieser Aufruf ist f�r den Windows Form-Designer erforderlich.
			InitializeComponent();

			SetStyle(
				ControlStyles.SupportsTransparentBackColor |
				ControlStyles.AllPaintingInWmPaint |
				//ControlStyles.Opaque |
				ControlStyles.UserPaint |
				ControlStyles.ResizeRedraw 
				| ControlStyles.DoubleBuffer
				,true);
			li = new Ambertation.Collections.GraphElements();
			li.ItemsChanged += new EventHandler(li_ItemsChanged);
			this.BackColor = SystemColors.ControlLightLight;
			lm = LinkControlLineMode.Bezier;
			quality = true;
			savebound = true;
			minwd = 0;
			minhg = 0;
			lk = false;
			update = false;

			autosz = false;
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

				if (li!=null) 
				{
					while (li.Count>0) 
					{
						GraphPanelElement l = li[0];
						li.RemoveAt(0);
						l.Dispose();
					}
				}


			}
			base.Dispose( disposing );

		}

		#region Vom Komponenten-Designer generierter Code
		


		/// <summary> 
		/// Erforderliche Methode f�r die Designerunterst�tzung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		#region Properties
		public new Control Parent
		{
			get {return base.Parent;}
			set 
			{
				if (base.Parent!=value)
				{
					if (Parent!=null) Parent.SizeChanged -= new EventHandler(Parent_SizeChanged);
					base.Parent = value;
					if (Parent!=null)
					{
						MinWidth = Parent.ClientRectangle.Width;
						MinHeight = Parent.ClientRectangle.Height;
						Parent.SizeChanged += new EventHandler(Parent_SizeChanged);
					}
				}
			}
		}
		bool lk;
		public bool LockItems
		{
			get {return lk;}
			set { 
				if (lk!=value)
				{
					lk = value;
					SetLocked();
				}
			}
		}
		bool savebound;
		public virtual bool SaveBounds
		{
			get { return savebound; }
			set { savebound = value; }
		}

		bool autosz;
		public bool AutoSize
		{
			get {return autosz;}
			set 
			{
				autosz = value;
				this.li_ItemsChanged(li, null);
				if (autosz) Dock = DockStyle.None;
			}
		}
		Ambertation.Windows.Forms.Graph.LinkControlLineMode lm;
		public Ambertation.Windows.Forms.Graph.LinkControlLineMode LineMode
		{
			get {return lm; }
			set 
			{				
				lm = value;
				this.SetLinkLineMode();
			}
		}

		bool quality;
		public bool Quality
		{
			get { return quality; }
			set 
			{
				quality = value;
				this.SetLinkQuality();
			}
		}
		int minwd, minhg;
		public int MinWidth
		{
			get { return minwd; }
			set 
			{
				minwd = value;
				Width = Math.Max(Width, minwd);
			}
		}

		
		public int MinHeight
		{
			get { return minhg; }
			set 
			{
				minhg = value;
				Height = Math.Max(Height, minhg);
			}
		}

		[Browsable(false)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{				
			}
		}

		#endregion

		

		#region Event Override		
		protected override void OnPaintBackground(PaintEventArgs e)
		{					
			base.OnPaintBackground (e);											
		}

		
		protected override void OnPaint(PaintEventArgs e)
		{									
			if (update) return;
			base.OnPaint(e);
			GraphPanelElement.SetGraphicsMode(e.Graphics, true);
			foreach (GraphPanelElement c in li) c.OnPaint(e);	
		}		

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);
			bool hit = false;
			for (int i=li.Count-1; i>=0; i--){
				GraphPanelElement c = li[i]; 
			
				if (c is DragPanel) 
				{	
					if (!hit) 
						if (((DragPanel)c).OnMouseDown(e)) 
						{						
							hit = true;
							((DragPanel)c).SetFocus(true);
							continue;
						}
					
					((DragPanel)c).SetFocus(false);
					
				}
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove (e);
			for (int i=li.Count-1; i>=0; i--)
			{
				GraphPanelElement c = li[i]; 
			
				if (c is DragPanel)  
				{					
					if (((DragPanel)c).OnMouseMove(e)) break;
				}
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp (e);
			for (int i=li.Count-1; i>=0; i--)
			{
				GraphPanelElement c = li[i]; 
			
				if (c is DragPanel) 
				{					
					if (((DragPanel)c).OnMouseUp(e)) break;
				}
			}
		}

		protected override void AdjustFormScrollbars(bool displayScrollbars)
		{
			base.AdjustFormScrollbars (displayScrollbars);			
		}


		#endregion

		void SetLinkLineMode()
		{
			foreach (GraphPanelElement gpe in li) gpe.ChangedParent();
		}

		void SetLinkQuality()
		{
			foreach (GraphPanelElement gpe in li) gpe.ChangedParent();
		}

		void SetSaveBound()
		{
			foreach (GraphPanelElement gpe in li) gpe.SaveBounds = this.SaveBounds;
		}

		void SetLocked()
		{
			foreach (GraphPanelElement gpe in li) 
			{
				if (gpe is DragPanel)
					((DragPanel)gpe).Movable = !this.LockItems;
			}
		}

		private void li_ItemsChanged(object sender, EventArgs e)
		{
			if (!autosz) return;
			int mx = 0;
			int my = 0;
			foreach (GraphPanelElement gpe in li)
			{
				mx = Math.Max(mx, gpe.Right);
				my = Math.Max(my, gpe.Bottom);
			}

			this.Width = Math.Max(mx, MinWidth);
			this.Height = Math.Max(my, MinHeight);
		}

		private void Parent_SizeChanged(object sender, EventArgs e)
		{
			MinWidth = Parent.ClientRectangle.Width;
			MinHeight = Parent.ClientRectangle.Height;
		}

		bool update;
		public void BeginUpdate()
		{
			update = true;
		}

		public void EndUpdate()
		{
			update=false;
			Refresh();
		}

		public void Clear()
		{
			while (li.Count>0) 
			{
				GraphPanelElement l = li[0];
				li.RemoveAt(0);
				l.Clear();
				l.Parent = null;
			}
		
			this.Refresh();
		}
	}
}
