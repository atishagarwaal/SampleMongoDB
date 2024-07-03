using System;
using System.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace SampleMongoDB
{
    class Program
    {
        private static readonly string connectionString = "mongodb://localhost:27017/";
        private static readonly string databaseName = "Testdb";
        private static readonly string collectionName = "files";

        static void Main(string[] args)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<FileDocument>(collectionName);

            // Upload a file
            string filePath = "MyFile.txt";
            UploadFile(filePath, collection);

            // Download a file
            string filename = "MyFile.txt";
            string downloadfilename = "MyFile2.txt";
            string downloadPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\", downloadfilename);
            DownloadFile(filename, downloadPath, collection);
        }

        // Method to upload a file to MongoDB
        public static void UploadFile(string sourcefilePath, IMongoCollection<FileDocument> collection)
        {
            // Read file bytes
            byte[] fileBytes = File.ReadAllBytes(sourcefilePath);
            var fileDocument = new FileDocument
            {
                FileName = Path.GetFileName(sourcefilePath),
                FileContent = fileBytes,
                UploadDate = DateTime.Now
            };

            // Insert file document into collection
            collection.InsertOne(fileDocument);
            Console.WriteLine("File uploaded successfully.");
        }

        // Method to download a file from MongoDB by filename
        public static void DownloadFile(string fileName, string downloadPath, IMongoCollection<FileDocument> collection)
        {
            // Create filter to find by filename
            var filter = Builders<FileDocument>.Filter.Eq("FileName", fileName);

            // Find the file document
            var fileDocument = collection.Find(filter).FirstOrDefault();

            if (fileDocument != null)
            {
                // Write file bytes to disk
                File.WriteAllBytes(downloadPath, fileDocument.FileContent);
                Console.WriteLine("File downloaded successfully.");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
    }

    // MongoDB document model for file storage
    public class FileDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("FileName")]
        public string FileName { get; set; }

        [BsonElement("FileContent")]
        public byte[] FileContent { get; set; }

        [BsonElement("UploadDate")]
        public DateTime UploadDate { get; set; }
    }
}
