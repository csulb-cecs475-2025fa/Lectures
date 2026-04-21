using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("Async/Await Lecture Demo");
        Console.WriteLine("Demonstrating asynchronous execution for I/O-bound tasks");
        Console.WriteLine("Building on concepts of sequential, concurrent, parallel, and distributed computing");
        Console.WriteLine();

        // Synchronous demo: Sequential execution, blocking the main thread
        Console.WriteLine("=== Synchronous Demo (Sequential Execution with Dependencies) ===");
        var sw = Stopwatch.StartNew();
        SynchronousDemo();
        sw.Stop();
        Console.WriteLine($"Total time: {sw.ElapsedMilliseconds} ms");
        Console.WriteLine("Tasks run sequentially with dependencies, fully blocking.");
        Console.WriteLine();

        // Threaded demo: Parallel execution using Thread objects
        Console.WriteLine("=== Threaded Demo (Parallel Execution with Dependencies) ===");
        sw = Stopwatch.StartNew();
        ThreadedDemo();
        sw.Stop();
        Console.WriteLine($"Total time: {sw.ElapsedMilliseconds} ms");
        Console.WriteLine("Tasks 1&2 run concurrently, then Task 3 runs after both complete.");
        Console.WriteLine();

        // Async demo: Asynchronous execution using async/await with task dependencies
        Console.WriteLine("=== Async Demo (Asynchronous Execution with Dependencies) ===");
        sw = Stopwatch.StartNew();
        AsyncDemo().Wait(); // Wait for async demo to complete
        sw.Stop();
        Console.WriteLine($"Total time: {sw.ElapsedMilliseconds} ms");
        Console.WriteLine("Tasks run asynchronously with dependencies, efficient for I/O workflows.");
        Console.WriteLine();
    }

    // Synchronous method: Simulates I/O-bound work sequentially with dependencies
    static void SynchronousDemo()
    {
        Console.WriteLine("Starting synchronous I/O tasks with dependencies...");
        var result1 = SimulateIO("Task 1");
        var result2 = SimulateIO("Task 2");
        SimulateIO($"Task 3 (depends on {result1} and {result2})");
        Console.WriteLine("Synchronous tasks completed.");
    }

    // Simulates I/O-bound operation using Thread.Sleep (blocking), now returns result
    static string SimulateIO(string taskName)
    {
        Console.WriteLine($"{taskName} starting I/O simulation...");
        Thread.Sleep(1000); // Simulate 1 second I/O operation
        var result = $"{taskName} result";
        Console.WriteLine($"{taskName} I/O simulation completed. Result: {result}");
        return result;
    }

    // Threaded method: Uses Thread objects for parallelism with dependencies
    static void ThreadedDemo()
    {
        Console.WriteLine("Starting threaded I/O tasks with dependencies...");
        // Task 1 and Task 2 are independent and run concurrently
        string? result1 = null;
        string? result2 = null;
        var t1 = new Thread(() => result1 = SimulateIO("Threaded Task 1"));
        var t2 = new Thread(() => result2 = SimulateIO("Threaded Task 2"));
        t1.Start();
        t2.Start();
        // Wait for both independent tasks to complete
        t1.Join();
        t2.Join();
        // Task 3 depends on both Task 1 and Task 2
        SimulateIO($"Threaded Task 3 (depends on {result1 ?? "[null]"} and {result2 ?? "[null]"})");
        Console.WriteLine("Threaded tasks completed.");
    }

    // Async method: Uses async/await for asynchronous execution with dependencies
    static async Task AsyncDemo()
    {
        Console.WriteLine("Starting async I/O tasks with dependencies...");
        // Task 1 and Task 2 are independent and run concurrently
        var task1 = SimulateAsyncIO("Async Task 1");
        var task2 = SimulateAsyncIO("Async Task 2");
        // Wait for both independent tasks to complete. There are two options:
        // var result1 = await task1;
        // var result2 = await task2;
        // This approach emphasizes that "await" returns the result of the async function.

        // OR:
        await Task.WhenAll(task1, task2);
        var result1 = task1.Result;
        var result2 = task2.Result;
        // This approach is a little cleaner and emphasizes that both tasks are needed
        // before we go on.


        // Task 3 depends on both Task 1 and Task 2
        var task3 = SimulateAsyncIO($"Async Task 3 (depends on Task 1: {result1} and Task 2: {result2})");
        var result3 = await task3;
        Console.WriteLine($"task3 reult: {result3}");
        Console.WriteLine("Async tasks with dependencies completed.");
    }

    // Async I/O simulation: Non-blocking using Task.Delay, now returns a result
    static async Task<string> SimulateAsyncIO(string taskName)
    {
        Console.WriteLine($"{taskName} starting async I/O simulation...");
        await Task.Delay(1000); // Simulate 1 second I/O operation asynchronously
        var result = $"{taskName} result";
        Console.WriteLine($"{taskName} async I/O simulation completed. Result: {result}");
        return result;
    }
}
