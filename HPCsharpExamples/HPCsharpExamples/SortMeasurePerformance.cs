﻿using HPCsharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HPCsharpExamples
{
    partial class Program
    {
        public static void SortMeasureArraySpeedup()
        {
            Random randNum = new Random(5);
            int arraySize = 16 * 1024 * 1024;
            uint[] benchArrayOne = new uint[arraySize];
            uint[] benchArrayTwo = new uint[arraySize];
            uint[] sortedArrayOne = new uint[arraySize];

            for (int i = 0; i < arraySize; i++)
            {
                benchArrayOne[i] = (uint)randNum.Next(0, Int32.MaxValue);    // fill array with random value between min and max
                //benchArrayOne[i] = (uint)i;
                //benchArrayOne[i] = 0;
                benchArrayTwo[i] = benchArrayOne[i];
            }

            Stopwatch stopwatch = new Stopwatch();
            long frequency = Stopwatch.Frequency;
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;

            stopwatch.Restart();
            //uint[] sortedArrayOne = benchArrayOne.RadixSortLSD();
            benchArrayOne.SortMergeInPlace();
            stopwatch.Stop();
            double timeMergeSort = stopwatch.ElapsedTicks * nanosecPerTick / 1000000000.0;
            stopwatch.Restart();
            Array.Sort(benchArrayTwo);
            stopwatch.Stop();
            double timeArraySort = stopwatch.ElapsedTicks * nanosecPerTick / 1000000000.0;

            bool equalSortedArrays = benchArrayOne.SequenceEqual(benchArrayTwo);
            if (equalSortedArrays)
                Console.WriteLine("Sorting results are equal");
            else
                Console.WriteLine("Sorting results did not compare!");

            Console.WriteLine("C# array of size {0}: Array.Sort {1:0.000} sec, SortMerge {2:0.000} sec, speedup {3:0.00}", arraySize,
                               timeArraySort, timeMergeSort, timeArraySort / timeMergeSort);
        }

        public static void SortMeasureListSpeedup()
        {
            Stopwatch stopwatch = new Stopwatch();
            Random randNum = new Random(2);
            int ListSize = 16 * 1024 * 1024;
            List<uint> benchListOne = new List<uint>(ListSize);
            List<uint> benchListTwo = new List<uint>(ListSize);

            for (int i = 0; i < ListSize; i++)
            {
                benchListOne.Add((uint)randNum.Next(0, Int32.MaxValue));    // fill lists with random value between min and max
                //benchListOne.Add((uint)i);
                //benchListOne.Add(0);
                benchListTwo.Add(benchListOne[i]);
            }

            long frequency = Stopwatch.Frequency;
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;

            stopwatch.Restart();
            List<uint> sortedArrayOne = benchListOne.SortRadix();
            stopwatch.Stop();
            double timeRadixSort = stopwatch.ElapsedTicks * nanosecPerTick / 1000000000.0;
            stopwatch.Restart();
            benchListTwo.Sort();
            stopwatch.Stop();
            double timeListSort = stopwatch.ElapsedTicks * nanosecPerTick / 1000000000.0;

            bool equalSortedArrays = sortedArrayOne.SequenceEqual(benchListTwo);
            if (equalSortedArrays)
                Console.WriteLine("Sorting results are equal");
            else
                Console.WriteLine("Sorting results did not compare!");

            Console.WriteLine("C# List of size {0}: List.Sort {1:0.000} sec, SortRadix {2:0.000} sec, speedup {3:0.00}", ListSize,
                               timeListSort, timeRadixSort, timeListSort / timeRadixSort);
        }
    }
}

