﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB03
{
    internal interface IHazardNotifier
    {
        void SendHazardNotification(string message);
    }
}
