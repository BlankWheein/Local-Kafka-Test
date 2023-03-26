# KafkaTest
 
# Image example
In this image example you can see the producer message that will be converted to Protobuf in red.
The blue message is the Protobuf byte array that the consumer consumes, and the green message is what the consumer converted the byte array into
![image](https://user-images.githubusercontent.com/47646799/221692122-82db8c1c-0617-4210-82ab-e64e285d9031.png)

This kafka test uses a producer and consumer in hosted services in C#.

# Producer
The producer produces a new message every second to topic1, this is done in an async Task factory to prevent it from blocking the process.

# Consumer
The consumer subscribes to the topic1 topic, it then listens to that topic until it recieves a message. When it recieves a message it will console log it.

# Protobuf
Both the Consumer and Producer are using Protobuf for the serialization, when the producer sends a message it will convert it into Protobuf byte array before sending. When the Consumer consumes a message it will convert the byte array into the original class.
