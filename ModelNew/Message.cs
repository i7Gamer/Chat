﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Message
    {
        private string content { get; set; }
        private DateTime sent { get; set; }
        private User from { get; set; }
        private Group toGroup { get; set; }
        private User toPerson { get; set; }
    }
}
