using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftGL
{
    partial class SoftGLRenderContext
    {
        private static void FindPixelsAtLine(vec3 p0, vec3 p1, List<vec3> result)
        {
            //vec3 originalP0 = p0;
            //vec3 originalP1 = p1;
            //Console.WriteLine("{0},{1}", originalP0, originalP1);
            p0 += new vec3(0.5f, 0.5f, 0); p1 += new vec3(0.5f, 0.5f, 0); // use a better(convenient) coordinate system.
            var list = new List<vec3>();
            if (p0.x < p1.x)
            {
                var x0Integer = (int)Math.Ceiling(p0.x);
                var x1Integer = (int)p1.x;
                for (int x = x0Integer; x <= x1Integer; x++)
                {
                    float alpha = (x - p0.x) / (p1.x - p0.x);
                    float y = (p1.y - p0.y) * alpha + p0.y;
                    float z = (p1.z - p0.z) * alpha + p0.z;
                    list.Add(new vec3(x, y, z));
                }
            }
            else // p1.x < p0.x
            {
                var x1Integer = (int)Math.Ceiling(p1.x);
                var x0Integer = (int)p0.x;
                for (int x = x1Integer; x <= x0Integer; x++)
                {
                    float alpha = (x - p1.x) / (p0.x - p1.x);
                    float y = (p0.y - p1.y) * alpha + p1.y;
                    float z = (p0.z - p1.z) * alpha + p1.z;
                    list.Add(new vec3(x, y, z));
                }
            }

            vec3[] yPoints;
            if (p0.y < p1.y)
            {
                var y0Integer = (int)Math.Ceiling(p0.y);
                var y1Integer = (int)p1.y;
                for (int y = y0Integer; y <= y1Integer; y++)
                {
                    float alpha = (y - p0.y) / (p1.y - p0.y);
                    float x = (p1.x - p0.x) * alpha + p0.x;
                    float z = (p1.z - p0.z) * alpha + p0.z;
                    list.Add(new vec3(x, y, z));
                }
            }
            else // p1.x < p0.x
            {
                var y1Integer = (int)Math.Ceiling(p1.y);
                var y0Integer = (int)p0.y;
                yPoints = new vec3[y0Integer - y1Integer + 1];
                for (int y = y1Integer; y <= y0Integer; y++)
                {
                    float alpha = (y - p1.y) / (p0.y - p1.y);
                    float x = (p0.x - p1.x) * alpha + p1.x;
                    float z = (p0.z - p1.z) * alpha + p1.z;
                    list.Add(new vec3(x, y, z));
                }
            }

            vec3[] points = (from item in list orderby item.x ascending select item).ToArray();
            vec3[] midPoints = GetMidPoints(points);
            if (midPoints != null && midPoints.Length > 0)
            {
                int index = 0, t = 0;
                if (result.Count == 0)
                {
                    vec3 item = midPoints[0];
                    result.Add(item);
                    index = 1;
                }

                while (index < midPoints.Length)
                {
                    vec3 item = midPoints[index];
                    if ((((int)item.x) != ((int)result[t].x))
                        || (((int)item.y) != ((int)result[t].y)))
                    {
                        result.Add(item);
                        t++;
                    }

                    index++;
                }
            }
        }

        private static vec3[] GetMidPoints(vec3[] points)
        {
            if (points == null || points.Length < 2) { return null; }
            var midPoints = new vec3[points.Length - 1];
            for (int i = 0; i < midPoints.Length; i++)
            {
                midPoints[i] = (points[i] + points[i + 1]) / 2.0f;
            }

            return midPoints;
        }

        private static vec3[] SortPoints(vec3 min, vec3[] xPoints, vec3[] yPoints, vec3 max)
        {
            bool horizontal = Math.Abs(max.x - min.x) >= Math.Abs(max.y - min.y);
            bool increase = min.y < max.y;
            int index = 0;
            var points = new vec3[1 + xPoints.Length + yPoints.Length + 1];

            points[index++] = min;

            int xIndex = 0, yIndex = 0;
            while (xIndex < xPoints.Length && yIndex < yPoints.Length)
            {
                if (horizontal)
                {
                    if (increase)
                    {
                        if (xPoints[xIndex].x < yPoints[yIndex].x)
                        {
                            points[index++] = xPoints[xIndex++];
                        }
                        else
                        {
                            points[index++] = yPoints[yIndex++];
                        }
                    }
                    else
                    {
                        if (xPoints[xIndex].x > yPoints[yIndex].x)
                        {
                            points[index++] = xPoints[xIndex++];
                        }
                        else
                        {
                            points[index++] = yPoints[yIndex++];
                        }
                    }
                }
                else
                {
                    if (increase)
                    {
                        if (xPoints[xIndex].y < yPoints[yIndex].y)
                        {
                            points[index++] = xPoints[xIndex++];
                        }
                        else
                        {
                            points[index++] = yPoints[yIndex++];
                        }
                    }
                    else
                    {
                        if (xPoints[xIndex].y > yPoints[yIndex].y)
                        {
                            points[index++] = xPoints[xIndex++];
                        }
                        else
                        {
                            points[index++] = yPoints[yIndex++];
                        }
                    }
                }
            }

            while (xIndex < xPoints.Length)
            {
                points[index++] = xPoints[xIndex++];
            }

            while (yIndex < yPoints.Length)
            {
                points[index++] = yPoints[yIndex++];
            }

            points[index++] = max;

            return points;
        }
    }
}
