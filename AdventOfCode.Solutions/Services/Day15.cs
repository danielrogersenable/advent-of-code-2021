using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Solutions.Models.Day15;
using AdventOfCode.Solutions.Models.Generic;

namespace AdventOfCode.Solutions.Services
{
    public class Day15 : BaseDayService
    {
        public override long SolvePart1(bool useSample)
        {
            var input = ParseInputToString(useSample)
                .Select(i => i.ToCharArray())
                .ToList();

            var graph = new List<GraphVertex>();

            for (var y = 0; y < input.Count(); y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    graph.Add(new GraphVertex
                    {
                        X = x,
                        Y = y,
                        Risk = int.Parse(input[y][x].ToString()),
                        ProposedMinimumDistance = null,
                        Confirmed = false
                    });
                }
            }

            var maxXIndex = graph.Max(g => g.X);
            var maxYIndex = graph.Max(g => g.Y);

            graph.Where(g => g.X == 0 && g.Y == 0).Single().ProposedMinimumDistance = 0;

            var hasFoundEnd = false;

            while (!hasFoundEnd)
            {
                var currentVertex = graph
                    .Where(g => g.ProposedMinimumDistance.HasValue && !g.Confirmed)
                    .OrderBy(g => g.ProposedMinimumDistance.Value)
                    .First();

                graph
                    .Where(g => Math.Abs(g.X - currentVertex.X) + Math.Abs(g.Y - currentVertex.Y) == 1)
                    .Where(g => !g.Confirmed)
                    .ToList()
                    .ForEach(g =>
                    {
                        var potentialValue = currentVertex.ProposedMinimumDistance.Value + g.Risk;
                        if (!g.ProposedMinimumDistance.HasValue)
                        {
                            g.ProposedMinimumDistance = potentialValue;
                        }
                        else
                        {
                            g.ProposedMinimumDistance = Math.Min(potentialValue, g.ProposedMinimumDistance.Value);
                        }
                    });

                currentVertex.Confirmed = true;

                if (currentVertex.X == maxXIndex && currentVertex.Y == maxYIndex)
                {
                    hasFoundEnd = true;
                }
            }

            var endVertex = graph.Where(g => g.X == maxXIndex && g.Y == maxYIndex).Single();

            return endVertex.ProposedMinimumDistance.Value;
        }

