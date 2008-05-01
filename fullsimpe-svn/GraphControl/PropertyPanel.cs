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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using Ambertation.Collections;

namespace Ambertation.Windows.Forms.Graph
{
	/// <summary>
	/// This is a Rounded Panel
	/// </summary>
	public abstract class PropertyPanel : RoundedPanel
	{
		
		public PropertyPanel() : this(new PropertyItems()) 
		{
			
		}

		public PropertyPanel(PropertyItems properties) :base ()
		{
			this.properties = properties;	
			txt = "";
		}

		

		#region public Properties
		PropertyItems properties;		
		public PropertyItems Properties 
		{
			get { return properties;}
			set 
			{
				if (value != properties)
				{
					properties = value;
					Invalidate();
				}
			}
		}

		Image thumb;
		public Image Thumbnail
		{
			get { return thumb; }
			set 
			{
				thumb = value;
				Invalidate();
			}
		}

		string txt;
		public string Text
		{
			get { return txt; }
			set 
			{
				txt = value;
				Invalidate();
			}
		}

		#endregion

		#region Properties
		
		#endregion


		

		#region Event Override
		#endregion

		#region Basic Draw Methods
		

		

		protected override void DrawText(Graphics gr)
		{
			if (this.properties==null) return;
			LinkGraphic.SetGraphicsMode(gr, !Quality);

			Pen linepen = new Pen(Color.FromArgb(90, Color.Black));
			gr.DrawLine(linepen, new Point(0, 20), new Point(Width, 20));
			linepen.Dispose();

			StringFormat sf = new StringFormat();
			sf.FormatFlags = StringFormatFlags.NoWrap;
			Font ftb = new Font(Font.FontFamily, Font.Size, FontStyle.Bold, Font.Unit);
			gr.DrawString(Text, ftb, new Pen(this.ForeColor).Brush, new RectangleF(new PointF(4, 4), new SizeF(Width-8, Height-8)), sf);
			ftb.Dispose();	
			int top = 24;
			Size indent = new Size(0,0);
			if (thumb!=null) 
			{
				gr.DrawImageUnscaled(thumb, 4, top, thumb.Width, thumb.Height);
				indent = new Size(thumb.Width+4, top+thumb.Height+4);
			}
				
			//Hashtable ht = new Hashtable();
			foreach (string k in properties.Keys) 
			{
				PropertyItem o = properties[k];
				if (o==null) continue;				
				string val = "";				
				val = (string)o.Value;			
					
				if (val!=null) 
				{
					int indentx = 0;
					if (top<indent.Height) indentx = indent.Width;
					Font ft = new Font(Font.FontFamily, Font.Size, FontStyle.Italic, Font.Unit);
					

					gr.DrawString(
						k+":", 
						ft, 
						new Pen(Color.FromArgb(160, this.ForeColor)).Brush, 
						new RectangleF(new PointF(indentx+10, top), new SizeF(Width-(24+indentx), top+16)), 
						sf);
					SizeF sz = gr.MeasureString(
						k+":", 
						ft);

					gr.DrawString(
						val, 
						Font, 
						new Pen(Color.FromArgb(140, this.ForeColor)).Brush, 
						new RectangleF(new PointF(indentx+12+sz.Width, top), new SizeF(Width-(24+sz.Width+indentx), top+16)), 
						sf);
					SizeF sz2 = gr.MeasureString(
						val, 
						Font);
						
					Rectangle rect = new Rectangle(new Point((int)(indentx+12+sz.Width), top), new Size((int)(Width-(24+sz.Width+indentx)), top+16));					

					top += (int)Math.Max(sz.Height, sz2.Height);
					ft.Dispose();
					
				}
			}

			//LinkGraphic.SetGraphicsMode(gr, true);
			//properties = ht;
		}
		
		#endregion
	}
}
