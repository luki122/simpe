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
using System.Collections.Generic;
using System.Text;

namespace SimPe
{
    public class Splash
    {
        static Splash scr;
        public static Splash Screen
        {
            get
            {
                if (scr == null) scr = new Splash();
                return scr;
            }
        }

        SimPe.Windows.Forms.SplashForm frm;
        //bool running;
        bool show;
        private Splash()
        {
            mmsg = "";
            //running = true;
            show = false;

            if (Helper.WindowsRegistry.ShowStartupSplash)
            {
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(StartThread));
                t.Start();
            }
        }

        protected void StartThread()
        {
            System.Threading.Thread.Sleep(300);
            frm = new SimPe.Windows.Forms.SplashForm();
            if (show) Start();
            SetMessage(mmsg);
        }

        string mmsg;
        public void SetMessage(string msg)
        {
            mmsg = msg;
            if (frm!=null)frm.Message = msg;
        }

        public void Start()
        {
            show = true;
            if (frm!=null) frm.StartSplash();
        }

        public void Stop()
        {
            show = false;
            if (frm!=null) frm.StopSplash();
        }

        public void ShutDown()
        {
            Stop();
            //running = false;
        }
    }
}
