﻿// SEVENENGINE LISCENSE:
// You are free to use, modify, and distribute any or all code segments/files for any purpose
// including commercial use under the following condition: any code using or originally taken 
// from the SevenEngine project must include citation to its original author(s) located at the
// top of each source code file, or you may include a reference to the SevenEngine project as
// a whole but you must include the current SevenEngine official website URL and logo.
// - Thanks.  :)  (support: seven@sevenengine.com)

// Author(s):
// - Zachary Aaron Patten (aka Seven) seven@sevenengine.com
// Special Thanks:
// - AVL Trees were originally invented by G. M. Adelson-Velskii and E. M. Landis in 1962
// Last Edited: 11-16-13

// This file contains the following interfaces:
// - AvlTree
// This file contains the following class:
// - AvlTreeLinked
//   - AvlTreeLinkedNode
//   - AvlTreeLinkedException

using System;
using System.Threading;
using SevenEngine.DataStructures.Interfaces;

namespace SevenEngine.DataStructures
{
  public interface AvlTree<ValueType, KeyType> : InterfaceTraversable<ValueType>
  {
    int Count { get; }
    bool IsEmpty { get; }
    ValueType Get(KeyType get);
    bool TryGet(KeyType get, out ValueType returnValue);
    bool Contains(KeyType containsCheck);
    void Add(ValueType addition);
    void Remove(KeyType removalKey);
    ValueType[] ToArray();
  }

  #region AvlTreeRecursive

  /// <summary>Implements an AVL Tree where the items are sorted by string id values.</summary>
  /// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
  public class AvlTreeLinked<ValueType, KeyType> : AvlTree<ValueType, KeyType>
  {
    #region AvlTreeNodeRecursive

    /// <summary>This class just holds the data for each individual node of the tree.</summary>
    private class AvlTreeLinkedNode
    {
      private ValueType _value;
      private AvlTreeLinkedNode _leftChild;
      private AvlTreeLinkedNode _rightChild;
      private int _height;

      internal ValueType Value { get { return _value; } set { _value = value; } }
      internal AvlTreeLinkedNode LeftChild { get { return _leftChild; } set { _leftChild = value; } }
      internal AvlTreeLinkedNode RightChild { get { return _rightChild; } set { _rightChild = value; } }
      internal int Height { get { return _height; } set { _height = value; } }

      internal AvlTreeLinkedNode(ValueType value)
      {
        _value = value;
        _leftChild = null;
        _rightChild = null;
        _height = 0;
      }
    }

    #endregion

    private AvlTreeLinkedNode _avlTree;
    private int _count;

    private Func<ValueType, ValueType, int> _valueComparisonFunction;
    private Func<ValueType, KeyType, int> _keyComparisonFunction;
    
    private Object _lock;
    private int _readers;
    private int _writers;

    /// <summary>Gets the number of elements in the collection.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public int Count { get { ReaderLock(); int count = _count; ReaderUnlock(); return _count; } }
    /// <summary>Gets whether the binary search tree is empty.</summary>
    /// <remarks>Runtime: O(1).</remarks>
    public bool IsEmpty { get { ReaderLock(); bool isEmpty = _avlTree == null; ReaderUnlock(); return isEmpty; } }

    /// <summary>Constructs an AVL Tree.</summary>
    /// <param name="valueComparisonFunction">A function that returns negative if left is less that right, 
    /// zero if they are equal, or positive if left is greater than right.</param>
    /// <param name="keyComparisonFunction">A function that returns negative if left is less that right, 
    /// zero if they are equal, or positive if left is greater than right.</param>
    /// <remarks>Runtime: O(1).</remarks>
    public AvlTreeLinked(
      Func<ValueType, ValueType, int> valueComparisonFunction,
      Func<ValueType, KeyType, int> keyComparisonFunction)
    {
      _avlTree = null;
      _count = 0;
      _lock = new Object();
      _readers = 0;
      _writers = 0;
      _valueComparisonFunction = valueComparisonFunction;
      _keyComparisonFunction = keyComparisonFunction;
    }

