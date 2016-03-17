using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Since this is PCL , the window library that contains some geometric structs (e.g, Size, Point , Rect) does not work. Therefore these here are structs which should do the job
/// </summary>
namespace DiceInvader.Base.Helpers
{
    public struct Size
    {
        //
        // Summary:
        //     Gets a value that represents a static empty System.Windows.Size.
        //
        // Returns:
        //     An empty instance of System.Windows.Size.
        public static Size Empty { get { return new Size(); }}
        //
        // Summary:
        //     Gets or sets the System.Windows.Size.Height of this instance of System.Windows.Size.
        //
        // Returns:
        //     The System.Windows.Size.Height of this instance of System.Windows.Size. The default
        //     is 0. The value cannot be negative.
        public double Height { get; set; }
        //
        // Summary:
        //     Gets a value that indicates whether this instance of System.Windows.Size is System.Windows.Size.Empty.
        //
        // Returns:
        //     true if this instance of size is System.Windows.Size.Empty; otherwise false.
        public bool IsEmpty { get; }
        //
        // Summary:
        //     Gets or sets the System.Windows.Size.Width of this instance of System.Windows.Size.
        //
        // Returns:
        //     The System.Windows.Size.Width of this instance of System.Windows.Size. The default
        //     value is 0. The value cannot be negative.
        public double Width { get; set; }
        /// <summary>
        /// Create an instance of Size
        /// </summary>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public Size(double width, double height)
        {
            Width = width;
            Height = height;
            IsEmpty = width + height > 0;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
    }

    public struct Point:IFormattable    
    {

        /// <summary>
        /// Creates a new Point structure that contains the specified coordinates.
        /// </summary>
        /// <param name="x"> The x-coordinate of the new Point structure.</param>
        /// <param name="y"> The y-coordinate of the new Point structure.</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        //
        // Summary:
        //     Gets or sets the System.Windows.Point.X-coordinate value of this System.Windows.Point
        //     structure.
        //
        // Returns:
        //     The System.Windows.Point.X-coordinate value of this System.Windows.Point structure.
        //     The default value is 0.
        public double X { get; set; }
        //
        // Summary:
        //     Gets or sets the System.Windows.Point.Y-coordinate value of this System.Windows.Point.
        //
        // Returns:
        //     The System.Windows.Point.Y-coordinate value of this System.Windows.Point structure.
        //     The default value is 0.
        public double Y { get; set; }


        /// <summary>
        ///  Compares two System.Windows.Point structures for equality.
        /// </summary>
        /// <param name="value"> The point to compare to this instance.</param>
        /// <returns>true if both Point structures contain the same Point.X and System.Windows.Point.Y values; otherwise, false.</returns>
        public bool Equals(Point value)
        {
            return value.X == X && value.Y == Y;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
    }

    public struct Rect : IFormattable
    {
        /// <summary>
        /// IsEmpty - this returns true if this rect is the Empty rectangle. 
        /// Note: If width or height are 0 this Rectangle still contains a 0 or 1 dimensional set
        /// of points, so this method should not be used to check for 0 area.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return Width <= 0 && Height <= 0;
            }
        }

        //
        // Summary:
        //     Gets the position of the top-left corner of the rectangle.
        //
        // Returns:
        //     The position of the top-left corner of the rectangle.
        public Point TopLeft { get; }
        //
        // Summary:
        //     Gets the position of the top-right corner of the rectangle.
        //
        // Returns:
        //     The position of the top-right corner of the rectangle.
        public Point TopRight { get; }
        //
        // Summary:
        //     Gets the position of the bottom-left corner of the rectangle
        //
        // Returns:
        //     The position of the bottom-left corner of the rectangle.
        public Point BottomLeft { get; }
        //
        // Summary:
        //     Gets the position of the bottom-right corner of the rectangle.
        //
        // Returns:
        //     The position of the bottom-right corner of the rectangle.
        public Point BottomRight { get; }
       
        //
        // Summary:
        //     Gets or sets the width of the rectangle.
        //
        // Returns:
        //     A positive number that represents the width of the rectangle. The default is
        //     0.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     System.Windows.Rect.Width is set to a negative value.
        //
        //   T:System.InvalidOperationException:
        //     System.Windows.Rect.Width is set on an System.Windows.Rect.Empty rectangle.
        public double Width { get; set; }

        //
        // Summary:
        //     Gets or sets the height of the rectangle.
        //
        // Returns:
        //     A positive number that represents the height of the rectangle. The default is
        //     0.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     System.Windows.Rect.Height is set to a negative value.
        //
        //   T:System.InvalidOperationException:
        //     System.Windows.Rect.Height is set on an System.Windows.Rect.Empty rectangle.
        public double Height { get; set; }


