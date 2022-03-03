using drawer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drawer
{

    internal static class Drawer
    {
        /** Преобразование примитивов с учётом отсечения */
        private static IEnumerable<IObraz> clipPrimitives(ILegend l, IDrawProperties pr, IRect rect, Func<ILegend, string, (double width, double height)> measureText)
        {
            foreach (var g in l.primitives)
            {
                if (g.rect.left >= rect.left && g.rect.bottom >= rect.bottom && g.rect.right <= rect.right && g.rect.top <= rect.top)
                    // Целиком лежит внутри прямоугольника
                    yield return new IObraz { coords = (double[])g.coords.Clone(), text = Text.calcText(g.name, l, (double[])g.coords.Clone(), pr, measureText) };
                else if (g.rect.left < rect.right && g.rect.bottom < rect.top && g.rect.right > rect.left && g.rect.top > rect.bottom)
                    // Необходимо отсекать
                    switch (l.type)
                    {
                        case GrTypeEnum.Line:

                            foreach (var cs in Poliline.clipPolyline(g, rect))
                                yield return new IObraz { coords = cs, text = Text.calcText(g.name, l, cs, pr, measureText) };
                            break;
                        case GrTypeEnum.Polygon:
                            {
                                var cs = Polygon.clipPolygon(g, rect);
                                if (cs.Length > 0)
                                    yield return new IObraz { coords = cs, text = Text.calcText(g.name, l, cs, pr, measureText) };
                            }

                            break;
                    }
            }
        }

        /** Подготовка данных для отрисовки */
        public static ILayer[] build(ILegend[] ls, IDrawProperties pr, IRect rect, Func<ILegend, string, (double width, double height)> measureText)
        {
            var result = new List<ILayer>();

            foreach (var l in ls)
            {
                //var size = l.text.font.size;
                //if (l.text.scaled)
                //    size *= pr.scale / mashtab2Scale(l.text.mashtabBase);
                //ctx.font = `${                    size            }                pt ${                    l.text.font.family            }`;

                var mas = new List<double[]>();
                var texts = new List<IText>();

                if (l.mashtabRange.min > pr.mashtab || l.mashtabRange.max < pr.mashtab) continue;
                foreach (var obraz in clipPrimitives(l, pr, rect, measureText))
                {
                    var csOpt = Calc.optimize(obraz.coords, 1 / pr.scale);
                    Calc.translate(csOpt, pr);
                    mas.Add(csOpt);

                    if (obraz.text == null) continue;
                    texts.Add(obraz.text);
                    Calc.translateText(obraz.text, pr);
                }

                result.Add(new ILayer { legendId = l.id, coords = mas.ToArray(), texts = texts, font = "" });
            }

            Text.optimizeTexts(result);
            return result.ToArray();
        }
    }
}