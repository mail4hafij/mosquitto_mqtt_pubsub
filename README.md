# Mosquitto MQTT Publish and Subscribe
Simple demonstration of Mosquitto MQTT broker.

## Run Mosquitto with docker-compose in a windows machine

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

