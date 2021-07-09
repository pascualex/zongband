using UnityEngine;
using System;

public class MinHeap<T> where T: IComparable<T>
{
    private T[] items;
    public int ItemCount { get; private set; } = 0;

    public MinHeap(int maxSize)
    {
        items = new T[maxSize];
    }

    public void Add(T item)
    {
        if (ItemCount >= items.Length) throw new InvalidOperationException();

        items[ItemCount] = item;
        ItemCount++;

        HeapifyUp();
    }

    public T Peek()
    {
        if (ItemCount < 1) throw new InvalidOperationException();

        return items[0];
    }

    public T Remove()
    {
        if (ItemCount < 1) throw new InvalidOperationException();

        var value = items[0];

        items[0] = items[ItemCount - 1];
        ItemCount--;
        HeapifyDown();

        return value;
    }

    private void HeapifyUp()
    {
        var i = ItemCount - 1;
        var parent = Parent(i);
        while (i > 0 && items[i].CompareTo(items[parent]) < 0)
        {
            var aux = items[i];
            items[i] = items[parent];
            items[parent] = aux;

            i = parent;
            parent = Parent(i);
        }
    }

    private void HeapifyDown()
    {
        var i = 0;
        var inOrder = false;
        while (!inOrder)
        {
            var min = i;
            var left = LeftChild(i);
            var right = RightChild(i);

            if (left < ItemCount && items[left].CompareTo(items[min]) < 0) min = left;
            if (right < ItemCount && items[right].CompareTo(items[min]) < 0) min = right;

            if (min != i)
            {
                var aux = items[i];
                items[i] = items[min];
                items[min] = aux;

                i = min;
            }
            else inOrder = true;
        }
    }

    private int Parent(int index)
    {
        return (index - 1) / 2;
    }

    private int LeftChild(int index)
    {
        return (index * 2) + 1;
    }

    private int RightChild(int index)
    {
        return (index * 2) + 2;
    }
}
