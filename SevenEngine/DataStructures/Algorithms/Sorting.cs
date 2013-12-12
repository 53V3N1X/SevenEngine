﻿// SEVENENGINE LISCENSE:
// You are free to use, modify, and distribute any or all code segments/files for any purpose
// including commercial use under the following condition: any code using or originally taken 
// from the SevenEngine project must include citation to its original author(s) located at the
// top of each source code file, or you may include a reference to the SevenEngine project as
// a whole but you must include the current SevenEngine official website URL and logo.
// - Thanks.  :)  (support: seven@sevenengine.com)

// Author(s):
// - Zachary Aaron Patten (aka Seven) seven@sevenengine.com
// Last Edited: 11-16-13

// This file contains the following classes:
// - Sorting

// This file contains space and stability values in the "remarks" xml tags of each method.
// Space values show how much space will be allocated during the algorithm.
// If a sorting algorithm is stable, it means that equal items will retain their relative order to each other.

using System;

namespace SevenEngine.DataStructures.Algotithms
{
  public static class Sorting
  {
    #region BubbleSort

    /*/// <summary>Sorts an entire array of type int in non-decreasing order using the bubble sort algorithm.</summary>
    /// <param name="array">the array to be sorted</param>
    /// <remarks>Runtime: Omega(n), average(n^2), O(n^2). Memory: in place. Stability: yes.</remarks>
    public static void BubbleSort(int[] array)
    {
      for (int i = 0; i < array.Length; i++)
        for (int j = 0; j < array.Length - 1; j++)
          if (array[j] > array[j + 1])
          {
            int temp = array[j + 1];
            array[j + 1] = array[j];
            array[j] = temp;
          }
    }*/

    /// <summary>Sorts an entire array of type IComparable in non-decreasing order using the bubble sort algorithm.</summary>
    /// <param name="array">the array to be sorted</param>
    /// <remarks>Runtime: Omega(n), average(n^2), O(n^2). Memory: in place. Stability: yes.</remarks>
    public static void BubbleSort(IComparable[] array)
    {
      for (int i = 0; i < array.Length; i++)
        for (int j = 0; j < array.Length - 1; j++)
          if (array[j].CompareTo(array[j + 1]) == 1)
          {
            IComparable temp = array[j + 1];
            array[j + 1] = array[j];
            array[j] = temp;
          }
    }

    /// <summary>Sorts an entire array of type IComparable in non-decreasing order using the bubble sort algorithm.</summary>
    /// <param name="array">the array to be sorted</param>
    /// <param name="comparisonFunction">The compare function (returns a positive value if left is greater than right).</param>
    /// <remarks>Runtime: Omega(n), average(n^2), O(n^2). Memory: in place. Stability: yes.</remarks>
    public static void BubbleSort<Type>(Type[] array, Func<Type, Type, int> comparisonFunction)
    {
      for (int i = 0; i < array.Length; i++)
        for (int j = 0; j < array.Length - 1; j++)
          if (comparisonFunction(array[j], array[j + 1]) > 0)
          {
            Type temp = array[j + 1];
            array[j + 1] = array[j];
            array[j] = temp;
          }
    }

    #endregion

    #region SelectionSort

    /*/// <summary>Sorts an entire array of type int in non-decreasing order using the selection sort algoritm.</summary>
    /// <param name="array">The array to be sorted</param>
    /// <remarks>Runtime: Omega(n^2), average(n^2), O(n^2). Memory: in place. Stablity: no.</remarks>
    public static void SelectionSort(int[] array)
    {
      for (int i = 0; i < array.Length; i++)
      {
        int min = i;
        for (int j = i + 1; j < array.Length; j++)
          if (array[j] < array[min])
          {
            min = j;
            int temp = array[i];
            array[i] = array[min];
            array[min] = temp;
          }
      }
    }*/

    /// <summary>Sorts an entire array of type IComparable in non-decreasing order 
    /// using the selection sort algoritm.
    /// Runtime: Omega(n^2), average(n^2), O(n^2).
    /// Memory: in place.
    /// Stablity: no.
    /// - Seven 03.06.2013
    /// </summary>
    /// <param name="array">the array to be sorted</param>
    public static void SelectionSort(IComparable[] array)
    {
      for (int i = 0; i < array.Length; i++)
      {
        int min = i;
        for (int j = i + 1; j < array.Length; j++)
          if (array[j].CompareTo(array[min]) == -1)
          {
            min = j;
            IComparable temp = array[i];
            array[i] = array[min];
            array[min] = temp;
          }
      }
    }