        public override long SolvePart2(bool useSample)
        {
            var input = ParseInputToString(useSample)
                .Select(i => i.ToCharArray())
                .ToList();

            var widthAndDepth = input.Count();

            var graph = new GraphVertex[widthAndDepth * 5, widthAndDepth * 5];

            for (var y = 0; y < widthAndDepth; y++)
            {
                for (var x = 0; x < widthAndDepth; x++)
                {
                    var risk = int.Parse(input[y][x].ToString());

                    for (var xTile = 0; xTile < 5; xTile++)
                    {
                        for (var yTile = 0; yTile < 5; yTile++)
                        {
                            graph[x + xTile * widthAndDepth, y + yTile * widthAndDepth] = new GraphVertex
                            {
                                X = x + xTile * widthAndDepth,
                                Y = y + yTile * widthAndDepth,
                                Risk = ((risk + xTile + yTile - 1) % 9) + 1,
                                ProposedMinimumDistance = null,
                                Confirmed = false
                            };
                        }
                    }
                }
            }

            var maxXIndex = widthAndDepth * 5 - 1;
            var maxYIndex = widthAndDepth * 5 - 1;

            graph[0, 0].ProposedMinimumDistance = 0;

            var activeVertices = new Dictionary<Coordinate, GraphVertex>
            {
                { new Coordinate(0, 0), graph[0, 0] }
            };

            var hasFoundEnd = false;

            int handled = 0;

            while (!hasFoundEnd)
            {
                var currentVertex = activeVertices
                    .Values
                    .OrderBy(g => g.ProposedMinimumDistance.Value)
                    .First();

                var currentKey = new Coordinate(currentVertex.X, currentVertex.Y);

                var hasNorth = currentVertex.Y > 0;
                var hasWest = currentVertex.X > 0;
                var hasSouth = currentVertex.Y < maxYIndex;
                var hasEast = currentVertex.X < maxXIndex;

                if (hasNorth)
                {
                    var x = currentVertex.X;
                    var y = currentVertex.Y - 1;
                    var key = new Coordinate(x, y);

                    if (activeVertices.ContainsKey(key))
                    {
                        var activeNeighbour = activeVertices[key];
                        activeNeighbour.ProposedMinimumDistance = Math.Min(
                            currentVertex.ProposedMinimumDistance.Value + activeNeighbour.Risk, activeNeighbour.ProposedMinimumDistance.Value);
                    }
                    else
                    {
                        var newVertex = graph[x, y];
                        if (!newVertex.Confirmed)
                        {
                            newVertex.ProposedMinimumDistance = currentVertex.ProposedMinimumDistance.Value + newVertex.Risk;
                            activeVertices.Add(key, newVertex);
                        }
                    }
                }

                if (hasSouth)
                {
                    var x = currentVertex.X;
                    var y = currentVertex.Y + 1;
                    var key = new Coordinate(x, y);

                    if (activeVertices.ContainsKey(key))
                    {
                        var activeNeighbour = activeVertices[key];
                        activeNeighbour.ProposedMinimumDistance = Math.Min(
                            currentVertex.ProposedMinimumDistance.Value + activeNeighbour.Risk, activeNeighbour.ProposedMinimumDistance.Value);
                    }
                    else
                    {
                        var newVertex = graph[x, y];
                        if (!newVertex.Confirmed)
                        {
                            newVertex.ProposedMinimumDistance = currentVertex.ProposedMinimumDistance.Value + newVertex.Risk;
                            activeVertices.Add(key, newVertex);
                        }
                    }
                }

                if (hasWest)
                {
                    var x = currentVertex.X - 1;
                    var y = currentVertex.Y;
                    var key = new Coordinate(x, y);

                    if (activeVertices.ContainsKey(key))
                    {
                        var activeNeighbour = activeVertices[key];
                        activeNeighbour.ProposedMinimumDistance = Math.Min(
                            currentVertex.ProposedMinimumDistance.Value + activeNeighbour.Risk, activeNeighbour.ProposedMinimumDistance.Value);
                    }
                    else
                    {
                        var newVertex = graph[x, y];
                        if (!newVertex.Confirmed)
                        {
                            newVertex.ProposedMinimumDistance = currentVertex.ProposedMinimumDistance.Value + newVertex.Risk;
                            activeVertices.Add(key, newVertex);
                        }
                    }
                }

                if (hasEast)
                {
                    var x = currentVertex.X + 1;
                    var y = currentVertex.Y;
                    var key = new Coordinate(x, y);

                    if (activeVertices.ContainsKey(key))
                    {
                        var activeNeighbour = activeVertices[key];
                        activeNeighbour.ProposedMinimumDistance = Math.Min(
                            currentVertex.ProposedMinimumDistance.Value + activeNeighbour.Risk, activeNeighbour.ProposedMinimumDistance.Value);
                    }
                    else
                    {
                        var newVertex = graph[x, y];
                        if (!newVertex.Confirmed)
                        {
                            newVertex.ProposedMinimumDistance = currentVertex.ProposedMinimumDistance.Value + newVertex.Risk;
                            activeVertices.Add(key, newVertex);
                        }
                    }
                }

                currentVertex.Confirmed = true;
                activeVertices.Remove(currentKey);

                if (currentVertex.X == maxXIndex && currentVertex.Y == maxYIndex)
                {
                    hasFoundEnd = true;
                }

                handled++;
            }

            var endVertex = graph[maxXIndex, maxYIndex];

            return endVertex.ProposedMinimumDistance.Value;
        }
    }
}
