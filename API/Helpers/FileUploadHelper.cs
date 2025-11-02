namespace Praticse.Helpers;

using System;
using System.IO;
public class FileUploadHelper
{
    public static async Task<string> SaveFile(IFormFile imageFile, string dir)
    {
        string finalDirPath = $"uploads/{dir}";
        if (imageFile == null || imageFile.Length == 0)
        {
            throw new Exception();
        }

        if (!Directory.Exists(finalDirPath))
        {
            Directory.CreateDirectory(finalDirPath);
        }
        //extract extension from file
        string fileExtension = Path.GetExtension(imageFile.FileName);

        //genrate unique file name
        string uniqueNameForFile = $"{Guid.NewGuid().ToString()}.{fileExtension}";

        //get full path which we will store in db ( we dont need to store from wwwroot)
        string fullPathToStoreInDB = $"{uniqueNameForFile}";

        //get path where we store image means wwwroot
        string fullPathToWrite = $"{finalDirPath}/{uniqueNameForFile}";


        // use stream to manipulate or save image in disk
        FileStream stream = new FileStream(fullPathToWrite, FileMode.CreateNew);
        await imageFile.CopyToAsync(stream);
        stream.Close();

        //retrurn path which we will store in db
        return fullPathToStoreInDB;

    }
    public static string DeleteFileFromUrl(string filePath)
    {
        // Validates that the file path is not empty
        if (string.IsNullOrWhiteSpace(filePath))
            return "File URL is required.";

        // Constructs the physical path to the file in the wwwroot/filepath directory
        var finalFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);
        Console.WriteLine(finalFilePath);
        // Checks if the file exists
        if (!System.IO.File.Exists(finalFilePath))
            return "File not found.";

        try
        {
            // Deletes the file from the file system
            System.IO.File.Delete(finalFilePath);
            return "File deleted successfully.";
        }
        catch (Exception ex)
        {
            // Rethrows the exception to be handled by the caller
            throw;
        }
    }
}