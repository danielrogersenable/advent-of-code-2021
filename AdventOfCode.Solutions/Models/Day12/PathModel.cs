using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Models.Day12
{
    public class PathModel
    {
        public PathModel()
        {
        }

        public PathModel (PathModel oldPathModel)
        {
            Vertices = oldPathModel.Vertices.ToList();
            HasRevisitedASmallCaveTwice = oldPathModel.HasRevisitedASmallCaveTwice;
        }

        public List<VertexModel> Vertices { get; set; }
        public bool HasRevisitedASmallCaveTwice { get; set; }
    }
}