    /// <summary>Constructs an AVL Tree. This default constructor requires the generic value type 
    /// of this class to follow the IComparable and IComparable<!--<-->KeyType<!-->--> interfaces.</summary>
    public AvlTreeLinked()
    {
      ValueType typeCheck = default(ValueType);
      if (!(typeCheck is IComparable))
        throw new AvlTreeLinkedException("using the no-parameter AvlTree constructor requires you to use IComparable generic types.");
      if (!(typeCheck is IComparable<KeyType>))
        throw new AvlTreeLinkedException("using the no-parameter AvlTree constructor requires you to use IComparable<!--<--!>KeyType<!-->--!> generic types.");
      _valueComparisonFunction = (ValueType left, ValueType right) => { return ((IComparable)left).CompareTo(right); };
      _keyComparisonFunction = (ValueType left, KeyType right) => { return ((IComparable<KeyType>)left).CompareTo(right); };
      _avlTree = null;
      _count = 0;
      _lock = new Object();
      _readers = 0;
      _writers = 0;
    }

    #region Recursive Versions

    // THE FOLLOWING FUNCTIONS ARE RECURSIVE VERSIONS OF THE EXISTING 
    // MEMBERS WITHIN THIS CLASS. THEY HAVE THE SAME FUNCTIONALITY.

    //public Type Get(string id)
    //{
    //  ReaderLock();
    //   Type returnValue = Get(id, _avlTree);
    //   ReaderUnlock();
    //   return returnValue;
    //}

    //private Type Get(string id, AvlTreeNode avlTree)
    //{
    //  if (avlTree == null)
    //    throw new AvlTreeException("Attempting to get a non-existing value: " + id + ".");
    //  int compResult = id.CompareTo(avlTree.Value.Id);
    //  if (compResult == 0)
    //    return avlTree.Value;
    //  else if (compResult < 0)
    //    return Get(id, avlTree.LeftChild);
    //  else
    //    return Get(id, avlTree.RightChild);
    //}

    //public bool Contains(string id)
    //{
    //   ReaderLock();
    //   bool returnValue = Contains(id, _avlTree);
    //   ReaderUnlock();
    //   return returnValue;
    //}

    //private bool Contains(string id, AvlTreeNode avlTree)
    //{
    //  if (avlTree == null) return false;
    //  int compareResult = id.CompareTo(avlTree.Value.Id);
    //  if (compareResult == 0) return true;
    //  else if (compareResult < 0) return Contains(id, avlTree.LeftChild);
    //  else return Contains(id, avlTree.RightChild);
    //}

    #endregion

    /// <summary>Gets the item with the designated by the string.</summary>
    /// <param name="id">The string ID to look for.</param>
    /// <returns>The object with the desired string ID if it exists.</returns>
    /// <remarks>Runtime: O(ln(n)), Omega(1).</remarks>
    public ValueType Get(KeyType get)
    {
      // THIS THIS THE ITERATIVE VERSION OF THIS FUNCTION. THERE IS A RECURSIVE
      // VERSION IN THE "RECURSIVE" REGION SHOULD YOU WISH TO SEE IT.
      ReaderLock();
      AvlTreeLinkedNode _current = _avlTree;
      while (_current != null)
      {
        int compareResult = _keyComparisonFunction(_current.Value, get);
        if (compareResult == 0)
        {
          ReaderUnlock();
          return _current.Value;
        }
        else if (compareResult > 0) _current = _current.LeftChild;
        else _current = _current.RightChild;
      }
      ReaderUnlock();
      throw new AvlTreeLinkedException("Attempting to get a non-existing value: " + get.ToString() + ".");
    }

    /// <summary>Does a standard get but returns whether or not the value exists (versus throwing an exception).</summary>
    /// <param name="get">The key value to get.</param>
    /// <param name="returnValue">The value cooresponding to the key value if it exists.</param>
    /// <returns>True if the requested key is found.</returns>
    public bool TryGet(KeyType get, out ValueType returnValue)
    {
      ReaderLock();
      AvlTreeLinkedNode _current = _avlTree;
      while (_current != null)
      {
        int compareResult = _keyComparisonFunction(_current.Value, get);
        if (compareResult == 0)
        {
          returnValue = _current.Value;
          ReaderUnlock();
          return true;
        }
        else if (compareResult > 0) _current = _current.LeftChild;
        else _current = _current.RightChild;
      }
      ReaderUnlock();
      returnValue = default(ValueType);
      return false;
    }