        //
        // Summary:
        //     Gets the x-axis value of the left side of the rectangle.
        //
        // Returns:
        //     The x-axis value of the left side of the rectangle.
        public double Left { get; }

        //
        // Summary:
        //     Gets the x-axis value of the right side of the rectangle.
        //
        // Returns:
        //     The x-axis value of the right side of the rectangle.
        public double Right { get; }

        //
        // Summary:
        //     Gets the y-axis position of the top of the rectangle.
        //
        // Returns:
        //     The y-axis position of the top of the rectangle.
        public double Top { get; }

        //
        // Summary:
        //     Gets the y-axis value of the bottom of the rectangle.
        //
        // Returns:
        //     The y-axis value of the bottom of the rectangle. If the rectangle is empty, the
        //     value is System.Double.NegativeInfinity .
        public double Bottom { get; }


        //
        // Summary:
        //     Gets or sets the x-axis value of the left side of the rectangle.
        //
        // Returns:
        //     The x-axis value of the left side of the rectangle.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     System.Windows.Rect.X is set on an System.Windows.Rect.Empty rectangle.
        public double X { get; set; }


        //
        // Summary:
        //     Gets or sets the y-axis value of the top side of the rectangle.
        //
        // Returns:
        //     The y-axis value of the top side of the rectangle.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     System.Windows.Rect.Y is set on an System.Windows.Rect.Empty rectangle.
        public double Y { get; set; }


        /// <summary>
        /// Initializes a new instance of the System.Windows.Rect structure that has the
        ///     specified top-left corner location and the specified width and height.
        /// </summary>
        /// <param name="location"> A point that specifies the location of the top-left corner of the rectangle.</param>
        /// <param name="size">  A Size structure that specifies the width and height of the rectangle.</param>
        public Rect(Point location, Size size)
        {
            TopLeft = location;            
            TopRight = new Point(location.X + size.Width, location.Y);
            BottomLeft = new Point(location.X, location.Y + size.Height);
            BottomRight = new Point(location.X + size.Width, location.Y + size.Height);
            Width = size.Width;
            Height = size.Height;
            Left = TopLeft.X;
            Right = TopRight.X;
            Top = TopLeft.Y;
            Bottom = BottomLeft.Y;
            X = TopLeft.X;
            Y = TopLeft.Y;
           
        }

        /// <summary>
        /// IntersectsWith - Returns true if the Rect intersects with this rectangle
        /// Returns false otherwise. 
        /// Note that if one edge is coincident, this is considered an intersection.
        /// </summary> 
        /// <returns> 
        /// Returns true if the Rect intersects with this rectangle
        /// Returns false otherwise. 
        /// or Height
        /// </returns>
        /// <param name="rect"> Rect 
        public bool IntersectsWith(Rect rect)
        {
            if (IsEmpty || rect.IsEmpty)
            {
                return false;
            }

            return (rect.Left <= Right) &&
                   (rect.Right >= Left) &&
                   (rect.Top <= Bottom) &&
                   (rect.Bottom >= Top);
        }


        /// <summary> 
        /// Contains - Returns true if the Point is within the rectangle, inclusive of the edges.
        /// Returns false otherwise. 
        /// </summary> 
        /// <param name="point"> The point which is being tested 
        /// <returns> 
        /// Returns true if the Point is within the rectangle.
        /// Returns false otherwise
        /// </returns>
        public bool Contains(Point point)
        {
            return Contains(point.X, point.Y);
        }

        /// <summary> 
        /// Contains - Returns true if the Point represented by x,y is within the rectangle inclusive of the edges.
        /// Returns false otherwise.
        /// </summary>
        /// <param name="x"> X coordinate of the point which is being tested  
        /// <param name="y"> Y coordinate of the point which is being tested 
        /// <returns> 
        /// Returns true if the Point represented by x,y is within the rectangle. 
        /// Returns false otherwise.
        /// </returns> 
        public bool Contains(double x, double y)
        {
            if (IsEmpty)
            {
                return false;
            }

            return ContainsInternal(x, y);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }



        /// <summary> 
        /// ContainsInternal - Performs just the "point inside" logic 
        /// </summary>
        /// <returns> 
        /// bool - true if the point is inside the rect
        /// </returns>
        /// <param name="x"> The x-coord of the point to test 
        /// <param name="y"> The y-coord of the point to test  
        private bool ContainsInternal(double x, double y)
        {
            // We include points on the edge as "contained". 
            // We do "x - _width <= _x" instead of "x <= _x + _width"
            // so that this check works when _width is PositiveInfinity 
            // and _x is NegativeInfinity.
            return ((x >= X) && (x - Width <= X) &&
                    (y >= Y) && (y - Height <= Y));
        }
    }
}
