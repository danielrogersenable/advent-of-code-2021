using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Models.Day12
{
    public class EdgeModel
    {
        public string[] Vertices { get; set; }

        public string GetConnectedVertex(string vertexName)
        {
            return Vertices.Where(v => v != vertexName).Single();
        }
    }
}