    /// <summary>Checks to see if the tree contains a specific key.</summary>
    /// <param name="id">The id to check for existance.</param>
    /// <returns>"true" if the key exists; "false" if the key does not exist.</returns>
    /// <remarks>Runtime: O(ln(n)).</remarks>
    public bool Contains(KeyType getCheck)
    {
      // THIS THIS THE ITERATIVE VERSION OF THIS FUNCTION. THERE IS A RECURSIVE
      // VERSION IN THE "RECURSIVE" REGION SHOULD YOU WISH TO SEE IT.
      ReaderLock();
      AvlTreeLinkedNode _current = _avlTree;
      while (_current != null)
      {
        int compareResult = _keyComparisonFunction(_current.Value, getCheck);
        if (compareResult == 0)
        {
          ReaderUnlock();
          return true;
        }
        else if (compareResult > 0) _current = _current.LeftChild;
        else _current = _current.RightChild;
      }
      ReaderUnlock();
      return false;
    }

    /// <summary>Adds an object to the AVL Tree.</summary>
    /// <param name="addition">The object to add.</param>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    public void Add(ValueType addition)
    {
      WriterLock();
      _avlTree = Add(addition, _avlTree);
      _count++;
      WriterUnlock();
    }

    private AvlTreeLinkedNode Add(ValueType addition, AvlTreeLinkedNode avlTree)
    {
      if (avlTree == null) return new AvlTreeLinkedNode(addition);
      int compareResult = _valueComparisonFunction(avlTree.Value, addition);
      if (compareResult == 0)
        throw new AvlTreeLinkedException("Attempting to add an already existing id exists.");
      else if (compareResult > 0) avlTree.LeftChild = Add(addition, avlTree.LeftChild);
      else avlTree.RightChild = Add(addition, avlTree.RightChild);
      return Balance(avlTree);
    }

    /// <summary>Removes an object from the AVL Tree.</summary>
    /// <param name="removal">The string ID of the object to remove.</param>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    public void Remove(KeyType removal)
    {
      WriterLock();
      _avlTree = Remove(removal, _avlTree);
      _count--;
      WriterUnlock();
    }

    /// <summary>Removes an object from the AVL Tree.</summary>
    /// <param name="removal">The string ID of the object to remove.</param>
    /// <param name="avlTree">The binary tree to remove from.</param>
    /// <returns>The resulting tree after the removal.</returns>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    private AvlTreeLinkedNode Remove(KeyType removal, AvlTreeLinkedNode avlTree)
    {
      if (avlTree != null)
      {
        int compareResult = _keyComparisonFunction(avlTree.Value, removal);
        if (compareResult == 0)
        {
          if (avlTree.RightChild != null)
          {
            AvlTreeLinkedNode leftMostOfRight;
            avlTree.RightChild = RemoveLeftMost(avlTree.RightChild, out leftMostOfRight);
            leftMostOfRight.RightChild = avlTree.RightChild;
            leftMostOfRight.LeftChild = avlTree.LeftChild;
            avlTree = leftMostOfRight;
          }
          else if (avlTree.LeftChild != null)
          {
            AvlTreeLinkedNode rightMostOfLeft;
            avlTree.LeftChild = RemoveRightMost(avlTree.LeftChild, out rightMostOfLeft);
            rightMostOfLeft.RightChild = avlTree.RightChild;
            rightMostOfLeft.LeftChild = avlTree.LeftChild;
            avlTree = rightMostOfLeft;
          }
          else return null;
          SetHeight(avlTree);
          return Balance(avlTree);
        }
        else if (compareResult > 0) avlTree.LeftChild = Remove(removal, avlTree.LeftChild);
        else avlTree.RightChild = Remove(removal, avlTree.RightChild);
        SetHeight(avlTree);
        return Balance(avlTree);
      }
      WriterUnlock();
      throw new AvlTreeLinkedException("Attempting to remove a non-existing entry.");
    }

    /// <summary>Removes the left-most child of an AVL Tree node and returns it 
    /// through the out parameter.</summary>
    /// <param name="avlTree">The tree to remove the left-most child from.</param>
    /// <param name="leftMost">The left-most child of this AVL tree.</param>
    /// <returns>The updated tree with the removal.</returns>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    private AvlTreeLinkedNode RemoveLeftMost(AvlTreeLinkedNode avlTree, out AvlTreeLinkedNode leftMost)
    {
      if (avlTree.LeftChild == null) { leftMost = avlTree; return null; }
      avlTree.LeftChild = RemoveLeftMost(avlTree.LeftChild, out leftMost);
      SetHeight(avlTree);
      return Balance(avlTree);
    }

