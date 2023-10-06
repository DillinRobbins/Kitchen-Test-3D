using System.Collections.Generic;
using System;

[System.Serializable]
public class MaxHeap
{
    public List<int> heap;
    public int capacity;

    public MaxHeap(int capacity)
    {
        this.capacity = capacity;
        heap = new List<int>(capacity);
        for(int i = 0; i < heap.Count; i++)
        {
            heap.Add(0);
        }
    }

    public void Insert(int score)
    {
        if (heap.Count >= capacity)
        {
            if (score <= heap[0])
            {
                return; // Don't insert if the score is not greater than the smallest score.
            }
            else
            {
                PopMin(); // Remove the smallest score to make space for the new score.
            }
        }

        heap.Add(score);
        int currentIndex = heap.Count - 1;

        while (currentIndex > 0)
        {
            int parentIndex = (currentIndex - 1) / 2;

            if (heap[currentIndex] <= heap[parentIndex])
            {
                break;
            }

            // Swap the current score with its parent.
            int temp = heap[currentIndex];
            heap[currentIndex] = heap[parentIndex];
            heap[parentIndex] = temp;

            currentIndex = parentIndex;
        }
    }

    public int[] GetScores()
    {
        return heap.ToArray();
    }

    public int GetHighScore()
    {
        if (heap.Count == 0) return 0;
        return heap[0];
    }

    private void PopMin()
    {
        if (heap.Count == 0)
        {
            return; // The heap is empty.
        }

        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        int currentIndex = 0;

        while (true)
        {
            int leftChild = currentIndex * 2 + 1;
            int rightChild = currentIndex * 2 + 2;
            int largest = currentIndex;

            if (leftChild < heap.Count && heap[leftChild] > heap[largest])
            {
                largest = leftChild;
            }

            if (rightChild < heap.Count && heap[rightChild] > heap[largest])
            {
                largest = rightChild;
            }

            if (largest == currentIndex)
            {
                break;
            }

            int temp = heap[currentIndex];
            heap[currentIndex] = heap[largest];
            heap[largest] = temp;

            currentIndex = largest;
        }
    }
}
