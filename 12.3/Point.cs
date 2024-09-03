using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._3
{
    public class Point<T> where T : IComparable
    {
        public T? Data { get; set; }
        public Point<T>? Right { get; set; }
        public Point<T>? Left { get; set; }

        public Point()
        {
            this.Data = default;
            this.Right = null;
            this.Left = null;
        }

        public Point(T data)
        {
            this.Data = data;
            this.Right = null;
            this.Left = null;
        }

        public override string ToString()
        {
            return Data == null ? "" : Data.ToString();
        }

        public int CompareTo(Point<T> p)
        {
            return Data.CompareTo(p.Data);
        }
    }
}
