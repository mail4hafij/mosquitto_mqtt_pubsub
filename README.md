# Mosquitto MQTT Publish and Subscribe
Simple demonstration of Mosquitto MQTT broker.
There are two dotnet core projects - 
1. Publisher: publish a message to a given topic.
2. Subscriber: subscribe to a topic and receive published messages.

## How to run locally

1. Setup Mosquitto with docker-compose localy in a windows machine
- Folders and files to create
```
C:\mosquitto\config\mosquitto.conf
C:\mosquitto\log\mosquitto.log
C:\mosquitto\data
```

- The mosquitto.conf file
```
persistence true
allow_anonymous true
protocol mqtt
```

- Navigate to Mosquitto project

- To run in the background
```docker-compose up -d```

- To shutt down
```docker-compose down```


2. Build the solution and run Subscriber project first, then run the Publisher project.
