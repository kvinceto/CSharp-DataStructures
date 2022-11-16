namespace Problem04.SinglyLinkedList
{
    public class Node<T>
    {
        public Node(T element, Node<T> next)
        {
            Element = element;
            Next = next;
        }
        public T Element { get; set; }
        public Node<T> Next { get; set; }
    }
}