    /// <summary>Removes the right-most child of an AVL Tree node and returns it 
    /// through the out parameter.</summary>
    /// <param name="avlTree">The tree to remove the right-most child from.</param>
    /// <param name="leftMost">The right-most child of this AVL tree.</param>
    /// <returns>The updated tree with the removal.</returns>
    /// <remarks>Runtime: Theta(ln(n)).</remarks>
    private AvlTreeLinkedNode RemoveRightMost(AvlTreeLinkedNode avlTree, out AvlTreeLinkedNode rightMost)
    {
      if (avlTree.RightChild == null) { rightMost = avlTree; return null; }
      avlTree.LeftChild = RemoveLeftMost(avlTree.RightChild, out rightMost);
      SetHeight(avlTree);
      return Balance(avlTree);
    }

    /// <summary>This is just a protection against the null valued leaf nodes, 
    /// which have a height of "-1".</summary>
    /// <param name="avlTree">The AVL Tree to find the hight of.</param>
    /// <returns>Returns "-1" if null (leaf) or the height property of the node.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    private int Height(AvlTreeLinkedNode avlTree)
    {
      if (avlTree == null) return -1;
      else return avlTree.Height;
    }

    /// <summary>Sets the height of a tree based on its children's heights.</summary>
    /// <param name="avlTree">The tree to have its height adjusted.</param>
    /// <remarks>Runtime: O(1).</remarks>
    private void SetHeight(AvlTreeLinkedNode avlTree)
    {
      if (Height(avlTree.LeftChild) < Height(avlTree.RightChild))
        avlTree.Height = Math.Max(Height(avlTree.LeftChild), Height(avlTree.RightChild)) + 1;
    }

    /// <summary>Standard balancing algorithm for an AVL Tree.</summary>
    /// <param name="avlTree">The tree to check the balancing of.</param>
    /// <returns>The result of the possible balancing.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    private AvlTreeLinkedNode Balance(AvlTreeLinkedNode avlTree)
    {
      if (Height(avlTree.LeftChild) == Height(avlTree.RightChild) + 2)
      {
        if (Height(avlTree.LeftChild.LeftChild) > Height(avlTree.RightChild))
          return SingleRotateRight(avlTree);
        else return DoubleRotateRight(avlTree);
      }
      else if (Height(avlTree.RightChild) == Height(avlTree.LeftChild) + 2)
      {
        if (Height(avlTree.RightChild.RightChild) > Height(avlTree.LeftChild))
          return SingleRotateLeft(avlTree);
        else return DoubleRotateLeft(avlTree);
      }
      SetHeight(avlTree);
      return avlTree;
    }

    /// <summary>Standard single rotation (to the right) algorithm for an AVL Tree.</summary>
    /// <param name="avlTree">The tree to single rotate right.</param>
    /// <returns>The resulting tree.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    private AvlTreeLinkedNode SingleRotateRight(AvlTreeLinkedNode avlTree)
    {
      AvlTreeLinkedNode temp = avlTree.LeftChild;
      avlTree.LeftChild = temp.RightChild;
      temp.RightChild = avlTree;
      SetHeight(avlTree);
      SetHeight(temp);
      return temp;
    }

    /// <summary>Standard single rotation (to the left) algorithm for an AVL Tree.</summary>
    /// <param name="avlTree">The tree to single rotate left.</param>
    /// <returns>The resulting tree.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    private AvlTreeLinkedNode SingleRotateLeft(AvlTreeLinkedNode avlTree)
    {
      AvlTreeLinkedNode temp = avlTree.RightChild;
      avlTree.RightChild = temp.LeftChild;
      temp.LeftChild = avlTree;
      SetHeight(avlTree);
      SetHeight(temp);
      return temp;
    }

    /// <summary>Standard double rotation (to the right) algorithm for an AVL Tree.</summary>
    /// <param name="avlTree">The tree to float rotate right.</param>
    /// <returns>The resulting tree.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    private AvlTreeLinkedNode DoubleRotateRight(AvlTreeLinkedNode avlTree)
    {
      AvlTreeLinkedNode temp = avlTree.LeftChild.RightChild;
      avlTree.LeftChild.RightChild = temp.LeftChild;
      temp.LeftChild = avlTree.LeftChild;
      avlTree.LeftChild = temp.RightChild;
      temp.RightChild = avlTree;
      SetHeight(temp.LeftChild);
      SetHeight(temp.RightChild);
      SetHeight(temp);
      return temp;
    }

