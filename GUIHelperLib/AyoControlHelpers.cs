using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;


namespace AyoControlLibrary
{
    public enum ERoundedType
    {
        None,
        Left,
        Right,
        Top,
        Bottom,
        All,
        Custom
    }

    [Flags]
    public enum ERoundedFlag
    {
        None = 1,
        UppLeft = 2,
        UppRight = 4,
        DownLeft = 8,
        DownRight = 16,
    }


    public static class AyoControlHelpers
    {

        public static PathGeometry GenerateBorder(ERoundedType type, int radius, Rect rect, ERoundedFlag roundedFlag)
        {
            PathGeometry res = new PathGeometry();

            switch (type)
            {
                case ERoundedType.None:
                    Rect r = new Rect(rect.X, rect.Y, rect.Width, rect.Height);
                    res.AddGeometry(new RectangleGeometry(r));
                    break;

                case ERoundedType.Left:
                    res = AyoControlHelpers.GenerateLeftRoundedPath(rect, radius);
                    break;

                case ERoundedType.Right:
                    res = AyoControlHelpers.GenerateRightRoundedPath(rect, radius);
                    break;

                case ERoundedType.Top:
                    res = AyoControlHelpers.GenerateTopRoundedPath(rect, radius);
                    break;

                case ERoundedType.Bottom:
                    res = AyoControlHelpers.GenerateBottomRoundedPath(rect, radius);
                    break;

                case ERoundedType.All:
                    res = AyoControlHelpers.GenerateRoundedBorderPath(rect, radius);
                    break;

                case ERoundedType.Custom:
                    res = AyoControlHelpers.GenerateCustomRoundedPath(rect, radius, roundedFlag);
                    break;

                default:
                    break;
            }

            return res;
        }

