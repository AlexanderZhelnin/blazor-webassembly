using drawer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drawer
{
    public class Init
    {
        public static ILegend[] ls = new ILegend[0];

        public static IRect r;
        public static double cx;
        public static double cy;


        //public static Bitmap bm1;
        //public static Graphics g;
        //private static ILegend LastLeg = null;
        //private static Dictionary<string, SizeF> Sizes = new Dictionary<string, SizeF>();

        public static Func<ILegend, string, (double width, double height)> mt;/* = new Func<ILegend, string, (double width, double height)>((ILegend l, string txt) =>*/
        //{
        //    if (LastLeg?.id == l.id)
        //    {
        //        if (Sizes.TryGetValue(txt, out var s)) return (s.Width, s.Height);
        //    }
        //    else
        //    {
        //        Sizes = new Dictionary<string, SizeF>();
        //        LastLeg = l;
        //    }

        //    //var size = l.text.font.size;
        //    //var ms = g.MeasureString(txt, new Font(l.text.font.family, (float)size));

        //    Sizes.Add(txt, ms);
        //    return (ms.Width, ms.Height);
        //});

        public Init(ILegend[] lss, Func<ILegend, string, (double width, double height)> mtt)
        {
            ls = lss;
            mt = mtt;
            //var json = File.ReadAllText(@"C:\Project\angular\angular-canvas\src\assets\primitives.json");
            //ls = JsonConvert.DeserializeObject<ILegend[]>(json);
            r = new IRect { left = -100, bottom = 100, right = 5300, top = 2800 };
            cx = (r.right + r.left) / 2;
            cy = (r.bottom + r.top) / 2;

            //bm1 = new Bitmap(100, 100);
            //g = Graphics.FromImage(bm1);
        }

        public static void tt() { }

    }
}
