# MasstransitMB

MasstransitMB is a message broker application using the MassTransit framework. It supports various message brokers, including RabbitMQ, Azure Service Bus, Kafka, and many others.

## Features

- **RabbitMQ**: Seamless integration with RabbitMQ.
- **Azure Service Bus**: Support for Azure's messaging infrastructure.
- **Kafka**: Integration with the distributed streaming platform, Kafka.
- **Other Brokers**: Easily extendable to support other message brokers.

## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) (version 5.0 or later)
- [RabbitMQ](https://www.rabbitmq.com/download.html)
- [Azure Service Bus](https://azure.microsoft.com/en-us/services/service-bus/)
- [Kafka](https://kafka.apache.org/quickstart)

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/sargis-tovmasyan/MasstransitMB.git
    cd MasstransitMB
    ```

2. Restore the dependencies:
    ```sh
    dotnet restore
    ```

3. Build the project:
    ```sh
    dotnet build
    ```

### Usage

To run the application, use the following command:
```sh
dotnet run