    /// <summary>
    /// Sorts an entire array of type IComparable in non-decreasing order using the selection sort algoritm.
    /// Runtime: Omega(n^2), average(n^2), O(n^2).
    /// Memory: in place.
    /// Stablity: no.
    /// - Seven 03.06.2013
    /// </summary>
    /// <param name="array">the array to be sorted</param>
    /// <param name="comparisonFunction">Returns negative if the left is less than the right.</param>
    public static void SelectionSort<Type>(Type[] array, Func<Type, Type, int> comparisonFunction)
    {
      for (int i = 0; i < array.Length; i++)
      {
        int min = i;
        for (int j = i + 1; j < array.Length; j++)
          if (comparisonFunction(array[j], array[min]) < 0)
          {
            min = j;
            Type temp = array[i];
            array[i] = array[min];
            array[min] = temp;
          }
      }
    }

    #endregion

    #region InsertionSort

    /*/// <summary>Sorts an entire array of type int in non-decreasing order using the insertion sort algorithm.</summary>
    /// <param name="array">the array to be sorted</param>
    /// <remarks>Runtime: Omega(n), average(n^2), O(n^2). Memory: in place. Stablity: yes.</remarks>
    public static void InsertionSort(int[] array)
    {
      for (int i = 1; i < array.Length; i++)
      {
        int temp = array[i];
        int j;
        for (j = i; j > 0 && array[j - 1] > temp; j--)
          array[j] = array[j - 1];
        array[j] = temp;
      }
    }*/

    /// <summary>Sorts an entire array of type IComparable in non-decreasing order using the insertion sort algorithm.</summary>
    /// <param name="array">the array to be sorted</param>
    /// <remarks>Runtime: Omega(n), average(n^2), O(n^2). Memory: in place. Stablity: yes.</remarks>
    public static void InsertionSort(IComparable[] array)
    {
      for (int i = 1; i < array.Length; i++)
      {
        IComparable temp = array[i];
        int j;
        for (j = i; j > 0 && array[j - 1].CompareTo(temp) == 1; j--)
          array[j] = array[j - 1];
        array[j] = temp;
      }
    }

    /// <summary>Sorts an entire array of type IComparable in non-decreasing order using the insertion sort algorithm.</summary>
    /// <param name="array">the array to be sorted</param>
    /// <param name="comparisonFunction">Returns positive if left greater than right.</param>
    /// <remarks>Runtime: Omega(n), average(n^2), O(n^2). Memory: in place. Stablity: yes.</remarks>
    public static void InsertionSort<Type>(Type[] array, Func<Type, Type, int> comparisonFunction)
    {
      for (int i = 1; i < array.Length; i++)
      {
        Type temp = array[i];
        int j;
        for (j = i; j > 0 && comparisonFunction(array[j - 1], temp) > 0; j--)
          array[j] = array[j - 1];
        array[j] = temp;
      }
    }

    #endregion

    #region QuickSort

    /*/// <summary>Sorts an entire array of type int in non-decreasing order using the quick sort algorithm.</summary>
    /// <param name="array">The array to be sorted.</param>
    /// <remarks>Runtime: Omega(n*ln(n)), average(n*ln(n)), O(n^2). Memory Space: ln(n). Stablity: no.</remarks>
    public static void QuickSort(int[] array) { QuickSort(array, 0, array.Length); }

    private static void QuickSort(int[] array, int start, int len)
    {
      if (len > 1)
      {
        int pivot = array[start];
        int i = start;
        int j = start + len - 1;
        int k = j;
        while (i <= j)
        {
          if (array[j] < pivot)
          {
            int temp = array[i];
            array[i++] = array[j];
            array[j] = temp;
          }
          else if (array[j] == pivot)
            j--;
          else
          {
            int temp = array[k];
            array[k--] = array[j];
            array[j--] = temp;
          }
        }
        QuickSort(array, start, i - start);
        QuickSort(array, k + 1, start + len - (k + 1));
      }
    }*/

    /// <summary>Sorts an entire array of type IComparable in non-decreasing order using the quick sort algorithm.</summary>
    /// <param name="array">The array to be sorted</param>
    /// <remarks>Runtime: Omega(n*ln(n)), average(n*ln(n)), O(n^2). Memory: ln(n). Stablity: no.</remarks>
    public static void QuickSort(IComparable[] array) { QuickSort(array, 0, array.Length); }

    private static void QuickSort(IComparable[] array, int start, int len)
    {
      if (len > 1)
      {
        IComparable pivot = array[start];
        int i = start;
        int j = start + len - 1;
        int k = j;
        while (i <= j)
        {
          if (array[j].CompareTo(pivot) == -1)
          {
            IComparable temp = array[i];
            array[i++] = array[j];
            array[j] = temp;
          }
          else if (array[j] == pivot)
            j--;
          else
          {
            IComparable temp = array[k];
            array[k--] = array[j];
            array[j--] = temp;
          }
        }
        QuickSort(array, start, i - start);
        QuickSort(array, k + 1, start + len - (k + 1));
      }
    }

