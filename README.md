# Kitchen-Test-3D
This project is focused on decoupled design, SOLID principles, and Game Programming Patterns.
This allows for low cognitive complexity and high scalability.

Some of the Game Programming Patterns I used:
The Command pattern allows players to customize their controls. This also allows caching the inputs for additional functionality, such as an undo/redo system. 
The Observer pattern  decouples event-triggering objects from listeners who need to be notified of an event.
The Type-Object pattern separates objects from their Type data allowing flexibility in storing and accessing data without having to instantiate a Gameobject. This also makes managing a large number of subTypes manageable as they can utilize data from the Type without having to create copies of the data for every instantiation.
