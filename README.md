# SampleMongoDB

## 1. Overview

SampleMongoDB is a minimal C#/.NET sample that demonstrates storing and retrieving files in MongoDB (GridFS). It includes examples and code showing how to upload files into MongoDB and download them back to the local filesystem.

## 2. Description

This repository contains a .NET application that uses the official MongoDB.Driver and GridFS APIs to:

- Upload files to a MongoDB GridFS bucket
- Download files from GridFS by id or filename
- Demonstrate both synchronous and asynchronous usage patterns

The sample is intended for learning and prototyping. For production use, secure credentials and follow MongoDB best practices.

## 3. Pre-requisites

- .NET SDK 10.0 (or the SDK matching the TFM used in the project)
- A running MongoDB instance (local, Docker, or MongoDB Atlas)
- A MongoDB connection string with a database and permissions for GridFS operations

Configuration options (example appsettings.json):

```json
{
  "MongoDb": {
    "ConnectionString": "mongodb://localhost:27017",
    "Database": "sample_files_db"
  }
}
```

You can also provide the connection string and database via environment variables if preferred.

## 4. Build and Run

From the repository root:

1. Restore and build

    ```bash
    dotnet build
    ```

2. Run the project

    ```bash
    dotnet run --project ./SampleMongoDB
    ```

Replace the project path with the actual project folder if different. If the solution contains multiple projects, specify the desired project path.

Notes:

- Ensure the MongoDB instance referenced in the configuration is running and accessible.
- For development with Docker, you can run a local MongoDB instance with: `docker run --name mongo -p 27017:27017 -d mongo:latest`
- For production, enable TLS/SSL and use credentials with least privilege.

For code examples and usage details, see the sample classes in the repository.