        public static PathGeometry GenerateCustomRoundedPath(Rect rect, int radius, ERoundedFlag flag)
        {
            if (rect == null) throw new ArgumentNullException(nameof(rect));
            if (radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
            List<PathSegment> list = new List<PathSegment>();

            LineSegment segUp = new LineSegment(new Point(rect.X + rect.Width, rect.Y), false);
            LineSegment segRight = new LineSegment(new Point(rect.X + rect.Width, rect.Y + rect.Height), false);
            LineSegment segDown = new LineSegment(new Point(rect.X, rect.Y + rect.Height), false);
            LineSegment segLeft = new LineSegment(new Point(rect.X, rect.Y), false);

            ArcSegment UppLeftArc = null;
            ArcSegment UppRightArc = null;
            ArcSegment DownLeftArc = null;
            ArcSegment DownRightArc = null;

            Point startPoint = new Point(rect.X, rect.Y);
            Size size = new Size(radius, radius);

            list.Add(segUp);
            list.Add(segRight);
            list.Add(segDown);
            list.Add(segLeft);




            // upp left
            if (flag.HasFlag(ERoundedFlag.UppLeft))
            {
                segLeft.Point = new Point(rect.X, rect.Y + radius);

                startPoint = new Point(rect.X, rect.Y + radius);
                UppLeftArc = new ArcSegment(new Point(rect.X + radius, rect.Y), size, 90, false, SweepDirection.Clockwise, false);

                int idx = list.IndexOf(segLeft) + 1;

                list.Insert(0, UppLeftArc);
            }

            // upp right
            if (flag.HasFlag(ERoundedFlag.UppRight))
            {
                segUp.Point = new Point(rect.X + rect.Width - radius, rect.Y);

                int idx = list.IndexOf(segUp) + 1;


                UppRightArc = new ArcSegment(new Point(rect.X + rect.Width, rect.Y + radius), size, 90, false, SweepDirection.Clockwise, false);
                list.Insert(idx, UppRightArc);
            }

            // down right
            if (flag.HasFlag(ERoundedFlag.DownRight))
            {

                segRight.Point = new Point(rect.X + rect.Width, rect.Y + rect.Height - radius);

                int idx = list.IndexOf(segRight) + 1;


                DownRightArc = new ArcSegment(new Point(rect.X + rect.Width - radius, rect.Y + rect.Height), size, 90, false, SweepDirection.Clockwise, false);
                list.Insert(idx, DownRightArc);

            }


            // down left
            if (flag.HasFlag(ERoundedFlag.DownLeft))
            {

                segDown.Point = new Point(rect.X + radius, rect.Y + rect.Height);

                int idx = list.IndexOf(segDown) + 1;

                DownLeftArc = new ArcSegment(new Point(rect.X, rect.Y + rect.Height - radius), size, 90, false, SweepDirection.Clockwise, false);
                list.Insert(idx, DownLeftArc);
            }



            PathFigure path = new PathFigure(startPoint, list, true);

            PathGeometry graphPath = new PathGeometry();
            graphPath.Figures.Add(path);

            return graphPath;
        }

        public static PathGeometry GenerateBottomRoundedPath(Rect rect, int radius)
        {
            if (rect == null) throw new ArgumentNullException(nameof(rect));
            if (radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
            List<PathSegment> list = new List<PathSegment>();

            Size size = new Size(radius, radius);

            Point p12 = new Point(rect.Width + rect.X, rect.Height + rect.Y);
            Point p3 = new Point(rect.Width - radius + rect.X, rect.Height + rect.Y);
            BezierSegment segment1 = new BezierSegment(p12, p12, p3, false);
            ArcSegment arcSegment1 = new ArcSegment(p3, size, 90, false, SweepDirection.Clockwise, false);


            LineSegment segment2 = new LineSegment(new Point(radius + rect.X, rect.Height + rect.Y), false);

            p12 = new Point(rect.X, rect.Height + rect.Y);
            p3 = new Point(rect.X, rect.Height - radius + rect.Y);
            BezierSegment segment3 = new BezierSegment(p12, p12, p3, false);
            ArcSegment arcSegment2 = new ArcSegment(p3, size, 90, false, SweepDirection.Clockwise, false);

            LineSegment segment4 = new LineSegment(new Point(rect.X, rect.Y), false);
            LineSegment segment5 = new LineSegment(new Point(rect.Width + rect.X, rect.Y), false);
            LineSegment segment6 = new LineSegment(new Point(rect.Width + rect.X, rect.Height - radius + rect.Y), false);

            list.Add(arcSegment1);
            list.Add(segment2);
            list.Add(arcSegment2);
            list.Add(segment4);
            list.Add(segment5);
            list.Add(segment6);

            PathFigure path = new PathFigure(new Point(rect.Width + rect.X, rect.Height - radius + rect.Y), list, true);

            PathGeometry graphPath = new PathGeometry();
            graphPath.Figures.Add(path);

            return graphPath;
        }

        public static PathGeometry GenerateLeftRoundedPath(Rect rect, int radius, int penSize = 1)
        {
            if (rect == null) throw new ArgumentNullException(nameof(rect));
            if (radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
            List<PathSegment> list = new List<PathSegment>();
            Size size = new Size(radius, radius);

            Point p12 = new Point(0 + rect.X, rect.Height + rect.Y);
            Point p3 = new Point(0 + rect.X, rect.Height - radius + rect.Y);
            BezierSegment segment1 = new BezierSegment(p12, p12, p3, false);
            ArcSegment arcSegment1 = new ArcSegment(p3, size, 90, false, SweepDirection.Clockwise, false);


            LineSegment segment2 = new LineSegment(new Point(0 + rect.X, radius + rect.Y), false);

            p12 = new Point(0 + rect.X, 0 + rect.Y);
            p3 = new Point(radius + rect.X, 0 + rect.Y);
            BezierSegment segment3 = new BezierSegment(p12, p12, p3, false);
            ArcSegment arcSegment2 = new ArcSegment(p3, size, 90, false, SweepDirection.Clockwise, false);


            LineSegment segment4 = new LineSegment(new Point(rect.Width + rect.X, 0 + rect.Y), false);
            LineSegment segment5 = new LineSegment(new Point(rect.Width + rect.X, rect.Height + rect.Y), false);
            LineSegment segment6 = new LineSegment(new Point(radius + rect.X, rect.Height + rect.Y), false);

            list.Add(arcSegment1);
            list.Add(segment2);
            list.Add(arcSegment2);
            list.Add(segment4);
            list.Add(segment5);
            list.Add(segment6);

            PathFigure path = new PathFigure(new Point(radius + rect.X, rect.Height + rect.Y), list, true);

            PathGeometry graphPath = new PathGeometry();
            graphPath.Figures.Add(path);
            return graphPath;
        }

        public static PathGeometry GenerateRightRoundedPath(Rect rect, int radius, int penSize = 1)
        {
            if (rect == null) throw new ArgumentNullException(nameof(rect));
            if (radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
            List<PathSegment> list = new List<PathSegment>();

            Size size = new Size(radius, radius);

            Point p12 = new Point(rect.Width + rect.X, 0 + rect.Y);
            Point p3 = new Point(rect.Width + rect.X, radius + rect.Y);
            BezierSegment segment1 = new BezierSegment(p12, p12, p3, false);
            ArcSegment arcSegment1 = new ArcSegment(p3, size, 90, false, SweepDirection.Clockwise, false);


            LineSegment segment2 = new LineSegment(new Point(rect.Width + rect.X, rect.Height - radius + rect.Y), false);

            p12 = new Point(rect.Width + rect.X, rect.Height + rect.Y);
            p3 = new Point(rect.Width - radius + rect.X, rect.Height + rect.Y);
            BezierSegment segment3 = new BezierSegment(p12, p12, p3, false);
            ArcSegment arcSegment2 = new ArcSegment(p3, size, 90, false, SweepDirection.Clockwise, false);


            LineSegment segment4 = new LineSegment(new Point(0 + rect.X, rect.Height + rect.Y), false);
            LineSegment segment5 = new LineSegment(new Point(0 + rect.X, 0 + rect.Y), false);
            LineSegment segment6 = new LineSegment(new Point(rect.Width - radius + rect.X, 0 + rect.Y), false);

            list.Add(arcSegment1);
            list.Add(segment2);
            list.Add(arcSegment2);
            list.Add(segment4);
            list.Add(segment5);
            list.Add(segment6);

            PathFigure path = new PathFigure(new Point(rect.Width - radius + rect.X, 0 + rect.Y), list, true);

            PathGeometry graphPath = new PathGeometry();
            graphPath.Figures.Add(path);

            return graphPath;
        }


        public static PathGeometry GenerateRoundedBorderPath(Rect rect, int radius, int penSize = 1)
        {
            if (rect == null) throw new ArgumentNullException(nameof(rect));
            if (radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
            List<PathSegment> list = new List<PathSegment>();

            Size size = new Size(radius, radius);

            Point p12 = new Point(0 + rect.X, 0 + rect.Y);
            Point p3 = new Point(radius + rect.X, 0 + rect.Y);
            BezierSegment segment1 = new BezierSegment(p12, p12, p3, false);
            ArcSegment arcSegment1 = new ArcSegment(p3, size, 90, false, SweepDirection.Clockwise, false);

            LineSegment segment2 = new LineSegment(new Point(rect.Width - radius + rect.X, 0 + rect.Y), false);

            p12 = new Point(rect.Width + rect.X, 0 + rect.Y);
            p3 = new Point(rect.Width + rect.X, radius + rect.Y);
            BezierSegment segment3 = new BezierSegment(p12, p12, p3, false);
            ArcSegment arcSegment2 = new ArcSegment(p3, size, 90, false, SweepDirection.Clockwise, false);


            LineSegment segment4 = new LineSegment(new Point(rect.Width + rect.X, rect.Height - radius + rect.Y), false);

            p12 = new Point(rect.Width + rect.X, rect.Height + rect.Y);
            p3 = new Point(rect.Width - radius + rect.X, rect.Height + rect.Y);
            BezierSegment segment5 = new BezierSegment(p12, p12, p3, false);
            ArcSegment arcSegment3 = new ArcSegment(p3, size, 90, false, SweepDirection.Clockwise, false);


            LineSegment segment6 = new LineSegment(new Point(radius + rect.X, rect.Height + rect.Y), false);

            p12 = new Point(0 + rect.X, rect.Height + rect.Y);
            p3 = new Point(0 + rect.X, rect.Height - radius + rect.Y);
            BezierSegment segment7 = new BezierSegment(p12, p12, p3, false);
            ArcSegment arcSegment4 = new ArcSegment(p3, size, 90, false, SweepDirection.Clockwise, false);


            LineSegment segment8 = new LineSegment(new Point(0 + rect.X, radius + rect.Y), false);


            list.Add(arcSegment1);
            list.Add(segment2);
            list.Add(arcSegment2);
            list.Add(segment4);
            list.Add(arcSegment3);
            list.Add(segment6);
            list.Add(arcSegment4);
            list.Add(segment8);

            PathFigure path = new PathFigure(new Point(0 + rect.X, radius + rect.Y), list, true);

            PathGeometry graphPath = new PathGeometry();
            graphPath.Figures.Add(path);

            return graphPath;
        }

        public static PathGeometry GenerateTopRoundedPath(Rect rect, int radius, int penSize = 1)
        {
            if (rect == null) throw new ArgumentNullException(nameof(rect));
            if (radius <= 0) throw new ArgumentException("Radius must be greater than 0.");
            List<PathSegment> list = new List<PathSegment>();

            ArcSegment arcSegment = new ArcSegment(new Point(radius + rect.X, 0 + rect.Y), new Size(radius, radius), 90, false, SweepDirection.Clockwise, false);

            Point p12 = new Point(0 + rect.X, 0 + rect.Y);
            Point p3 = new Point(radius + rect.X, 0 + rect.Y);
            BezierSegment segment1 = new BezierSegment(p12, p12, p3, false);

            LineSegment segment2 = new LineSegment(new Point(rect.Width - radius + rect.X, 0 + rect.Y), false);

            p12 = new Point(rect.Width + rect.X, 0 + rect.Y);
            p3 = new Point(rect.Width + rect.X, radius + rect.Y);
            BezierSegment segment3 = new BezierSegment(p12, p12, p3, false);

            ArcSegment arcSegment2 = new ArcSegment(new Point(rect.Width + rect.X, radius + rect.Y), new Size(radius, radius), 90, false, SweepDirection.Clockwise, false);



            LineSegment segment4 = new LineSegment(new Point(rect.Width + rect.X, rect.Height + rect.Y), false);
            LineSegment segment5 = new LineSegment(new Point(0 + rect.X, rect.Height + rect.Y), false);
            LineSegment segment6 = new LineSegment(new Point(0 + rect.X, radius + rect.Y), false);

            list.Add(arcSegment);
            list.Add(segment2);
            list.Add(arcSegment2);
            list.Add(segment4);
            list.Add(segment5);
            list.Add(segment6);

            PathFigure path = new PathFigure(new Point(0 + rect.X, radius + rect.Y), list, true);

            PathGeometry graphPath = new PathGeometry();
            graphPath.Figures.Add(path);

            return graphPath;
        }


    }
}