    /// <summary>Standard double rotation (to the left) algorithm for an AVL Tree.</summary>
    /// <param name="avlTree">The tree to float rotate left.</param>
    /// <returns>The resulting tree.</returns>
    /// <remarks>Runtime: O(1).</remarks>
    private AvlTreeLinkedNode DoubleRotateLeft(AvlTreeLinkedNode avlTree)
    {
      AvlTreeLinkedNode temp = avlTree.RightChild.LeftChild;
      avlTree.RightChild.LeftChild = temp.RightChild;
      temp.RightChild = avlTree.RightChild;
      avlTree.RightChild = temp.LeftChild;
      temp.LeftChild = avlTree;
      SetHeight(temp.LeftChild);
      SetHeight(temp.RightChild);
      SetHeight(temp);
      return temp;
    }

    /// <summary>Returns the tree to an iterative state.</summary>
    public void Clear() { WriterLock(); _avlTree = null; _count = 0; WriterUnlock(); }

    /// <summary>Performs a functional paradigm in-order traversal of the AVL tree.</summary>
    /// <param name="traversalFunction">The function to perform during iteration.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public bool TraverseBreakable(Func<ValueType, bool> traversalFunction) { return TraversalInOrder(traversalFunction); }

    /// <summary>Performs a functional paradigm in-order traversal of the AVL tree.</summary>
    /// <param name="traversalAction">The action to perform during iteration.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public void Traverse(Action<ValueType> traversalAction) { TraversalInOrder(traversalAction); }

    /// <summary>Performs a functional paradigm in-order traversal of the AVL tree.</summary>
    /// <param name="traversalFunction">The function to perform during iteration.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public bool TraversalInOrder(Func<ValueType, bool> traversalFunction)
    {
      ReaderLock();
      if (!TraversalInOrder(traversalFunction, _avlTree))
      {
        ReaderUnlock();
        return false;
      }
      ReaderUnlock();
      return true;
    }
    private bool TraversalInOrder(Func<ValueType, bool> traversalFunction, AvlTreeLinkedNode avltreeNode)
    {
      if (avltreeNode != null)
      {
        if (!TraversalInOrder(traversalFunction, avltreeNode.LeftChild)) return false;
        if (!traversalFunction(avltreeNode.Value)) return false;
        if (!TraversalInOrder(traversalFunction, avltreeNode.RightChild)) return false;
      }
      return true;
    }

    public void TraversalInOrder(Action<ValueType> traversalFunction)
    {
      ReaderLock();
      TraversalInOrder(traversalFunction, _avlTree);
      ReaderUnlock();
    }
    private void TraversalInOrder(Action<ValueType> traversalFunction, AvlTreeLinkedNode avltreeNode)
    {
      if (avltreeNode != null)
      {
        TraversalInOrder(traversalFunction, avltreeNode.LeftChild);
        traversalFunction(avltreeNode.Value);
        TraversalInOrder(traversalFunction, avltreeNode.RightChild);
      }
    }

    /// <summary>Performs a functional paradigm post-order traversal of the AVL tree.</summary>
    /// <param name="traversalFunction">The function to perform during iteration.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public bool TraversalPostOrder(Func<ValueType, bool> traversalFunction)
    {
      ReaderLock();
      if (TraversalPostOrder(traversalFunction, _avlTree))
      {
        ReaderUnlock();
        return false;
      }
      ReaderUnlock();
      return true;
    }
    private bool TraversalPostOrder(Func<ValueType, bool> traversalFunction, AvlTreeLinkedNode avltreeNode)
    {
      if (avltreeNode != null)
      {
        if (!TraversalPostOrder(traversalFunction, avltreeNode.RightChild)) return false;
        if (!traversalFunction(avltreeNode.Value)) return false;
        if (!TraversalPostOrder(traversalFunction, avltreeNode.LeftChild)) return false;
      }
      return true;
    }


    /// <summary>Performs a functional paradigm pre-order traversal of the AVL tree.</summary>
    /// <param name="traversalFunction">The function to perform during iteration.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public bool TraversalPreOrder(Func<ValueType, bool> traversalFunction)
    {
      ReaderLock();
      if (!TraversalPreOrder(traversalFunction, _avlTree))
      {
        ReaderUnlock();
        return false;
      }
      ReaderUnlock();
      return true;
    }
    private bool TraversalPreOrder(Func<ValueType, bool> traversalFunction, AvlTreeLinkedNode avltreeNode)
    {
      if (avltreeNode != null)
      {
        if (!traversalFunction(avltreeNode.Value)) return false;
        if (!TraversalPreOrder(traversalFunction, avltreeNode.LeftChild)) return false;
        if (!TraversalPreOrder(traversalFunction, avltreeNode.RightChild)) return false;
      }
      return true;
    }

