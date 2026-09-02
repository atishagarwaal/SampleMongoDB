## Overview

This sample project demonstrates how to store and retrieve files in MongoDB from a C# application. It includes examples for uploading files to MongoDB (GridFS) and downloading them back to the local filesystem.

## Features

- Upload files to MongoDB
- Download files from MongoDB
- Example C#/.NET project showing the usage

## Prerequisites

- .NET SDK (6.0 or later recommended)
- MongoDB server (local or MongoDB Atlas)
- A MongoDB connection string with appropriate credentials

## Configuration

Update the MongoDB connection string and database name in the application's configuration (for example, appsettings.json):

```json
{
  "MongoDb": {
    "ConnectionString": "mongodb://localhost:27017",
    "Database": "sample_files_db"
  }
}
```

If the project uses environment variables, set the appropriate variable instead of appsettings.json.

## C# examples

Below are minimal C# examples showing how to upload and download files using MongoDB GridFS via the official MongoDB.Driver package.

Example: Upload and download using GridFS (async)

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

public class GridFsExample
{
    public static async Task RunAsync()
    {
        var connectionString = "mongodb://localhost:27017"; // replace with your connection string
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("sample_files_db");
        var bucket = new GridFSBucket(database);

        // Upload a file
        using (var sourceStream = File.OpenRead("path/to/local/file.jpg"))
        {
            ObjectId id = await bucket.UploadFromStreamAsync("file.jpg", sourceStream);
            Console.WriteLine($"Uploaded file id: {id}");
        }

        // Download the file by id
        // Replace with the actual ObjectId returned above
        var fileId = new ObjectId("614c1b8f1a4e4b6f9a0b1234");
        using (var destination = File.Create("downloads/file.jpg"))
        {
            await bucket.DownloadToStreamAsync(fileId, destination);
            Console.WriteLine("Downloaded file to downloads/file.jpg");
        }
    }
}
```

Synchronous example (simplified):

```csharp
using System.IO;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

var client = new MongoClient("mongodb://localhost:27017");
var db = client.GetDatabase("sample_files_db");
var bucket = new GridFSBucket(db);

// Upload
using (var s = File.OpenRead("path/to/file.txt"))
{
    var id = bucket.UploadFromStream("file.txt", s);
    Console.WriteLine($"Uploaded id: {id}");
}

// Download
using (var outStream = File.Create("out/file.txt"))
{
    bucket.DownloadToStreamByName("file.txt", outStream);
}
```

Adjust paths and identifiers to match your application and storage strategy.

## Build and run

From the repository root, build and run the project:

```bash
dotnet build
dotnet run --project ./<YourProjectFolder>
```

Replace `<YourProjectFolder>` with the path to the C# project in this repo (for example: `src/SampleMongoDB`).

## Usage

- Upload a file: run the upload command or use the provided UI (if present) and provide the path to the file. The app will store the file in MongoDB and return an identifier (ObjectId or filename).

- Download a file: run the download command or use the UI and provide the stored identifier. The application will retrieve the file and save it locally.

Refer to the sample code in the repository to find exact method names and command-line options.

## Notes

- If the project uses GridFS, files are stored in chunks; ensure your MongoDB deployment supports GridFS.
- For production, secure your MongoDB credentials and use TLS/SSL (especially with Atlas). Use role-based access control and network restrictions.
