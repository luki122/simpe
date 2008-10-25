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
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace SimPe
{
    public class WaitingScreen
    {
        /// <summary>
		/// Display a new WaitingScreen image
		/// </summary>
		/// <param name="image">the Image to show</param>
        public static void UpdateImage(System.Drawing.Image image) { Screen.doUpdate(image); }
        /// <summary>
        /// Display a new WaitingScreen image and message
        /// </summary>
        /// <param name="both">the MessageAndImage to show</param>
        public static void Update(System.Drawing.Image image, string msg) { Screen.doUpdate(image, msg); }
        /// <summary>
        /// Display a new WaitingScreen message
        /// </summary>
        /// <param name="msg">The Message to show</param>
        public static void UpdateMessage(string msg) { Screen.doUpdate(msg); }
		/// <summary>
        /// Show the WaitingScreen
		/// </summary>
        public static void Wait() { Screen.doWait(); }
        /// <summary>
        /// Stop the WaitingScreen and focus the given Form
        /// </summary>
        /// <param name="form">The form to focus</param>
        public static void Stop(Form form) { Screen.doStop(); form.Activate(); }
		/// <summary>
        /// Stop the WaitingScreen
		/// </summary>
        public static void Stop() { Screen.doStop(); }
		/// <summary>
		/// True if the WaitingScreen is displayed
		/// </summary>
        public static bool Running { get { return scr != null; } }
        /// <summary>
        /// Returns the Size of the Dispalyed Image
        /// </summary>
        public static System.Drawing.Size ImageSize { get { return new System.Drawing.Size(64, 64); } }


        /// <summary>
        /// Event1: StartThread has created frm and sent SetMessage
        /// </summary>
        static System.Threading.ManualResetEvent ev1 = new System.Threading.ManualResetEvent(false);
        static WaitingScreen scr;
        static object lockObj = new object();
        static object lockObj2 = new object();
        static WaitingScreen Screen
        {
            get
            {
                lock (lockObj2)
                {
                    if (scr == null)
                    {
                        ev1.Reset();
                        scr = new WaitingScreen();
                        ev1.WaitOne();
                    }
                }
                return scr;
            }
        }



        System.Drawing.Image prevImage = null;
        string prevMessage = "";
        SimPe.WaitingForm frm;
        System.Threading.Thread t = null;

        void doUpdate(System.Drawing.Image image) { lock (lockObj) { prevImage = image; } if (frm != null) frm.SetImage(image); Application.DoEvents(); }
        void doUpdate(string msg) { lock (lockObj) { prevMessage = msg; } if (frm != null) frm.SetMessage(msg); Application.DoEvents(); }
        void doUpdate(System.Drawing.Image image, string msg) { doUpdate(image); doUpdate(msg); }
        void doWait() { if (frm != null) frm.StartSplash(); }
        void doStop() { if (frm != null) frm.StopSplash(); }



        private WaitingScreen()
        {
            System.Diagnostics.Debug.WriteLine("SimPe.WaitingScreen.WaitingScreen()");
            if (Helper.WindowsRegistry.WaitingScreen)
            {
                t = new System.Threading.Thread(new System.Threading.ThreadStart(StartThread));
                t.Start();
            }
            else
                ev1.Set();
        }

        protected void StartThread()
        {
            System.Diagnostics.Debug.WriteLine("SimPe.WaitingScreen.StartThread()");
            frm = new SimPe.WaitingForm();
            System.Diagnostics.Debug.WriteLine("SimPe.WaitingScreen.StartThread() - created new SimPe.WaitingForm()");
            lock (lockObj)
            {
                prevImage = frm.Image;
                prevMessage = frm.Message;
            }
            System.Diagnostics.Debug.WriteLine("SimPe.WaitingScreen.StartThread() - set frm.Image and frm.Message");

            frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
            doUpdate(prevImage, prevMessage);
            System.Diagnostics.Debug.WriteLine("SimPe.WaitingScreen.StartThread() - about to raise ev1");
            ev1.Set();
            System.Diagnostics.Debug.WriteLine("SimPe.WaitingScreen.StartThread() - ev1 raised");
            frm.StartSplash();
            System.Diagnostics.Debug.WriteLine("SimPe.WaitingScreen.StartThread() - returned from frm.StartSplash()");
        }

        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("SimPe.WaitingScreen.frm_FormClosed(...)");
            lock (lockObj)
            {
                //if (t != null) t.Join();
                t = null;
                frm = null;
                scr = null;
            }
        }
    }
}
