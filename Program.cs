using System;
using System.Collections.Generic;

namespace KnapsackProblem
{
    class Program
    {
        // Leaving all code in Main for simplicity
        static void Main(string[] args)
        {
            List<List<Item>> tests = new List<List<Item>>
            {
                new List<Item> {
                    new Item { Weight = 4, Price = 18 },
                    new Item { Weight = 5, Price = 15 },
                    new Item { Weight = 3, Price = 5 },
                    new Item { Weight = 7, Price = 7 },
                    new Item { Weight = 1, Price = 6 },
                    new Item { Weight = 2, Price = 10 },
                    new Item { Weight = 1, Price = 3 },
                },
                new List<Item> {
                    new Item { Weight = 1, Price = 5 },
                    new Item { Weight = 3, Price = 10 },
                    new Item { Weight = 5, Price = 15 },
                    new Item { Weight = 4, Price = 7 },
                    new Item { Weight = 1, Price = 8 },
                    new Item { Weight = 3, Price = 9 },
                    new Item { Weight = 2, Price = 4 },
                }
            };

            List<Int32> knapsackCapacities = new List<Int32> { 14, 15 };

            Int32 index = 0;

            foreach (Int32 knapsackCapacity in knapsackCapacities)
            {
                List<Item> items = tests[index];

                var itemCount = items.Count;

                int[,] memoizations = new int[itemCount + 1, knapsackCapacity + 1];

                // Loop through all items
                for (int i = 0; i <= itemCount; i++)
                {
                    // Loop until knapsack is full
                    for (int j = 0; j <= knapsackCapacity; j++)
                    {
                        // first loop...memoize first sub problem as 0
                        if (i == 0 || j == 0)
                        {
                            memoizations[i, j] = 0;

                            continue;
                        }

                        Item item = items[i - 1];

                        // If the current item will fit in the knaspacks remaining space
                        if (item.Weight <= j)
                        {
                            // Combine current items value with previous max value of item
                            // accounting for current item weight
                            Int32 currentItemMaxValue = item.Price + memoizations[i - 1, j - item.Weight];

                            // Get the previous items value
                            Int32 previousItemValue = memoizations[i - 1, j];

                            // Take the maximum value from current item max and previous item max
                            memoizations[i, j] = Math.Max(currentItemMaxValue, previousItemValue);
                        }
                        else
                        {
                            memoizations[i, j] = memoizations[i - 1, j];
                        }
                    }
                }

                Console.WriteLine($"Knapsack Capacity: {knapsackCapacity}");

                Console.WriteLine($"Number of items: {itemCount}");

                Int32 maxValue = memoizations[itemCount, knapsackCapacity];

                Console.WriteLine($"Max value from items: {maxValue}");

                Console.WriteLine();

                index++;
            }

            Console.WriteLine("Press any key to exit...");

            Console.ReadLine();

            return;
        }
    }

    class Item
    {
        public Int32 Weight { get; set; }

        public Int32 Price { get; set; }
    }
}
