1.	Barber Shop
You are given a skeleton with a class BarberShop that implements the IBarberShop interface. 
The BarberShop works with Barber & Client entities, all barbers and clients are identified by their unique names. Implements all the operations from the interface:
●	void AddBarber(Barber b) – adds a barber. If there is a barber with the same name added before, throw ArgumentException().
●	void AddClient(Client c) – adds a client. If a client with the same name exists, throw ArgumentException().
●	bool Exist(Barber b) – returns whether the Barber has been added or not
●	bool Exist(Client c) – returns whether the Client has been added or not
●	IEnumerable<Barber> GetBarbers() – returns all added barbers. If there aren’t any - return empty collection
●	IEnumerable<Client> GetClients() – returns all added clients. If there aren’t any - return empty collection
●	void AssignClient(Barber b, Client c) – adds a client  for the provided barber. If the barber or the client does not exist, throw ArgumentException()
●	void DeleteAllClientsFrom(Barber b) – Deletes all assigned clients for the provided barber. If the barber does not exist throw ArgumentException()
●	IEnumerable<Client> GetClientsWithNoBarber() – return only clients with no assigned barber
●	IEnumerable<Barber> GetAllBarbersSortedWithClientsCountDesc() – return all added barbers ordered by their clients count descending. If there are not any barbers return empty collection
●	IEnumerable<Barber> GetAllBarbersSortedWithStarsDecsendingAndHaircutPriceAsc() – returns all barbers sorted by their stars descending and their haircut price ascending
●	IEnumerable<Client> GetClientsSortedByAgeDescAndBarbersStarsDesc() – return only clients who are assigned to berber and sorted by their age descending and by their barber stars descending

2.	Barber Shop – Performance
For this task you will only be required to submit the code from the previous problem. If you are having a problem with this task you should perform detailed algorithmic complexity analysis, and try to figure out weak spots inside your implementation.
For this problem it is important that other operations are implemented correctly according to the specific problems:  add, size, remove, get etc…
You can submit code to this problem without full coverage from the previous problem, not all test cases will be considered, only the general behaviour will be important, edge cases will mostly be ignored such as throwing exceptions etc…


