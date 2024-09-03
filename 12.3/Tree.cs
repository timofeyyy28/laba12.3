using ClassLibraryLabor10;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._3
{
    public class Tree<T> where T : IInit, IComparable, ICloneable, new()
    {
        Point<T>? root = null;

        int count = 0;

        public int Count => count;

       
        public Tree() { }

        T minItem = default;

        
        public Tree(int length)
        {
            count = length;
            root = MakeTree(length, root);
        }

        
        Point<T> MakeTree(int length, Point<T>? point)
        {
            T data = new T();
            data.RandomInit();
            Point<T> newItem = new Point<T>(data); 
            if (length == 0)
                return null;
            int nl = length / 2;
            int nr = length - nl - 1;
            newItem.Left = MakeTree(nl, newItem.Left); 
            newItem.Right = MakeTree(nr, newItem.Right); 
            return newItem;
        }


        public T SearchItem()
        {
            if (root == null)
                return default; 

            
            minItem = root.Data;

            return FindMinItem(root);
        }

        public T FindMinItem(Point<T> point)
        {
            if (point == null)
                return minItem;

           
            if (point.Left != null)
            {
                FindMinItem(point.Left);
            }

            
            if (point.Data is Musicalinstrument currentInstrument)
            {
                if (minItem is Musicalinstrument minInstrument)
                {
                    if (currentInstrument.id.number < minInstrument.id.number)
                    {
                        minItem = point.Data;
                    }
                }
                else
                {
                    minItem = point.Data;
                }
            }

           
            if (point.Right != null)
            {
                FindMinItem(point.Right);
            }

            return minItem;
        }


        public void PrintTree()
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }

            //очередь для хранения узлов и уровней
            Queue<(Point<T> Node, int Level)> queue = new Queue<(Point<T> Node, int Level)>();
            queue.Enqueue((root, 0));

            
            int currentLevel = 0;
            while (queue.Count > 0)
            {
                var (node, level) = queue.Dequeue();

                //пробелы 
                if (level > currentLevel)
                {
                    Console.WriteLine(); //новая строкаа
                    currentLevel = level;
                }

                
                Console.Write($"{node.Data} ");

                //добавление потомков в очередь с уровнем +1
                if (node.Left != null)
                    queue.Enqueue((node.Left, level + 1));
                if (node.Right != null)
                    queue.Enqueue((node.Right, level + 1));
            }

            
            Console.WriteLine();
            Console.WriteLine($"Количество узлов: {Count}");
        }



        public void AddPoint(T data)
        {
            if (root == null)
            {
                root = new Point<T>(data);
                count++;
                return;
            }

            Point<T> point = root;
            Point<T> curr = null;

            while (point != null)
            {
                curr = point;
                if (data.CompareTo(point.Data) < 0) //сравниваем для поиска места вставки
                    point = point.Left;
                else if (data.CompareTo(point.Data) > 0) //сравниваем для поиска места вставки
                    point = point.Right;
                else
                    return; //дубликаты не добавляем
            }

            Point<T> newPoint = new Point<T>(data);
            if (data.CompareTo(curr.Data) < 0)
                curr.Left = newPoint;
            else
                curr.Right = newPoint;

            count++;
        }

        public bool TransformToTree(Tree<T> originTree)
        {
            if (originTree.root == null)
                return false;

            List<T> sortedData = new List<T>();
            int curr = 0;

            //преобразуем дерево в отсортированный массив
            TransformToArr(originTree.root, sortedData, ref curr);

            //заново создаем дерево поиска
            root = null; //очищаем текущее дерево
            count = 0;

            foreach (T item in sortedData)
            {
                AddPoint(item);
            }

            return true;
        }

        public void TransformToArr(Point<T>? point, List<T> sortedData, ref int curr)
        {
            if (point != null)
            {
                TransformToArr(point.Left, sortedData, ref curr);
                sortedData.Add(point.Data);
                curr++;
                TransformToArr(point.Right, sortedData, ref curr);
            }
        }

        public void DeleteTreeFromMemory()
        {
            root = null;
            count = 0;
            Console.WriteLine("Дерево удалено из памяти.");
        }


        public bool DeleteTree()
        {
            if (root == null) return false;
            else
            {
                
                root = null;
                count = 0;
                return true;
            }
        }

        public Point<T> ClonePoint(Point<T>? point)
        {
            if (point == null)
                return null;
            else
            {
                Point<T> clonedPoint = new Point<T>((T)point.Data.Clone());
                clonedPoint.Left = ClonePoint(point.Left);
                clonedPoint.Right = ClonePoint(point.Right);
                return clonedPoint;
            }
        }

        public Tree<T> CloneTree(Tree<T> originTree)
        {
            if (originTree.Count != 0)
            {
                Tree<T> clonedTree = new Tree<T>(originTree.Count);
                clonedTree.root = ClonePoint(originTree.root);
                clonedTree.count = originTree.count;
                return clonedTree;
            }
            else
                return null;
        }


        public Point<T> FindPoint(Point<T> point, string name)
        {
            if (point == null)
                return null;

            else
            {
                if (point.Data is Musicalinstrument m)
                {
                    if (m.Name.Equals(name))
                        return point;
                    else
                    {
                        if (name.CompareTo(m.Name) < 0)
                            return FindPoint(point.Left, name);
                        if (name.CompareTo(m.Name) > 0)
                            return FindPoint(point.Right, name);
                    }
                }
            }
            return null;
        }

        public Point<T> FindItem(Tree<T> originTree, string name)
        {
            if (originTree.root == null)
                return null;

            return FindPoint(originTree.root, name);
        }

        public Point<T> RemovePoint(Point<T> point, T itemToDelete)
        {
            if (point == null)
                return null;

            else if (itemToDelete.CompareTo(point.Data) < 0) 
            {
                point.Left = RemovePoint(point.Left, itemToDelete);
                return point;
            }
            else if (itemToDelete.CompareTo(point.Data) > 0) 
            {
                point.Right = RemovePoint(point.Right, itemToDelete);
                return point;
            }
            else
            {
                if (point.Data.Equals(itemToDelete))
                {
                    if (point.Left == null && point.Right == null) 
                        return null; 
                    else if (point.Left == null) 
                        return point.Right; 
                    else if (point.Right == null) 
                        return point.Left; 
                    else
                    {
                       
                        T minItem = FindMinValue(point.Right); 
                        point.Data = minItem;
                        point.Right = RemovePoint(point.Right, minItem);
                        return point;
                    }
                }
            }
            return point;
        }


        public bool RemoveItem(Tree<T> originTree, T itemToDelete, string name)
        {
            if (FindItem(originTree, name) != null)
            {
                originTree.root = RemovePoint(originTree.root, itemToDelete);
                count--;
                return true;
            }
            else return false;
        }

        public T FindMinValue(Point<T> point)
        {
            T minValue = point.Data;
            while (point.Left != null)
            {
                minValue = point.Left.Data;
                point = point.Left;
                FindMinValue(point);
            }
            return minValue;
        }
    }
}
