namespace Problem03.Queue
{
    public class Node<T>
    {
        public Node(T element, Node<T> next)
        {
            Element = element;
            Next = next;
        }
        public Node(T element) : this(element, null)
        {
            Element = element;
            Next = null;
        }
        public T Element { get; set; }
        public Node<T> Next { get; set; }

    }
}