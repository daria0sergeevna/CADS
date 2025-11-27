class PriorityQueueComparer<T> : IComparer<T>
{
    public virtual int Compare(T a, T b)
    {
        return Comparer<T>.Default.Compare(a, b);
    }
}