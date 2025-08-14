





using System;
using System.Collections.Generic;
using System.IO;

// Student class
public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int Score { get; set; }

    public Student(int id, string fullName, int score)
    {
        Id = id;
        FullName = fullName;
        Score = score;
    }

    public string GetGrade()
    {
        return Score switch
        {
            >= 80 and <= 100 => "A",
            >= 70 and <= 79 => "B",
            >= 60 and <= 69 => "C",
            >= 50 and <= 59 => "D",
            _ => "F"
        };
    }

    public override string ToString()
    {
        return $"{FullName} (ID: {Id}): Score = {Score}, Grade = {GetGrade()}";
    }
}

// Custom exception classes
public class InvalidScoreFormatException : Exception
{
    public InvalidScoreFormatException(string message) : base(message) { }
}

public class MissingFieldException : Exception
{
    public MissingFieldException(string message) : base(message) { }
}

// StudentResultProcessor class
public class StudentResultProcessor
{
    public List<Student> ReadStudentsFromFile(string inputFilePath)
    {
        var students = new List<Student>();
        
        using (var reader = new StreamReader(inputFilePath))
        {
            string line;
            int lineNumber = 0;
            
            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;
                
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                
                try
                {
                    var fields = line.Split(',');
                    
                    // Check for missing fields
                    if (fields.Length != 3)
                    {
                        throw new MissingFieldException($"Line {lineNumber}: Expected 3 fields but found {fields.Length}");
                    }
                    
                    // Parse ID
                    if (!int.TryParse(fields[0].Trim(), out int id))
                    {
                        throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid ID format '{fields[0].Trim()}'");
                    }
                    
                    // Get name
                    string fullName = fields[1].Trim();
                    if (string.IsNullOrEmpty(fullName))
                    {
                        throw new MissingFieldException($"Line {lineNumber}: Name field is empty");
                    }
                    
                    // Parse score
                    if (!int.TryParse(fields[2].Trim(), out int score))
                    {
                        throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid score format '{fields[2].Trim()}'");
                    }
                    
                    // Validate score range
                    if (score < 0 || score > 100)
                    {
                        throw new InvalidScoreFormatException($"Line {lineNumber}: Score {score} is out of valid range (0-100)");
                    }
                    
                    students.Add(new Student(id, fullName, score));
                }
                catch (InvalidScoreFormatException)
                {
                    throw; // Re-throw custom exceptions
                }
                catch (MissingFieldException)
                {
                    throw; // Re-throw custom exceptions
                }
                catch (Exception ex)
                {
                    throw new Exception($"Line {lineNumber}: Unexpected error - {ex.Message}");
                }
            }
        }
        
        return students;
    }

public void WriteReportToFile(List<Student> students, string outputFilePath)
    {
        using (var writer = new StreamWriter(outputFilePath))
        {
            writer.WriteLine("=== Student Grade Report ===");
            writer.WriteLine($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            writer.WriteLine($"Total Students: {students.Count}");
            writer.WriteLine();
            
            foreach (var student in students)
            {
                writer.WriteLine(student.ToString());
            }
            
            writer.WriteLine();
            writer.WriteLine("=== Grade Distribution ===");
            
            int gradeA = 0, gradeB = 0, gradeC = 0, gradeD = 0, gradeF = 0;
            
            foreach (var student in students)
            {
                switch (student.GetGrade())
                {
                    case "A": gradeA++; break;
                    case "B": gradeB++; break;
                    case "C": gradeC++; break;
                    case "D": gradeD++; break;
                    case "F": gradeF++; break;
                }
            }
            
            writer.WriteLine($"A: {gradeA} students");
            writer.WriteLine($"B: {gradeB} students");
            writer.WriteLine($"C: {gradeC} students");
            writer.WriteLine($"D: {gradeD} students");
            writer.WriteLine($"F: {gradeF} students");
        }
    }

    public void CreateSampleInputFile(string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        {
            writer.WriteLine("101,Alice Smith,84");
            writer.WriteLine("102,Bob Johnson,76");
            writer.WriteLine("103,Charlie Brown,92");
            writer.WriteLine("104,Diana Ross,68");
            writer.WriteLine("105,Edward Norton,45");
            writer.WriteLine("106,Fiona Apple,88");
            writer.WriteLine("107,George Lucas,55");
        }
    }
}

// Main program
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== School Grading System ===");
        
        var processor = new StudentResultProcessor();
        string inputFile = "students.txt";
        string outputFile = "grade_report.txt";
        
        try
        {
            // Create sample input file if it doesn't exist
            if (!File.Exists(inputFile))
            {
                processor.CreateSampleInputFile(inputFile);
                Console.WriteLine($"Sample input file '{inputFile}' created.");
            }
            
            // Read students from file
            Console.WriteLine($"Reading students from '{inputFile}'...");
            var students = processor.ReadStudentsFromFile(inputFile);
            Console.WriteLine($"Successfully read {students.Count} students.");
            
            // Write report to file
            Console.WriteLine($"Writing report to '{outputFile}'...");
            processor.WriteReportToFile(students, outputFile);
            Console.WriteLine("Report generated successfully!");
            
            // Display summary
            Console.WriteLine("\n=== Summary ===");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error: Input file not found - {ex.Message}");
        }
        catch (InvalidScoreFormatException ex)
        {
            Console.WriteLine($"Error: Invalid score format - {ex.Message}");
        }
        catch (MissingFieldException ex)
        {
            Console.WriteLine($"Error: Missing field - {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Error: Access denied - {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error: File I/O error - {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }


Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}