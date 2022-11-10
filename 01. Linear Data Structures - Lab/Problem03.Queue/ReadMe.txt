3.	Queue
Your task is to implement the ADS IAbstractQueue<T> inside the Queue<T> class provided. 

You have to implement all the methods in order to solve the problem, however you are free 
to add more methods with any access modifier you want.

•	void Enqueue(T item) 
o	Adds an element at the end of the queue and increases the size.

•	T Dequeue()
o	 Removes and returns the first element at the queue also decreases the size and performs 
a check if this method is called upon empty collection. 
o	If so throw InvalidOperationException with message of your choice.

•	T Peek()
o	Returns the element at the current front of the queue. If the collection is empty throw 
InvalidOperationException with appropriate message.

•	int Count 
o	Returns the number of elements inside the stack.
