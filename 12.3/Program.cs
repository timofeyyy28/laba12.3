using ClassLibraryLabor10;
using System;

namespace _12._3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tree<Musicalinstrument> balancedTree = new Tree<Musicalinstrument>();
            Tree<Musicalinstrument> searchTree = new Tree<Musicalinstrument>();
            Tree<Musicalinstrument> clonedTree = null;

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Создать идеально сбалансированное дерево");
                Console.WriteLine("2. Распечатать идеально сбалансированное дерево");
                Console.WriteLine("3. Найти минимальный элемент в ИСД");
                Console.WriteLine("4. Преобразовать ИСД в дерево поиска");
                Console.WriteLine("5. Распечатать дерево поиска");
                Console.WriteLine("6. Удалить дерево поиска");
                Console.WriteLine("7. Клонировать дерево поиска");
                Console.WriteLine("8. Распечатать клонированное дерево");
                Console.WriteLine("9. Удалить элемент из дерева поиска");
                Console.WriteLine("10. Удаление дерева из памяти");
                Console.WriteLine("0. Выход");

                Console.Write("\nВаш выбор: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        balancedTree = CreateBalancedTree();
                        break;
                    case "2":
                        PrintTree(balancedTree);
                        break;
                    case "3":
                        FindAndPrintMinItem(balancedTree);
                        break;
                    case "4":
                        ConvertAndPrintResult(balancedTree, searchTree);
                        break;
                    case "5":
                        PrintTree(searchTree);
                        break;
                    case "6":
                        DeleteSearchTree(searchTree);
                        break;
                    case "7":
                        clonedTree = CloneSearchTree(searchTree);
                        break;
                    case "8":
                        PrintClonedTree(clonedTree);
                        break;
                    case "9":
                        RemoveElementFromSearchTree(searchTree);
                        break;
                    case "10":
                        balancedTree.DeleteTreeFromMemory();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, введите корректное число.");
                        break;
                }
            }
        }

        static Tree<Musicalinstrument> CreateBalancedTree()
        {
            Console.Write("Введите количество элементов для ИСД: ");
            if (int.TryParse(Console.ReadLine(), out int size) && size > 0)
            {
                Tree<Musicalinstrument> tree = new Tree<Musicalinstrument>(size);
                Console.WriteLine("Идеально сбалансированное дерево создано.");
                return tree;
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Дерево не создано.");
                return new Tree<Musicalinstrument>();
            }
        }

        static void PrintTree(Tree<Musicalinstrument> tree)
        {
            try
            {
                tree.PrintTree();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void FindAndPrintMinItem(Tree<Musicalinstrument> tree)
        {
            try
            {
                var minItem = tree.SearchItem();
                Console.WriteLine($"Минимальный элемент: {minItem}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ConvertAndPrintResult(Tree<Musicalinstrument> sourceTree, Tree<Musicalinstrument> targetTree)
        {
            if (targetTree.TransformToTree(sourceTree))
                Console.WriteLine("ИСД успешно преобразовано в дерево поиска.");
            else
                Console.WriteLine("Преобразование не удалось. Исходное дерево пустое.");
        }

        static void DeleteSearchTree(Tree<Musicalinstrument> tree)
        {
            if (tree.DeleteTree())
                Console.WriteLine("Дерево поиска удалено.");
            else
                Console.WriteLine("Не удалось удалить дерево поиска.");
        }

        static Tree<Musicalinstrument> CloneSearchTree(Tree<Musicalinstrument> tree)
        {
            if (tree.Count == 0)
            {
                Console.WriteLine("Исходное дерево пустое. Клонирование невозможно.");
                return null;
            }
            else
            {
                var cloned = tree.CloneTree(tree);
                Console.WriteLine("Дерево поиска успешно клонировано.");
                return cloned;
            }
        }

        static void PrintClonedTree(Tree<Musicalinstrument> tree)
        {
            if (tree == null)
            {
                Console.WriteLine("Клонированное дерево отсутствует.");
            }
            else
            {
                Console.WriteLine("Отображение клонированного дерева:");
                tree.PrintTree();

                Console.WriteLine("\nДобавление нового элемента в клонированное дерево...");
                tree.AddPoint(new Musicalinstrument("Новый инструмент", 999));
                tree.PrintTree();
            }
        }

        static void RemoveElementFromSearchTree(Tree<Musicalinstrument> tree)
        {
            Console.Write("Введите название инструмента для удаления: ");
            string name = Console.ReadLine();

            var point = tree.FindItem(tree, name);
            if (point != null)
            {
                if (tree.RemoveItem(tree, point.Data, name))
                    Console.WriteLine($"Элемент \"{name}\" успешно удален из дерева.");
                else
                    Console.WriteLine($"Не удалось удалить элемент \"{name}\".");
            }
            else
            {
                Console.WriteLine($"Элемент с названием \"{name}\" не найден в дереве.");
            }
        }
    }
}
