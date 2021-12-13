using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Solutions.Models.Day12;

namespace AdventOfCode.Solutions.Services
{
    public class Day12 : BaseDayService
    {
        public override long SolvePart1(bool useSample)
        {
            var input = ParseInputToString(useSample)
                .Select(i => i.Split("-").ToArray());

            var vertices = input
                .SelectMany(i => i)
                .Distinct()
                .Select(i => new VertexModel
                {
                    Name = i,
                    CanRevisit = i.ToUpper() == i
                })
                .ToList();

            var edges = input
                .Select(i => new EdgeModel
                {
                    Vertices = i
                })
                .ToList();

            var graph = new Graph
            {
                Vertices = vertices,
                Edges = edges
            };

            var incompletePaths = new List<List<VertexModel>>();
            var completePaths = new List<List<VertexModel>>();

            var startVertex = vertices.Where(v => v.Name == "start").Single();
            var endVertex = vertices.Where(v => v.Name == "end").Single();

            incompletePaths.Add(new List<VertexModel>() { startVertex });

            while (incompletePaths.Any())
            {
                var incompletePath = incompletePaths.First();
                incompletePaths.Remove(incompletePath);

                var currentVertex = incompletePath.Last();

                var validAttachedVertices = edges
                    .Where(e => e.Vertices.Contains(currentVertex.Name))
                    .Select(e => e.GetConnectedVertex(currentVertex.Name))
                    .Select(v => graph.GetVertex(v))
                    .Where(v => v.CanRevisit || !incompletePath.Contains(v))
                    .ToList();

                foreach (var vertex in validAttachedVertices)
                {
                    var newPath = incompletePath.ToList();
                    newPath.Add(vertex);

                    if (vertex == endVertex)
                    {
                        completePaths.Add(newPath);
                    }
                    else
                    {
                        incompletePaths.Add(newPath);
                    }
                }
            }

            return completePaths.Count();
        }

        public override long SolvePart2(bool useSample)
        {
            var input = ParseInputToString(useSample)
                .Select(i => i.Split("-").ToArray());

            var vertices = input
                .SelectMany(i => i)
                .Distinct()
                .Select(i => new VertexModel
                {
                    Name = i,
                    CanRevisit = i.ToUpper() == i
                })
                .ToList();

            var edges = input
                .Select(i => new EdgeModel
                {
                    Vertices = i
                })
                .ToList();

            var graph = new Graph
            {
                Vertices = vertices,
                Edges = edges
            };

            var incompletePaths = new List<PathModel>();
            var completePaths = new List<PathModel>();

            var startVertex = vertices.Where(v => v.Name == "start").Single();
            var endVertex = vertices.Where(v => v.Name == "end").Single();

            incompletePaths.Add(new PathModel { Vertices = new List<VertexModel>() { startVertex } });

            while (incompletePaths.Any())
            {
                var incompletePath = incompletePaths.First();
                incompletePaths.Remove(incompletePath);

                var currentVertex = incompletePath.Vertices.Last();

                var validAttachedVertices = edges
                    .Where(e => e.Vertices.Contains(currentVertex.Name))
                    .Select(e => e.GetConnectedVertex(currentVertex.Name))
                    .Select(v => graph.GetVertex(v))
                    .Where(v => v.CanRevisit || !incompletePath.Vertices.Contains(v) || !incompletePath.HasRevisitedASmallCaveTwice)
                    .Where(v => v.Name != "start")
                    .ToList();

                foreach (var vertex in validAttachedVertices)
                {
                    var newPath = new PathModel(incompletePath);
                    if (newPath.Vertices.Contains(vertex) && !vertex.CanRevisit)
                    {
                        newPath.HasRevisitedASmallCaveTwice = true;
                    }

                    newPath.Vertices.Add(vertex);

                    if (vertex == endVertex)
                    {
                        completePaths.Add(newPath);
                    }
                    else
                    {
                        incompletePaths.Add(newPath);
                    }
                }
            }

            return completePaths.Count();
        }
    }
}