    /// <summary>Sorts an entire array of type IComparable in non-decreasing order using the quick sort algorithm.</summary>
    /// <param name="array">The array to be sorted</param>
    /// <param name="comparisonFunction">Returns negative if left is less than right, and zero if left equals right.</param>
    /// <remarks>Runtime: Omega(n*ln(n)), average(n*ln(n)), O(n^2). Memory: ln(n). Stablity: no.</remarks>
    public static void QuickSort<Type>(Type[] array, Func<Type, Type, int> comparisonFunction) { QuickSort<Type>(array, comparisonFunction, 0, array.Length); }

    private static void QuickSort<Type>(Type[] array, Func<Type, Type, int> comparisonFunction, int start, int len)
    {
      if (len > 1)
      {
        Type pivot = array[start];
        int i = start;
        int j = start + len - 1;
        int k = j;
        while (i <= j)
        {
          if (comparisonFunction(array[j], pivot) < 0)
          {
            Type temp = array[i];
            array[i++] = array[j];
            array[j] = temp;
          }
          else if (comparisonFunction(array[j], pivot) == 0)
            j--;
          else
          {
            Type temp = array[k];
            array[k--] = array[j];
            array[j--] = temp;
          }
        }
        QuickSort(array, comparisonFunction, start, i - start);
        QuickSort(array, comparisonFunction, k + 1, start + len - (k + 1));
      }
    }

    #endregion

    #region MergeSort

    /*/// <summary>Sorts up to an array of type int in non-decreasing order using the merge sort algorithm.</summary>
    /// <param name="array">The array to be sorted</param>
    /// <remarks>Runtime: Omega(n*ln(n)), average(n*ln(n)), O(n*ln(n)). Memory: n. Stablity: yes.</remarks>
    public static void MergeSort(int[] array)
    {
      MergeSort(array, 0, array.Length);
    }
    private static void MergeSort(int[] array, int start, int len)
    {
      if (len > 1)
      {
        int half = len / 2;
        MergeSort(array, start, half);
        MergeSort(array, start + half, len - half);
        int[] sorted = new int[len];
        int i = start;
        int j = start + half;
        int k = 0;
        while (i < start + half && j < start + len)
        {
          if (array[i] <= array[j])
            sorted[k++] = array[i++];
          else
            sorted[k++] = array[j++];
        }
        for (int h = 0; h < start + half - i; h++)
          sorted[k + h] = array[i + h];
        for (int h = 0; h < start + len - j; h++)
          sorted[k + h] = array[j + h];
        for (int h = 0; h < len; h++)
          array[start + h] = sorted[0 + h];
      }
    }*/

    /// <summary>Sorts up to an array of type IComparable in non-decreasing order using the merge sort algorithm.</summary>
    /// <param name="array">The array to be sorted</param>
    /// <remarks>Runtime: Omega(n*ln(n)), average(n*ln(n)), O(n*ln(n)). Memory: n. Stablity: yes.</remarks>
    public static void MergeSort(IComparable[] array)
    {
      MergeSort(array, 0, array.Length);
    }
    private static void MergeSort(IComparable[] array, int start, int len)
    {
      if (len > 1)
      {
        int half = len / 2;
        MergeSort(array, start, half);
        MergeSort(array, start + half, len - half);
        IComparable[] sorted = new IComparable[len];
        int i = start;
        int j = start + half;
        int k = 0;
        while (i < start + half && j < start + len)
        {
          if (array[i].CompareTo(array[j]) <= 0)
            sorted[k++] = array[i++];
          else
            sorted[k++] = array[j++];
        }
        for (int h = 0; h < start + half - i; h++)
          sorted[k + h] = array[i + h];
        for (int h = 0; h < start + len - j; h++)
          sorted[k + h] = array[j + h];
        for (int h = 0; h < len; h++)
          array[start + h] = sorted[0 + h];
      }
    }

    /// <summary>Sorts up to an array of type IComparable in non-decreasing order using the merge sort algorithm.</summary>
    /// <param name="array">The array to be sorted</param>
    /// <param name="comparisonFunction">Returns zero or negative if the left is less than or equal to the right.</param>
    /// <remarks>Runtime: Omega(n*ln(n)), average(n*ln(n)), O(n*ln(n)). Memory: n. Stablity: yes.</remarks>
    public static void MergeSort<Type>(Type[] array, Func<Type, Type, int> comparisonFunction)
    {
      MergeSort<Type>(array, comparisonFunction, 0, array.Length);
    }
    private static void MergeSort<Type>(Type[] array, Func<Type, Type, int> comparisonFunction, int start, int len)
    {
      if (len > 1)
      {
        int half = len / 2;
        MergeSort<Type>(array,comparisonFunction, start, half);
        MergeSort<Type>(array, comparisonFunction, start + half, len - half);
        Type[] sorted = new Type[len];
        int i = start;
        int j = start + half;
        int k = 0;
        while (i < start + half && j < start + len)
        {
          if (comparisonFunction(array[i], array[j]) <= 0)
            sorted[k++] = array[i++];
          else
            sorted[k++] = array[j++];
        }
        for (int h = 0; h < start + half - i; h++)
          sorted[k + h] = array[i + h];
        for (int h = 0; h < start + len - j; h++)
          sorted[k + h] = array[j + h];
        for (int h = 0; h < len; h++)
          array[start + h] = sorted[0 + h];
      }
    }

