# Aquarium.NET 

A collection of windows service frameworks, using lots of technologies.
* TopShelf
* Quartz.NET
* RabbitMQ
* EasyNetQ
* Castle Windsor


---- 
### SeaSnake
Create producers that send messages to a RabbitMq queue. Configuration based plugin system to allow many message sources to fire on different schedules within a single service. Uses EasyNetQ

### Octopus
Create consumers that handle messages from a RabbitMq queue. Configuration based plugin system to link consumers to message types in a single service. Uses EasyNetQ.

### TriggerFish
Create a scheduled job and configure triggers to run the job whenever needed. Uses TopShelf, Quartz.NET, CastleWindsor.


----
### Todo
* Sort out the logging and console.writelines with log4net calls
* Put seasnake mappings into config 
