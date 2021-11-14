# Dead Letter Redemption

Project for redriving messages from dead letter queue for RabbitMQ.

Application should read messages from DLQ, index it by some field, and store it to storage. Then it is possible to redrive all messages or specific by select field to main queue. 

Project is under development.