    #endregion

    #region HeapSort

    /*/// <summary>Sorts an entire array of type int in non-decreasing order using the heap sort algorithm.</summary>
    /// <param name="array">The array to be sorted</param>
    /// <remarks>Runtime: Omega(n*ln(n)), average(n*ln(n)), O(n^2). Memory: in place. Stablity: no.</remarks>
    public static void HeapSort(int[] array)
    {
      int heapSize = array.Length;
      for (int i = (heapSize - 1) / 2; i >= 0; i--)
        MaxHeapify(array, heapSize, i);
      for (int i = array.Length - 1; i > 0; i--)
      {
        int temp = array[0];
        array[0] = array[i];
        array[i] = temp;
        heapSize--;
        MaxHeapify(array, heapSize, 0);
      }
    }

    private static void MaxHeapify(int[] array, int heapSize, int index)
    {
      int left = (index + 1) * 2 - 1;
      int right = (index + 1) * 2;
      int largest = 0;
      if (left < heapSize && array[left] > array[index])
        largest = left;
      else
        largest = index;
      if (right < heapSize && array[right] > array[largest])
        largest = right;
      if (largest != index)
      {
        int temp = array[index];
        array[index] = array[largest];
        array[largest] = temp;
        MaxHeapify(array, heapSize, largest);
      }
    }*/

    /// <summary>Sorts an entire array of type IComparable in non-decreasing order using the heap sort algorithm.</summary>
    /// <param name="array">The array to be sorted</param>
    /// <remarks>Runtime: Omega(n*ln(n)), average(n*ln(n)), O(n^2). Memory: in place. Stablity: no.</remarks>
    public static void HeapSort(IComparable[] array)
    {
      int heapSize = array.Length;
      for (int i = (heapSize - 1) / 2; i >= 0; i--)
        MaxHeapify(array, heapSize, i);
      for (int i = array.Length - 1; i > 0; i--)
      {
        IComparable temp = array[0];
        array[0] = array[i];
        array[i] = temp;
        heapSize--;
        MaxHeapify(array, heapSize, 0);
      }
    }

    private static void MaxHeapify(IComparable[] array, int heapSize, int index)
    {
      int left = (index + 1) * 2 - 1;
      int right = (index + 1) * 2;
      int largest = 0;
      if (left < heapSize && array[left].CompareTo(array[index]) == 1)
        largest = left;
      else
        largest = index;
      if (right < heapSize && array[right].CompareTo(array[largest]) == 1)
        largest = right;
      if (largest != index)
      {
        IComparable temp = array[index];
        array[index] = array[largest];
        array[largest] = temp;
        MaxHeapify(array, heapSize, largest);
      }
    }

    /// <summary>Sorts an entire array of type IComparable in non-decreasing order using the heap sort algorithm.</summary>
    /// <param name="array">The array to be sorted</param>
    /// <remarks>Runtime: Omega(n*ln(n)), average(n*ln(n)), O(n^2). Memory: in place. Stablity: no.</remarks>
    public static void HeapSort<Type>(Type[] array, Func<Type, Type, int> comparisonFunction)
    {
      int heapSize = array.Length;
      for (int i = (heapSize - 1) / 2; i >= 0; i--)
        MaxHeapify(array, comparisonFunction, heapSize, i);
      for (int i = array.Length - 1; i > 0; i--)
      {
        Type temp = array[0];
        array[0] = array[i];
        array[i] = temp;
        heapSize--;
        MaxHeapify(array, comparisonFunction, heapSize, 0);
      }
    }

    private static void MaxHeapify<Type>(Type[] array, Func<Type, Type, int> comparisonFunction, int heapSize, int index)
    {
      int left = (index + 1) * 2 - 1;
      int right = (index + 1) * 2;
      int largest = 0;
      if (left < heapSize && comparisonFunction(array[left], array[index]) > 0)
        largest = left;
      else
        largest = index;
      if (right < heapSize && comparisonFunction(array[right], array[largest]) > 0)
        largest = right;
      if (largest != index)
      {
        Type temp = array[index];
        array[index] = array[largest];
        array[largest] = temp;
        MaxHeapify(array, comparisonFunction, heapSize, largest);
      }
    }

    #endregion
  }
}