using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Models.Day12
{
    public class Graph
    {
        public List<VertexModel> Vertices { get; set; }
        public List<EdgeModel> Edges { get; set; }

        public VertexModel GetVertex(string vertextName)
        {
            return Vertices.Where(v => v.Name == vertextName).Single();
        }
    }
}
