# Dead Letter Redemption

Project for redriving messages from dead letter queue.

Application should read messages from DLQ, index it by some field, and store it to storage. Then it is possible to redrive all messages or portion of messages to main queue. 

Project is under development, in first iteration only RabbitMQ provider will be supported.
