﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GisTest.Models
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}