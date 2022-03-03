using drawer.Models;
using System.Collections.Generic;

namespace drawer
{

    internal static class Polygon
    {
        public static int getNextIndex(int curIndex, int len)
        {
            curIndex += 2;
            if (curIndex >= len) curIndex = 0;
            return curIndex;
        }

        private static double[] clipLeft(double[] coords, double left)
        {
            if (coords.Length == 0) return coords;


            var pl = new List<double>();
            var curIndex = 0;

            var px1 = coords[0];
            var py1 = coords[1];

            if (px1 >= left)
            {
                pl.Add(px1);
                pl.Add(py1);
            }

            var len = coords.Length / 2;
            for (var i = 1; i <= len; i++)
            {
                curIndex = getNextIndex(curIndex, coords.Length);
                var px2 = coords[curIndex];
                var py2 = coords[curIndex + 1];

                if (px1 >= left && px2 >= left)
                {
                    pl.Add(px2);
                    pl.Add(py2);
                }
                else if (px1 < left && px2 > left)
                {
                    pl.Add(left);
                    pl.Add((left - px1) * (py2 - py1) / (px2 - px1) + py1);

                    pl.Add(px2);
                    pl.Add(py2);
                }
                else if (px1 > left && px2 < left)
                {
                    // pl.Add(px1);
                    // pl.Add(py1);

                    pl.Add(left);
                    pl.Add((left - px1) * (py2 - py1) / (px2 - px1) + py1);
                }
                px1 = px2;
                py1 = py2;
            }

            return pl.ToArray();
        }
        private static double[] clipRight(double[] coords, double right)
        {
            var curIndex = 0;


            if (coords.Length == 0) return coords;

            var pl = new List<double>();

            var px1 = coords[0];
            var py1 = coords[1];

            if (px1 <= right)
            {
                pl.Add(px1);
                pl.Add(py1);
            }
            var len = coords.Length / 2;

            for (var i = 0; i < len; i++)
            {
                curIndex = getNextIndex(curIndex, coords.Length);

                var px2 = coords[curIndex];
                var py2 = coords[curIndex + 1];

                if (px1 <= right && px2 <= right)
                {
                    pl.Add(px2);
                    pl.Add(py2);
                }
                else if (px1 > right && px2 < right)
                {
                    pl.Add(right);
                    pl.Add((right - px1) * (py2 - py1) / (px2 - px1) + py1);

                    pl.Add(px2);
                    pl.Add(py2);
                }
                else if (px1 < right && px2 > right)
                {
                    // pl.Add(px1);
                    // pl.Add(py1);

                    pl.Add(right);
                    pl.Add((right - px1) * (py2 - py1) / (px2 - px1) + py1);

                    // res.Add(pl);

                    // pl = [];
                }
                px1 = px2;
                py1 = py2;
            }

            return pl.ToArray();
        }
        private static double[] clipBottom(double[] coords, double bottom)
        {

            if (coords.Length == 0) return coords;
            var curIndex = 0;


            var pl = new List<double>();

            var px1 = coords[0];
            var py1 = coords[0 + 1];

            if (py1 >= bottom)
            {
                pl.Add(px1);
                pl.Add(py1);
            }

            var len = coords.Length / 2;
            for (var i = 0; i < len; i++)
            {
                curIndex = getNextIndex(curIndex, coords.Length);
                var px2 = coords[curIndex];
                var py2 = coords[curIndex + 1];

                if (py1 >= bottom && py2 >= bottom)
                {
                    pl.Add(px2);
                    pl.Add(py2);
                }
                else if (py1 < bottom && py2 > bottom)
                {
                    pl.Add((bottom - py1) * (px2 - px1) / (py2 - py1) + px1);
                    pl.Add(bottom);

                    pl.Add(px2);
                    pl.Add(py2);
                }
                else if (py1 > bottom && py2 < bottom)
                {
                    // pl.Add(px1);
                    // pl.Add(py1);

                    pl.Add((bottom - py1) * (px2 - px1) / (py2 - py1) + px1);
                    pl.Add(bottom);

                    // res.Add(pl);

                    // pl = [];
                }
                px1 = px2;
                py1 = py2;
            }

            return pl.ToArray();
        }
        private static double[] clipTop(double[] coords, double top)
        {
            if (coords.Length == 0) return coords;

            var curIndex = 0;

            var pl = new List<double>();

            var px1 = coords[0];
            var py1 = coords[1];

            if (py1 <= top)
            {
                pl.Add(px1);
                pl.Add(py1);
            }

            var len = coords.Length / 2;
            for (var i = 0; i < len; i++)
            {
                curIndex = getNextIndex(curIndex, coords.Length);
                var px2 = coords[curIndex];
                var py2 = coords[curIndex + 1];

                if (py1 <= top && py2 <= top)
                {
                    pl.Add(px2);
                    pl.Add(py2);
                }
                else if (py1 < top && py2 > top)
                {
                    // pl.Add(px1);
                    // pl.Add(py1);

                    pl.Add((top - py1) * (px2 - px1) / (py2 - py1) + px1);
                    pl.Add(top);
                }
                else if (py1 > top && py2 < top)
                {
                    pl.Add((top - py1) * (px2 - px1) / (py2 - py1) + px1);
                    pl.Add(top);

                    pl.Add(px2);
                    pl.Add(py2);
                }

                px1 = px2;
                py1 = py2;
            }

            return pl.ToArray();
        }

        /** Отсечение полигона по прямоугольнику */
        public static double[] clipPolygon(IPrimitive g, IRect rect)
        {
            double[] res;

            if (g.rect.left < rect.left)
                res = clipLeft(g.coords, rect.left);
            else
                res = (double[])g.coords.Clone();

            if (g.rect.bottom < rect.bottom)
                res = clipBottom(res, rect.bottom);

            if (g.rect.right > rect.right)
                res = clipRight(res, rect.right);

            if (g.rect.top > rect.top)
                res = clipTop(res, rect.top);

            return res;
        }
    }
}
