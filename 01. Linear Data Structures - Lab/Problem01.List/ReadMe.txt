1.	List
Your task is to implement the ADS IAbstractList<T> inside the List<T> class provided. 

You have to implement all the methods in order to solve the problem, 
however you are free to add more methods with any access modifier you want.

•	void Add(T item)
o	Adds an element at the end of the sequence. 
o	This method should in addition increase the size of the structure and ensure that there is 
enough space for the addition to work.
o	If needed you will have to resize the array.

•	int Count 
o	 Returns the number of elements.

•	void Insert(int index, T item) 
o	Inserts the passed element at the specified index in the sequence (if possible). 
o	If the index is outside of the sequence bounds, throw an IndexOutOfRangeException.

•	Get (indexer)
o	Returns the element at the given index and does not remove it from the collection. 
o	If the index is invalid throw IndexOutOfRangeException with a proper message of your chose 
(the message itself in not subjected to testing).

•	Set (indexer)
o	Sets the element at given index. Again you should validate the index and throw 
IndexOutOfRangeException if the validation fails.

•	void RemoveAt(int index) 
o	Removes the element at specified index. 
o	If the index is outside of the sequence bounds, throw an IndexOutOfRangeException.

•	bool Contains (T item) 
o	 Returns true or false if the element is present inside the structure. 

•	int IndexOf(T item) 
o	Returns the index of an element if the element is not present in the structure then return -1 as invalid array index.

•	bool Remove (T item) 
o	Removes the first occurrence of the passed item and returns true.
o	If it is not present in the list, return false.
