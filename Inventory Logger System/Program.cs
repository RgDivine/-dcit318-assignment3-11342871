






using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Define the marker interface
public interface IInventoryEntity
{
    int Id { get; }
}

// Define the immutable InventoryItem record
public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;

// Generic InventoryLogger class
public class InventoryLogger<T> where T : IInventoryEntity
{
    private List<T> _log = new List<T>();
    private readonly string _filePath;

    public InventoryLogger(string filePath)
    {
        _filePath = filePath;
    }

    public void Add(T item)
    {
        _log.Add(item);
        Console.WriteLine($"Added item with ID {item.Id} to the log.");
    }

    public List<T> GetAll()
    {
        return new List<T>(_log);
    }

    public void SaveToFile()
    {
        try
        {
            using (var writer = new StreamWriter(_filePath))
            {
                var json = JsonSerializer.Serialize(_log, new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });
                writer.Write(json);
            }
            Console.WriteLine($"Data saved to file: {_filePath}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Access denied while saving file: {ex.Message}");
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine($"Directory not found while saving file: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"I/O error while saving file: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while saving file: {ex.Message}");
        }
    }

    public void LoadFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine($"File {_filePath} does not exist. Starting with empty log.");
                _log.Clear();
                return;
            }

            using (var reader = new StreamReader(_filePath))
            {
                var json = reader.ReadToEnd();
                if (string.IsNullOrWhiteSpace(json))
                {
                    Console.WriteLine("File is empty. Starting with empty log.");
                    _log.Clear();
                    return;
                }

                var loadedItems = JsonSerializer.Deserialize<List<T>>(json);
                _log = loadedItems ?? new List<T>();
            }
            Console.WriteLine($"Data loaded from file: {_filePath}. Items count: {_log.Count}");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File not found while loading: {ex.Message}");
            _log.Clear();
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Access denied while loading file: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON parsing error while loading file: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"I/O error while loading file: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while loading file: {ex.Message}");
        }
    }

    public void ClearLog()
    {
        _log.Clear();
        Console.WriteLine("Log cleared from memory.");
    }
}

// Integration layer - InventoryApp
public class InventoryApp
{
    private readonly InventoryLogger<InventoryItem> _logger;

    public InventoryApp()
    {
        _logger = new InventoryLogger<InventoryItem>("inventory_log.json");
    }

public void SeedSampleData()
    {
        Console.WriteLine("\n=== Seeding Sample Data ===");
        
        var items = new[]
        {
            new InventoryItem(1, "Laptop", 10, DateTime.Now.AddDays(-5)),
            new InventoryItem(2, "Mouse", 50, DateTime.Now.AddDays(-3)),
            new InventoryItem(3, "Keyboard", 25, DateTime.Now.AddDays(-2)),
            new InventoryItem(4, "Monitor", 8, DateTime.Now.AddDays(-1)),
            new InventoryItem(5, "Headphones", 30, DateTime.Now)
        };

        foreach (var item in items)
        {
            _logger.Add(item);
        }
    }

    public void SaveData()
    {
        Console.WriteLine("\n=== Saving Data ===");
        _logger.SaveToFile();
    }

    public void LoadData()
    {
        Console.WriteLine("\n=== Loading Data ===");
        _logger.LoadFromFile();
    }

    public void PrintAllItems()
    {
        Console.WriteLine("\n=== All Inventory Items ===");
        var items = _logger.GetAll();
        
        if (items.Count == 0)
        {
            Console.WriteLine("No items found in inventory.");
            return;
        }

        foreach (var item in items)
        {
            Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Date Added: {item.DateAdded:yyyy-MM-dd}");
        }
        
        Console.WriteLine($"Total items: {items.Count}");
    }

    public void ClearMemory()
    {
        Console.WriteLine("\n=== Simulating New Session (Clearing Memory) ===");
        _logger.ClearLog();
    }
}

// Main program
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Inventory Logger System ===");
        
        try
        {
            var app = new InventoryApp();
            
            // Seed sample data
            app.SeedSampleData();
            
            // Save data to file
            app.SaveData();
            
            // Clear memory to simulate new session
            app.ClearMemory();
            
            // Load data from file
            app.LoadData();
            
            // Print all items to confirm data was recovered
            app.PrintAllItems();
            
            Console.WriteLine("\n=== System completed successfully! ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"System error: {ex.Message}");
        }
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}