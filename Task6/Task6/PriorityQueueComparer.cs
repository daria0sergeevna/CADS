class PriorityQueueComparer<T> : IComparer<T>
{
    public int Compare(T a, T b)
    {
        return Comparer<T>.Default.Compare(a, b);
    }
}