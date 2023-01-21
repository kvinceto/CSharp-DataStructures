1.	Description
You have to implement a structure that keeps track of the Microsystem store for computers. Your structure will have to support the following functionalities:
•	CreateComputer(computer) – you have to create a new computer and add it to the store. If there is already a computer with that number, throw ArgumentException
•	Contains(int number) – checks if a computer with the provided number exists in the store. 
•	Count() – returns the count of computers in the store
•	GetComputer(number) – returns the computer with the given number. If there isn’t such throw ArgumentException.
•	Remove(int number) – removes the computer with the provided number. If there isn’t such throw ArgumentException
•	RemoveWithBrand(brand) – removes all computers with the given brand. If there aren’t any throw ArgumentException
•	UpgradeRam(ram, number) – finds the computer with the given number and sets its ram to the given one (only if the given one is bigger). If there isn’t a computer with the provided number throw ArgumentException
•	GetAllFromBrand(brand) – finds all computers with the provided brand. Order them by price descending. If there aren’t any return empty collection.
•	GetAllWithScreenSize(screenSize) – finds all computers with screen size equal to the given. Order them by number descending. If there aren’t any return empty collection.
•	GetAllWithColor(color) – finds all computers with the same color as the given. Order them by price descending. If there aren’t any return empty collection.
•	GetInRangePrice(minPrice, maxPrice) – finds all computers with price between the given inclusive. Order them by price descending. If there aren’t any return empty collection.
Feel free to override Equals() and GetHashCode() if necessary.

2.	Input/Output
You are given a Visual Studio C# project skeleton (unfinished project) holding the interface IMicrosystems, the classes Microsystem and Computer. Tests covering the Microsystems functionality and performance.
Your task is to finish this class to make the tests run correctly.
•	You are not allowed to change the tests.
•	You are not allowed to change the interface.
•	You can add to the Computer class, but don't remove anything.
•	You can edit the Microsystems class if it implements the IMicrosystems interface.