    /// <summary>Performs a functional paradigm in-order traversal of the AVL tree.</summary>
    /// <param name="traversalFunction">The function to perform during iteration.</param>
    /// <remarks>Runtime: O(n * traversalFunction).</remarks>
    public bool TraversalInOrder(Func<ValueType, bool> traversalFunction, KeyType minimum, KeyType maximum)
    {
      ReaderLock();
      if (!TraversalInOrder(traversalFunction, _avlTree, minimum, maximum))
      {
        ReaderUnlock();
        return false;
      }
      ReaderUnlock();
      return true;
    }
    private bool TraversalInOrder(Func<ValueType, bool> traversalFunction, AvlTreeLinkedNode avltreeNode, KeyType minimum, KeyType maximum)
    {
      if (avltreeNode != null)
      {
        if (_keyComparisonFunction(avltreeNode.LeftChild.Value, minimum) >= 0)
          if (!TraversalInOrder(traversalFunction, avltreeNode.LeftChild, minimum, maximum)) return false;
        if (_keyComparisonFunction(avltreeNode.Value, minimum) >= 0 && _keyComparisonFunction(avltreeNode.Value, maximum) <= 0)
          if (!traversalFunction(avltreeNode.Value)) return false;
        if (_keyComparisonFunction(avltreeNode.RightChild.Value, maximum) <= 0)
          if (!TraversalInOrder(traversalFunction, avltreeNode.RightChild, minimum, maximum)) return false;
      }
      return true;
    }

    /// <summary>Creates an array out of the values in this structure.</summary>
    /// <returns>An array containing the values in this structure.</returns>
    /// <remarks>Runtime: Theta(n),</remarks>
    public ValueType[] ToArray() { return ToArrayInOrder(); }

    /// <summary>Puts all the items in the tree into a list by alphabetical order.</summary>
    /// <returns>The alphabetized list of items.</returns>
    /// <remarks>Runtime: Theta(n).</remarks>
    public ValueType[] ToArrayInOrder()
    {
      ReaderLock();
      ValueType[] array = new ValueType[_count];
      ToArrayInOrder(array, _avlTree, 0);
      ReaderUnlock();
      return array;
    }
    private void ToArrayInOrder(ValueType[] array, AvlTreeLinkedNode avltreeNode, int position)
    {
      if (avltreeNode != null)
      {
        ToArrayInOrder(array, avltreeNode.LeftChild, position);
        array[position++] = avltreeNode.Value;
        ToArrayInOrder(array, avltreeNode.RightChild, position);
      }
    }

    /// <summary>Puts all the items in the tree into a list by reverse alphabetical order.</summary>
    /// <returns>The alphabetized list of items.</returns>
    /// <remarks>Runtime: Theta(n).</remarks>
    public ValueType[] ToArrayPostOrder()
    {
      ReaderLock();
      ValueType[] array = new ValueType[_count];
      ToArrayPostOrder(array, _avlTree, 0);
      ReaderUnlock();
      return array;
    }
    private void ToArrayPostOrder(ValueType[] array, AvlTreeLinkedNode avltreeNode, int position)
    {
      if (avltreeNode != null)
      {
        ToArrayInOrder(array, avltreeNode.RightChild, position);
        array[position++] = avltreeNode.Value;
        ToArrayInOrder(array, avltreeNode.LeftChild, position);
      }
    }

    /// <summary>Thread safe enterance for readers.</summary>
    private void ReaderLock() { lock (_lock) { while (!(_writers == 0)) Monitor.Wait(_lock); _readers++; } }
    /// <summary>Thread safe exit for readers.</summary>
    private void ReaderUnlock() { lock (_lock) { _readers--; Monitor.Pulse(_lock); } }
    /// <summary>Thread safe enterance for writers.</summary>
    private void WriterLock() { lock (_lock) { while (!(_writers == 0) && !(_readers == 0)) Monitor.Wait(_lock); _writers++; } }
    /// <summary>Thread safe exit for readers.</summary>
    private void WriterUnlock() { lock (_lock) { _writers--; Monitor.PulseAll(_lock); } }

    /// <summary>This is used for throwing AVL Tree exceptions only to make debugging faster.</summary>
    private class AvlTreeLinkedException : Exception { public AvlTreeLinkedException(string message) : base(message) { } }
  }

  #endregion
}